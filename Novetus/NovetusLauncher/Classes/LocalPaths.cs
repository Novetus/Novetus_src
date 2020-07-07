
namespace NovetusLauncher
{
    class LocalPaths
    {
        //plugins???? UNCOMMENT.
        //public static readonly string BasePath = RootPath.Replace(@"\", @"\\");

        //assetcache
        public static readonly string DirFonts = "\\fonts";
        public static readonly string DirSounds = "\\sounds";
        public static readonly string DirTextures = "\\textures";
        public static readonly string DirScripts = "\\scripts";
        public static readonly string FontsGameDir = "fonts/";
        public static readonly string SoundsGameDir = "sounds/";
        public static readonly string TexturesGameDir = "textures/";
        public static readonly string ScriptsGameDir = "scripts/";
        //item asset dirs
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

        public static readonly string AssetCacheDir = GlobalPaths.DataPath + "\\assetcache";
        public static readonly string AssetCacheDirSky = AssetCacheDir + "\\sky";
        public static readonly string AssetCacheDirFonts = AssetCacheDir + DirFonts;
        public static readonly string AssetCacheDirSounds = AssetCacheDir + DirSounds;
        public static readonly string AssetCacheDirTextures = AssetCacheDir + DirTextures;
        public static readonly string AssetCacheDirTexturesGUI = AssetCacheDirTextures + "\\gui";
        public static readonly string AssetCacheDirScripts = AssetCacheDir + DirScripts;

        public static readonly string AssetCacheGameDir = GlobalPaths.SharedDataGameDir + "assetcache/";
        public static readonly string AssetCacheFontsGameDir = AssetCacheGameDir + FontsGameDir;
        public static readonly string AssetCacheSkyGameDir = AssetCacheGameDir + "sky/";
        public static readonly string AssetCacheSoundsGameDir = AssetCacheGameDir + SoundsGameDir;
        public static readonly string AssetCacheTexturesGameDir = AssetCacheGameDir + TexturesGameDir;
        public static readonly string AssetCacheTexturesGUIGameDir = AssetCacheTexturesGameDir + "gui/";
        public static readonly string AssetCacheScriptsGameDir = AssetCacheGameDir + ScriptsGameDir;
    }
}
