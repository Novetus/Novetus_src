using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace RobloxPlayerLauncher
{
    /// <summary>
    /// Installs the client a game needs (2007-2013, Novetus naming: year + E/M/L) from the website (/install/version.ashx and
    /// /install/download.ashx), like the 2013 bootstrapper did with setup.roblox.com: download the
    /// package once per version, check its SHA-256, unzip it into its own Versions folder.
    /// </summary>
    public static class ClientInstaller
    {
        public static readonly string[] SupportedClients =
        {
            "2007E", "2007M", "2007L", "2008E", "2008M", "2008L", "2009E", "2009M", "2009L",
            "2010E", "2010M", "2010L", "2011E", "2011M", "2011L", "2012E", "2012M", "2012L",
            "2013E", "2013M", "2013L"
        };

        const string MarkerFile = ".robloxserver-package";

        public static string CleanClientName(string client)
        {
            string match = SupportedClients.FirstOrDefault(c => string.Equals(c, (client ?? "").Trim(), StringComparison.OrdinalIgnoreCase));
            if (match == null)
            {
                throw new LauncherException("This game uses the client '" + client + "'. This launcher plays the 2007-2013 clients ("
                    + string.Join(", ", SupportedClients) + ").");
            }
            return match;
        }

        /// <summary>One folder per website, so two sites with different 2012M packages never mix.</summary>
        static string SiteFolder(Uri baseUrl)
        {
            string key = Settings.SiteKey(baseUrl) + baseUrl.AbsolutePath;
            using (var sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(key.ToLowerInvariant()));
                string host = new string(baseUrl.Host.Where(char.IsLetterOrDigit).Take(24).ToArray());
                return Path.Combine(Paths.Versions, host + "-" + BitConverter.ToString(hash, 0, 4).Replace("-", "").ToLowerInvariant());
            }
        }

        public static string Sha256Of(string path)
        {
            using (var sha = SHA256.Create())
            using (var stream = File.OpenRead(path))
            {
                return BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>Returns the folder of the installed client, downloading it first when needed.</summary>
        public static string Ensure(SiteClient site, string client, Action<string, int> report, CancellationToken cancel)
        {
            client = CleanClientName(client);
            report("Checking for updates...", -1);

            PackageInfo package = site.GetPackage(client);
            if (!package.Available)
            {
                throw new LauncherException(package.Message ?? ("The " + client + " client is not available on this website."));
            }
            if (string.IsNullOrEmpty(package.Sha256) || string.IsNullOrEmpty(package.Version) || package.Version.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                throw new LauncherException("The website sent an invalid version for " + client + ".");
            }

            string siteFolder = SiteFolder(site.BaseUrl);
            string folder = Path.Combine(siteFolder, client + "-" + package.Version);
            string marker = Path.Combine(folder, MarkerFile);
            if (File.Exists(marker) && string.Equals(File.ReadAllText(marker).Trim(), package.Sha256, StringComparison.OrdinalIgnoreCase))
            {
                return folder;
            }

            string zip = Path.Combine(Paths.Downloads, client + "-" + package.Version + ".zip");
            string temp = folder + ".tmp";
            try
            {
                report("Downloading ROBLOX " + client + "...", 0);
                site.Download("Install/Download.ashx?client=" + client, zip, (received, total) =>
                {
                    long size = total > 0 ? total : package.Size;
                    report("Downloading ROBLOX " + client + "... " + (received / 1048576) + " MB", size > 0 ? (int)Math.Min(100, received * 100 / size) : -1);
                }, cancel);

                report("Verifying ROBLOX " + client + "...", -1);
                if (!string.Equals(Sha256Of(zip), package.Sha256, StringComparison.OrdinalIgnoreCase))
                {
                    throw new LauncherException("The downloaded " + client + " client is damaged (SHA-256 mismatch). Try again.");
                }

                report("Installing ROBLOX " + client + "...", -1);
                if (Directory.Exists(temp))
                {
                    Directory.Delete(temp, true);
                }
                Extract(zip, temp, cancel);
                File.WriteAllText(Path.Combine(temp, MarkerFile), package.Sha256);
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                }
                Directory.Move(temp, folder);
            }
            finally
            {
                try
                {
                    File.Delete(zip);
                    if (Directory.Exists(temp))
                    {
                        Directory.Delete(temp, true);
                    }
                }
                catch (Exception ex)
                {
                    Paths.Log("Could not clean up the " + client + " download: " + ex.Message);
                }
            }

            RemoveOldVersions(siteFolder, client, folder);
            return folder;
        }

        /// <summary>ZipFile.ExtractToDirectory, but refusing entries that would land outside the folder.</summary>
        static void Extract(string zip, string folder, CancellationToken cancel)
        {
            Directory.CreateDirectory(folder);
            using (ZipArchive archive = ZipFile.OpenRead(zip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    cancel.ThrowIfCancellationRequested();
                    string path = Path.GetFullPath(Path.Combine(folder, entry.FullName.Replace('/', Path.DirectorySeparatorChar)));
                    if (!Paths.IsInside(path, folder))
                    {
                        throw new LauncherException("The client package contains an unsafe path: " + entry.FullName);
                    }
                    if (entry.FullName.EndsWith("/") || entry.FullName.EndsWith("\\"))
                    {
                        Directory.CreateDirectory(path);
                        continue;
                    }
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    entry.ExtractToFile(path, true);
                }
            }
        }

        static void RemoveOldVersions(string siteFolder, string client, string keep)
        {
            foreach (string old in Directory.GetDirectories(siteFolder, client + "-*"))
            {
                if (Paths.SamePath(old, keep))
                {
                    continue;
                }
                try
                {
                    Directory.Delete(old, true);
                }
                catch (Exception ex)
                {
                    // Still running (a game server on the old version); removed next time.
                    Paths.Log("Could not remove " + old + ": " + ex.Message);
                }
            }
        }
    }
}
