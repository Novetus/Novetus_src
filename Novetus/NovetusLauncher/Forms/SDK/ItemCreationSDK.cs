#region Usings
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
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
        MeshTypeBox.SelectedItem = "BlockMesh";
        SpecialMeshTypeBox.SelectedItem = "Head";
    }

    private void BrowseImageButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ItemNameBox.Text))
        {
            MessageBox.Show("You must assign an item name before you change the icon.", "Novetus Item Creation SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            string previconpath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".png";

            if (File.Exists(previconpath))
            {
               DialogResult result = MessageBox.Show("An icon with this item's name already exists. Would you like to replace it?", "Novetus Item Creation SDK - Icon already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               if (result == DialogResult.No)
               {
                    return;
               }
            }

            IconLoader icon = new IconLoader();
            icon.CopyToItemDir = true;
            icon.ItemDir = GetPathForType(type);
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
                MessageBox.Show(icon.getInstallOutcome(), "Novetus Item Creation SDK - Icon Copy Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Image icon1 = GlobalFuncs.LoadImage(icon.ItemDir + "\\" + icon.ItemName.Replace(" ", "") + ".png", "");
            ItemIcon.Image = icon1;

            if (type == RobloxFileType.TShirt || type == RobloxFileType.Face)
            {
                Option1Path = icon.ItemPath;
                if (Option1TextBox.ReadOnly) Option1TextBox.ReadOnly = false;
                Option1TextBox.Text = Path.GetFileName(Option1Path);
                if (!Option1TextBox.ReadOnly) Option1TextBox.ReadOnly = true;
            }
        }
    }

    private void ItemNameBox_TextChanged(object sender, EventArgs e)
    {
        UpdateWarnings();
    }

    private void ItemTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itempath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".png";
        string previconpath = itempath + ".png";
        string rbxmpath = itempath + ".rbxm";

        if (File.Exists(previconpath) && !File.Exists(rbxmpath))
        {
            GlobalFuncs.FixedFileDelete(previconpath);
        }

        type = GetTypeForInt(ItemTypeListBox.SelectedIndex);

        switch (type)
        {
            case RobloxFileType.Hat:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Hat Mesh", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("Uses Existing Hat Mesh");
                ToggleHatTextureBox("Uses Existing Hat Texture");
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Hat Texture", true);
                Option2Path = "";
                Option2Required = true;
                ToggleGroup(CoordGroup, "Hat Attachment Point");
                ToggleGroup(CoordGroup2, "Hat Mesh Scale");
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
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(CoordGroup2, "", false);
                ToggleGroup(MeshOptionsGroup, "Head Mesh Options");
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadNoCustomMeshTemplate.rbxm";
                RequiresIconForTexture = false;
                break;
            case RobloxFileType.Head:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Head Mesh", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("", false);
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Head Texture", true);
                Option2Path = "";
                Option2Required = true;
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(CoordGroup2, "", false);
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
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(CoordGroup2, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\FaceTemplate.rbxm";
                RequiresIconForTexture = true;
                break;
            case RobloxFileType.TShirt:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Load the Item Icon to load a T-Shirt Template.", false, false);
                Option1Path = "";
                Option1Required = false;
                ToggleHatMeshBox("", false);
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(CoordGroup2, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\TShirtTemplate.rbxm";
                RequiresIconForTexture = true;
                break;
            case RobloxFileType.Shirt:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Shirt Template", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("", false);
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(CoordGroup2, "", false);
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
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(CoordGroup2, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                Template = GlobalPaths.ConfigDirTemplates + "\\PantsTemplate.rbxm";
                FileDialogFilter1 = "*.png";
                FileDialogName1 = "Pants Template";
                RequiresIconForTexture = false;
                break;
            default:
                break;
        }

        UpdateWarnings();
    }

    private void CreateItemButton_Click(object sender, EventArgs e)
    {
        if (!CheckItemRequirements())
            return;

        string ItemName = ItemNameBox.Text.Replace(" ", "");
        if (CreateItem(Template,
            type,
            ItemName,
            new string[] { Option1Path, Option2Path, Option1TextBox.Text, Option2TextBox.Text },
            new double[] { Convert.ToDouble(XBox.Value), Convert.ToDouble(YBox.Value), Convert.ToDouble(ZBox.Value) },
            new double[] { Convert.ToDouble(XBox360.Value), Convert.ToDouble(YBox2.Value), Convert.ToDouble(ZBox2.Value) },
            new object[] { Convert.ToDouble(BevelBox.Value), 
                Convert.ToDouble(RoundnessBox.Value), 
                Convert.ToDouble(BulgeBox.Value), 
                SpecialMeshTypeBox.SelectedIndex, 
                MeshTypeBox.SelectedItem.ToString(),
                Convert.ToInt32(LODXBox.Value),
                Convert.ToInt32(LODYBox.Value)},
            DescBox.Text
            ))
        {
            DialogResult LaunchCharCustom = MessageBox.Show("The creation of your item, " + ItemNameBox.Text + ", is successful! Would you like to test your item out in Character Customization?", "Novetus Item Creation SDK - Item Creation Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (LaunchCharCustom == DialogResult.Yes)
            {
                //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "CharacterCustomizationExtended" || 
                        frm.Name == "CharacterCustomizationCompact")
                    {
                        frm.Close();
                        break;
                    }
                }

                switch (GlobalVars.UserConfiguration.LauncherStyle)
                {
                    case Settings.Style.Extended:
                        CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
                        ccustom.Show();
                        break;
                    case Settings.Style.Compact:
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
            Option1TextBox.Enabled = false;
            Option1BrowseButton.Enabled = false;
        }
        else
        {
            Option1TextBox.Enabled = true;
            Option1BrowseButton.Enabled = true;
            Option1TextBox.Text = "";
        }
    }

    private void UsesHatTexBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Option2Path = "";

        if (UsesHatTexBox.SelectedItem.ToString() != "None")
        {
            Option2TextBox.Text = UsesHatTexBox.Text;
            Option2TextBox.Enabled = false;
            Option2BrowseButton.Enabled = false;
        }
        else
        {
            Option2TextBox.Enabled = true;
            Option2BrowseButton.Enabled = true;
            Option2TextBox.Text = "";
        }
    }
    #endregion

    #region Functions
    public static void SetItemFontVals(XDocument doc, VarStorage.AssetCacheDef assetdef, int idIndex, int outputPathIndex, int inGameDirIndex, string assetpath, string assetfilename)
    {
        SetItemFontVals(doc, assetdef.Class, assetdef.Id[idIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex], assetpath, assetfilename);
    }

    public static void SetItemFontVals(XDocument doc, string itemClassValue, string itemIdValue, string outputPath, string inGameDir, string assetpath, string assetfilename)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants("Content")
                     where nodes.Attribute("name").Value == itemIdValue
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("url")
                         select nodes;

                foreach (var item3 in v3)
                {
                    if (!string.IsNullOrWhiteSpace(assetpath))
                    {
                        GlobalFuncs.FixedFileCopy(assetpath, outputPath + "\\" + assetfilename, true);
                    }
                    item3.Value = inGameDir + assetfilename;
                }
            }
        }
    }

    public static void SetItemCoordVals(XDocument doc, VarStorage.AssetCacheDef assetdef, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        SetItemCoordVals(doc, assetdef.Class, X, Y, Z, CoordClass, CoordName);
    }

    public static void SetItemCoordVals(XDocument doc, string itemClassValue, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        SetItemCoordXML(v, X, Y, Z, CoordClass, CoordName);
    }

    public static void SetItemCoordValsNoClassSearch(XDocument doc, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        SetItemCoordXML(v, X, Y, Z, CoordClass, CoordName);
    }

    private static void SetItemCoordXML(IEnumerable<XElement> v, double X, double Y, double Z, string CoordClass, string CoordName)
    {
        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(CoordClass)
                     where nodes.Attribute("name").Value == CoordName
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("X")
                         select nodes;

                foreach (var item3 in v3)
                {
                    item3.Value = X.ToString();
                }

                var v4 = from nodes in item2.Descendants("Y")
                         select nodes;

                foreach (var item4 in v4)
                {
                    item4.Value = Y.ToString();
                }

                var v5 = from nodes in item2.Descendants("Z")
                         select nodes;

                foreach (var item5 in v5)
                {
                    item5.Value = Z.ToString();
                }
            }
        }
    }

    public static void SetHatScaleVals(XDocument doc, double X, double Y, double Z)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "Hat"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in doc.Descendants("Item")
                     where nodes.Attribute("class").Value == "Part"
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in doc.Descendants("Item")
                         where nodes.Attribute("class").Value == "SpecialMesh"
                         select nodes;

                foreach (var item3 in v3)
                {
                    SetItemCoordXML(v3, X, Y, Z, "Vector3", "Scale");
                }
            }
        }
    }

    public static void SetHeadBevel(XDocument doc, double bevel, double bevelRoundness, double bulge, int meshtype, string meshclass, int LODX, int LODY)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            item.SetAttributeValue("class", meshclass);

            var v2 = from nodes in item.Descendants(XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bevel"
                     select nodes;

            foreach (var item2 in v2)
            {
                item2.Value = bevel.ToString();
            }

            var v3 = from nodes in item.Descendants(XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bevel Roundness"
                     select nodes;

            foreach (var item3 in v3)
            {
                item3.Value = bevelRoundness.ToString();
            }

            var v4 = from nodes in item.Descendants(XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bulge"
                     select nodes;

            foreach (var item4 in v4)
            {
                item4.Value = bulge.ToString();
            }

            var vX = from nodes in item.Descendants(XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "LODX"
                     select nodes;

            foreach (var itemX in vX)
            {
                itemX.Value = LODX.ToString();
            }

            var vY = from nodes in item.Descendants(XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "LODY"
                     select nodes;

            foreach (var itemY in vY)
            {
                itemY.Value = LODY.ToString();
            }

            var v5 = from nodes in item.Descendants(XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "MeshType"
                     select nodes;

            foreach (var item5 in v5)
            {
                item5.Value = meshtype.ToString();
            }
        }
    }

    public static string GetPathForType(RobloxFileType type)
    {
        switch (type)
        {
            case RobloxFileType.Hat:
                return GlobalPaths.hatdir;
            case RobloxFileType.HeadNoCustomMesh:
            case RobloxFileType.Head:
                return GlobalPaths.headdir;
            case RobloxFileType.Face:
                return GlobalPaths.facedir;
            case RobloxFileType.TShirt:
                return GlobalPaths.tshirtdir;
            case RobloxFileType.Shirt:
                return GlobalPaths.shirtdir;
            case RobloxFileType.Pants:
                return GlobalPaths.pantsdir;
            default:
                return "";
        }
    }

    public static string[] GetOptionPathsForType(RobloxFileType type)
    {
        switch (type)
        {
            case RobloxFileType.Hat:
                return RobloxDefs.ItemHatFonts.Dir;
            case RobloxFileType.HeadNoCustomMesh:
            case RobloxFileType.Head:
                return RobloxDefs.ItemHeadFonts.Dir;
            case RobloxFileType.Face:
                return RobloxDefs.ItemFaceTexture.Dir;
            case RobloxFileType.TShirt:
                return RobloxDefs.ItemTShirtTexture.Dir;
            case RobloxFileType.Shirt:
                return RobloxDefs.ItemShirtTexture.Dir;
            case RobloxFileType.Pants:
                return RobloxDefs.ItemPantsTexture.Dir;
            default:
                return null;
        }
    }

    public static RobloxFileType GetTypeForInt(int type)
    {
        switch (type)
        {
            case 0:
                return RobloxFileType.Hat;
            case 1:
                return RobloxFileType.Head;
            case 2:
                return RobloxFileType.HeadNoCustomMesh;
            case 3:
                return RobloxFileType.Face;
            case 4:
                return RobloxFileType.TShirt;
            case 5:
                return RobloxFileType.Shirt;
            case 6:
                return RobloxFileType.Pants;
            default:
                return RobloxFileType.RBXM;
        }
    }

    public static bool CreateItem(string filepath, RobloxFileType type, string itemname, string[] assetfilenames, double[] coordoptions, double[] coordoptions2, object[] headoptions, string desctext = "")
    {
        /*MessageBox.Show(assetfilenames[0] + "\n" + 
            assetfilenames[1] + "\n" +
            assetfilenames[2] + "\n" +
            assetfilenames[3] + "\n" +
            coordoptions[0] + "\n" +
            coordoptions[1] + "\n" +
            coordoptions[2] + "\n" +
            headoptions[0] + "\n" +
            headoptions[1] + "\n" +
            headoptions[2] + "\n");*/

        string oldfile = File.ReadAllText(filepath);
        string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile));
        XDocument doc = XDocument.Parse(fixedfile);
        string savDocPath = GetPathForType(type);
        bool success = true;

        try
        {
            switch (type)
            {
                case RobloxFileType.Hat:
                    SetItemFontVals(doc, RobloxDefs.ItemHatFonts, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    SetItemFontVals(doc, RobloxDefs.ItemHatFonts, 1, 1, 1, assetfilenames[1], assetfilenames[3]);
                    SetItemCoordVals(doc, "Hat", coordoptions[0], coordoptions[1], coordoptions[2], "CoordinateFrame", "AttachmentPoint");
                    SetHatScaleVals(doc, coordoptions2[0], coordoptions2[1], coordoptions2[2]);
                    break;
                case RobloxFileType.Head:
                    SetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    SetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 1, 1, 1, assetfilenames[1], assetfilenames[3]);
                    SetItemCoordVals(doc, RobloxDefs.ItemHeadFonts, coordoptions[0], coordoptions[1], coordoptions[2], "Vector3", "Scale");
                    break;
                case RobloxFileType.Face:
                    SetItemFontVals(doc, RobloxDefs.ItemFaceTexture, 0, 0, 0, "", assetfilenames[2]);
                    break;
                case RobloxFileType.TShirt:
                    SetItemFontVals(doc, RobloxDefs.ItemTShirtTexture, 0, 0, 0, "", assetfilenames[2]);
                    break;
                case RobloxFileType.Shirt:
                    SetItemFontVals(doc, RobloxDefs.ItemShirtTexture, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    savDocPath = GlobalPaths.shirtdir;
                    break;
                case RobloxFileType.Pants:
                    SetItemFontVals(doc, RobloxDefs.ItemPantsTexture, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    break;
                case RobloxFileType.HeadNoCustomMesh:
                    SetHeadBevel(doc, Convert.ToDouble(headoptions[0]),
                        Convert.ToDouble(headoptions[1]),
                        Convert.ToDouble(headoptions[2]),
                        Convert.ToInt32(headoptions[3]),
                        headoptions[4].ToString(),
                        Convert.ToInt32(headoptions[5]),
                        Convert.ToInt32(headoptions[6]));
                    SetItemCoordValsNoClassSearch(doc, coordoptions[0], coordoptions[1], coordoptions[2], "Vector3", "Scale");
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("The Item Creation SDK has experienced an error: " + ex.Message, "Novetus Item Creation SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            success = false;
        }
        finally
        {
            doc.Save(savDocPath + "\\" + itemname + ".rbxm");
            if (!string.IsNullOrWhiteSpace(desctext))
            {
                File.WriteAllText(savDocPath + "\\" + itemname + "_desc.txt", desctext);
            }
        }

        return success;
    }

    private void ToggleOptionSet(Label label, TextBox textbox, Button button, string labelText, bool browseButton, bool enable = true)
    {
        label.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        textbox.ReadOnly = !enable;
        textbox.Text = "";
        button.Enabled = browseButton;
        ItemIcon.Image = GlobalFuncs.LoadImage("", "");
    }

    private void ToggleGroup(GroupBox groupbox, string labelText, bool enable = true)
    {
        groupbox.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        groupbox.Enabled = enable;
        ItemIcon.Image = GlobalFuncs.LoadImage("", "");
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

    private void ToggleHatTextureBox(string labelText, bool enable = true)
    {
        UsesHatTexLabel.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        UsesHatTexBox.Enabled = enable;

        if (enable && Directory.Exists(GlobalPaths.hatdirTextures))
        {
            UsesHatTexBox.Items.Add("None");
            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.hatdirTextures);
            FileInfo[] Files = dinfo.GetFiles("*.png");
            foreach (FileInfo file in Files)
            {
                if (file.Name.Equals(string.Empty))
                {
                    continue;
                }

                UsesHatTexBox.Items.Add(file.Name);
            }

            UsesHatTexBox.SelectedItem = "None";
        }
        else
        {
            UsesHatTexBox.Items.Clear();
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
        string msgboxtext = "Some issues have been found with your item:\n";
        bool passed = true;
        bool itemExists = false;

        if (string.IsNullOrWhiteSpace(ItemNameBox.Text))
        {
            msgboxtext += "\n - You must assign an item name.";
            passed = false;
        }
        else
        {
            if(File.Exists(GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".rbxm"))
            {
                msgboxtext += "\n - The item already exists.";
                passed = false;
                itemExists = true;
            }
        }

        if (!itemExists)
        {
            if (ItemIcon.Image == null)
            {
                msgboxtext += "\n - You must assign an icon.";

                if (RequiresIconForTexture && ItemIcon.Image == null)
                {
                    msgboxtext += " This item type requires that you must select an Icon to use as a Template or Texture.";
                }

                passed = false;
            }

            if (Option1Required)
            {
                if (string.IsNullOrWhiteSpace(Option1TextBox.Text))
                {
                    msgboxtext += "\n - You must assign a " + Option1Label.Text + ".";
                    passed = false;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(UsesHatTexBox.Text) || UsesHatMeshBox.Text == "None")
                    {
                        if (!File.Exists(Option1Path))
                        {
                            msgboxtext += "\n - The file assigned as a " + Option1Label.Text + " does not exist. Please rebrowse for the file again.";
                            passed = false;
                        }

                        if (File.Exists(GetOptionPathsForType(type)[0] + "\\" + Option1TextBox.Text))
                        {
                            msgboxtext += "\n - The file assigned as a " + Option1Label.Text + " already exists. Please find an alternate file.";
                            passed = false;
                        }
                    }
                }
            }

            if (Option2Required)
            {
                if (string.IsNullOrWhiteSpace(Option2TextBox.Text))
                {
                    msgboxtext += "\n - You must assign a " + Option2Label.Text + ".";
                    passed = false;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(UsesHatTexBox.Text) || UsesHatTexBox.Text == "None")
                    {
                        if (!File.Exists(Option2Path))
                        {
                            msgboxtext += "\n - The file assigned as a " + Option2Label.Text + " does not exist. Please rebrowse for the file again.";
                            passed = false;
                        }

                        if (File.Exists(GetOptionPathsForType(type)[1] + "\\" + Option2TextBox.Text))
                        {
                            msgboxtext += "\n - The file assigned as a " + Option2Label.Text + " already exists. Please find an alternate file.";
                            passed = false;
                        }
                    }
                }
            }
        }

        if (!passed)
        {
            msgboxtext += "\n\nThese issues must be fixed before the item can be created.";
            MessageBox.Show(msgboxtext, "Novetus Item Creation SDK - Requirements", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return passed;
    }

    private void UpdateWarnings()
    {
        string iconpath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".png";

        string warningtext = "";

        if (File.Exists(GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".rbxm"))
        {
            warningtext = "Warning: This item already exists. Your item will not be created with this name.";
        }

        if (File.Exists(iconpath))
        {
            Image icon1 = GlobalFuncs.LoadImage(iconpath);
            ItemIcon.Image = icon1;
        }
        else
        {
            ItemIcon.Image = null;
        }

        Warning.Text = warningtext;
    }
    #endregion
}
#endregion

