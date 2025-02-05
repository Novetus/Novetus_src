#region Usings
using Novetus.Core;
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

        parent.partColorID = ConvertSafe.ToInt32Safe(colorMenu.Items[selectedIndex].Tag);
        parent.partColorLabel.Text = parent.partColorID.ToString();
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

        PartColor.AddPartColorsToListView(GlobalVars.PartColorList, colorMenu, 48, true);
        CenterToScreen();
    }
    #endregion
}
#endregion
