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
		private string hatdir = GlobalVars.CustomPlayerDir + "\\hats";
		private string facedir = GlobalVars.CustomPlayerDir + "\\faces";
		private string headdir = GlobalVars.CustomPlayerDir + "\\heads";
		private string tshirtdir = GlobalVars.CustomPlayerDir + "\\tshirts";
		private string shirtdir = GlobalVars.CustomPlayerDir + "\\shirts";
		private string pantsdir = GlobalVars.CustomPlayerDir + "\\pants";
		private string extradir = GlobalVars.CustomPlayerDir + "\\custom";
		
		public CharacterCustomization()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
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
                    Image icon1 = Image.FromFile(extradir + "\\icons\\" + GlobalVars.PlayerName + ".png");
                    pictureBox10.Image = icon1;
                }
                catch (Exception)
                {
                    Image icon1 = Image.FromFile(extradir + "\\NoExtra.png");
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
        		
        		if (Directory.Exists(hatdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(hatdir);
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
        			Image icon1 = Image.FromFile(hatdir + @"\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox1.Image = icon1;
        			Image icon2 = Image.FromFile(hatdir + @"\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        			pictureBox2.Image = icon2;
        			Image icon3 = Image.FromFile(hatdir + @"\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
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
        		
        		if (Directory.Exists(facedir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(facedir);
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
        			Image icon1 = Image.FromFile(facedir + @"\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
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
        		
        		if (Directory.Exists(tshirtdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(tshirtdir);
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
        			Image icon1 = Image.FromFile(tshirtdir + @"\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
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
        		
        		if (Directory.Exists(shirtdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(shirtdir);
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
        			Image icon1 = Image.FromFile(shirtdir + @"\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
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
        		
        		if (Directory.Exists(pantsdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(pantsdir);
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
        			Image icon1 = Image.FromFile(pantsdir + @"\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
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
        		
        		if (Directory.Exists(headdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(headdir);
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
        			Image icon1 = Image.FromFile(headdir + @"\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
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
        		
        		if (Directory.Exists(extradir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(extradir);
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
        			if (Directory.Exists(hatdir))
        			{
        				DirectoryInfo dinfo = new DirectoryInfo(hatdir);
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
        			Image icon1 = Image.FromFile(extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
				}
				catch(Exception)
				{
					if (Directory.Exists(hatdir))
        			{
        				Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
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
        	if (Directory.Exists(hatdir))
        	{
        		GlobalVars.Custom_Hat1ID_Offline = listBox1.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        	}
		}
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(hatdir))
        	{
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
        	}
		}
		
		void ListBox3SelectedIndexChanged(object sender, EventArgs e)
		{
        	if (Directory.Exists(hatdir))
        	{
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
        	}
		}
		
		void Button41Click(object sender, EventArgs e)
		{
        	if (Directory.Exists(hatdir))
        	{
        		Random random = new Random();
				int randomHat1  = random.Next(listBox1.Items.Count);
				listBox1.SelectedItem = listBox1.Items[randomHat1];
        		GlobalVars.Custom_Hat1ID_Offline = listBox1.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
        		int randomHat2  = random.Next(listBox2.Items.Count);
				listBox2.SelectedItem = listBox1.Items[randomHat2];
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
        		int randomHat3  = random.Next(listBox3.Items.Count);
				listBox3.SelectedItem = listBox1.Items[randomHat3];
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
        	}
		}
		
		void Button42Click(object sender, EventArgs e)
		{
        	if (Directory.Exists(hatdir))
        	{
				listBox1.SelectedItem = "NoHat.rbxm";
        		GlobalVars.Custom_Hat1ID_Offline = listBox1.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat1ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox1.Image = icon1;
				listBox2.SelectedItem = "NoHat.rbxm";
        		GlobalVars.Custom_Hat2ID_Offline = listBox2.SelectedItem.ToString();
        		Image icon2 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat2ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox2.Image = icon2;
				listBox3.SelectedItem = "NoHat.rbxm";
        		GlobalVars.Custom_Hat3ID_Offline = listBox3.SelectedItem.ToString();
        		Image icon3 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Hat3ID_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox3.Image = icon3;
        	}
		}
		
		//faces
		
		void ListBox4SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(facedir))
        	{
        		GlobalVars.Custom_Face_Offline = listBox4.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(facedir + "\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;
        	}
		}
		
		void Button45Click(object sender, EventArgs e)
		{
			if (Directory.Exists(facedir))
        	{
        		Random random = new Random();
				int randomFace1  = random.Next(listBox4.Items.Count);
				listBox4.SelectedItem = listBox4.Items[randomFace1];
        		GlobalVars.Custom_Face_Offline = listBox4.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(facedir + "\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;
        	}			
		}
		
		void Button44Click(object sender, EventArgs e)
		{
			if (Directory.Exists(facedir))
        	{
				listBox4.SelectedItem = "DefaultFace.rbxm";
        		GlobalVars.Custom_Face_Offline = listBox4.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(facedir + "\\" + GlobalVars.Custom_Face_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox4.Image = icon1;
        	}
		}
		
		//t-shirt
		
		void ListBox5SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(tshirtdir))
        	{
        		GlobalVars.Custom_T_Shirt_Offline = listBox5.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(tshirtdir + "\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;
        	}
		}
		
		void Button47Click(object sender, EventArgs e)
		{
			if (Directory.Exists(tshirtdir))
        	{
				Random random = new Random();
				int randomTShirt1  = random.Next(listBox5.Items.Count);
				listBox5.SelectedItem = listBox5.Items[randomTShirt1];
        		GlobalVars.Custom_T_Shirt_Offline = listBox5.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(tshirtdir + "\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;
        	}
		}
		
		void Button46Click(object sender, EventArgs e)
		{
			if (Directory.Exists(tshirtdir))
        	{
				listBox5.SelectedItem = "NoTShirt.rbxm";
        		GlobalVars.Custom_T_Shirt_Offline = listBox5.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(tshirtdir + "\\" + GlobalVars.Custom_T_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox5.Image = icon1;
        	}
		}
		
		//shirt
		
		void ListBox6SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(shirtdir))
        	{
        		GlobalVars.Custom_Shirt_Offline = listBox6.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(shirtdir + "\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;
        	}
		}
		
		void Button49Click(object sender, EventArgs e)
		{
			if (Directory.Exists(shirtdir))
        	{
				Random random = new Random();
				int randomShirt1  = random.Next(listBox6.Items.Count);
				listBox6.SelectedItem = listBox6.Items[randomShirt1];
        		GlobalVars.Custom_Shirt_Offline = listBox6.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(shirtdir + "\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;
        	}
		}
		
		void Button48Click(object sender, EventArgs e)
		{
			if (Directory.Exists(shirtdir))
        	{
				listBox6.SelectedItem = "NoShirt.rbxm";
        		GlobalVars.Custom_Shirt_Offline = listBox6.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(shirtdir + "\\" + GlobalVars.Custom_Shirt_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox6.Image = icon1;
        	}
		}
		
		//pants
		
		void ListBox7SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(pantsdir))
        	{
        		GlobalVars.Custom_Pants_Offline = listBox7.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(pantsdir + "\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;
        	}
		}
		
		void Button51Click(object sender, EventArgs e)
		{
			if (Directory.Exists(pantsdir))
        	{
				Random random = new Random();
				int randomPants1  = random.Next(listBox7.Items.Count);
				listBox7.SelectedItem = listBox7.Items[randomPants1];
        		GlobalVars.Custom_Pants_Offline = listBox7.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(pantsdir + "\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;
        	}
		}
		
		void Button50Click(object sender, EventArgs e)
		{
			if (Directory.Exists(pantsdir))
        	{
				listBox7.SelectedItem = "NoPants.rbxm";
        		GlobalVars.Custom_Pants_Offline = listBox7.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(pantsdir + "\\" + GlobalVars.Custom_Pants_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox7.Image = icon1;
        	}
		}
		
		//head
		
		void ListBox8SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(headdir))
        	{
        		GlobalVars.Custom_Head_Offline = listBox8.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(headdir + "\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;
        	}
		}
		
		void Button57Click(object sender, EventArgs e)
		{
			if (Directory.Exists(headdir))
        	{
				Random random = new Random();
				int randomHead1  = random.Next(listBox8.Items.Count);
				listBox8.SelectedItem = listBox8.Items[randomHead1];
        		GlobalVars.Custom_Head_Offline = listBox8.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(headdir + "\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
        		pictureBox8.Image = icon1;
        	}
		}
		
		void Button56Click(object sender, EventArgs e)
		{
			if (Directory.Exists(headdir))
        	{
				listBox8.SelectedItem = "DefaultHead.rbxm";
        		GlobalVars.Custom_Head_Offline = listBox8.SelectedItem.ToString();
        		Image icon1 = Image.FromFile(headdir + "\\" + GlobalVars.Custom_Head_Offline.Replace(".rbxm", "") + ".png");
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
		
		void Button7Click(object sender, EventArgs e)
		{
			Color ButtonColor = button7.BackColor;
			int colorID = 1;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button8Click(object sender, EventArgs e)
		{		
			Color ButtonColor = button8.BackColor;
			int colorID = 208;
			ChangeColorOfPart(colorID, ButtonColor);			
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			Color ButtonColor = button9.BackColor;
			int colorID = 194;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			Color ButtonColor = button10.BackColor;
			int colorID = 199;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			Color ButtonColor = button14.BackColor;
			int colorID = 26;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			Color ButtonColor = button13.BackColor;
			int colorID = 21;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			Color ButtonColor = button12.BackColor;
			int colorID = 24;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			Color ButtonColor = button11.BackColor;
			int colorID = 226;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button18Click(object sender, EventArgs e)
		{
			Color ButtonColor = button18.BackColor;
			int colorID = 23;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			Color ButtonColor = button17.BackColor;
			int colorID = 107;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			Color ButtonColor = button16.BackColor;
			int colorID = 102;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			Color ButtonColor = button15.BackColor;
			int colorID = 11;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			Color ButtonColor = button22.BackColor;
			int colorID = 45;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button21Click(object sender, EventArgs e)
		{
			Color ButtonColor = button21.BackColor;
			int colorID = 135;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button20Click(object sender, EventArgs e)
		{
			Color ButtonColor = button20.BackColor;
			int colorID = 106;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button19Click(object sender, EventArgs e)
		{
			Color ButtonColor = button19.BackColor;
			int colorID = 105;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button26Click(object sender, EventArgs e)
		{
			Color ButtonColor = button26.BackColor;
			int colorID = 141;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button25Click(object sender, EventArgs e)
		{
			Color ButtonColor = button25.BackColor;
			int colorID = 28;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button24Click(object sender, EventArgs e)
		{
			Color ButtonColor = button24.BackColor;
			int colorID = 37;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button23Click(object sender, EventArgs e)
		{
			Color ButtonColor = button23.BackColor;
			int colorID = 119;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button30Click(object sender, EventArgs e)
		{
			Color ButtonColor = button30.BackColor;
			int colorID = 29;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button29Click(object sender, EventArgs e)
		{
			Color ButtonColor = button29.BackColor;
			int colorID = 151;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button28Click(object sender, EventArgs e)
		{
			Color ButtonColor = button28.BackColor;
			int colorID = 38;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button27Click(object sender, EventArgs e)
		{
			Color ButtonColor = button27.BackColor;
			int colorID = 192;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button34Click(object sender, EventArgs e)
		{
			Color ButtonColor = button34.BackColor;
			int colorID = 104;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button33Click(object sender, EventArgs e)
		{
			Color ButtonColor = button33.BackColor;
			int colorID = 9;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button32Click(object sender, EventArgs e)
		{
			Color ButtonColor = button32.BackColor;
			int colorID = 101;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button31Click(object sender, EventArgs e)
		{
			Color ButtonColor = button31.BackColor;
			int colorID = 5;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button38Click(object sender, EventArgs e)
		{
			Color ButtonColor = button38.BackColor;
			int colorID = 153;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button37Click(object sender, EventArgs e)
		{
			Color ButtonColor = button37.BackColor;
			int colorID = 217;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button36Click(object sender, EventArgs e)
		{
			Color ButtonColor = button36.BackColor;
			int colorID = 18;
			ChangeColorOfPart(colorID, ButtonColor);
		}
		
		void Button35Click(object sender, EventArgs e)
		{
			Color ButtonColor = button35.BackColor;
			int colorID = 125;
			ChangeColorOfPart(colorID, ButtonColor);
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
					GlobalVars.HeadColorID = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_HeadColor = ColorArray[RandomColor, 1];
					button1.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_HeadColor);
				}
				else if (i == 2)
				{
					GlobalVars.TorsoColorID = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_TorsoColor = ColorArray[RandomColor, 1];
					button2.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_TorsoColor);
				}
				else if (i == 3)
				{
					GlobalVars.RightArmColorID = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_RightArmColor = ColorArray[RandomColor, 1];
					button3.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightArmColor);
				}
				else if (i == 4)
				{
					GlobalVars.LeftArmColorID = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_LeftArmColor = ColorArray[RandomColor, 1];
					button4.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftArmColor);
				}
				else if (i == 5)
				{
					GlobalVars.RightLegColorID = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_RightLegColor = ColorArray[RandomColor, 1];
					button5.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_RightLegColor);
				}
				else if (i == 6)
				{
					GlobalVars.LeftLegColorID = Convert.ToInt32(ColorArray[RandomColor, 0]);
					GlobalVars.ColorMenu_LeftLegColor = ColorArray[RandomColor, 1];
					button6.BackColor = ConvertStringtoColor(GlobalVars.ColorMenu_LeftLegColor);
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
			if (Directory.Exists(extradir))
        	{
				try
				{
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = Image.FromFile(extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
				}
				catch(Exception)
				{
					if (Directory.Exists(hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
					}
				}
        	}
		}
		
		void Button59Click(object sender, EventArgs e)
		{
			if (Directory.Exists(extradir))
        	{
				Random random = new Random();
				int randomItem1  = random.Next(listBox9.Items.Count);
				listBox9.SelectedItem = listBox9.Items[randomItem1];
        		try
				{
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = Image.FromFile(extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
				}
				catch(Exception)
				{
					if (Directory.Exists(hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        				pictureBox9.Image = icon1;
        				GlobalVars.Custom_Extra_SelectionIsHat = true;
					}
				}
        	}
		}
		
		void Button58Click(object sender, EventArgs e)
		{
			if (Directory.Exists(extradir))
        	{
				listBox9.SelectedItem = "NoExtra.rbxm";
        		try
				{
        			GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        			Image icon1 = Image.FromFile(extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
				}
				catch(Exception)
				{
					if (Directory.Exists(hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
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
				
				if (Directory.Exists(hatdir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(hatdir);
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
				
				if (Directory.Exists(extradir))
        		{
        			DirectoryInfo dinfo = new DirectoryInfo(extradir);
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
        			Image icon1 = Image.FromFile(extradir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
        			pictureBox9.Image = icon1;
        			GlobalVars.Custom_Extra_SelectionIsHat = false;
				}
				catch(Exception)
				{
					if (Directory.Exists(hatdir))
        			{
						GlobalVars.Custom_Extra = listBox9.SelectedItem.ToString();
        				Image icon1 = Image.FromFile(hatdir + "\\" + GlobalVars.Custom_Extra.Replace(".rbxm", "") + ".png");
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

            if (!string.IsNullOrWhiteSpace(icon.installOutcome))
            {
                MessageBox.Show(icon.installOutcome);
            }

            try
            {
                Image icon1 = Image.FromFile(extradir + "\\icons\\" + GlobalVars.PlayerName + ".png");
                pictureBox10.Image = icon1;
            }
            catch (Exception)
            {
                Image icon1 = Image.FromFile(extradir + "\\NoExtra.png");
                pictureBox10.Image = icon1;
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVars.IsWebServerOn == true)
                {
                    string IP = SecurityFuncs.GetExternalIPAddress();
                    string localWebServerURL = "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString();
                    string localWebServer_BodyColors = localWebServerURL + "/charcustom/bodycolors.rbxm";
                    string charapp = localWebServer_BodyColors + ";" +
                        GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat1ID_Offline + ";" +
                        GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat2ID_Offline + ";" +
                        GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat3ID_Offline + ";" +
                        GlobalVars.WebServer_HeadDir + GlobalVars.Custom_Head_Offline + ";" +
                        GlobalVars.WebServer_FaceDir + GlobalVars.Custom_Face_Offline + ";" +
                        GlobalVars.WebServer_TShirtDir + GlobalVars.Custom_T_Shirt_Offline + ";" +
                        GlobalVars.WebServer_ShirtDir + GlobalVars.Custom_Shirt_Offline + ";" +
                        GlobalVars.WebServer_PantsDir + GlobalVars.WebServer_PantsDir;
                    textBox1.Text = charapp;
                    GlobalVars.CharacterID = charapp;
                }
                else
                {
                    MessageBox.Show("Could not generate charapp. Is are you running Novetus as as administrator and is the webserver running?");
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
    }
}
