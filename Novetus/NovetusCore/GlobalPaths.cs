#region Usings
using System.IO;
using System.Reflection;
#endregion

#region Global Paths

public class GlobalPaths
{
    #region Base Game Paths
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
    #endregion

    #region Customization Paths
    public static readonly string CustomPlayerDir = DataPath + "\\charcustom";
    public static readonly string hatdir = CustomPlayerDir + "\\hats";
    public static readonly string facedir = CustomPlayerDir + "\\faces";
    public static readonly string headdir = CustomPlayerDir + "\\heads";
    public static readonly string tshirtdir = CustomPlayerDir + "\\tshirts";
    public static readonly string shirtdir = CustomPlayerDir + "\\shirts";
    public static readonly string pantsdir = CustomPlayerDir + "\\pants";
    public static readonly string extradir = CustomPlayerDir + "\\custom";
    public static readonly string extradirIcons = extradir + "\\icons";

    public static readonly string CharCustomGameDir = SharedDataGameDir + "charcustom/";
    public static readonly string hatGameDir = CharCustomGameDir + "hats/";
    public static readonly string faceGameDir = CharCustomGameDir + "faces/";
    public static readonly string headGameDir = CharCustomGameDir + "heads/";
    public static readonly string tshirtGameDir = CharCustomGameDir + "tshirts/";
    public static readonly string shirtGameDir = CharCustomGameDir + "shirts/";
    public static readonly string pantsGameDir = CharCustomGameDir + "pants/";
    public static readonly string extraGameDir = CharCustomGameDir + "custom/";
    #endregion

    #region Web Server Paths
    public static string WebServer_CustomPlayerDir = GlobalVars.WebServerURI + "/charcustom/";
    public static string WebServer_HatDir = WebServer_CustomPlayerDir + "hats/";
    public static string WebServer_FaceDir = WebServer_CustomPlayerDir + "faces/";
    public static string WebServer_HeadDir = WebServer_CustomPlayerDir + "heads/";
    public static string WebServer_TShirtDir = WebServer_CustomPlayerDir + "tshirts/";
    public static string WebServer_ShirtDir = WebServer_CustomPlayerDir + "shirts/";
    public static string WebServer_PantsDir = WebServer_CustomPlayerDir + "pants/";
    public static string WebServer_ExtraDir = WebServer_CustomPlayerDir + "custom/";
    #endregion

    #region File Names
    public static readonly string ConfigName = "config.ini";
    public static string ConfigNameCustomization = "config_customization.ini";
    public static readonly string InfoName = "info.ini";
    public static readonly string ScriptName = "CSMPFunctions";
    public static readonly string ScriptGenName = "CSMPBoot";
    #endregion

    #region Empty Paths (automatically changed)
    public static string FullMapPath = "";
    public static string AddonScriptPath = "";
    #endregion
}
#endregion
