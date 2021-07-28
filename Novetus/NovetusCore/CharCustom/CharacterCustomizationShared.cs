#region Usings
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
#endregion

#region CharacterCustomization - Shared
class CharacterCustomizationShared
{
    #region Variables
    public string SelectedPart = "Head";
    public string Custom_T_Shirt_URL = "";
    public string Custom_Shirt_URL = "";
    public string Custom_Pants_URL = "";
    public string Custom_Face_URL = "";
    public List<VarStorage.PartColors> PartColorList;
    public Provider[] contentProviders;
    public Form Parent;
    public Settings.UIOptions.Style FormStyle;
    public Button WhiteButton, LightStoneGreyButton, MediumStoneGreyButton, DarkStoneGreyButton, BlackButton,
        BrightRedButton, BrightYellowButton, CoolYellowButton, BrightBlueButton, BrightBluishGreenButton, MediumBlueButton,
        PastelBlueButton, LightBlueButton, SandBlueButton, BrightOrangeButton, BrightYellowishOrangeButton, EarthGreenButton, DarkGreenButton,
        BrightGreenButton, BrightYellowishGreenButton, MediumGreenButton, SandGreenButton, DarkOrangeButton, ReddishBrownButton, BrightVioletButton,
        LightReddishVioletButton, MediumRedButton, BrickYellowButton, SandRedButton, BrownButton, NougatButton, LightOrangeButton, MediumReddishViolet,
        DarkNougatButton, HeadButton, TorsoButton, LeftArmButton, RightArmButton, LeftLegButton, RightLegButton;
    public ComboBox FaceTypeBox, TShirtsTypeBox, ShirtsTypeBox, PantsTypeBox;
    public TextBox FaceIDBox, TShirtsIDBox, ShirtsIDBox, PantsIDBox, CharacterIDBox, Hat1Desc, Hat2Desc, Hat3Desc, HeadDesc, TShirtDesc, ShirtDesc, PantsDesc, FaceDesc, ExtraItemDesc;
    public CheckBox ShowHatsInExtraBox;
    public Label SelectedPartLabel, IconLabel;
    public TabControl CharacterTabControl;
    public Panel OrganizationPanel;
    public ListBox Hat1List, Hat2List, Hat3List, HeadList, TShirtList, ShirtList, PantsList, FaceList, ExtraItemList;
    public PictureBox Hat1Image, Hat2Image, Hat3Image, HeadImage, TShirtImage, ShirtImage, PantsImage, FaceImage, ExtraItemImage, IconImage;
    #endregion

    #region Constructor
    public CharacterCustomizationShared()
    {

    }

    public void InitColors()
    {
        PartColorList = new List<VarStorage.PartColors>()
        {
            //White
            new VarStorage.PartColors{ ColorID = 1, ButtonColor = WhiteButton.BackColor },
            //Light stone grey
            new VarStorage.PartColors{ ColorID = 208, ButtonColor = LightStoneGreyButton.BackColor },
            //Medium stone grey
            new VarStorage.PartColors{ ColorID = 194, ButtonColor = MediumStoneGreyButton.BackColor },
            //Dark stone grey
            new VarStorage.PartColors{ ColorID = 199, ButtonColor = DarkStoneGreyButton.BackColor },
            //Black
            new VarStorage.PartColors{ ColorID = 26, ButtonColor = BlackButton.BackColor },
            //Bright red
            new VarStorage.PartColors{ ColorID = 21, ButtonColor = BrightRedButton.BackColor },
            //Bright yellow
            new VarStorage.PartColors{ ColorID = 24, ButtonColor = BrightYellowButton.BackColor },
            //Cool yellow
            new VarStorage.PartColors{ ColorID = 226, ButtonColor = CoolYellowButton.BackColor },
            //Bright blue
            new VarStorage.PartColors{ ColorID = 23, ButtonColor = BrightBlueButton.BackColor },
            //Bright bluish green
            new VarStorage.PartColors{ ColorID = 107, ButtonColor = BrightBluishGreenButton.BackColor },
            //Medium blue
            new VarStorage.PartColors{ ColorID = 102, ButtonColor = MediumBlueButton.BackColor },
            //Pastel Blue
            new VarStorage.PartColors{ ColorID = 11, ButtonColor = PastelBlueButton.BackColor },
            //Light blue
            new VarStorage.PartColors{ ColorID = 45, ButtonColor = LightBlueButton.BackColor },
            //Sand blue
            new VarStorage.PartColors{ ColorID = 135, ButtonColor = SandBlueButton.BackColor },
            //Bright orange
            new VarStorage.PartColors{ ColorID = 106, ButtonColor = BrightOrangeButton.BackColor },
            //Br. yellowish orange
            new VarStorage.PartColors{ ColorID = 105, ButtonColor = BrightYellowishOrangeButton.BackColor },
            //Earth green
            new VarStorage.PartColors{ ColorID = 141, ButtonColor = EarthGreenButton.BackColor },
            //Dark green
            new VarStorage.PartColors{ ColorID = 28, ButtonColor = DarkGreenButton.BackColor },
            //Bright green
            new VarStorage.PartColors{ ColorID = 37, ButtonColor = BrightGreenButton.BackColor },
            //Br. yellowish green
            new VarStorage.PartColors{ ColorID = 119, ButtonColor = BrightYellowishGreenButton.BackColor },
            //Medium green
            new VarStorage.PartColors{ ColorID = 29, ButtonColor = MediumGreenButton.BackColor },
            //Sand green
            new VarStorage.PartColors{ ColorID = 151, ButtonColor = SandGreenButton.BackColor },
            //Dark orange
            new VarStorage.PartColors{ ColorID = 38, ButtonColor = DarkOrangeButton.BackColor },
            //Reddish brown
            new VarStorage.PartColors{ ColorID = 192, ButtonColor = ReddishBrownButton.BackColor },
            //Bright violet
            new VarStorage.PartColors{ ColorID = 104, ButtonColor = BrightVioletButton.BackColor },
            //Light reddish violet
            new VarStorage.PartColors{ ColorID = 9, ButtonColor = LightReddishVioletButton.BackColor },
            //Medium red
            new VarStorage.PartColors{ ColorID = 101, ButtonColor = MediumRedButton.BackColor },
            //Brick yellow
            new VarStorage.PartColors{ ColorID = 5, ButtonColor = BrickYellowButton.BackColor },
            //Sand red
            new VarStorage.PartColors{ ColorID = 153, ButtonColor = SandRedButton.BackColor },
            //Brown
            new VarStorage.PartColors{ ColorID = 217, ButtonColor = BrownButton.BackColor },
            //Nougat
            new VarStorage.PartColors{ ColorID = 18, ButtonColor = NougatButton.BackColor },
            //Light orange
            new VarStorage.PartColors{ ColorID = 125, ButtonColor = LightOrangeButton.BackColor },
            // RARE 2006 COLORS!!
            //Med. reddish violet
            new VarStorage.PartColors{ ColorID = 22, ButtonColor = MediumReddishViolet.BackColor },
            //Dark nougat
            new VarStorage.PartColors{ ColorID = 128, ButtonColor = DarkNougatButton.BackColor }
        };
    }
    #endregion

    #region Form Event Functions
    public void InitForm()
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
        {
            contentProviders = OnlineClothing.GetContentProviders();

            for (int i = 0; i < contentProviders.Length; i++)
            {
                FaceTypeBox.Items.Add(contentProviders[i].Name);
                TShirtsTypeBox.Items.Add(contentProviders[i].Name);
                ShirtsTypeBox.Items.Add(contentProviders[i].Name);
                PantsTypeBox.Items.Add(contentProviders[i].Name);
            }

            //face
            if (GlobalVars.UserCustomization.Face.Contains("http://"))
            {
                Provider faceProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Face);
                FaceIDBox.Text = GlobalVars.UserCustomization.Face.Replace(faceProvider.URL, "");
                FaceTypeBox.SelectedItem = faceProvider.Name;
            }

            //clothing
            if (GlobalVars.UserCustomization.TShirt.Contains("http://"))
            {
                Provider tShirtProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.TShirt);
                TShirtsIDBox.Text = GlobalVars.UserCustomization.TShirt.Replace(tShirtProvider.URL, "");
                TShirtsTypeBox.SelectedItem = tShirtProvider.Name;
            }

            if (GlobalVars.UserCustomization.Shirt.Contains("http://"))
            {
                Provider shirtProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Shirt);
                ShirtsIDBox.Text = GlobalVars.UserCustomization.Shirt.Replace(shirtProvider.URL, "");
                ShirtsTypeBox.SelectedItem = shirtProvider.Name;
            }

            if (GlobalVars.UserCustomization.Pants.Contains("http://"))
            {
                Provider pantsProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Pants);
                PantsIDBox.Text = GlobalVars.UserCustomization.Pants.Replace(pantsProvider.URL, "");
                PantsTypeBox.SelectedItem = pantsProvider.Name;
            }
        }
        else
        {
            FaceTypeBox.Enabled = false;
            TShirtsTypeBox.Enabled = false;
            ShirtsTypeBox.Enabled = false;
            PantsTypeBox.Enabled = false;
            FaceIDBox.Enabled = false;
            TShirtsIDBox.Enabled = false;
            ShirtsIDBox.Enabled = false;
            PantsIDBox.Enabled = false;
        }

        //body
        SelectedPartLabel.Text = SelectedPart;
        HeadButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
        TorsoButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
        RightArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
        LeftArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
        RightLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
        LeftLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);

        //icon
        IconLabel.Text = GlobalVars.UserCustomization.Icon;

        //charid
        CharacterIDBox.Text = GlobalVars.UserCustomization.CharacterID;

        ShowHatsInExtraBox.Checked = GlobalVars.UserCustomization.ShowHatsInExtra;

        //discord
        GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InCustomization, GlobalVars.UserConfiguration.Map);

        GlobalFuncs.ReloadLoadoutValue();
    }

    public void ChangeTabs()
    {
        switch (CharacterTabControl.SelectedTab)
        {
            case TabPage pg1 when pg1 == CharacterTabControl.TabPages["tabPage1"]:
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();
                break;
            case TabPage pg7 when pg7 == CharacterTabControl.TabPages["tabPage7"]:
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradirIcons + "\\" + GlobalVars.UserConfiguration.PlayerName + ".png", GlobalPaths.extradir + "\\NoExtra.png");
                IconImage.Image = icon1;

                break;
            case TabPage pg2 when pg2 == CharacterTabControl.TabPages["tabPage2"]:
                //hats
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 239);
                }
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Hat1,
                        GlobalPaths.hatdir,
                        "NoHat",
                        Hat1Image,
                        Hat1Desc,
                        Hat1List,
                        true
                    );

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Hat2,
                        GlobalPaths.hatdir,
                        "NoHat",
                        Hat2Image,
                        Hat2Desc,
                        Hat2List,
                        true
                    );

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Hat3,
                        GlobalPaths.hatdir,
                        "NoHat",
                        Hat3Image,
                        Hat3Desc,
                        Hat3List,
                        true
                    );

                break;
            case TabPage pg3 when pg3 == CharacterTabControl.TabPages["tabPage3"]:
                //faces
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }

                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                ExtraItemList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Face,
                        GlobalPaths.facedir,
                        "DefaultFace",
                        FaceImage,
                        FaceDesc,
                        FaceList,
                        true,
                        FaceTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(contentProviders, FaceTypeBox.SelectedItem.ToString()) : null
                    );

                break;
            case TabPage pg4 when pg4 == CharacterTabControl.TabPages["tabPage4"]:
                //faces
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.TShirt,
                        GlobalPaths.tshirtdir,
                        "NoTShirt",
                        TShirtImage,
                        TShirtDesc,
                        TShirtList,
                        true,
                        TShirtsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(contentProviders, TShirtsTypeBox.SelectedItem.ToString()) : null
                    );

                break;
            case TabPage pg5 when pg5 == CharacterTabControl.TabPages["tabPage5"]:
                //faces
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Shirt,
                        GlobalPaths.shirtdir,
                        "NoShirt",
                        ShirtImage,
                        ShirtDesc,
                        ShirtList,
                        true,
                        ShirtsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(contentProviders, ShirtsTypeBox.SelectedItem.ToString()) : null
                    );

                break;
            case TabPage pg6 when pg6 == CharacterTabControl.TabPages["tabPage6"]:
                //faces
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Pants,
                        GlobalPaths.pantsdir,
                        "NoPants",
                        PantsImage,
                        PantsDesc,
                        PantsList,
                        true,
                        PantsTypeBox.SelectedItem != null ? OnlineClothing.FindContentProviderByName(contentProviders, PantsTypeBox.SelectedItem.ToString()) : null
                    );

                break;
            case TabPage pg8 when pg8 == CharacterTabControl.TabPages["tabPage8"]:
                //faces
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Head,
                        GlobalPaths.headdir,
                        "DefaultHead",
                        HeadImage,
                        HeadDesc,
                        HeadList,
                        true
                    );

                break;
            case TabPage pg9 when pg9 == CharacterTabControl.TabPages["tabPage9"]:
                //faces
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();

                CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Extra,
                        GlobalPaths.extradir,
                        "NoExtra",
                        ExtraItemImage,
                        ExtraItemDesc,
                        ExtraItemList,
                        true
                    );

                if (GlobalVars.UserCustomization.ShowHatsInExtra)
                {
                    CustomizationFuncs.ChangeItem(
                        GlobalVars.UserCustomization.Extra,
                        GlobalPaths.hatdir,
                        "NoHat",
                        ExtraItemImage,
                        ExtraItemDesc,
                        ExtraItemList,
                        true,
                        GlobalVars.UserCustomization.ShowHatsInExtra
                    );
                }
                break;
            default:
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 359);
                }
                Hat1List.Items.Clear();
                Hat2List.Items.Clear();
                Hat3List.Items.Clear();
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();
                break;
        }
    }
    #endregion

    #region Color Funcs
    Color ConvertStringtoColor(string CString)
    {
        var p = CString.Split(new char[] { ',', ']' });

        int A = Convert.ToInt32(p[0].Substring(p[0].IndexOf('=') + 1));
        int R = Convert.ToInt32(p[1].Substring(p[1].IndexOf('=') + 1));
        int G = Convert.ToInt32(p[2].Substring(p[2].IndexOf('=') + 1));
        int B = Convert.ToInt32(p[3].Substring(p[3].IndexOf('=') + 1));

        return Color.FromArgb(A, R, G, B);
    }

    public void ChangeColorOfPart(int ColorID)
    {
        ChangeColorOfPart(ColorID, PartColorList.Find(x => x.ColorID == ColorID).ButtonColor);
    }

    public void ChangeColorOfPart(int ColorID, Color ButtonColor)
    {
        ChangeColorOfPart(SelectedPart, ColorID, ButtonColor);
    }

    public void ChangeColorOfPart(string part, int ColorID, Color ButtonColor)
    {
        switch (part)
        {
            case "Head":
                GlobalVars.UserCustomization.HeadColorID = ColorID;
                GlobalVars.UserCustomization.HeadColorString = ButtonColor.ToString();
                HeadButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
                break;
            case "Torso":
                GlobalVars.UserCustomization.TorsoColorID = ColorID;
                GlobalVars.UserCustomization.TorsoColorString = ButtonColor.ToString();
                TorsoButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
                break;
            case "Right Arm":
                GlobalVars.UserCustomization.RightArmColorID = ColorID;
                GlobalVars.UserCustomization.RightArmColorString = ButtonColor.ToString();
                RightArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
                break;
            case "Left Arm":
                GlobalVars.UserCustomization.LeftArmColorID = ColorID;
                GlobalVars.UserCustomization.LeftArmColorString = ButtonColor.ToString();
                LeftArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
                break;
            case "Right Leg":
                GlobalVars.UserCustomization.RightLegColorID = ColorID;
                GlobalVars.UserCustomization.RightLegColorString = ButtonColor.ToString();
                RightLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
                break;
            case "Left Leg":
                GlobalVars.UserCustomization.LeftLegColorID = ColorID;
                GlobalVars.UserCustomization.LeftLegColorString = ButtonColor.ToString();
                LeftLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);
                break;
            default:
                break;
        }
    }

    public void ApplyPreset(int head, int torso, int larm, int rarm, int lleg, int rleg)
    {
        ChangeColorOfPart("Head", head, PartColorList.Find(x => x.ColorID == head).ButtonColor);
        ChangeColorOfPart("Torso", torso, PartColorList.Find(x => x.ColorID == torso).ButtonColor);
        ChangeColorOfPart("Left Arm", larm, PartColorList.Find(x => x.ColorID == larm).ButtonColor);
        ChangeColorOfPart("Right Arm", rarm, PartColorList.Find(x => x.ColorID == rarm).ButtonColor);
        ChangeColorOfPart("Left Leg", lleg, PartColorList.Find(x => x.ColorID == lleg).ButtonColor);
        ChangeColorOfPart("Right Leg", rleg, PartColorList.Find(x => x.ColorID == rleg).ButtonColor);
    }

    public void ResetColors()
    {
        GlobalVars.UserCustomization.HeadColorID = 24;
        GlobalVars.UserCustomization.TorsoColorID = 23;
        GlobalVars.UserCustomization.LeftArmColorID = 24;
        GlobalVars.UserCustomization.RightArmColorID = 24;
        GlobalVars.UserCustomization.LeftLegColorID = 119;
        GlobalVars.UserCustomization.RightLegColorID = 119;
        GlobalVars.UserCustomization.CharacterID = "";
        GlobalVars.UserCustomization.HeadColorString = "Color [A=255, R=245, G=205, B=47]";
        GlobalVars.UserCustomization.TorsoColorString = "Color [A=255, R=13, G=105, B=172]";
        GlobalVars.UserCustomization.LeftArmColorString = "Color [A=255, R=245, G=205, B=47]";
        GlobalVars.UserCustomization.RightArmColorString = "Color [A=255, R=245, G=205, B=47]";
        GlobalVars.UserCustomization.LeftLegColorString = "Color [A=255, R=164, G=189, B=71]";
        GlobalVars.UserCustomization.RightLegColorString = "Color [A=255, R=164, G=189, B=71]";
        HeadButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
        TorsoButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
        RightArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
        LeftArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
        RightLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
        LeftLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);
    }

    public void RandomizeColors()
    {
        Random rand = new Random();

        for (int i = 1; i <= 6; i++)
        {
            int RandomColor = rand.Next(PartColorList.Count);

            switch (i)
            {
                case 1:
                    ChangeColorOfPart("Head", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                    break;
                case 2:
                    ChangeColorOfPart("Torso", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                    break;
                case 3:
                    ChangeColorOfPart("Left Arm", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                    break;
                case 4:
                    ChangeColorOfPart("Right Arm", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                    break;
                case 5:
                    ChangeColorOfPart("Left Leg", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                    break;
                case 6:
                    ChangeColorOfPart("Right Leg", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
}
#endregion