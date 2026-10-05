using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RobloxPlayerLauncher
{
    /// <summary>
    /// %LocalAppData%\RobloxServer\Settings.ini (key=value lines):
    ///   TrustedSite=http://robloxserver.lan/   one line per website the user allowed
    ///   HostPort=53640                         UDP port used when hosting a game server
    ///   HostAddress=                           address players connect to (empty: the website decides)
    ///   HostLanAddress=                        this PC's address on the local network (empty: detected)
    /// </summary>
    public class Settings
    {
        public const int DefaultHostPort = 53640;

        public List<string> TrustedSites { get; private set; }
        public int HostPort { get; set; }
        public string HostAddress { get; set; }
        public string HostLanAddress { get; set; }

        Settings()
        {
            TrustedSites = new List<string>();
            HostPort = DefaultHostPort;
            HostAddress = "";
            HostLanAddress = "";
        }

        public static Settings Load()
        {
            var settings = new Settings();
            if (!File.Exists(Paths.SettingsFile))
            {
                return settings;
            }

            foreach (string raw in File.ReadAllLines(Paths.SettingsFile))
            {
                string line = raw.Trim();
                int equals = line.IndexOf('=');
                if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#") || equals <= 0)
                {
                    continue;
                }

                string key = line.Substring(0, equals).Trim();
                string value = line.Substring(equals + 1).Trim();
                int number;
                switch (key.ToLowerInvariant())
                {
                    case "trustedsite":
                        if (value.Length > 0 && !settings.TrustedSites.Contains(value, StringComparer.OrdinalIgnoreCase))
                        {
                            settings.TrustedSites.Add(value);
                        }
                        break;
                    case "hostport":
                        if (int.TryParse(value, out number) && number > 0 && number <= 65535)
                        {
                            settings.HostPort = number;
                        }
                        break;
                    case "hostaddress":
                        settings.HostAddress = value;
                        break;
                    case "hostlanaddress":
                        settings.HostLanAddress = value;
                        break;
                }
            }
            return settings;
        }

        public void Save()
        {
            var text = new StringBuilder();
            text.AppendLine("; RobloxPlayerLauncher settings");
            text.AppendLine("HostPort=" + HostPort);
            text.AppendLine("HostAddress=" + (HostAddress ?? ""));
            text.AppendLine("HostLanAddress=" + (HostLanAddress ?? ""));
            foreach (string site in TrustedSites)
            {
                text.AppendLine("TrustedSite=" + site);
            }
            Directory.CreateDirectory(Paths.Root);
            File.WriteAllText(Paths.SettingsFile, text.ToString());
        }

        /// <summary>scheme://host:port/ of a website, the unit the user trusts.</summary>
        public static string SiteKey(Uri url)
        {
            return url.Scheme.ToLowerInvariant() + "://" + url.Authority.ToLowerInvariant() + "/";
        }

        public bool IsTrusted(Uri url)
        {
            string key = SiteKey(url);
            return TrustedSites.Any(s => string.Equals(s, key, StringComparison.OrdinalIgnoreCase));
        }

        public void Trust(Uri url)
        {
            if (!IsTrusted(url))
            {
                TrustedSites.Add(SiteKey(url));
            }
        }
    }
}
