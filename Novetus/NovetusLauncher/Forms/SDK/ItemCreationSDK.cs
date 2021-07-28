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
    private static bool Option1Required = false;
    private static bool Option2Required = false;
    private static bool RequiresIconForTexture = false;
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
            MessageBox.Show("You must assign an item name before you change the icon.", "Novetus Item Creation SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            Image icon1 = CustomizationFuncs.LoadImage(icon.ItemDir + "\\" + icon.ItemName.Replace(" ", "") + ".png", "");
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
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Hat Mesh", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("Uses Existing Hat Mesh");
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Hat Texture", true);
                Option2Path = "";
                Option2Required = true;
                ToggleGroup(CoordGroup, "Hat Attachment Point");
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HatTemplate.rbxm";
                FileDialogFilter1 = "*.mesh";
                FileDialogName1 = "Hat Mesh";
                FileDialogFilter2 = "*.png";
                FileDialogName2 = "Hat Texture";
                RequiresIconForTexture = false;
                break;
            case RobloxFileType.HeadNoCustomMesh:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "", false, false);
                Option1Path = "";
                Option1Required = false;
                ToggleHatMeshBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(MeshOptionsGroup, "Head Mesh Options");
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadNoCustomMeshTemplate.rbxm";
                RequiresIconForTexture = false;
                break;
            case RobloxFileType.Head:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Head Mesh", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Head Texture", true);
                Option2Path = "";
                Option2Required = true;
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadTemplate.rbxm";
                FileDialogFilter1 = "*.mesh";
                FileDialogName1 = "Head Mesh";
                FileDialogFilter2 = "*.png";
                FileDialogName2 = "Head Texture";
                RequiresIconForTexture = false;
                break;
            case RobloxFileType.Face:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Load the Item Icon to load a Face Texture.", false, false);
                Option1Path = "";
                Option1Required = false;
                ToggleHatMeshBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\FaceTemplate.rbxm";
                RequiresIconForTexture = true;
                break;
            case RobloxFileType.TShirt:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Load the Item Icon to load a T-Shirt Template.", false, false);
                Option1Path = "";
                Option1Required = false;
                ToggleHatMeshBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\TShirtTemplate.rbxm";
                RequiresIconForTexture = true;
                break;
            case RobloxFileType.Shirt:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Shirt Template", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\ShirtTemplate.rbxm";
                FileDialogFilter1 = "*.png";
                FileDialogName1 = "Shirt Template";
                RequiresIconForTexture = false;
                break;
            case RobloxFileType.Pants:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Pants Template", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\PantsTemplate.rbxm";
                FileDialogFilter1 = "*.png";
                FileDialogName1 = "Pants Template";
                RequiresIconForTexture = false;
                break;
            default:
                break;
        }
    }

    private void CreateItemButton_Click(object sender, EventArgs e)
    {
        if (!CheckItemRequirements())
            return;

        string ItemName = ItemNameBox.Text.Replace(" ", "");
        if (SDKFuncs.CreateItem(Template,
            type,
            ItemName,
            new string[] { Option1Path, Option2Path, Option1TextBox.Text, Option2TextBox.Text },
            new double[] { Convert.ToDouble(XBox.Value), Convert.ToDouble(YBox.Value), Convert.ToDouble(ZBox.Value) },
            new double[] { Convert.ToDouble(BevelBox.Value), Convert.ToDouble(RoundnessBox.Value), Convert.ToDouble(BulgeBox.Value) },
            DescBox.Text
            ))
        {
            DialogResult LaunchCharCustom = MessageBox.Show("The creation of your item, " + ItemNameBox.Text + ", is successful! Would you like to test your item out in Character Customization?", "Novetus Item Creation SDK", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (LaunchCharCustom == DialogResult.Yes)
            {
                switch (GlobalVars.UserConfiguration.LauncherStyle)
                {
                    case Settings.UIOptions.Style.Extended:
                        CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
                        ccustom.Show();
                        break;
                    case Settings.UIOptions.Style.Compact:
                        CharacterCustomizationCompact ccustom2 = new CharacterCustomizationCompact();
                        ccustom2.Show();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Option1BrowseButton_Click(object sender, EventArgs e)
    {
        Option1Path = LoadAsset(FileDialogName1, FileDialogFilter1);
        Option1TextBox.Text = Path.GetFileName(Option1Path);
    }

    private void Option2BrowseButton_Click(object sender, EventArgs e)
    {
        Option2Path = LoadAsset(FileDialogName2, FileDialogFilter2);
        Option2TextBox.Text = Path.GetFileName(Option2Path);
    }

    private void UsesHatMeshBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Option1Path = "";

        if (UsesHatMeshBox.SelectedItem.ToString() != "None")
        {
            Option1TextBox.Text = UsesHatMeshBox.Text;
        }
        else
        {
            Option1TextBox.Text = "";
        }
    }
    #endregion

    #region Functions
    private void ToggleOptionSet(Label label, TextBox textbox, Button button, string labelText, bool browseButton, bool enable = true)
    {
        label.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        textbox.ReadOnly = !enable;
        textbox.Text = "";
        button.Enabled = browseButton;
        ItemIcon.Image = CustomizationFuncs.LoadImage("", "");
    }

    private void ToggleGroup(GroupBox groupbox, string labelText, bool enable = true)
    {
        groupbox.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        groupbox.Enabled = enable;
        ItemIcon.Image = CustomizationFuncs.LoadImage("", "");
    }

    private void ToggleHatMeshBox(string labelText, bool enable = true)
    {
        UsesHatMeshLabel.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        UsesHatMeshBox.Enabled = enable;

        if (enable && Directory.Exists(GlobalPaths.hatdirFonts))
        {
            UsesHatMeshBox.Items.Add("None");
            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.hatdirFonts);
            FileInfo[] Files = dinfo.GetFiles("*.mesh");
            foreach (FileInfo file in Files)
            {
                if (file.Name.Equals(string.Empty))
                {
                    continue;
                }

                UsesHatMeshBox.Items.Add(file.Name);
            }

            UsesHatMeshBox.SelectedItem = "None";
        }
        else
        {
            UsesHatMeshBox.Items.Clear();
        }
    }

    private string LoadAsset(string assetName, string assetFilter)
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select a " + assetName + " file",
            Filter = assetName + " (" + assetFilter + ")|" + assetFilter,
            Title = "Open " + assetName
        };

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            return openFileDialog1.FileName;
        }
        else
        {
            return "";
        }
    }

    private bool CheckItemRequirements()
    {
        string msgboxtext = "The Item Creation SDK has experienced an error: You are missing some requirements:\n";
        bool passed = true;

        if (string.IsNullOrWhiteSpace(ItemNameBox.Text) && ItemIcon.Image == null || string.IsNullOrWhiteSpace(ItemNameBox.Text) || ItemIcon.Image == null)
        {
            msgboxtext += "\n - You must assign an item name and/or icon.";

            if (RequiresIconForTexture && ItemIcon.Image == null)
            {
                msgboxtext += " This item type requires that you must select an Icon to select a Template or Texture.";
            }

            passed = false;
        }

        if (Option1Required && string.IsNullOrWhiteSpace(Option1TextBox.Text))
        {
            msgboxtext += "\n - You must assign a " + Option1Label.Text + ".";
            passed = false;
        }

        if (Option2Required && string.IsNullOrWhiteSpace(Option2TextBox.Text))
        {
            msgboxtext += "\n - You must assign a " + Option2Label.Text + ".";
            passed = false;
        }

        if (!passed)
        {
            msgboxtext += "\n\nThese requirements must be fullfiled before the item can be created.";
            MessageBox.Show(msgboxtext, "Novetus Item Creation SDK", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return passed;
    }
    #endregion
}
#endregion
