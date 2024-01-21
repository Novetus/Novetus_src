#region Usings
using Ionic.Zip;
using Novetus.Core;
using System;
using System.Collections.Generic;
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

    public enum ModType
    {
        ModPackage,
        AddonScript
    }

    private ModMode globalMode;
    private ModType globalType;
    private OpenFileDialog openFileDialog1;
    private SaveFileDialog saveFileDialog1;
    private string installOutcome = "";
    private int fileListDisplay = 0;
    private CancellationTokenSource tokenSource;
    private int pastPercentage = 0;

    public ModManager(ModMode mode)
    {
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
                    FileName = "Select a mod .zip or addon *.lua file",
                    Filter = "Compressed zip files (*.zip)|*.zip|LUA Script (*.lua)|*.lua",
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
            try
            {
                globalType = (ModType)(openFileDialog1.FilterIndex - 1);

                if (globalType == ModType.ModPackage)
                {
                    MessageBox.Show("Your mod is loading. You will recieve a notification when it is installed. Please keep the launcher open. If the Console is open, you can see the installation progress.", "Novetus - Mod Loading");

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
                else if (globalType == ModType.AddonScript)
                {
                    try
                    {
                        IOSafe.File.Copy(openFileDialog1.FileName, GlobalPaths.AddonDir + @"\" + openFileDialog1.SafeFileName, false);

                        string AddonPath = GlobalPaths.AddonCoreDir + "\\" + GlobalPaths.AddonLoaderFileName;
                        var lines = File.ReadLines(AddonPath);
                        List<string> FileLines = lines.ToList();
                        for (var i = 0; i < FileLines.Count; i++)
                        {
                            if (FileLines[i].Contains("Addons"))
                            {
                                if (FileLines[i].Contains(Path.GetFileNameWithoutExtension(openFileDialog1.SafeFileName)))
                                {
                                    installOutcome = "Error: Script has already been added.";
                                    break;
                                }

                                string[] list = FileLines[i].Replace("Addons", "").Replace("=", "").Replace("{", "").Replace("}", "").Replace(" ", "").Split(',');
                                List<string> Addons = list.ToList();
                                Addons.Add("\"" + Path.GetFileNameWithoutExtension(openFileDialog1.SafeFileName) + "\"");
                                string newline = "Addons = {" + string.Join(", ", Addons) + "}";
                                FileLines[i] = newline;
                                File.WriteAllLines(AddonPath, FileLines.ToArray());
                                installOutcome = "Addon Script " + openFileDialog1.SafeFileName + " installed!";
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.LogExceptions(ex);
                        installOutcome = "Error: Script has already been added.";
                    }
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
            int intPercent = ConvertSafe.ToInt32Safe(percentage);

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

    public async Task CreateModPackage(string[] filesToPackage, string modName, string modAuthor, string modDesc)
    {
        if (globalMode == ModMode.ModInstallation)
            return;

        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string outputPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                string fullModName = modAuthor + " - " + modName;
                string outputSavePath = outputPath + @"\" + fullModName;

                if (!Directory.Exists(outputSavePath))
                {
                    Directory.CreateDirectory(outputSavePath);
                }

                List<string> info = new List<string>();
                info.Add(modDesc);
                info.Add("FILE PATHS:");
                foreach (string filepath in filesToPackage)
                {
                    info.Add(filepath.Replace(GlobalPaths.RootPath, ""));
                }

                File.WriteAllLines(outputSavePath + @"\" + fullModName + "_info.txt", info.ToArray());

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

                    IOSafe.File.Move(originalPath, destPath, true);

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

                //don't include the info file.
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
