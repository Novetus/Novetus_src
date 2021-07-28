#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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

        GlobalFuncs.CreateAssetCacheDirectories();
    }

    void AssetSDK_Close(object sender, CancelEventArgs e)
    {
        //asset localizer
        AssetLocalization_BackgroundWorker.CancelAsync();
    }
    #endregion

    #region Asset Downloader

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
            SDKFuncs.StartItemDownload(
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
                FileName = ".",
                //"Compressed zip files (*.zip)|*.zip|All files (*.*)|*.*"
                Filter = "Roblox Model(*.rbxm) | *.rbxm | Roblox Mesh(*.mesh) | *.mesh | PNG Image(*.png) | *.png | WAV Sound(*.wav) | *.wav",
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
                    SDKFuncs.StartItemBatchDownload(
                        linesplit[0] + extension,
                        url,
                        linesplit[1],
                        Convert.ToInt32(AssetDownloader_AssetVersionSelector.Value),
                        isWebSite, basepath);
                }

                AssetDownloaderBatch_Status.Visible = false;

                MessageBox.Show("Batch download complete! " + lines.Count() + " items downloaded!", "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
        else
        {
            AssetDownloaderBatch_BatchIDBox.Enabled = false;
            AssetDownloaderBatch_Note.Visible = false;
            AssetDownloader_AssetIDBox.Enabled = true;
            AssetDownloader_AssetNameBox.Enabled = true;
        }
    }

    #endregion

    #region Asset Localizer
    private void AssetLocalization_AssetTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentType = SDKFuncs.SelectROBLOXFileType(AssetLocalization_AssetTypeBox.SelectedIndex);
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
        OpenFileDialog robloxFileDialog = SDKFuncs.LoadROBLOXFileDialog(currentType);

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
        SDKFuncs.LocalizeAsset(currentType, worker, path, name, meshname);
    }

    // This event handler updates the progress.
    private void AssetLocalization_BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        AssetLocalization_StatusText.Text = SDKFuncs.GetProgressString(currentType, e.ProgressPercentage);
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
                break;
            default:
                AssetLocalization_StatusText.Text = "Done!";
                break;
        }
    }

    private void URLOverrideBox_Click(object sender, EventArgs e)
    {
        if (hasOverrideWarningOpenedOnce == false)
        {
            MessageBox.Show("By using the custom URL setting, you will override any selected entry in the default URL list. Keep this in mind before downloading anything with this option.\n\nAlso, the URL must be a asset url with 'asset/?id=' at the end of it in order for the Asset Downloader to work smoothly.", "Novetus Asset SDK | URL Override Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            hasOverrideWarningOpenedOnce = true;
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
        MessageBox.Show("File " + properName + " created!");
    }
    #endregion

    #endregion
}
