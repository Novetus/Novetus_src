/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 11/28/2016
 * Time: 7:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of ClientinfoCreator.
	/// </summary>
	public partial class ClientinfoEditor : Form
	{
		//clientinfocreator
		private bool UsesPlayerName = false;
		private bool UsesID = false;
		private bool LoadsAssetsOnline = false;
		private string SelectedClientDesc = "";
		private bool LegacyMode = false;
		private string SelectedClientMD5 = "";
		private string SelectedClientScriptMD5 = "";
		private string SelectedClientInfoPath = "";
		private bool Locked = false;
		
		public ClientinfoEditor()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				UsesPlayerName = true;
			}
			else if (checkBox1.Checked == false)
			{
				UsesPlayerName = false;
			}
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked == true)
			{
				UsesID = true;
			}
			else if (checkBox2.Checked == false)
			{
				UsesID = false;
			}
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			SelectedClientDesc = textBox1.Text;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
        	{
				ofd.Filter = "Text files (*.txt)|*.txt";
            	ofd.FilterIndex = 2;
            	ofd.FileName = "clientinfo.txt";
            	ofd.Title = "Load clientinfo.txt";
            	if (ofd.ShowDialog() == DialogResult.OK)
            	{
					string line1;
					string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8;
					
					using(StreamReader reader = new StreamReader(ofd.FileName)) 
					{
						SelectedClientInfoPath = Path.GetDirectoryName(ofd.FileName);
    					line1 = reader.ReadLine();
					}
			
					if (!SecurityFuncs.IsBase64String(line1))
						return;
					
					string ConvertedLine = SecurityFuncs.Base64Decode(line1);
					string[] result = ConvertedLine.Split('|');
					Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
    				Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
    				Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
    				Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
    				Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
    				Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
    				Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
    				
					try
    				{
    					Decryptline8 = SecurityFuncs.Base64Decode(result[7]);
    				
    					if (GlobalVars.AdminMode != true)
    					{
    						Boolean bline8 = Convert.ToBoolean(Decryptline8);
    						if (bline8 == true)
    						{
    							MessageBox.Show("This client is locked and therefore it cannot be loaded.","Novetus Launcher - Error when loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
    							return;
    						}
    						else
    						{
    							Locked = bline8;
    							checkBox4.Checked = false;
    						}
    					}
    					else
    					{
    						Boolean bline8 = Convert.ToBoolean(Decryptline8);
    						Locked = bline8;
    						checkBox4.Checked = false;
    					}
    				}
    				catch(IndexOutOfRangeException)
    				{
    				}
					
					Boolean bline1 = Convert.ToBoolean(Decryptline1);
					UsesPlayerName = bline1;
					
					Boolean bline2 = Convert.ToBoolean(Decryptline2);
					UsesID = bline2;
					
					Boolean bline3 = Convert.ToBoolean(Decryptline3);
					LoadsAssetsOnline = bline3;
					
					Boolean bline4 = Convert.ToBoolean(Decryptline4);
					LegacyMode = bline4;
					
					SelectedClientMD5 = Decryptline5;
					
					SelectedClientScriptMD5 = Decryptline6;
					
					SelectedClientDesc = Decryptline7;
					
					checkBox1.Checked = UsesPlayerName;
					checkBox2.Checked = UsesID;
					checkBox5.Checked = LoadsAssetsOnline;
					checkBox3.Checked = LegacyMode;
					textBox3.Text = SelectedClientScriptMD5.ToUpper();
					textBox2.Text = SelectedClientMD5.ToUpper();
					textBox1.Text = SelectedClientDesc;
            	}
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
        	{
            	sfd.Filter = "Text files (*.txt)|*.txt";
            	sfd.FilterIndex = 2;
            	sfd.FileName = "clientinfo.txt";
            	sfd.Title = "Save clientinfo.txt";

            	if (sfd.ShowDialog() == DialogResult.OK)
            	{
            		string[] lines = { 
            			SecurityFuncs.Base64Encode(UsesPlayerName.ToString()),
            			SecurityFuncs.Base64Encode(UsesID.ToString()),
            			SecurityFuncs.Base64Encode(LoadsAssetsOnline.ToString()),
            			SecurityFuncs.Base64Encode(LegacyMode.ToString()),
            			SecurityFuncs.Base64Encode(SelectedClientMD5.ToString()),
            			SecurityFuncs.Base64Encode(SelectedClientScriptMD5.ToString()),
            			SecurityFuncs.Base64Encode(SelectedClientDesc.ToString()),
            			SecurityFuncs.Base64Encode(Locked.ToString())
            		};
            		File.WriteAllText(sfd.FileName, SecurityFuncs.Base64Encode(string.Join("|",lines)));
            	}     
			}			
		}
		
		void ClientinfoCreatorLoad(object sender, EventArgs e)
		{
			if (GlobalVars.AdminMode == true)
			{
				checkBox4.Visible = true;
			}
			else
			{
				checkBox4.Visible = false;
			}
		}
		
		void CheckBox5CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox5.Checked == true)
			{
				LoadsAssetsOnline = true;
			}
			else if (checkBox5.Checked == false)
			{
				LoadsAssetsOnline = false;
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			UsesPlayerName = false;
			UsesID = false;
			LoadsAssetsOnline = false;
			LegacyMode = false;
			SelectedClientDesc = "";
			SelectedClientMD5 = "";
			SelectedClientScriptMD5 = "";
			checkBox1.Checked = UsesPlayerName;
			checkBox2.Checked = UsesID;
			checkBox5.Checked = LoadsAssetsOnline;
			checkBox3.Checked = LegacyMode;
			textBox3.Text = SelectedClientScriptMD5.ToUpper();
			textBox2.Text = SelectedClientMD5.ToUpper();
			textBox1.Text = SelectedClientDesc;
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked == true)
			{
				LegacyMode = true;
			}
			else if (checkBox3.Checked == false)
			{
				LegacyMode = false;
			}
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			textBox2.Text = textBox2.Text.ToUpper();
			SelectedClientMD5 = textBox2.Text.ToUpper();
		}
		
		void TextBox3TextChanged(object sender, EventArgs e)
		{
			textBox3.Text = textBox3.Text.ToUpper();
			SelectedClientScriptMD5 = textBox3.Text.ToUpper();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			string ClientName = "";
        			
    		if (!LegacyMode)
        	{
        		ClientName = "\\RobloxApp_Client.exe";
        	}
        	else
        	{
        		ClientName = "\\RobloxApp.exe";
        	}
    				
    		string ClientMD5 = File.Exists(SelectedClientInfoPath + ClientName) ? SecurityFuncs.CalculateMD5(SelectedClientInfoPath + ClientName) : "";
        			
        	if (!string.IsNullOrWhiteSpace(ClientMD5))
        	{
        		textBox2.Text = ClientMD5.ToUpper();
				SelectedClientMD5 = textBox2.Text.ToUpper();
        	}
        	else
        	{
        		MessageBox.Show("Cannot load '" + ClientName.Trim('/') + "'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
					
        	string ClientScriptMD5 = File.Exists(SelectedClientInfoPath + "\\content\\scripts\\CSMPFunctions.lua") ? SecurityFuncs.CalculateMD5(SelectedClientInfoPath + "\\content\\scripts\\CSMPFunctions.lua") : "";
        			
			if (!string.IsNullOrWhiteSpace(ClientScriptMD5))
        	{
        		textBox3.Text = ClientScriptMD5.ToUpper();
				SelectedClientScriptMD5 = textBox3.Text.ToUpper();
			}
			else
        	{
        		MessageBox.Show("Cannot load 'CSMPFunctions.lua'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for script", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
			
			MessageBox.Show("MD5s generated.","Novetus Launcher - Novetus Client SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox4.Checked == true)
			{
				Locked = true;
			}
			else if (checkBox4.Checked == false && Locked == true)
			{
				Locked = true;
			}		
		}
	}
}
