#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
#endregion

public partial class AssetFixer : Form
{
    #region Private Variables
    public ContentProvider[] contentProviders;
    private string url = "";
    private bool isWebSite = false;
    private RobloxFileType currentType;
    private string path;
    private string customFolder;
    private int errors = 0;
    private bool hasOverrideWarningOpenedOnce = false;
    private bool compressedMap = false;
    #endregion

    #region Constructor
    public AssetFixer()
    {
        InitializeComponent();
    }
    #endregion

    #region Form Events

    #region Load/Close Events
    private void AssetSDK_Load(object sender, EventArgs e)
    {
        //shared
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
        {
            contentProviders = ContentProvider.GetContentProviders();

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

        //asset localizer
        AssetLocalization_SaveBackups.Checked = GlobalVars.UserConfiguration.ReadSettingBool("AssetSDKFixerSaveBackups");
        AssetLocalization_AssetTypeBox.SelectedItem = "RBXL";

        SetAssetCachePaths();

        FileManagement.CreateAssetCacheDirectories();
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
        if (hasOverrideWarningOpenedOnce == false && !GlobalVars.UserConfiguration.ReadSettingBool("DisabledAssetSDKHelp"))
        {
            MessageBox.Show("By using the custom URL setting, you will override any selected entry in the default URL list. Keep this in mind before downloading anything with this option.\n\nAlso, the URL must be a asset url with 'asset/?id=' at the end of it in order for the Asset Downloader to work smoothly.", "Asset Fixer - URL Override Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            ContentProvider pro = ContentProvider.FindContentProviderByName(contentProviders, URLSelection.SelectedItem.ToString());
            if (pro != null)
            {
                url = pro.URL;
                isWebSite = false;
            }
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
                typeFilter = "Roblox Level (*.rbxl)|*.rbxl|Roblox Level (*.rbxlx)|*.rbxlx|BZip2 compressed Roblox Level (*.bz2)|*.bz2";
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

    void ProgressChangedEvent()
    {
        if (AssetFixer_ProgressBar.Value < AssetFixer_ProgressBar.Maximum)
        {
            AssetFixer_ProgressBar.Value += 1;
        }

        AssetFixer_ProgressLabel.Text = "Progress: " + AssetFixer_ProgressBar.Value.ToString() + "/" + AssetFixer_ProgressBar.Maximum.ToString();
    }

    public static void DownloadFilesFromNode(string url, string path, string fileext, string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            Downloader download = new Downloader(url, id);
            download.filePath = path;
            download.showErrorInfo = false;
            download.overwrite = false;
            download.InitDownloadDirect(fileext, "", true);
            if (download.getDownloadOutcome().Contains("Error"))
            {
                Util.ConsolePrint("Download Outcome: " + download.getDownloadOutcome(), 2);
                throw new IOException(download.getDownloadOutcome());
            }
            else
            {
                Util.ConsolePrint("Download Outcome: " + download.getDownloadOutcome(), 3);
            }
        }
    }

    public void FixURLSOrDownloadFromScript(string filepath, string savefilepath, string inGameDir, bool useURLs, string url)
    {
        string[] file = File.ReadAllLines(filepath);

        int length = 0;

        foreach (var line in file)
        {
            if (line.Contains("www.w3.org") || line.Contains("roblox.xsd"))
            {
                continue;
            }

            if (!(line.Contains("http://") || line.Contains("https://")))
            {
                continue;
            }

            length++;
        }

        AssetFixer_ProgressBar.Maximum = length;

        while (AssetFixer_ProgressBar.Value < AssetFixer_ProgressBar.Maximum)
        {
            int index = 0;

            foreach (var line in file)
            {
                ++index;

                try
                {
                    if (line.Contains("www.w3.org") || line.Contains("roblox.xsd"))
                    {
                        continue;
                    }

                    //https://stackoverflow.com/questions/3809401/what-is-a-good-regular-expression-to-match-a-url
                    if (line.Contains("http://") || line.Contains("https://"))
                    {
                        //https://stackoverflow.com/questions/10576686/c-sharp-regex-pattern-to-extract-urls-from-given-string-not-full-html-urls-but
                        List<string> links = new List<string>();

                        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.AssetFixerPatternFileName))
                        {
                            string pattern = File.ReadAllText(GlobalPaths.ConfigDir + "\\" + GlobalPaths.AssetFixerPatternFileName);

                            var linkParser = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            foreach (Match m in linkParser.Matches(line))
                            {
                                string link = m.Value;
                                links.Add(link);
                            }

                            foreach (string link in links)
                            {
                                if (link.Contains(".png") || link.Contains(".jpg") || link.Contains(".jpeg"))
                                {
                                    continue;
                                }

                                if (link.Contains("my-roblox-character-item"))
                                {
                                    continue;
                                }

                                string urlFixed = "";

                                if (useURLs)
                                {
                                    string oldurl = line;
                                    urlFixed = NovetusFuncs.FixURLString(oldurl, url);
                                }
                                else
                                {
                                    string newurl = ((!link.Contains("http://") || !link.Contains("https://")) ? "https://" : "")
                                    + "assetdelivery.roblox.com/v1/asset/?id=";
                                    string urlReplaced = newurl.Contains("https://") ? link.Replace("http://", "").Replace("https://", "") : link.Replace("http://", "https://");
                                    urlFixed = NovetusFuncs.FixURLString(urlReplaced, newurl);
                                }

                                string peram = "id=";

                                if (urlFixed.Contains(peram))
                                {
                                    if (useURLs)
                                    {
                                        file[index - 1] = file[index - 1].Replace(link, urlFixed);
                                    }
                                    else
                                    {
                                        string IDVal = urlFixed.After(peram);
                                        DownloadFilesFromNode(urlFixed, savefilepath, "", IDVal);
                                        file[index - 1] = file[index - 1].Replace(link, inGameDir + IDVal);
                                    }
                                }
                            }

                            ProgressChangedEvent();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    errors += 1;
                    Util.LogPrint("ASSETFIX|FILE " + path + " LINE #" + (index) + " " + ex.Message, 2);
                    Util.LogPrint("ASSETFIX|Asset might be private or unavailable.");
                    ProgressChangedEvent();
                    continue;
                }
            }
        }

        File.WriteAllLines(filepath, file);
    }

    public void LocalizeAsset(RobloxFileType type, BackgroundWorker worker, string path, bool useURLs = false, string remoteurl = "")
    {
        if (path.Contains(".bz2"))
        {
            Util.Decompress(path, true);
            compressedMap = true;
        }

        string fixedPath = path.Replace(".rbxlx.bz2", ".rbxlx").Replace(".rbxl.bz2", ".rbxl");

        LocalizePermanentlyIfNeeded();
        AssetFixer_ProgressLabel.Text = "Loading...";

        bool error = false;
        string[] file = File.ReadAllLines(path);

        foreach (var line in file)
        {
            if (line.Contains("<roblox!"))
            {
                error = true;
                break;
            }
        }

        if (!error && GlobalVars.UserConfiguration.ReadSettingBool("AssetSDKFixerSaveBackups"))
        {
            try
            {
                IOSafe.File.Copy(fixedPath, fixedPath + ".bak", false);
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return;
            }
        }

        //assume we're a script
        try
        {
            if (error)
            {
                if (compressedMap)
                {
                    IOSafe.File.Delete(fixedPath);
                    compressedMap = false;
                }

                throw new FileFormatException("Cannot load models/places in binary format.");
            }
            else
            {
                FixURLSOrDownloadFromScript(path, GlobalPaths.AssetCacheDirAssets, GlobalPaths.AssetCacheAssetsGameDir, useURLs, url);

                if (compressedMap)
                {
                    //compress adds bz2 to our file though? this shouldn't be necessary.
                    Util.Compress(fixedPath, true);
                    IOSafe.File.Delete(fixedPath);
                    compressedMap = false;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
            MessageBox.Show("Error: Unable to load the asset. " + ex.Message + "\n\nIf the asset is a modern place or model, try converting the place or model to rbxlx/rbxmx format using MODERN Roblox Studio, then convert it using the Roblox Legacy Place Converter. It should then load fine in the Asset Fixer.", "Asset Fixer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }

    private void SetAssetCachePaths(bool perm = false)
    {
        if (perm)
        {
            if (!string.IsNullOrWhiteSpace(customFolder))
            {
                GlobalPaths.AssetCacheDir = GlobalPaths.DataPath + "\\" + customFolder;
                GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir + customFolder + "/";
            }
            else
            {
                GlobalPaths.AssetCacheDir = GlobalPaths.DataPath;
                GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir;
            }

            GlobalPaths.AssetCacheDirAssets = GlobalPaths.AssetCacheDir + "\\assets";
            GlobalPaths.AssetCacheAssetsGameDir = GlobalPaths.AssetCacheGameDir + "assets/";
        }
        else
        {
            GlobalPaths.AssetCacheDir = GlobalPaths.DataPath + "\\assetcache";
            GlobalPaths.AssetCacheGameDir = GlobalPaths.SharedDataGameDir + "assetcache/";

            GlobalPaths.AssetCacheDirAssets = GlobalPaths.AssetCacheDir + "\\assets";
            GlobalPaths.AssetCacheAssetsGameDir = GlobalPaths.AssetCacheGameDir + "assets/";
        }

        FileManagement.CreateAssetCacheDirectories();
    }

    private void AssetLocalization_AssetTypeBox_SelectedIndexChanged(object sender, EventArgs e)
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

    private void AssetLocalization_ItemNameBox_TextChanged(object sender, EventArgs e)
    {
        customFolder = AssetLocalization_CustomFolderNameBox.Text;
    }

    private void AssetLocalization_SaveBackups_CheckedChanged(object sender, EventArgs e)
    {
        GlobalVars.UserConfiguration.SaveSettingBool("AssetSDKFixerSaveBackups", AssetLocalization_SaveBackups.Checked);
    }

    private void AssetLocalization_LocalizeButton_Click(object sender, EventArgs e)
    {
        if (isWebSite)
        {
            MessageBox.Show("Error: Unable to fix the asset because you chose a URL that cannot be downloaded from. Please choose a different URL.", "Asset Fixer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        LocalizeAsset(currentType, worker, path,
                AssetLocalization_AssetLinks.Checked ? AssetLocalization_AssetLinks.Checked : false,
                AssetLocalization_AssetLinks.Checked ? url : "");
    }

    // This event handler updates the progress.
    private void AssetLocalization_BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        //Progress Bar doesn't work here, wtf?
    }

    // This event handler deals with the results of the background operation.
    private void AssetLocalization_BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        switch (e)
        {
            case RunWorkerCompletedEventArgs can when can.Cancelled:
                AssetFixer_ProgressLabel.Text = "Canceled!";
                break;
            case RunWorkerCompletedEventArgs err when err.Error != null:
                AssetFixer_ProgressLabel.Text = "Error: " + e.Error.Message;
                MessageBox.Show("Error: " + e.Error.Message + "\n\n" + e.Error.StackTrace, "Asset Fixer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            default:
                if (errors > 0)
                {
                    bool isOnlyOneError = (errors == 1 || errors == -1);

                    string errorCountStringLabel = errors + (isOnlyOneError ? " error" : " errors");
                    AssetFixer_ProgressLabel.Text = "Completed with " + errorCountStringLabel + "!";

                    string errorCountStringBox = errors + (isOnlyOneError ? " error was" : " errors were");
                    MessageBox.Show(errorCountStringBox + " found. Please look in today's log in \"" + GlobalPaths.LogDir + "\" for more details." +
                            "\n\nSome assets may be removed due to " +
                            "\n- Removal of the asset by the original owner" +
                            "\n- Privatization of the original asset by the owner" +
                            "\n- The asset just isn't available for the user to download (common for models)" +
                            "\n\nYour file may still function, but it may have issues that need to be corrected manually.", "Asset Fixer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AssetFixer_ProgressLabel.Text = "Completed!";
                }
                break;
        }

        AssetFixer_ProgressBar.Value = 0;
    }

    private void AssetLocalization_LocalizePermanentlyBox_Click(object sender, EventArgs e)
    {
        if (AssetLocalization_LocalizePermanentlyBox.Checked && !GlobalVars.UserConfiguration.ReadSettingBool("DisabledAssetSDKHelp"))
        {
            DialogResult res = MessageBox.Show("If you toggle this option, the Asset SDK will download all localized files directly into your Novetus data, rather than into the Asset Cache. This means you won't be able to clear these files with the 'Clear Asset Cache' option in the Launcher.\n\nWould you like to continue with the option anyways?", "Asset Fixer - Permanent Localization Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            AssetLocalization_AssetLinks.Enabled = false;
            AssetLocalization_CustomFolderNameBox.Enabled = true;
        }
        else
        {
            AssetLocalization_AssetLinks.Enabled = true;
            AssetLocalization_CustomFolderNameBox.Enabled = false;
        }
    }

    private void AssetLocalization_AssetLinks_CheckedChanged(object sender, EventArgs e)
    {
        if (AssetLocalization_AssetLinks.Checked)
        {
            AssetLocalization_LocalizeButton.Text = AssetLocalization_LocalizeButton.Text.Replace("Localize", "Fix");
            AssetLocalization_LocalizePermanentlyBox.Enabled = false;
            URLSelection.Enabled = true;
            URLOverrideBox.Enabled = true;
        }
        else
        {
            AssetLocalization_LocalizeButton.Text = AssetLocalization_LocalizeButton.Text.Replace("Fix", "Localize");
            AssetLocalization_LocalizePermanentlyBox.Enabled = true;
            URLSelection.Enabled = false;
            URLOverrideBox.Enabled = false;
        }
    }

    void LocalizePermanentlyIfNeeded()
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

    #endregion
}
