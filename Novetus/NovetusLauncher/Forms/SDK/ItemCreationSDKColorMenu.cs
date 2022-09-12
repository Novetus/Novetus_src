#region Usings
using System;
using System.Windows.Forms;
#endregion

#region Item Creation SDK - Color Menu
public partial class ItemCreationSDKColorMenu : Form
{
    #region Variables
    private ItemCreationSDK parent;
    public bool closeOnLaunch = false;
    #endregion

    #region Constructor
    public ItemCreationSDKColorMenu(ItemCreationSDK par)
    {
        InitializeComponent();
        parent = par;
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
#pragma warning disable CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
        parent.partColorLabel.Text = parent.partColorID.ToString();
#pragma warning restore CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
        Close();
    }

    private void ItemCreationSDKColorMenu_Load(object sender, EventArgs e)
    {
        if (FileManagement.HasColorsChanged())
        {
            GlobalVars.ColorsLoaded = FileManagement.InitColors();
            closeOnLaunch = !GlobalVars.ColorsLoaded;
        }

        if (closeOnLaunch)
        {
            MessageBox.Show("The part colors cannot be loaded. The part colors menu will now close.", "Novetus - Cannot load part colors.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
            return;
        }

        PartColorLoader.AddPartColorsToListView(GlobalVars.PartColorList, colorMenu, 48, true);
        CenterToScreen();
    }
    #endregion
}
#endregion
