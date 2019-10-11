using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Linq;

public class AddonLoader
{
    private OpenFileDialog openFileDialog1;
    public string installOutcome = "";
    public int fileListDisplay = 0;

    public AddonLoader()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an addon .zip file",
            Filter = "Compressed zip files (*.zip)|*.zip",
            Title = "Open addon .zip"
        };
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

    void CopyStream(Stream source, Stream dest)
    {
        int n;
        var buf = new byte[2048];
        while ((n = source.Read(buf, 0, buf.Length)) > 0)
        {
            dest.Write(buf, 0, n);
        }
    }
}
