#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

#region CharacterCustomization - Compact
public partial class CharacterCustomizationCompact : Form
{
    #region Constructor
    public CharacterCustomizationCompact()
    {
        InitializeComponent();
        InitCompactForm();
    }
    #endregion

    #region Form Events
    void CharacterCustomizationLoad(object sender, EventArgs e)
    {
        characterCustomizationForm.InitForm();
        CenterToScreen();
    }

    void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeTabs();
    }

    void CharacterCustomizationClose(object sender, CancelEventArgs e)
    {
        characterCustomizationForm.CloseEvent();
    }

    #region Hats
    void ListBox1SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.hatdir))
        {
            GlobalVars.UserCustomization.SaveSetting("Hat1", listBox1.SelectedItem.ToString());

            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Hat1"),
                            GlobalPaths.hatdir,
                            "NoHat",
                            pictureBox1,
                            textBox2,
                            listBox1,
                            false
                        );
        }
    }

    void ListBox2SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.hatdir))
        {
            GlobalVars.UserCustomization.SaveSetting("Hat2", listBox2.SelectedItem.ToString());

            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Hat2"),
                            GlobalPaths.hatdir,
                            "NoHat",
                            pictureBox2,
                            textBox3,
                            listBox2,
                            false
                        );
        }
    }

    void ListBox3SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.hatdir))
        {
            GlobalVars.UserCustomization.SaveSetting("Hat3", listBox3.SelectedItem.ToString());

            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Hat3"),
                            GlobalPaths.hatdir,
                            "NoHat",
                            pictureBox3,
                            textBox4,
                            listBox3,
                            false
                        );
        }
    }

    void Button41Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.hatdir))
        {
            Random random = new Random();
            int randomHat1 = random.Next(listBox1.Items.Count);
            listBox1.SelectedItem = listBox1.Items[randomHat1];

            int randomHat2 = random.Next(listBox2.Items.Count);
            listBox2.SelectedItem = listBox1.Items[randomHat2];

            int randomHat3 = random.Next(listBox3.Items.Count);
            listBox3.SelectedItem = listBox1.Items[randomHat3];
        }
    }

    void Button42Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.hatdir))
        {
            listBox1.SelectedItem = "NoHat.rbxm";

            listBox2.SelectedItem = "NoHat.rbxm";

            listBox3.SelectedItem = "NoHat.rbxm";
        }
    }
    #endregion

    #region Faces

    void ListBox4SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.facedir))
        {
            string previtem = listBox4.SelectedItem.ToString();
            if (!FaceIDBox.Focused && !FaceTypeBox.Focused)
            {
                FaceIDBox.Text = "";
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
                {
                    FaceTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
                }
            }
            listBox4.SelectedItem = previtem;
            GlobalVars.UserCustomization.SaveSetting("Face", previtem);

            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Face"),
                            GlobalPaths.facedir,
                            "DefaultFace",
                            pictureBox4,
                            textBox6,
                            listBox4,
                            false,
                            FaceTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, FaceTypeBox.SelectedItem.ToString()) : null
                        );
        }
    }

    void Button45Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.facedir))
        {
            FaceIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                FaceTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            Random random = new Random();
            int randomFace1 = random.Next(listBox4.Items.Count);
            listBox4.SelectedItem = listBox4.Items[randomFace1];
        }
    }

    void Button44Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.facedir))
        {
            FaceIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                FaceTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            listBox4.SelectedItem = "DefaultFace.rbxm";
        }
    }

    private void FaceIDBox_TextChanged(object sender, EventArgs e)
    {
        listBox4.SelectedItem = "DefaultFace.rbxm";

        if (!string.IsNullOrWhiteSpace(FaceIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("Face", characterCustomizationForm.Custom_Face_URL + FaceIDBox.Text);
            FaceIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.SaveSetting("Face", listBox4.SelectedItem.ToString());
        }

        characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Face"),
                            GlobalPaths.facedir,
                            "DefaultFace",
                            pictureBox4,
                            textBox6,
                            listBox4,
                            false,
                            FaceTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, FaceTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void FaceTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ContentProvider faceProvider = null;

        if (FaceTypeBox.SelectedItem != null)
        {
            faceProvider = ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, FaceTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_Face_URL = faceProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(FaceIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("Face", characterCustomizationForm.Custom_Face_URL + FaceIDBox.Text);
            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Face"),
                            GlobalPaths.facedir,
                            "DefaultFace",
                            pictureBox4,
                            textBox6,
                            listBox4,
                            false,
                            faceProvider
                        );
        }
    }

    #endregion

    #region T-Shirt

    void ListBox5SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.tshirtdir))
        {
            string previtem = listBox5.SelectedItem.ToString();
            if (!TShirtsIDBox.Focused && !TShirtsTypeBox.Focused)
            {
                TShirtsIDBox.Text = "";
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
                {
                    TShirtsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
                }
            }
            listBox5.SelectedItem = previtem;
            GlobalVars.UserCustomization.SaveSetting("TShirt", previtem);

            characterCustomizationForm.ChangeItem(
                        GlobalVars.UserCustomization.ReadSetting("TShirt"),
                        GlobalPaths.tshirtdir,
                        "NoTShirt",
                        pictureBox5,
                        textBox7,
                        listBox5,
                        false,
                        TShirtsTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, TShirtsTypeBox.SelectedItem.ToString()) : null
                    );
        }
    }

    void Button47Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.tshirtdir))
        {
            TShirtsIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                TShirtsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            Random random = new Random();
            int randomTShirt1 = random.Next(listBox5.Items.Count);
            listBox5.SelectedItem = listBox5.Items[randomTShirt1];
        }
    }

    void Button46Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.tshirtdir))
        {
            TShirtsIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                TShirtsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            listBox5.SelectedItem = "NoTShirt.rbxm";
        }
    }

    private void TShirtsIDBox_TextChanged(object sender, EventArgs e)
    {
        listBox5.SelectedItem = "NoTShirt.rbxm";

        if (!string.IsNullOrWhiteSpace(TShirtsIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("TShirt", characterCustomizationForm.Custom_T_Shirt_URL + TShirtsIDBox.Text);
            TShirtsIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.SaveSetting("TShirt", listBox5.SelectedItem.ToString());
        }

        characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("TShirt"),
                            GlobalPaths.tshirtdir,
                            "NoTShirt",
                            pictureBox5,
                            textBox7,
                            listBox5,
                            false,
                            TShirtsTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, TShirtsTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void TShirtsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ContentProvider tShirtProvider = null;

        if (TShirtsTypeBox.SelectedItem != null)
        {
            tShirtProvider = ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, TShirtsTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_T_Shirt_URL = tShirtProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(TShirtsIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("TShirt", characterCustomizationForm.Custom_T_Shirt_URL + TShirtsIDBox.Text);
            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("TShirt"),
                            GlobalPaths.tshirtdir,
                            "NoTShirt",
                            pictureBox5,
                            textBox7,
                            listBox5,
                            false,
                            tShirtProvider
                        );
        }
    }
    #endregion

    #region Shirt

    void ListBox6SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.shirtdir))
        {
            string previtem = listBox6.SelectedItem.ToString();
            if (!ShirtsIDBox.Focused && !ShirtsTypeBox.Focused)
            {
                ShirtsIDBox.Text = "";
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
                {
                    ShirtsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
                }
            }
            listBox6.SelectedItem = previtem;
            GlobalVars.UserCustomization.SaveSetting("Shirt", previtem);

            characterCustomizationForm.ChangeItem(
                        GlobalVars.UserCustomization.ReadSetting("Shirt"),
                        GlobalPaths.shirtdir,
                        "NoShirt",
                        pictureBox6,
                        textBox8,
                        listBox6,
                        false,
                        ShirtsTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, ShirtsTypeBox.SelectedItem.ToString()) : null
                    );
        }
    }

    void Button49Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.shirtdir))
        {
            ShirtsIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                ShirtsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            Random random = new Random();
            int randomShirt1 = random.Next(listBox6.Items.Count);
            listBox6.SelectedItem = listBox6.Items[randomShirt1];
        }
    }

    void Button48Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.shirtdir))
        {
            ShirtsIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                ShirtsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            listBox6.SelectedItem = "NoShirt.rbxm";
        }
    }

    private void ShirtsIDBox_TextChanged(object sender, EventArgs e)
    {
        listBox6.SelectedItem = "NoShirt.rbxm";

        if (!string.IsNullOrWhiteSpace(ShirtsIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("Shirt", characterCustomizationForm.Custom_Shirt_URL + ShirtsIDBox.Text);
            ShirtsIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.SaveSetting("Shirt", listBox6.SelectedItem.ToString());
        }

        characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Shirt"),
                            GlobalPaths.shirtdir,
                            "NoShirt",
                            pictureBox6,
                            textBox8,
                            listBox6,
                            false,
                            ShirtsTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, ShirtsTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void ShirtsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ContentProvider shirtProvider = null;

        if (ShirtsTypeBox.SelectedItem != null)
        {
            shirtProvider = ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, ShirtsTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_Shirt_URL = shirtProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(ShirtsIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("Shirt", characterCustomizationForm.Custom_Shirt_URL + ShirtsIDBox.Text);
            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Shirt"),
                            GlobalPaths.shirtdir,
                            "NoShirt",
                            pictureBox6,
                            textBox8,
                            listBox6,
                            false,
                            shirtProvider
                        );
        }
    }
    #endregion

    #region Pants

    void ListBox7SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.pantsdir))
        {
            string previtem = listBox7.SelectedItem.ToString();
            if (!PantsIDBox.Focused && !PantsTypeBox.Focused)
            {
                PantsIDBox.Text = "";
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
                {
                    PantsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
                }
            }
            listBox7.SelectedItem = previtem;
            GlobalVars.UserCustomization.SaveSetting("Pants", previtem);

            characterCustomizationForm.ChangeItem(
                        GlobalVars.UserCustomization.ReadSetting("Pants"),
                        GlobalPaths.pantsdir,
                        "NoPants",
                        pictureBox7,
                        textBox9,
                        listBox7,
                        false,
                        PantsTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, PantsTypeBox.SelectedItem.ToString()) : null
                    );
        }
    }

    void Button51Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.pantsdir))
        {
            PantsIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                PantsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            Random random = new Random();
            int randomPants1 = random.Next(listBox7.Items.Count);
            listBox7.SelectedItem = listBox7.Items[randomPants1];
        }
    }

    void Button50Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.pantsdir))
        {
            PantsIDBox.Text = "";
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                PantsTypeBox.SelectedItem = characterCustomizationForm.contentProviders.FirstOrDefault().Name;
            }
            listBox7.SelectedItem = "NoPants.rbxm";
        }
    }

    private void PantsIDBox_TextChanged(object sender, EventArgs e)
    {
        listBox7.SelectedItem = "NoPants.rbxm";

        if (!string.IsNullOrWhiteSpace(PantsIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("Pants", characterCustomizationForm.Custom_Pants_URL + PantsIDBox.Text);
            PantsIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.SaveSetting("Pants", listBox7.SelectedItem.ToString());
        }

        characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Pants"),
                            GlobalPaths.pantsdir,
                            "NoPants",
                            pictureBox7,
                            textBox9,
                            listBox7,
                            false,
                            PantsTypeBox.SelectedItem != null ? ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, PantsTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void PantsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ContentProvider pantsProvider = null;

        if (PantsTypeBox.SelectedItem != null)
        {
            pantsProvider = ContentProvider.FindContentProviderByName(characterCustomizationForm.contentProviders, PantsTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_Pants_URL = pantsProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(PantsIDBox.Text))
        {
            GlobalVars.UserCustomization.SaveSetting("Pants", characterCustomizationForm.Custom_Pants_URL + PantsIDBox.Text);
            characterCustomizationForm.ChangeItem(
                            GlobalVars.UserCustomization.ReadSetting("Pants"),
                            GlobalPaths.pantsdir,
                            "NoPants",
                            pictureBox7,
                            textBox9,
                            listBox7,
                            false,
                            pantsProvider
                        );
        }
    }
    #endregion

    #region Head

    void ListBox8SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.headdir))
        {
            GlobalVars.UserCustomization.SaveSetting("Head", listBox8.SelectedItem.ToString());

            characterCustomizationForm.ChangeItem(
                        GlobalVars.UserCustomization.ReadSetting("Head"),
                        GlobalPaths.headdir,
                        "DefaultHead",
                        pictureBox8,
                        textBox5,
                        listBox8,
                        false
                    );
        }
    }

    void Button57Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.headdir))
        {
            Random random = new Random();
            int randomHead1 = random.Next(listBox8.Items.Count);
            listBox8.SelectedItem = listBox8.Items[randomHead1];
        }
    }

    void Button56Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.headdir))
        {
            listBox8.SelectedItem = "DefaultHead.rbxm";
        }
    }
    #endregion

    #region Extra
    void ListBox9SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.extradir))
        {
            GlobalVars.UserCustomization.SaveSetting("Extra", listBox9.SelectedItem.ToString());

            characterCustomizationForm.ChangeItem(
                        GlobalVars.UserCustomization.ReadSetting("Extra"),
                        GlobalPaths.extradir,
                        "NoExtra",
                        pictureBox9,
                        textBox10,
                        listBox9,
                        false
                    );

            if (GlobalVars.UserCustomization.ReadSettingBool("ShowHatsInExtra"))
            {
                characterCustomizationForm.ChangeItem(
                    GlobalVars.UserCustomization.ReadSetting("Extra"),
                    GlobalPaths.hatdir,
                    "NoExtra",
                    pictureBox9,
                    textBox10,
                    listBox9,
                    false,
                    GlobalVars.UserCustomization.ReadSettingBool("ShowHatsInExtra"),
                    GlobalPaths.extradir
                );
            }
        }
    }

    void Button59Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.extradir))
        {
            Random random = new Random();
            int randomItem1 = random.Next(listBox9.Items.Count);
            listBox9.SelectedItem = listBox9.Items[randomItem1];
        }
    }

    void Button58Click(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.extradir))
        {
            listBox9.SelectedItem = "NoExtra.rbxm";
        }
    }

    void CheckBox1CheckedChanged(object sender, EventArgs e)
    {
        GlobalVars.UserCustomization.SaveSettingBool("ShowHatsInExtra", checkBox1.Checked);
        listBox9.Items.Clear();

        characterCustomizationForm.ChangeItem(
                        GlobalVars.UserCustomization.ReadSetting("Extra"),
                        GlobalPaths.extradir,
                        "NoExtra",
                        pictureBox9,
                        textBox10,
                        listBox9,
                        true
                    );

        if (GlobalVars.UserCustomization.ReadSettingBool("ShowHatsInExtra"))
        {
            characterCustomizationForm.ChangeItem(
                GlobalVars.UserCustomization.ReadSetting("Extra"),
                GlobalPaths.hatdir,
                "NoExtra",
                pictureBox9,
                textBox10,
                listBox9,
                true,
                GlobalVars.UserCustomization.ReadSettingBool("ShowHatsInExtra"),
                GlobalPaths.extradir
            );
        }
        else
        {
            listBox9.SelectedItem = "NoExtra.rbxm";
        }
    }
    #endregion

    #region Body Colors

    void Button1Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectPart("Head");
    }

    void Button2Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectPart("Torso");
    }

    void Button3Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectPart("Right Arm");
    }

    void Button4Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectPart("Left Arm");
    }

    void Button5Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectPart("Right Leg");
    }

    void Button6Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectPart("Left Leg");
    }

    void Button39Click(object sender, EventArgs e)
    {
        characterCustomizationForm.RandomizeColors();
    }

    void Button40Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ResetColors();
    }

    private void button61_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(24, 194, 24, 24, 119, 119);
    }

    private void button62_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(24, 101, 24, 24, 9, 9);
    }

    private void button63_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(24, 23, 24, 24, 119, 119);
    }

    private void button64_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(24, 101, 24, 24, 119, 119);
    }

    private void button68_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(24, 45, 24, 24, 119, 119);
    }

    private void button67_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(106, 194, 106, 106, 119, 119);
    }

    private void button66_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(106, 119, 106, 106, 119, 119);
    }

    private void button65_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ApplyPreset(9, 194, 9, 9, 119, 119);
    }
    #endregion

    #region Icon
    void Button52Click(object sender, EventArgs e)
    {
        IconURLBox.Text = "";
        GlobalVars.UserCustomization.SaveSetting("Icon", "BC");
        label5.Text = GlobalVars.UserCustomization.ReadSetting("Icon");
        characterCustomizationForm.SaveOutfit(false);
    }

    void Button53Click(object sender, EventArgs e)
    {
        IconURLBox.Text = "";
        GlobalVars.UserCustomization.SaveSetting("Icon", "TBC");
        label5.Text = GlobalVars.UserCustomization.ReadSetting("Icon");
        characterCustomizationForm.SaveOutfit(false);
    }

    void Button54Click(object sender, EventArgs e)
    {
        IconURLBox.Text = "";
        GlobalVars.UserCustomization.SaveSetting("Icon", "OBC");
        label5.Text = GlobalVars.UserCustomization.ReadSetting("Icon");
        characterCustomizationForm.SaveOutfit(false);
    }

    void Button55Click(object sender, EventArgs e)
    {
        IconURLBox.Text = "";
        GlobalVars.UserCustomization.SaveSetting("Icon", "NBC");
        label5.Text = GlobalVars.UserCustomization.ReadSetting("Icon");
        characterCustomizationForm.SaveOutfit(false);
    }

    private void button60_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.LaunchLoadLocalIcon();
    }

    private void IconURLBox_TextChanged(object sender, EventArgs e)
    {
        characterCustomizationForm.LoadRemoteIcon();
    }
    #endregion

    void Button43Click(object sender, EventArgs e)
    {
        characterCustomizationForm.Launch3DView();
    }

    private void button71_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SaveOutfit();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.LoadOutfit();
    }

    void TextBox1TextChanged(object sender, EventArgs e)
    {
        GlobalVars.UserCustomization.SaveSetting("CharacterID", textBox1.Text);
        characterCustomizationForm.SaveOutfit(false);
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        characterCustomizationForm.ColorButton();
    }
    #endregion
}
#endregion
