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
    public static readonly string ConfigDir = BasePath + @"\\config";
    public static readonly string ConfigDirClients = ConfigDir + @"\\clients";
    public static readonly string ConfigDirData = BasePathLauncher + @"\\data";
    public static readonly string ClientDir = BasePath + @"\\clients";
    public static readonly string MapsDir = BasePath + @"\\maps";
    public static readonly string MapsDirBase = "maps";
    public static readonly string BaseGameDir = "rbxasset://../../../";
    public static readonly string AltBaseGameDir = "rbxasset://";
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

    #region Asset Cache Paths

    #region Base Paths
    public static readonly string DirFonts = "\\fonts";
    public static readonly string DirSounds = "\\sounds";
    public static readonly string DirTextures = "\\textures";
    public static readonly string DirScripts = "\\scripts";
    public static readonly string FontsGameDir = "fonts/";
    public static readonly string SoundsGameDir = "sounds/";
    public static readonly string TexturesGameDir = "textures/";
    public static readonly string ScriptsGameDir = "scripts/";
    #endregion

    #region Asset Dirs
    public static readonly string AssetCacheDir = GlobalPaths.DataPath + "\\assetcache";
    public static readonly string AssetCacheDirSky = AssetCacheDir + "\\sky";
    public static readonly string AssetCacheDirFonts = AssetCacheDir + DirFonts;
    public static readonly string AssetCacheDirSounds = AssetCacheDir + DirSounds;
    public static readonly string AssetCacheDirTextures = AssetCacheDir + DirTextures;
    public static readonly string AssetCacheDirTexturesGUI = AssetCacheDirTextures + "\\gui";
    public static readonly string AssetCacheDirScripts = AssetCacheDir + DirScripts;
    //public static readonly string AssetCacheDirScriptAssets = AssetCacheDir + "\\scriptassets";

    public static readonly string AssetCacheGameDir = GlobalPaths.SharedDataGameDir + "assetcache/";
    public static readonly string AssetCacheFontsGameDir = AssetCacheGameDir + FontsGameDir;
    public static readonly string AssetCacheSkyGameDir = AssetCacheGameDir + "sky/";
    public static readonly string AssetCacheSoundsGameDir = AssetCacheGameDir + SoundsGameDir;
    public static readonly string AssetCacheTexturesGameDir = AssetCacheGameDir + TexturesGameDir;
    public static readonly string AssetCacheTexturesGUIGameDir = AssetCacheTexturesGameDir + "gui/";
    public static readonly string AssetCacheScriptsGameDir = AssetCacheGameDir + ScriptsGameDir;
    //public static readonly string AssetCacheScriptAssetsGameDir = AssetCacheGameDir + "scriptassets/";
    #endregion

    #region Item Dirs
    public static readonly string hatdirFonts = GlobalPaths.hatdir + DirFonts;
    public static readonly string hatdirTextures = GlobalPaths.hatdir + DirTextures;
    public static readonly string hatdirSounds = GlobalPaths.hatdir + DirSounds;
    public static readonly string hatdirScripts = GlobalPaths.hatdir + DirScripts;
    public static readonly string facedirTextures = GlobalPaths.facedir + DirTextures;
    public static readonly string headdirFonts = GlobalPaths.headdir + DirFonts;
    public static readonly string headdirTextures = GlobalPaths.headdir + DirTextures;
    public static readonly string tshirtdirTextures = GlobalPaths.tshirtdir + DirTextures;
    public static readonly string shirtdirTextures = GlobalPaths.shirtdir + DirTextures;
    public static readonly string pantsdirTextures = GlobalPaths.pantsdir + DirTextures;

    public static readonly string hatGameDirFonts = GlobalPaths.hatGameDir + FontsGameDir;
    public static readonly string hatGameDirTextures = GlobalPaths.hatGameDir + TexturesGameDir;
    public static readonly string hatGameDirSounds = GlobalPaths.hatGameDir + SoundsGameDir;
    public static readonly string hatGameDirScripts = GlobalPaths.hatGameDir + ScriptsGameDir;
    public static readonly string faceGameDirTextures = GlobalPaths.faceGameDir + TexturesGameDir;
    public static readonly string headGameDirFonts = GlobalPaths.headGameDir + FontsGameDir;
    public static readonly string headGameDirTextures = GlobalPaths.headGameDir + TexturesGameDir;
    public static readonly string tshirtGameDirTextures = GlobalPaths.tshirtGameDir + TexturesGameDir;
    public static readonly string shirtGameDirTextures = GlobalPaths.shirtGameDir + TexturesGameDir;
    public static readonly string pantsGameDirTextures = GlobalPaths.pantsGameDir + TexturesGameDir;
    #endregion

    #endregion

    #region File Names
    public static readonly string ConfigName = "config.ini";
    public static string ConfigNameCustomization = "config_customization.ini";
    public static readonly string InfoName = "info.ini";
    public static readonly string ScriptName = "CSMPFunctions";
    public static readonly string ScriptGenName = "CSMPBoot";
    #endregion

    #region Empty Paths (automatically changed)
    public static string AddonScriptPath = "";
    #endregion
}
#endregion
