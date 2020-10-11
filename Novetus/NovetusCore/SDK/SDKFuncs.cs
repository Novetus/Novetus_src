#region Usings
using NLog.Filters;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
#endregion

#region SDKApps
enum SDKApps
{
    ClientSDK = 0,
    AssetSDK = 1,
    ClientScriptDoc = 2,
    SplashTester = 3,
    ScriptGenerator = 4,
    LegacyPlaceConverter = 5,
    DiogenesEditor = 6,
    ClientScriptTester = 7
}
#endregion

#region SDK Functions
class SDKFuncs
{
    #region Asset Localizer
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Fonts);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                    //skybox
                    worker.ReportProgress(10);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Decal);
                    //texture
                    worker.ReportProgress(20);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Texture);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Shirt);
                    worker.ReportProgress(65);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                    worker.ReportProgress(70);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Pants);
                    //scripts
                    worker.ReportProgress(80);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.LocalScript);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Fonts);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                    //skybox
                    worker.ReportProgress(10);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Decal);
                    //texture
                    worker.ReportProgress(20);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Texture);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Shirt);
                    worker.ReportProgress(65);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                    worker.ReportProgress(70);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Pants);
                    //scripts
                    worker.ReportProgress(80);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.LocalScript);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHatFonts, itemname, meshname);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHatFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(25);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHatSound);
                    //scripts
                    worker.ReportProgress(50);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHatScript);
                    worker.ReportProgress(75);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHatLocalScript);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, itemname);
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, 1, 1, 1, 1, itemname);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemFaceTexture, itemname);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemTShirtTexture, itemname);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemShirtTexture, itemname);
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
                    RobloxXML.DownloadFromNodes(path, RobloxDefs.ItemPantsTexture, itemname);
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
                return SDKApps.ClientScriptDoc;
            case 3:
                return SDKApps.SplashTester;
            case 4:
                return SDKApps.ScriptGenerator;
            case 5:
                return SDKApps.LegacyPlaceConverter;
            case 6:
                return SDKApps.DiogenesEditor;
            case 7:
                return SDKApps.ClientScriptTester;
            default:
                return SDKApps.ClientSDK;
        }
    }
    #endregion
}
#endregion
