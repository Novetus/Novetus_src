using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Linq;

public class IconLoader
{
    private OpenFileDialog openFileDialog1;
    public string installOutcome = "";
    private string extradir = GlobalVars.CustomPlayerDir + "\\custom";

    public IconLoader()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an icon .png file",
            Filter = "Portable Network Graphics image (*.png)|*.png",
            Title = "Open icon .png"
        };
    }

    public void LoadImage()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                using (Stream str = openFileDialog1.OpenFile())
                {
                    CopyStream(openFileDialog1.FileName, extradir + "\\icons\\" + GlobalVars.PlayerName + ".png");
                }

                installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing icon: " + ex.Message;
            }
        }
    }

    public static void CopyStream(string inputFilePath, string outputFilePath)
    {
        int bufferSize = 1024 * 1024;

        using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        //using (FileStream fs = File.Open(<file-path>, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.ReadWrite);
            fileStream.SetLength(fs.Length);
            int bytesRead = -1;
            byte[] bytes = new byte[bufferSize];

            while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
            {
                fileStream.Write(bytes, 0, bytesRead);
            }
        }
    }
}
