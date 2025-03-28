/*
 * TODO:
 * 
 * change control names for all forms
 * Make launcher form line count smaller
 * organize launcher codebase
 * replace == and != with .equals
 */

#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
#endregion

namespace Novetus.Core
{
    #region Script Type
    public enum ScriptType
    {
        Client = 0,
        Server = 1,
        Solo = 2,
        Studio = 3,
        SoloServer = 4,
        OutfitView = 5,
        None = 6
    }
    #endregion

    #region Game Server Definition
    public class GameServer
    {
        public GameServer(string ip, int port)
        {
            ServerIP = ip;
            ServerPort = port;
        }

        public override string ToString()
        {
            return ServerIP + ":" + ServerPort.ToString();
        }

        public void SetValues(string input)
        {
            try
            {
                string[] vals = input.Split(':');
                string ip = vals[0];
                int port = ConvertSafe.ToInt32Safe(vals[1]);

                ServerIP = ip;
                ServerPort = port;
            }
            catch (Exception)
            {
                ServerIP = input;
                ServerPort = GlobalVars.DefaultRobloxPort;
            }
        }

        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
    }
    #endregion

    #region Global Variables
    public static class GlobalVars
    {
        #region Discord
        public enum LauncherState
        {
            InLauncher = 0,
            InMPGame = 1,
            InSoloGame = 2,
            InStudio = 3,
            InCustomization = 4,
            LoadingURI = 5
        }

        public static IDiscordRPC.EventHandlers handlers;
        #endregion

        #region Class definitions
        public static FileFormat.ProgramInfo ProgramInformation = new FileFormat.ProgramInfo();
        public static FileFormat.Config UserConfiguration = new FileFormat.Config();
        public static FileFormat.ClientInfo SelectedClientInfo = new FileFormat.ClientInfo();
        public static FileFormat.CustomizationConfig UserCustomization = new FileFormat.CustomizationConfig();
        public static PartColor[] PartColorList;
        public static List<PartColor> PartColorListConv;
        #endregion

        #region Joining/Hosting
        public static string DefaultIP = "localhost";
        public static int DefaultRobloxPort = 53640;
        public static GameServer CurrentServer = new GameServer(DefaultIP, DefaultRobloxPort);
        public static string ExternalIP = NovetusFuncs.GetExternalIPAddress();
        public static ScriptType GameOpened = ScriptType.None;
        public static string PlayerTripcode = NovetusFuncs.GenerateTripcode();
#if LAUNCHER || URI
        public static WebProxy Proxy = new WebProxy();
#endif
        #endregion

#if LAUNCHER
        #region Novetus Launcher
        public static NovetusLauncher.NovetusConsole consoleForm = null;
        #endregion
#endif

        #region Customization
        public static string Loadout = "";
        public static string TShirtTextureID = "";
        public static string ShirtTextureID = "";
        public static string PantsTextureID = "";
        public static string FaceTextureID = "";
        public static string TShirtTextureLocal = "";
        public static string ShirtTextureLocal = "";
        public static string PantsTextureLocal = "";
        public static string FaceTextureLocal = "";
        #endregion

        #region Discord Variables
        //discord
        public static IDiscordRPC.RichPresence presence;
        public static string appid = "505955125727330324";
        public static string imagekey_large = "novetus_large";
        public static string image_ingame = "ingame_small";
        public static string image_inlauncher = "inlauncher_small";
        public static string image_instudio = "instudio_small";
        public static string image_incustomization = "incustomization_small";
        #endregion

        #region Other
        public static DateTime ClientLoadDelay = DateTime.Now;
        public static bool ExtendedVersionNumber = false;
        public static bool LocalPlayMode = false;
#if DEBUG
        public static bool AdminMode = true;
#else
        public static bool AdminMode = false;
#endif
        public static bool ColorsLoaded = false;
        public static int ValidatedExtraFiles = 0;
        public static bool NoFileList = false;
        public static string ServerID = "N/A";
        public static string PingURL = "";
        public static string NextCommand = "";
        public static bool AppClosed = false;
        public static bool isConsoleOnly = false;
        public static bool isMapCompressed = false;
        public static int Clicks = 0;
        public static bool EasterEggMode = false;
        public static int PlaySoloPort = 1027;
#endregion
    }
#endregion
}
