#region Usings
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

#region CharacterCustomization - Extended
public partial class CharacterCustomizationExtended : Form
{
    #region Constructor
    public CharacterCustomizationExtended()
	{
		InitializeComponent();
        InitExtendedForm();
        characterCustomizationForm.InitColors();

        Size = new Size(671, 337);
        panel2.Size = new Size(568, 302);
    }
    #endregion

    #region Form Events
    void CharacterCustomizationLoad(object sender, EventArgs e)
	{
        characterCustomizationForm.InitForm();
    }

    void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeTabs();
    }

    void CharacterCustomizationClose(object sender, CancelEventArgs e)
    {
        GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");
        GlobalFuncs.ReloadLoadoutValue();
    }

    #region Hats

    void ListBox1SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Directory.Exists(GlobalPaths.hatdir))
        {
            GlobalVars.UserCustomization.Hat1 = listBox1.SelectedItem.ToString();

            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Hat1,
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
            GlobalVars.UserCustomization.Hat2 = listBox2.SelectedItem.ToString();

            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Hat2,
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
            GlobalVars.UserCustomization.Hat3 = listBox3.SelectedItem.ToString();

            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Hat3,
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
            GlobalVars.UserCustomization.Face = previtem;

            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Face,
                            GlobalPaths.facedir,
                            "DefaultFace",
                            pictureBox4,
                            textBox6,
                            listBox4,
                            false,
                            FaceTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, FaceTypeBox.SelectedItem.ToString()) : null
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
            GlobalVars.UserCustomization.Face = characterCustomizationForm.Custom_Face_URL + FaceIDBox.Text;
            FaceIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.Face = listBox4.SelectedItem.ToString();
        }

        CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Face,
                            GlobalPaths.facedir,
                            "DefaultFace",
                            pictureBox4,
                            textBox6,
                            listBox4,
                            false,
                            FaceTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, FaceTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void FaceTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Provider faceProvider = null;

        if (FaceTypeBox.SelectedItem != null)
        {
            faceProvider = OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, FaceTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_Face_URL = faceProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(FaceIDBox.Text))
        {
            GlobalVars.UserCustomization.Face = characterCustomizationForm.Custom_Face_URL + FaceIDBox.Text;
            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Face,
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
            GlobalVars.UserCustomization.TShirt = previtem;

            CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.TShirt,
                        GlobalPaths.tshirtdir,
                        "NoTShirt",
                        pictureBox5,
                        textBox7,
                        listBox5,
                        false,
                        TShirtsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, TShirtsTypeBox.SelectedItem.ToString()) : null
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
            GlobalVars.UserCustomization.TShirt = characterCustomizationForm.Custom_T_Shirt_URL + TShirtsIDBox.Text;
            TShirtsIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.TShirt = listBox5.SelectedItem.ToString();
        }

        CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.TShirt,
                            GlobalPaths.tshirtdir,
                            "NoTShirt",
                            pictureBox5,
                            textBox7,
                            listBox5,
                            false,
                            TShirtsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, TShirtsTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void TShirtsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Provider tShirtProvider = null;

        if (TShirtsTypeBox.SelectedItem != null)
        {
            tShirtProvider = OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, TShirtsTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_T_Shirt_URL = tShirtProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(TShirtsIDBox.Text))
        {
            GlobalVars.UserCustomization.TShirt = characterCustomizationForm.Custom_T_Shirt_URL + TShirtsIDBox.Text;
            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.TShirt,
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
            GlobalVars.UserCustomization.Shirt = previtem;

            CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Shirt,
                        GlobalPaths.shirtdir,
                        "NoShirt",
                        pictureBox6,
                        textBox8,
                        listBox6,
                        false,
                        ShirtsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, ShirtsTypeBox.SelectedItem.ToString()) : null
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
            GlobalVars.UserCustomization.Shirt = characterCustomizationForm.Custom_Shirt_URL + ShirtsIDBox.Text;
            ShirtsIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.Shirt = listBox6.SelectedItem.ToString();
        }

        CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Shirt,
                            GlobalPaths.shirtdir,
                            "NoShirt",
                            pictureBox6,
                            textBox8,
                            listBox6,
                            false,
                            ShirtsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, ShirtsTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void ShirtsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Provider shirtProvider = null;

        if (ShirtsTypeBox.SelectedItem != null)
        {
            shirtProvider = OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, ShirtsTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_Shirt_URL = shirtProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(ShirtsIDBox.Text))
        {
            GlobalVars.UserCustomization.Shirt = characterCustomizationForm.Custom_Shirt_URL + ShirtsIDBox.Text;
            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Shirt,
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
            GlobalVars.UserCustomization.Pants = previtem;

            CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Pants,
                        GlobalPaths.pantsdir,
                        "NoPants",
                        pictureBox7,
                        textBox9,
                        listBox7,
                        false,
                        PantsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, PantsTypeBox.SelectedItem.ToString()) : null
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
            GlobalVars.UserCustomization.Pants = characterCustomizationForm.Custom_Pants_URL + PantsIDBox.Text;
            PantsIDBox.Focus();
        }
        else
        {
            GlobalVars.UserCustomization.Pants = listBox7.SelectedItem.ToString();
        }

        CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Pants,
                            GlobalPaths.pantsdir,
                            "NoPants",
                            pictureBox7,
                            textBox9,
                            listBox7,
                            false,
                            PantsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, PantsTypeBox.SelectedItem.ToString()) : null
                        );
    }

    private void PantsTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Provider pantsProvider = null;

        if (PantsTypeBox.SelectedItem != null)
        {
            pantsProvider = OnlineClothing.FindContentProviderByName(characterCustomizationForm.contentProviders, PantsTypeBox.SelectedItem.ToString());
            characterCustomizationForm.Custom_Pants_URL = pantsProvider.URL;
        }

        if (!string.IsNullOrWhiteSpace(PantsIDBox.Text))
        {
            GlobalVars.UserCustomization.Pants = characterCustomizationForm.Custom_Pants_URL + PantsIDBox.Text;
            CustomizationFuncs.ChangeItem(
                            GlobalVars.UserCustomization.Pants,
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
            GlobalVars.UserCustomization.Head = listBox8.SelectedItem.ToString();

            CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Head,
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
            GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();

            CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Extra,
                        GlobalPaths.extradir,
                        "NoExtra",
                        pictureBox9,
                        textBox10,
                        listBox9,
                        false
                    );

            if (GlobalVars.UserCustomization.ShowHatsInExtra)
            {
                CustomizationFuncs.ChangeItem(
                    GlobalVars.UserCustomization.Extra,
                    GlobalPaths.hatdir,
                    "NoHat",
                    pictureBox9,
                    textBox10,
                    listBox9,
                    false,
                    GlobalVars.UserCustomization.ShowHatsInExtra
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
        GlobalVars.UserCustomization.ShowHatsInExtra = checkBox1.Checked;
        listBox9.Items.Clear();

        CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Extra,
                        GlobalPaths.extradir,
                        "NoExtra",
                        pictureBox9,
                        textBox10,
                        listBox9,
                        true
                    );

        if (GlobalVars.UserCustomization.ShowHatsInExtra)
        {
            CustomizationFuncs.ChangeItem(
                GlobalVars.UserCustomization.Extra,
                GlobalPaths.hatdir,
                "NoHat",
                pictureBox9,
                textBox10,
                listBox9,
                true,
                GlobalVars.UserCustomization.ShowHatsInExtra
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
        characterCustomizationForm.SelectedPart = "Head";
        label2.Text = characterCustomizationForm.SelectedPart;
    }

    void Button2Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectedPart = "Torso";
        label2.Text = characterCustomizationForm.SelectedPart;
    }

    void Button3Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectedPart = "Right Arm";
        label2.Text = characterCustomizationForm.SelectedPart;
    }

    void Button4Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectedPart = "Left Arm";
        label2.Text = characterCustomizationForm.SelectedPart;
    }

    void Button5Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectedPart = "Right Leg";
        label2.Text = characterCustomizationForm.SelectedPart;
    }

    void Button6Click(object sender, EventArgs e)
    {
        characterCustomizationForm.SelectedPart = "Left Leg";
        label2.Text = characterCustomizationForm.SelectedPart;
    }

    void Button7Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(1);
    }

    void Button8Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(208);
    }

    void Button9Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(194);
    }

    void Button10Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(199);
    }

    void Button14Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(26);
    }

    void Button13Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(21);
    }

    void Button12Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(24);
    }

    void Button11Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(226);
    }

    void Button18Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(23);
    }

    void Button17Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(107);
    }

    void Button16Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(102);
    }

    void Button15Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(11);
    }

    void Button22Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(45);
    }

    void Button21Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(135);
    }

    void Button20Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(106);
    }

    void Button19Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(105);
    }

    void Button26Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(141);
    }

    void Button25Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(28);
    }

    void Button24Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(37);
    }

    void Button23Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(119);
    }

    void Button30Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(29);
    }

    void Button29Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(151);
    }

    void Button28Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(38);
    }

    void Button27Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(192);
    }

    void Button34Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(104);
    }

    void Button33Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(9);
    }

    void Button32Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(101);
    }

    void Button31Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(5);
    }

    void Button38Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(153);
    }

    void Button37Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(217);
    }

    void Button36Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(18);
    }

    void Button35Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(125);
    }

    private void button69_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(22);
    }

    private void button70_Click(object sender, EventArgs e)
    {
        characterCustomizationForm.ChangeColorOfPart(128);
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
        GlobalVars.UserCustomization.Icon = "BC";
        label5.Text = GlobalVars.UserCustomization.Icon;
    }

    void Button53Click(object sender, EventArgs e)
    {
        GlobalVars.UserCustomization.Icon = "TBC";
        label5.Text = GlobalVars.UserCustomization.Icon;
    }

    void Button54Click(object sender, EventArgs e)
    {
        GlobalVars.UserCustomization.Icon = "OBC";
        label5.Text = GlobalVars.UserCustomization.Icon;
    }

    void Button55Click(object sender, EventArgs e)
    {
        GlobalVars.UserCustomization.Icon = "NBC";
        label5.Text = GlobalVars.UserCustomization.Icon;
    }

    private void button60_Click(object sender, EventArgs e)
    {
        IconLoader icon = new IconLoader();
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

        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradirIcons + "\\" + GlobalVars.UserConfiguration.PlayerName + ".png", GlobalPaths.extradir + "\\NoExtra.png");
        pictureBox10.Image = icon1;
    }
    #endregion

    #region Navigation
    private void button72_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage1;
    }

    private void button73_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage2;
    }

    private void button74_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage8;
    }

    private void button75_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage3;
    }

    private void button76_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage4;
    }

    private void button77_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage5;
    }

    private void button78_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage6;
    }

    private void button79_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage9;
    }

    private void button80_Click(object sender, EventArgs e)
    {
        tabControl1.SelectedTab = tabPage7;
    }

    private void button81_Click(object sender, EventArgs e)
    {
        tabControl2.SelectedTab = tabPage10;
    }

    private void button82_Click(object sender, EventArgs e)
    {
        tabControl2.SelectedTab = tabPage11;
    }

    private void button83_Click(object sender, EventArgs e)
    {
        tabControl2.SelectedTab = tabPage12;
    }
    #endregion

    void Button43Click(object sender, EventArgs e)
    {
        CustomizationFuncs.Launch3DView();
    }

    private void button71_Click(object sender, EventArgs e)
    {
        GlobalFuncs.Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
        MessageBox.Show("Outfit Saved!");
    }

    void TextBox1TextChanged(object sender, EventArgs e)
    {
        GlobalVars.UserCustomization.CharacterID = textBox1.Text;
    }
    #endregion
}
#endregion
