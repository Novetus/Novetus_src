#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
#endregion

public partial class AssetSDK : Form
{
    #region Private Variables
    //localizer
    private RobloxFileType currentType;
    private string path;
    private string name;
    private string meshname;
    //downloader
    private string url = "https://assetdelivery.roblox.com/v1/asset/?id=";
    private bool isWebSite = false;
    private bool batchMode = false;
    private bool hasOverrideWarningOpenedOnce = false;
    //obj2mesh
    private OpenFileDialog MeshConverter_OpenOBJDialog;
    #endregion

    #region Constructor
    public AssetSDK()
    {
        InitializeComponent();

        //meshconverter
        MeshConverter_OpenOBJDialog = new OpenFileDialog()
        {
            FileName = "Select a .OBJ file",
            Filter = "Wavefront .obj file (*.obj)|*.obj",
            Title = "Open model .obj"
        };
    }
    #endregion

    #region Form Events

    #region Load/Close Events
    private void AssetSDK_Load(object sender, EventArgs e)
    {
        //asset downloader
        AssetDownloader_URLSelection.SelectedItem = "https://assetdelivery.roblox.com/";
        isWebSite = false;

        AssetDownloader_LoadHelpMessage.Checked = GlobalVars.UserConfiguration.DisabledItemMakerHelp;

        //asset localizer
        AssetLocalization_SaveBackups.Checked = GlobalVars.UserConfiguration.AssetLocalizerSaveBackups;
        AssetLocalization_AssetTypeBox.SelectedItem = "RBXL";
        AssetLocalization_UsesHatMeshBox.SelectedItem = "None";

        if (Directory.Exists(GlobalPaths.hatdirFonts))
        {
            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.hatdirFonts);
            FileInfo[] Files = dinfo.GetFiles("*.mesh");
            foreach (FileInfo file in Files)
            {
                if (file.Name.Equals(string.Empty))
                {
                    continue;
                }

                AssetLocalization_UsesHatMeshBox.Items.Add(file.Name);
            }
        }

        SetAssetCachePaths();

        GlobalFuncs.CreateAssetCacheDirectories();
    }

    void AssetSDK_Close(object sender, CancelEventArgs e)
    {
        SetAssetCachePaths();

        //asset localizer
        AssetLocalization_BackgroundWorker.CancelAsync();
    }
    #endregion

    #region Asset Downloader
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
                    string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.rbxl - ROBLOX Place\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                    MessageBox.Show(helptext, "Novetus Asset SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Downloader download = new Downloader(fullURL, name, "Roblox Model (*.rbxm)|*.rbxm|Roblox Place (*.rbxl) |*.rbxl|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav");

                try
                {
                    string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, use the Asset Localizer in the Asset SDK.\n\nIf you get a corrupted file, change the URL using the drop down box.";
                    download.InitDownload((!GlobalVars.UserConfiguration.DisabledItemMakerHelp) ? helptext : "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Unable to download the file. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
                {
                    MessageBox.Show(download.getDownloadOutcome(), "Novetus Asset SDK - Download Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Process.Start(fullURL);
            }
        }
        catch (Exception)
        {
            MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.", "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    private void AssetDownloader_URLSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (AssetDownloader_URLSelection.SelectedIndex)
        {
            case 1:
                url = "https://www.roblox.com/catalog/";
                isWebSite = true;
                break;
            case 2:
                url = "https://www.roblox.com/library/";
                isWebSite = true;
                break;
            default:
                //use defaults
                url = "https://assetdelivery.roblox.com/v1/asset/?id=";
                isWebSite = false;
                break;
        }
    }

    private void AssetDownloader_AssetDownloaderButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(URLOverrideBox.Text))
        {
            url = URLOverrideBox.Text;
        }

        if (batchMode == false)
        {
            StartItemDownload(
                AssetDownloader_AssetNameBox.Text,
                url,
                AssetDownloader_AssetIDBox.Text,
                Convert.ToInt32(AssetDownloader_AssetVersionSelector.Value),
                isWebSite);
        }
        else
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                FileName = "Choose batch download location.",
                //"Compressed zip files (*.zip)|*.zip|All files (*.*)|*.*"
                Filter = "Roblox Model (*.rbxm)|*.rbxm|Roblox Place (*.rbxl) |*.rbxl|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav",
                DefaultExt = ".rbxm",
                Title = "Save files downloaded via batch"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string basepath = Path.GetDirectoryName(saveFileDialog1.FileName);
                string extension = Path.GetExtension(saveFileDialog1.FileName);

                AssetDownloaderBatch_Status.Visible = true;

                string[] lines = AssetDownloaderBatch_BatchIDBox.Lines;

                foreach (var line in lines)
                {
                    string[] linesplit = line.Split('|');
                    StartItemBatchDownload(
                        linesplit[0] + extension,
                        url,
                        linesplit[1],
                        Convert.ToInt32(linesplit[2]),
                        isWebSite, basepath);
                }

                AssetDownloaderBatch_Status.Visible = false;

                MessageBox.Show("Batch download complete! " + lines.Count() + " items downloaded!", "Novetus Asset SDK - Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private void AssetDownloader_LoadHelpMessage_CheckedChanged(object sender, EventArgs e)
    {
        GlobalVars.UserConfiguration.DisabledItemMakerHelp = AssetDownloader_LoadHelpMessage.Checked;
    }
    private void AssetDownloader_BatchMode_CheckedChanged(object sender, EventArgs e)
    {
        batchMode = AssetDownloader_BatchMode.Checked;

        if (batchMode)
        {
            AssetDownloaderBatch_BatchIDBox.Enabled = true;
            AssetDownloaderBatch_Note.Visible = true;
            AssetDownloader_AssetIDBox.Enabled = false;
            AssetDownloader_AssetNameBox.Enabled = false;
            AssetDownloader_AssetVersionSelector.Enabled = false;
        }
        else
        {
            AssetDownloaderBatch_BatchIDBox.Enabled = false;
            AssetDownloaderBatch_Note.Visible = false;
            AssetDownloader_AssetIDBox.Enabled = true;
            AssetDownloader_AssetNameBox.Enabled = true;
            AssetDownloader_AssetVersionSelector.Enabled = true;
        }
    }

    private void URLOverrideBox_Click(object sender, EventArgs e)
    {
        if (hasOverrideWarningOpenedOnce == false)
        {
            MessageBox.Show("By using the custom URL setting, you will override any selected entry in the default URL list. Keep this in mind before downloading anything with this option.\n\nAlso, the URL must be a asset url with 'asset/?id=' at the end of it in order for the Asset Downloader to work smoothly.", "Novetus Asset SDK - URL Override Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            hasOverrideWarningOpenedOnce = true;
        }
    }

    private void URLOverrideBox_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(URLOverrideBox.Text))
        {
            AssetDownloader_URLSelection.Enabled = false;
        }
        else
        {
            AssetDownloader_URLSelection.Enabled = true;
        }
    }
    #endregion

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
                MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static void DownloadFromNodes(XDocument doc, VarStorage.AssetCacheDef assetdef, string name = "", string meshname = "")
    {
        DownloadFromNodes(doc, assetdef.Class, assetdef.Id[0], assetdef.Ext[0], assetdef.Dir[0], assetdef.GameDir[0], name, meshname);
    }

    public static void DownloadFromNodes(XDocument doc, VarStorage.AssetCacheDef assetdef, int idIndex, int extIndex, int outputPathIndex, int inGameDirIndex, string name = "", string meshname = "")
    {
        DownloadFromNodes(doc, assetdef.Class, assetdef.Id[idIndex], assetdef.Ext[extIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex], name, meshname);
    }

    public static void DownloadFromNodes(XDocument doc, string itemClassValue, string itemIdValue, string fileext, string outputPath, string inGameDir, string name = "", string meshname = "")
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
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        string oldfile = File.ReadAllText(path);
        string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile)).Replace("&#9;", "\t").Replace("#9;", "\t");
        XDocument doc = null;
        XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };
        Stream filestream = GlobalFuncs.GenerateStreamFromString(fixedfile);
        using (XmlReader xmlReader = XmlReader.Create(filestream, xmlReaderSettings))
        {
            xmlReader.MoveToContent();
            doc = XDocument.Load(xmlReader);
        }

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
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxl", " - BAK.rbxl"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.Fonts);
                    DownloadFromNodes(doc, RobloxDefs.Fonts, 1, 1, 1, 1);
                    //skybox
                    worker.ReportProgress(10);
                    DownloadFromNodes(doc, RobloxDefs.Sky);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 1, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 2, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 3, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 4, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    DownloadFromNodes(doc, RobloxDefs.Decal);
                    //texture
                    worker.ReportProgress(20);
                    DownloadFromNodes(doc, RobloxDefs.Texture);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    DownloadFromNodes(doc, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    DownloadFromNodes(doc, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    DownloadFromNodes(doc, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    DownloadFromNodes(doc, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    DownloadFromNodes(doc, RobloxDefs.Shirt);
                    worker.ReportProgress(65);
                    DownloadFromNodes(doc, RobloxDefs.ShirtGraphic);
                    worker.ReportProgress(70);
                    DownloadFromNodes(doc, RobloxDefs.Pants);
                    //scripts
                    worker.ReportProgress(80);
                    DownloadFromNodes(doc, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    DownloadFromNodes(doc, RobloxDefs.LocalScript);
                    //localize any scripts that are not handled
                    /*
                    worker.ReportProgress(95);
                    RobloxXML.DownloadScriptFromNodes(doc, "Script");
                    worker.ReportProgress(97);
                    RobloxXML.DownloadScriptFromNodes(doc, "LocalScript");*/
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.RBXM:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.Fonts);
                    DownloadFromNodes(doc, RobloxDefs.Fonts, 1, 1, 1, 1);
                    //skybox
                    worker.ReportProgress(10);
                    DownloadFromNodes(doc, RobloxDefs.Sky);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 1, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 2, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 3, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 4, 0, 0, 0);
                    DownloadFromNodes(doc, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    DownloadFromNodes(doc, RobloxDefs.Decal);
                    //texture
                    worker.ReportProgress(20);
                    DownloadFromNodes(doc, RobloxDefs.Texture);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    DownloadFromNodes(doc, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    DownloadFromNodes(doc, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    DownloadFromNodes(doc, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    DownloadFromNodes(doc, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    DownloadFromNodes(doc, RobloxDefs.Shirt);
                    worker.ReportProgress(65);
                    DownloadFromNodes(doc, RobloxDefs.ShirtGraphic);
                    worker.ReportProgress(70);
                    DownloadFromNodes(doc, RobloxDefs.Pants);
                    //scripts
                    worker.ReportProgress(80);
                    DownloadFromNodes(doc, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    DownloadFromNodes(doc, RobloxDefs.LocalScript);
                    //localize any scripts that are not handled
                    /*
                    worker.ReportProgress(95);
                    RobloxXML.DownloadScriptFromNodes(doc, "Script");
                    worker.ReportProgress(97);
                    RobloxXML.DownloadScriptFromNodes(doc, "LocalScript");*/
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Hat:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.ItemHatFonts, itemname, meshname);
                    DownloadFromNodes(doc, RobloxDefs.ItemHatFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(25);
                    DownloadFromNodes(doc, RobloxDefs.ItemHatSound);
                    //scripts
                    worker.ReportProgress(50);
                    DownloadFromNodes(doc, RobloxDefs.ItemHatScript);
                    worker.ReportProgress(75);
                    DownloadFromNodes(doc, RobloxDefs.ItemHatLocalScript);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Head:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.ItemHeadFonts, itemname);
                    DownloadFromNodes(doc, RobloxDefs.ItemHeadFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Face:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.ItemFaceTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.TShirt:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.ItemTShirtTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Shirt:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.ItemShirtTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                case RobloxFileType.Pants:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " BAK.rbxm"), false);
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
                    DownloadFromNodes(doc, RobloxDefs.ItemPantsTexture, itemname);
                    worker.ReportProgress(100);
                    break;
                /*case RobloxFileType.Script:
                    if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".lua", " BAK.lua"), false);
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
            MessageBox.Show("Error: Unable to localize the asset. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { CheckCharacters = false, Indent = true };
            using (XmlWriter xmlReader = XmlWriter.Create(path, xmlWriterSettings))
            {
                doc.WriteTo(xmlReader);
            }
        }
    }

    private void SetAssetCachePaths(bool perm = false)
    {
        if (perm)
        {
            GlobalPaths.AssetCacheDir = GlobalPaths.DataPath;
            GlobalPaths.AssetCacheDirSky = GlobalPaths.AssetCacheDir + "\\sky";
            GlobalPaths.AssetCacheDirFonts = GlobalPaths.AssetCacheDir + GlobalPaths.DirFonts;
            GlobalPaths.AssetCacheDirSounds = GlobalPaths.AssetCacheDir + GlobalPaths.DirSounds;
            GlobalPaths.AssetCacheDirTextures = GlobalPaths.AssetCacheDir + GlobalPaths.DirTextures;
            GlobalPaths.AssetCacheDirTexturesGUI = GlobalPaths.AssetCacheDirTextures + "\\gui";
            GlobalPaths.AssetCacheDirScripts = GlobalPaths.AssetCacheDir + GlobalPaths.DirScripts;
            //GlobalPaths.AssetCacheDirScriptAssets = GlobalPaths.AssetCacheDir + "\\scriptassets";

            GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir;
            GlobalPaths.AssetCacheFontsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.FontsGameDir;
            GlobalPaths.AssetCacheSkyGameDir = GlobalPaths.AssetCacheGameDir + "sky/";
            GlobalPaths.AssetCacheSoundsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.SoundsGameDir;
            GlobalPaths.AssetCacheTexturesGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.TexturesGameDir;
            GlobalPaths.AssetCacheTexturesGUIGameDir = GlobalPaths.AssetCacheTexturesGameDir + "gui/";
            GlobalPaths.AssetCacheScriptsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.ScriptsGameDir;
            //GlobalPaths.AssetCacheScriptAssetsGameDir = GlobalPaths.AssetCacheGameDir + "scriptassets/";
        }
        else
        {
            GlobalPaths.AssetCacheDir = GlobalPaths.DataPath + "\\assetcache";
            GlobalPaths.AssetCacheDirSky = GlobalPaths.AssetCacheDir + "\\sky";
            GlobalPaths.AssetCacheDirFonts = GlobalPaths.AssetCacheDir + GlobalPaths.DirFonts;
            GlobalPaths.AssetCacheDirSounds = GlobalPaths.AssetCacheDir + GlobalPaths.DirSounds;
            GlobalPaths.AssetCacheDirTextures = GlobalPaths.AssetCacheDir + GlobalPaths.DirTextures;
            GlobalPaths.AssetCacheDirTexturesGUI = GlobalPaths.AssetCacheDirTextures + "\\gui";
            GlobalPaths.AssetCacheDirScripts = GlobalPaths.AssetCacheDir + GlobalPaths.DirScripts;
            //GlobalPaths.AssetCacheDirScriptAssets = GlobalPaths.AssetCacheDir + "\\scriptassets";

            GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir + "assetcache/";
            GlobalPaths.AssetCacheFontsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.FontsGameDir;
            GlobalPaths.AssetCacheSkyGameDir = GlobalPaths.AssetCacheGameDir + "sky/";
            GlobalPaths.AssetCacheSoundsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.SoundsGameDir;
            GlobalPaths.AssetCacheTexturesGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.TexturesGameDir;
            GlobalPaths.AssetCacheTexturesGUIGameDir = GlobalPaths.AssetCacheTexturesGameDir + "gui/";
            GlobalPaths.AssetCacheScriptsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.ScriptsGameDir;
            //GlobalPaths.AssetCacheScriptAssetsGameDir = GlobalPaths.AssetCacheGameDir + "scriptassets/";
        }
    }

    private void AssetLocalization_AssetTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentType = (RobloxFileType)AssetLocalization_AssetTypeBox.SelectedIndex;
    }

    private void AssetLocalization_ItemNameBox_TextChanged(object sender, EventArgs e)
    {
        name = AssetLocalization_ItemNameBox.Text;
    }

    private void AssetLocalization_UsesHatMeshBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AssetLocalization_UsesHatMeshBox.SelectedItem.ToString() == "None")
        {
            meshname = "";
        }
        else
        {
            meshname = AssetLocalization_UsesHatMeshBox.SelectedItem.ToString();
        }
    }

    private void AssetLocalization_SaveBackups_CheckedChanged(object sender, EventArgs e)
    {
        GlobalVars.UserConfiguration.AssetLocalizerSaveBackups = AssetLocalization_SaveBackups.Checked;
    }

    private void AssetLocalization_LocalizeButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog robloxFileDialog = LoadROBLOXFileDialog(currentType);

        if (robloxFileDialog.ShowDialog() == DialogResult.OK)
        {
            path = robloxFileDialog.FileName;
            AssetLocalization_BackgroundWorker.RunWorkerAsync();
        }
    }

    // This event handler is where the time-consuming work is done.
    private void AssetLocalization_BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        BackgroundWorker worker = sender as BackgroundWorker;
        LocalizeAsset(currentType, worker, path, name, meshname);
    }

    // This event handler updates the progress.
    private void AssetLocalization_BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        AssetLocalization_StatusText.Text = GetProgressString(currentType, e.ProgressPercentage);
        AssetLocalization_StatusBar.Value = e.ProgressPercentage;
    }

    // This event handler deals with the results of the background operation.
    private void AssetLocalization_BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        switch (e)
        {
            case RunWorkerCompletedEventArgs can when can.Cancelled:
                AssetLocalization_StatusText.Text = "Canceled!";
                break;
            case RunWorkerCompletedEventArgs err when err.Error != null:
                AssetLocalization_StatusText.Text = "Error: " + e.Error.Message;
                MessageBox.Show("Error: " + e.Error.Message + "\n\n" + e.Error.StackTrace, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            default:
                AssetLocalization_StatusText.Text = "Done!";
                break;
        }
    }

    private void AssetLocalization_LocalizePermanentlyBox_Click(object sender, EventArgs e)
    {
        if (AssetLocalization_LocalizePermanentlyBox.Checked)
        {
            DialogResult res = MessageBox.Show("If you toggle this option, the Asset SDK will download all localized files directly into your Novetus data, rather than into the Asset Cache. This means you won't be able to clear these files with the 'Clear Asset Cache' option in the Launcher.\n\nWould you like to continue with the option anyways?", "Novetus Asset SDK - Permanent Localization Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.No)
            {
                AssetLocalization_LocalizePermanentlyBox.Checked = false;
            }
        }
    }

    private void AssetLocalization_LocalizePermanentlyBox_CheckedChanged(object sender, EventArgs e)
    {
        if (AssetLocalization_LocalizePermanentlyBox.Checked)
        {
            SetAssetCachePaths(true);
        }
        else
        {
            SetAssetCachePaths();
        }
    }
    #endregion

    #region Mesh Converter
    private void MeshConverter_ConvertButton_Click(object sender, EventArgs e)
    {
        if (MeshConverter_OpenOBJDialog.ShowDialog() == DialogResult.OK)
        {
            MeshConverter_ProcessOBJ(GlobalPaths.ConfigDirData + "\\RBXMeshConverter.exe", MeshConverter_OpenOBJDialog.FileName);
        }
    }

    private void MeshConverter_ProcessOBJ(string EXEName, string FileName)
    {
        MeshConverter_StatusText.Text = "Loading utility...";
        Process proc = new Process();
        proc.StartInfo.FileName = EXEName;
        proc.StartInfo.Arguments = "-f " + FileName + " -v " + MeshConverter_MeshVersionSelector.Value;
        proc.StartInfo.CreateNoWindow = false;
        proc.StartInfo.UseShellExecute = false;
        proc.EnableRaisingEvents = true;
        proc.Exited += new EventHandler(OBJ2MeshV1Exited);
        proc.Start();
        MeshConverter_StatusText.Text = "Converting OBJ to ROBLOX Mesh v" + MeshConverter_MeshVersionSelector.Value + "...";
    }

    void OBJ2MeshV1Exited(object sender, EventArgs e)
    {
        MeshConverter_StatusText.Text = "Ready";
        string properName = Path.GetFileName(MeshConverter_OpenOBJDialog.FileName) + ".mesh";
        MessageBox.Show("File " + properName + " created!", "Novetus Asset SDK - Mesh File Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    #endregion

    #endregion
}
