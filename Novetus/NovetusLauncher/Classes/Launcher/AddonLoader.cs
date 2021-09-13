#region Usings
using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
#endregion

#region Addon Loader
public class AddonLoader
{
    private readonly OpenFileDialog openFileDialog1;
    private string installOutcome = "";
    private int fileListDisplay = 0;
    private RichTextBox consoleBox;
    private CancellationTokenSource tokenSource;
    private int pastPercentage = 0;

    public AddonLoader(RichTextBox box)
    {
        Application.ApplicationExit += new EventHandler(OnApplicationExit);
        consoleBox = box;
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an addon .zip file",
            Filter = "Compressed zip files (*.zip)|*.zip",
            Title = "Open addon .zip"
        };
    }

    public void setInstallOutcome(string text)
    {
        installOutcome = text;
    }

    public string getInstallOutcome()
    {
        return installOutcome;
    }

    public void setFileListDisplay(int number)
    {
        fileListDisplay = number;
    }

    public async Task LoadAddon()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            MessageBox.Show("Your addon is loading. You will recieve a notification when it is installed. Please keep the launcher open. You can see the installation progress in the Console.", "Novetus - Addon Loading");

            try
            {
                int filecount = 0;
                StringBuilder filelistbuilder = new StringBuilder();
                StringBuilder filelistcutdown = new StringBuilder();

                using (Stream str = openFileDialog1.OpenFile())
                {
                    using (var zipFile = ZipFile.Read(str))
                    {
                        zipFile.ExtractProgress += ExtractProgress;
                        ZipEntry[] entries = zipFile.Entries.ToArray();

                        foreach (ZipEntry entry in entries)
                        {
                            filelistbuilder.Append(!entry.IsDirectory ? (entry.FileName + " (" + entry.UncompressedSize + " KB)" + Environment.NewLine) : "");

                            if (filecount < fileListDisplay)
                            {
                                filelistcutdown.Append(!entry.IsDirectory ? (entry.FileName + " (" + entry.UncompressedSize + " KB)" + Environment.NewLine) : "");
                            }

                            if (!entry.IsDirectory)
                            {
                                filecount++;
                            }
                        }

                        tokenSource = new CancellationTokenSource();
                        var token = tokenSource.Token;
                        await Task.Factory.StartNew(() => zipFile.ExtractAll(GlobalPaths.BasePath, ExtractExistingFileAction.OverwriteSilently), token);
                        zipFile.Dispose();
                    }
                }

                string filelist = filelistbuilder.ToString();

                if (filecount > fileListDisplay)
                {
                    installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelistcutdown + Environment.NewLine + "and " + (filecount - fileListDisplay) + " more files!";
                }
                else
                {
                    installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist;
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
                installOutcome = "Error when installing addon: " + ex.Message;
            }
        }
    }

    //https://stackoverflow.com/questions/38948801/dotnetzip-display-progress-of-extraction
    void ExtractProgress(object sender, ExtractProgressEventArgs e)
    {
        if (e.EventType == ZipProgressEventType.Extracting_EntryBytesWritten)
        {
            double percentage = Math.Round(e.BytesTransferred / (0.01 * e.TotalBytesToTransfer), 2);
            int intPercent = Convert.ToInt32(percentage);

            if (intPercent % 25 == 0 && pastPercentage != intPercent)
            {
                GlobalFuncs.ConsolePrint("AddonLoader - Extracting: "
                    + e.CurrentEntry.FileName + ". Progress: "
                    + e.BytesTransferred + "/" + e.TotalBytesToTransfer
                    + " (" + intPercent + "%)", 3, consoleBox, true);

                pastPercentage = intPercent;
            }
        }
        else if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
        {
            GlobalFuncs.ConsolePrint("AddonLoader - Extracting: " + e.CurrentEntry.FileName, 3, consoleBox);
        }
    }

    public void OnApplicationExit(object sender, EventArgs e)
    {
        tokenSource.Cancel();
    }
}
#endregion
