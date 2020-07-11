/*
 * TODO:
 * 
 * change control names for all forms
 * add regions to rest of forms. Launcher forms are left!
 * Finish Rise of the Killbots update
 */

#region Global Variables
public static class GlobalVars
{
    #region Launcher State for Discord
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
    #endregion

    #region Class definitions
    public static FileFormat.ProgramInfo ProgramInformation = new FileFormat.ProgramInfo();
    public static FileFormat.Config UserConfiguration = new FileFormat.Config();
    public static FileFormat.ClientInfo SelectedClientInfo = new FileFormat.ClientInfo();
    public static FileFormat.CustomizationConfig UserCustomization = new FileFormat.CustomizationConfig();
    public static SimpleHTTPServer WebServer = null;
    #endregion

    #region Joining
    public static string IP = "localhost";
    public static int DefaultRobloxPort = 53640;
    public static string SharedArgs = "";
    #endregion

    #region NovetusCMD
    //only for novetuscmd. only here because of launchrbxclient >:(
    public static int ProcessID = 0;
    public static bool RequestToOutputInfo = false;
    public static string ServerInfoFileName = "serverinfo.txt";
    #endregion

    #region Customization
    public static string Loadout = "";
    public static string soloLoadout = "";
    #endregion

    #region Booleans
    public static bool IsWebServerOn = false;
    public static bool IsSnapshot = false;
    public static bool LocalPlayMode = false;
    public static bool AdminMode = false;
    #endregion

    #region Web Server Vars
    public static int WebServerPort = 40735;
    public static string LocalWebServerURI = "http://localhost:" + (WebServerPort).ToString();
    public static string WebServerURI = "http://" + IP + ":" + (WebServerPort).ToString();
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
}
#endregion
