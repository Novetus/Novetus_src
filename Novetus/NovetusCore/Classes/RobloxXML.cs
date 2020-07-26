#region Usings
using System;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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
    Pants
}
#endregion

#region XML Types
public enum XMLTypes
{
    Token,
    Bool,
    Float
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

#region Roblox XML Parser
public static class RobloxXML
{
    public static void DownloadFromNodes(string filepath, VarStorage.AssetCacheDef assetdef, string name = "", string meshname = "")
    {
        DownloadFromNodes(filepath, assetdef.Class, assetdef.Id[0], assetdef.Ext[0], assetdef.Dir[0], assetdef.GameDir[0], name, meshname);
    }

    public static void DownloadFromNodes(string filepath, VarStorage.AssetCacheDef assetdef, int idIndex, int extIndex, int outputPathIndex, int inGameDirIndex, string name = "", string meshname = "")
    {
        DownloadFromNodes(filepath, assetdef.Class, assetdef.Id[idIndex], assetdef.Ext[extIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex], name, meshname);
    }

    public static void DownloadFromNodes(string filepath, string itemClassValue, string itemIdValue, string fileext, string outputPath, string inGameDir, string name = "", string meshname = "")
    {
        string oldfile = File.ReadAllText(filepath);
        string fixedfile = RemoveInvalidXmlChars(ReplaceHexadecimalSymbols(oldfile));
        XDocument doc = XDocument.Parse(fixedfile);

        try
        {
            var v = from nodes in doc.Descendants("Item")
                    where nodes.Attribute("class").Value == itemClassValue
                    select nodes;

            foreach (var item in v)
            {
                var v2 = from nodes in item.Descendants("Content")
                         where nodes.Attribute("name").Value == itemIdValue
                         select nodes;

                foreach (var item2 in v2)
                {
                    var v3 = from nodes in item2.Descendants("url")
                             select nodes;

                    foreach (var item3 in v3)
                    {
                        if (!item3.Value.Contains("rbxassetid"))
                        {
                            if (!item3.Value.Contains("rbxasset"))
                            {
                                if (string.IsNullOrWhiteSpace(meshname))
                                {
                                    string url = item3.Value;
                                    string urlFixed = url.Replace("&amp;", "&").Replace("amp;", "&");
                                    string peram = "id=";

                                    if (string.IsNullOrWhiteSpace(name))
                                    {
                                        if (urlFixed.Contains(peram))
                                        {
                                            string IDVal = urlFixed.After(peram);
                                            DownloadFilesFromNode(urlFixed, outputPath, fileext, IDVal);
                                            item3.Value = inGameDir + IDVal + fileext;
                                        }
                                    }
                                    else
                                    {
                                        DownloadFilesFromNode(urlFixed, outputPath, fileext, name);
                                        item3.Value = inGameDir + name + fileext;
                                    }
                                }
                                else
                                {
                                    item3.Value = inGameDir + meshname;
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(meshname))
                            {
                                string url = item3.Value;
                                string rbxassetid = "rbxassetid://";
                                string urlFixed = "https://www.roblox.com/asset/?id=" + url.After(rbxassetid);
                                string peram = "id=";

                                if (string.IsNullOrWhiteSpace(name))
                                {
                                    if (urlFixed.Contains(peram))
                                    {
                                        string IDVal = urlFixed.After(peram);
                                        DownloadFilesFromNode(urlFixed, outputPath, fileext, IDVal);
                                        item3.Value = inGameDir + IDVal + fileext;
                                    }
                                }
                                else
                                {
                                    DownloadFilesFromNode(urlFixed, outputPath, fileext, name);
                                    item3.Value = inGameDir + name + fileext;
                                }
                            }
                            else
                            {
                                item3.Value = inGameDir + meshname;
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            doc.Save(filepath);
        }
    }

    public static string GetStringForXMLType(XMLTypes type)
    {
        switch (type)
        {
            case XMLTypes.Bool:
                return "bool";
            case XMLTypes.Float:
                return "float";
            case XMLTypes.Token:
            default:
                return "token";
        }
    }

    public static void EditRenderSettings(XDocument doc, string setting, string value, XMLTypes type)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "RenderSettings"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(GetStringForXMLType(type))
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                item2.Value = value;
            }
        }
    }

    public static string GetRenderSettings(XDocument doc, string setting, XMLTypes type)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "RenderSettings"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(GetStringForXMLType(type))
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                return item2.Value;
            }
        }

        return "";
    }

    private static void DownloadFilesFromNode(string url, string path, string fileext, string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            Downloader download = new Downloader(url, id);

            try
            {
                download.InitDownload(path, fileext);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public static string RemoveInvalidXmlChars(string content)
    {
        return new string(content.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());
    }

    public static string ReplaceHexadecimalSymbols(string txt)
    {
        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F]";
        return Regex.Replace(txt, r, "", RegexOptions.Compiled);
    }
}
#endregion
