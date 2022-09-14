#region Usings
using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion
#region Mod Manager
public class ModManager
{
    public enum ModMode
    {
        ModInstallation,
        ModCreation
    }

    private ModMode globalMode;
    private OpenFileDialog openFileDialog1;
    private SaveFileDialog saveFileDialog1;
    private string installOutcome = "";
    private int fileListDisplay = 0;
    private RichTextBox consoleBox;
    private CancellationTokenSource tokenSource;
    private int pastPercentage = 0;

    public ModManager(ModMode mode)
    {
        Init(mode);
    }

    public ModManager(ModMode mode, RichTextBox box)
    {
        consoleBox = box;
        Init(mode);
    }

    public void Init(ModMode mode)
    {
        Application.ApplicationExit += new EventHandler(OnApplicationExit);
        globalMode = mode;

        switch (globalMode)
        {
            case ModMode.ModCreation:
                saveFileDialog1 = new SaveFileDialog()
                {
                    FileName = "Specify the place where you will save your .zip file",
                    Filter = "Compressed zip files (*.zip)|*.zip",
                    Title = "Save mod .zip"
                };
                break;
            case ModMode.ModInstallation:
            default:
                openFileDialog1 = new OpenFileDialog()
                {
                    FileName = "Select a mod .zip file",
                    Filter = "Compressed zip files (*.zip)|*.zip",
                    Title = "Open mod .zip"
                };
                break;
        }
    }

    public void setOutcome(string text)
    {
        installOutcome = text;
    }

    public string getOutcome()
    {
        return installOutcome;
    }

    public void setFileListDisplay(int number)
    {
        fileListDisplay = number;
    }

    public async Task LoadMod()
    {
        if (globalMode == ModMode.ModCreation)
            return;

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            MessageBox.Show("Your mod is loading. You will recieve a notification when it is installed. Please keep the launcher open. You can see the installation progress in the Console.", "Novetus - Mod Loading");

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
                    installOutcome = "Mod " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelistcutdown + Environment.NewLine + "and " + (filecount - fileListDisplay) + " more files!";
                }
                else
                {
                    installOutcome = "Mod " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist;
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                installOutcome = "Error when installing mod: " + ex.Message;
            }
        }
    }

    //https://stackoverflow.com/questions/38948801/dotnetzip-display-progress-of-extraction
    void ExtractProgress(object sender, ExtractProgressEventArgs e)
    {
        if (globalMode == ModMode.ModCreation)
            return;

        if (e.EventType == ZipProgressEventType.Extracting_EntryBytesWritten)
        {
            double percentage = Math.Round(e.BytesTransferred / (0.01 * e.TotalBytesToTransfer), 2);
            int intPercent = Convert.ToInt32(percentage);

            if (intPercent % 25 == 0 && pastPercentage != intPercent)
            {
                Util.ConsolePrint("ModManager - Extracting: "
                    + e.CurrentEntry.FileName + ". Progress: "
                    + e.BytesTransferred + "/" + e.TotalBytesToTransfer
                    + " (" + intPercent + "%)", 3, true);

                pastPercentage = intPercent;
            }
        }
        else if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
        {
            Util.ConsolePrint("ModManager - Extracting: " + e.CurrentEntry.FileName, 3);
        }
    }

    public void OnApplicationExit(object sender, EventArgs e)
    {
        tokenSource.Cancel();
    }

    public async Task CreateModPackage(string[] filesToPackage, string modName)
    {
        if (globalMode == ModMode.ModInstallation)
            return;

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string outputSavePath = Path.GetDirectoryName(saveFileDialog1.FileName) + @"\" + modName;

                int filecount = 0;

                foreach (string file in filesToPackage)
                {
                    string originalPath = GlobalPaths.RootPath + file;
                    string destPath = outputSavePath + file;

                    FileInfo fileInfo = new FileInfo(destPath);

                    if (!Directory.Exists(fileInfo.DirectoryName))
                    {
                        Directory.CreateDirectory(fileInfo.DirectoryName);
                    }

                    Util.FixedFileMove(originalPath, destPath, true);

                    ++filecount;
                }

                ZipFile zip = new ZipFile(outputSavePath + ".zip");

                DirectoryInfo dinfo = new DirectoryInfo(outputSavePath);
                FileInfo[] Files = dinfo.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo file in Files)
                {
                    try
                    {
                        zip.AddDirectory(outputSavePath, "");
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                }

                zip.ParallelDeflateThreshold = -1;
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                await Task.Factory.StartNew(() => zip.Save(), token);
                zip.Dispose();

                Directory.Delete(outputSavePath, true);

                installOutcome = filecount + " files have been successfully moved and compressed into " + outputSavePath + ".zip!";
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                installOutcome = "Error when creating mod: " + ex.Message;
            }
        }
    }
}
#endregion
