/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 2/5/2017
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of CharacterCustomization.
	/// </summary>
	public partial class CharacterCustomization : Form
	{
		private string SelectedPart = "Head";
        private string Custom_T_Shirt_URL = "http://www.roblox.com/asset/?id=";
        private string Custom_Shirt_URL = "http://www.roblox.com/asset/?id=";
        private string Custom_Pants_URL = "http://www.roblox.com/asset/?id=";
        List<VarStorage.PartColors> PartColorList;

        public CharacterCustomization()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            InitColors();

            Size = new Size(671, 337);
            panel2.Size = new Size(568, 302);

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        void InitColors()
        {
            PartColorList = new List<VarStorage.PartColors>()
            {
                //White
                new VarStorage.PartColors{ ColorID = 1, ButtonColor = button7.BackColor },
                //Light stone grey
                new VarStorage.PartColors{ ColorID = 208, ButtonColor = button8.BackColor },
                //Medium stone grey
                new VarStorage.PartColors{ ColorID = 194, ButtonColor = button9.BackColor },
                //Dark stone grey
                new VarStorage.PartColors{ ColorID = 199, ButtonColor = button10.BackColor },
                //Black
                new VarStorage.PartColors{ ColorID = 26, ButtonColor = button14.BackColor },
                //Bright red
                new VarStorage.PartColors{ ColorID = 21, ButtonColor = button13.BackColor },
                //Bright yellow
                new VarStorage.PartColors{ ColorID = 24, ButtonColor = button12.BackColor },
                //Cool yellow
                new VarStorage.PartColors{ ColorID = 226, ButtonColor = button11.BackColor },
                //Bright blue
                new VarStorage.PartColors{ ColorID = 23, ButtonColor = button18.BackColor },
                //Bright bluish green
                new VarStorage.PartColors{ ColorID = 107, ButtonColor = button17.BackColor },
                //Medium blue
                new VarStorage.PartColors{ ColorID = 102, ButtonColor = button16.BackColor },
                //Pastel Blue
                new VarStorage.PartColors{ ColorID = 11, ButtonColor = button15.BackColor },
                //Light blue
                new VarStorage.PartColors{ ColorID = 45, ButtonColor = button22.BackColor },
                //Sand blue
                new VarStorage.PartColors{ ColorID = 135, ButtonColor = button21.BackColor },
                //Bright orange
                new VarStorage.PartColors{ ColorID = 106, ButtonColor = button20.BackColor },
                //Br. yellowish orange
                new VarStorage.PartColors{ ColorID = 105, ButtonColor = button19.BackColor },
                //Earth green
                new VarStorage.PartColors{ ColorID = 141, ButtonColor = button26.BackColor },
                //Dark green
                new VarStorage.PartColors{ ColorID = 28, ButtonColor = button25.BackColor },
                //Bright green
                new VarStorage.PartColors{ ColorID = 37, ButtonColor = button24.BackColor },
                //Br. yellowish green
                new VarStorage.PartColors{ ColorID = 119, ButtonColor = button23.BackColor },
                //Medium green
                new VarStorage.PartColors{ ColorID = 29, ButtonColor = button30.BackColor },
                //Sand green
                new VarStorage.PartColors{ ColorID = 151, ButtonColor = button29.BackColor },
                //Dark orange
                new VarStorage.PartColors{ ColorID = 38, ButtonColor = button28.BackColor },
                //Reddish brown
                new VarStorage.PartColors{ ColorID = 192, ButtonColor = button27.BackColor },
                //Bright violet
                new VarStorage.PartColors{ ColorID = 104, ButtonColor = button34.BackColor },
                //Light reddish violet
                new VarStorage.PartColors{ ColorID = 9, ButtonColor = button33.BackColor },
                //Medium red
                new VarStorage.PartColors{ ColorID = 101, ButtonColor = button32.BackColor },
                //Brick yellow
                new VarStorage.PartColors{ ColorID = 5, ButtonColor = button31.BackColor },
                //Sand red
                new VarStorage.PartColors{ ColorID = 153, ButtonColor = button38.BackColor },
                //Brown
                new VarStorage.PartColors{ ColorID = 217, ButtonColor = button37.BackColor },
                //Nougat
                new VarStorage.PartColors{ ColorID = 18, ButtonColor = button36.BackColor },
                //Light orange
                new VarStorage.PartColors{ ColorID = 125, ButtonColor = button35.BackColor },
                // RARE 2006 COLORS!!
                //Med. reddish violet
                new VarStorage.PartColors{ ColorID = 22, ButtonColor = button69.BackColor },
                //Dark nougat
                new VarStorage.PartColors{ ColorID = 128, ButtonColor = button70.BackColor }
            };
        }

        void CharacterCustomizationLoad(object sender, EventArgs e)
		{
			//body
			label2.Text = SelectedPart;
			button1.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
			button2.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
			button3.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
			button4.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
			button5.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
			button6.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);
			
			//icon
			label5.Text = GlobalVars.UserCustomization.Icon;
			
			//charid
			textBox1.Text = GlobalVars.UserCustomization.CharacterID;
			
			checkBox1.Checked = GlobalVars.UserCustomization.ShowHatsInExtra;

            //clothing
            if (GlobalVars.UserCustomization.TShirt.Contains("http://"))
            {
                switch (GlobalVars.UserCustomization.TShirt)
                {
                    case string finobe when finobe.Contains("http://finobe.com/asset/?id="):
                        textBox12.Text = GlobalVars.UserCustomization.TShirt.Replace("http://finobe.com/asset/?id=", "");
                        comboBox3.SelectedItem = "Finobe";
                        break;
                    case string roblox when roblox.Contains("http://www.roblox.com/asset/?id="):
                    default:
                        textBox12.Text = GlobalVars.UserCustomization.TShirt.Replace("http://www.roblox.com/asset/?id=", "");
                        comboBox3.SelectedItem = "Roblox";
                        break;
                }
            }

            if (GlobalVars.UserCustomization.Shirt.Contains("http://"))
            {
                switch (GlobalVars.UserCustomization.Shirt)
                {
                    case string finobe when finobe.Contains("http://finobe.com/asset/?id="):
                        textBox11.Text = GlobalVars.UserCustomization.Shirt.Replace("http://finobe.com/asset/?id=", "");
                        comboBox2.SelectedItem = "Finobe";
                        break;
                    case string roblox when roblox.Contains("http://www.roblox.com/asset/?id="):
                    default:
                        textBox11.Text = GlobalVars.UserCustomization.Shirt.Replace("http://www.roblox.com/asset/?id=", "");
                        comboBox2.SelectedItem = "Roblox";
                        break;
                }
            }

            if (GlobalVars.UserCustomization.Pants.Contains("http://"))
            {
                switch (GlobalVars.UserCustomization.Pants)
                {
                    case string finobe when finobe.Contains("http://finobe.com/asset/?id="):
                        textBox13.Text = GlobalVars.UserCustomization.Pants.Replace("http://finobe.com/asset/?id=", "");
                        comboBox1.SelectedItem = "Finobe";
                        break;
                    case string roblox when roblox.Contains("http://www.roblox.com/asset/?id="):
                    default:
                        textBox13.Text = GlobalVars.UserCustomization.Pants.Replace("http://www.roblox.com/asset/?id=", "");
                        comboBox1.SelectedItem = "Roblox";
                        break;
                }
            }

            //discord
            GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InCustomization, GlobalVars.UserConfiguration.Map);
        	
        	GlobalFuncs.ReloadLoadoutValue();
		}
		
		void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
            switch (tabControl1.SelectedTab)
            {
                case TabPage pg1 when pg1 == tabControl1.TabPages["tabPage1"]:
                    panel3.Location = new Point(110, 359);
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();
                    break;
                case TabPage pg7 when pg7 == tabControl1.TabPages["tabPage7"]:
                    panel3.Location = new Point(110, 359);
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();

                    try
                    {
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradirIcons + "\\" + GlobalVars.UserConfiguration.PlayerName + ".png");
                        pictureBox10.Image = icon1;
                    }
                    catch (Exception)
                    {
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\NoExtra.png");
                        pictureBox10.Image = icon1;
                    }
                    break;
                case TabPage pg2 when pg2 == tabControl1.TabPages["tabPage2"]:
                    //hats
                    panel3.Location = new Point(110, 239);
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();

                    if (Directory.Exists(GlobalPaths.hatdir))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.hatdir);
                        FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.Equals(String.Empty))
                            {
                                continue;
                            }

                            listBox1.Items.Add(file.Name);
                            listBox2.Items.Add(file.Name);
                            listBox3.Items.Add(file.Name);
                        }
                        listBox1.SelectedItem = GlobalVars.UserCustomization.Hat1;
                        listBox2.SelectedItem = GlobalVars.UserCustomization.Hat2;
                        listBox3.SelectedItem = GlobalVars.UserCustomization.Hat3;
                        listBox1.Enabled = true;
                        listBox2.Enabled = true;
                        listBox3.Enabled = true;
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + ".png");
                        pictureBox1.Image = icon1;
                        Image icon2 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + ".png");
                        pictureBox2.Image = icon2;
                        Image icon3 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + ".png");
                        pictureBox3.Image = icon3;
                        if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox2.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox2.Text = GlobalVars.UserCustomization.Hat1;
                        }

                        if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox3.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox3.Text = GlobalVars.UserCustomization.Hat2;
                        }

                        if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox4.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox4.Text = GlobalVars.UserCustomization.Hat3;
                        }
                    }
                    break;
                case TabPage pg3 when pg3 == tabControl1.TabPages["tabPage3"]:
                    //faces
                    panel3.Location = new Point(110, 359);
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();

                    if (Directory.Exists(GlobalPaths.facedir))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.facedir);
                        FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.Equals(String.Empty))
                            {
                                continue;
                            }

                            listBox4.Items.Add(file.Name);
                        }
                        listBox4.SelectedItem = GlobalVars.UserCustomization.Face;
                        listBox4.Enabled = true;
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + ".png");
                        pictureBox4.Image = icon1;

                        if (File.Exists(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox6.Text = File.ReadAllText(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox6.Text = GlobalVars.UserCustomization.Face;
                        }
                    }
                    break;
                case TabPage pg4 when pg4 == tabControl1.TabPages["tabPage4"]:
                    //faces
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();

                    try
                    {
                        if (Directory.Exists(GlobalPaths.tshirtdir))
                        {
                            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.tshirtdir);
                            FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                            foreach (FileInfo file in Files)
                            {
                                if (file.Name.Equals(String.Empty))
                                {
                                    continue;
                                }

                                listBox5.Items.Add(file.Name);
                            }
                            listBox5.SelectedItem = GlobalVars.UserCustomization.TShirt;
                            listBox5.Enabled = true;
                            Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + ".png");
                            pictureBox5.Image = icon1;

                            if (File.Exists(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt"))
                            {
                                textBox7.Text = File.ReadAllText(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt");
                            }
                            else
                            {
                                textBox7.Text = GlobalVars.UserCustomization.TShirt;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.tshirtdir + @"\\NoTShirt.png");
                        pictureBox5.Image = icon1;
                    }
                    break;
                case TabPage pg5 when pg5 == tabControl1.TabPages["tabPage5"]:
                    //faces
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();

                    try
                    {
                        if (Directory.Exists(GlobalPaths.shirtdir))
                        {
                            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.shirtdir);
                            FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                            foreach (FileInfo file in Files)
                            {
                                if (file.Name.Equals(String.Empty))
                                {
                                    continue;
                                }

                                listBox6.Items.Add(file.Name);
                            }
                            listBox6.SelectedItem = GlobalVars.UserCustomization.Shirt;
                            listBox6.Enabled = true;
                            Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + ".png");
                            pictureBox6.Image = icon1;

                            if (File.Exists(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt"))
                            {
                                textBox8.Text = File.ReadAllText(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt");
                            }
                            else
                            {
                                textBox8.Text = GlobalVars.UserCustomization.Shirt;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.shirtdir + @"\\NoShirt.png");
                        pictureBox6.Image = icon1;
                    }
                    break;
                case TabPage pg6 when pg6 == tabControl1.TabPages["tabPage6"]:
                    //faces
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();

                    try
                    {
                        if (Directory.Exists(GlobalPaths.pantsdir))
                        {
                            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.pantsdir);
                            FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                            foreach (FileInfo file in Files)
                            {
                                if (file.Name.Equals(String.Empty))
                                {
                                    continue;
                                }

                                listBox7.Items.Add(file.Name);
                            }
                            listBox7.SelectedItem = GlobalVars.UserCustomization.Pants;
                            listBox7.Enabled = true;
                            Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + ".png");
                            pictureBox7.Image = icon1;

                            if (File.Exists(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt"))
                            {
                                textBox9.Text = File.ReadAllText(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt");
                            }
                            else
                            {
                                textBox9.Text = GlobalVars.UserCustomization.Pants;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.pantsdir + @"\\NoPants.png");
                        pictureBox7.Image = icon1;
                    }
                    break;
                case TabPage pg8 when pg8 == tabControl1.TabPages["tabPage8"]:
                    //faces
                    panel3.Location = new Point(110, 359);
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox9.Items.Clear();

                    if (Directory.Exists(GlobalPaths.headdir))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.headdir);
                        FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.Equals(String.Empty))
                            {
                                continue;
                            }

                            listBox8.Items.Add(file.Name);
                        }
                        listBox8.SelectedItem = GlobalVars.UserCustomization.Head;
                        listBox8.Enabled = true;
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + ".png");
                        pictureBox8.Image = icon1;

                        if (File.Exists(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox5.Text = File.ReadAllText(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox5.Text = GlobalVars.UserCustomization.Head;
                        }
                    }
                    break;
                case TabPage pg9 when pg9 == tabControl1.TabPages["tabPage9"]:
                    //faces
                    panel3.Location = new Point(110, 359);
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();

                    if (Directory.Exists(GlobalPaths.extradir))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.extradir);
                        FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.Equals(String.Empty))
                            {
                                continue;
                            }

                            listBox9.Items.Add(file.Name);
                        }
                    }

                    if (GlobalVars.UserCustomization.ShowHatsInExtra)
                    {
                        if (Directory.Exists(GlobalPaths.hatdir))
                        {
                            DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.hatdir);
                            FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                            foreach (FileInfo file in Files)
                            {
                                if (file.Name.Equals(String.Empty))
                                {
                                    continue;
                                }

                                if (file.Name.Equals("NoHat.rbxm"))
                                {
                                    continue;
                                }

                                listBox9.Items.Add(file.Name);
                            }
                        }
                    }

                    listBox9.SelectedItem = GlobalVars.UserCustomization.Extra;
                    listBox9.Enabled = true;
                    try
                    {
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
                        pictureBox9.Image = icon1;
                        if (File.Exists(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.UserCustomization.Extra;
                        }
                    }
                    catch (Exception)
                    {
                        if (Directory.Exists(GlobalPaths.hatdir))
                        {
                            Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
                            pictureBox9.Image = icon1;
                            if (File.Exists(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                            {
                                textBox10.Text = File.ReadAllText(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                            }
                            else
                            {
                                textBox10.Text = GlobalVars.UserCustomization.Extra;
                            }
                        }
                    }
                    break;
                default:
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    listBox5.Items.Clear();
                    listBox6.Items.Clear();
                    listBox7.Items.Clear();
                    listBox8.Items.Clear();
                    listBox9.Items.Clear();
                    break;
            }
		}
		
		void CharacterCustomizationClose(object sender, CancelEventArgs e)
		{
            GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");
            GlobalFuncs.ReloadLoadoutValue();
		}
		
		// hats
		
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalPaths.hatdir))
        	{
        		GlobalVars.UserCustomization.Hat1 = listBox1.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox2.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox2.Text = GlobalVars.UserCustomization.Hat1;
                }
            }
        }
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalPaths.hatdir))
        	{
        		GlobalVars.UserCustomization.Hat2 = listBox2.SelectedItem.ToString();
        		Image icon2 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox3.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox3.Text = GlobalVars.UserCustomization.Hat2;
                }
            }
		}
		
		void ListBox3SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalPaths.hatdir))
        	{
        		GlobalVars.UserCustomization.Hat3 = listBox3.SelectedItem.ToString();
        		Image icon3 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = GlobalVars.UserCustomization.Hat3;
                }
            }
		}
		
		void Button41Click(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalPaths.hatdir))
        	{
        		Random random = new Random();
				int randomHat1  = random.Next(listBox1.Items.Count);
				listBox1.SelectedItem = listBox1.Items[randomHat1];
        		GlobalVars.UserCustomization.Hat1 = listBox1.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        		int randomHat2  = random.Next(listBox2.Items.Count);
				listBox2.SelectedItem = listBox1.Items[randomHat2];
        		GlobalVars.UserCustomization.Hat2 = listBox2.SelectedItem.ToString();
        		Image icon2 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
        		int randomHat3  = random.Next(listBox3.Items.Count);
				listBox3.SelectedItem = listBox1.Items[randomHat3];
        		GlobalVars.UserCustomization.Hat3 = listBox3.SelectedItem.ToString();
        		Image icon3 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox2.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox2.Text = GlobalVars.UserCustomization.Hat1;
                }

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox3.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox3.Text = GlobalVars.UserCustomization.Hat2;
                }

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = GlobalVars.UserCustomization.Hat3;
                }
            }
		}
		
		void Button42Click(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalPaths.hatdir))
        	{
				listBox1.SelectedItem = "NoHat.rbxm";
        		GlobalVars.UserCustomization.Hat1 = listBox1.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
				listBox2.SelectedItem = "NoHat.rbxm";
        		GlobalVars.UserCustomization.Hat2 = listBox2.SelectedItem.ToString();
        		Image icon2 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
				listBox3.SelectedItem = "NoHat.rbxm";
        		GlobalVars.UserCustomization.Hat3 = listBox3.SelectedItem.ToString();
        		Image icon3 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox2.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat1.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox2.Text = GlobalVars.UserCustomization.Hat1;
                }

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox3.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat2.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox3.Text = GlobalVars.UserCustomization.Hat2;
                }

                if (File.Exists(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalPaths.hatdir + @"\\" + GlobalVars.UserCustomization.Hat3.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = GlobalVars.UserCustomization.Hat3;
                }
            }
		}
		
		//faces
		
		void ListBox4SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.facedir))
        	{
        		GlobalVars.UserCustomization.Face = listBox4.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.facedir + "\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;

                if (File.Exists(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox6.Text = File.ReadAllText(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox6.Text = GlobalVars.UserCustomization.Face;
                }
            }
		}
		
		void Button45Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.facedir))
        	{
        		Random random = new Random();
				int randomFace1  = random.Next(listBox4.Items.Count);
				listBox4.SelectedItem = listBox4.Items[randomFace1];
        		GlobalVars.UserCustomization.Face = listBox4.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.facedir + "\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;

                if (File.Exists(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox6.Text = File.ReadAllText(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox6.Text = GlobalVars.UserCustomization.Face;
                }
            }			
		}
		
		void Button44Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.facedir))
        	{
				listBox4.SelectedItem = "DefaultFace.rbxm";
        		GlobalVars.UserCustomization.Face = listBox4.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.facedir + "\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;

                if (File.Exists(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox6.Text = File.ReadAllText(GlobalPaths.facedir + @"\\" + GlobalVars.UserCustomization.Face.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox6.Text = GlobalVars.UserCustomization.Face;
                }
            }
		}
		
		//t-shirt
		
		void ListBox5SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.tshirtdir))
        	{
                string previtem = listBox5.SelectedItem.ToString();
                textBox12.Text = "";
                comboBox3.SelectedItem = "Roblox";
                listBox5.SelectedItem = previtem;
                GlobalVars.UserCustomization.TShirt = listBox5.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.tshirtdir + "\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;

                if (File.Exists(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox7.Text = File.ReadAllText(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox7.Text = GlobalVars.UserCustomization.TShirt;
                }
            }
		}
		
		void Button47Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.tshirtdir))
        	{
                textBox12.Text = "";
                comboBox3.SelectedItem = "Roblox";
                Random random = new Random();
				int randomTShirt1  = random.Next(listBox5.Items.Count);
				listBox5.SelectedItem = listBox5.Items[randomTShirt1];
        		GlobalVars.UserCustomization.TShirt = listBox5.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.tshirtdir + "\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;

                if (File.Exists(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox7.Text = File.ReadAllText(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox7.Text = GlobalVars.UserCustomization.TShirt;
                }
            }
		}
		
		void Button46Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.tshirtdir))
        	{
                textBox12.Text = "";
                comboBox3.SelectedItem = "Roblox";
                listBox5.SelectedItem = "NoTShirt.rbxm";
        		GlobalVars.UserCustomization.TShirt = listBox5.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.tshirtdir + "\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;

                if (File.Exists(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox7.Text = File.ReadAllText(GlobalPaths.tshirtdir + @"\\" + GlobalVars.UserCustomization.TShirt.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox7.Text = GlobalVars.UserCustomization.TShirt;
                }
            }
		}
		
		//shirt
		
		void ListBox6SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.shirtdir))
        	{
                string previtem = listBox6.SelectedItem.ToString();
                textBox11.Text = "";
                comboBox2.SelectedItem = "Roblox";
                listBox6.SelectedItem = previtem;
                GlobalVars.UserCustomization.Shirt = listBox6.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.shirtdir + "\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;

                if (File.Exists(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox8.Text = File.ReadAllText(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox8.Text = GlobalVars.UserCustomization.Shirt;
                }
            }
		}
		
		void Button49Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.shirtdir))
        	{
                textBox11.Text = "";
                comboBox2.SelectedItem = "Roblox";
                Random random = new Random();
				int randomShirt1  = random.Next(listBox6.Items.Count);
				listBox6.SelectedItem = listBox6.Items[randomShirt1];
        		GlobalVars.UserCustomization.Shirt = listBox6.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.shirtdir + "\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;

                if (File.Exists(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox8.Text = File.ReadAllText(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox8.Text = GlobalVars.UserCustomization.Shirt;
                }
            }
		}
		
		void Button48Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.shirtdir))
        	{
                textBox11.Text = "";
                comboBox2.SelectedItem = "Roblox";
                listBox6.SelectedItem = "NoShirt.rbxm";
        		GlobalVars.UserCustomization.Shirt = listBox6.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.shirtdir + "\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;

                if (File.Exists(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox8.Text = File.ReadAllText(GlobalPaths.shirtdir + @"\\" + GlobalVars.UserCustomization.Shirt.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox8.Text = GlobalVars.UserCustomization.Shirt;
                }
            }
		}
		
		//pants
		
		void ListBox7SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.pantsdir))
        	{
                string previtem = listBox7.SelectedItem.ToString();
                textBox13.Text = "";
                comboBox1.SelectedItem = "Roblox";
                listBox7.SelectedItem = previtem;
                GlobalVars.UserCustomization.Pants = listBox7.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.pantsdir + "\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;

                if (File.Exists(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox9.Text = File.ReadAllText(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox9.Text = GlobalVars.UserCustomization.Pants;
                }
            }
		}
		
		void Button51Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.pantsdir))
        	{
                textBox13.Text = "";
                comboBox1.SelectedItem = "Roblox";
                Random random = new Random();
				int randomPants1  = random.Next(listBox7.Items.Count);
				listBox7.SelectedItem = listBox7.Items[randomPants1];
        		GlobalVars.UserCustomization.Pants = listBox7.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.pantsdir + "\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;

                if (File.Exists(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox9.Text = File.ReadAllText(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox9.Text = GlobalVars.UserCustomization.Pants;
                }
            }
		}
		
		void Button50Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.pantsdir))
        	{
                textBox13.Text = "";
                comboBox1.SelectedItem = "Roblox";
                listBox7.SelectedItem = "NoPants.rbxm";
        		GlobalVars.UserCustomization.Pants = listBox7.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.pantsdir + "\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;

                if (File.Exists(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox9.Text = File.ReadAllText(GlobalPaths.pantsdir + @"\\" + GlobalVars.UserCustomization.Pants.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox9.Text = GlobalVars.UserCustomization.Pants;
                }
            }
		}
		
		//head
		
		void ListBox8SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.headdir))
        	{
        		GlobalVars.UserCustomization.Head = listBox8.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.headdir + "\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;

                if (File.Exists(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox5.Text = File.ReadAllText(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox5.Text = GlobalVars.UserCustomization.Head;
                }
            }
		}
		
		void Button57Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.headdir))
        	{
				Random random = new Random();
				int randomHead1  = random.Next(listBox8.Items.Count);
				listBox8.SelectedItem = listBox8.Items[randomHead1];
        		GlobalVars.UserCustomization.Head = listBox8.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.headdir + "\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;

                if (File.Exists(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox5.Text = File.ReadAllText(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox5.Text = GlobalVars.UserCustomization.Head;
                }
            }
		}
		
		void Button56Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.headdir))
        	{
				listBox8.SelectedItem = "DefaultHead.rbxm";
        		GlobalVars.UserCustomization.Head = listBox8.SelectedItem.ToString();
        		Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.headdir + "\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;

                if (File.Exists(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox5.Text = File.ReadAllText(GlobalPaths.headdir + @"\\" + GlobalVars.UserCustomization.Head.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox5.Text = GlobalVars.UserCustomization.Head;
                }
            }
		}
		
		//body
		
		void Button1Click(object sender, EventArgs e)
		{
			SelectedPart = "Head";
			label2.Text = SelectedPart;	
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			SelectedPart = "Torso";
			label2.Text = SelectedPart;	
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			SelectedPart = "Right Arm";
			label2.Text = SelectedPart;				
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			SelectedPart = "Left Arm";
			label2.Text = SelectedPart;				
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			SelectedPart = "Right Leg";
			label2.Text = SelectedPart;	
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			SelectedPart = "Left Leg";
			label2.Text = SelectedPart;	
		}
		
		Color ConvertStringtoColor(string CString)
		{
			var p = CString.Split(new char[]{',',']'});

			int A = Convert.ToInt32(p[0].Substring(p[0].IndexOf('=') + 1));
			int R = Convert.ToInt32(p[1].Substring(p[1].IndexOf('=') + 1));
			int G = Convert.ToInt32(p[2].Substring(p[2].IndexOf('=') + 1));
			int B = Convert.ToInt32(p[3].Substring(p[3].IndexOf('=') + 1));
			
			return Color.FromArgb(A,R,G,B);
		}

        void ChangeColorOfPart(int ColorID)
        {
            ChangeColorOfPart(ColorID, PartColorList.Find(x => x.ColorID == ColorID).ButtonColor);
        }

        void ChangeColorOfPart(int ColorID, Color ButtonColor)
		{
            ChangeColorOfPart(SelectedPart, ColorID, ButtonColor);
        }

        void ChangeColorOfPart(string part, int ColorID, Color ButtonColor)
        {
            switch (part)
            {
                case "Head":
                    GlobalVars.UserCustomization.HeadColorID = ColorID;
                    GlobalVars.UserCustomization.HeadColorString = ButtonColor.ToString();
                    button1.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
                    break;
                case "Torso":
                    GlobalVars.UserCustomization.TorsoColorID = ColorID;
                    GlobalVars.UserCustomization.TorsoColorString = ButtonColor.ToString();
                    button2.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
                    break;
                case "Right Arm":
                    GlobalVars.UserCustomization.RightArmColorID = ColorID;
                    GlobalVars.UserCustomization.RightArmColorString = ButtonColor.ToString();
                    button3.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
                    break;
                case "Left Arm":
                    GlobalVars.UserCustomization.LeftArmColorID = ColorID;
                    GlobalVars.UserCustomization.LeftArmColorString = ButtonColor.ToString();
                    button4.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
                    break;
                case "Right Leg":
                    GlobalVars.UserCustomization.RightLegColorID = ColorID;
                    GlobalVars.UserCustomization.RightLegColorString = ButtonColor.ToString();
                    button5.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
                    break;
                case "Left Leg":
                    GlobalVars.UserCustomization.LeftLegColorID = ColorID;
                    GlobalVars.UserCustomization.LeftLegColorString = ButtonColor.ToString();
                    button6.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);
                    break;
                default:
                    break;
            }
        }

        void Button7Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(1);
		}
		
		void Button8Click(object sender, EventArgs e)
		{		
			ChangeColorOfPart(208);			
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(194);
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(199);
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(26);
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(21);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(24);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(226);
		}
		
		void Button18Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(23);
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(107);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(102);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(11);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(45);
		}
		
		void Button21Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(135);
		}
		
		void Button20Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(106);
		}
		
		void Button19Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(105);
		}
		
		void Button26Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(141);
		}
		
		void Button25Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(28);
		}
		
		void Button24Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(37);
		}
		
		void Button23Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(119);
		}
		
		void Button30Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(29);
		}
		
		void Button29Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(151);
		}
		
		void Button28Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(38);
		}
		
		void Button27Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(192);
		}
		
		void Button34Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(104);
		}
		
		void Button33Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(9);
		}
		
		void Button32Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(101);
		}
		
		void Button31Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(5);
		}
		
		void Button38Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(153);
		}
		
		void Button37Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(217);
		}
		
		void Button36Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(18);
		}
		
		void Button35Click(object sender, EventArgs e)
		{
			ChangeColorOfPart(125);
		}

        private void button69_Click(object sender, EventArgs e)
        {
            ChangeColorOfPart(22);
        }

        private void button70_Click(object sender, EventArgs e)
        {
            ChangeColorOfPart(128);
        }

        void Button39Click(object sender, EventArgs e)
		{
            Random rand = new Random();

			for (int i=1; i <= 6; i++)
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
		
		void Button40Click(object sender, EventArgs e)
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
			button1.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.HeadColorString);
			button2.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.TorsoColorString);
			button3.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightArmColorString);
			button4.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftArmColorString);
			button5.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.RightLegColorString);
			button6.BackColor = ConvertStringtoColor(GlobalVars.UserCustomization.LeftLegColorString);
		}
		
		//3dview
		
		void Button43Click(object sender, EventArgs e)
		{
			GlobalFuncs.ReloadLoadoutValue();
			string luafile = "rbxasset://scripts\\\\CSView.lua";
			string mapfile = GlobalPaths.BasePathLauncher + "\\preview\\content\\fonts\\3DView.rbxl";
			string rbxexe = GlobalPaths.BasePathLauncher + "\\preview\\3DView.exe";
			string quote = "\"";
			string args = quote + mapfile + "\" -script \"" + GlobalFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); _G.CS3DView(0,'Player'," + GlobalVars.Loadout + ");" + quote;
			try
			{
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
                client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
            }
			catch (Exception ex)
            {
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		//Icon
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
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.UserCustomization.CharacterID = textBox1.Text;
		}
		
		//extra
		
		void ListBox9SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.extradir))
        	{
				try
				{
        			GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.UserCustomization.ExtraSelectionIsHat = false;
                    if (File.Exists(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.UserCustomization.Extra;
                    }
                }
				catch (Exception)
                {
					if (Directory.Exists(GlobalPaths.hatdir))
        			{
						GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.UserCustomization.ExtraSelectionIsHat = true;
                        if (File.Exists(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.UserCustomization.Extra;
                        }
                    }
				}
        	}
		}
		
		void Button59Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.extradir))
        	{
				Random random = new Random();
				int randomItem1  = random.Next(listBox9.Items.Count);
				listBox9.SelectedItem = listBox9.Items[randomItem1];
        		try
				{
        			GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.UserCustomization.ExtraSelectionIsHat = false;
                    if (File.Exists(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.UserCustomization.Extra;
                    }
                }
				catch (Exception)
                {
					if (Directory.Exists(GlobalPaths.hatdir))
        			{
						GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.UserCustomization.ExtraSelectionIsHat = true;
                        if (File.Exists(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.UserCustomization.Extra;
                        }
                    }
				}
        	}
		}
		
		void Button58Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalPaths.extradir))
        	{
				listBox9.SelectedItem = "NoExtra.rbxm";
        		try
				{
        			GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.UserCustomization.ExtraSelectionIsHat = false;
                    if (File.Exists(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.UserCustomization.Extra;
                    }
                }
				catch (Exception)
                {
					if (Directory.Exists(GlobalPaths.hatdir))
        			{
						GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.UserCustomization.ExtraSelectionIsHat = true;
                        if (File.Exists(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.UserCustomization.Extra;
                        }
                    }
				}
        	}
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
            switch (checkBox1.Checked)
            {
                case true:
                    GlobalVars.UserCustomization.ShowHatsInExtra = true;

                    if (Directory.Exists(GlobalPaths.hatdir))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.hatdir);
                        FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.Equals(String.Empty))
                            {
                                continue;
                            }

                            if (file.Name.Equals("NoHat.rbxm"))
                            {
                                continue;
                            }

                            listBox9.Items.Add(file.Name);
                        }
                    }
                    break;
                case false:
                    GlobalVars.UserCustomization.ShowHatsInExtra = false;
                    listBox9.Items.Clear();

                    if (Directory.Exists(GlobalPaths.extradir))
                    {
                        DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.extradir);
                        FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                        foreach (FileInfo file in Files)
                        {
                            if (file.Name.Equals(String.Empty))
                            {
                                continue;
                            }

                            listBox9.Items.Add(file.Name);
                        }
                    }

                    listBox9.SelectedItem = "NoExtra.rbxm";
                    try
                    {
                        GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
                        Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
                        pictureBox9.Image = icon1;
                        GlobalVars.UserCustomization.ExtraSelectionIsHat = false;
                        if (File.Exists(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalPaths.extradir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.UserCustomization.Extra;
                        }
                    }
                    catch (Exception)
                    {
                        if (Directory.Exists(GlobalPaths.hatdir))
                        {
                            GlobalVars.UserCustomization.Extra = listBox9.SelectedItem.ToString();
                            Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + ".png");
                            pictureBox9.Image = icon1;
                            GlobalVars.UserCustomization.ExtraSelectionIsHat = true;
                            if (File.Exists(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt"))
                            {
                                textBox10.Text = File.ReadAllText(GlobalPaths.hatdir + "\\" + GlobalVars.UserCustomization.Extra.Replace(".rbxm", "") + "_desc.txt");
                            }
                            else
                            {
                                textBox10.Text = GlobalVars.UserCustomization.Extra;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }		
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

            try
            {
                Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradirIcons + "\\" + GlobalVars.UserConfiguration.PlayerName + ".png");
                pictureBox10.Image = icon1;
            }
            catch (Exception)
            {
                Image icon1 = GlobalFuncs.LoadImage(GlobalPaths.extradir + "\\NoExtra.png");
                pictureBox10.Image = icon1;
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

        private void button61_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 194, 24, 24, 119, 119);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 101, 24, 24, 9, 9);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 23, 24, 24, 119, 119);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 101, 24, 24, 119, 119);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 45, 24, 24, 119, 119);
        }

        private void button67_Click(object sender, EventArgs e)
        {
            ApplyPreset(106, 194, 106, 106, 119, 119);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            ApplyPreset(106, 119, 106, 106, 119, 119);
        }

        private void button65_Click(object sender, EventArgs e)
        {
            ApplyPreset(9, 194, 9, 9, 119, 119);
        }

        private void button71_Click(object sender, EventArgs e)
        {
            GlobalFuncs.Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
            MessageBox.Show("Outfit Saved!");
        }

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

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            listBox6.SelectedItem = "NoShirt.rbxm";
            GlobalVars.UserCustomization.Shirt = Custom_Shirt_URL + textBox11.Text;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            listBox5.SelectedItem = "NoTShirt.rbxm";
            GlobalVars.UserCustomization.TShirt = Custom_T_Shirt_URL + textBox12.Text;
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            listBox7.SelectedItem = "NoPants.rbxm";
            GlobalVars.UserCustomization.Pants = Custom_Pants_URL + textBox13.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 1:
                    Custom_Shirt_URL = "http://finobe.com/asset/?id=";
                    break;
                default:
                    Custom_Shirt_URL = "http://www.roblox.com/asset/?id=";
                    break;
            }

            if (!string.IsNullOrWhiteSpace(textBox11.Text))
            {
                GlobalVars.UserCustomization.Shirt = Custom_Shirt_URL + textBox11.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    Custom_Pants_URL = "http://finobe.com/asset/?id=";
                    break;
                default:
                    Custom_Pants_URL = "http://www.roblox.com/asset/?id=";
                    break;
            }

            if (!string.IsNullOrWhiteSpace(textBox13.Text))
            {
                GlobalVars.UserCustomization.Pants = Custom_Pants_URL + textBox13.Text;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 1:
                    Custom_T_Shirt_URL = "http://finobe.com/asset/?id=";
                    break;
                default:
                    Custom_T_Shirt_URL = "http://www.roblox.com/asset/?id=";
                    break;
            }

            if (!string.IsNullOrWhiteSpace(textBox12.Text))
            {
                GlobalVars.UserCustomization.TShirt = Custom_T_Shirt_URL + textBox12.Text;
            }
        }
    }
}
