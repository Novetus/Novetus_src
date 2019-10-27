using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Linq;

public class AddonLoader
{
    private readonly OpenFileDialog openFileDialog1;
    private string installOutcome = "";
    private int fileListDisplay = 0;

    public AddonLoader()
    {
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

    public void LoadAddon()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                int filecount = 0;
                string filelist = "";

                using (Stream str = openFileDialog1.OpenFile())
                {
                    using (ZipArchive archive = new ZipArchive(str))
                    {
                        filecount = archive.Entries.Count;

                        ZipArchiveEntry[] entries = archive.Entries.Take(fileListDisplay).ToArray();

                        foreach (ZipArchiveEntry entry in entries)
                        {
                            filelist += entry.FullName + Environment.NewLine;
                        }
                        archive.ExtractToDirectory(GlobalVars.BasePath, true);
                    }
                }

                if (filecount > fileListDisplay)
                {
                    installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist + Environment.NewLine + "and " + (filecount - fileListDisplay) + " more files!";
                }
                else
                {
                    installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist;
                }
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing addon: " + ex.Message;
            }
        }
    }
}
