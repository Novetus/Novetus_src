#region Usings
using Novetus.Core;
using RobloxFiles.DataTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
#endregion

#region Item Creation SDK
public partial class ItemCreationSDK : Form
{
    #region Variables
    enum BodyParts
    {
        HEAD,
        TORSO,
        LARM,
        RARM,
        LLEG,
        RLEG
    }

    private static RobloxFileType type;
    private static string Template = "";
    private static string Option1Path = "";
    private static string Option2Path = "";
    private static string[] PackageMeshPaths = {"","","","","",""};
    private static bool Option1Required = false;
    private static bool Option2Required = false;
    private static bool RequiresIconForTexture = false;
    private static bool ItemEditing = false;
    private static bool IsReskin = false;
    private static bool IsResized = false;
    private OpenFileDialog openFileDialog1;
    private static string FileDialogFilter1 = "";
    private static string FileDialogName1 = "";
    private static string FileDialogFilter2 = "";
    private static string FileDialogName2 = "";
    public int partColorID = 194;
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
        Size = new Size(323, 450);
        ItemSettingsGroup.Height = 393;
        ItemTypeListBox.SelectedItem = "Hat";
        MeshTypeBox.SelectedItem = "BlockMesh";
        SpecialMeshTypeBox.SelectedItem = "Head";
        Reset(true);
        CenterToScreen();
    }

    private void ItemCreationSDK_Close(object sender, FormClosingEventArgs e)
    {
        DeleteStrayIcons();
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
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
            }

            if (!string.IsNullOrWhiteSpace(icon.getInstallOutcome()))
            {
                MessageBoxIcon boxicon = MessageBoxIcon.Information;

                if (icon.getInstallOutcome().Contains("Error"))
                {
                    boxicon = MessageBoxIcon.Error;
                }

                MessageBox.Show(icon.getInstallOutcome(), "Novetus Item Creation SDK - Icon Copy Completed", MessageBoxButtons.OK, boxicon);
            }

            Image icon1 = Util.LoadImage(icon.ItemDir + "\\" + icon.ItemName.Replace(" ", "") + ".png", "");
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
        LoadItemIfExists();
        UpdateWarnings();
    }

    private void ItemTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetupSDK();
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
            new Vector3Legacy(ConvertSafe.ToDoubleSafe(XBox.Value), ConvertSafe.ToDoubleSafe(YBox.Value), ConvertSafe.ToDoubleSafe(ZBox.Value)),
            new Vector3(ConvertSafe.ToSingleSafe(XBox360.Value), ConvertSafe.ToSingleSafe(YBox2.Value), ConvertSafe.ToSingleSafe(ZBox2.Value)),
            new Vector3(ConvertSafe.ToSingleSafe(XBoxOne.Value), ConvertSafe.ToSingleSafe(YBox3.Value), ConvertSafe.ToSingleSafe(ZBox3.Value)),
            new Vector3[] { 
                new Vector3(ConvertSafe.ToSingleSafe(rightXBox.Value), ConvertSafe.ToSingleSafe(rightYBox.Value), ConvertSafe.ToSingleSafe(rightZBox.Value)), 
                new Vector3(ConvertSafe.ToSingleSafe(upXBox.Value), ConvertSafe.ToSingleSafe(upYBox.Value), ConvertSafe.ToSingleSafe(upZBox.Value)), 
                new Vector3(-ConvertSafe.ToSingleSafe(forwardXBox.Value), -ConvertSafe.ToSingleSafe(forwardYBox.Value), -ConvertSafe.ToSingleSafe(forwardZBox.Value)) },
            ConvertSafe.ToDoubleSafe(transparencyBox.Value),
            ConvertSafe.ToDoubleSafe(reflectivenessBox.Value),
            new object[] { ConvertSafe.ToDoubleSafe(BevelBox.Value), 
                ConvertSafe.ToDoubleSafe(RoundnessBox.Value), 
                ConvertSafe.ToDoubleSafe(BulgeBox.Value), 
                SpecialMeshTypeBox.SelectedIndex, 
                MeshTypeBox.SelectedItem.ToString(),
                ConvertSafe.ToInt32Safe(LODXBox.Value),
                ConvertSafe.ToInt32Safe(LODYBox.Value)},
            DescBox.Text
            ))
        {
            string itemName = ItemNameBox.Text;
            RobloxFileType itemType = type;

            Reset(true, true);
            ItemTypeListBox.SelectedIndex = GetIntForType(itemType);
            ItemNameBox.Text = itemName;
            LoadItemIfExists();

            DialogResult LaunchCharCustom = MessageBox.Show("The creation of your item, " + ItemNameBox.Text + ", is successful! Would you like to test your item out in Character Customization?", "Novetus Item Creation SDK - Item Creation Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (LaunchCharCustom == DialogResult.Yes)
            {
                //we need to keep the form open if we're testing items.
                //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "CharacterCustomizationExtended" ||
                        frm.Name == "CharacterCustomizationCompact")
                    {
                        return;
                    }
                }

                NovetusFuncs.LaunchCharacterCustomization(true);
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

    private void PartButtonClick(TextBox box, int index)
    {
        PackageMeshPaths[index] = LoadAsset(FileDialogName1, FileDialogFilter1);
        box.Text = Path.GetFileName(PackageMeshPaths[index]);
    }

    private void PartBoxSelect(ComboBox box, TextBox box2, Button button, int index)
    {
        PackageMeshPaths[index] = "";

        if (box.SelectedItem.ToString() != "None")
        {
            box2.Text = box.Text;
            box2.Enabled = false;
            button.Enabled = false;
        }
        else
        {
            box2.Enabled = true;
            button.Enabled = true;
            box2.Text = "";
        }
    }

    private void Head_LoadFileButton_Click(object sender, EventArgs e)
    {
        PartButtonClick(Head_LoadFileBox, (int)BodyParts.HEAD);
    }

    private void Head_ExistingFileBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        PartBoxSelect(Head_ExistingFileBox, Head_LoadFileBox, Head_LoadFileButton, (int)BodyParts.HEAD);
    }

    private void Head_ExistingFileButton_Click(object sender, EventArgs e)
    {
        TogglePackageMeshBox(Head_ExistingFileBox, Head_ExistingFileButton);
    }

    private void Torso_LoadFileButton_Click(object sender, EventArgs e)
    {
        PartButtonClick(Torso_LoadFileBox, (int)BodyParts.TORSO);
    }

    private void Torso_ExistingFileBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        PartBoxSelect(Torso_ExistingFileBox, Torso_LoadFileBox, Torso_LoadFileButton, (int)BodyParts.TORSO);
    }

    private void Torso_ExistingFileButton_Click(object sender, EventArgs e)
    {
        TogglePackageMeshBox(Torso_ExistingFileBox, Torso_ExistingFileButton);
    }

    private void LeftArm_LoadFileButton_Click(object sender, EventArgs e)
    {
        PartButtonClick(LeftArm_LoadFileBox, (int)BodyParts.LARM);
    }

    private void LeftArm_ExistingFileBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        PartBoxSelect(LeftArm_ExistingFileBox, LeftArm_LoadFileBox, LeftArm_LoadFileButton, (int)BodyParts.LARM);
    }

    private void LeftArm_ExistingFileButton_Click(object sender, EventArgs e)
    {
        TogglePackageMeshBox(LeftArm_ExistingFileBox, LeftArm_ExistingFileButton);
    }

    private void RightArm_LoadFileButton_Click(object sender, EventArgs e)
    {
        PartButtonClick(RightArm_LoadFileBox, (int)BodyParts.RARM);
    }

    private void RightArm_ExistingFileBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        PartBoxSelect(RightArm_ExistingFileBox, RightArm_LoadFileBox, RightArm_LoadFileButton, (int)BodyParts.RARM);
    }

    private void RightArm_ExistingFileButton_Click(object sender, EventArgs e)
    {
        TogglePackageMeshBox(RightArm_ExistingFileBox, RightArm_ExistingFileButton);
    }

    private void LeftLeg_LoadFileButton_Click(object sender, EventArgs e)
    {
        PartButtonClick(LeftLeg_LoadFileBox, (int)BodyParts.LLEG);
    }

    private void LeftLeg_ExistingFileBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        PartBoxSelect(LeftLeg_ExistingFileBox, LeftLeg_LoadFileBox, LeftLeg_LoadFileButton, (int)BodyParts.LLEG);
    }

    private void LeftLeg_ExistingFileButton_Click(object sender, EventArgs e)
    {
        TogglePackageMeshBox(LeftLeg_ExistingFileBox, LeftLeg_ExistingFileButton);
    }

    private void RightLeg_LoadFileButton_Click(object sender, EventArgs e)
    {
        PartButtonClick(RightLeg_LoadFileBox, (int)BodyParts.RLEG);
    }

    private void RightLeg_ExistingFileBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        PartBoxSelect(RightLeg_ExistingFileBox, RightLeg_LoadFileBox, RightLeg_LoadFileButton, (int)BodyParts.RLEG);
    }

    private void RightLeg_ExistingFileButton_Click(object sender, EventArgs e)
    {
        TogglePackageMeshBox(RightLeg_ExistingFileBox, RightLeg_ExistingFileButton);
    }

    private void EditItem_CheckedChanged(object sender, EventArgs e)
    {
        ItemEditing = EditItemBox.Checked;
        UpdateWarnings();
    }

    private void ReskinBox_CheckedChanged(object sender, EventArgs e)
    {
        IsReskin = ReskinBox.Checked;
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        Reset(true);
        MessageBox.Show("All fields reset!", "Novetus Item Creation SDK - Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SettingsButton_Click(object sender, EventArgs e)
    {
        if (IsResized)
        {
            Size = new Size(323, 450);
            IsResized = false;
        }
        else
        {
            Size = new Size(914, 450);
            IsResized = true;
        }
    }

    private void HatColorButton_Click(object sender, EventArgs e)
    {
        ItemCreationSDKColorMenu menu = new ItemCreationSDKColorMenu(this);
        menu.Show();
    }

    private void UsesHatMeshBoxRefresh_Click(object sender, EventArgs e)
    {
        switch (type)
        {
            case RobloxFileType.Hat:
                ToggleHatMeshBox("Uses Existing Hat Mesh");
                break;
            default:
                break;
        }
    }

    private void UsesHatTexBoxRefresh_Click(object sender, EventArgs e)
    {
        switch (type)
        {
            case RobloxFileType.Hat:
                ToggleHatTextureBox("Uses Existing Hat Texture");
                break;
            default:
                break;
        }
    }
    #endregion

    #region Functions

    #region XML Editing/Fetching
    public static void SetItemFontVals(XDocument doc, AssetCacheDef assetdef, int idIndex, int outputPathIndex, int inGameDirIndex, string assetpath, string assetfilename)
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
                        IOSafe.File.Copy(assetpath, outputPath + "\\" + assetfilename, true);
                    }
                    item3.Value = inGameDir + assetfilename;
                }
            }
        }
    }

    public static void SetItemFontValEmpty(XDocument doc, AssetCacheDef assetdef, int idIndex)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == assetdef.Class
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants("Content")
                     where nodes.Attribute("name").Value == assetdef.Id[idIndex]
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("url")
                         select nodes;

                foreach (var item3 in v3)
                {
                    item3.Value = "";
                }
            }
        }
    }

    public static string GetItemFontVals(XDocument doc, AssetCacheDef assetdef, int idIndex)
    {
        return GetItemFontVals(doc, assetdef.Class, assetdef.Id[idIndex]);
    }

    public static string GetItemFontVals(XDocument doc, string itemClassValue, string itemIdValue)
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
                    return item3.Value;
                }
            }
        } 
        
        return "";
    }

    public static void SetItemCoordVals(XDocument doc, AssetCacheDef assetdef, Vector3Legacy coord, string CoordClass, string CoordName)
    {
        SetItemCoordVals(doc, assetdef.Class, coord, CoordClass, CoordName);
    }

    public static void SetItemCoordVals(XDocument doc, string itemClassValue, Vector3Legacy coord, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        SetItemCoordXML(v, coord, CoordClass, CoordName);
    }

    public static void SetItemCoordValsNoClassSearch(XDocument doc, Vector3 coord, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        SetItemCoordXML(v, coord, CoordClass, CoordName);
    }

    //this is dumb
    public static void SetItemCoordValsNoClassSearch(XDocument doc, Vector3Legacy coord, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        SetItemCoordXML(v, coord, CoordClass, CoordName);
    }

    //this is dumb
    private static void SetItemCoordXML(IEnumerable<XElement> v, Vector3Legacy coord, string CoordClass, string CoordName)
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
                    item3.Value = coord.X.ToString();
                }

                var v4 = from nodes in item2.Descendants("Y")
                         select nodes;

                foreach (var item4 in v4)
                {
                    item4.Value = coord.Y.ToString();
                }

                var v5 = from nodes in item2.Descendants("Z")
                         select nodes;

                foreach (var item5 in v5)
                {
                    item5.Value = coord.Z.ToString();
                }
            }
        }
    }

    private static void SetItemCoordXML(IEnumerable<XElement> v, Vector3 coord, string CoordClass, string CoordName)
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
                    item3.Value = coord.X.ToString();
                }

                var v4 = from nodes in item2.Descendants("Y")
                         select nodes;

                foreach (var item4 in v4)
                {
                    item4.Value = coord.Y.ToString();
                }

                var v5 = from nodes in item2.Descendants("Z")
                         select nodes;

                foreach (var item5 in v5)
                {
                    item5.Value = coord.Z.ToString();
                }
            }
        }
    }

    public static string GetItemCoordVals(XDocument doc, AssetCacheDef assetdef, string CoordClass, string CoordName)
    {
        return GetItemCoordVals(doc, assetdef.Class, CoordClass, CoordName);
    }

    public static string GetItemCoordVals(XDocument doc, string itemClassValue, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        return GetItemCoordXML(v, CoordClass, CoordName);
    }

    public static string GetItemCoordValsNoClassSearch(XDocument doc, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        return GetItemCoordXML(v, CoordClass, CoordName);
    }

    private static string GetItemCoordXML(IEnumerable<XElement> v, string CoordClass, string CoordName)
    {
        string coord = "";

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
                    coord += item3.Value + ",";
                }

                var v4 = from nodes in item2.Descendants("Y")
                         select nodes;

                foreach (var item4 in v4)
                {
                    coord += item4.Value + ",";
                }

                var v5 = from nodes in item2.Descendants("Z")
                         select nodes;

                foreach (var item5 in v5)
                {
                    coord += item5.Value;
                }
            }
        }

        return coord;
    }

    public static void SetItemRotationVals(XDocument doc, AssetCacheDef assetdef, Vector3 right, Vector3 up, Vector3 forward, string CoordClass, string CoordName)
    {
        SetItemRotationVals(doc, assetdef.Class, right, up, forward, CoordClass, CoordName);
    }

    public static void SetItemRotationVals(XDocument doc, string itemClassValue, Vector3 right, Vector3 up, Vector3 forward, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        SetItemRotationXML(v, right, up, forward, CoordClass, CoordName);
    }

    public static void SetItemRotationValsNoClassSearch(XDocument doc, Vector3 right, Vector3 up, Vector3 forward, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        SetItemRotationXML(v, right, up, forward, CoordClass, CoordName);
    }

    private static void SetItemRotationXML(IEnumerable<XElement> v, Vector3 right, Vector3 up, Vector3 forward, string CoordClass, string CoordName)
    {
        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(CoordClass)
                     where nodes.Attribute("name").Value == CoordName
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("R00")
                         select nodes;

                foreach (var item3 in v3)
                {
                    item3.Value = right.X.ToString();
                }

                var v4 = from nodes in item2.Descendants("R01")
                         select nodes;

                foreach (var item4 in v4)
                {
                    item4.Value = right.Y.ToString();
                }

                var v5 = from nodes in item2.Descendants("R02")
                         select nodes;

                foreach (var item5 in v5)
                {
                    item5.Value = right.Z.ToString();
                }

                var v6 = from nodes in item2.Descendants("R10")
                         select nodes;

                foreach (var item6 in v6)
                {
                    item6.Value = up.X.ToString();
                }

                var v7 = from nodes in item2.Descendants("R11")
                         select nodes;

                foreach (var item7 in v7)
                {
                    item7.Value = up.Y.ToString();
                }

                var v8 = from nodes in item2.Descendants("R12")
                         select nodes;

                foreach (var item8 in v8)
                {
                    item8.Value = up.Z.ToString();
                }

                var v9 = from nodes in item2.Descendants("R20")
                         select nodes;

                foreach (var item9 in v9)
                {
                    item9.Value = forward.X.ToString();
                }

                var v10 = from nodes in item2.Descendants("R21")
                          select nodes;

                foreach (var item10 in v10)
                {
                   item10.Value = forward.Y.ToString();
                }

                var v11 = from nodes in item2.Descendants("R22")
                          select nodes;

                foreach (var item11 in v11)
                {
                    item11.Value = forward.Z.ToString();
                }
            }
        }
    }

    public static string GetItemRotationVals(XDocument doc, AssetCacheDef assetdef, string CoordClass, string CoordName)
    {
        return GetItemRotationVals(doc, assetdef.Class, CoordClass, CoordName);
    }

    public static string GetItemRotationVals(XDocument doc, string itemClassValue, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == itemClassValue
                select nodes;

        return GetItemRotationXML(v, CoordClass, CoordName);
    }

    public static string GetItemRotationValsNoClassSearch(XDocument doc, string CoordClass, string CoordName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        return GetItemRotationXML(v, CoordClass, CoordName);
    }

    private static string GetItemRotationXML(IEnumerable<XElement> v, string CoordClass, string CoordName)
    {
        string coord = "";

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(CoordClass)
                     where nodes.Attribute("name").Value == CoordName
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("R00")
                         select nodes;

                foreach (var item3 in v3)
                {
                    coord += item3.Value + ",";
                }

                var v4 = from nodes in item2.Descendants("R01")
                         select nodes;

                foreach (var item4 in v4)
                {
                    coord += item4.Value + ",";
                }

                var v5 = from nodes in item2.Descendants("R02")
                         select nodes;

                foreach (var item5 in v5)
                {
                    coord += item5.Value + ",";
                }

                var v6 = from nodes in item2.Descendants("R10")
                         select nodes;

                foreach (var item6 in v6)
                {
                    coord += item6.Value + ",";
                }

                var v7 = from nodes in item2.Descendants("R11")
                         select nodes;

                foreach (var item7 in v7)
                {
                    coord += item7.Value + ",";
                }

                var v8 = from nodes in item2.Descendants("R12")
                         select nodes;

                foreach (var item8 in v8)
                {
                    coord += item8.Value + ",";
                }

                var v9 = from nodes in item2.Descendants("R20")
                         select nodes;

                foreach (var item9 in v9)
                {
                    coord += item9.Value + ",";
                }

                var v10 = from nodes in item2.Descendants("R21")
                         select nodes;

                foreach (var item10 in v10)
                {
                    coord += item10.Value + ",";
                }

                var v11 = from nodes in item2.Descendants("R22")
                         select nodes;

                foreach (var item11 in v11)
                {
                    coord += item11.Value;
                }
            }
        }

        return coord;
    }

    public static void SetHatMeshVals(XDocument doc, Vector3 coord, string type, string val)
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
                    SetItemCoordXML(v3, coord, type, val);
                }
            }
        }
    }

    public static string GetHatMeshVals(XDocument doc, string type, string val)
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
                    return GetItemCoordXML(v3, type, val);
                }
            }
        }

        return "";
    }

    public static void SetHatPartVals(XDocument doc, int colorID, double transparency, double reflectiveness)
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
                var v5 = from nodes in item.Descendants(RobloxXML.XMLTypes.Int.ToString().ToLower())
                         where nodes.Attribute("name").Value == "BrickColor"
                         select nodes;

                foreach (var item5 in v5)
                {
                    item5.Value = colorID.ToString();
                }

                var v4 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                         where nodes.Attribute("name").Value == "Reflectance"
                         select nodes;

                foreach (var item4 in v4)
                {
                    item4.Value = reflectiveness.ToString();
                }

                var v3 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                         where nodes.Attribute("name").Value == "Transparency"
                         select nodes;

                foreach (var item3 in v3)
                {
                    item3.Value = transparency.ToString();
                }
            }
        }
    }

    public static bool DoesHatHavePartColor(XDocument doc)
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
                var v5 = from nodes in item.Descendants(RobloxXML.XMLTypes.Int.ToString().ToLower())
                         where nodes.Attribute("name").Value == "BrickColor"
                         select nodes;

                foreach (var item5 in v5)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static string GetHatPartVals(XDocument doc)
    {
        string hatpartsettings = "";

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
                if (DoesHatHavePartColor(doc))
                {
                    var v5 = from nodes in item.Descendants(RobloxXML.XMLTypes.Int.ToString().ToLower())
                             where nodes.Attribute("name").Value == "BrickColor"
                             select nodes;

                    foreach (var item5 in v5)
                    {
                        hatpartsettings += item5.Value + ",";
                    }
                }
                else
                {
                    hatpartsettings += "194,";
                }

                var v4 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                         where nodes.Attribute("name").Value == "Reflectance"
                         select nodes;

                foreach (var item4 in v4)
                {
                    hatpartsettings += item4.Value + ",";
                }

                var v3 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                         where nodes.Attribute("name").Value == "Transparency"
                         select nodes;

                foreach (var item3 in v3)
                {
                    hatpartsettings += item3.Value;
                }
            }
        }

        return hatpartsettings;
    }

    public static void SetHeadBevel(XDocument doc, double bevel, double bevelRoundness, double bulge, int meshtype, string meshclass, int LODX, int LODY)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            item.SetAttributeValue("class", meshclass);

            var v2 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bevel"
                     select nodes;

            foreach (var item2 in v2)
            {
                item2.Value = bevel.ToString();
            }

            var v3 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bevel Roundness"
                     select nodes;

            foreach (var item3 in v3)
            {
                item3.Value = bevelRoundness.ToString();
            }

            var v4 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bulge"
                     select nodes;

            foreach (var item4 in v4)
            {
                item4.Value = bulge.ToString();
            }

            var vX = from nodes in item.Descendants(RobloxXML.XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "LODX"
                     select nodes;

            foreach (var itemX in vX)
            {
                itemX.Value = LODX.ToString();
            }

            var vY = from nodes in item.Descendants(RobloxXML.XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "LODY"
                     select nodes;

            foreach (var itemY in vY)
            {
                itemY.Value = LODY.ToString();
            }

            var v5 = from nodes in item.Descendants(RobloxXML.XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "MeshType"
                     select nodes;

            foreach (var item5 in v5)
            {
                item5.Value = meshtype.ToString();
            }
        }
    }

    public static string GetHeadBevel(XDocument doc)
    {
        string bevelsettings = "";

        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bevel"
                     select nodes;

            foreach (var item2 in v2)
            {
                bevelsettings += item2.Value + ",";
            }

            var v3 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bevel Roundness"
                     select nodes;

            foreach (var item3 in v3)
            {
                bevelsettings += item3.Value + ",";
            }

            var v4 = from nodes in item.Descendants(RobloxXML.XMLTypes.Float.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Bulge"
                     select nodes;

            foreach (var item4 in v4)
            {
                bevelsettings += item4.Value + ",";
            }

            var vX = from nodes in item.Descendants(RobloxXML.XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "LODX"
                     select nodes;

            foreach (var itemX in vX)
            {
                bevelsettings += itemX.Value + ",";
            }

            var vY = from nodes in item.Descendants(RobloxXML.XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "LODY"
                     select nodes;

            foreach (var itemY in vY)
            {
                bevelsettings += itemY.Value + ",";
            }

            var v5 = from nodes in item.Descendants(RobloxXML.XMLTypes.Token.ToString().ToLower())
                     where nodes.Attribute("name").Value == "MeshType"
                     select nodes;

            foreach (var item5 in v5)
            {
                bevelsettings += item5.Value;
            }
        }

        return bevelsettings;
    }

    public static bool IsHeadMesh(XDocument doc)
    {
        bool check = true;

        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "SpecialMesh"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants("Content")
                     where nodes.Attribute("name").Value == "MeshId"
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants("url")
                         select nodes;

                foreach (var item3 in v3)
                {
                    if (!string.IsNullOrWhiteSpace(item3.Value))
                    {
                        check = false;
                    }
                }
            }
        }

        return check;
    }

    public static void SetPackageName(XDocument doc, string packageName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(RobloxXML.XMLTypes.String.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Value"
                     select nodes;

            foreach (var item2 in v2)
            {
                item2.Value = packageName;
            }
        }
    }

    public static void SetPackagePartVal(XDocument doc, string partName, string assetName, int index = -1)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants("Item")
                     where nodes.Attribute("class").Value == "StringValue"
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants(RobloxXML.XMLTypes.String.ToString().ToLower())
                         where nodes.Attribute("name").Value == "Name"
                         select nodes;

                foreach (var item3 in v3)
                {
                    if (item3.Value == partName)
                    {
                        var v4 = from nodes in item2.Descendants(RobloxXML.XMLTypes.String.ToString().ToLower())
                                 where nodes.Attribute("name").Value == "Value"
                                 select nodes;

                        foreach (var item4 in v4)
                        {
                            if (!string.IsNullOrWhiteSpace(assetName))
                            {
                                if (!string.IsNullOrWhiteSpace(PackageMeshPaths[index]) && index != -1)
                                {
                                    IOSafe.File.Copy(PackageMeshPaths[index], GlobalPaths.extradirFonts + "\\" + assetName, true);
                                }

                                item4.Value = GlobalPaths.extraGameDirFonts + assetName;
                            }
                            else
                            {
                                item4.Value = "";
                            }
                        }
                    }
                }
            }
        }
    }

    public static string GetPackageName(XDocument doc)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(RobloxXML.XMLTypes.String.ToString().ToLower())
                     where nodes.Attribute("name").Value == "Value"
                     select nodes;

            foreach (var item2 in v2)
            {
                return item2.Value;
            }
        }

        return "";
    }

    public static string GetPackagePartVal(XDocument doc, string partName)
    {
        var v = from nodes in doc.Descendants("Item")
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants("Item")
                     where nodes.Attribute("class").Value == "StringValue"
                     select nodes;

            foreach (var item2 in v2)
            {
                var v3 = from nodes in item2.Descendants(RobloxXML.XMLTypes.String.ToString().ToLower())
                         where nodes.Attribute("name").Value == "Name"
                         select nodes;

                foreach (var item3 in v3)
                {
                    if (item3.Value == partName)
                    {
                        var v4 = from nodes in item2.Descendants(RobloxXML.XMLTypes.String.ToString().ToLower())
                                 where nodes.Attribute("name").Value == "Value"
                                 select nodes;

                        foreach (var item4 in v4)
                        {
                            return item4.Value;
                        }
                    }
                }
            }
        }

        return "null";
    }

    #endregion

    public void SetupSDK()
    {
        ReskinBox.Checked = false;
        DeleteStrayIcons();

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
                Option2Required = false;
                ToggleGroup(CoordGroup, "Hat Attachment Point");
                ToggleGroup(CoordGroup2, "Hat Mesh Scale");
                ToggleGroup(CoordGroup3, "Hat Mesh Vertex Color");
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "Additional Hat Options");
                ToggleGroup(ItemSettingsGroup, "Hat Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HatTemplate.rbxm";
                FileDialogFilter1 = "*.mesh";
                FileDialogName1 = "Hat Mesh";
                FileDialogFilter2 = "*.png";
                FileDialogName2 = "Hat Texture";
                RequiresIconForTexture = false;
                HatOptionsGroup.Location = new Point(610, 215);
                MeshOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
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
                ToggleGroup(CoordGroup2, "Head Mesh Vertex Color");
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "Head Mesh Options");
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "Head Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadNoCustomMeshTemplate.rbxm";
                RequiresIconForTexture = false;
                MeshOptionsGroup.Location = new Point(610, 215);
                HatOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
                break;
            case RobloxFileType.Head:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "Head Mesh", true);
                Option1Path = "";
                Option1Required = true;
                ToggleHatMeshBox("", false);
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "Head Texture", true);
                Option2Path = "";
                Option2Required = false;
                ToggleGroup(CoordGroup, "Head Mesh Scale");
                ToggleGroup(CoordGroup2, "Head Mesh Vertex Color");
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "Head Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\HeadTemplate.rbxm";
                FileDialogFilter1 = "*.mesh";
                FileDialogName1 = "Head Mesh";
                FileDialogFilter2 = "*.png";
                FileDialogName2 = "Head Texture";
                RequiresIconForTexture = false;
                MeshOptionsGroup.Location = new Point(610, 215);
                HatOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
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
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "Face Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\FaceTemplate.rbxm";
                RequiresIconForTexture = true;
                HatOptionsGroup.Location = new Point(610, 215);
                MeshOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
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
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "T-Shirt Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\TShirtTemplate.rbxm";
                RequiresIconForTexture = true;
                HatOptionsGroup.Location = new Point(610, 215);
                MeshOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
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
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "Shirt Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\ShirtTemplate.rbxm";
                FileDialogFilter1 = "*.png";
                FileDialogName1 = "Shirt Template";
                RequiresIconForTexture = false;
                HatOptionsGroup.Location = new Point(610, 215);
                MeshOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
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
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "Pants Settings", true);
                ToggleGroup(PackageOptionsBox, "", false);
                InitPackageGroup(false);
                Template = GlobalPaths.ConfigDirTemplates + "\\PantsTemplate.rbxm";
                FileDialogFilter1 = "*.png";
                FileDialogName1 = "Pants Template";
                RequiresIconForTexture = false;
                HatOptionsGroup.Location = new Point(610, 215);
                MeshOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(1180, 67);
                break;
            case RobloxFileType.Package:
                ToggleOptionSet(Option1Label, Option1TextBox, Option1BrowseButton, "", false, false);
                Option1Path = "";
                Option1Required = false;
                ToggleHatMeshBox("", false);
                ToggleHatTextureBox("", false);
                ToggleOptionSet(Option2Label, Option2TextBox, Option2BrowseButton, "", false, false);
                Option2Path = "";
                Option2Required = false;
                PackageMeshPaths = new string[] { "","","","","",""};
                ToggleGroup(CoordGroup, "", false);
                ToggleGroup(CoordGroup2, "", false);
                ToggleGroup(CoordGroup3, "", false);
                ToggleGroup(MeshOptionsGroup, "", false);
                ToggleGroup(HatOptionsGroup, "", false);
                ToggleGroup(ItemSettingsGroup, "Package Options", true);
                ToggleGroup(PackageOptionsBox, "Package Contents", true);
                InitPackageGroup();
                Template = GlobalPaths.ConfigDirTemplates + "\\PackageTemplate.rbxm";
                RequiresIconForTexture = false;
                FileDialogFilter1 = "*.mesh";
                FileDialogName1 = "Part Mesh";
                HatOptionsGroup.Location = new Point(911, 20);
                MeshOptionsGroup.Location = new Point(911, 20);
                PackageOptionsBox.Location = new Point(334, 30);
                break;
            default:
                break;
        }

        LoadItemIfExists();
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
            case RobloxFileType.Package:
                return GlobalPaths.extradir;
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
            case RobloxFileType.Package:
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
            case 7:
                return RobloxFileType.Package;
            default:
                return RobloxFileType.RBXM;
        }
    }

    public static int GetIntForType(RobloxFileType type)
    {
        switch (type)
        {
            case RobloxFileType.Hat:
                return 0;
            case RobloxFileType.Head:
                return 1;
            case RobloxFileType.HeadNoCustomMesh:
                return 2;
            case RobloxFileType.Face:
                return 3;
            case RobloxFileType.TShirt:
                return 4;
            case RobloxFileType.Shirt:
                return 5;
            case RobloxFileType.Pants:
                return 6;
            case RobloxFileType.Package:
                return 7;
            default:
                return -1;
        }
    }

    public bool CreateItem(string filepath, RobloxFileType type, string itemname, string[] assetfilenames, Vector3Legacy coordoptions, Vector3 coordoptions2, Vector3 coordoptions3, Vector3[] rotationoptions, double transparency, double reflectiveness, object[] headoptions, string desctext = "")
    {
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
                    if (!string.IsNullOrWhiteSpace(assetfilenames[3]))
                    {
                        SetItemFontVals(doc, RobloxDefs.ItemHatFonts, 1, 1, 1, assetfilenames[1], assetfilenames[3]);
                    }
                    else
                    {
                        SetItemFontValEmpty(doc, RobloxDefs.ItemHatFonts, 1);
                    }
                    SetItemCoordVals(doc, "Hat", coordoptions, "CoordinateFrame", "AttachmentPoint");
                    SetHatMeshVals(doc, coordoptions2, "Vector3", "Scale");
                    SetHatMeshVals(doc, coordoptions3, "Vector3", "VertexColor");
                    SetHatPartVals(doc, partColorID, transparency, reflectiveness);
                    SetItemRotationVals(doc, "Hat", rotationoptions[0], rotationoptions[1], rotationoptions[2], "CoordinateFrame", "AttachmentPoint");
                    break;
                case RobloxFileType.Head:
                    SetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    if (!string.IsNullOrWhiteSpace(assetfilenames[3]))
                    {
                        SetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 1, 1, 1, assetfilenames[1], assetfilenames[3]);
                    }
                    else
                    {
                        SetItemFontValEmpty(doc, RobloxDefs.ItemHeadFonts, 1);
                    }
                    SetItemCoordVals(doc, RobloxDefs.ItemHeadFonts, coordoptions, "Vector3", "Scale");
                    break;
                case RobloxFileType.Face:
                    SetItemFontVals(doc, RobloxDefs.ItemFaceTexture, 0, 0, 0, "", assetfilenames[2]);
                    break;
                case RobloxFileType.TShirt:
                    SetItemFontVals(doc, RobloxDefs.ItemTShirtTexture, 0, 0, 0, "", assetfilenames[2]);
                    break;
                case RobloxFileType.Shirt:
                    SetItemFontVals(doc, RobloxDefs.ItemShirtTexture, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    break;
                case RobloxFileType.Pants:
                    SetItemFontVals(doc, RobloxDefs.ItemPantsTexture, 0, 0, 0, assetfilenames[0], assetfilenames[2]);
                    break;
                case RobloxFileType.HeadNoCustomMesh:
                    SetHeadBevel(doc, ConvertSafe.ToDoubleSafe(headoptions[0]),
                        ConvertSafe.ToDoubleSafe(headoptions[1]),
                        ConvertSafe.ToDoubleSafe(headoptions[2]),
                        ConvertSafe.ToInt32Safe(headoptions[3]),
                        headoptions[4].ToString(),
                        ConvertSafe.ToInt32Safe(headoptions[5]),
                        ConvertSafe.ToInt32Safe(headoptions[6]));
                    SetItemCoordValsNoClassSearch(doc, coordoptions, "Vector3", "Scale");
                    SetItemCoordValsNoClassSearch(doc, coordoptions2, "Vector3", "VertexColor");
                    break;
                case RobloxFileType.Package:
                    SetPackageName(doc, PackageNameBox.Text);
                    SetPackagePartVal(doc, "Head", Head_LoadFileBox.Text, (int)BodyParts.HEAD);
                    SetPackagePartVal(doc, "Torso", Torso_LoadFileBox.Text, (int)BodyParts.TORSO);
                    SetPackagePartVal(doc, "Right Arm", RightArm_LoadFileBox.Text, (int)BodyParts.RARM);
                    SetPackagePartVal(doc, "Left Arm", LeftArm_LoadFileBox.Text, (int)BodyParts.LARM);
                    SetPackagePartVal(doc, "Right Leg", RightLeg_LoadFileBox.Text, (int)BodyParts.RLEG);
                    SetPackagePartVal(doc, "Left Leg", LeftLeg_LoadFileBox.Text, (int)BodyParts.LLEG);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
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

    //https://stackoverflow.com/questions/6921105/given-a-filesystem-path-is-there-a-shorter-way-to-extract-the-filename-without
    static string GetFileBaseNameUsingSplit(string path)
    {
        string[] pathArr = path.Split('/');
        string[] fileArr = pathArr.Last().Split('.');
        string fileBaseName = fileArr.First().ToString();

        return fileBaseName;
    }

    private void LoadPartMesh(XDocument doc, ComboBox box, string partName)
    {
        string PAKHeadMeshFilename = GetFileBaseNameUsingSplit(GetPackagePartVal(doc, partName)) + ".mesh";
        int head = box.FindStringExact(PAKHeadMeshFilename);
        if (head >= 0)
        {
            box.SelectedIndex = head;
        }
    }

    public bool LoadItem(string filepath, RobloxFileType type)
    {
        Option1Path = "";
        Option2Path = "";

        string oldfile = File.ReadAllText(filepath);
        string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile));
        XDocument doc = XDocument.Parse(fixedfile);
        bool success = true;

        try
        {
            switch (type)
            {
                case RobloxFileType.Hat:
                    ToggleHatMeshBox("Uses Existing Hat Mesh");
                    ToggleHatTextureBox("Uses Existing Hat Texture");
                    string MeshFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemHatFonts, 0)) + ".mesh";
                    string TextureFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemHatFonts, 1)) + ".png";

                    //https://stackoverflow.com/questions/10160708/how-do-i-find-an-item-by-value-in-an-combobox-in-c
                    int i = UsesHatMeshBox.FindStringExact(MeshFilename);
                    if (i >= 0)
                    {
                        UsesHatMeshBox.SelectedIndex = i;
                    }

                    int i2 = UsesHatTexBox.FindStringExact(TextureFilename);
                    if (i2 >= 0)
                    {
                        UsesHatTexBox.SelectedIndex = i2;
                    }

                    string HatCoords = GetItemCoordVals(doc, "Hat", "CoordinateFrame", "AttachmentPoint");

                    if (!string.IsNullOrWhiteSpace(HatCoords))
                    {
                        string[] HatCoordsSplit = HatCoords.Split(',');
                        XBox.Value = ConvertSafe.ToDecimalSafe(HatCoordsSplit[0]);
                        YBox.Value = ConvertSafe.ToDecimalSafe(HatCoordsSplit[1]);
                        ZBox.Value = ConvertSafe.ToDecimalSafe(HatCoordsSplit[2]);
                    }

                    string HatScaleCoords = GetHatMeshVals(doc, "Vector3", "Scale");

                    if (!string.IsNullOrWhiteSpace(HatScaleCoords))
                    {
                        string[] HatScaleCoordsSplit = HatScaleCoords.Split(',');
                        XBox360.Value = ConvertSafe.ToDecimalSafe(HatScaleCoordsSplit[0]);
                        YBox2.Value = ConvertSafe.ToDecimalSafe(HatScaleCoordsSplit[1]);
                        ZBox2.Value = ConvertSafe.ToDecimalSafe(HatScaleCoordsSplit[2]);
                    }

                    string HatColorCoords = GetHatMeshVals(doc, "Vector3", "VertexColor");

                    if (!string.IsNullOrWhiteSpace(HatColorCoords))
                    {
                        string[] HatColorCoordsSplit = HatColorCoords.Split(',');
                        XBoxOne.Value = ConvertSafe.ToDecimalSafe(HatColorCoordsSplit[0]);
                        YBox3.Value = ConvertSafe.ToDecimalSafe(HatColorCoordsSplit[1]);
                        ZBox3.Value = ConvertSafe.ToDecimalSafe(HatColorCoordsSplit[2]);
                    }

                    string HatRotation = GetItemRotationVals(doc, "Hat", "CoordinateFrame", "AttachmentPoint");

                    if (!string.IsNullOrWhiteSpace(HatRotation))
                    {
                        string[] HatRotationSplit = HatRotation.Split(',');
                        rightXBox.Value = ConvertSafe.ToDecimalSafe(HatRotationSplit[0]);
                        rightYBox.Value = ConvertSafe.ToDecimalSafe(HatRotationSplit[1]);
                        rightZBox.Value = ConvertSafe.ToDecimalSafe(HatRotationSplit[2]);
                        upXBox.Value = ConvertSafe.ToDecimalSafe(HatRotationSplit[3]);
                        upYBox.Value = ConvertSafe.ToDecimalSafe(HatRotationSplit[4]);
                        upZBox.Value = ConvertSafe.ToDecimalSafe(HatRotationSplit[5]);
                        forwardXBox.Value = -ConvertSafe.ToDecimalSafe(HatRotationSplit[6]);
                        forwardYBox.Value = -ConvertSafe.ToDecimalSafe(HatRotationSplit[7]);
                        forwardZBox.Value = -ConvertSafe.ToDecimalSafe(HatRotationSplit[8]);
                    }

                    string HatPartVals = GetHatPartVals(doc);

                    if (!string.IsNullOrWhiteSpace(HatPartVals))
                    {
                        string[] HatPartValsSplit = HatPartVals.Split(',');
                        partColorID = ConvertSafe.ToInt32Safe(HatPartValsSplit[0]);
                        partColorLabel.Text = partColorID.ToString();
                        reflectivenessBox.Value = ConvertSafe.ToDecimalSafe(HatPartValsSplit[1]);
                        transparencyBox.Value = ConvertSafe.ToDecimalSafe(HatPartValsSplit[2]);
                    }

                    break;
                case RobloxFileType.Head:
                case RobloxFileType.HeadNoCustomMesh:
                    if (IsHeadMesh(doc))
                    {
                        string BevelCoords = GetHeadBevel(doc);
                        if (!string.IsNullOrWhiteSpace(BevelCoords))
                        {
                            string[] BevelCoordsSplit = BevelCoords.Split(',');

                            BevelBox.Value = ConvertSafe.ToDecimalSafe(BevelCoordsSplit[0]);
                            RoundnessBox.Value = ConvertSafe.ToDecimalSafe(BevelCoordsSplit[1]);
                            BulgeBox.Value = ConvertSafe.ToDecimalSafe(BevelCoordsSplit[2]);
                            LODXBox.Value = ConvertSafe.ToDecimalSafe(BevelCoordsSplit[3]);
                            LODYBox.Value = ConvertSafe.ToDecimalSafe(BevelCoordsSplit[4]);

                            if (!string.IsNullOrWhiteSpace(BevelCoordsSplit[5]))
                            {
                                SpecialMeshTypeBox.SelectedIndex = ConvertSafe.ToInt32Safe(BevelCoordsSplit[5]);
                            }
                        }

                        string HeadScaleCoords = GetItemCoordValsNoClassSearch(doc, "Vector3", "Scale");
                        if (!string.IsNullOrWhiteSpace(HeadScaleCoords))
                        {
                            string[] HeadScaleCoordsSplit = HeadScaleCoords.Split(',');
                            XBox.Value = ConvertSafe.ToDecimalSafe(HeadScaleCoordsSplit[0]);
                            YBox.Value = ConvertSafe.ToDecimalSafe(HeadScaleCoordsSplit[1]);
                            ZBox.Value = ConvertSafe.ToDecimalSafe(HeadScaleCoordsSplit[2]);
                        }

                        string HeadColorCoords = GetItemCoordValsNoClassSearch(doc, "Vector3", "VertexColor");
                        if (!string.IsNullOrWhiteSpace(HeadColorCoords))
                        {
                            string[] HeadColorCoordsSplit = HeadColorCoords.Split(',');
                            XBox360.Value = ConvertSafe.ToDecimalSafe(HeadColorCoordsSplit[0]);
                            YBox2.Value = ConvertSafe.ToDecimalSafe(HeadColorCoordsSplit[1]);
                            ZBox2.Value = ConvertSafe.ToDecimalSafe(HeadColorCoordsSplit[2]);
                        }

                        ItemTypeListBox.SelectedIndex = 2;
                    }
                    else
                    {
                        string HeadMeshFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 0)) + ".mesh";
                        string HeadTextureFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemHeadFonts, 1)) + ".png";

                        Option1TextBox.Text = HeadMeshFilename;
                        Option2TextBox.Text = HeadTextureFilename;

                        string HeadMeshScaleCoords = GetItemCoordVals(doc, RobloxDefs.ItemHeadFonts, "Vector3", "Scale");
                        if (!string.IsNullOrWhiteSpace(HeadMeshScaleCoords))
                        {
                            string[] HeadMeshScaleCoordsSplit = HeadMeshScaleCoords.Split(',');
                            XBox.Value = ConvertSafe.ToDecimalSafe(HeadMeshScaleCoordsSplit[0]);
                            YBox.Value = ConvertSafe.ToDecimalSafe(HeadMeshScaleCoordsSplit[1]);
                            ZBox.Value = ConvertSafe.ToDecimalSafe(HeadMeshScaleCoordsSplit[2]);
                        }

                        string HeadMeshColorCoords = GetItemCoordVals(doc, RobloxDefs.ItemHeadFonts, "Vector3", "VertexColor");
                        if (!string.IsNullOrWhiteSpace(HeadMeshColorCoords))
                        {
                            string[] HeadMeshColorCoordsSplit = HeadMeshColorCoords.Split(',');
                            XBox360.Value = ConvertSafe.ToDecimalSafe(HeadMeshColorCoordsSplit[0]);
                            YBox2.Value = ConvertSafe.ToDecimalSafe(HeadMeshColorCoordsSplit[1]);
                            ZBox2.Value = ConvertSafe.ToDecimalSafe(HeadMeshColorCoordsSplit[2]);
                        }

                        ItemTypeListBox.SelectedIndex = 1;
                    }
                    break;
                case RobloxFileType.Face:
                    string FaceTextureFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemFaceTexture, 0)) + ".png";
                    Option1TextBox.Text = FaceTextureFilename;
                    break;
                case RobloxFileType.TShirt:
                    string TShirtTextureFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemTShirtTexture, 0)) + ".png";
                    Option1TextBox.Text = TShirtTextureFilename;
                    break;
                case RobloxFileType.Shirt:
                    string ShirtTextureFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemShirtTexture, 0)) + ".png";
                    Option1TextBox.Text = ShirtTextureFilename;
                    break;
                case RobloxFileType.Pants:
                    string PantsTextureFilename = GetFileBaseNameUsingSplit(GetItemFontVals(doc, RobloxDefs.ItemPantsTexture, 0)) + ".png";
                    Option1TextBox.Text = PantsTextureFilename;
                    break;
                case RobloxFileType.Package:
                    LoadPartMesh(doc, Head_ExistingFileBox, "Head");
                    LoadPartMesh(doc, Torso_ExistingFileBox, "Torso");
                    LoadPartMesh(doc, RightArm_ExistingFileBox, "Right Arm");
                    LoadPartMesh(doc, LeftArm_ExistingFileBox, "Left Arm");
                    LoadPartMesh(doc, RightLeg_ExistingFileBox, "Right Leg");
                    LoadPartMesh(doc, LeftLeg_ExistingFileBox, "Left Leg");
                    PackageNameBox.Text = GetPackageName(doc);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
            MessageBox.Show("The Item Creation SDK has experienced an error: " + ex.Message, "Novetus Item Creation SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            success = false;
        }

        return success;
    }

    private void ToggleOptionSet(Label label, TextBox textbox, Button button, string labelText, bool browseButton, bool enable = true)
    {
        label.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        textbox.ReadOnly = !enable;
        textbox.Text = "";
        button.Enabled = browseButton;
        ItemIcon.Image = Util.LoadImage("", "");
    }

    private void ToggleGroup(GroupBox groupbox, string labelText, bool enable = true)
    {
        groupbox.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        groupbox.Enabled = enable;
        ItemIcon.Image = Util.LoadImage("", "");
    }

    private void ToggleHatMeshBox(string labelText, bool enable = true)
    {
        UsesHatMeshLabel.Text = enable ? labelText : (string.IsNullOrWhiteSpace(labelText) ? "This option is disabled." : labelText);
        UsesHatMeshBox.Enabled = enable;
        UsesHatMeshBoxRefresh.Enabled = enable;

        if (enable && Directory.Exists(GlobalPaths.hatdirFonts))
        {
            UsesHatMeshBox.Items.Clear();
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
        UsesHatTexBoxRefresh.Enabled = enable;

        if (enable && Directory.Exists(GlobalPaths.hatdirTextures))
        {
            UsesHatTexBox.Items.Clear();
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

    private void TogglePackageMeshBox(ComboBox box, Button button, bool enable = true)
    {
        box.Enabled = enable;
        button.Enabled = enable;

        if (enable && Directory.Exists(GlobalPaths.extradirFonts))
        {
            box.Items.Clear();
            box.Items.Add("None");
            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.extradirFonts);
            FileInfo[] Files = dinfo.GetFiles("*.mesh");
            foreach (FileInfo file in Files)
            {
                if (file.Name.Equals(string.Empty))
                {
                    continue;
                }

                box.Items.Add(file.Name);
            }

            box.SelectedItem = "None";
        }
        else
        {
            box.Items.Clear();
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
            if(File.Exists(GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".rbxm") && !ItemEditing)
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
                        if (!File.Exists(Option1Path) && !File.Exists(GetOptionPathsForType(type)[0] + "\\" + Option1TextBox.Text) && !ItemEditing)
                        {
                            msgboxtext += "\n - The file assigned as a " + Option1Label.Text + " does not exist. Please rebrowse for the file again.";
                            passed = false;
                        }

                        if (File.Exists(GetOptionPathsForType(type)[0] + "\\" + Option1TextBox.Text) && !ItemEditing)
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
                        if (!File.Exists(Option2Path) && !File.Exists(GetOptionPathsForType(type)[1] + "\\" + Option2TextBox.Text) && !ItemEditing)
                        {
                            msgboxtext += "\n - The file assigned as a " + Option2Label.Text + " does not exist. Please rebrowse for the file again.";
                            passed = false;
                        }

                        if (File.Exists(GetOptionPathsForType(type)[1] + "\\" + Option2TextBox.Text) && !ItemEditing)
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

    private void LoadItemIfExists()
    {
        string iconpath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".png";

        if (File.Exists(iconpath))
        {
            Image icon1 = Util.LoadImage(iconpath);
            ItemIcon.Image = icon1;
        }
        else
        {
            ItemIcon.Image = null;
        }

        string descpath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + "_desc.txt";

        if (File.Exists(descpath))
        {
            DescBox.Text = File.ReadAllText(descpath);
        }
        else
        {
            DescBox.Text = "";
        }

        if (!IsReskin)
        {
            string rxbmpath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".rbxm";

            if (File.Exists(rxbmpath))
            {
                LoadItem(rxbmpath, type);
            }
            else
            {
                Reset();
            }
        }
    }

    private void InitPackageGroup(bool enable = true)
    {
        PackageNameBox.Text = "";
        TogglePackageMeshBox(Head_ExistingFileBox, Head_ExistingFileButton, enable);
        TogglePackageMeshBox(Torso_ExistingFileBox, Torso_ExistingFileButton, enable);
        TogglePackageMeshBox(LeftArm_ExistingFileBox, LeftArm_ExistingFileButton, enable);
        TogglePackageMeshBox(RightArm_ExistingFileBox, RightArm_ExistingFileButton, enable);
        TogglePackageMeshBox(LeftLeg_ExistingFileBox, LeftLeg_ExistingFileButton, enable);
        TogglePackageMeshBox(RightLeg_ExistingFileBox, RightLeg_ExistingFileButton, enable);
    }

    private void UpdateWarnings()
    {
        string warningtext = "";

        if (File.Exists(GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "") + ".rbxm"))
        {
            warningtext += "Warning: This item already exists.";
            if (ItemEditing)
            {
                warningtext += " The item's settings will be overridden since Item Editing is enabled.";
            }
            else
            {
                warningtext += " Your item will not be created with this name unless Item Editing is enabled.";
            }
        }

        Warning.Text = warningtext;
    }

    private void DeleteStrayIcons()
    {
        string itempath = GetPathForType(type) + "\\" + ItemNameBox.Text.Replace(" ", "");
        string previconpath = itempath + ".png";
        string rbxmpath = itempath + ".rbxm";

        if (File.Exists(previconpath) && !File.Exists(rbxmpath))
        {
            IOSafe.File.Delete(previconpath);
        }
    }

    private void Reset(bool full = false, bool editmode = false)
    {
        if (full)
        {
            ItemNameBox.Text = "";
            DescBox.Text = "";
            ItemTypeListBox.SelectedItem = "Hat";
            ItemIcon.Image = null;
            EditItemBox.Checked = false;
            ReskinBox.Checked = false;
            SetupSDK();
        }

        if (!editmode)
        {
            UsesHatMeshBox.SelectedItem = "None";
            UsesHatTexBox.SelectedItem = "None";
            SpecialMeshTypeBox.SelectedItem = "Head";
            Option1TextBox.Text = "";
            Option2TextBox.Text = "";
            XBox.Value = 1;
            YBox.Value = 1;
            ZBox.Value = 1;
            XBox360.Value = 1;
            YBox2.Value = 1;
            ZBox2.Value = 1;
            XBoxOne.Value = 1;
            YBox3.Value = 1;
            ZBox3.Value = 1;
            BevelBox.Value = 0M;
            RoundnessBox.Value = 0M;
            BulgeBox.Value = 0M;
            LODXBox.Value = 1M;
            LODYBox.Value = 2M;
            rightXBox.Value = 1;
            rightYBox.Value = 0;
            rightZBox.Value = 0;
            upXBox.Value = 0;
            upYBox.Value = 1;
            upZBox.Value = 0;
            forwardXBox.Value = 0;
            forwardYBox.Value = 0;
            forwardZBox.Value = -1;
            transparencyBox.Value = 0;
            reflectivenessBox.Value = 0;
            partColorID = 194;
            partColorLabel.Text = partColorID.ToString();
            InitPackageGroup();
        }
    }
    #endregion
}
#endregion

#region Vector3 (Legacy)
public class Vector3Legacy
{
    public double X;
    public double Y;
    public double Z;

    public Vector3Legacy(double aX, double aY, double aZ)
    {
        X = aX;
        Y = aY;
        Z = aZ;
    }
}
#endregion

