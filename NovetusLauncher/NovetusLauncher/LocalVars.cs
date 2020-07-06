namespace NovetusLauncher
{
    #region LocalVars
    class LocalVars
    {
        public static int Clicks = 0;
        public static string prevsplash = "";
        public static int DefaultRobloxPort = 53640;
        public static bool LocalPlayMode = false;
    }
    #endregion

    #region Roblox Type Definitions
    public class RobloxTypeDefs
    {
        public static AssetCacheDef Fonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId" }, new string[] { ".mesh", ".png" }, new string[] { Directories.AssetCacheDirFonts, Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheFontsGameDir, Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Sky { get { return new AssetCacheDef("Sky", new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirSky }, new string[] { Directories.AssetCacheSkyGameDir }); } }
        public static AssetCacheDef Decal { get { return new AssetCacheDef("Decal", new string[] { "Texture" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Texture { get { return new AssetCacheDef("Texture", new string[] { "Texture" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef HopperBin { get { return new AssetCacheDef("HopperBin", new string[] { "TextureId" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Tool { get { return new AssetCacheDef("Tool", new string[] { "TextureId" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Sound { get { return new AssetCacheDef("Sound", new string[] { "SoundId" }, new string[] { ".wav" }, new string[] { Directories.AssetCacheDirSounds }, new string[] { Directories.AssetCacheSoundsGameDir }); } }
        public static AssetCacheDef ImageLabel { get { return new AssetCacheDef("ImageLabel", new string[] { "Image" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Shirt { get { return new AssetCacheDef("Shirt", new string[] { "ShirtTemplate" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef ShirtGraphic { get { return new AssetCacheDef("ShirtGraphic", new string[] { "Graphic" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Pants { get { return new AssetCacheDef("Pants", new string[] { "PantsTemplate" }, new string[] { ".png" }, new string[] { Directories.AssetCacheDirTextures }, new string[] { Directories.AssetCacheTexturesGameDir }); } }
        public static AssetCacheDef Script { get { return new AssetCacheDef("Script", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { Directories.AssetCacheDirScripts }, new string[] { Directories.AssetCacheScriptsGameDir }); } }
        public static AssetCacheDef LocalScript { get { return new AssetCacheDef("LocalScript", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { Directories.AssetCacheDirScripts }, new string[] { Directories.AssetCacheScriptsGameDir }); } }
        //item defs below
        public static AssetCacheDef ItemHatFonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId" }, new string[] { ".mesh", ".png" }, new string[] { Directories.hatdirFonts, Directories.hatdirTextures }, new string[] { Directories.hatGameDirFonts, Directories.hatGameDirTextures }); } }
        public static AssetCacheDef ItemHatSound { get { return new AssetCacheDef("Sound", new string[] { "SoundId" }, new string[] { ".wav" }, new string[] { Directories.hatdirSounds }, new string[] { Directories.hatGameDirSounds }); } }
        public static AssetCacheDef ItemHatScript { get { return new AssetCacheDef("Script", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { Directories.hatdirScripts }, new string[] { Directories.hatGameDirScripts }); } }
        public static AssetCacheDef ItemHatLocalScript { get { return new AssetCacheDef("LocalScript", new string[] { "LinkedSource" }, new string[] { ".lua" }, new string[] { Directories.hatdirScripts }, new string[] { Directories.hatGameDirScripts }); } }
        public static AssetCacheDef ItemHeadFonts { get { return new AssetCacheDef("SpecialMesh", new string[] { "MeshId", "TextureId" }, new string[] { ".mesh", ".png" }, new string[] { Directories.headdirFonts, Directories.headdirTextures }, new string[] { Directories.headGameDirFonts, Directories.headGameDirTextures }); } }
        public static AssetCacheDef ItemFaceTexture { get { return new AssetCacheDef("Decal", new string[] { "Texture" }, new string[] { ".png" }, new string[] { Directories.facedirTextures }, new string[] { Directories.faceGameDirTextures }); } }
        public static AssetCacheDef ItemShirtTexture { get { return new AssetCacheDef("Shirt", new string[] { "ShirtTemplate" }, new string[] { ".png" }, new string[] { Directories.shirtdirTextures }, new string[] { Directories.shirtGameDirTextures }); } }
        public static AssetCacheDef ItemTShirtTexture { get { return new AssetCacheDef("ShirtGraphic", new string[] { "Graphic" }, new string[] { ".png" }, new string[] { Directories.tshirtdirTextures }, new string[] { Directories.tshirtGameDirTextures }); } }
        public static AssetCacheDef ItemPantsTexture { get { return new AssetCacheDef("Pants", new string[] { "PantsTemplate" }, new string[] { ".png" }, new string[] { Directories.pantsdirTextures }, new string[] { Directories.pantsGameDirTextures }); } }
    }
    #endregion
}
