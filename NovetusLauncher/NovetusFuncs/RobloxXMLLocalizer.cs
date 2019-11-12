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
        XML,
        //Items
        Hat,
        Head,
        Face,
        TShirt,
        Shirt,
        Pants
    }

    public static void LoadRBXFile(string path, DLType type)
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select a ROBLOX level or model",
            Filter = "ROBLOX Level (*.rbxl)|*.rbxl|ROBLOX Model (*.rbxm)|*.rbxm",
            Title = "Open ROBLOX level or model"
        };

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            switch (type)
            {
                case DLType.XML:
                    //meshes
                    DownloadFromNodes(path, GlobalVars.Fonts);
                    DownloadFromNodes(path, GlobalVars.Fonts, 1, 1, 1, 1);
                    //skybox
                    DownloadFromNodes(path, GlobalVars.Sky);
                    DownloadFromNodes(path, GlobalVars.Sky, 0, 1, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 0, 2, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 0, 3, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 0, 4, 0, 0);
                    DownloadFromNodes(path, GlobalVars.Sky, 0, 5, 0, 0);
                    //decal
                    DownloadFromNodes(path, GlobalVars.Decal);
                    //texture
                    DownloadFromNodes(path, GlobalVars.Texture);
                    //tools and hopperbin
                    DownloadFromNodes(path, GlobalVars.Tool);
                    DownloadFromNodes(path, GlobalVars.HopperBin);
                    //sound
                    DownloadFromNodes(path, GlobalVars.Sound);
                    //gui
                    DownloadFromNodes(path, GlobalVars.ImageLabel);
                    //clothing
                    DownloadFromNodes(path, GlobalVars.Shirt);
                    DownloadFromNodes(path, GlobalVars.ShirtGraphic);
                    DownloadFromNodes(path, GlobalVars.Pants);
                    break;
                case DLType.Hat:
                    //meshes
                    DownloadFromNodes(path, GlobalVars.ItemHatFonts);
                    DownloadFromNodes(path, GlobalVars.ItemHatFonts, 1, 1, 1, 1);
                    DownloadFromNodes(path, GlobalVars.ItemHatSound);
                    break;
                case DLType.Head:
                    //meshes
                    DownloadFromNodes(path, GlobalVars.ItemHeadFonts);
                    DownloadFromNodes(path, GlobalVars.ItemHeadFonts, 1, 1, 1, 1);
                    break;
                case DLType.Face:
                    //decal
                    DownloadFromNodes(path, GlobalVars.ItemFaceTexture);
                    break;
                case DLType.TShirt:
                    //texture
                    DownloadFromNodes(path, GlobalVars.ItemTShirtTexture);
                    break;
                case DLType.Shirt:
                    //texture
                    DownloadFromNodes(path, GlobalVars.ItemShirtTexture);
                    break;
                case DLType.Pants:
                    //texture
                    DownloadFromNodes(path, GlobalVars.ItemPantsTexture);
                    break;
                default:
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
                            string[] substrings = url.Split('=');
                            item3.Value = inGameDir + substrings[1] + fileext;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                catch (Exception ex)
                {
                    MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
