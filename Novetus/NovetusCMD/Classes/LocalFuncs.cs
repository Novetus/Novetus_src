#region Usings
using System;
using System.Diagnostics;
using System.Linq;
#endregion

namespace NovetusCMD
{
    #region LocalFuncs
    public class LocalFuncs
    {
        public static bool ProcessExists(int id)
        {
            return Process.GetProcesses().Any(x => x.Id == id);
        }

        public static void CommandInfo()
        {
            Util.ConsolePrint("Novetus CMD Command Line Arguments", 3, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("General", 3, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("-help | Displays the help.", 4, true, true);
            Util.ConsolePrint("-no3d | Launches server in NoGraphics mode", 4, true, true);
            Util.ConsolePrint("-outputinfo | Outputs all information about the running server to a text file.", 4, true, true);
            Util.ConsolePrint("-debug | Disables launching of the server for debugging purposes.", 4, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Custom server options", 3, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("-upnp | Turns on UPnP.", 4, true, true);
            Util.ConsolePrint("-map <map filename> | Sets the map.", 4, true, true);
            Util.ConsolePrint("-client <client name> | Sets the client.", 4, true, true);
            Util.ConsolePrint("-port <port number> | Sets the server port.", 4, true, true);
            Util.ConsolePrint("-maxplayers <number of players> | Sets the number of players.", 4, true, true);
            Util.ConsolePrint("-notifications <true/false> | Toggles server join/leave notifications.", 4, true, true);
            Util.ConsolePrint("-serverbrowsername <server name> | Changes the name the server uses upon connection to the master server.", 4, true, true);
            Util.ConsolePrint("-serverbrowseraddress <master server address> | Changes the master server address.", 4, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("How to launch:", 3, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Create a shortcut to NovetusCMD in the bin folder of Novetus' Directory or", 4, true, true);
            Util.ConsolePrint("create a batch file that launches NovetusCMD.", 4, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Shortcuts", 3, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Right-click your shortcut and then go to Properties -> Shortcut.", 4, true, true);
            Util.ConsolePrint("Go to 'Target' and then click the end of where it says 'NovetusCMD.exe'", 4, true, true);
            Util.ConsolePrint("Press space and then type in whatever arguments you please.", 4, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Batch", 3, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Click the end of where it says 'NovetusCMD.exe'", 4, true, true);
            Util.ConsolePrint("Press space and then type in whatever arguments you please.", 4, true, true);
            Util.ConsolePrint("---------", 1, true, true);
            Util.ConsolePrint("Press any key to close...", 2, true, true);
        }
    }
    #endregion
}
