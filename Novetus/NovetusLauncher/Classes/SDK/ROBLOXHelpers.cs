#region Vector3
public class Vector3
{
    public double X;
    public double Y;
    public double Z;

    public Vector3(double aX, double aY, double aZ)
    {
        X = aX;
        Y = aY;
        Z = aZ;
    }
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
    Pants,
    HeadNoCustomMesh,
    //for downloading script assets
    //Script
}
#endregion

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

#region Roblox Type Definitions
public struct RobloxDefs
{
    public static AssetCacheDef Fonts
    {
        get
        {
            return new AssetCacheDef("SpecialMesh",
            new string[] { "MeshId", "TextureId" },
            new string[] { ".mesh", ".png" },
            new string[] { GlobalPaths.AssetCacheDirFonts, GlobalPaths.AssetCacheDirTextures },
            new string[] { GlobalPaths.AssetCacheFontsGameDir, GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Sky
    {
        get
        {
            return new AssetCacheDef("Sky",
                new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirSky },
                new string[] { GlobalPaths.AssetCacheSkyGameDir });
        }
    }

    public static AssetCacheDef Decal
    {
        get
        {
            return new AssetCacheDef("Decal",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Texture
    {
        get
        {
            return new AssetCacheDef("Texture",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef HopperBin
    {
        get
        {
            return new AssetCacheDef("HopperBin",
              new string[] { "TextureId" },
              new string[] { ".png" },
              new string[] { GlobalPaths.AssetCacheDirTextures },
              new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Tool
    {
        get
        {
            return new AssetCacheDef("Tool",
                new string[] { "TextureId" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Sound
    {
        get
        {
            return new AssetCacheDef("Sound",
                new string[] { "SoundId" },
                new string[] { ".wav" },
                new string[] { GlobalPaths.AssetCacheDirSounds },
                new string[] { GlobalPaths.AssetCacheSoundsGameDir });
        }
    }

    public static AssetCacheDef ImageLabel
    {
        get
        {
            return new AssetCacheDef("ImageLabel",
                new string[] { "Image" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Shirt
    {
        get
        {
            return new AssetCacheDef("Shirt",
                new string[] { "ShirtTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef ShirtGraphic
    {
        get
        {
            return new AssetCacheDef("ShirtGraphic",
                new string[] { "Graphic" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Pants
    {
        get
        {
            return new AssetCacheDef("Pants",
                new string[] { "PantsTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Script
    {
        get
        {
            return new AssetCacheDef("Script",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.AssetCacheDirScripts },
                new string[] { GlobalPaths.AssetCacheScriptsGameDir });
        }
    }

    public static AssetCacheDef LocalScript
    {
        get
        {
            return new AssetCacheDef("LocalScript",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.AssetCacheDirScripts },
                new string[] { GlobalPaths.AssetCacheScriptsGameDir });
        }
    }

    //item defs below
    public static AssetCacheDef ItemHatFonts
    {
        get
        {
            return new AssetCacheDef("SpecialMesh",
                new string[] { "MeshId", "TextureId" },
                new string[] { ".mesh", ".png" },
                new string[] { GlobalPaths.hatdirFonts, GlobalPaths.hatdirTextures },
                new string[] { GlobalPaths.hatGameDirFonts, GlobalPaths.hatGameDirTextures });
        }
    }

    public static AssetCacheDef ItemHatSound
    {
        get
        {
            return new AssetCacheDef("Sound",
                new string[] { "SoundId" },
                new string[] { ".wav" },
                new string[] { GlobalPaths.hatdirSounds },
                new string[] { GlobalPaths.hatGameDirSounds });
        }
    }

    public static AssetCacheDef ItemHatScript
    {
        get
        {
            return new AssetCacheDef("Script",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.hatdirScripts },
                new string[] { GlobalPaths.hatGameDirScripts });
        }
    }

    public static AssetCacheDef ItemHatLocalScript
    {
        get
        {
            return new AssetCacheDef("LocalScript",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.hatdirScripts },
                new string[] { GlobalPaths.hatGameDirScripts });
        }
    }

    public static AssetCacheDef ItemHeadFonts
    {
        get
        {
            return new AssetCacheDef("SpecialMesh",
                new string[] { "MeshId", "TextureId" },
                new string[] { ".mesh", ".png" },
                new string[] { GlobalPaths.headdirFonts, GlobalPaths.headdirTextures },
                new string[] { GlobalPaths.headGameDirFonts, GlobalPaths.headGameDirTextures });
        }
    }

    public static AssetCacheDef ItemFaceTexture
    {
        get
        {
            return new AssetCacheDef("Decal",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.facedirTextures },
                new string[] { GlobalPaths.faceGameDirTextures });
        }
    }

    public static AssetCacheDef ItemShirtTexture
    {
        get
        {
            return new AssetCacheDef("Shirt",
                new string[] { "ShirtTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.shirtdirTextures },
                new string[] { GlobalPaths.shirtGameDirTextures });
        }
    }

    public static AssetCacheDef ItemTShirtTexture
    {
        get
        {
            return new AssetCacheDef("ShirtGraphic",
                new string[] { "Graphic" },
                new string[] { ".png" },
                new string[] { GlobalPaths.tshirtdirTextures },
                new string[] { GlobalPaths.tshirtGameDirTextures });
        }
    }

    public static AssetCacheDef ItemPantsTexture
    {
        get
        {
            return new AssetCacheDef("Pants",
                new string[] { "PantsTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.pantsdirTextures },
                new string[] { GlobalPaths.pantsGameDirTextures });
        }
    }
}
#endregion
