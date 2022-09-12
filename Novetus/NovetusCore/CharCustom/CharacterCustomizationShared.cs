#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
    public Provider[] contentProviders;
    public Form Parent;
    public Settings.Style FormStyle;
    public Button HeadButton, TorsoButton, LeftArmButton, RightArmButton, LeftLegButton, RightLegButton, BrowseIconButton;
    public ComboBox FaceTypeBox, TShirtsTypeBox, ShirtsTypeBox, PantsTypeBox;
    public TextBox FaceIDBox, TShirtsIDBox, ShirtsIDBox, PantsIDBox, CharacterIDBox, 
        Hat1Desc, Hat2Desc, Hat3Desc, HeadDesc, TShirtDesc, ShirtDesc, PantsDesc, FaceDesc, ExtraItemDesc,
        IconURLBox;
    public CheckBox ShowHatsInExtraBox;
    public Label SelectedPartLabel, IconLabel, AestheticDivider;
    public TabControl CharacterTabControl, HatTabControl;
    public Panel OrganizationPanel, AestheticPanel1, AestheticPanel2;
    public ListBox Hat1List, Hat2List, Hat3List, HeadList, TShirtList, ShirtList, PantsList, FaceList, ExtraItemList;
    public PictureBox Hat1Image, Hat2Image, Hat3Image, HeadImage, TShirtImage, ShirtImage, PantsImage, FaceImage, ExtraItemImage, IconImage, CharBackground;
    public ListView ColorView;
    public bool closeOnLaunch = false;
    #endregion

    #region Constructor
    public CharacterCustomizationShared()
    {

    }
    #endregion

    #region Form Event Functions
    public void InitForm()
    {
        if (FileManagement.HasColorsChanged())
        {
            GlobalVars.ColorsLoaded = FileManagement.InitColors();
            closeOnLaunch = !GlobalVars.ColorsLoaded;
        }

        if (closeOnLaunch)
        {
            MessageBox.Show("The part colors cannot be loaded. The character customization will now close.", "Novetus - Cannot load part colors.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Parent.Close();
            return;
        }

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
            if (GlobalVars.UserCustomization.Face.Contains("http://") || GlobalVars.UserCustomization.Face.Contains("https://"))
            {
                Provider faceProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Face);
                FaceIDBox.Text = GlobalVars.UserCustomization.Face.Replace(faceProvider.URL, "");
                FaceTypeBox.SelectedItem = faceProvider.Name;
            }

            //clothing
            if (GlobalVars.UserCustomization.TShirt.Contains("http://") || GlobalVars.UserCustomization.TShirt.Contains("https://"))
            {
                Provider tShirtProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.TShirt);
                TShirtsIDBox.Text = GlobalVars.UserCustomization.TShirt.Replace(tShirtProvider.URL, "");
                TShirtsTypeBox.SelectedItem = tShirtProvider.Name;
            }

            if (GlobalVars.UserCustomization.Shirt.Contains("http://") || GlobalVars.UserCustomization.Shirt.Contains("https://"))
            {
                Provider shirtProvider = OnlineClothing.FindContentProviderByURL(contentProviders, GlobalVars.UserCustomization.Shirt);
                ShirtsIDBox.Text = GlobalVars.UserCustomization.Shirt.Replace(shirtProvider.URL, "");
                ShirtsTypeBox.SelectedItem = shirtProvider.Name;
            }

            if (GlobalVars.UserCustomization.Pants.Contains("http://") || GlobalVars.UserCustomization.Pants.Contains("https://"))
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

        int imgsize = (FormStyle == Settings.Style.Extended) ? 28 : 18;
        PartColorLoader.AddPartColorsToListView(GlobalVars.PartColorList, ColorView, imgsize);

        //body
        SelectedPartLabel.Text = SelectedPart;
        ReloadColors();

        //icon
        if (GlobalVars.UserCustomization.Icon.Contains("http://") || GlobalVars.UserCustomization.Icon.Contains("https://"))
        {
            IconLabel.Text = "NBC";
        }
        else
        {
            IconLabel.Text = GlobalVars.UserCustomization.Icon;
        }

        //charid
        CharacterIDBox.Text = GlobalVars.UserCustomization.CharacterID;

        ShowHatsInExtraBox.Checked = GlobalVars.UserCustomization.ShowHatsInExtra;

        if (GlobalVars.UserConfiguration.LauncherStyle == Settings.Style.Stylish)
        {
            Color robBlue = Color.FromArgb(110, 152, 200);
            if (FormStyle == Settings.Style.Extended)
            {
                AestheticPanel1.BorderStyle = BorderStyle.None;
                AestheticPanel1.BackColor = robBlue;

                foreach (Control C in AestheticPanel1.Controls)
                {
                    if (C is Button)
                    {
                        Button button = (Button)C;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = Color.White;
                        button.ForeColor = Color.White;
                        button.Font = new Font("Comic Sans MS", 7.8f, FontStyle.Bold);
                        button.BackColor = robBlue;
                        button.Location = new Point(button.Location.X + 1, button.Location.Y);
                    }
                }

                AestheticPanel2.BorderStyle = BorderStyle.FixedSingle;
                AestheticDivider.BorderStyle = BorderStyle.None;
                AestheticDivider.Size = new Size(AestheticDivider.Size.Width + 3, AestheticDivider.Size.Height);
            }

            string backgroundImage = GlobalPaths.DataDir + @"\\CharacterBackdrop.png";

            if (File.Exists(backgroundImage))
            {
                Image im = Util.LoadImage(backgroundImage);
                CharBackground.Image = im;
            }
        }

        //discord
        ClientManagement.UpdateRichPresence(GlobalVars.LauncherState.InCustomization);

        FileManagement.ReloadLoadoutValue();
    }

    public void CloseEvent()
    {
        ClientManagement.UpdateRichPresence(ClientManagement.GetStateForType(GlobalVars.GameOpened));
        FileManagement.ReloadLoadoutValue();
        SaveOutfit(false);
    }

    public void ChangeTabs()
    {
        ColorView.SelectedIndices.Clear();
        switch (CharacterTabControl.SelectedTab)
        {
            case TabPage pg1 when pg1 == CharacterTabControl.TabPages["tabPage1"]:
                if (FormStyle == Settings.Style.Extended)
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
                if (FormStyle == Settings.Style.Extended)
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

                if (GlobalVars.UserCustomization.Icon.Contains("http://") || GlobalVars.UserCustomization.Icon.Contains("https://"))
                {
                    IconURLBox.Text = GlobalVars.UserCustomization.Icon;
                    LoadRemoteIcon();
                }
                else
                {
                    LoadLocalIcon();
                }

                break;
            case TabPage pg2 when pg2 == CharacterTabControl.TabPages["tabPage2"]:
                //hats
                if (FormStyle == Settings.Style.Extended)
                {
                    OrganizationPanel.Location = new Point(110, 239);
                }
                HeadList.Items.Clear();
                TShirtList.Items.Clear();
                ShirtList.Items.Clear();
                PantsList.Items.Clear();
                FaceList.Items.Clear();
                ExtraItemList.Items.Clear();

                ChangeItem(
                        GlobalVars.UserCustomization.Hat1,
                        GlobalPaths.hatdir,
                        "NoHat",
                        Hat1Image,
                        Hat1Desc,
                        Hat1List,
                        true
                    );

                ChangeItem(
                        GlobalVars.UserCustomization.Hat2,
                        GlobalPaths.hatdir,
                        "NoHat",
                        Hat2Image,
                        Hat2Desc,
                        Hat2List,
                        true
                    );

                ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

                ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

                ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

                ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

                ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

                ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

                ChangeItem(
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
                    ChangeItem(
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
                if (FormStyle == Settings.Style.Extended)
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

    #region Part/Color Funcs
    public void ReloadColors()
    {
        HeadButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
        TorsoButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
        RightArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
        LeftArmButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
        RightLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
        LeftLegButton.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);
    }

    public void ColorButton()
    {
        int selectedIndex = 0;

        if (ColorView.SelectedIndices.Count > 0)
        {
            selectedIndex = ColorView.SelectedIndices[0];
        }
        else
        { 
            return; 
        }

        ChangeColorOfPart(Convert.ToInt32(ColorView.Items[selectedIndex].Tag));
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
        ChangeColorOfPart(ColorID, GlobalVars.PartColorListConv.Find(x => x.ColorID == ColorID).ColorObject);
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

    public void SelectPart(string part)
    {
        ColorView.SelectedIndices.Clear();
        SelectedPart = part;
        SelectedPartLabel.Text = SelectedPart;
    }

    public void ApplyPreset(int head, int torso, int larm, int rarm, int lleg, int rleg)
    {
        try
        {
            ColorView.SelectedIndices.Clear();
            ChangeColorOfPart("Head", head, GlobalVars.PartColorListConv.Find(x => x.ColorID == head).ColorObject);
            ChangeColorOfPart("Torso", torso, GlobalVars.PartColorListConv.Find(x => x.ColorID == torso).ColorObject);
            ChangeColorOfPart("Left Arm", larm, GlobalVars.PartColorListConv.Find(x => x.ColorID == larm).ColorObject);
            ChangeColorOfPart("Right Arm", rarm, GlobalVars.PartColorListConv.Find(x => x.ColorID == rarm).ColorObject);
            ChangeColorOfPart("Left Leg", lleg, GlobalVars.PartColorListConv.Find(x => x.ColorID == lleg).ColorObject);
            ChangeColorOfPart("Right Leg", rleg, GlobalVars.PartColorListConv.Find(x => x.ColorID == rleg).ColorObject);
        }
        catch(Exception ex)
        {
            MessageBox.Show("Failed to load required colors for the preset.", "Novetus - Preset Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ResetColors();
            Util.LogExceptions(ex);
        }
    }

    public void ResetColors()
    {
        ColorView.SelectedIndices.Clear();
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
        ColorView.SelectedIndices.Clear();
        Random rand = new Random();

        for (int i = 1; i <= 6; i++)
        {
            int RandomColor = rand.Next(GlobalVars.PartColorListConv.Count);

            switch (i)
            {
                case 1:
                    ChangeColorOfPart("Head", GlobalVars.PartColorListConv[RandomColor].ColorID, GlobalVars.PartColorListConv[RandomColor].ColorObject);
                    break;
                case 2:
                    ChangeColorOfPart("Torso", GlobalVars.PartColorListConv[RandomColor].ColorID, GlobalVars.PartColorListConv[RandomColor].ColorObject);
                    break;
                case 3:
                    ChangeColorOfPart("Left Arm", GlobalVars.PartColorListConv[RandomColor].ColorID, GlobalVars.PartColorListConv[RandomColor].ColorObject);
                    break;
                case 4:
                    ChangeColorOfPart("Right Arm", GlobalVars.PartColorListConv[RandomColor].ColorID, GlobalVars.PartColorListConv[RandomColor].ColorObject);
                    break;
                case 5:
                    ChangeColorOfPart("Left Leg", GlobalVars.PartColorListConv[RandomColor].ColorID, GlobalVars.PartColorListConv[RandomColor].ColorObject);
                    break;
                case 6:
                    ChangeColorOfPart("Right Leg", GlobalVars.PartColorListConv[RandomColor].ColorID, GlobalVars.PartColorListConv[RandomColor].ColorObject);
                    break;
                default:
                    break;
            }
        }
    }

    public void SaveOutfit(bool box = true)
    {
        FileManagement.Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
        if (box)
        {
            MessageBox.Show("Outfit Saved!", "Novetus - Outfit Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public void LoadOutfit()
    {
        using (var ofd = new OpenFileDialog())
        {
            ofd.Filter = "Novetus config_customization files (*.ini)|*.ini";
            ofd.FilterIndex = 1;
            ofd.FileName = "config_customization.ini";
            ofd.Title = "Load config_customization.ini";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileManagement.Customization(ofd.FileName, false);
                ReloadColors();
                MessageBox.Show("Outfit Loaded!", "Novetus - Outfit Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public void ChangeItem(string item, string itemdir, string defaultitem, PictureBox outputImage, TextBox outputString, ListBox box, bool initial, bool hatsinextra = false)
    {
        ChangeItem(item, itemdir, defaultitem, outputImage, outputString, box, initial, null, hatsinextra);
    }

    public void ChangeItem(string item, string itemdir, string defaultitem, PictureBox outputImage, TextBox outputString, ListBox box, bool initial, Provider provider, bool hatsinextra = false)
    {
        if (Directory.Exists(itemdir))
        {
            if (initial)
            {
                if (!hatsinextra)
                {
                    box.Items.Clear();
                }
                DirectoryInfo dinfo = new DirectoryInfo(itemdir);
                FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                foreach (FileInfo file in Files)
                {
                    if (file.Name.Equals(string.Empty))
                    {
                        continue;
                    }

                    if (hatsinextra)
                    {
                        if (file.Name.Equals("NoHat.rbxm"))
                        {
                            continue;
                        }
                    }

                    box.Items.Add(file.Name);
                }
                //selecting items triggers the event.
                try
                {
                    box.SelectedItem = item;
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    box.SelectedItem = defaultitem + ".rbxm";
                }

                box.Enabled = true;
            }
        }

        if (File.Exists(itemdir + @"\\" + item.Replace(".rbxm", "") + "_desc.txt"))
        {
            outputString.Text = File.ReadAllText(itemdir + @"\\" + item.Replace(".rbxm", "") + "_desc.txt");
        }
        else
        {
            outputString.Text = item;
        }

        if (provider != null && IsItemURL(item))
        {
            outputImage.Image = GetItemURLImageFromProvider(provider);
        }
        else
        {
            outputImage.Image = Util.LoadImage(itemdir + @"\\" + item.Replace(".rbxm", "") + ".png", itemdir + @"\\" + defaultitem + ".png");
        }
    }

    public bool IsItemURL(string item)
    {
        if (item.Contains("http://") || item.Contains("https://"))
            return true;

        return false;
    }

    public Image GetItemURLImageFromProvider(Provider provider)
    {
        if (provider != null)
            return Util.LoadImage(GlobalPaths.CustomPlayerDir + @"\\" + provider.Icon, GlobalPaths.extradir + @"\\NoExtra.png");

        return Util.LoadImage(GlobalPaths.extradir + @"\\NoExtra.png");
    }

    //we launch the 3dview seperately from normal clients.
    public void Launch3DView()
    {
        FileManagement.ReloadLoadoutValue();
        SaveOutfit(false);
        //HACK!
        try
        {
            ClientManagement.ChangeGameSettings("2011E");
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
        }

        string luafile = "rbxasset://scripts\\\\CSView.lua";
        string mapfile = GlobalPaths.BasePathLauncher + "\\preview\\content\\fonts\\3DView.rbxl";
        string rbxexe = GlobalPaths.BasePathLauncher + (GlobalVars.AdminMode ? "\\preview\\3DView_studio.exe" : "\\preview\\3DView.exe");
        string quote = "\"";
        string script = "_G.CS3DView(0,'" + GlobalVars.UserConfiguration.PlayerName + "'," + GlobalVars.Loadout + ");";

        if (GlobalVars.AdminMode)
        {
            DialogResult adminres = MessageBox.Show("Would you like to run 3D Preview Studio with or without scripts?\n\nPress Yes to load with scripts, press No to load without.", "Novetus - 3D Preview Studio", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (adminres == DialogResult.No)
            {
                script = "_G.CS3DViewEdit();";
            }
        }

        string args = quote + mapfile + "\" -script \" dofile('" + luafile + "');" + script + quote;

        try
        {
            ClientManagement.OpenClient(ScriptType.None, rbxexe, args, "", "", null, true);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to launch the 3D Preview. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Util.LogExceptions(ex);
        }
    }

    public void LaunchLoadLocalIcon()
    {
        IconLoader icon = new IconLoader();
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

            MessageBox.Show(icon.getInstallOutcome(), "Novetus - Icon Installed", MessageBoxButtons.OK, boxicon);
        }

        LoadLocalIcon();
    }

    public void LoadLocalIcon()
    {
        Image icon1 = Util.LoadImage(GlobalPaths.extradirIcons + "\\" + GlobalVars.UserConfiguration.PlayerName + ".png", GlobalPaths.extradir + "\\NoExtra.png");
        IconImage.Image = icon1;
    }

    public void LoadRemoteIcon()
    {
        if (string.IsNullOrWhiteSpace(IconURLBox.Text) ||
            IconURLBox.Text.Contains("BC") ||
            IconURLBox.Text.Contains("TBC") ||
            IconURLBox.Text.Contains("OBC") ||
            IconURLBox.Text.Contains("NBC"))
        {
            IconURLBox.Text = "";
            GlobalVars.UserCustomization.Icon = "NBC";
            IconLabel.Text = GlobalVars.UserCustomization.Icon;
            BrowseIconButton.Enabled = true;
            LoadLocalIcon();
            return;
        }
        else if (IconURLBox.Text.Contains("http://") || IconURLBox.Text.Contains("https://"))
        {
            GlobalVars.UserCustomization.Icon = IconURLBox.Text;
            IconLabel.Text = "NBC";
            BrowseIconButton.Enabled = false;
        }

        try
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(IconURLBox.Text);
            MemoryStream ms = new MemoryStream(bytes);
            Image img = Image.FromStream(ms);
            IconImage.Image = img;
        }
        catch (Exception ex)
        {
            Image icon1 = Util.LoadImage(GlobalPaths.extradir + "\\NoExtra.png", GlobalPaths.extradir + "\\NoExtra.png");
            IconImage.Image = icon1;
            Util.LogExceptions(ex);
        }
    }
    #endregion
}
#endregion