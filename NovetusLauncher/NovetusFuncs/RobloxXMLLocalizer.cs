using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

public static class RobloxXMLLocalizer
{
    public enum DLType
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

    public static void LoadRBXFile(DLType type, string stringToUpdate = "")
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select a ROBLOX level or model",
            Filter = "ROBLOX Level (*.rbxl)|*.rbxl|ROBLOX Model (*.rbxm)|*.rbxm",
            Title = "Open ROBLOX level or model"
        };

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            string path = openFileDialog1.FileName;

            switch (type)
            {
                case DLType.RBXL:
                    //backup the original copy
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(stringToUpdate))
                        {
                            stringToUpdate = "Backing up RBXL...";
                        }
                        File.Copy(path, path.Replace(".rbxl", " BAK.rbxl"));
                    }
                    catch(Exception ex) when (!Env.Debugging)
                    {
                        if (!string.IsNullOrWhiteSpace(stringToUpdate))
                        {
                            stringToUpdate = "Failed to back up RBXL. " + ex.Message;
                        }
                    }
                    //meshes
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Meshes and Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Fonts);
                    DownloadFromNodes(path, GlobalVars.Fonts, 1, 1, 1, 1);
                    //skybox
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Skybox Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Sky);
                    DownloadFromNodes(path, GlobalVars.Sky, 1, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 2, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 3, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 4, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 5, 0, 0, 0);
                    //decal
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Decal Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Decal);
                    //texture
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Texture);
                    //tools and hopperbin
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Tool Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Tool);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL HopperBin Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.HopperBin);
                    //sound
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Sounds...";
                    }
                    DownloadFromNodes(path, GlobalVars.Sound);
                    //gui
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL GUI Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ImageLabel);
                    //clothing
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Shirt Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Shirt);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL T-Shirt Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ShirtGraphic);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Pants Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Pants);
                    //scripts
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Linked Scripts...";
                    }
                    DownloadFromNodes(path, GlobalVars.Script);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXL Linked LocalScripts...";
                    }
                    DownloadFromNodes(path, GlobalVars.LocalScript);
                    goto default;
                case DLType.RBXM:
                    //meshes
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Meshes and Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Fonts);
                    DownloadFromNodes(path, GlobalVars.Fonts, 1, 1, 1, 1);
                    //skybox
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Skybox Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Sky);
                    DownloadFromNodes(path, GlobalVars.Sky, 1, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 2, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 3, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 4, 0, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 5, 0, 0, 0);
                    //decal
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Decal Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Decal);
                    //texture
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Texture);
                    //tools and hopperbin
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Tool Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Tool);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM HopperBin Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.HopperBin);
                    //sound
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Sounds...";
                    }
                    DownloadFromNodes(path, GlobalVars.Sound);
                    //gui
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM GUI Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ImageLabel);
                    //clothing
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Shirt Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Shirt);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM T-Shirt Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ShirtGraphic);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Pants Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.Pants);
                    //scripts
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Linked Scripts...";
                    }
                    DownloadFromNodes(path, GlobalVars.Script);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading RBXM Linked LocalScripts...";
                    }
                    DownloadFromNodes(path, GlobalVars.LocalScript);
                    goto default;
                case DLType.Hat:
                    //meshes
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Hat Meshes and Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemHatFonts);
                    DownloadFromNodes(path, GlobalVars.ItemHatFonts, 1, 1, 1, 1);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Hat Sounds...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemHatSound);
                    //scripts
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Hat Linked Scripts...";
                    }
                    DownloadFromNodes(path, GlobalVars.Script);
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Hat Linked LocalScripts...";
                    }
                    DownloadFromNodes(path, GlobalVars.LocalScript);
                    goto default;
                case DLType.Head:
                    //meshes
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Head Meshes and Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemHeadFonts);
                    DownloadFromNodes(path, GlobalVars.ItemHeadFonts, 1, 1, 1, 1);
                    goto default;
                case DLType.Face:
                    //decal
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Face Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemFaceTexture);
                    goto default;
                case DLType.TShirt:
                    //texture
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading T-Shirt Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemTShirtTexture);
                    goto default;
                case DLType.Shirt:
                    //texture
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Shirt Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemShirtTexture);
                    goto default;
                case DLType.Pants:
                    //texture
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Downloading Pants Textures...";
                    }
                    DownloadFromNodes(path, GlobalVars.ItemPantsTexture);
                    goto default;
                default:
                    if (!string.IsNullOrWhiteSpace(stringToUpdate))
                    {
                        stringToUpdate = "Doing Nothing";
                    }
                    break;
            }
        }
    }

    public static void DownloadFromNodes(string filepath, AssetCacheDef assetdef)
    {
        DownloadFromNodes(filepath, assetdef.Class, assetdef.Id[0], assetdef.Ext[0], assetdef.Dir[0], assetdef.GameDir[0]);
    }

    public static void DownloadFromNodes(string filepath, AssetCacheDef assetdef, int idIndex, int extIndex, int outputPathIndex, int inGameDirIndex)
    {
        DownloadFromNodes(filepath, assetdef.Class, assetdef.Id[idIndex], assetdef.Ext[extIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex]);
    }

    public static void DownloadFromNodes(string filepath, string itemClassValue, string itemIdValue, string fileext, string outputPath, string inGameDir)
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
                        if (!item3.Value.Contains("rbxasset"))
                        {
                            //do whatever with your value
                            string url = item3.Value;
                            DownloadFilesFromNode(url, outputPath, fileext);
                            if (url.Contains('='))
                            {
                                string[] substrings = url.Split('=');
                                item3.Value = inGameDir + substrings[1] + fileext;
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex) when (!Env.Debugging)
        {
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            doc.Save(filepath);
        }
    }

    private static void DownloadFilesFromNode(string url, string path, string fileext)
    {
        if (url.Contains('='))
        {
            string[] substrings = url.Split('=');

            if (!string.IsNullOrWhiteSpace(substrings[1]))
            {
                Downloader download = new Downloader(url, substrings[1]);

                try
                {
                    download.InitDownload(path, fileext);
                }
                catch (Exception ex) when (!Env.Debugging)
                {
                    MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    private static string RemoveInvalidXmlChars(string content)
    {
        return new string(content.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());
    }

    private static string ReplaceHexadecimalSymbols(string txt)
    {
        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
        return Regex.Replace(txt, r, "", RegexOptions.Compiled);
    }
}
