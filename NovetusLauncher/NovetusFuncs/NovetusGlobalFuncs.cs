#region Usings
using Microsoft.Win32;
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

/*
 * change field names for all forms
 * Rewrite client launching into one function.
 * add regions to ALL classes.
 */

#region Enums

#region Quality Level
public enum QualityLevel
{
    VeryLow = 1,
    Low = 2,
    Medium = 3,
    High = 4,
    Ultra = 5
}
#endregion

#region Graphics Mode
public enum GraphicsMode
{
    None = 0,
    OpenGL = 1,
    DirectX = 2
}
#endregion

#region Roblox File Types
public enum RobloxFileType
{
    //RBXL and RBXM
    RBXL,
    RBXM,
    //Items
    Hat,
    Head,
    Face,
    TShirt,
    Shirt,
    Pants
}
#endregion

#endregion

#region Classes

#region Definition Classes

#region GlobalVars
public static class GlobalVars
{
    public static ProgramInfo ProgramInformation = new ProgramInfo();
    public static Config UserConfiguration = new Config();
    public static string IP = "localhost";
    public static string SharedArgs = "";
    public static readonly string ScriptName = "CSMPFunctions";
    public static readonly string ScriptGenName = "CSMPBoot";
    public static SimpleHTTPServer WebServer = null;
    public static bool IsWebServerOn = false;
    public static bool IsSnapshot = false;
    //misc vars
    public static string FullMapPath = "";
    //weebserver
    public static int WebServerPort = 40735;
    public static string LocalWebServerURI = "http://localhost:" + (WebServerPort).ToString();
    public static string WebServerURI = "http://" + IP + ":" + (WebServerPort).ToString();
    //config name
    public static readonly string ConfigName = "config.ini";
    public static string ConfigNameCustomization = "config_customization.ini";
    public static readonly string InfoName = "info.ini";
    //client shit
    public static ClientInfo SelectedClientInfo = new ClientInfo();
    public static string AddonScriptPath = "";
    //charcustom
    public static CustomizationConfig UserCustomization = new CustomizationConfig();
    public static string loadtext = "";
    public static string sololoadtext = "";
    //color menu.
    public static bool AdminMode = false;
    public static string important = "";
    //discord
    public static DiscordRpc.RichPresence presence;
    public static string appid = "505955125727330324";
    public static string imagekey_large = "novetus_large";
    public static string image_ingame = "ingame_small";
    public static string image_inlauncher = "inlauncher_small";
    public static string image_instudio = "instudio_small";
    public static string image_incustomization = "incustomization_small";

    public static string MultiLine(params string[] args)
    {
        return string.Join(Environment.NewLine, args);
    }

    public static string RemoveEmptyLines(string lines)
    {
        return Regex.Replace(lines, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
    }

    public static bool ProcessExists(int id)
    {
        return Process.GetProcesses().Any(x => x.Id == id);
    }

    //task.delay is only available on net 4.5.......
    public static async void Delay(int miliseconds)
    {
        await TaskEx.Delay(miliseconds);
    }
}
#endregion

#region Directories
public class Directories
{
    public static readonly string RootPathLauncher = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static readonly string BasePathLauncher = RootPathLauncher.Replace(@"\", @"\\");
    public static readonly string RootPath = Directory.GetParent(RootPathLauncher).ToString();
    public static readonly string BasePath = RootPath.Replace(@"\", @"\\");
    public static readonly string DataPath = BasePath + @"\\shareddata";
    public static readonly string ServerDir = DataPath + "\\server";
    public static readonly string ConfigDir = BasePath + @"\\config";
    public static readonly string ConfigDirData = BasePathLauncher + @"\\data";
    public static readonly string ClientDir = BasePath + @"\\clients";
    public static readonly string MapsDir = BasePath + @"\\maps";
    public static readonly string MapsDirBase = "maps";
    public static readonly string BaseGameDir = "rbxasset://../../../";
    public static readonly string SharedDataGameDir = BaseGameDir + "shareddata/";
    public static readonly string DirFonts = "\\fonts";
    public static readonly string DirSounds = "\\sounds";
    public static readonly string DirTextures = "\\textures";
    public static readonly string DirScripts = "\\scripts";

    public static readonly string FontsGameDir = "fonts/";
    public static readonly string SoundsGameDir = "sounds/";
    public static readonly string TexturesGameDir = "textures/";
    public static readonly string ScriptsGameDir = "scripts/";
    //customization
    public static readonly string CustomPlayerDir = DataPath + "\\charcustom";
    public static readonly string hatdir = CustomPlayerDir + "\\hats";
    public static readonly string facedir = CustomPlayerDir + "\\faces";
    public static readonly string headdir = CustomPlayerDir + "\\heads";
    public static readonly string tshirtdir = CustomPlayerDir + "\\tshirts";
    public static readonly string shirtdir = CustomPlayerDir + "\\shirts";
    public static readonly string pantsdir = CustomPlayerDir + "\\pants";
    public static readonly string extradir = CustomPlayerDir + "\\custom";

    public static readonly string CharCustomGameDir = SharedDataGameDir + "charcustom/";
    public static readonly string hatGameDir = CharCustomGameDir + "hats/";
    public static readonly string faceGameDir = CharCustomGameDir + "faces/";
    public static readonly string headGameDir = CharCustomGameDir + "heads/";
    public static readonly string tshirtGameDir = CharCustomGameDir + "tshirts/";
    public static readonly string shirtGameDir = CharCustomGameDir + "shirts/";
    public static readonly string pantsGameDir = CharCustomGameDir + "pants/";
    public static readonly string extraGameDir = CharCustomGameDir + "custom/";
    //item asset dirs
    public static readonly string hatdirFonts = hatdir + DirFonts;
    public static readonly string hatdirTextures = hatdir + DirTextures;
    public static readonly string hatdirSounds = hatdir + DirSounds;
    public static readonly string hatdirScripts = hatdir + DirScripts;
    public static readonly string facedirTextures = facedir + DirTextures;
    public static readonly string headdirFonts = headdir + DirFonts;
    public static readonly string headdirTextures = headdir + DirTextures;
    public static readonly string tshirtdirTextures = tshirtdir + DirTextures;
    public static readonly string shirtdirTextures = shirtdir + DirTextures;
    public static readonly string pantsdirTextures = pantsdir + DirTextures;
    public static readonly string extradirIcons = extradir + "\\icons";

    public static readonly string hatGameDirFonts = hatGameDir + FontsGameDir;
    public static readonly string hatGameDirTextures = hatGameDir + TexturesGameDir;
    public static readonly string hatGameDirSounds = hatGameDir + SoundsGameDir;
    public static readonly string hatGameDirScripts = hatGameDir + ScriptsGameDir;
    public static readonly string faceGameDirTextures = faceGameDir + TexturesGameDir;
    public static readonly string headGameDirFonts = headGameDir + FontsGameDir;
    public static readonly string headGameDirTextures = headGameDir + TexturesGameDir;
    public static readonly string tshirtGameDirTextures = tshirtGameDir + TexturesGameDir;
    public static readonly string shirtGameDirTextures = shirtGameDir + TexturesGameDir;
    public static readonly string pantsGameDirTextures = pantsGameDir + TexturesGameDir;
    //asset cache
    public static readonly string AssetCacheDir = DataPath + "\\assetcache";

    public static readonly string AssetCacheDirSky = AssetCacheDir + "\\sky";
    public static readonly string AssetCacheDirFonts = AssetCacheDir + DirFonts;
    public static readonly string AssetCacheDirSounds = AssetCacheDir + DirSounds;
    public static readonly string AssetCacheDirTextures = AssetCacheDir + DirTextures;
    public static readonly string AssetCacheDirTexturesGUI = AssetCacheDirTextures + "\\gui";
    public static readonly string AssetCacheDirScripts = AssetCacheDir + DirScripts;

    public static readonly string AssetCacheGameDir = SharedDataGameDir + "assetcache/";
    public static readonly string AssetCacheFontsGameDir = AssetCacheGameDir + FontsGameDir;
    public static readonly string AssetCacheSkyGameDir = AssetCacheGameDir + "sky/";
    public static readonly string AssetCacheSoundsGameDir = AssetCacheGameDir + SoundsGameDir;
    public static readonly string AssetCacheTexturesGameDir = AssetCacheGameDir + TexturesGameDir;
    public static readonly string AssetCacheTexturesGUIGameDir = AssetCacheTexturesGameDir + "gui/";
    public static readonly string AssetCacheScriptsGameDir = AssetCacheGameDir + ScriptsGameDir;
    //webserver
    public static string WebServer_CustomPlayerDir = GlobalVars.WebServerURI + "/charcustom/";
    public static string WebServer_HatDir = WebServer_CustomPlayerDir + "hats/";
    public static string WebServer_FaceDir = WebServer_CustomPlayerDir + "faces/";
    public static string WebServer_HeadDir = WebServer_CustomPlayerDir + "heads/";
    public static string WebServer_TShirtDir = WebServer_CustomPlayerDir + "tshirts/";
    public static string WebServer_ShirtDir = WebServer_CustomPlayerDir + "shirts/";
    public static string WebServer_PantsDir = WebServer_CustomPlayerDir + "pants/";
    public static string WebServer_ExtraDir = WebServer_CustomPlayerDir + "custom/";
}
#endregion

#endregion

#region Variable Storage Classes

#region Asset Cache Definition
public class AssetCacheDef
{
    public AssetCacheDef(string clas, string[] id, string[] ext,
        string[] dir, string[] gamedir)
    {
        Class = clas;
        Id = id;
        Ext = ext;
        Dir = dir;
        GameDir = gamedir;
    }

    public string Class { get; set; }
    public string[] Id { get; set; }
    public string[] Ext { get; set; }
    public string[] Dir { get; set; }
    public string[] GameDir { get; set; }
}
#endregion

#region Client Information
public class ClientInfo
{
    public ClientInfo()
    {
        UsesPlayerName = false;
        UsesID = true;
        Description = "";
        Warning = "";
        LegacyMode = false;
        ClientMD5 = "";
        ScriptMD5 = "";
        Fix2007 = false;
        AlreadyHasSecurity = false;
        NoGraphicsOptions = false;
        CommandLineArgs = "";
    }

    public bool UsesPlayerName { get; set; }
    public bool UsesID { get; set; }
    public string Description { get; set; }
    public string Warning { get; set; }
    public bool LegacyMode { get; set; }
    public string ClientMD5 { get; set; }
    public string ScriptMD5 { get; set; }
    public bool Fix2007 { get; set; }
    public bool AlreadyHasSecurity { get; set; }
    public bool NoGraphicsOptions { get; set; }
    public string CommandLineArgs { get; set; }
}
#endregion

#region Configuration
public class Config
{
    public Config()
    {
        SelectedClient = "";
        Map = "";
        CloseOnLaunch = false;
        UserID = 0;
        PlayerName = "Player";
        PlayerTripcode = "";
        RobloxPort = 53640;
        PlayerLimit = 12;
        UPnP = false;
        DisabledItemMakerHelp = false;
        DiscordPresence = true;
        MapPath = "";
        MapPathSnip = "";
        GraphicsMode = GraphicsMode.OpenGL;
        ReShade = false;
        QualityLevel = QualityLevel.Ultra;
        LauncherLayout = LauncherLayout.Extended;
        ReShadeFPSDisplay = false;
        ReShadePerformanceMode = false;
    }

    public string SelectedClient { get; set; }
    public string Map { get; set; }
    public bool CloseOnLaunch { get; set; }
    public int UserID { get; set; }
    public string PlayerName { get; set; }
    public string PlayerTripcode { get; set; }
    public int RobloxPort { get; set; }
    public int PlayerLimit { get; set; }
    public bool UPnP { get; set; }
    public bool DisabledItemMakerHelp { get; set; }
    public bool DiscordPresence { get; set; }
    public string MapPath { get; set; }
    public string MapPathSnip { get; set; }
    public GraphicsMode GraphicsMode { get; set; }
    public bool ReShade { get; set; }
    public QualityLevel QualityLevel { get; set; }
    public LauncherLayout LauncherLayout { get; set; }
    public bool ReShadeFPSDisplay { get; set; }
    public bool ReShadePerformanceMode { get; set; }
}
#endregion

#region Customization Configuration
public class CustomizationConfig
{
    public CustomizationConfig()
    {
        Hat1 = "NoHat.rbxm";
        Hat2 = "NoHat.rbxm";
        Hat3 = "NoHat.rbxm";
        Face = "DefaultFace.rbxm";
        Head = "DefaultHead.rbxm";
        TShirt = "NoTShirt.rbxm";
        Shirt = "NoShirt.rbxm";
        Pants = "NoPants.rbxm";
        Icon = "NBC";
        Extra = "NoExtra.rbxm";
        HeadColorID = 24;
        TorsoColorID = 23;
        LeftArmColorID = 24;
        RightArmColorID = 24;
        LeftLegColorID = 119;
        RightLegColorID = 119;
        HeadColorString = "Color [A=255, R=245, G=205, B=47]";
        TorsoColorString = "Color [A=255, R=13, G=105, B=172]";
        LeftArmColorString = "Color [A=255, R=245, G=205, B=47]";
        RightArmColorString = "Color [A=255, R=245, G=205, B=47]";
        LeftLegColorString = "Color [A=255, R=164, G=189, B=71]";
        RightLegColorString = "Color [A=255, R=164, G=189, B=71]";
        ExtraSelectionIsHat = false;
        ShowHatsInExtra = false;
        CharacterID = "";
    }

    public string Hat1 { get; set; }
    public string Hat2 { get; set; }
    public string Hat3 { get; set; }
    public string Face { get; set; }
    public string Head { get; set; }
    public string TShirt { get; set; }
    public string Shirt { get; set; }
    public string Pants { get; set; }
    public string Icon { get; set; }
    public string Extra { get; set; }
    public int HeadColorID { get; set; }
    public int TorsoColorID { get; set; }
    public int LeftArmColorID { get; set; }
    public int RightArmColorID { get; set; }
    public int LeftLegColorID { get; set; }
    public int RightLegColorID { get; set; }
    public string HeadColorString { get; set; }
    public string TorsoColorString { get; set; }
    public string LeftArmColorString { get; set; }
    public string RightArmColorString { get; set; }
    public string LeftLegColorString { get; set; }
    public string RightLegColorString { get; set; }
    public bool ExtraSelectionIsHat { get; set; }
    public bool ShowHatsInExtra { get; set; }
    public string CharacterID { get; set; }
}
#endregion

#region Program Information
public class ProgramInfo
{
    public ProgramInfo()
    {
        Version = "";
        Branch = "";
        DefaultClient = "";
        RegisterClient1 = "";
        RegisterClient2 = "";
        DefaultMap = "";
    }

    public string Version { get; set; }
    public string Branch { get; set; }
    public string DefaultClient { get; set; }
    public string RegisterClient1 { get; set; }
    public string RegisterClient2 { get; set; }
    public string DefaultMap { get; set; }
}
#endregion

#region Part Colors
public class PartColors
{
    public int ColorID { get; set; }
    public Color ButtonColor { get; set; }
}

#endregion

#region Discord RPC
public class DiscordRpc
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ReadyCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DisconnectedCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ErrorCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void JoinCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SpectateCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RequestCallback(JoinRequest request);

    public struct EventHandlers
    {
        public ReadyCallback readyCallback;
        public DisconnectedCallback disconnectedCallback;
        public ErrorCallback errorCallback;
        public JoinCallback joinCallback;
        public SpectateCallback spectateCallback;
        public RequestCallback requestCallback;
    }

    [System.Serializable]
    public struct RichPresence
    {
        public string state;
        /* max 128 bytes */
        public string details;
        /* max 128 bytes */
        public long startTimestamp;
        public long endTimestamp;
        public string largeImageKey;
        /* max 32 bytes */
        public string largeImageText;
        /* max 128 bytes */
        public string smallImageKey;
        /* max 32 bytes */
        public string smallImageText;
        /* max 128 bytes */
        public string partyId;
        /* max 128 bytes */
        public int partySize;
        public int partyMax;
        public string matchSecret;
        /* max 128 bytes */
        public string joinSecret;
        /* max 128 bytes */
        public string spectateSecret;
        /* max 128 bytes */
        public bool instance;
    }

    [System.Serializable]
    public struct JoinRequest
    {
        public string userId;
        public string username;
        public string avatar;
    }

    public enum Reply
    {
        No = 0,
        Yes = 1,
        Ignore = 2
    }
    [DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

    [DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Shutdown();

    [DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RunCallbacks();

    [DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
    public static extern void UpdatePresence(ref RichPresence presence);

    [DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Respond(string userId, Reply reply);
}
#endregion

#endregion

#region Function Classes

#region Enum Parser
public static class EnumParser
{
    public static QualityLevel GetQualityLevelForInt(int level)
    {
        switch (level)
        {
            case 1:
                return QualityLevel.VeryLow;
            case 2:
                return QualityLevel.Low;
            case 3:
                return QualityLevel.Medium;
            case 4:
                return QualityLevel.High;
            case 5:
            default:
                return QualityLevel.Ultra;
        }
    }

    public static int GetIntForQualityLevel(QualityLevel level)
    {
        switch (level)
        {
            case QualityLevel.VeryLow:
                return 1;
            case QualityLevel.Low:
                return 2;
            case QualityLevel.Medium:
                return 3;
            case QualityLevel.High:
                return 4;
            case QualityLevel.Ultra:
            default:
                return 5;
        }
    }

    public static GraphicsMode GetGraphicsModeForInt(int level)
    {
        switch (level)
        {
            case 1:
                return GraphicsMode.OpenGL;
            case 2:
                return GraphicsMode.DirectX;
            default:
                return GraphicsMode.None;
        }
    }

    public static int GetIntForGraphicsMode(GraphicsMode level)
    {
        switch (level)
        {
            case GraphicsMode.OpenGL:
                return 1;
            case GraphicsMode.DirectX:
                return 2;
            default:
                return 0;
        }
    }

    public static LauncherLayout GetLauncherLayoutForInt(int level)
    {
        switch (level)
        {
            case 1:
                return LauncherLayout.Extended;
            case 2:
                return LauncherLayout.Compact;
            default:
                return LauncherLayout.None;
        }
    }

    public static int GetIntForLauncherLayout(LauncherLayout level)
    {
        switch (level)
        {
            case LauncherLayout.Extended:
                return 1;
            case LauncherLayout.Compact:
                return 2;
            default:
                return 0;
        }
    }
}
#endregion

#region UPNP
public static class UPnP
{
    public static void InitUPnP(EventHandler<DeviceEventArgs> DeviceFound, EventHandler<DeviceEventArgs> DeviceLost)
    {
        if (GlobalVars.UserConfiguration.UPnP == true)
        {
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.DeviceLost += DeviceLost;
            NatUtility.StartDiscovery();
        }
    }

    public static void StartUPnP(INatDevice device, Protocol protocol, int port)
    {
        if (GlobalVars.UserConfiguration.UPnP == true)
        {
            Mapping checker = device.GetSpecificMapping(protocol, port);
            int mapPublic = checker.PublicPort;
            int mapPrivate = checker.PrivatePort;

            if (mapPublic == -1 && mapPrivate == -1)
            {
                Mapping portmap = new Mapping(protocol, port, port);
                portmap.Description = "Novetus";
                device.CreatePortMap(portmap);
            }
        }
    }

    public static void StopUPnP(INatDevice device, Protocol protocol, int port)
    {
        if (GlobalVars.UserConfiguration.UPnP == true)
        {
            Mapping checker = device.GetSpecificMapping(protocol, port);
            int mapPublic = checker.PublicPort;
            int mapPrivate = checker.PrivatePort;

            if (mapPublic != -1 && mapPrivate != -1)
            {
                Mapping portmap = new Mapping(protocol, port, port);
                portmap.Description = "Novetus";
                device.DeletePortMap(portmap);
            }
        }
    }
}
#endregion

#region URI Registration
//code based off https://stackoverflow.com/questions/35626050/registering-custom-url-handler-in-c-sharp-on-windows-8
public class URIReg
{
    private static string _Protocol = "";
    private static string _ProtocolHandler = "";

    private static readonly string _launch = string.Format(
        "{0}{1}{0} {0}%1{0}", (char)34, Application.ExecutablePath);

    private static readonly Version _win8Version = new Version(6, 2, 9200, 0);

    private static readonly bool _isWin8 =
        Environment.OSVersion.Platform == PlatformID.Win32NT &&
        Environment.OSVersion.Version >= _win8Version;

    public URIReg(string protocol, string protocolhandle)
    {
        _Protocol = protocol;
        _ProtocolHandler = protocolhandle;
    }

    public void Register()
    {
        if (_isWin8) RegisterWin8();
        else RegisterWin7();
    }

    private static void RegisterWin7()
    {
        var regKey = Registry.ClassesRoot.CreateSubKey(_Protocol);

        regKey.CreateSubKey("DefaultIcon")
            .SetValue(null, string.Format("{0}{1},1{0}", (char)34,
                Application.ExecutablePath));

        regKey.SetValue(null, "URL:" + _Protocol + " Protocol");
        regKey.SetValue("URL Protocol", "");

        regKey = regKey.CreateSubKey(@"shell\open\command");
        regKey.SetValue(null, _launch);
    }

    private static void RegisterWin8()
    {
        RegisterWin7();

        var regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes")
            .CreateSubKey(_ProtocolHandler);

        regKey.SetValue(null, _Protocol);

        regKey.CreateSubKey("DefaultIcon")
             .SetValue(null, string.Format("{0}{1},1{0}", (char)34,
                 Application.ExecutablePath));

        regKey.CreateSubKey(@"shell\open\command").SetValue(null, _launch);

        Registry.LocalMachine.CreateSubKey(string.Format(
            @"SOFTWARE\{0}\{1}\Capabilities\ApplicationDescription\URLAssociations",
            Application.CompanyName, Application.ProductName))
            .SetValue(_Protocol, _ProtocolHandler);

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RegisteredApplications")
            .SetValue(Application.ProductName, string.Format(
                @"SOFTWARE\{0}\Capabilities", Application.ProductName));
    }

    public void Unregister()
    {
        if (!_isWin8)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(_Protocol, false);
            return;
        }

        // extra work required.
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes")
            .DeleteSubKeyTree(_ProtocolHandler, false);

        Registry.LocalMachine.DeleteSubKeyTree(string.Format(@"SOFTWARE\{0}\{1}",
            Application.CompanyName, Application.ProductName));

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RegisteredApplications")
            .DeleteValue(Application.ProductName);
    }
}
#endregion

#region INI File Parser
//credit to BLaZiNiX
public class IniFile
{
    public string path;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);

    /// <summary>
    /// INIFile Constructor.
    /// </summary>
    /// <PARAM name="INIPath"></PARAM>
    public IniFile(string INIPath)
    {
        path = INIPath;
    }
    /// <summary>
    /// Write Data to the INI File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// Section name
    /// <PARAM name="Key"></PARAM>
    /// Key Name
    /// <PARAM name="Value"></PARAM>
    /// Value Name
    public void IniWriteValue(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, this.path);
    }

    /// <summary>
    /// Read Data Value From the Ini File
    /// </summary>
    /// <PARAM name="Section"></PARAM>
    /// <PARAM name="Key"></PARAM>
    /// <PARAM name="Default Value. Optional for creating values in case they are invalid."></PARAM>
    /// <returns></returns>
    public string IniReadValue(string Section, string Key, string DefaultValue = "")
    {
        try
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                  255, this.path);
            return temp.ToString();
        }
        catch (Exception)
        {
            IniWriteValue(Section, Key, DefaultValue);
            return IniReadValue(Section, Key);
        }
    }
}
#endregion

#region CryptoRandom
public class CryptoRandom : RandomNumberGenerator
{
    private static RandomNumberGenerator r;

    public CryptoRandom()
    {
        r = Create();
    }

    ///<param name=”buffer”>An array of bytes to contain random numbers.</param>
    public override void GetBytes(byte[] buffer)
    {
        r.GetBytes(buffer);
    }

    public override void GetNonZeroBytes(byte[] data)
    {
        r.GetNonZeroBytes(data);
    }
    public double NextDouble()
    {
        byte[] b = new byte[4];
        r.GetBytes(b);
        return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
    }

    ///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
    ///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
    public int Next(int minValue, int maxValue)
    {
        return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
    }
    public int Next()
    {
        return Next(0, int.MaxValue);
    }

    ///<param name=”maxValue”>The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
    public int Next(int maxValue)
    {
        return Next(0, maxValue);
    }
}
#endregion

#region Tree Node Helper
public static class TreeNodeHelper
{
    public static void ListDirectory(TreeView treeView, string path, string filter = ".*")
    {
        treeView.Nodes.Clear();
        var rootDirectoryInfo = new DirectoryInfo(path);
        treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, filter));
    }

    public static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, string filter = ".*")
    {
        var directoryNode = new TreeNode(directoryInfo.Name);
        foreach (var directory in directoryInfo.GetDirectories())
            directoryNode.Nodes.Add(CreateDirectoryNode(directory, filter));
        foreach (var file in directoryInfo.GetFiles("*" + filter))
            directoryNode.Nodes.Add(new TreeNode(file.Name));
        return directoryNode;
    }

    //https://stackoverflow.com/questions/42295131/searching-a-treeview-for-a-specific-string
    public static TreeNode SearchTreeView(string p_sSearchTerm, TreeNodeCollection p_Nodes)
    {
        foreach (TreeNode node in p_Nodes)
        {
            if (node.Text == p_sSearchTerm)
                return node;

            if (node.Nodes.Count > 0)
            {
                TreeNode child = SearchTreeView(p_sSearchTerm, node.Nodes);
                if (child != null)
                {
                    return child;
                }
            }
        }

        return null;
    }

    public static string GetFolderNameFromPrefix(string source, string seperator = " -")
    {
        try
        {
            string result = source.Substring(0, source.IndexOf(seperator));

            if (Directory.Exists(Directories.MapsDir + @"\\" + result))
            {
                return result + @"\\";
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static void CopyNodes(TreeNodeCollection oldcollection, TreeNodeCollection newcollection)
    {
        foreach (TreeNode node in oldcollection)
        {
            newcollection.Add((TreeNode)node.Clone());
        }
    }

    public static List<TreeNode> GetAllNodes(this TreeView _self)
    {
        List<TreeNode> result = new List<TreeNode>();
        foreach (TreeNode child in _self.Nodes)
        {
            result.AddRange(child.GetAllNodes());
        }
        return result;
    }

    public static List<TreeNode> GetAllNodes(this TreeNode _self)
    {
        List<TreeNode> result = new List<TreeNode>();
        result.Add(_self);
        foreach (TreeNode child in _self.Nodes)
        {
            result.AddRange(child.GetAllNodes());
        }
        return result;
    }

    public static List<TreeNode> Ancestors(this TreeNode node)
    {
        return AncestorsInternal(node).Reverse().ToList();
    }
    public static List<TreeNode> AncestorsAndSelf(this TreeNode node)
    {
        return AncestorsInternal(node, true).Reverse().ToList();
    }
    private static IEnumerable<TreeNode> AncestorsInternal(TreeNode node, bool self = false)
    {
        if (self)
            yield return node;
        while (node.Parent != null)
        {
            node = node.Parent;
            yield return node;
        }
    }
}
#endregion

#region Text Line Remover and Friends
public static class TextLineRemover
{
    public static void RemoveTextLines(IList<string> linesToRemove, string filename, string tempFilename)
    {
        // Initial values
        int lineNumber = 0;
        int linesRemoved = 0;
        DateTime startTime = DateTime.Now;

        // Read file
        using (var sr = new StreamReader(filename))
        {
            // Write new file
            using (var sw = new StreamWriter(tempFilename))
            {
                // Read lines
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lineNumber++;
                    // Look for text to remove
                    if (!ContainsString(line, linesToRemove))
                    {
                        // Keep lines that does not match
                        sw.WriteLine(line);
                    }
                    else
                    {
                        // Ignore lines that DO match
                        linesRemoved++;
                        InvokeOnRemovedLine(new RemovedLineArgs
                        {
                            RemovedLine = line,
                            RemovedLineNumber = lineNumber
                        });
                    }
                }
            }
        }
        // Delete original file
        File.Delete(filename);

        // ... and put the temp file in its place.
        File.Move(tempFilename, filename);

        // Final calculations
        DateTime endTime = DateTime.Now;
        InvokeOnFinished(new FinishedArgs
        {
            LinesRemoved = linesRemoved,
            TotalLines = lineNumber,
            TotalTime = endTime.Subtract(startTime)
        });
    }

    private static bool ContainsString(string line, IEnumerable<string> linesToRemove)
    {
        foreach (var lineToRemove in linesToRemove)
        {
            if (line.Contains(lineToRemove))
                return true;
        }
        return false;
    }

    public static event RemovedLine OnRemovedLine;
    public static event Finished OnFinished;

    public static void InvokeOnFinished(FinishedArgs args)
    {
        Finished handler = OnFinished;
        if (handler != null)
            handler(null, args);
    }

    public static void InvokeOnRemovedLine(RemovedLineArgs args)
    {
        RemovedLine handler = OnRemovedLine;
        if (handler != null)
            handler(null, args);
    }
}

public delegate void Finished(object sender, FinishedArgs args);

public class FinishedArgs
{
    public int TotalLines { get; set; }
    public int LinesRemoved { get; set; }
    public TimeSpan TotalTime { get; set; }
}

public delegate void RemovedLine(object sender, RemovedLineArgs args);

public class RemovedLineArgs
{
    public string RemovedLine { get; set; }
    public int RemovedLineNumber { get; set; }
}
#endregion

#region Simple HTTP Server
//made by aksakalli
public class SimpleHTTPServer
{

    private readonly string[] _indexFiles = {
        "index.html",
        "index.htm",
        "index.php",
        "default.html",
        "default.htm",
        "default.php"
    };

    private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        { ".asf", "video/x-ms-asf" },
        { ".asx", "video/x-ms-asf" },
        { ".avi", "video/x-msvideo" },
        { ".bin", "application/octet-stream" },
        { ".cco", "application/x-cocoa" },
        { ".crt", "application/x-x509-ca-cert" },
        { ".css", "text/css" },
        { ".deb", "application/octet-stream" },
        { ".der", "application/x-x509-ca-cert" },
        { ".dll", "application/octet-stream" },
        { ".dmg", "application/octet-stream" },
        { ".ear", "application/java-archive" },
        { ".eot", "application/octet-stream" },
        { ".exe", "application/octet-stream" },
        { ".flv", "video/x-flv" },
        { ".gif", "image/gif" },
        { ".hqx", "application/mac-binhex40" },
        { ".htc", "text/x-component" },
        { ".htm", "text/html" },
        { ".html", "text/html" },
        { ".ico", "image/x-icon" },
        { ".img", "application/octet-stream" },
        { ".iso", "application/octet-stream" },
        { ".jar", "application/java-archive" },
        { ".jardiff", "application/x-java-archive-diff" },
        { ".jng", "image/x-jng" },
        { ".jnlp", "application/x-java-jnlp-file" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".js", "application/x-javascript" },
        { ".mml", "text/mathml" },
        { ".mng", "video/x-mng" },
        { ".mov", "video/quicktime" },
        { ".mp3", "audio/mpeg" },
        { ".mpeg", "video/mpeg" },
        { ".mpg", "video/mpeg" },
        { ".msi", "application/octet-stream" },
        { ".msm", "application/octet-stream" },
        { ".msp", "application/octet-stream" },
        { ".pdb", "application/x-pilot" },
        { ".pdf", "application/pdf" },
        { ".pem", "application/x-x509-ca-cert" },
        { ".php", "text/html" },
        { ".pl", "application/x-perl" },
        { ".pm", "application/x-perl" },
        { ".png", "image/png" },
        { ".prc", "application/x-pilot" },
        { ".ra", "audio/x-realaudio" },
        { ".rar", "application/x-rar-compressed" },
        { ".rpm", "application/x-redhat-package-manager" },
        { ".rss", "text/xml" },
        { ".run", "application/x-makeself" },
        { ".sea", "application/x-sea" },
        { ".shtml", "text/html" },
        { ".sit", "application/x-stuffit" },
        { ".swf", "application/x-shockwave-flash" },
        { ".tcl", "application/x-tcl" },
        { ".tk", "application/x-tcl" },
        { ".txt", "text/plain" },
        { ".war", "application/java-archive" },
        { ".wbmp", "image/vnd.wap.wbmp" },
        { ".wmv", "video/x-ms-wmv" },
        { ".xml", "text/xml" },
        { ".xpi", "application/x-xpinstall" },
        { ".zip", "application/zip" },
    };
    private Thread _serverThread;
    private string _rootDirectory;
    private HttpListener _listener;
    private int _port;

    public int Port
    {
        get { return _port; }
        private set { }
    }

    /// <summary>
    /// Construct server with given port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    /// <param name="port">Port of the server.</param>
    public SimpleHTTPServer(string path, int port)
    {
        this.Initialize(path, port);
    }

    /// <summary>
    /// Construct server with suitable port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    public SimpleHTTPServer(string path)
    {
        //get an empty port
        TcpListener l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        this.Initialize(path, port);
    }

    /// <summary>
    /// Stop server and dispose all functions.
    /// </summary>
    public void Stop()
    {
        _serverThread.Abort();
        _listener.Stop();
        GlobalVars.IsWebServerOn = false;
    }

    private void Listen()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
        _listener.Start();
        while (true)
        {
            try
            {
                HttpListenerContext context = _listener.GetContext();
                Process(context);
            }
            catch (Exception)
            {

            }
        }
    }

    private string ProcessPhpPage(string phpCompilerPath, string pageFileName)
    {
        Process proc = new Process();
        proc.StartInfo.FileName = phpCompilerPath;
        proc.StartInfo.Arguments = "-d \"display_errors=1\" -d \"error_reporting=E_PARSE\" \"" + pageFileName + "\"";
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.RedirectStandardError = true;
        proc.Start();
        string res = proc.StandardOutput.ReadToEnd();
        proc.StandardOutput.Close();
        proc.Close();
        return res;
    }

    private void Process(HttpListenerContext context)
    {
        string filename = context.Request.Url.AbsolutePath;
        filename = filename.Substring(1);

        if (string.IsNullOrEmpty(filename))
        {
            foreach (string indexFile in _indexFiles)
            {
                if (File.Exists(Path.Combine(_rootDirectory, indexFile)))
                {
                    filename = indexFile;
                    break;
                }
            }
        }

        filename = Path.Combine(_rootDirectory, filename);

        if (File.Exists(filename))
        {
            try
            {
                var ext = new FileInfo(filename);

                if (ext.Extension == ".php")
                {
                    string output = ProcessPhpPage(Directories.ConfigDirData + "\\php\\php.exe", filename);
                    byte[] input = ASCIIEncoding.UTF8.GetBytes(output);
                    //Adding permanent http response headers
                    string mime;
                    context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));
                    context.Response.OutputStream.Write(input, 0, input.Length);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
                else
                {
                    Stream input = new FileStream(filename, FileMode.Open);
                    //Adding permanent http response headers
                    string mime;
                    context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                    byte[] buffer = new byte[1024 * 16];
                    int nbytes;
                    while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        context.Response.OutputStream.Write(buffer, 0, nbytes);
                    input.Close();

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Flush();
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

        }

        context.Response.OutputStream.Close();
    }

    private void Initialize(string path, int port)
    {
        this._rootDirectory = path;
        this._port = port;
        _serverThread = new Thread(this.Listen);
        _serverThread.Start();
        GlobalVars.IsWebServerOn = true;
    }
}
#endregion

#endregion

#endregion