using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RobloxPlayerLauncher
{
    /// <summary>
    /// Does what the 2013 RobloxPlayerLauncher did after the Play button: log in with the ticket,
    /// ask PlaceLauncher.ashx for a server, install/update the client and start it with the join
    /// script. "Host Server" registers a job instead and starts the client as the game server.
    /// Runs on a worker thread; progress goes through <see cref="Report"/>.
    /// </summary>
    public class GameStarter
    {
        readonly LaunchRequest request;
        readonly Settings settings;
        readonly SiteClient site;

        /// <summary>(status text, percent or -1 for "busy").</summary>
        public Action<string, int> Report { get; set; }

        public GameStarter(LaunchRequest request, Settings settings)
        {
            this.request = request;
            this.settings = settings;
            site = new SiteClient(request.BaseUrl);
            Report = (text, percent) => { };
        }

        public SiteClient Site
        {
            get { return site; }
        }

        /// <summary>Starts the game. Returns the running game server for Host, null for Play.</summary>
        public GameHost Run(CancellationToken cancel)
        {
            Report("Connecting to " + request.BaseUrl.Host + "...", -1);
            site.Negotiate(request.Ticket);
            cancel.ThrowIfCancellationRequested();

            return request.Mode == LaunchMode.Host ? Host(cancel) : Play(cancel);
        }

        GameHost Play(CancellationToken cancel)
        {
            PlaceLauncherResult game = null;
            for (int attempt = 0; attempt < 10; attempt++)
            {
                Report(attempt == 0 ? "Requesting a server..." : "Waiting for a server...", -1);
                game = site.RequestGame(request.PlaceLauncherUrl);
                // 0 and 1: no server yet (someone may be starting one), ask again like the 2013 launcher.
                if (game.Status != 0 && game.Status != 1)
                {
                    break;
                }
                cancel.WaitHandle.WaitOne(2000);
                cancel.ThrowIfCancellationRequested();
            }

            if (game.Status != 2 || string.IsNullOrEmpty(game.JoinScriptUrl))
            {
                throw new LauncherException(game.Message ?? ("The game could not be started (status " + game.Status + ")."));
            }

            string folder = ClientInstaller.Ensure(site, game.Client, Report, cancel);
            ClientManifest manifest = ClientManifest.Load(folder);
            string exe = manifest.ExeFor(LaunchMode.Play);

            Report("Joining game...", -1);
            string script = site.GetString(game.JoinScriptUrl, "Join script");
            string scriptPath = WriteScript(exe, "join", script);

            string args = manifest.ArgsFor(LaunchMode.Play, Values(scriptPath, null, 0));
            Report("Starting the game...", -1);
            if (!ExitedEarly(Start(exe, args), args))
            {
                return null;
            }

            // The client died right away. Retry once the way the 2010-era clients start their own test
            // players (loadfile(...)() instead of dofile(...)); both attempts are written to launcher.log.
            // loadfile() takes a plain file path (not rbxasset://), and in a Lua string a backslash starts an
            // escape: pass the real path of the generated script with forward slashes.
            string scriptFile = scriptPath.Replace('\\', '/');
            string retry = Regex.Replace(args, @"dofile\('([^']*)'\)", m => "loadfile('" + scriptFile + "')()");
            if (retry != args)
            {
                Report("Retrying...", -1);
                if (!ExitedEarly(Start(exe, retry), retry))
                {
                    return null;
                }
            }
            throw new LauncherException("The game closed right after starting. The exit code is in " + Paths.LogFile + ".");
        }

        /// <summary>True when the client exits within a few seconds of starting; logs the exit code.</summary>
        static bool ExitedEarly(Process process, string arguments)
        {
            if (!process.WaitForExit(8000))
            {
                return false;
            }
            Paths.Log("The client exited right after starting, exit code " + process.ExitCode + " (0x" + process.ExitCode.ToString("X8") + "). Arguments: " + arguments);
            return true;
        }

        GameHost Host(CancellationToken cancel)
        {
            Report("Loading game details...", -1);
            GameInfo game = site.GetGame(request.PlaceId);
            if (!game.CanHost)
            {
                throw new LauncherException("You are not allowed to host a server for " + game.Name + ".");
            }

            string folder = ClientInstaller.Ensure(site, game.Client, Report, cancel);
            ClientManifest manifest = ClientManifest.Load(folder);
            string exe = manifest.ExeFor(LaunchMode.Host);

            string placePath = null;
            if (!manifest.ServerLoadsPlace)
            {
                placePath = DownloadPlace(game, cancel);
            }
            cancel.ThrowIfCancellationRequested();

            Report("Starting the game server...", -1);
            string lanAddress = string.IsNullOrWhiteSpace(settings.HostLanAddress) ? SiteClient.DetectLanAddress() : settings.HostLanAddress.Trim();
            HostJob job = site.RegisterServer(game.Id, settings.HostPort, game.MaxPlayers, settings.HostAddress, lanAddress);
            try
            {
                string script = site.GetGameServerScript(job, manifest.ServerLoadsPlace);
                string scriptPath = WriteScript(exe, "gameserver", script);
                Process process = Start(exe, manifest.ArgsFor(LaunchMode.Host, Values(scriptPath, placePath, settings.HostPort)));
                Paths.Log("Hosting place " + game.Id + " as job " + job.JobId + " on port " + settings.HostPort);
                return new GameHost(site, job, process, game.Name, settings.HostPort);
            }
            catch (Exception)
            {
                try
                {
                    site.Unregister(job);
                }
                catch (Exception ex)
                {
                    Paths.Log("Could not unregister job " + job.JobId + ": " + ex.Message);
                }
                throw;
            }
        }

        string DownloadPlace(GameInfo game, CancellationToken cancel)
        {
            string path = Path.Combine(Paths.Places, game.Id + ".rbxl");
            bool upToDate = File.Exists(path) && game.Md5.Length > 0 && string.Equals(Md5Of(path), game.Md5, StringComparison.OrdinalIgnoreCase);
            if (upToDate)
            {
                return path;
            }

            Report("Downloading " + game.Name + "...", 0);
            string temp = path + ".part";
            site.Download("asset/?id=" + game.Id, temp, (received, total) =>
            {
                Report("Downloading " + game.Name + "...", total > 0 ? (int)Math.Min(100, received * 100 / total) : -1);
            }, cancel);
            if (game.Md5.Length > 0 && !string.Equals(Md5Of(temp), game.Md5, StringComparison.OrdinalIgnoreCase))
            {
                File.Delete(temp);
                throw new LauncherException("The downloaded place does not match the website (MD5). Try again.");
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Move(temp, path);
            return path;
        }

        static string Md5Of(string path)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(path))
            {
                return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
            }
        }

        Dictionary<string, string> Values(string scriptPath, string placePath, int port)
        {
            return new Dictionary<string, string>
            {
                { "script", scriptPath },
                { "scriptasset", "rbxasset://scripts/" + Path.GetFileName(scriptPath) },
                { "place", placePath ?? "" },
                { "port", port.ToString() },
                { "baseurl", request.BaseUrl.ToString() }
            };
        }

        /// <summary>Saves the website's script in the client's content\scripts folder.</summary>
        static string WriteScript(string exe, string kind, string script)
        {
            ClientManifest.CleanScripts(exe);
            string folder = ClientManifest.ScriptFolder(exe);
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, "robloxserver_" + kind + "_" + Guid.NewGuid().ToString("N").Substring(0, 8) + ".lua");
            File.WriteAllText(path, script, new UTF8Encoding(false));
            return path;
        }

        static Process Start(string exe, string arguments)
        {
            Paths.Log("Starting " + exe + " " + arguments);
            var info = new ProcessStartInfo(exe, arguments)
            {
                WorkingDirectory = Path.GetDirectoryName(exe),
                UseShellExecute = false
            };
            Process process = Process.Start(info);
            if (process == null)
            {
                throw new LauncherException("ROBLOX could not be started.");
            }
            return process;
        }
    }
}
