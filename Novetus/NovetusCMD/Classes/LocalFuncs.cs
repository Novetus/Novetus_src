#region Usings
using System;
using System.IO;
#endregion

namespace NovetusCMD
{
    #region LocalFuncs
    public class LocalFuncs
    {
        public static async void CreateTXT()
        {
            if (LocalVars.RequestToOutputInfo)
            {
                string IP = await SecurityFuncs.GetExternalIPAddressAsync();
                string[] lines1 = {
                        SecurityFuncs.Base64Encode(IP),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
                string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1, true));
                string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
                string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2, true));

                string text = GlobalFuncs.MultiLine(
                       "Process ID: " + (LocalVars.ProcessID == 0 ? "N/A" : LocalVars.ProcessID.ToString()),
                       "Don't copy the Process ID when sharing the server.",
                       "--------------------",
                       "Server Info:",
                       "Client: " + GlobalVars.UserConfiguration.SelectedClient,
                       "IP: " + IP,
                       "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                       "Map: " + GlobalVars.UserConfiguration.Map,
                       "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                       "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                       "Online URI Link:",
                       URI,
                       "Local URI Link:",
                       URI2,
                       GlobalVars.IsWebServerOn ? "Web Server URL:" : "",
                       GlobalVars.IsWebServerOn ? "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString() : "",
                       GlobalVars.IsWebServerOn ? "Local Web Server URL:" : "",
                       GlobalVars.IsWebServerOn ? GlobalVars.LocalWebServerURI : ""
                   );

                File.WriteAllText(GlobalPaths.BasePath + "\\" + LocalVars.ServerInfoFileName, GlobalFuncs.RemoveEmptyLines(text));
                ConsolePrint("Server Information sent to file " + GlobalPaths.BasePath + "\\" + LocalVars.ServerInfoFileName, 4);
            }
        }

        public static void ConsolePrint(string text, int type)
        {
            ConsoleText("[" + DateTime.Now.ToShortTimeString() + "] - ", ConsoleColor.White);

            switch (type)
            {
                case 2:
                    ConsoleText(text, ConsoleColor.Red);
                    break;
                case 3:
                    ConsoleText(text, ConsoleColor.Green);
                    break;
                case 4:
                    ConsoleText(text, ConsoleColor.Cyan);
                    break;
                case 5:
                    ConsoleText(text, ConsoleColor.Yellow);
                    break;
                case 1:
                default:
                    ConsoleText(text, ConsoleColor.White);
                    break;
            }

            ConsoleText(Environment.NewLine, ConsoleColor.White);
        }

        public static void ConsoleText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
    #endregion
}
