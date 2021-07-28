#region Usings
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

#region Item Creation SDK
public partial class ItemCreationSDK : Form
{
    #region Variables
    private static RobloxFileType type;
    private static string Template = "";
    private static string Option1Path = "";
    private static string Option2Path = "";
    private OpenFileDialog openFileDialog1;
    private static string FileDialogFilter1 = "";
    private static string FileDialogName1 = "";
    private static string FileDialogFilter2 = "";
    private static string FileDialogName2 = "";
    #endregion

    #region Constructor
    public ItemCreationSDK()
    {
        InitializeComponent();
    }
    #endregion

    #region Form Events
    private void ItemCreationSDK_Load(object sender, EventArgs e)
    {
        ItemTypeListBox.SelectedItem = "Hat";
    }

    private void BrowseImageButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ItemNameBox.Text))
        {
            //message box here.
            return;
        }
        else
        {
            IconLoader icon = new IconLoader();
            icon.CopyToItemDir = true;
            icon.ItemDir = SDKFuncs.GetPathForType(type);
            icon.ItemName = ItemNameBox.Text;
            try
            {
                icon.LoadImage();
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrWhiteSpace(icon.getInstallOutcome()))
            {
                MessageBox.Show(icon.getInstallOutcome());
            }

            Image icon1 = CustomizationFuncs.LoadImage(icon.ItemDir + "\\" + icon.ItemName.Replace(" ", "") + ".png", GlobalPaths.extradir + "\\NoExtra.png");
            ItemIcon.Image = icon1;

            if (type == RobloxFileType.TShirt || type == RobloxFileType.Face)
            {
                Option1Path = icon.ItemPath;
                Option1TextBox.Text = Path.GetFileName(Option1Path);
            }
        }
    }
    private void ItemTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        type = SDKFuncs.GetTypeForInt(ItemTypeListBox.SelectedIndex);

        switch (type)
        {
            case RobloxFileType.Hat:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Hat Mesh", Option1Path, true);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Hat Texture", Option2Path, true);
                ToggleGroup(CoordGroup, "Hat Attachment Point");
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HatTemplate.rbxm";
                FileDialogFilter1 = "*.mesh";
                FileDialogName1 = "Hat Mesh";
                FileDialogFilter2 = "*.png";
                FileDialogName2 = "Hat Texture";
                break;
            case RobloxFileType.HeadNoCustomMesh:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "", Option1Path, false, false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", Option2Path, false, false);
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(MeshOptionsGroup, "Head Mesh Options");
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadNoCustomMeshTemplate.rbxm";
                break;
            case RobloxFileType.Head:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Head Mesh", Option1Path, true);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Head Texture", Option2Path, true);
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadTemplate.rbxm";
                break;
            case RobloxFileType.Face:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Load the Item Icon to load a T-Shirt Template.", Option1Path, false, false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", Option2Path, false, false);
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\FaceTemplate.rbxm";
                break;
            case RobloxFileType.TShirt:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Load the Item Icon to load a T-Shirt Template.", Option1Path, false, false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", Option2Path, false, false);
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\TShirtTemplate.rbxm";
                break;
            case RobloxFileType.Shirt:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Shirt Template", Option1Path, true);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", Option2Path, false, false);
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\ShirtTemplate.rbxm";
                break;
            case RobloxFileType.Pants:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Pants Template", Option1Path, true);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", Option2Path, false, false);
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\PantsTemplate.rbxm";
                break;
            default:
                break;
        }
    }

    private void CreateItemButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ItemNameBox.Text) && ItemIcon.Image == null || string.IsNullOrWhiteSpace(ItemNameBox.Text) || ItemIcon.Image == null)
        {
            //message box here.
            return;
        }
        else
        {
            ItemNameBox.Text = ItemNameBox.Text.Replace(" ", "");
            SDKFuncs.CreateItem(Template,
                type,
                ItemNameBox.Text,
                new string[] { Option1Path, Option2Path, Option1TextBox.Text, Option2TextBox.Text },
                new double[] { Convert.ToDouble(XBox.Value), Convert.ToDouble(YBox.Value), Convert.ToDouble(ZBox.Value) },
                new double[] { Convert.ToDouble(BevelBox.Value), Convert.ToDouble(RoundnessBox.Value), Convert.ToDouble(BulgeBox.Value) },
                DescBox.Text
                );
        }
    }

    private void Option1BrowseButton_Click(object sender, EventArgs e)
    {
        LoadAsset(FileDialogName1, FileDialogFilter1, Option1Path, Option1TextBox);
    }

    private void Option2BrowseButton_Click(object sender, EventArgs e)
    {
        LoadAsset(FileDialogName2, FileDialogFilter2, Option2Path, Option2TextBox);
    }
    #endregion

    #region Functions
    private void ToggleOptionSet(Label label, TextBox textbox, Button button, string labelText, string Path, bool browseButton, bool enable = true)
    {
        label.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        textbox.ReadOnly = !enable;
        textbox.Text = "";
        button.Enabled = browseButton;
        ItemIcon.Image = null;
        Path = "";
    }

    private void ToggleGroup(GroupBox groupbox, string labelText, bool enable = true)
    {
        groupbox.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        groupbox.Enabled = enable;
        ItemIcon.Image = null;
    }

    private void LoadAsset(string assetName, string assetFilter, string optionPath, TextBox optionTextBox)
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select a " + assetName + " file",
            Filter = assetName + " (" + assetFilter + ")|" + assetFilter,
            Title = "Open " + assetName
        };

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            MessageBox.Show(openFileDialog1.FileName);
            optionPath = openFileDialog1.FileName;
            optionTextBox.Text = Path.GetFileName(optionPath);
        }
    }
    #endregion
}
#endregion
