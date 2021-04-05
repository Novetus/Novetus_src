#region Usings
using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

#region Addon Loader
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
                StringBuilder filelistbuilder = new StringBuilder();

                using (Stream str = openFileDialog1.OpenFile())
                {
                    using (var zipFile = ZipFile.Read(str))
                    {
                        ZipEntry[] entries = zipFile.Entries.ToArray();

                        foreach (ZipEntry entry in entries)
                        {
                            filelistbuilder.Append(!entry.IsDirectory ? (entry.FileName + " (" + entry.UncompressedSize + " KB)" + Environment.NewLine) : "");

                            if (!entry.IsDirectory)
                            {
                                filecount++;
                            }
                        }

                        zipFile.ExtractAll(GlobalPaths.BasePath, ExtractExistingFileAction.OverwriteSilently);
                    }
                }

                string filelist = filelistbuilder.ToString();

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
#endregion
