#region Usings
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
#endregion

#region Item Creation SDK - Color Menu
public partial class ItemCreationSDKColorMenu : Form
{
    #region Variables
    private ItemCreationSDK parent;
    private ImageList ColorImageList;
    private PartColor[] PartColorList;
    private List<PartColor> PartColorListConv;
    public bool closeOnLaunch = false;
    #endregion

    #region Constructor
    public ItemCreationSDKColorMenu(ItemCreationSDK par)
    {
        InitializeComponent();
        InitColors();
        closeOnLaunch = !InitColors();
        parent = par;
    }

    public bool InitColors()
    {
        try
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
            {
                PartColorList = PartColorLoader.GetPartColors();
                PartColorListConv = new List<PartColor>();
                PartColorListConv.AddRange(PartColorList);
                return true;
            }
            else
            {
                goto Failure;
            }
        }
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
            goto Failure;
        }

    Failure:
        MessageBox.Show("The part colors cannot be loaded. The color menu will now close.", "Novetus - Cannot load part colors.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
    #endregion

    #region Form Events
    private void colorMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = 0;

        if (colorMenu.SelectedIndices.Count > 0)
        {
            selectedIndex = colorMenu.SelectedIndices[0];
        }
        else
        {
            return;
        }

        parent.partColorID = Convert.ToInt32(colorMenu.Items[selectedIndex].Tag);
        parent.partColorLabel.Text = parent.partColorID.ToString();
        Close();
    }

    private void ItemCreationSDKColorMenu_Load(object sender, EventArgs e)
    {
        if (closeOnLaunch)
        {
            Close();
            return;
        }

        int imgsize = 32;
        ColorImageList = new ImageList();
        ColorImageList.ImageSize = new Size(imgsize, imgsize);
        ColorImageList.ColorDepth = ColorDepth.Depth32Bit;
        colorMenu.LargeImageList = ColorImageList;
        colorMenu.SmallImageList = ColorImageList;

        foreach (var item in PartColorList)
        {
            var lvi = new ListViewItem(item.ColorName);
            lvi.Tag = item.ColorID;

            Bitmap Bmp = new Bitmap(imgsize, imgsize, PixelFormat.Format32bppArgb);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(item.ColorObject))
            {
                gfx.FillRectangle(brush, 0, 0, imgsize, imgsize);
            }

            ColorImageList.Images.Add(item.ColorName, Bmp);
            lvi.ImageIndex = ColorImageList.Images.IndexOfKey(item.ColorName);
            colorMenu.Items.Add(lvi);
        }

        CenterToScreen();
    }
    #endregion
}
#endregion
