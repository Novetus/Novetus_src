using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

class CharacterCustomizationShared
{
    #region Variables
    public string SelectedPart = "Head";
    public string Custom_T_Shirt_URL = "";
    public string Custom_Shirt_URL = "";
    public string Custom_Pants_URL = "";
    public string Custom_Face_URL = "";
    public List<VarStorage.PartColors> PartColorList;
    public Settings.Provider[] contentProviders;
    public Form Parent;
    public Settings.UIOptions.Style FormStyle;
    public Button WhiteButton, LightStoneGreyButton, MediumStoneGreyButton, DarkStoneGreyButton, BlackButton,
        BrightRedButton, BrightYellowButton, CoolYellowButton, BrightBlueButton, BrightBluishGreenButton, MediumBlueButton,
        PastelBlueButton, LightBlueButton, SandBlueButton, BrightOrangeButton, BrightYellowishOrangeButton, EarthGreenButton, DarkGreenButton,
        BrightGreenButton, BrightYellowishGreenButton, MediumGreenButton, SandGreenButton, DarkOrangeButton, ReddishBrownButton, BrightVioletButton,
        LightReddishVioletButton, MediumRedButton, BrickYellowButton, SandRedButton, BrownButton, NougatButton, LightOrangeButton, MediumReddishViolet,
        DarkNougatButton, HeadButton, TorsoButton, LeftArmButton, RightArmButton, LeftLegButton, RightLegButton;
    public ComboBox FaceTypeBox, TShirtsTypeBox, ShirtsTypeBox, PantsTypeBox;
    #endregion

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

    public void InitForm()
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\ContentProviders.xml"))
        {
            contentProviders = Settings.OnlineClothing.GetContentProviders();

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
                Settings.Provider faceProvider = Settings.OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Face);
                FaceIDBox.Text = GlobalVars.UserCustomization.Face.Replace(faceProvider.URL, "");
                FaceTypeBox.SelectedItem = faceProvider.Name;
            }

            //clothing
            if (GlobalVars.UserCustomization.TShirt.Contains("http://"))
            {
                Settings.Provider tShirtProvider = Settings.OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.TShirt);
                TShirtsIDBox.Text = GlobalVars.UserCustomization.TShirt.Replace(tShirtProvider.URL, "");
                TShirtsTypeBox.SelectedItem = tShirtProvider.Name;
            }

            if (GlobalVars.UserCustomization.Shirt.Contains("http://"))
            {
                Settings.Provider shirtProvider = Settings.OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Shirt);
                ShirtsIDBox.Text = GlobalVars.UserCustomization.Shirt.Replace(shirtProvider.URL, "");
                ShirtsTypeBox.SelectedItem = shirtProvider.Name;
            }

            if (GlobalVars.UserCustomization.Pants.Contains("http://"))
            {
                Settings.Provider pantsProvider = Settings.OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Pants);
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
        label2.Text = SelectedPart;
        HeadButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
        TorsoButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
        RightArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
        LeftArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
        RightLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
        LeftLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);

        //icon
        label5.Text = GlobalVars.UserCustomization.Icon;

        //charid
        textBox1.Text = GlobalVars.UserCustomization.CharacterID;

        checkBox1.Checked = GlobalVars.UserCustomization.ShowHatsInExtra;

        //discord
        GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InCustomization, GlobalVars.UserConfiguration.Map);

        GlobalFuncs.ReloadLoadoutValue();
    }

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
}