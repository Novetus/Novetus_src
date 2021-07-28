#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
#endregion

#region SDKApps
enum SDKApps
{
    ClientSDK = 0,
    AssetSDK = 1,
    ItemCreationSDK = 2,
    ClientScriptDoc = 3,
    SplashTester = 4,
    ScriptGenerator = 5,
    LegacyPlaceConverter = 6,
    DiogenesEditor = 7,
    ClientScriptTester = 8
}
#endregion

#region SDK Functions
class SDKFuncs
{
    #region Asset Localizer
    private static void DownloadFilesFromNode(string url, string path, string fileext, string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            Downloader download = new Downloader(url, id);

            try
            {
                download.InitDownload(path, fileext, "", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

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
        string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile));
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
                                    string newurl = "assetdelivery.roblox.com/v1/asset/?id=";
                                    string urlFixed = url.Replace("http://", "https://")
                                        .Replace("?version=1&amp;id=", "?id=")
                                        .Replace("www.roblox.com/asset/?id=", newurl)
                                        .Replace("www.roblox.com/asset?id=", newurl)
                                        .Replace("assetgame.roblox.com/asset/?id=", newurl)
                                        .Replace("assetgame.roblox.com/asset?id=", newurl)
                                        .Replace("roblox.com/asset/?id=", newurl)
                                        .Replace("roblox.com/asset?id=", newurl)
                                        .Replace("&amp;", "&")
                                        .Replace("amp;", "&");
                                    string peram = "id=";

                                    if (string.IsNullOrWhiteSpace(name))
                                    {
                                        if (urlFixed.Contains(peram))
                                        {
                                            string IDVal = urlFixed.After(peram);
                                            DownloadFilesFromNode(urlFixed, outputPath, fileext, IDVal);
                                            item3.Value = (inGameDir + IDVal + fileext).Replace(" ", "");
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
                                string urlFixed = "https://assetdelivery.roblox.com/v1/asset/?id=" + url.After(rbxassetid);
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
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            doc.Save(filepath);
        }
    }

    //TODO: actually download the script assets instead of fixing the scripts lol. fixing the scripts won't work anyways because we don't support https natively.
    /*
    public static void DownloadScriptFromNodes(string filepath, string itemClassValue)
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
                var v2 = from nodes in item.Descendants("Properties")
                         select nodes;

                foreach (var item2 in v2)
                {
                    var v3 = from nodes in doc.Descendants("ProtectedString")
                             where nodes.Attribute("name").Value == "Source"
                             select nodes;

                    foreach (var item3 in v3)
                    {
                        string newurl = "assetdelivery.roblox.com/v1/asset/?id=";
                        item3.Value.Replace("http://", "https://")
                            .Replace("?version=1&id=", "?id=")
                            .Replace("www.roblox.com/asset/?id=", newurl)
                            .Replace("www.roblox.com/asset?id=", newurl)
                            .Replace("assetgame.roblox.com/asset/?id=", newurl)
                            .Replace("assetgame.roblox.com/asset?id=", newurl)
                            .Replace("roblox.com/asset/?id=", newurl)
                            .Replace("roblox.com/asset?id=", newurl);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            doc.Save(filepath);
        }
    }

    public static void DownloadFromScript(string filepath)
    {
        string[] file = File.ReadAllLines(filepath);

        try
        {
            foreach (var line in file)
            {
                if (line.Contains("www.roblox.com/asset/?id=") || line.Contains("assetgame.roblox.com/asset/?id="))
                {
                    string newurl = "assetdelivery.roblox.com/v1/asset/?id=";
                    line.Replace("http://", "https://")
                        .Replace("?version=1&id=", "?id=")
                            .Replace("www.roblox.com/asset/?id=", newurl)
                            .Replace("www.roblox.com/asset?id=", newurl)
                            .Replace("assetgame.roblox.com/asset/?id=", newurl)
                            .Replace("assetgame.roblox.com/asset?id=", newurl)
                            .Replace("roblox.com/asset/?id=", newurl)
                            .Replace("roblox.com/asset?id=", newurl);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            File.WriteAllLines(filepath, file);
        }
    }*/

    public static OpenFileDialog LoadROBLOXFileDialog(RobloxFileType type)
    {
        string typeFilter = "";

        switch (type)
        {
            case RobloxFileType.RBXL:
                typeFilter = "ROBLOX Level (*.rbxl)|*.rbxl|ROBLOX Level (*.rbxlx)|*.rbxlx";
                break;
            /*case RobloxFileType.Script:
                typeFilter = "Lua Script (*.lua)|*.lua";
                break;*/
            default:
                typeFilter = "ROBLOX Model (*.rbxm)|*.rbxm";
                break;
        }

        OpenFileDialog openFileDialog1 = new OpenFileDialog
        {
            Filter = typeFilter,
            Title = "Open ROBLOX level or model",
        };

        return openFileDialog1;
    }

    public static RobloxFileType SelectROBLOXFileType(int index)
    {
        RobloxFileType type;

        switch (index)
        {
            case 1:
                type = RobloxFileType.RBXM;
                break;
            case 2:
                type = RobloxFileType.Hat;
                break;
            case 3:
                type = RobloxFileType.Head;
                break;
            case 4:
                type = RobloxFileType.Face;
                break;
            case 5:
                type = RobloxFileType.Shirt;
                break;
            case 6:
                type = RobloxFileType.TShirt;
                break;
            case 7:
                type = RobloxFileType.Pants;
                break;
            //case 8:
            //type = RobloxFileType.Script;
            //break;
            default:
                type = RobloxFileType.RBXL;
                break;
        }

        return type;
    }

    public static string GetProgressString(RobloxFileType type, int percent)
    {
        string progressString = "";

        switch (type)
        {
            case RobloxFileType.RBXL:
                switch (percent)
                {
                    case 0:
                        progressString = "Backing up RBXL...";
                        break;
                    case 5:
                        progressString = "Downloading RBXL Meshes and Textures...";
                        break;
                    case 10:
                        progressString = "Downloading RBXL Skybox Textures...";
                        break;
                    case 15:
                        progressString = "Downloading RBXL Decal Textures...";
                        break;
                    case 20:
                        progressString = "Downloading RBXL Textures...";
                        break;
                    case 25:
                        progressString = "Downloading RBXL Tool Textures...";
                        break;
                    case 30:
                        progressString = "Downloading RBXL HopperBin Textures...";
                        break;
                    case 40:
                        progressString = "Downloading RBXL Sounds...";
                        break;
                    case 50:
                        progressString = "Downloading RBXL GUI Textures...";
                        break;
                    case 60:
                        progressString = "Downloading RBXL Shirt Textures...";
                        break;
                    case 65:
                        progressString = "Downloading RBXL T-Shirt Textures...";
                        break;
                    case 70:
                        progressString = "Downloading RBXL Pants Textures...";
                        break;
                    case 80:
                        progressString = "Downloading RBXL Linked Scripts...";
                        break;
                    case 90:
                        progressString = "Downloading RBXL Linked LocalScripts...";
                        break;
                        //case 95:
                        //progressString = "Fixing RBXL Scripts...";
                        //break;
                        //case 97:
                        //progressString = "Fixing RBXL LocalScripts...";
                        //break;
                }
                break;
            case RobloxFileType.RBXM:
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading RBXM Meshes and Textures...";
                        break;
                    case 10:
                        progressString = "Downloading RBXM Skybox Textures...";
                        break;
                    case 15:
                        progressString = "Downloading RBXM Decal Textures...";
                        break;
                    case 20:
                        progressString = "Downloading RBXM Textures...";
                        break;
                    case 25:
                        progressString = "Downloading RBXM Tool Textures...";
                        break;
                    case 30:
                        progressString = "Downloading RBXM HopperBin Textures...";
                        break;
                    case 40:
                        progressString = "Downloading RBXM Sounds...";
                        break;
                    case 50:
                        progressString = "Downloading RBXM GUI Textures...";
                        break;
                    case 60:
                        progressString = "Downloading RBXM Shirt Textures...";
                        break;
                    case 65:
                        progressString = "Downloading RBXM T-Shirt Textures...";
                        break;
                    case 70:
                        progressString = "Downloading RBXM Pants Textures...";
                        break;
                    case 80:
                        progressString = "Downloading RBXM Linked Scripts...";
                        break;
                    case 90:
                        progressString = "Downloading RBXM Linked LocalScripts...";
                        break;
                        //case 95:
                        //progressString = "Fixing RBXM Scripts...";
                        //break;
                        //case 97:
                        //progressString = "Fixing RBXM LocalScripts...";
                        //break;
                }
                break;
            case RobloxFileType.Hat:
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading Hat Meshes and Textures...";
                        break;
                    case 25:
                        progressString = "Downloading Hat Sounds...";
                        break;
                    case 50:
                        progressString = "Downloading Hat Linked Scripts...";
                        break;
                    case 75:
                        progressString = "Downloading Hat Linked LocalScripts...";
                        break;
                }
                break;
            case RobloxFileType.Head:
                //meshes
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading Head Meshes and Textures...";
                        break;
                }
                break;
            case RobloxFileType.Face:
                //decal
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading Face Textures...";
                        break;
                }
                break;
            case RobloxFileType.TShirt:
                //texture
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading T-Shirt Textures...";
                        break;
                }
                break;
            case RobloxFileType.Shirt:
                //texture
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading Shirt Textures...";
                        break;
                }
                break;
            case RobloxFileType.Pants:
                //texture
                switch (percent)
                {
                    case 0:
                        progressString = "Downloading Pants Textures...";
                        break;
                }
                break;
            /*
        case RobloxFileType.Script:
            //script
            switch (percent)
            {
                case 0:
                    progressString = "Fixing Script...";
                    break;
            }
            break;*/
            default:
                progressString = "Idle";
                break;
        }

        return progressString + " " + percent.ToString() + "%";
    }

    public static void LocalizeAsset(RobloxFileType type, BackgroundWorker worker, string path, string itemname, string meshname)
    {
        try
        {
            switch (type)
            {
                case RobloxFileType.RBXL:
                    //backup the original copy
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxl", " BAK.rbxl"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    worker.ReportProgress(5);
                    DownloadFromNodes(path, RobloxDefs.Fonts);
                    DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                    //skybox
                    worker.ReportProgress(10);
                    DownloadFromNodes(path, RobloxDefs.Sky);
                    DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    DownloadFromNodes(path, RobloxDefs.Decal);
                    //texture
                    worker.ReportProgress(20);
                    DownloadFromNodes(path, RobloxDefs.Texture);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    DownloadFromNodes(path, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    DownloadFromNodes(path, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    DownloadFromNodes(path, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    DownloadFromNodes(path, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    DownloadFromNodes(path, RobloxDefs.Shirt);
                    worker.ReportProgress(65);
                    DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                    worker.ReportProgress(70);
                    DownloadFromNodes(path, RobloxDefs.Pants);
                    //scripts
                    worker.ReportProgress(80);
                    DownloadFromNodes(path, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    DownloadFromNodes(path, RobloxDefs.LocalScript);
                    //localize any scripts that are not handled
                    /*
                    worker.ReportProgress(95);
                    RobloxXML.DownloadScriptFromNodes(path, "Script");
                    worker.ReportProgress(97);
                    RobloxXML.DownloadScriptFromNodes(path, "LocalScript");*/
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.RBXM:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    DownloadFromNodes(path, RobloxDefs.Fonts);
                    DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                    //skybox
                    worker.ReportProgress(10);
                    DownloadFromNodes(path, RobloxDefs.Sky);
                    DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                    DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    DownloadFromNodes(path, RobloxDefs.Decal);
                    //texture
                    worker.ReportProgress(20);
                    DownloadFromNodes(path, RobloxDefs.Texture);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    DownloadFromNodes(path, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    DownloadFromNodes(path, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    DownloadFromNodes(path, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    DownloadFromNodes(path, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    DownloadFromNodes(path, RobloxDefs.Shirt);
                    worker.ReportProgress(65);
                    DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                    worker.ReportProgress(70);
                    DownloadFromNodes(path, RobloxDefs.Pants);
                    //scripts
                    worker.ReportProgress(80);
                    DownloadFromNodes(path, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    DownloadFromNodes(path, RobloxDefs.LocalScript);
                    //localize any scripts that are not handled
                    /*
                    worker.ReportProgress(95);
                    RobloxXML.DownloadScriptFromNodes(path, "Script");
                    worker.ReportProgress(97);
                    RobloxXML.DownloadScriptFromNodes(path, "LocalScript");*/
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Hat:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    DownloadFromNodes(path, RobloxDefs.ItemHatFonts, itemname, meshname);
                    DownloadFromNodes(path, RobloxDefs.ItemHatFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(25);
                    DownloadFromNodes(path, RobloxDefs.ItemHatSound);
                    //scripts
                    worker.ReportProgress(50);
                    DownloadFromNodes(path, RobloxDefs.ItemHatScript);
                    worker.ReportProgress(75);
                    DownloadFromNodes(path, RobloxDefs.ItemHatLocalScript);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Head:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, itemname);
                    DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Face:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //decal
                    DownloadFromNodes(path, RobloxDefs.ItemFaceTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.TShirt:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //texture
                    DownloadFromNodes(path, RobloxDefs.ItemTShirtTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Shirt:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //texture
                    DownloadFromNodes(path, RobloxDefs.ItemShirtTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Pants:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //texture
                    DownloadFromNodes(path, RobloxDefs.ItemPantsTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                /*case RobloxFileType.Script:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            File.Copy(path, path.Replace(".lua", " BAK.lua"));
                        }
                        catch (Exception)
                        {
                            worker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }

                    RobloxXML.DownloadFromScript(path);
                    worker.ReportProgress(100);
                    break;*/
                default:
                    worker.ReportProgress(100);
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: Unable to localize the asset. " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion

    #region Item Creation SDK
    public static void SetItemFontVals(XDocument doc, VarStorage.AssetCacheDef assetdef, int idIndex, int outputPathIndex, int inGameDirIndex, string assetpath, string assetfilename)
    {
        SetItemFontVals(doc, assetdef.Class, assetdef.Id[idIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex], assetpath, assetfilename);
    }

    public static void SetItemFontVals(XDocument doc, string itemClassValue, string itemIdValue, string outputPath, string inGameDir, string assetpath, string assetfilename)
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
                    if (!string.IsNullOrWhiteSpace(assetpath))
                    {
                        GlobalFuncs.FixedFileCopy(assetpath, outputPath + "\\" + assetfilename, true);
                    }
                    item3.Value = inGameDir + assetfilename;
                }
            }
        }
    }

    public static void SetItemCoordVals(XDocument doc, VarStorage.AssetCacheDef assetdef, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        SetItemCoordVals(doc, assetdef.Class, X, Y, Z, CoordClass, CoordName);
    }

    public static void SetItemCoordVals(XDocument doc, string itemClassValue, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        SetItemCoordXML(v, X, Y, Z, CoordClass, CoordName);
    }

    public static void SetItemCoordValsNoClassSearch(XDocument doc, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        SetItemCoordXML(v, X, Y, Z, CoordClass, CoordName);
    }

    private static void SetItemCoordXML(IEnumerable<XElement> v, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(CoordClass)
                     where nodes.Attribute("name").Value == CoordName
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("X")
                         select nodes;

                foreach (var item3 in v3)
                {
                    item3.Value = X.ToString();
                }

                var v4 = from nodes in item2.Descendants("Y")
                         select nodes;

                foreach (var item4 in v4)
                {
                    item4.Value = Y.ToString();
                }

                var v5 = from nodes in item2.Descendants("Z")
                         select nodes;

                foreach (var item5 in v5)
                {
                    item5.Value = Z.ToString();
                }
            }
        }
    }

    public static void SetHeadBevel(XDocument doc, double bevel, double bevelRoundness, double bulge)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(RobloxXML.GetStringForXMLType(XMLTypes.Float))
                     where nodes.Attribute("name").Value == "Bevel"
                     select nodes;

            foreach (var item2 in v2)
            {
                item2.Value = bevel.ToString();
            }

            var v3 = from nodes in item.Descendants(RobloxXML.GetStringForXMLType(XMLTypes.Float))
                     where nodes.Attribute("name").Value == "Bevel Roundness"
                     select nodes;

            foreach (var item3 in v3)
            {
                item3.Value = bevelRoundness.ToString();
            }

            var v4 = from nodes in item.Descendants(RobloxXML.GetStringForXMLType(XMLTypes.Float))
                     where nodes.Attribute("name").Value == "Bulge"
                     select nodes;

            foreach (var item4 in v4)
            {
                item4.Value = bulge.ToString();
            }
        }
    }

    public static string GetPathForType(RobloxFileType type)
    {
        switch (type)
        {
            case RobloxFileType.Hat:
                return GlobalPaths.hatdir;
            case RobloxFileType.HeadNoCustomMesh:
            case RobloxFileType.Head:
                return GlobalPaths.headdir;
            case RobloxFileType.Face:
                return GlobalPaths.facedir;
            case RobloxFileType.TShirt:
                return GlobalPaths.tshirtdir;
            case RobloxFileType.Shirt:
                return GlobalPaths.shirtdir;
            case RobloxFileType.Pants:
                return GlobalPaths.pantsdir;
            default:
                return "";
        }
    }

    public static RobloxFileType GetTypeForInt(int type)
    {
        switch (type)
        {
            case 0:
                return RobloxFileType.Hat;
            case 1:
                return RobloxFileType.Head;
            case 2:
                return RobloxFileType.HeadNoCustomMesh;
            case 3:
                return RobloxFileType.Face;
            case 4:
                return RobloxFileType.TShirt;
            case 5:
                return RobloxFileType.Shirt;
            case 6:
                return RobloxFileType.Pants;
            default:
                return RobloxFileType.RBXM;
        }
    }

    public static bool CreateItem(string filepath, RobloxFileType type, string itemname, string[] assetfilenames, double[] coordoptions, double[] headoptions, string desctext = "")
    {
        /*MessageBox.Show(assetfilenames[0] + "\n" + 
            assetfilenames[1] + "\n" +
            assetfilenames[2] + "\n" +
            assetfilenames[3] + "\n" +
            coordoptions[0] + "\n" +
            coordoptions[1] + "\n" +
            coordoptions[2] + "\n" +
            headoptions[0] + "\n" +
            headoptions[1] + "\n" +
            headoptions[2] + "\n");*/

        string oldfile = File.ReadAllText(filepath);
        string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile));
        XDocument doc = XDocument.Parse(fixedfile);
        string savDocPath = GetPathForType(type);
        bool success = true;

        try
        {
            switch (type)
            {
                case RobloxFileType.Hat:
                    SetItemFontVals(doc, RobloxDefs.ItemHatFonts, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    SetItemFontVals(doc, RobloxDefs.ItemHatFonts, 1, 1, 1, assetfilenames[1], assetfilenames[3]);
                    SetItemCoordVals(doc, "Hat", coordoptions[0], coordoptions[1], coordoptions[2], "CoordinateFrame", "AttachmentPoint");
                    break;
                case RobloxFileType.Head:
                    SetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    SetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 1, 1, 1, assetfilenames[1], assetfilenames[3]);
                    SetItemCoordVals(doc, RobloxDefs.ItemHeadFonts, coordoptions[0], coordoptions[1], coordoptions[2], "Vector3", "Scale");
                    break;
                case RobloxFileType.Face:
                    SetItemFontVals(doc, RobloxDefs.ItemFaceTexture, 0, 0, 0, "", assetfilenames[2]);
                    break;
                case RobloxFileType.TShirt:
                    SetItemFontVals(doc, RobloxDefs.ItemTShirtTexture, 0, 0, 0, "", assetfilenames[2]);
                    break;
                case RobloxFileType.Shirt:
                    SetItemFontVals(doc, RobloxDefs.ItemShirtTexture, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    savDocPath = GlobalPaths.shirtdir;
                    break;
                case RobloxFileType.Pants:
                    SetItemFontVals(doc, RobloxDefs.ItemPantsTexture, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    break;
                case RobloxFileType.HeadNoCustomMesh:
                    SetHeadBevel(doc, headoptions[0], headoptions[1], headoptions[2]);
                    SetItemCoordValsNoClassSearch(doc, coordoptions[0], coordoptions[1], coordoptions[2], "Vector3", "Scale");
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("The Item Creation SDK has experienced an error: " + ex.Message, "Novetus Item Creation SDK", MessageBoxButtons.OK, MessageBoxIcon.Error);
            success = false;
        }
        finally
        {
            doc.Save(savDocPath + "\\" + itemname + ".rbxm");
            if (!string.IsNullOrWhiteSpace(desctext))
            {
                File.WriteAllText(savDocPath + "\\" + itemname + "_desc.txt", desctext);
            }
        }

        return success;
    }
    #endregion

    #region Item SDK

    public static void StartItemDownload(string name, string url, string id, int ver, bool iswebsite)
    {
        try
        {
            string version = ((ver != 0) && (!iswebsite)) ? "&version=" + ver : "";
            string fullURL = url + id + version;

            if (!iswebsite)
            {
                if (!GlobalVars.UserConfiguration.DisabledItemMakerHelp)
                {
                    string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                    MessageBox.Show(helptext, "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Downloader download = new Downloader(fullURL, name, "Roblox Model (*.rbxm)|*.rbxm|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav");

                try
                {
                    string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.";
                    download.InitDownload((!GlobalVars.UserConfiguration.DisabledItemMakerHelp) ? helptext : "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Unable to download the file. " + ex.Message, "Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
                {
                    MessageBox.Show(download.getDownloadOutcome(), "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Process.Start(fullURL);

                if (!GlobalVars.UserConfiguration.DisabledItemMakerHelp)
                {
                    string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.\n\nIf you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                    MessageBox.Show(helptext, "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        catch (Exception)
        {
            MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.", "Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void StartItemBatchDownload(string name, string url, string id, int ver, bool iswebsite, string path)
    {
        try
        {
            string version = ((ver != 0) && (!iswebsite)) ? "&version=" + ver : "";
            string fullURL = url + id + version;

            if (!iswebsite)
            {
                Downloader download = new Downloader(fullURL, name, "", path);

                try
                {
                    download.InitDownloadNoDialog(download.GetFullDLPath());
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Process.Start(fullURL);
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region SDK Launcher
    public static SDKApps GetSDKAppForIndex(int index)
    {
        switch (index)
        {
            case 1:
                return SDKApps.AssetSDK;
            case 2:
                return SDKApps.ItemCreationSDK;
            case 3:
                return SDKApps.ClientScriptDoc;
            case 4:
                return SDKApps.SplashTester;
            case 5:
                return SDKApps.ScriptGenerator;
            case 6:
                return SDKApps.LegacyPlaceConverter;
            case 7:
                return SDKApps.DiogenesEditor;
            case 8:
                return SDKApps.ClientScriptTester;
            default:
                return SDKApps.ClientSDK;
        }
    }
    #endregion
}
#endregion
