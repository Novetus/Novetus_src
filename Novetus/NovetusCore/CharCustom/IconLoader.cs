#region Usings
using System;
using System.IO;
using System.Windows.Forms;
#endregion

#region Icon Loader

public class IconLoader
{
    private OpenFileDialog openFileDialog1;
    private string installOutcome = "";
    public bool CopyToItemDir = false;
    public string ItemDir = "";
    public string ItemName = "";
    public string ItemPath = "";

    public IconLoader()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an icon .png file",
            Filter = "Portable Network Graphics image (*.png)|*.png",
            Title = "Open icon .png"
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

    public void LoadImage()
    {
        string ItemNameFixed = ItemName.Replace(" ", "");
        string dir = CopyToItemDir ? ItemDir + "\\" + ItemNameFixed : GlobalPaths.extradir + "\\icons\\" + GlobalVars.UserConfiguration.PlayerName;

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                GlobalFuncs.FixedFileCopy(openFileDialog1.FileName, dir + ".png", true);

                if (CopyToItemDir)
                {
                    ItemPath = ItemDir + "\\" + ItemNameFixed + ".png";
                }

                installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing icon: " + ex.Message;
#if URI || LAUNCHER || CMD
                GlobalFuncs.LogExceptions(ex);
#endif
            }
        }
    }
}
#endregion