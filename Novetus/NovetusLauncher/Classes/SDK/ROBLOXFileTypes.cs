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
    Pants,
    HeadNoCustomMesh,
    //for downloading script assets
    //Script
}
#endregion

#region Roblox Type Definitions
public struct RobloxDefs
{
    public static VarStorage.AssetCacheDef Fonts
    {
        get
        {
            return new VarStorage.AssetCacheDef("SpecialMesh",
            new string[] { "MeshId", "TextureId" },
            new string[] { ".mesh", ".png" },
            new string[] { GlobalPaths.AssetCacheDirFonts, GlobalPaths.AssetCacheDirTextures },
            new string[] { GlobalPaths.AssetCacheFontsGameDir, GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Sky
    {
        get
        {
            return new VarStorage.AssetCacheDef("Sky",
                new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirSky },
                new string[] { GlobalPaths.AssetCacheSkyGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Decal
    {
        get
        {
            return new VarStorage.AssetCacheDef("Decal",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Texture
    {
        get
        {
            return new VarStorage.AssetCacheDef("Texture",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef HopperBin
    {
        get
        {
            return new VarStorage.AssetCacheDef("HopperBin",
              new string[] { "TextureId" },
              new string[] { ".png" },
              new string[] { GlobalPaths.AssetCacheDirTextures },
              new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Tool
    {
        get
        {
            return new VarStorage.AssetCacheDef("Tool",
                new string[] { "TextureId" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Sound
    {
        get
        {
            return new VarStorage.AssetCacheDef("Sound",
                new string[] { "SoundId" },
                new string[] { ".wav" },
                new string[] { GlobalPaths.AssetCacheDirSounds },
                new string[] { GlobalPaths.AssetCacheSoundsGameDir });
        }
    }

    public static VarStorage.AssetCacheDef ImageLabel
    {
        get
        {
            return new VarStorage.AssetCacheDef("ImageLabel",
                new string[] { "Image" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Shirt
    {
        get
        {
            return new VarStorage.AssetCacheDef("Shirt",
                new string[] { "ShirtTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef ShirtGraphic
    {
        get
        {
            return new VarStorage.AssetCacheDef("ShirtGraphic",
                new string[] { "Graphic" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Pants
    {
        get
        {
            return new VarStorage.AssetCacheDef("Pants",
                new string[] { "PantsTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static VarStorage.AssetCacheDef Script
    {
        get
        {
            return new VarStorage.AssetCacheDef("Script",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.AssetCacheDirScripts },
                new string[] { GlobalPaths.AssetCacheScriptsGameDir });
        }
    }

    public static VarStorage.AssetCacheDef LocalScript
    {
        get
        {
            return new VarStorage.AssetCacheDef("LocalScript",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.AssetCacheDirScripts },
                new string[] { GlobalPaths.AssetCacheScriptsGameDir });
        }
    }

    //item defs below
    public static VarStorage.AssetCacheDef ItemHatFonts
    {
        get
        {
            return new VarStorage.AssetCacheDef("SpecialMesh",
                new string[] { "MeshId", "TextureId" },
                new string[] { ".mesh", ".png" },
                new string[] { GlobalPaths.hatdirFonts, GlobalPaths.hatdirTextures },
                new string[] { GlobalPaths.hatGameDirFonts, GlobalPaths.hatGameDirTextures });
        }
    }

    public static VarStorage.AssetCacheDef ItemHatSound
    {
        get
        {
            return new VarStorage.AssetCacheDef("Sound",
                new string[] { "SoundId" },
                new string[] { ".wav" },
                new string[] { GlobalPaths.hatdirSounds },
                new string[] { GlobalPaths.hatGameDirSounds });
        }
    }

    public static VarStorage.AssetCacheDef ItemHatScript
    {
        get
        {
            return new VarStorage.AssetCacheDef("Script",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.hatdirScripts },
                new string[] { GlobalPaths.hatGameDirScripts });
        }
    }

    public static VarStorage.AssetCacheDef ItemHatLocalScript
    {
        get
        {
            return new VarStorage.AssetCacheDef("LocalScript",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.hatdirScripts },
                new string[] { GlobalPaths.hatGameDirScripts });
        }
    }

    public static VarStorage.AssetCacheDef ItemHeadFonts
    {
        get
        {
            return new VarStorage.AssetCacheDef("SpecialMesh",
                new string[] { "MeshId", "TextureId" },
                new string[] { ".mesh", ".png" },
                new string[] { GlobalPaths.headdirFonts, GlobalPaths.headdirTextures },
                new string[] { GlobalPaths.headGameDirFonts, GlobalPaths.headGameDirTextures });
        }
    }

    public static VarStorage.AssetCacheDef ItemFaceTexture
    {
        get
        {
            return new VarStorage.AssetCacheDef("Decal",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.facedirTextures },
                new string[] { GlobalPaths.faceGameDirTextures });
        }
    }

    public static VarStorage.AssetCacheDef ItemShirtTexture
    {
        get
        {
            return new VarStorage.AssetCacheDef("Shirt",
                new string[] { "ShirtTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.shirtdirTextures },
                new string[] { GlobalPaths.shirtGameDirTextures });
        }
    }

    public static VarStorage.AssetCacheDef ItemTShirtTexture
    {
        get
        {
            return new VarStorage.AssetCacheDef("ShirtGraphic",
                new string[] { "Graphic" },
                new string[] { ".png" },
                new string[] { GlobalPaths.tshirtdirTextures },
                new string[] { GlobalPaths.tshirtGameDirTextures });
        }
    }

    public static VarStorage.AssetCacheDef ItemPantsTexture
    {
        get
        {
            return new VarStorage.AssetCacheDef("Pants",
                new string[] { "PantsTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.pantsdirTextures },
                new string[] { GlobalPaths.pantsGameDirTextures });
        }
    }
}
#endregion
