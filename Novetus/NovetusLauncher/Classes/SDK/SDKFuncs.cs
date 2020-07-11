#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region SDKApps
    enum SDKApps
    {
        ItemSDK = 0,
        ClientSDK = 1,
        ClientScriptDoc = 2,
        AssetLocalizer = 3,
        SplashTester = 4,
        Obj2MeshV1GUI = 5,
        ScriptGenerator = 6,
        LegacyPlaceConverter = 7,
        DiogenesEditor = 8
    }
    #endregion

    #region SDK Functions
    class SDKFuncs
    {
        #region Asset Localizer
        public static OpenFileDialog LoadROBLOXFileDialog(RobloxFileType type)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = (type == RobloxFileType.RBXL) ? "ROBLOX Level (*.rbxl)|*.rbxl" : "ROBLOX Model (*.rbxm)|*.rbxm",
                Title = "Open ROBLOX level or model"
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
                    }
                    break;
                case RobloxFileType.RBXM:
                    switch (percent)
                    {
                        case 0:
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                        //skybox
                        worker.ReportProgress(10);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                        //decal
                        worker.ReportProgress(15);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Decal);
                        //texture
                        worker.ReportProgress(20);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Texture);
                        //tools and hopperbin
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Tool);
                        worker.ReportProgress(30);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.HopperBin);
                        //sound
                        worker.ReportProgress(40);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sound);
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ImageLabel);
                        //clothing
                        worker.ReportProgress(60);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Shirt);
                        worker.ReportProgress(65);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                        worker.ReportProgress(70);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Pants);
                        //scripts
                        worker.ReportProgress(80);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Script);
                        worker.ReportProgress(90);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.LocalScript);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                        //skybox
                        worker.ReportProgress(10);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                        //decal
                        worker.ReportProgress(15);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Decal);
                        //texture
                        worker.ReportProgress(20);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Texture);
                        //tools and hopperbin
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Tool);
                        worker.ReportProgress(30);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.HopperBin);
                        //sound
                        worker.ReportProgress(40);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sound);
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ImageLabel);
                        //clothing
                        worker.ReportProgress(60);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Shirt);
                        worker.ReportProgress(65);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                        worker.ReportProgress(70);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Pants);
                        //scripts
                        worker.ReportProgress(80);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Script);
                        worker.ReportProgress(90);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.LocalScript);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatFonts, itemname, meshname);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatFonts, 1, 1, 1, 1, itemname);
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatSound);
                        //scripts
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatScript);
                        worker.ReportProgress(75);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatLocalScript);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, itemname);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, 1, 1, 1, 1, itemname);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemFaceTexture, itemname);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemTShirtTexture, itemname);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemShirtTexture, itemname);
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
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemPantsTexture, itemname);
                        worker.ReportProgress(100);
                        break;
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

        #region Diogenes Editor
        // credit to Carrot for this :D

        public static string DiogenesCrypt(string word)
        {
            StringBuilder result = new StringBuilder("");
            byte[] bytes = Encoding.ASCII.GetBytes(word);

            foreach (byte singular in bytes)
            {
                result.Append(Convert.ToChar(0x55 ^ singular));
            }

            return result.ToString();
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
                    System.Diagnostics.Process.Start(fullURL);

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
        #endregion

        #region SDK Launcher
        public static void LaunchSDKAppByIndex(int index)
        {
            SDKApps selectedApp = GetSDKAppForIndex(index);

            switch (selectedApp)
            {
                case SDKApps.ClientSDK:
                    ClientinfoEditor cie = new ClientinfoEditor();
                    cie.Show();
                    break;
                case SDKApps.ClientScriptDoc:
                    ClientScriptDocumentation csd = new ClientScriptDocumentation();
                    csd.Show();
                    break;
                case SDKApps.AssetLocalizer:
                    AssetLocalizer al = new AssetLocalizer();
                    al.Show();
                    break;
                case SDKApps.SplashTester:
                    SplashTester st = new SplashTester();
                    st.Show();
                    break;
                case SDKApps.Obj2MeshV1GUI:
                    Obj2MeshV1GUI obj = new Obj2MeshV1GUI();
                    obj.Show();
                    break;
                case SDKApps.ScriptGenerator:
                    Process proc = new Process();
                    proc.StartInfo.FileName = GlobalPaths.ConfigDirData + "\\RSG.exe";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    break;
                case SDKApps.LegacyPlaceConverter:
                    Process proc2 = new Process();
                    proc2.StartInfo.FileName = GlobalPaths.ConfigDirData + "\\Roblox_Legacy_Place_Converter.exe";
                    proc2.StartInfo.CreateNoWindow = false;
                    proc2.StartInfo.UseShellExecute = false;
                    proc2.Start();
                    break;
                case SDKApps.DiogenesEditor:
                    DiogenesEditor dio = new DiogenesEditor();
                    dio.Show();
                    break;
                default:
                    ItemMaker im = new ItemMaker();
                    im.Show();
                    break;
            }
        }

        public static SDKApps GetSDKAppForIndex(int index)
        {
            switch (index)
            {
                case 1:
                    return SDKApps.ClientSDK;
                case 2:
                    return SDKApps.ClientScriptDoc;
                case 3:
                    return SDKApps.AssetLocalizer;
                case 4:
                    return SDKApps.SplashTester;
                case 5:
                    return SDKApps.Obj2MeshV1GUI;
                case 6:
                    return SDKApps.ScriptGenerator;
                case 7:
                    return SDKApps.LegacyPlaceConverter;
                case 8:
                    return SDKApps.DiogenesEditor;
                default:
                    return SDKApps.ItemSDK;
            }
        }
        #endregion
    }
    #endregion
}
