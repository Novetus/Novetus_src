using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

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
                    using (Stream output = new FileStream(extradir + "\\icons\\" + GlobalVars.PlayerName + ".png", FileMode.Create))
                    {
                        byte[] buffer = new byte[32 * 1024];
                        int read;

                        while ((read = str.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, read);
                        }
                    }

                    str.Close();
                }

                installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing icon: " + ex.Message;
            }
        }
    }
}
