#region Usings
using System.IO;
using System.Reflection;
#endregion

namespace Novetus.Core
{
    #region Global Paths

    public class GlobalPaths
    {
        #region Base Game Paths
        public static readonly string RootPathLauncher = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string BasePathLauncher = RootPathLauncher.Replace(@"\", @"\\");
        public static readonly string RootPath = Directory.GetParent(RootPathLauncher).ToString();
#if BASICLAUNCHER
        public static readonly string BasePath = BasePathLauncher + @"\\data";
#else
        public static readonly string BasePath = RootPath.Replace(@"\", @"\\");
#endif
        public static readonly string DataPath = BasePath + @"\\shareddata";
        public static readonly string AssetsPath = BasePath + @"\\assets";
        public static readonly string BinDir = BasePath + @"\\bin";
        public static readonly string ConfigDir = BasePath + @"\\config";
        public static readonly string LogDir = BasePath + @"\\logs";
        public static readonly string ConfigDirClients = ConfigDir + @"\\clients";
        public static readonly string ConfigDirTemplates = ConfigDir + @"\\itemtemplates";
        public static readonly string DataDir = ConfigDir + @"\\launcherdata";
        public static readonly string ClientDir = BasePath + @"\\clients";
        public static readonly string MapsDir = BasePath + @"\\maps";
        public static readonly string AddonDir = BasePath + @"\\addons";
        public static readonly string AddonCoreDir = AddonDir + @"\\core";
        public static readonly string AddonNovetusExts = AddonDir + @"\\novetusexts";
        public static readonly string NovetusExtsWebProxy = AddonNovetusExts + @"\\webproxy";
        public static readonly string MapsDirCustom = MapsDir + @"\\Custom";
        public static readonly string MiscDir = BasePath + @"\\misc";
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
        public static string AssetCacheDir = DataPath + "\\assetcache";
        public static string AssetCacheDirAssets = AssetCacheDir + "\\assets";

        public static string AssetCacheGameDir = SharedDataGameDir + "assetcache/";
        public static string AssetCacheAssetsGameDir = AssetCacheGameDir + "assets/";
#endregion

#region Item Dirs
        public static readonly string hatdirFonts = hatdir + DirFonts;
        public static readonly string hatdirTextures = hatdir + DirTextures;
        public static readonly string hatdirSounds = hatdir + DirSounds;
        public static readonly string hatdirScripts = hatdir + DirScripts;
        public static readonly string facedirTextures = facedir; //+ DirTextures;
        public static readonly string headdirFonts = headdir + DirFonts;
        public static readonly string headdirTextures = headdir + DirTextures;
        public static readonly string tshirtdirTextures = tshirtdir; //+ DirTextures;
        public static readonly string shirtdirTextures = shirtdir + DirTextures;
        public static readonly string pantsdirTextures = pantsdir + DirTextures;
        public static readonly string extradirFonts = extradir + DirFonts;

        public static readonly string hatGameDirFonts = hatGameDir + FontsGameDir;
        public static readonly string hatGameDirTextures = hatGameDir + TexturesGameDir;
        public static readonly string hatGameDirSounds = hatGameDir + SoundsGameDir;
        public static readonly string hatGameDirScripts = hatGameDir + ScriptsGameDir;
        public static readonly string faceGameDirTextures = faceGameDir; //+ TexturesGameDir;
        public static readonly string headGameDirFonts = headGameDir + FontsGameDir;
        public static readonly string headGameDirTextures = headGameDir + TexturesGameDir;
        public static readonly string tshirtGameDirTextures = tshirtGameDir; //+ TexturesGameDir;
        public static readonly string shirtGameDirTextures = shirtGameDir + TexturesGameDir;
        public static readonly string pantsGameDirTextures = pantsGameDir + TexturesGameDir;
        public static readonly string extraGameDirFonts = extraGameDir + FontsGameDir;
        #endregion

        #endregion

        #region File Names
        public static readonly string ConfigName = "config.json";
        public static string ConfigNameCustomization = "config_customization.json";
        public static readonly string InfoName = "info.json";
        public static readonly string ScriptName = "CSMPFunctions";
        public static readonly string ScriptGenName = "CSMPBoot";
        public static readonly string ContentProviderXMLName = "ContentProviders.json";
        public static readonly string PartColorXMLName = "PartColors.json";
        public static readonly string FileDeleteFilterName = "FileDeleteFilter.txt";
        public static readonly string InitialFileListIgnoreFilterName = "InitialFileListIgnoreFilter.txt";
        public static readonly string ServerInfoFileName = "serverinfo.txt";
        public static readonly string ConsoleHelpFileName = "consolehelp.txt";
        public static readonly string ClientScriptDocumentationFileName = "documentation.txt";
        public static readonly string AddonLoaderFileName = "AddonLoader.lua";
        public static readonly string AssetFixerPatternFileName = "assetfixer_pattern.txt";
        public static readonly string TermListFileName = "term-list.txt";
#endregion
    }
#endregion
}
