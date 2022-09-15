/*
 * TODO:
 * 
 * change control names for all forms
 * Make launcher form line count smaller
 * organize launcher codebase
 * replace == and != with .equals
 */

#region Usings
using System.Collections.Generic;
using System.Windows.Forms;
#endregion

#region Script Type
public enum ScriptType
{
    Client = 0,
    Server = 1,
    Solo = 2,
    Studio = 3,
    EasterEgg = 4,
    None = 5
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
        InEasterEggGame = 5,
        LoadingURI = 6
    }

    public static DiscordRPC.EventHandlers handlers;
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
    public static string IP = "localhost";
    public static string ExternalIP = SecurityFuncs.GetExternalIPAddress();
    public static int DefaultRobloxPort = 53640;
    public static int JoinPort = DefaultRobloxPort;
    public static ScriptType GameOpened = ScriptType.None;
    public static string PlayerTripcode = "";
    #endregion

    #region NovetusCMD
    //only for novetuscmd. only here because of launchrbxclient >:(
    public static int ProcessID = 0;
    public static bool RequestToOutputInfo = false;
    #endregion

#if LAUNCHER
    #region Novetus Launcher
    public static NovetusLauncher.NovetusConsole consoleForm = null;
    #endregion
#endif

    #region Customization
    public static string Loadout = "";
    public static string soloLoadout = "";
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
    public static DiscordRPC.RichPresence presence;
    public static string appid = "505955125727330324";
    public static string imagekey_large = "novetus_large";
    public static string image_ingame = "ingame_small";
    public static string image_inlauncher = "inlauncher_small";
    public static string image_instudio = "instudio_small";
    public static string image_incustomization = "incustomization_small";
    #endregion

    #region Other
    public static bool ExtendedVersionNumber = false;
    public static bool LocalPlayMode = false;
    public static bool AdminMode = false;
    public static bool ColorsLoaded = false;
    public static int ValidatedExtraFiles = 0;
    public static bool NoFileList = false;
    public static string ServerID = "N/A";
    public static string PingURL = "";
    public static string Important = "";
    public static string Important2 = "";
    public static string NextCommand = "";
    public static bool AppClosed = false;
    public static bool isConsoleOnly = false;
    #endregion
}
#endregion
