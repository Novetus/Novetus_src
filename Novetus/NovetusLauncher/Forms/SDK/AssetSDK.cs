#region Usings
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
#endregion

public partial class AssetSDK : Form
{
    #region Private Variables
    //shared
    public Provider[] contentProviders;
    private string url = "";
    private bool isWebSite = false;
    //localizer
    private RobloxFileType currentType;
    private string path;
    private string name;
    private string meshname;
    //downloader
    private bool batchMode = false;
    private bool hasOverrideWarningOpenedOnce = false;
    //obj2mesh
    private OpenFileDialog MeshConverter_OpenOBJDialog;
    private string output;
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
        //shared
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
        {
            contentProviders = OnlineClothing.GetContentProviders();

            for (int i = 0; i < contentProviders.Length; i++)
            {
                if (contentProviders[i].URL.Contains("?id="))
                {
                    URLSelection.Items.Add(contentProviders[i].Name);
                }
            }
        }

        URLSelection.Items.Add("https://www.roblox.com/catalog/");
        URLSelection.Items.Add("https://www.roblox.com/library/");
        isWebSite = false;

        URLSelection.SelectedItem = URLSelection.Items[0];

        //downloader
        AssetDownloader_LoadHelpMessage.Checked = GlobalVars.UserConfiguration.DisabledAssetSDKHelp;

        //asset localizer
        AssetLocalization_SaveBackups.Checked = GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups;
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

        //MeshConverter
        MeshConverter_MeshVersionSelector.SelectedItem = "1.00";

        SetAssetCachePaths();

        GlobalFuncs.CreateAssetCacheDirectories();
    }

    void AssetSDK_Close(object sender, CancelEventArgs e)
    {
        SetAssetCachePaths();

        //asset localizer
        AssetLocalization_BackgroundWorker.CancelAsync();
    }

    private void URLSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetURL();
    }

    private void URLOverrideBox_Click(object sender, EventArgs e)
    {
        if (hasOverrideWarningOpenedOnce == false && !GlobalVars.UserConfiguration.DisabledAssetSDKHelp)
        {
            MessageBox.Show("By using the custom URL setting, you will override any selected entry in the default URL list. Keep this in mind before downloading anything with this option.\n\nAlso, the URL must be a asset url with 'asset/?id=' at the end of it in order for the Asset Downloader to work smoothly.", "Novetus Asset SDK - URL Override Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            hasOverrideWarningOpenedOnce = true;
        }
    }

    private void URLOverrideBox_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(URLOverrideBox.Text))
        {
            URLSelection.Enabled = false;
            url = URLOverrideBox.Text;
        }
        else
        {
            URLSelection.Enabled = true;
            SetURL();
        }

        MessageBox.Show(url);
    }

    void SetURL()
    {
        if (URLSelection.SelectedItem.Equals("https://www.roblox.com/catalog/") || URLSelection.SelectedItem.Equals("https://www.roblox.com/library/"))
        {
            url = URLSelection.SelectedItem.ToString();
            isWebSite = true;
        }
        else
        {
            Provider pro = OnlineClothing.FindContentProviderByName(contentProviders, URLSelection.SelectedItem.ToString());
            if (pro != null)
            {
                url = pro.URL;
                isWebSite = false;
            }
        }
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
                if (!GlobalVars.UserConfiguration.DisabledAssetSDKHelp)
                {
                    string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - Roblox Model/Item\n.rbxl - Roblox Place\n.mesh - Roblox Mesh\n.png - Texture/Icon\n.wav - Sound\n.lua - Lua Script";
                    MessageBox.Show(helptext, "Novetus Asset SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Downloader download = new Downloader(fullURL, name, "Roblox Model (*.rbxm)|*.rbxm|Roblox Place (*.rbxl) |*.rbxl|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav|Lua Script (*.lua)|*.lua");

                try
                {
                    string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, use the Asset Localizer in the Asset SDK.\n\nIf you get a corrupted file, change the URL using the drop down box.";
                    download.InitDownload((!GlobalVars.UserConfiguration.DisabledAssetSDKHelp) ? helptext : "");
                }
                catch (Exception ex)
                {
                    GlobalFuncs.LogExceptions(ex);
                    MessageBox.Show("Error: Unable to download the file. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
                {
                    MessageBoxIcon boxicon = MessageBoxIcon.Information;

                    if (download.getDownloadOutcome().Contains("Error"))
                    {
                        boxicon = MessageBoxIcon.Error;
                    }

                    MessageBox.Show(download.getDownloadOutcome(), "Novetus Asset SDK - Download Completed", MessageBoxButtons.OK, boxicon);
                }
            }
            else
            {
                Process.Start(fullURL);
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.", "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static bool StartItemBatchDownload(string name, string url, string id, int ver, bool iswebsite, string path)
    {
        bool noErrors = true;

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
                catch (Exception ex)
                {
                    GlobalFuncs.LogExceptions(ex);
                    noErrors = false;
                }
            }
            else
            {
                Process.Start(fullURL);
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            noErrors = false;
        }

        return noErrors;
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
            try
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

                    int lineCount = lines.Count();

                    foreach (var line in lines)
                    {
                        string[] linesplit = line.Split('|');
                        bool noErrors = StartItemBatchDownload(
                            linesplit[0] + extension,
                            url,
                            linesplit[1],
                            Convert.ToInt32(linesplit[2]),
                            isWebSite, basepath);

                        if (!noErrors)
                        {
                            --lineCount;
                        }
                    }

                    AssetDownloaderBatch_Status.Visible = false;

                    string extraText = (lines.Count() != lineCount) ? "\n" + (lines.Count() - lineCount) + " errors were detected during the download. Make sure your IDs and links are valid." : "";

                    MessageBox.Show("Batch download complete! " + lineCount + " items downloaded!" + extraText, "Novetus Asset SDK - Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);

                MessageBox.Show("Unable to batch download files. Error:" + ex.Message + "\n Make sure your items are set up properly.", "Novetus Asset SDK - Unable to batch download files.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void AssetDownloader_LoadHelpMessage_CheckedChanged(object sender, EventArgs e)
    {
        GlobalVars.UserConfiguration.DisabledAssetSDKHelp = AssetDownloader_LoadHelpMessage.Checked;
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
    #endregion

    #region Asset Fixer

    public static OpenFileDialog LoadROBLOXFileDialog(RobloxFileType type)
    {
        string typeFilter = "";

        switch (type)
        {
            case RobloxFileType.RBXL:
                typeFilter = "Roblox Level (*.rbxl)|*.rbxl|Roblox Level (*.rbxlx)|*.rbxlx";
                break;
            case RobloxFileType.Script:
                typeFilter = "Lua Script (*.lua)|*.lua";
                break;
            default:
                typeFilter = "Roblox Model (*.rbxm)|*.rbxm";
                break;
        }

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        openFileDialog1.Filter = typeFilter;
        openFileDialog1.Title = "Open Roblox level or model";

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
                        progressString = "Fixing RBXL Meshes and Textures...";
                        break;
                    case 10:
                        progressString = "Fixing RBXL Skybox Textures...";
                        break;
                    case 15:
                        progressString = "Fixing RBXL Decal Textures...";
                        break;
                    case 20:
                        progressString = "Fixing RBXL Textures...";
                        break;
                    case 25:
                        progressString = "Fixing RBXL Tool Textures...";
                        break;
                    case 30:
                        progressString = "Fixing RBXL HopperBin Textures...";
                        break;
                    case 40:
                        progressString = "Fixing RBXL Sounds...";
                        break;
                    case 50:
                        progressString = "Fixing RBXL GUI Textures...";
                        break;
                    case 60:
                        progressString = "Fixing RBXL Shirt Textures...";
                        break;
                    case 65:
                        progressString = "Fixing RBXL T-Shirt Textures...";
                        break;
                    case 70:
                        progressString = "Fixing RBXL Pants Textures...";
                        break;
                    case 80:
                        progressString = "Fixing RBXL Linked Scripts...";
                        break;
                    case 90:
                        progressString = "Fixing RBXL Linked LocalScripts...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.RBXM:
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing RBXM Meshes and Textures...";
                        break;
                    case 10:
                        progressString = "Fixing RBXM Skybox Textures...";
                        break;
                    case 15:
                        progressString = "Fixing RBXM Decal Textures...";
                        break;
                    case 20:
                        progressString = "Fixing RBXM Textures...";
                        break;
                    case 25:
                        progressString = "Fixing RBXM Tool Textures...";
                        break;
                    case 30:
                        progressString = "Fixing RBXM HopperBin Textures...";
                        break;
                    case 40:
                        progressString = "Fixing RBXM Sounds...";
                        break;
                    case 50:
                        progressString = "Fixing RBXM GUI Textures...";
                        break;
                    case 60:
                        progressString = "Fixing RBXM Shirt Textures...";
                        break;
                    case 65:
                        progressString = "Fixing RBXM T-Shirt Textures...";
                        break;
                    case 70:
                        progressString = "Fixing RBXM Pants Textures...";
                        break;
                    case 80:
                        progressString = "Fixing RBXM Linked Scripts...";
                        break;
                    case 90:
                        progressString = "Fixing RBXM Linked LocalScripts...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.Hat:
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing Hat Meshes and Textures...";
                        break;
                    case 25:
                        progressString = "Fixing Hat Sounds...";
                        break;
                    case 50:
                        progressString = "Fixing Hat Linked Scripts...";
                        break;
                    case 75:
                        progressString = "Fixing Hat Linked LocalScripts...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.Head:
                //meshes
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing Head Meshes and Textures...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.Face:
                //decal
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing Face Textures...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.TShirt:
                //texture
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing T-Shirt Textures...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.Shirt:
                //texture
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing Shirt Textures...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.Pants:
                //texture
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing Pants Textures...";
                        break;
                    case 95:
                        progressString = "Downloading extra assets...";
                        break;
                }
                break;
            case RobloxFileType.Script:
                //script
                switch (percent)
                {
                    case 0:
                        progressString = "Fixing Script...";
                        break;
                }
                break;
            default:
                progressString = "Idle";
                break;
        }

        return progressString + " " + percent.ToString() + "%";
    }

    public static void DownloadFromScript(string filepath, string savefilepath, string inGameDir)
    {
        string[] file = File.ReadAllLines(filepath);

        try
        {
            int index = 0;
            foreach (var line in file)
            {
                ++index;

                if (line.Contains("http://") || line.Contains("https://"))
                {
                    //https://stackoverflow.com/questions/10576686/c-sharp-regex-pattern-to-extract-urls-from-given-string-not-full-html-urls-but
                    List<string> links = new List<string>();
                    var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    foreach (Match m in linkParser.Matches(line))
                    {
                        string link = m.Value;
                        links.Add(link);
                    }

                    foreach (string link in links)
                    {
                        string newurl = ((!link.Contains("http://") || !link.Contains("https://")) ? "https://" : "") 
                            + "assetdelivery.roblox.com/v1/asset/?id=";
                        string urlReplaced = newurl.Contains("https://") ? link.Replace("http://", "").Replace("https://", "") : link.Replace("http://", "https://");
                        string urlFixed = urlReplaced.Replace("?version=1&amp;id=", "?id=")
                            .Replace("www.roblox.com/asset/?id=", newurl)
                            .Replace("www.roblox.com/asset?id=", newurl)
                            .Replace("assetgame.roblox.com/asset/?id=", newurl)
                            .Replace("assetgame.roblox.com/asset?id=", newurl)
                            .Replace("roblox.com/asset/?id=", newurl)
                            .Replace("roblox.com/asset?id=", newurl)
                            .Replace("&amp;", "&")
                            .Replace("amp;", "&")
                            .Replace("}", "")
                            .Replace("]", "")
                            .Replace("\"", "")
                            .Replace("'", "")
                            .Replace("&quot;", "")
                            .Replace("&quot", "");

                        string peram = "id=";

                        if (urlFixed.Contains(peram))
                        {
                            string IDVal = urlFixed.After(peram);
                            string OriginalIDVal = link.After(peram);
                            RobloxXML.DownloadFilesFromNode(urlFixed, savefilepath, "", IDVal);
                            file[index - 1] = file[index - 1].Replace(link, inGameDir + OriginalIDVal);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            MessageBox.Show("Error: Unable to fix the asset. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            File.WriteAllLines(filepath, file);
        }
    }

    public static void FixURLSInScript(string filepath, string url)
    {
        string[] file = File.ReadAllLines(filepath);

        try
        {
            int index = 0;

            foreach (var line in file)
            {
                ++index;

                if ((line.Contains("http://") || line.Contains("https://")) && !line.Contains(url))
                {
                    string oldurl = line;
                    string urlFixed = oldurl.Replace("http://", "")
                        .Replace("https://", "")
                        .Replace("?version=1&amp;id=", "?id=")
                        .Replace("www.roblox.com/asset/?id=", url)
                        .Replace("www.roblox.com/asset?id=", url)
                        .Replace("assetgame.roblox.com/asset/?id=", url)
                        .Replace("assetgame.roblox.com/asset?id=", url)
                        .Replace("roblox.com/asset/?id=", url)
                        .Replace("roblox.com/asset?id=", url)
                        .Replace("&amp;", "&")
                        .Replace("amp;", "&");

                    string peram = "id=";

                    if (urlFixed.Contains(peram))
                    {
                        file[index - 1] = urlFixed;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            MessageBox.Show("Error: Unable to fix the asset. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            File.WriteAllLines(filepath, file);
        }
    }

    public static void FixURLSOrDownloadFromScript(string filepath, string savefilepath, string inGameDir, bool useURLs, string url)
    {
        if (useURLs)
        {
            FixURLSInScript(filepath, url);
        }
        else
        {
            DownloadFromScript(filepath, savefilepath, inGameDir);
        }
    }

    public void LocalizeAsset(RobloxFileType type, BackgroundWorker worker, string path, string itemname, string meshname, bool useURLs = false, string remoteurl = "")
    {
        string oldfile = File.ReadAllText(path);
        XDocument doc = null;

        try
        {
            string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile)).Replace("&#9;", "\t").Replace("#9;", "\t");
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };
            Stream filestream = GlobalFuncs.GenerateStreamFromString(fixedfile);
            using (XmlReader xmlReader = XmlReader.Create(filestream, xmlReaderSettings))
            {
                xmlReader.MoveToContent();
                doc = XDocument.Load(xmlReader);
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            //assume we're a script
            if (type == RobloxFileType.Script)
            {
                if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                {
                    try
                    {
                        worker.ReportProgress(0);
                        GlobalFuncs.FixedFileCopy(path, path.Replace(".lua", " - BAK.lua"), false);
                    }
                    catch (Exception ex2)
                    {
                        GlobalFuncs.LogExceptions(ex2);
                        worker.ReportProgress(100);
                        return;
                    }
                }
                else
                {
                    worker.ReportProgress(0);
                }

                FixURLSOrDownloadFromScript(path, GlobalPaths.AssetCacheDirAssets, GlobalPaths.AssetCacheAssetsGameDir, useURLs, url);
                worker.ReportProgress(100);
            }
            else
            {
                MessageBox.Show("Error: Unable to fix the asset. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                worker.ReportProgress(0);
            }
            return;
        }

        try
        {
            switch (type)
            {
                case RobloxFileType.RBXL:
                    //backup the original copy
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxl", " - BAK.rbxl"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    worker.ReportProgress(5);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Fonts, itemname);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Fonts, 1, 1, 1, 1, itemname);
                    //skybox
                    worker.ReportProgress(10);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 1, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 2, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 3, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 4, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Decal, itemname);
                    //texture
                    worker.ReportProgress(20);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Texture, itemname);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Shirt, itemname);
                    worker.ReportProgress(65);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ShirtGraphic, itemname);
                    worker.ReportProgress(70);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Pants, itemname);
                    //scripts
                    worker.ReportProgress(80);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.LocalScript);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.RBXM:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Fonts, itemname);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Fonts, 1, 1, 1, 1, itemname);
                    //skybox
                    worker.ReportProgress(10);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 1, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 2, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 3, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 4, 0, 0, 0);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sky, 5, 0, 0, 0);
                    //decal
                    worker.ReportProgress(15);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Decal, itemname);
                    //texture
                    worker.ReportProgress(20);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Texture, itemname);
                    //tools and hopperbin
                    worker.ReportProgress(25);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Tool);
                    worker.ReportProgress(30);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.HopperBin);
                    //sound
                    worker.ReportProgress(40);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Sound);
                    worker.ReportProgress(50);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ImageLabel);
                    //clothing
                    worker.ReportProgress(60);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Shirt, itemname);
                    worker.ReportProgress(65);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ShirtGraphic, itemname);
                    worker.ReportProgress(70);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Pants, itemname);
                    //scripts
                    worker.ReportProgress(80);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.Script);
                    worker.ReportProgress(90);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.LocalScript);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.Hat:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHatFonts, itemname, meshname);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHatFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(25);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHatSound);
                    //scripts
                    worker.ReportProgress(50);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHatScript);
                    worker.ReportProgress(75);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHatLocalScript);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.Head:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //meshes
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHeadFonts, itemname);
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemHeadFonts, 1, 1, 1, 1, itemname);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.Face:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //decal
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemFaceTexture, itemname);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.TShirt:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //texture
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemTShirtTexture, itemname);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.Shirt:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //texture
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemShirtTexture, itemname);
                    worker.ReportProgress(95);
                    break;
                case RobloxFileType.Pants:
                    if (GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups)
                    {
                        try
                        {
                            worker.ReportProgress(0);
                            GlobalFuncs.FixedFileCopy(path, path.Replace(".rbxm", " - BAK.rbxm"), false);
                        }
                        catch (Exception ex)
                        {
                            GlobalFuncs.LogExceptions(ex);
                            worker.ReportProgress(100);
                            return;
                        }
                    }
                    else
                    {
                        worker.ReportProgress(0);
                    }
                    //texture
                    RobloxXML.DownloadOrFixURLS(doc, useURLs, remoteurl, RobloxDefs.ItemPantsTexture, itemname);
                    worker.ReportProgress(95);
                    break;
                default:
                    worker.ReportProgress(100);
                    break;
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            MessageBox.Show("Error: Unable to fix the asset. " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { CheckCharacters = false, Indent = true };
            using (XmlWriter xmlReader = XmlWriter.Create(path, xmlWriterSettings))
            {
                doc.WriteTo(xmlReader);
            }

            //download any assets we missed.
            FixURLSOrDownloadFromScript(path, GlobalPaths.AssetCacheDirAssets, GlobalPaths.AssetCacheAssetsGameDir, useURLs, url);
            worker.ReportProgress(100);
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
            GlobalPaths.AssetCacheDirAssets = GlobalPaths.AssetCacheDir + "\\assets";

            GlobalFuncs.CreateAssetCacheDirectories();

            GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir;
            GlobalPaths.AssetCacheFontsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.FontsGameDir;
            GlobalPaths.AssetCacheSkyGameDir = GlobalPaths.AssetCacheGameDir + "sky/";
            GlobalPaths.AssetCacheSoundsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.SoundsGameDir;
            GlobalPaths.AssetCacheTexturesGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.TexturesGameDir;
            GlobalPaths.AssetCacheTexturesGUIGameDir = GlobalPaths.AssetCacheTexturesGameDir + "gui/";
            GlobalPaths.AssetCacheScriptsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.ScriptsGameDir;
            GlobalPaths.AssetCacheAssetsGameDir = GlobalPaths.AssetCacheGameDir + "assets/";
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
            GlobalPaths.AssetCacheDirAssets = GlobalPaths.AssetCacheDir + "\\assets";

            GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir + "assetcache/";
            GlobalPaths.AssetCacheFontsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.FontsGameDir;
            GlobalPaths.AssetCacheSkyGameDir = GlobalPaths.AssetCacheGameDir + "sky/";
            GlobalPaths.AssetCacheSoundsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.SoundsGameDir;
            GlobalPaths.AssetCacheTexturesGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.TexturesGameDir;
            GlobalPaths.AssetCacheTexturesGUIGameDir = GlobalPaths.AssetCacheTexturesGameDir + "gui/";
            GlobalPaths.AssetCacheScriptsGameDir = GlobalPaths.AssetCacheGameDir + GlobalPaths.ScriptsGameDir;
            GlobalPaths.AssetCacheAssetsGameDir = GlobalPaths.AssetCacheGameDir + "assets/";
        }
    }

    private void AssetLocalization_AssetTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AssetLocalization_ShowItemTypes.Checked)
        {
            switch (AssetLocalization_AssetTypeBox.SelectedIndex)
            {
                case 1:
                    currentType = RobloxFileType.RBXM;
                    break;
                case 2:
                    currentType = RobloxFileType.Hat;
                    break;
                case 3:
                    currentType = RobloxFileType.Head;
                    break;
                case 4:
                    currentType = RobloxFileType.Face;
                    break;
                case 5:
                    currentType = RobloxFileType.TShirt;
                    break;
                case 6:
                    currentType = RobloxFileType.Shirt;
                    break;
                case 7:
                    currentType = RobloxFileType.Pants;
                    break;
                case 8:
                    currentType = RobloxFileType.Script;
                    break;
                default:
                    currentType = RobloxFileType.RBXL;
                    break;
            }
        }
        else
        {
            switch (AssetLocalization_AssetTypeBox.SelectedIndex)
            {
                case 1:
                    currentType = RobloxFileType.RBXM;
                    break;
                case 2:
                    currentType = RobloxFileType.Script;
                    break;
                default:
                    currentType = RobloxFileType.RBXL;
                    break;
            }
        }
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
        GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups = AssetLocalization_SaveBackups.Checked;
    }

    private void AssetLocalization_LocalizeButton_Click(object sender, EventArgs e)
    {
        if (isWebSite)
        {
            MessageBox.Show("Error: Unable to fix the asset because you chose a URL that cannot be downloaded from. Please choose a different URL.", "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

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
        LocalizeAsset(currentType, worker, path, name, meshname,
                AssetLocalization_AssetLinks.Checked ? AssetLocalization_AssetLinks.Checked : false,
                AssetLocalization_AssetLinks.Checked ? url : "");
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
        if (AssetLocalization_LocalizePermanentlyBox.Checked && !GlobalVars.UserConfiguration.DisabledAssetSDKHelp)
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
        LocalizePermanentlyIfNeeded();
    }

    private void AssetLocalization_AssetLinks_CheckedChanged(object sender, EventArgs e)
    {
        if (AssetLocalization_AssetLinks.Checked)
        {
            AssetLocalization_LocalizeButton.Text = AssetLocalization_LocalizeButton.Text.Replace("Localize", "Fix");
            AssetLocalization_LocalizePermanentlyBox.Enabled = false;
            SetAssetCachePaths();
        }
        else
        {
            AssetLocalization_LocalizeButton.Text = AssetLocalization_LocalizeButton.Text.Replace("Fix", "Localize");
            AssetLocalization_LocalizePermanentlyBox.Enabled = true;
            LocalizePermanentlyIfNeeded();
        }
    }

    void LocalizePermanentlyIfNeeded()
    {
        if (AssetLocalization_LocalizePermanentlyBox.Checked)
        {
            AssetLocalization_AssetLinks.Enabled = false;
            SetAssetCachePaths(true);
        }
        else
        {
            AssetLocalization_AssetLinks.Enabled = true;
            SetAssetCachePaths();
        }
    }

    private void AssetLocalization_ShowItemTypes_CheckedChanged(object sender, EventArgs e)
    {
        AssetLocalization_AssetTypeBox.Items.Clear();

        if (AssetLocalization_ShowItemTypes.Checked)
        {
            AssetLocalization_AssetTypeBox.Items.AddRange(new object[] {
                "RBXL",
                "RBXM",
                "Hat",
                "Head",
                "Face",
                "T-Shirt",
                "Shirt",
                "Pants",
                "Lua Script"});
        }
        else
        {
            AssetLocalization_AssetTypeBox.Items.AddRange(new object[] {
                "RBXL",
                "RBXM",
                "Lua Script"});
        }

        AssetLocalization_AssetTypeBox.SelectedItem = "RBXL";
    }
    #endregion

    #region Mesh Converter
    private void MeshConverter_ConvertButton_Click(object sender, EventArgs e)
    {
        if (MeshConverter_OpenOBJDialog.ShowDialog() == DialogResult.OK)
        {
            MeshConverter_ProcessOBJ(GlobalPaths.DataDir + "\\ObjToRBXMesh.exe", MeshConverter_OpenOBJDialog.FileName);
        }
    }

    private void MeshConverter_ProcessOBJ(string EXEName, string FileName)
    {
        MeshConverter_StatusText.Text = "Loading utility...";
        Process proc = new Process();
        proc.StartInfo.FileName = EXEName;
        proc.StartInfo.Arguments = "\"" + FileName + "\" " + MeshConverter_MeshVersionSelector.Text;
        proc.StartInfo.CreateNoWindow = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.EnableRaisingEvents = true;
        proc.Exited += new EventHandler(OBJ2MeshV1Exited);
        proc.Start();
        MeshConverter_StatusText.Text = "Converting OBJ to Roblox Mesh v" + MeshConverter_MeshVersionSelector.Text + "...";
        output = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit();
    }

    void OBJ2MeshV1Exited(object sender, EventArgs e)
    {
        MeshConverter_StatusText.Text = "Ready";
        string properName = Path.GetFileName(MeshConverter_OpenOBJDialog.FileName) + ".mesh";
        string message = "File " + properName + " created!";

        if (output.Contains("ERROR"))
        {
            message = "Error when creating file.";
        }

        string small_output = output.Substring(0, 1024);

        MessageBox.Show(message + "\nOutput:\n" + small_output, "Novetus Asset SDK - Mesh File Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    #endregion

    #endregion
}
