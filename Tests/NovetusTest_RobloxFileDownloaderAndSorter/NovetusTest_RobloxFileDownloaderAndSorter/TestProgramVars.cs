using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NovetusTest_RobloxFileDownloaderAndSorter
{
    public class AssetCacheDef
    {
        public AssetCacheDef(string clas, string[] id, string[] ext, string[] dir, string[] gamedir)
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

    class GlobalVars
    {
        public static readonly string RootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string BasePath = RootPath.Replace(@"\", @"\\");
        public static readonly string DataPath = BasePath + "\\shareddata";
        public static readonly string AssetCacheDir = DataPath + "\\assetcache";
        public static readonly string AssetCacheDirSky = AssetCacheDir + "\\sky";
        public static readonly string AssetCacheDirFonts = AssetCacheDir + "\\fonts";
        public static readonly string AssetCacheDirSounds = AssetCacheDir + "\\sounds";
        public static readonly string AssetCacheDirTextures = AssetCacheDir + "\\textures";

        public static readonly string BaseGameDir = "rbxasset://../../../";
        public static readonly string SharedDataGameDir = BaseGameDir + "shareddata/";
        public static readonly string AssetCacheGameDir = SharedDataGameDir + "assetcache/";
        public static readonly string AssetCacheFontsGameDir = AssetCacheGameDir + "fonts/";
        public static readonly string AssetCacheSkyGameDir = AssetCacheGameDir + "sky/";
        public static readonly string AssetCacheSoundsGameDir = AssetCacheGameDir + "sounds/";
        public static readonly string AssetCacheTexturesGameDir = AssetCacheGameDir + "textures/";

        public static AssetCacheDef Fonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId"}, new string[] { ".mesh", ".png" }, new string[] { AssetCacheDirFonts, AssetCacheDirTextures }, new string[] { AssetCacheFontsGameDir, AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Sky { get { return new AssetCacheDef("Sky", new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" }, new string[] { ".png" }, new string[] { AssetCacheDirSky }, new string[] { AssetCacheSkyGameDir }); } }
        public static AssetCacheDef Decal { get { return new AssetCacheDef("Decal", new string[] { "Texture" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Texture { get { return new AssetCacheDef("Texture", new string[] { "Texture" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef HopperBin { get { return new AssetCacheDef("HopperBin", new string[] { "TextureId" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Tool { get { return new AssetCacheDef("Tool", new string[] { "TextureId" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Sound { get { return new AssetCacheDef("Sound", new string[] { "SoundId" }, new string[] { ".wav" }, new string[] { AssetCacheDirSounds }, new string[] { AssetCacheSoundsGameDir }); } }
        public static AssetCacheDef ImageLabel { get { return new AssetCacheDef("ImageLabel", new string[] { "Image" }, new string[] { ".png" }, new string[] { AssetCacheDirTextures }, new string[] { AssetCacheTexturesGameDir }); } }
    }
}
