using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace RobloxPlayerLauncher
{
    /// <summary>An error the user should see as it is (the website's message, a missing client...).</summary>
    public class LauncherException : Exception
    {
        public int Status { get; private set; }

        public LauncherException(string message) : base(message)
        {
        }

        public LauncherException(string message, int status) : base(message)
        {
            Status = status;
        }
    }

    public class PackageInfo
    {
        public string Name { get; set; }
        public bool Available { get; set; }
        public string Version { get; set; }
        public string Sha256 { get; set; }
        public long Size { get; set; }
        public string Message { get; set; }
    }

    public class PlaceLauncherResult
    {
        public int Status { get; set; }
        public string JobId { get; set; }
        public string JoinScriptUrl { get; set; }
        public string Client { get; set; }
        public string Message { get; set; }
    }

    public class GameInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public string Md5 { get; set; }
        public int MaxPlayers { get; set; }
        public bool CanHost { get; set; }
    }

    public class HostJob
    {
        public string JobId { get; set; }
        public string ServerKey { get; set; }
        public long PlaceId { get; set; }
        public int HeartbeatSeconds { get; set; }
    }

    /// <summary>
    /// Talks to one RobloxServer website with its own cookie jar, like the 2013 launcher: the ticket from
    /// the Play button is traded for a .ROBLOSECURITY cookie (Login/Negotiate.ashx) and every later call
    /// uses that cookie. Requests never follow redirects, so nothing is fetched from another host.
    /// </summary>
    public class SiteClient
    {
        const string UserAgent = "Roblox/WinInet RobloxPlayerLauncher/2.0";

        readonly CookieContainer cookies = new CookieContainer();
        string csrfToken;

        public Uri BaseUrl { get; private set; }

        public SiteClient(Uri baseUrl)
        {
            BaseUrl = baseUrl;
        }

        Uri Resolve(string pathOrUrl)
        {
            Uri url = new Uri(BaseUrl, pathOrUrl);
            if (!LaunchRequest.SameSite(url, BaseUrl))
            {
                throw new LauncherException("Refusing to contact " + url.Host + ": it is not " + BaseUrl.Host + ".");
            }
            return url;
        }

        HttpWebRequest Create(string pathOrUrl, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(Resolve(pathOrUrl));
            request.Method = method;
            request.UserAgent = UserAgent;
            request.CookieContainer = cookies;
            request.AllowAutoRedirect = false;
            request.Timeout = 30000;
            request.ReadWriteTimeout = 60000;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (csrfToken != null)
            {
                request.Headers["X-CSRF-TOKEN"] = csrfToken;
            }
            return request;
        }

        static HttpWebResponse Send(HttpWebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response == null)
                {
                    throw new LauncherException("Could not reach " + request.RequestUri.Host + " (" + ex.Message + ").");
                }
                return response;
            }
        }

        static string ReadText(HttpWebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        static void EnsureSuccess(HttpWebResponse response, string body, string what)
        {
            int status = (int)response.StatusCode;
            if (status >= 200 && status < 300)
            {
                return;
            }
            string detail = (body ?? "").Trim();
            if (detail.Length > 200 || detail.StartsWith("<"))
            {
                detail = response.StatusDescription;
            }
            throw new LauncherException(what + " failed: " + status + " " + detail, status);
        }

        public string GetString(string pathOrUrl, string what)
        {
            using (HttpWebResponse response = Send(Create(pathOrUrl, "GET")))
            {
                string body = ReadText(response);
                EnsureSuccess(response, body, what);
                return body;
            }
        }

        /// <summary>POST a form; on the 2015 style 403 "Token Validation Failed" retries once with X-CSRF-TOKEN.</summary>
        public string PostForm(string path, IDictionary<string, string> fields, string what)
        {
            byte[] data = Encoding.UTF8.GetBytes(string.Join("&", fields.Select(f => Uri.EscapeDataString(f.Key) + "=" + Uri.EscapeDataString(f.Value ?? ""))));
            for (int attempt = 0; ; attempt++)
            {
                HttpWebRequest request = Create(path, "POST");
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (HttpWebResponse response = Send(request))
                {
                    string body = ReadText(response);
                    string token = response.Headers["X-CSRF-TOKEN"];
                    if ((int)response.StatusCode == 403 && !string.IsNullOrEmpty(token) && attempt == 0)
                    {
                        csrfToken = token;
                        continue;
                    }
                    EnsureSuccess(response, body, what);
                    return body;
                }
            }
        }

        /// <summary>Downloads to <paramref name="path"/>, reporting (received, total) bytes.</summary>
        public void Download(string pathOrUrl, string path, Action<long, long> progress, CancellationToken cancel)
        {
            HttpWebRequest request = Create(pathOrUrl, "GET");
            request.Timeout = 60000;
            request.AutomaticDecompression = DecompressionMethods.None;
            using (HttpWebResponse response = Send(request))
            {
                if ((int)response.StatusCode != 200)
                {
                    EnsureSuccess(response, ReadText(response), "Download");
                }

                long total = response.ContentLength;
                long received = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (Stream input = response.GetResponseStream())
                using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[81920];
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cancel.ThrowIfCancellationRequested();
                        output.Write(buffer, 0, read);
                        received += read;
                        if (progress != null)
                        {
                            progress(received, total);
                        }
                    }
                }
            }
        }

        // ----------------------------------------------------------------- website API

        /// <summary>Trades the Play button's one-time ticket for a .ROBLOSECURITY cookie.</summary>
        public void Negotiate(string ticket)
        {
            using (HttpWebResponse response = Send(Create("Login/Negotiate.ashx?suggest=" + Uri.EscapeDataString(ticket), "GET")))
            {
                string body = ReadText(response);
                if ((int)response.StatusCode == 403)
                {
                    throw new LauncherException("Your login ticket expired or was already used. Go back to the website and click the button again.", 403);
                }
                EnsureSuccess(response, body, "Login");
            }
        }

        public PackageInfo GetPackage(string name)
        {
            using (HttpWebResponse response = Send(Create("Install/Version.ashx?client=" + Uri.EscapeDataString(name), "GET")))
            {
                string body = ReadText(response);
                int status = (int)response.StatusCode;
                if (status != 200 && status != 404)
                {
                    EnsureSuccess(response, body, "Version check");
                }

                Dictionary<string, object> json;
                try
                {
                    json = Json.Parse(body);
                }
                catch (FormatException)
                {
                    // An older website without /install/: nothing to install from it.
                    return new PackageInfo { Name = name, Available = false, Message = "This website cannot install the " + name + " client." };
                }

                return new PackageInfo
                {
                    Name = json.Str("client") ?? name,
                    Available = json.Bool("available"),
                    Version = json.Str("version"),
                    Sha256 = (json.Str("sha256") ?? "").ToLowerInvariant(),
                    Size = json.Long("size"),
                    Message = json.Str("message")
                };
            }
        }

        public PlaceLauncherResult RequestGame(Uri placeLauncherUrl)
        {
            Dictionary<string, object> json = Json.Parse(GetString(placeLauncherUrl.ToString(), "PlaceLauncher"));
            return new PlaceLauncherResult
            {
                Status = json.Int("status"),
                JobId = json.Str("jobId"),
                JoinScriptUrl = json.Str("joinScriptUrl"),
                Client = json.Str("client"),
                Message = json.Str("message")
            };
        }

        public GameInfo GetGame(long placeId)
        {
            Dictionary<string, object> json = Json.Parse(GetString("Api/Games.ashx?id=" + placeId, "Game lookup"));
            Dictionary<string, object> game = json.Objects("data").FirstOrDefault();
            if (game == null)
            {
                throw new LauncherException("This game does not exist or is private.");
            }
            return new GameInfo
            {
                Id = game.Long("id"),
                Name = game.Str("name") ?? ("Place " + placeId),
                Client = game.Str("client"),
                Md5 = game.Str("md5") ?? "",
                MaxPlayers = game.Int("maxPlayers"),
                CanHost = game.Bool("canHost")
            };
        }

        public HostJob RegisterServer(long placeId, int port, int maxPlayers, string address, string lanAddress)
        {
            var fields = new Dictionary<string, string>
            {
                { "action", "register" },
                { "placeId", placeId.ToString() },
                { "port", port.ToString() },
                { "maxPlayers", maxPlayers.ToString() },
                { "address", address ?? "" },
                { "lanAddress", lanAddress ?? "" },
                { "version", "RobloxPlayerLauncher/2.0" }
            };
            Dictionary<string, object> json = Json.Parse(PostForm("Game/Servers.ashx", fields, "Server registration"));
            if (!json.Bool("success"))
            {
                throw new LauncherException(json.Str("message") ?? "The website did not accept the game server.");
            }
            return new HostJob
            {
                JobId = json.Str("jobId"),
                ServerKey = json.Str("serverKey"),
                PlaceId = json.Long("placeId"),
                HeartbeatSeconds = Math.Max(15, json.Int("heartbeatSeconds"))
            };
        }

        /// <summary>
        /// This PC's private IPv4 on the network it reaches the Internet through, or null. The website hands it
        /// to players that come from the same public IP (same router), because most home routers cannot loop a
        /// connection back to their own public IP. A UDP "connect" only picks the route; nothing is sent.
        /// </summary>
        public static string DetectLanAddress()
        {
            try
            {
                using (var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                    System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp))
                {
                    socket.Connect("8.8.8.8", 53);
                    var local = socket.LocalEndPoint as IPEndPoint;
                    if (local == null)
                    {
                        return null;
                    }
                    byte[] b = local.Address.GetAddressBytes();
                    bool isPrivate = b[0] == 10
                        || (b[0] == 172 && b[1] >= 16 && b[1] <= 31)
                        || (b[0] == 192 && b[1] == 168)
                        || (b[0] == 100 && b[1] >= 64 && b[1] <= 127);
                    return isPrivate ? local.Address.ToString() : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        string JobQuery(HostJob job)
        {
            return "jobId=" + Uri.EscapeDataString(job.JobId) + "&serverKey=" + Uri.EscapeDataString(job.ServerKey);
        }

        public void Heartbeat(HostJob job)
        {
            GetString("Game/Servers.ashx?action=heartbeat&" + JobQuery(job), "Heartbeat");
        }

        public void Unregister(HostJob job)
        {
            GetString("Game/Servers.ashx?action=unregister&" + JobQuery(job), "Unregister");
        }

        public string GetGameServerScript(HostJob job, bool loadPlace)
        {
            return GetString("Game/GameServer.ashx?" + JobQuery(job) + "&loadPlace=" + (loadPlace ? "true" : "false"), "Game server script");
        }

        public int GetPlayerCount(HostJob job)
        {
            Dictionary<string, object> json = Json.Parse(GetString("Api/Servers.ashx?placeId=" + job.PlaceId, "Server list"));
            Dictionary<string, object> server = json.Objects("data").FirstOrDefault(s => string.Equals(s.Str("jobId"), job.JobId, StringComparison.OrdinalIgnoreCase));
            return server != null ? server.Int("players") : 0;
        }
    }
}
