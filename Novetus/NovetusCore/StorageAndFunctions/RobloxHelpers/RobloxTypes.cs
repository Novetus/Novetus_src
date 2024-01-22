using System.Collections.Generic;

namespace Novetus.Core
{
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
        Script,
        HeadNoCustomMesh,
        Package
    }
    #endregion

    #region Asset Cache Definition
    public class AssetCacheDefBasic
    {
        public AssetCacheDefBasic(string clas, string[] id)
        {
            Class = clas;
            Id = id;
        }

        public string Class { get; set; }
        public string[] Id { get; set; }
    }

    public class AssetCacheDef : AssetCacheDefBasic
    {
        public AssetCacheDef(string clas, string[] id, string[] ext,
            string[] dir, string[] gamedir) : base(clas, id)
        {
            Ext = ext;
            Dir = dir;
            GameDir = gamedir;
        }

        public string[] Ext { get; set; }
        public string[] Dir { get; set; }
        public string[] GameDir { get; set; }
    }
    #endregion

    #region Roblox Type Definitions
    public struct RobloxDefs
    {
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
}
