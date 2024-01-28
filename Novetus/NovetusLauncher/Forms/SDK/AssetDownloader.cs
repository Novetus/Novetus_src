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

public partial class AssetDownloader : Form
{
    #region Private Variables
    public ContentProvider[] contentProviders;
    private string url = "";
    private bool isWebSite = false;
    private bool batchMode = false;
    private bool hasOverrideWarningOpenedOnce = false;
    private static int batchDownloadSize = 0;
    #endregion

    #region Constructor
    public AssetDownloader()
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

        //downloader
        AssetDownloader_LoadHelpMessage.Checked = GlobalVars.UserConfiguration.ReadSettingBool("DisabledAssetSDKHelp");
        Height = 193;
        CenterToScreen();
    }

    void AssetSDK_Close(object sender, CancelEventArgs e)
    {
    }

    private void URLSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetURL();
    }

    private void URLOverrideBox_Click(object sender, EventArgs e)
    {
        if (hasOverrideWarningOpenedOnce == false && !GlobalVars.UserConfiguration.ReadSettingBool("DisabledAssetSDKHelp"))
        {
            MessageBox.Show("By using the custom URL setting, you will override any selected entry in the default URL list. Keep this in mind before downloading anything with this option.\n\nAlso, the URL must be a asset url with 'asset/?id=' at the end of it in order for the Asset Downloader to work smoothly.", "Asset Downloader - URL Override Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    #region Asset Downloader
    public static void StartItemDownload(string name, string url, string id, int ver, bool iswebsite)
    {
        try
        {
            string version = ((ver != 0) && (!iswebsite)) ? "&version=" + ver : "";
            string fullURL = url + id + version;

            if (!iswebsite)
            {
                if (!GlobalVars.UserConfiguration.ReadSettingBool("DisabledAssetSDKHelp"))
                {
                    string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - Roblox Model/Item\n.rbxl - Roblox Place\n.mesh - Roblox Mesh\n.png - Texture/Icon\n.wav - Sound\n.lua - Lua Script";
                    MessageBox.Show(helptext, "Asset Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Downloader download = new Downloader(fullURL, name, "Roblox Model (*.rbxm)|*.rbxm|Roblox Place (*.rbxl) |*.rbxl|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav|Lua Script (*.lua)|*.lua");

                try
                {
                    download.InitDownload();
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    MessageBox.Show("Error: Unable to download the file. " + ex.Message, "Asset Downloader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
                {
                    MessageBoxIcon boxicon = MessageBoxIcon.Information;

                    if (download.getDownloadOutcome().Contains("Error"))
                    {
                        Util.ConsolePrint("Download Outcome: " + download.getDownloadOutcome(), 2);
                        boxicon = MessageBoxIcon.Error;
                    }
                    else
                    {
                        Util.ConsolePrint("Download Outcome: " + download.getDownloadOutcome(), 3);
                    }

                    MessageBox.Show(download.getDownloadOutcome(), "Asset Downloader - Download Completed", MessageBoxButtons.OK, boxicon);
                }
            }
            else
            {
                Process.Start(fullURL);
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
            MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.", "Asset Downloader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Util.LogExceptions(ex);
                    noErrors = false;
                }

                if (noErrors)
                {
                    batchDownloadSize += download.downloadSize;
                }
            }
            else
            {
                Process.Start(fullURL);
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
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
                ConvertSafe.ToInt32Safe(AssetDownloader_AssetVersionSelector.Value),
                isWebSite);
        }
        else
        {
            if (isWebSite)
            {
                DialogResult siteQuestion = MessageBox.Show("The Batch Downloader is not made for loading websites.", "Asset Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            try
            {
                if (!GlobalVars.UserConfiguration.ReadSettingBool("DisabledAssetSDKHelp"))
                {
                    string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - Roblox Model/Item\n.rbxl - Roblox Place\n.mesh - Roblox Mesh\n.png - Texture/Icon\n.wav - Sound\n.lua - Lua Script";
                    MessageBox.Show(helptext, "Asset Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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
                            ConvertSafe.ToInt32Safe(linesplit[2]),
                            isWebSite, basepath);

                        if (!noErrors)
                        {
                            --lineCount;
                        }
                    }

                    AssetDownloaderBatch_Status.Visible = false;

                    string extraText = (lines.Count() != lineCount) ? "\n" + (lines.Count() - lineCount) + " errors were detected during the download. Make sure your IDs and links are valid." : "";

                    MessageBox.Show("Batch download complete! " + lineCount + " items downloaded! " + Util.SizeSuffix(ConvertSafe.ToInt64Safe(batchDownloadSize), 2) + " written (" + batchDownloadSize + " bytes)!" + extraText, "Asset Downloader - Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);

                MessageBox.Show("Unable to batch download files. Error:" + ex.Message + "\n Make sure your items are set up properly.", "Asset Downloader - Unable to batch download files.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void AssetDownloader_LoadHelpMessage_CheckedChanged(object sender, EventArgs e)
    {
        GlobalVars.UserConfiguration.SaveSettingBool("DisabledAssetSDKHelp", AssetDownloader_LoadHelpMessage.Checked);
    }
    private void AssetDownloader_BatchMode_CheckedChanged(object sender, EventArgs e)
    {
        batchMode = AssetDownloader_BatchMode.Checked;

        if (batchMode)
        {
            Height = 454;
            AssetDownloaderBatch_BatchIDBox.Enabled = true;
            AssetDownloaderBatch_Note.Visible = true;
            AssetDownloader_AssetIDBox.Enabled = false;
            AssetDownloader_AssetNameBox.Enabled = false;
            AssetDownloader_AssetVersionSelector.Enabled = false;
        }
        else
        {
            Height = 193;
            AssetDownloaderBatch_BatchIDBox.Enabled = false;
            AssetDownloaderBatch_Note.Visible = false;
            AssetDownloader_AssetIDBox.Enabled = true;
            AssetDownloader_AssetNameBox.Enabled = true;
            AssetDownloader_AssetVersionSelector.Enabled = true;
        }
    }
    #endregion

    #endregion
}
