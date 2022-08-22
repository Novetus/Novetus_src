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
            GlobalFuncs.ConsolePrint("Novetus CMD Command Line Arguments", 3, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("General", 3, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("-help | Displays the help.", 4, true, true);
            GlobalFuncs.ConsolePrint("-no3d | Launches server in NoGraphics mode", 4, true, true);
            GlobalFuncs.ConsolePrint("-outputinfo | Outputs all information about the running server to a text file.", 4, true, true);
            GlobalFuncs.ConsolePrint("-debug | Disables launching of the server for debugging purposes.", 4, true, true);
            GlobalFuncs.ConsolePrint("-debug | Disables launching of the server for debugging purposes.", 4, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Custom server options", 3, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("-upnp | Turns on UPnP.", 4, true, true);
            GlobalFuncs.ConsolePrint("-map <map filename> | Sets the map.", 4, true, true);
            GlobalFuncs.ConsolePrint("-client <client name> | Sets the client.", 4, true, true);
            GlobalFuncs.ConsolePrint("-port <port number> | Sets the server port.", 4, true, true);
            GlobalFuncs.ConsolePrint("-maxplayers <number of players> | Sets the number of players.", 4, true, true);
            GlobalFuncs.ConsolePrint("-notifications <true/false> | Toggles server join/leave notifications.", 4, true, true);
            GlobalFuncs.ConsolePrint("-serverbrowsername <server name> | Changes the name the server uses upon connection to the master server.", 4, true, true);
            GlobalFuncs.ConsolePrint("-serverbrowseraddress <master server address> | Changes the master server address.", 4, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("How to launch:", 3, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Create a shortcut to NovetusCMD in the bin folder of Novetus' Directory or", 4, true, true);
            GlobalFuncs.ConsolePrint("create a batch file that launches NovetusCMD.", 4, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Shortcuts", 3, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Right-click your shortcut and then go to Properties -> Shortcut.", 4, true, true);
            GlobalFuncs.ConsolePrint("Go to 'Target' and then click the end of where it says 'NovetusCMD.exe'", 4, true, true);
            GlobalFuncs.ConsolePrint("Press space and then type in whatever arguments you please.", 4, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Batch", 3, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Click the end of where it says 'NovetusCMD.exe'", 4, true, true);
            GlobalFuncs.ConsolePrint("Press space and then type in whatever arguments you please.", 4, true, true);
            GlobalFuncs.ConsolePrint("---------", 1, true, true);
            GlobalFuncs.ConsolePrint("Press any key to close...", 2, true, true);
        }
    }
    #endregion
}
