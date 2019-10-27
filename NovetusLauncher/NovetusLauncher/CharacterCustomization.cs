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
using System.Net;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of CharacterCustomization.
	/// </summary>
	public partial class CharacterCustomization : Form
	{
		private string SelectedPart = "Head";
		private string[,] ColorArray;
		
		
		public CharacterCustomization()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            /// <summary>
            ///ColorArray[index, 0] = id (string)
            ///ColorArray[index, 1] = button color (string)
            /// </summary>
            ColorArray = new string[32, 2] {
			{ "1", button7.BackColor.ToString() }, 
			{ "208", button8.BackColor.ToString() },
			{ "194", button9.BackColor.ToString() }, 
			{ "199", button10.BackColor.ToString() },
			{ "26", button14.BackColor.ToString() },
			{ "21", button13.BackColor.ToString() },
			{ "24", button12.BackColor.ToString() },
			{ "226", button11.BackColor.ToString() },
			{ "23", button18.BackColor.ToString() },
			{ "107", button17.BackColor.ToString() },
			{ "102", button16.BackColor.ToString() },
			{ "11", button15.BackColor.ToString() },
			{ "45", button22.BackColor.ToString() },
			{ "135", button21.BackColor.ToString() },
			{ "106", button20.BackColor.ToString() },
			{ "105", button19.BackColor.ToString() },
			{ "141", button26.BackColor.ToString() },
			{ "28", button25.BackColor.ToString() },
			{ "37", button24.BackColor.ToString() },
			{ "119", button23.BackColor.ToString() },
			{ "29", button30.BackColor.ToString() },
			{ "151", button29.BackColor.ToString() },
			{ "38", button28.BackColor.ToString() },
			{ "192", button27.BackColor.ToString() },
			{ "104", button34.BackColor.ToString() },
			{ "9", button33.BackColor.ToString() },
			{ "101", button32.BackColor.ToString() },
			{ "5", button31.BackColor.ToString() },
			{ "153", button38.BackColor.ToString() },
			{ "217", button37.BackColor.ToString() },
			{ "18", button36.BackColor.ToString() },
			{ "125", button35.BackColor.ToString() }
			};
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
			GlobalVars.presence.details = "Customizing " + GlobalVars.PlayerName;
            GlobalVars.presence.state = "In Character Customization";
            GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In Character Customization";
            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
        	
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
                    Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\icons\\" + GlobalVars.PlayerName + ".png");
                    pictureBox10.Image = icon1;
                }
                catch (Exception)
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
				}
				catch(Exception)
				{
					if (Directory.Exists(GlobalVars.hatdir))
        			{
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
					}
				}
     		}
		}
		
		void CharacterCustomizationClose(object sender, CancelEventArgs e)
		{
            GlobalVars.presence.state = "In Launcher";
            GlobalVars.presence.details = "Selected " + GlobalVars.SelectedClient;
            GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In Launcher";
            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
			
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
        	}
		}
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
        	}
		}
		
		void ListBox3SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(GlobalVars.hatdir))
        	{
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
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

        void Button7Click(object sender, EventArgs e)
		{
			Color ButtonColor = button7.BackColor;
			ChangeColorOfPart(1, ButtonColor);
		}
		
		void Button8Click(object sender, EventArgs e)
		{		
			Color ButtonColor = button8.BackColor;
			ChangeColorOfPart(208, ButtonColor);			
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			Color ButtonColor = button9.BackColor;
			ChangeColorOfPart(194, ButtonColor);
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			Color ButtonColor = button10.BackColor;
			ChangeColorOfPart(199, ButtonColor);
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			Color ButtonColor = button14.BackColor;
			ChangeColorOfPart(26, ButtonColor);
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			Color ButtonColor = button13.BackColor;
			ChangeColorOfPart(21, ButtonColor);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			Color ButtonColor = button12.BackColor;
			ChangeColorOfPart(24, ButtonColor);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			Color ButtonColor = button11.BackColor;
			ChangeColorOfPart(226, ButtonColor);
		}
		
		void Button18Click(object sender, EventArgs e)
		{
			Color ButtonColor = button18.BackColor;
			ChangeColorOfPart(23, ButtonColor);
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			Color ButtonColor = button17.BackColor;
			ChangeColorOfPart(107, ButtonColor);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			Color ButtonColor = button16.BackColor;
			ChangeColorOfPart(102, ButtonColor);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			Color ButtonColor = button15.BackColor;
			ChangeColorOfPart(11, ButtonColor);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			Color ButtonColor = button22.BackColor;
			ChangeColorOfPart(45, ButtonColor);
		}
		
		void Button21Click(object sender, EventArgs e)
		{
			Color ButtonColor = button21.BackColor;
			ChangeColorOfPart(135, ButtonColor);
		}
		
		void Button20Click(object sender, EventArgs e)
		{
			Color ButtonColor = button20.BackColor;
			ChangeColorOfPart(106, ButtonColor);
		}
		
		void Button19Click(object sender, EventArgs e)
		{
			Color ButtonColor = button19.BackColor;
			ChangeColorOfPart(105, ButtonColor);
		}
		
		void Button26Click(object sender, EventArgs e)
		{
			Color ButtonColor = button26.BackColor;
			ChangeColorOfPart(141, ButtonColor);
		}
		
		void Button25Click(object sender, EventArgs e)
		{
			Color ButtonColor = button25.BackColor;
			ChangeColorOfPart(28, ButtonColor);
		}
		
		void Button24Click(object sender, EventArgs e)
		{
			Color ButtonColor = button24.BackColor;
			ChangeColorOfPart(37, ButtonColor);
		}
		
		void Button23Click(object sender, EventArgs e)
		{
			Color ButtonColor = button23.BackColor;
			ChangeColorOfPart(119, ButtonColor);
		}
		
		void Button30Click(object sender, EventArgs e)
		{
			Color ButtonColor = button30.BackColor;
			ChangeColorOfPart(29, ButtonColor);
		}
		
		void Button29Click(object sender, EventArgs e)
		{
			Color ButtonColor = button29.BackColor;
			ChangeColorOfPart(151, ButtonColor);
		}
		
		void Button28Click(object sender, EventArgs e)
		{
			Color ButtonColor = button28.BackColor;
			ChangeColorOfPart(38, ButtonColor);
		}
		
		void Button27Click(object sender, EventArgs e)
		{
			Color ButtonColor = button27.BackColor;
			ChangeColorOfPart(192, ButtonColor);
		}
		
		void Button34Click(object sender, EventArgs e)
		{
			Color ButtonColor = button34.BackColor;
			ChangeColorOfPart(104, ButtonColor);
		}
		
		void Button33Click(object sender, EventArgs e)
		{
			Color ButtonColor = button33.BackColor;
			ChangeColorOfPart(9, ButtonColor);
		}
		
		void Button32Click(object sender, EventArgs e)
		{
			Color ButtonColor = button32.BackColor;
			ChangeColorOfPart(101, ButtonColor);
		}
		
		void Button31Click(object sender, EventArgs e)
		{
			Color ButtonColor = button31.BackColor;
			ChangeColorOfPart(5, ButtonColor);
		}
		
		void Button38Click(object sender, EventArgs e)
		{
			Color ButtonColor = button38.BackColor;
			ChangeColorOfPart(153, ButtonColor);
		}
		
		void Button37Click(object sender, EventArgs e)
		{
			Color ButtonColor = button37.BackColor;
			ChangeColorOfPart(217, ButtonColor);
		}
		
		void Button36Click(object sender, EventArgs e)
		{
			Color ButtonColor = button36.BackColor;
			ChangeColorOfPart(18, ButtonColor);
		}
		
		void Button35Click(object sender, EventArgs e)
		{
			Color ButtonColor = button35.BackColor;
			ChangeColorOfPart(125, ButtonColor);
		}
		
		void Button39Click(object sender, EventArgs e)
		{
            Random rand = new Random();
			int RandomColor;
			
			for (int i=1; i <= 6; i++)
			{
				RandomColor = rand.Next(ColorArray.GetLength(0));
				if (i == 1)
				{
                    ChangeColorOfPart("Head", Convert.ToInt32(ColorArray[RandomColor, 0]), ConvertStringtoColor(ColorArray[RandomColor, 1]));  
				}
				else if (i == 2)
				{
                    ChangeColorOfPart("Torso", Convert.ToInt32(ColorArray[RandomColor, 0]), ConvertStringtoColor(ColorArray[RandomColor, 1]));
				}
				else if (i == 3)
				{
                    ChangeColorOfPart("Left Arm", Convert.ToInt32(ColorArray[RandomColor, 0]), ConvertStringtoColor(ColorArray[RandomColor, 1]));
                }
				else if (i == 4)
				{
                    ChangeColorOfPart("Right Arm", Convert.ToInt32(ColorArray[RandomColor, 0]), ConvertStringtoColor(ColorArray[RandomColor, 1]));
                }
				else if (i == 5)
				{
                    ChangeColorOfPart("Left Leg", Convert.ToInt32(ColorArray[RandomColor, 0]), ConvertStringtoColor(ColorArray[RandomColor, 1]));
                }
				else if (i == 6)
				{
                    ChangeColorOfPart("Right Leg", Convert.ToInt32(ColorArray[RandomColor, 0]), ConvertStringtoColor(ColorArray[RandomColor, 1]));
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
			string mapfile = GlobalVars.ConfigDir + "\\preview\\content\\fonts\\3DView.rbxl";
			string rbxexe = GlobalVars.ConfigDir + "\\preview\\3DView.exe";
			string quote = "\"";
			string args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); _G.CS3DView(0,'Player'," + GlobalVars.loadtext + ");" + quote;
			try
			{
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.Start();
			}
			catch (Exception ex)
			{
				DialogResult result2 = MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				}
				catch(Exception)
				{
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
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
				}
				catch(Exception)
				{
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
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
				}
				catch(Exception)
				{
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
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
				}
				catch(Exception)
				{
					if (Directory.Exists(GlobalVars.hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = LauncherFuncs.LoadImage(GlobalVars.hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
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
            catch (Exception)
            {
            }

            if (!string.IsNullOrWhiteSpace(icon.getInstallOutcome()))
            {
                MessageBox.Show(icon.getInstallOutcome());
            }

            try
            {
                Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\icons\\" + GlobalVars.PlayerName + ".png");
                pictureBox10.Image = icon1;
            }
            catch (Exception)
            {
                Image icon1 = LauncherFuncs.LoadImage(GlobalVars.extradir + "\\NoExtra.png");
                pictureBox10.Image = icon1;
            }
        }

        public void ApplyPreset(int head, int torso, int larm, int rarm, int lleg, int rleg)
        {
            ChangeColorOfPart("Head", head, ConvertStringtoColor(ColorArray[Convert.ToInt32(ColorArray.FindInDimensions(Convert.ToString(head))), 1]));
            ChangeColorOfPart("Torso", torso, ConvertStringtoColor(ColorArray[Convert.ToInt32(ColorArray.FindInDimensions(Convert.ToString(torso))), 1]));
            ChangeColorOfPart("Left Arm", larm, ConvertStringtoColor(ColorArray[Convert.ToInt32(ColorArray.FindInDimensions(Convert.ToString(larm))), 1]));
            ChangeColorOfPart("Right Arm", rarm, ConvertStringtoColor(ColorArray[Convert.ToInt32(ColorArray.FindInDimensions(Convert.ToString(rarm))), 1]));
            ChangeColorOfPart("Left Leg", lleg, ConvertStringtoColor(ColorArray[Convert.ToInt32(ColorArray.FindInDimensions(Convert.ToString(lleg))), 1]));
            ChangeColorOfPart("Right Leg", rleg, ConvertStringtoColor(ColorArray[Convert.ToInt32(ColorArray.FindInDimensions(Convert.ToString(rleg))), 1]));
        }

        private void button61_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 194, 24, 24, 119, 119);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 22, 24, 24, 9, 9);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 23, 24, 24, 119, 119);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 22, 24, 24, 119, 119);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            ApplyPreset(24, 11, 24, 24, 119, 119);
        }

        private void button67_Click(object sender, EventArgs e)
        {
            ApplyPreset(38, 194, 38, 38, 119, 119);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            ApplyPreset(128, 119, 128, 128, 119, 119);
        }

        private void button65_Click(object sender, EventArgs e)
        {
            ApplyPreset(9, 194, 9, 9, 119, 119);
        }

        /*
        private void button61_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVars.IsWebServerOn == true)
                {
                    string IP = SecurityFuncs.GetExternalIPAddress();
                    string localWebServerURL = "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString();
                    string localWebServer_CustomPlayerDir = localWebServerURL + "/charcustom/";
                    string localWebServer_HatDir = localWebServer_CustomPlayerDir + "hats/";
                    string localWebServer_FaceDir = localWebServer_CustomPlayerDir + "faces/";
                    string localWebServer_HeadDir = localWebServer_CustomPlayerDir + "heads/";
                    string localWebServer_TShirtDir = localWebServer_CustomPlayerDir + "tshirts/";
                    string localWebServer_ShirtDir = localWebServer_CustomPlayerDir + "shirts/";
                    string localWebServer_PantsDir = localWebServer_CustomPlayerDir + "pants/";
                    string localWebServer_ExtraDir = localWebServer_CustomPlayerDir + "custom/";
                    string localWebServer_BodyColors = localWebServer_CustomPlayerDir + "bodycolors.rbxm";
                    string charapp = localWebServer_BodyColors + ";" +
                        localWebServer_HatDir + GlobalVars.Custom_Hat1ID_Offline + ";" +
                        localWebServer_HatDir + GlobalVars.Custom_Hat2ID_Offline + ";" +
                        localWebServer_HatDir + GlobalVars.Custom_Hat3ID_Offline + ";" +
                        localWebServer_HeadDir + GlobalVars.Custom_Head_Offline + ";" +
                        localWebServer_FaceDir + GlobalVars.Custom_Face_Offline + ";" +
                        localWebServer_TShirtDir + GlobalVars.Custom_T_Shirt_Offline + ";" +
                        localWebServer_ShirtDir + GlobalVars.Custom_Shirt_Offline + ";" +
                        localWebServer_PantsDir + GlobalVars.Custom_Pants_Offline + ";" +
                        localWebServer_ExtraDir + GlobalVars.Custom_Extra;
                    textBox1.Text = charapp;
                    GlobalVars.CharacterID = charapp;
                }
                else
                {
                    MessageBox.Show("Could not generate charapp. Are you running Novetus as as administrator and is the webserver running?");
                    textBox1.Text = "";
                    GlobalVars.CharacterID = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Could not generate charapp. Error: " + ex.Message);
                textBox1.Text = "";
                GlobalVars.CharacterID = "";
            }
        }
        */
    }
}
