#region Usings
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using System.Net;
#endregion

namespace Novetus.Core
{
    #region Novetus Functions
    public class NovetusFuncs
    {
        public static int GenerateRandomNumber()
        {
            CryptoRandom random = new CryptoRandom();
            int randomID = 0;
            int randIDmode = random.Next(0, 8);
            int idlimit = 0;

            switch (randIDmode)
            {
                case 0:
                    idlimit = 9;
                    break;
                case 1:
                    idlimit = 99;
                    break;
                case 2:
                    idlimit = 999;
                    break;
                case 3:
                    idlimit = 9999;
                    break;
                case 4:
                    idlimit = 99999;
                    break;
                case 5:
                    idlimit = 999999;
                    break;
                case 6:
                    idlimit = 9999999;
                    break;
                case 7:
                    idlimit = 99999999;
                    break;
                case 8:
                default:
                    break;
            }

            if (idlimit > 0)
            {
                randomID = random.Next(0, idlimit);
            }
            else
            {
                randomID = random.Next();
            }

            //2147483647 is max id.
            return randomID;
        }

        public static int GeneratePlayerID()
        {
            return GenerateRandomNumber();
        }

        public static void PingMasterServer(bool online, string reason)
        {
            if (GlobalVars.GameOpened == ScriptType.Server || GlobalVars.GameOpened == ScriptType.SoloServer)
                return;

            if (string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.ReadSetting("ServerBrowserServerAddress")))
                return;

            if (string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.ReadSetting("ServerBrowserServerName")))
            {
                Util.ConsolePrint("Your server doesn't have a name. Please specify one for it to show on the master server list after server restart.", 2);
                return;
            }

            string AlternateServerIP = GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP");

            if (online)
            {
                GlobalVars.ServerID = RandomString(30) + GenerateRandomNumber();
                GlobalVars.PingURL = "http://" + GlobalVars.UserConfiguration.ReadSetting("ServerBrowserServerAddress") +
                "/list.php?name=" + GlobalVars.UserConfiguration.ReadSetting("ServerBrowserServerName") +
                "&ip=" + (!string.IsNullOrWhiteSpace(AlternateServerIP) ? AlternateServerIP : GlobalVars.ExternalIP) +
                "&port=" + GlobalVars.UserConfiguration.ReadSettingInt("RobloxPort") +
                "&client=" + GlobalVars.UserConfiguration.ReadSetting("SelectedClient") +
                "&version=" + GlobalVars.ProgramInformation.Version +
                "&id=" + GlobalVars.ServerID;
            }
            else
            {
                GlobalVars.PingURL = "http://" + GlobalVars.UserConfiguration.ReadSetting("ServerBrowserServerAddress") +
                "/delist.php?id=" + GlobalVars.ServerID;
                GlobalVars.ServerID = "N/A";
            }

            Util.ConsolePrint("Pinging master server. " + reason, 4);
            Task.Factory.StartNew(() => {
                string response = Util.HttpGet(GlobalVars.PingURL);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    Util.ConsolePrint(response, response.Contains("ERROR:") ? 2 : 4);

                    if (response.Contains("ERROR:"))
                    {
                        GlobalVars.ServerID = "N/A";
                    }
                }

                if (!GlobalVars.ServerID.Equals("N/A"))
                {
                    Util.ConsolePrint("Master server ping successful. Your server's ID is " + GlobalVars.ServerID, 4);
                }

                GlobalVars.PingURL = "";
            });
        }

        public static string[] LoadServerInformation()
        {
            string AlternateServerIP = GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP");
            int RobloxPort = GlobalVars.UserConfiguration.ReadSettingInt("RobloxPort");
            string SelectedClient = GlobalVars.UserConfiguration.ReadSetting("SelectedClient");

            string[] lines1 = {
                        SecurityFuncs.Encode(!string.IsNullOrWhiteSpace(AlternateServerIP) ? AlternateServerIP : GlobalVars.ExternalIP),
                        SecurityFuncs.Encode(RobloxPort.ToString()),
                        SecurityFuncs.Encode(SelectedClient)
                    };
            string URI = "novetus://" + SecurityFuncs.Encode(string.Join("|", lines1), true);
            string[] lines2 = {
                        SecurityFuncs.Encode("localhost"),
                        SecurityFuncs.Encode(RobloxPort.ToString()),
                        SecurityFuncs.Encode(SelectedClient)
                    };
            string URI2 = "novetus://" + SecurityFuncs.Encode(string.Join("|", lines2), true);
            GameServer server = new GameServer((!string.IsNullOrWhiteSpace(AlternateServerIP) ? AlternateServerIP : GlobalVars.ExternalIP), RobloxPort);
            string[] text = {
                       "Server IP Address: " + server.ToString(),
                       "Client: " + SelectedClient,
                       "Map: " + GlobalVars.UserConfiguration.ReadSetting("Map"),
                       "Players: " + GlobalVars.UserConfiguration.ReadSettingInt("PlayerLimit"),
                       "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                       "Online URI Link:",
                       URI,
                       "Local URI Link:",
                       URI2
                       };

            return text;
        }

#if LAUNCHER || URI
        public static void LaunchCharacterCustomization(bool skipopencheck = false)
        {
            if (!skipopencheck)
            {
                //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "CharacterCustomizationExtended" ||
                        frm.Name == "CharacterCustomizationCompact")
                    {
                        frm.Close();
                        break;
                    }
                }
            }

            switch ((Settings.Style)GlobalVars.UserConfiguration.ReadSettingInt("LauncherStyle"))
            {
                case Settings.Style.Extended:
                    CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
                    ccustom.Show();
                    break;
                case Settings.Style.Compact:
                    CharacterCustomizationCompact ccustom2 = new CharacterCustomizationCompact();
                    ccustom2.Show();
                    break;
                case Settings.Style.Stylish:
                default:
                    CharacterCustomizationExtended ccustom3 = new CharacterCustomizationExtended();
                    ccustom3.Show();
                    break;
            }
        }
#endif

        public static string FixURLString(string str, string str2)
        {
            string fixedStr = str.ToLower().Replace("?version=1&amp;id=", "?id=")
                        .Replace("?version=1&id=", "?id=")
                        .Replace("&amp;", "&")
                        .Replace("amp;", "&");

            string baseurl = fixedStr.Before("/asset/?id=");

            if (baseurl == "")
            {
                baseurl = fixedStr.Before("/asset?id=");
                if (baseurl == "")
                {
                    baseurl = fixedStr.Before("/item.aspx?id=");
                }
            }

            string fixedUrl = fixedStr.Replace(baseurl + "/asset/?id=", str2)
                        .Replace(baseurl + "/asset?id=", str2)
                        .Replace(baseurl + "/item.aspx?id=", str2);

            //...because scripts mess it up.

            string id = fixedUrl.After("id=");
            if (id.Contains("&version="))
            {
                string ver = id.After("&version=");
                id = id.Replace("&version=" + ver, "");
            }

            string fixedID = Regex.Replace(id, "[^0-9]", "");

            //really fucking hacky.
            string finalUrl = fixedUrl.Before("id=") + "id=" + fixedID;

            return finalUrl;
        }

        public static string RandomString(int length = 30, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz")
        {
            CryptoRandom random = new CryptoRandom();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetExternalIPAddress()
        {
            string ipAddress;

            try
            {
                ipAddress = new WebClient().DownloadString("https://ipv4.icanhazip.com/").TrimEnd();
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                ipAddress = "localhost";
            }

            return ipAddress;
        }
    }
    #endregion
}
