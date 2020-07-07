#region Usings
using System.IO;
using System.Reflection;
#endregion

#region Global Paths

public class GlobalPaths
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
