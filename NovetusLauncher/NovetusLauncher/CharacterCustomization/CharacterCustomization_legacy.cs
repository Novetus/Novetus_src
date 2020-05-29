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
using NovetusFuncs;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of CharacterCustomization.
	/// </summary>
	public partial class CharacterCustomization_legacy : Form
	{
		private string SelectedPart = "Head";
        List<PartColors> PartColorList;

        public CharacterCustomization_legacy()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            InitColors();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        void InitColors()
        {
            PartColorList = new List<PartColors>()
            {
                //White
                new PartColors{ ColorID = 1, ButtonColor = button7.BackColor },
                //Light stone grey
                new PartColors{ ColorID = 208, ButtonColor = button8.BackColor },
                //Medium stone grey
                new PartColors{ ColorID = 194, ButtonColor = button9.BackColor },
                //Dark stone grey
                new PartColors{ ColorID = 199, ButtonColor = button10.BackColor },
                //Black
                new PartColors{ ColorID = 26, ButtonColor = button14.BackColor },
                //Bright red
                new PartColors{ ColorID = 21, ButtonColor = button13.BackColor },
                //Bright yellow
                new PartColors{ ColorID = 24, ButtonColor = button12.BackColor },
                //Cool yellow
                new PartColors{ ColorID = 226, ButtonColor = button11.BackColor },
                //Bright blue
                new PartColors{ ColorID = 23, ButtonColor = button18.BackColor },
                //Bright bluish green
                new PartColors{ ColorID = 107, ButtonColor = button17.BackColor },
                //Medium blue
                new PartColors{ ColorID = 102, ButtonColor = button16.BackColor },
                //Pastel Blue
                new PartColors{ ColorID = 11, ButtonColor = button15.BackColor },
                //Light blue
                new PartColors{ ColorID = 45, ButtonColor = button22.BackColor },
                //Sand blue
                new PartColors{ ColorID = 135, ButtonColor = button21.BackColor },
                //Bright orange
                new PartColors{ ColorID = 106, ButtonColor = button20.BackColor },
                //Br. yellowish orange
                new PartColors{ ColorID = 105, ButtonColor = button19.BackColor },
                //Earth green
                new PartColors{ ColorID = 141, ButtonColor = button26.BackColor },
                //Dark green
                new PartColors{ ColorID = 28, ButtonColor = button25.BackColor },
                //Bright green
                new PartColors{ ColorID = 37, ButtonColor = button24.BackColor },
                //Br. yellowish green
                new PartColors{ ColorID = 119, ButtonColor = button23.BackColor },
                //Medium green
                new PartColors{ ColorID = 29, ButtonColor = button30.BackColor },
                //Sand green
                new PartColors{ ColorID = 151, ButtonColor = button29.BackColor },
                //Dark orange
                new PartColors{ ColorID = 38, ButtonColor = button28.BackColor },
                //Reddish brown
                new PartColors{ ColorID = 192, ButtonColor = button27.BackColor },
                //Bright violet
                new PartColors{ ColorID = 104, ButtonColor = button34.BackColor },
                //Light reddish violet
                new PartColors{ ColorID = 9, ButtonColor = button33.BackColor },
                //Medium red
                new PartColors{ ColorID = 101, ButtonColor = button32.BackColor },
                //Brick yellow
                new PartColors{ ColorID = 5, ButtonColor = button31.BackColor },
                //Sand red
                new PartColors{ ColorID = 153, ButtonColor = button38.BackColor },
                //Brown
                new PartColors{ ColorID = 217, ButtonColor = button37.BackColor },
                //Nougat
                new PartColors{ ColorID = 18, ButtonColor = button36.BackColor },
                //Light orange
                new PartColors{ ColorID = 125, ButtonColor = button35.BackColor },
                // RARE 2006 COLORS!!
                //Med. reddish violet
                new PartColors{ ColorID = 22, ButtonColor = button69.BackColor },
                //Dark nougat
                new PartColors{ ColorID = 128, ButtonColor = button70.BackColor }
            };
        }

        void CharacterCustomizationLoad(object sender, EventArgs e)
		{
			//body
			label2.Text = SelectedPart;
			button1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			button2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			button3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			button4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			button5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			button6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
			
			//icon
			label5.Text = GlobalVars.Custom_Icon_Offline;
			
			//charid
			textBox1.Text = GlobalVars.CharacterID;
			
			checkBox1.Checked = GlobalVars.Custom_Extra_ShowHats;

            //discord
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InCustomization, GlobalVars.Map);
        	
        	LauncherFuncs.ReloadLoadtextValue();
		}
		
		void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();
                listBox6.Items.Clear();
                listBox7.Items.Clear();
                listBox8.Items.Clear();
                listBox9.Items.Clear();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
            {
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
                    Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradirIcons + "\\" + GlobalVars.PlayerName + ".png");
                    pictureBox10.Image = icon1;
                }
                catch (Exception) when (!Env.Debugging)
                {
                    Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\NoExtra.png");
                    pictureBox10.Image = icon1;
                }
            } 
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
     		{
                //hats
                listBox4.Items.Clear();
				listBox5.Items.Clear();
				listBox6.Items.Clear();
				listBox7.Items.Clear();
				listBox8.Items.Clear();
				listBox9.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.hatdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.hatdir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox1.Items.Add(file.Name);
   						listBox2.Items.Add(file.Name);
   						listBox3.Items.Add(file.Name);
					}
					listBox1.SelectedItem = GlobalVars.Custom_Hat1ID_Offline;
					listBox2.SelectedItem = GlobalVars.Custom_Hat2ID_Offline;
					listBox3.SelectedItem = GlobalVars.Custom_Hat3ID_Offline;
					listBox1.Enabled = true;
        			listBox2.Enabled = true;
        			listBox3.Enabled = true;
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox1.Image = icon1;
        			Image icon2 = LauncherFuncs.LoadImage(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox2.Image = icon2;
        			Image icon3 = LauncherFuncs.LoadImage(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox3.Image = icon3;
                    if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox2.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox2.Text = GlobalVars.Custom_Hat1ID_Offline;
                    }

                    if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox3.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox3.Text = GlobalVars.Custom_Hat2ID_Offline;
                    }

                    if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox4.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox4.Text = GlobalVars.Custom_Hat3ID_Offline;
                    }
                }
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])//your specific tabname
     		{
                //faces
                listBox1.Items.Clear();
				listBox2.Items.Clear();
				listBox3.Items.Clear();
				listBox5.Items.Clear();
				listBox6.Items.Clear();
				listBox7.Items.Clear();
				listBox8.Items.Clear();
				listBox9.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.facedir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.facedir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox4.Items.Add(file.Name);
					}
					listBox4.SelectedItem = GlobalVars.Custom_Face_Offline;
					listBox4.Enabled = true;
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox4.Image = icon1;

                    if (File.Exists(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox6.Text = File.ReadAllText(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox6.Text = GlobalVars.Custom_Face_Offline;
                    }
                }
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])//your specific tabname
     		{
                //faces
                listBox1.Items.Clear();
				listBox2.Items.Clear();
				listBox3.Items.Clear();
				listBox4.Items.Clear();
				listBox6.Items.Clear();
				listBox7.Items.Clear();
				listBox8.Items.Clear();
				listBox9.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.tshirtdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.tshirtdir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox5.Items.Add(file.Name);
					}
					listBox5.SelectedItem = GlobalVars.Custom_T_Shirt_Offline;
					listBox5.Enabled = true;
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox5.Image = icon1;

                    if (File.Exists(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox7.Text = File.ReadAllText(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox7.Text = GlobalVars.Custom_T_Shirt_Offline;
                    }
                }
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])//your specific tabname
     		{
                //faces
                listBox1.Items.Clear();
				listBox2.Items.Clear();
				listBox3.Items.Clear();
				listBox4.Items.Clear();
				listBox5.Items.Clear();
				listBox7.Items.Clear();
				listBox8.Items.Clear();
				listBox9.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.shirtdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.shirtdir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox6.Items.Add(file.Name);
					}
					listBox6.SelectedItem = GlobalVars.Custom_Shirt_Offline;
					listBox6.Enabled = true;
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox6.Image = icon1;

                    if (File.Exists(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox8.Text = File.ReadAllText(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox8.Text = GlobalVars.Custom_Shirt_Offline;
                    }
                }
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])//your specific tabname
     		{
                //faces
                listBox1.Items.Clear();
				listBox2.Items.Clear();
				listBox3.Items.Clear();
				listBox4.Items.Clear();
				listBox5.Items.Clear();
				listBox6.Items.Clear();
				listBox8.Items.Clear();
				listBox9.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.pantsdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.pantsdir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox7.Items.Add(file.Name);
					}
					listBox7.SelectedItem = GlobalVars.Custom_Pants_Offline;
					listBox7.Enabled = true;
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox7.Image = icon1;

                    if (File.Exists(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox9.Text = File.ReadAllText(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox9.Text = GlobalVars.Custom_Pants_Offline;
                    }
                }
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage8"])//your specific tabname
     		{
                //faces
                listBox1.Items.Clear();
				listBox2.Items.Clear();
				listBox3.Items.Clear();
				listBox4.Items.Clear();
				listBox5.Items.Clear();
				listBox6.Items.Clear();
				listBox7.Items.Clear();
				listBox9.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.headdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.headdir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox8.Items.Add(file.Name);
					}
					listBox8.SelectedItem = GlobalVars.Custom_Head_Offline;
					listBox8.Enabled = true;
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox8.Image = icon1;

                    if (File.Exists(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox5.Text = File.ReadAllText(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox5.Text = GlobalVars.Custom_Head_Offline;
                    }
                }
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage9"])//your specific tabname
     		{
                //faces
                listBox1.Items.Clear();
				listBox2.Items.Clear();
				listBox3.Items.Clear();
				listBox4.Items.Clear();
				listBox5.Items.Clear();
				listBox6.Items.Clear();
				listBox7.Items.Clear();
				listBox8.Items.Clear();
        		
        		if (Directory.Exists(GlobalVars.extradir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.extradir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
					{
						if (file.Name.Equals(String.Empty))
						{
   							continue;
						}
					
						listBox9.Items.Add(file.Name);
					}
        		}
        		
        		if (GlobalVars.Custom_Extra_ShowHats == true)
        		{
        			if (Directory.Exists(GlobalVars.hatdir))
        			{
        				DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.hatdir);
						FileInfo[] Files = dinfo.GetFiles("*.rbxm");
						foreach( FileInfo file in Files )
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
        		
        		listBox9.SelectedItem = GlobalVars.Custom_Extra;
				listBox9.Enabled = true;
        		try
				{
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
                    if (File.Exists(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.Custom_Extra;
                    }
                }
				catch(Exception) when (!Env.Debugging)
                {
					if (Directory.Exists(GlobalVars.hatdir))
        			{
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
                        if (File.Exists(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.Custom_Extra;
                        }
                    }
				}
     		}
		}
		
		void CharacterCustomizationClose(object sender, CancelEventArgs e)
		{
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InLauncher, "");
            LauncherFuncs.ReloadLoadtextValue();
		}
		
		// hats
		
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
        		GlobalVars.Custom_Hat1ID_Offline = listBox1.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox2.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox2.Text = GlobalVars.Custom_Hat1ID_Offline;
                }
            }
        }
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox3.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox3.Text = GlobalVars.Custom_Hat2ID_Offline;
                }
            }
		}
		
		void ListBox3SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = GlobalVars.Custom_Hat3ID_Offline;
                }
            }
		}
		
		void Button41Click(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
        		Random random = new Random();
				int randomHat1  = random.Next(listBox1.Items.Count);
				listBox1.SelectedItem = listBox1.Items[randomHat1];
        		GlobalVars.Custom_Hat1ID_Offline = listBox1.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        		int randomHat2  = random.Next(listBox2.Items.Count);
				listBox2.SelectedItem = listBox1.Items[randomHat2];
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
        		int randomHat3  = random.Next(listBox3.Items.Count);
				listBox3.SelectedItem = listBox1.Items[randomHat3];
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox2.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox2.Text = GlobalVars.Custom_Hat1ID_Offline;
                }

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox3.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox3.Text = GlobalVars.Custom_Hat2ID_Offline;
                }

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = GlobalVars.Custom_Hat3ID_Offline;
                }
            }
		}
		
		void Button42Click(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
				listBox1.SelectedItem = "NoHat.rbxm";
        		GlobalVars.Custom_Hat1ID_Offline = listBox1.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
				listBox2.SelectedItem = "NoHat.rbxm";
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
				listBox3.SelectedItem = "NoHat.rbxm";
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox2.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox2.Text = GlobalVars.Custom_Hat1ID_Offline;
                }

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox3.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox3.Text = GlobalVars.Custom_Hat2ID_Offline;
                }

                if (File.Exists(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalVars.hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = GlobalVars.Custom_Hat3ID_Offline;
                }
            }
		}
		
		//faces
		
		void ListBox4SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.facedir))
        	{
        		GlobalVars.Custom_Face_Offline = listBox4.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.facedir + "\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;

                if (File.Exists(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox6.Text = File.ReadAllText(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox6.Text = GlobalVars.Custom_Face_Offline;
                }
            }
		}
		
		void Button45Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.facedir))
        	{
        		Random random = new Random();
				int randomFace1  = random.Next(listBox4.Items.Count);
				listBox4.SelectedItem = listBox4.Items[randomFace1];
        		GlobalVars.Custom_Face_Offline = listBox4.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.facedir + "\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;

                if (File.Exists(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox6.Text = File.ReadAllText(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox6.Text = GlobalVars.Custom_Face_Offline;
                }
            }			
		}
		
		void Button44Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.facedir))
        	{
				listBox4.SelectedItem = "DefaultFace.rbxm";
        		GlobalVars.Custom_Face_Offline = listBox4.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.facedir + "\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;

                if (File.Exists(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox6.Text = File.ReadAllText(GlobalVars.facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox6.Text = GlobalVars.Custom_Face_Offline;
                }
            }
		}
		
		//t-shirt
		
		void ListBox5SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.tshirtdir))
        	{
        		GlobalVars.Custom_T_Shirt_Offline = listBox5.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.tshirtdir + "\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;

                if (File.Exists(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox7.Text = File.ReadAllText(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox7.Text = GlobalVars.Custom_T_Shirt_Offline;
                }
            }
		}
		
		void Button47Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.tshirtdir))
        	{
				Random random = new Random();
				int randomTShirt1  = random.Next(listBox5.Items.Count);
				listBox5.SelectedItem = listBox5.Items[randomTShirt1];
        		GlobalVars.Custom_T_Shirt_Offline = listBox5.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.tshirtdir + "\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;

                if (File.Exists(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox7.Text = File.ReadAllText(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox7.Text = GlobalVars.Custom_T_Shirt_Offline;
                }
            }
		}
		
		void Button46Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.tshirtdir))
        	{
				listBox5.SelectedItem = "NoTShirt.rbxm";
        		GlobalVars.Custom_T_Shirt_Offline = listBox5.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.tshirtdir + "\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;

                if (File.Exists(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox7.Text = File.ReadAllText(GlobalVars.tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox7.Text = GlobalVars.Custom_T_Shirt_Offline;
                }
            }
		}
		
		//shirt
		
		void ListBox6SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.shirtdir))
        	{
        		GlobalVars.Custom_Shirt_Offline = listBox6.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.shirtdir + "\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;

                if (File.Exists(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox8.Text = File.ReadAllText(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox8.Text = GlobalVars.Custom_Shirt_Offline;
                }
            }
		}
		
		void Button49Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.shirtdir))
        	{
				Random random = new Random();
				int randomShirt1  = random.Next(listBox6.Items.Count);
				listBox6.SelectedItem = listBox6.Items[randomShirt1];
        		GlobalVars.Custom_Shirt_Offline = listBox6.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.shirtdir + "\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;

                if (File.Exists(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox8.Text = File.ReadAllText(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox8.Text = GlobalVars.Custom_Shirt_Offline;
                }
            }
		}
		
		void Button48Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.shirtdir))
        	{
				listBox6.SelectedItem = "NoShirt.rbxm";
        		GlobalVars.Custom_Shirt_Offline = listBox6.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.shirtdir + "\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;

                if (File.Exists(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox8.Text = File.ReadAllText(GlobalVars.shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox8.Text = GlobalVars.Custom_Shirt_Offline;
                }
            }
		}
		
		//pants
		
		void ListBox7SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.pantsdir))
        	{
        		GlobalVars.Custom_Pants_Offline = listBox7.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.pantsdir + "\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;

                if (File.Exists(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox9.Text = File.ReadAllText(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox9.Text = GlobalVars.Custom_Pants_Offline;
                }
            }
		}
		
		void Button51Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.pantsdir))
        	{
				Random random = new Random();
				int randomPants1  = random.Next(listBox7.Items.Count);
				listBox7.SelectedItem = listBox7.Items[randomPants1];
        		GlobalVars.Custom_Pants_Offline = listBox7.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.pantsdir + "\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;

                if (File.Exists(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox9.Text = File.ReadAllText(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox9.Text = GlobalVars.Custom_Pants_Offline;
                }
            }
		}
		
		void Button50Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.pantsdir))
        	{
				listBox7.SelectedItem = "NoPants.rbxm";
        		GlobalVars.Custom_Pants_Offline = listBox7.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.pantsdir + "\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;

                if (File.Exists(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox9.Text = File.ReadAllText(GlobalVars.pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox9.Text = GlobalVars.Custom_Pants_Offline;
                }
            }
		}
		
		//head
		
		void ListBox8SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.headdir))
        	{
        		GlobalVars.Custom_Head_Offline = listBox8.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.headdir + "\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;

                if (File.Exists(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox5.Text = File.ReadAllText(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox5.Text = GlobalVars.Custom_Head_Offline;
                }
            }
		}
		
		void Button57Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.headdir))
        	{
				Random random = new Random();
				int randomHead1  = random.Next(listBox8.Items.Count);
				listBox8.SelectedItem = listBox8.Items[randomHead1];
        		GlobalVars.Custom_Head_Offline = listBox8.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.headdir + "\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;

                if (File.Exists(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox5.Text = File.ReadAllText(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox5.Text = GlobalVars.Custom_Head_Offline;
                }
            }
		}
		
		void Button56Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.headdir))
        	{
				listBox8.SelectedItem = "DefaultHead.rbxm";
        		GlobalVars.Custom_Head_Offline = listBox8.SelectedItem.ToString();
        		Image icon1 = LauncherFuncs.LoadImage(GlobalVars.headdir + "\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;

                if (File.Exists(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt"))
                {
                    textBox5.Text = File.ReadAllText(GlobalVars.headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + "_desc.txt");
                }
                else
                {
                    textBox5.Text = GlobalVars.Custom_Head_Offline;
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
		
		void ChangeColorOfPart(int ColorID, Color ButtonColor)
		{
			if (SelectedPart == "Head")
			{
				GlobalVars.HeadColorID = ColorID;
				GlobalVars.ColorMenu_HeadColor = ButtonColor.ToString();
				button1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			}
			else if (SelectedPart == "Torso")
			{
				GlobalVars.TorsoColorID = ColorID;
				GlobalVars.ColorMenu_TorsoColor = ButtonColor.ToString();
				button2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			}
			else if (SelectedPart == "Right Arm")
			{
				GlobalVars.RightArmColorID = ColorID;
				GlobalVars.ColorMenu_RightArmColor = ButtonColor.ToString();
				button3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			}
			else if (SelectedPart == "Left Arm")
			{
				GlobalVars.LeftArmColorID = ColorID;
				GlobalVars.ColorMenu_LeftArmColor = ButtonColor.ToString();
				button4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			}
			else if (SelectedPart == "Right Leg")
			{
				GlobalVars.RightLegColorID = ColorID;
				GlobalVars.ColorMenu_RightLegColor = ButtonColor.ToString();
				button5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			}
			else if (SelectedPart == "Left Leg")
			{
				GlobalVars.LeftLegColorID = ColorID;
				GlobalVars.ColorMenu_LeftLegColor = ButtonColor.ToString();
				button6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
			}
		}

        void ChangeColorOfPart(string part, int ColorID, Color ButtonColor)
        {
            if (part == "Head")
            {
                GlobalVars.HeadColorID = ColorID;
                GlobalVars.ColorMenu_HeadColor = ButtonColor.ToString();
                button1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
            }
            else if (part == "Torso")
            {
                GlobalVars.TorsoColorID = ColorID;
                GlobalVars.ColorMenu_TorsoColor = ButtonColor.ToString();
                button2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
            }
            else if (part == "Right Arm")
            {
                GlobalVars.RightArmColorID = ColorID;
                GlobalVars.ColorMenu_RightArmColor = ButtonColor.ToString();
                button3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
            }
            else if (part == "Left Arm")
            {
                GlobalVars.LeftArmColorID = ColorID;
                GlobalVars.ColorMenu_LeftArmColor = ButtonColor.ToString();
                button4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
            }
            else if (part == "Right Leg")
            {
                GlobalVars.RightLegColorID = ColorID;
                GlobalVars.ColorMenu_RightLegColor = ButtonColor.ToString();
                button5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
            }
            else if (part == "Left Leg")
            {
                GlobalVars.LeftLegColorID = ColorID;
                GlobalVars.ColorMenu_LeftLegColor = ButtonColor.ToString();
                button6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
            }
        }

        void ChangeColorOfPart(int ColorID)
        {
            ChangeColorOfPart(ColorID, PartColorList.Find(x => x.ColorID == ColorID).ButtonColor);
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

				if (i == 1)
				{
                    ChangeColorOfPart("Head", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);  
				}
				else if (i == 2)
				{
                    ChangeColorOfPart("Torso", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
				}
				else if (i == 3)
				{
                    ChangeColorOfPart("Left Arm", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                }
				else if (i == 4)
				{
                    ChangeColorOfPart("Right Arm", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                }
				else if (i == 5)
				{
                    ChangeColorOfPart("Left Leg", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                }
				else if (i == 6)
				{
                    ChangeColorOfPart("Right Leg", PartColorList[RandomColor].ColorID, PartColorList[RandomColor].ButtonColor);
                } 
			}
		}
		
		void Button40Click(object sender, EventArgs e)
		{
			GlobalVars.HeadColorID = 24;
			GlobalVars.TorsoColorID = 23;
			GlobalVars.LeftArmColorID = 24;
			GlobalVars.RightArmColorID = 24;
			GlobalVars.LeftLegColorID = 119;
			GlobalVars.RightLegColorID = 119;
			GlobalVars.CharacterID = "";
			GlobalVars.ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
			GlobalVars.ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
			GlobalVars.ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
			GlobalVars.ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
			GlobalVars.ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
			GlobalVars.ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
			button1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
			button2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
			button3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
			button4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
			button5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
			button6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
		}
		
		//3dview
		
		void Button43Click(object sender, EventArgs e)
		{
			LauncherFuncs.ReloadLoadtextValue();
			string luafile = "rbxasset://scripts\\\\CSView.lua";
			string mapfile = GlobalVars.BasePathLauncher + "\\preview\\content\\fonts\\3DView.rbxl";
			string rbxexe = GlobalVars.BasePathLauncher + "\\preview\\3DView.exe";
			string quote = "\"";
			string args = quote + mapfile + "\" -script \"" + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); _G.CS3DView(0,'Player'," + GlobalVars.loadtext + ");" + quote;
			try
			{
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
                client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
            }
			catch (Exception ex) when (!Env.Debugging)
            {
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		//Icon
		void Button52Click(object sender, EventArgs e)
		{
			GlobalVars.Custom_Icon_Offline = "BC";
			label5.Text = GlobalVars.Custom_Icon_Offline;
		}
		
		void Button53Click(object sender, EventArgs e)
		{
			GlobalVars.Custom_Icon_Offline = "TBC";
			label5.Text = GlobalVars.Custom_Icon_Offline;		
		}
		
		void Button54Click(object sender, EventArgs e)
		{
			GlobalVars.Custom_Icon_Offline = "OBC";
			label5.Text = GlobalVars.Custom_Icon_Offline;		
		}
		
		void Button55Click(object sender, EventArgs e)
		{
			GlobalVars.Custom_Icon_Offline = "NBC";
			label5.Text = GlobalVars.Custom_Icon_Offline;
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.CharacterID = textBox1.Text;
		}
		
		//extra
		
		void ListBox9SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.extradir))
        	{
				try
				{
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
                    if (File.Exists(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.Custom_Extra;
                    }
                }
				catch(Exception) when (!Env.Debugging)
                {
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
                        if (File.Exists(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.Custom_Extra;
                        }
                    }
				}
        	}
		}
		
		void Button59Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.extradir))
        	{
				Random random = new Random();
				int randomItem1  = random.Next(listBox9.Items.Count);
				listBox9.SelectedItem = listBox9.Items[randomItem1];
        		try
				{
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
                    if (File.Exists(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.Custom_Extra;
                    }
                }
				catch(Exception) when (!Env.Debugging)
                {
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
                        if (File.Exists(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.Custom_Extra;
                        }
                    }
				}
        	}
		}
		
		void Button58Click(object sender, EventArgs e)
		{
			if (Directory.Exists(GlobalVars.extradir))
        	{
				listBox9.SelectedItem = "NoExtra.rbxm";
        		try
				{
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
                    if (File.Exists(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.Custom_Extra;
                    }
                }
				catch(Exception) when (!Env.Debugging)
                {
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
                        if (File.Exists(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.Custom_Extra;
                        }
                    }
				}
        	}
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				GlobalVars.Custom_Extra_ShowHats = true;
				
				if (Directory.Exists(GlobalVars.hatdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.hatdir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
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
			else if (checkBox1.Checked == false)
			{
				GlobalVars.Custom_Extra_ShowHats = false;
				listBox9.Items.Clear();
				
				if (Directory.Exists(GlobalVars.extradir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(GlobalVars.extradir);
					FileInfo[] Files = dinfo.GetFiles("*.rbxm");
					foreach( FileInfo file in Files )
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
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
                    if (File.Exists(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                    {
                        textBox10.Text = File.ReadAllText(GlobalVars.extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                    }
                    else
                    {
                        textBox10.Text = GlobalVars.Custom_Extra;
                    }
                }
				catch(Exception) when (!Env.Debugging)
                {
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
                        if (File.Exists(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt"))
                        {
                            textBox10.Text = File.ReadAllText(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + "_desc.txt");
                        }
                        else
                        {
                            textBox10.Text = GlobalVars.Custom_Extra;
                        }
                    }
				}
			}			
		}

        private void button60_Click(object sender, EventArgs e)
        {
            IconLoader icon = new IconLoader();
            try
            {
                icon.LoadImage();
            }
            catch (Exception) when (!Env.Debugging)
            {
            }

            if (!string.IsNullOrWhiteSpace(icon.getInstallOutcome()))
            {
                MessageBox.Show(icon.getInstallOutcome());
            }

            try
            {
                Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradirIcons + "\\" + GlobalVars.PlayerName + ".png");
                pictureBox10.Image = icon1;
            }
            catch (Exception) when (!Env.Debugging)
            {
                Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\NoExtra.png");
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
            LauncherFuncs.Customization(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, true);
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
    }
}
