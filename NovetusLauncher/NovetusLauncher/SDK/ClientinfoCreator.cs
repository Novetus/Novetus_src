/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 11/28/2016
 * Time: 7:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

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
		private string Warning = "";
		private string SelectedClientDesc = "";
		private bool LegacyMode = false;
		private string SelectedClientMD5 = "";
		private string SelectedClientScriptMD5 = "";
		private string SelectedClientInfoPath = "";
		private bool Locked = false;
		private bool FixScriptMapMode = false;
		private bool AlreadyHasSecurity = false;
		private string CustomArgs = "";
		
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
		
		void ClientinfoCreatorLoad(object sender, EventArgs e)
		{
            string cfgpath = GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName;
            if (!File.Exists(cfgpath))
            {
                LauncherFuncs.Config(cfgpath, true);
            }
            else
            {
                LauncherFuncs.Config(cfgpath, false);
            }

            if (GlobalVars.AdminMode == true)
			{
				checkBox4.Visible = true;
			}
			else
			{
				checkBox4.Visible = false;
			}
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
			textBox2.Text = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
			SelectedClientMD5 = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
		}
		
		void TextBox3TextChanged(object sender, EventArgs e)
		{
			textBox3.Text = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
			SelectedClientScriptMD5 = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(SelectedClientInfoPath))
			{
				FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
				if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) 
				{
    				SelectedClientInfoPath = folderBrowserDialog1.SelectedPath;
				}
			}
			
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
        		textBox2.Text = ClientMD5.ToUpper(CultureInfo.InvariantCulture);
				SelectedClientMD5 = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
        	}
        	else
        	{
        		MessageBox.Show("Cannot load '" + ClientName.Trim('/') + "'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
					
        	string ClientScriptMD5 = File.Exists(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") ? SecurityFuncs.CalculateMD5(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") : "";
        			
			if (!string.IsNullOrWhiteSpace(ClientScriptMD5))
        	{
        		textBox3.Text = ClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
				SelectedClientScriptMD5 = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
			}
			else
        	{
        		MessageBox.Show("Cannot load '" + GlobalVars.ScriptName + ".lua'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for script", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
		
		void CheckBox6CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox6.Checked == true)
			{
				FixScriptMapMode = true;
			}
			else if (checkBox6.Checked == false)
			{
				FixScriptMapMode = false;
			}		
		}
		
		void CheckBox7CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox7.Checked == true)
			{
				AlreadyHasSecurity = true;
			}
			else if (checkBox7.Checked == false)
			{
				AlreadyHasSecurity = false;
			}		
		}
		
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			UsesPlayerName = false;
			UsesID = false;
			Warning = "";
			LegacyMode = false;
			FixScriptMapMode = false;
			AlreadyHasSecurity = false;
			SelectedClientDesc = "";
			SelectedClientMD5 = "";
			SelectedClientScriptMD5 = "";
			SelectedClientInfoPath = "";
			CustomArgs = "";
			Locked = false;
			checkBox1.Checked = UsesPlayerName;
			checkBox2.Checked = UsesID;
			checkBox3.Checked = LegacyMode;
			checkBox4.Checked = Locked;
			checkBox6.Checked = FixScriptMapMode;
			checkBox7.Checked = AlreadyHasSecurity;
			textBox3.Text = SelectedClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
			textBox2.Text = SelectedClientMD5.ToUpper(CultureInfo.InvariantCulture);
			textBox1.Text = SelectedClientDesc;
			textBox4.Text = CustomArgs;
			textBox5.Text = Warning;
		}
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
        	{
				ofd.Filter = "Novetus Clientinfo files (*.nov)|*.nov";
            	ofd.FilterIndex = 2;
            	ofd.FileName = "clientinfo.nov";
            	ofd.Title = "Load clientinfo.nov";
            	if (ofd.ShowDialog() == DialogResult.OK)
            	{
					string line1;
					string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, Decryptline10, Decryptline11;
					
					using(StreamReader reader = new StreamReader(ofd.FileName)) 
					{
						SelectedClientInfoPath = Path.GetDirectoryName(ofd.FileName);
    					line1 = reader.ReadLine();
					}

					string ConvertedLine = "";

					try
					{
						label9.Text = "v2";
						ConvertedLine = SecurityFuncs.Base64DecodeNew(line1);
					}
					catch(Exception)
					{
						label9.Text = "v1";
						ConvertedLine = SecurityFuncs.Base64DecodeOld(line1);
					}

					string[] result = ConvertedLine.Split('|');
					Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
    				Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
    				Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
    				Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
    				Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
    				Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
    				Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
    				Decryptline8 = SecurityFuncs.Base64Decode(result[7]);
    				Decryptline9 = SecurityFuncs.Base64Decode(result[8]);
    				Decryptline10 = SecurityFuncs.Base64Decode(result[9]);
    				Decryptline11 = SecurityFuncs.Base64Decode(result[10]);
    				
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
    						checkBox4.Checked = Locked;
    					}
    				}
    				else
    				{
    					Boolean bline8 = Convert.ToBoolean(Decryptline8);
    					Locked = bline8;
    					checkBox4.Checked = Locked;
    				}
					
					Boolean bline1 = Convert.ToBoolean(Decryptline1);
					UsesPlayerName = bline1;
					
					Boolean bline2 = Convert.ToBoolean(Decryptline2);
					UsesID = bline2;
					
					Warning = Decryptline3;
					
					Boolean bline4 = Convert.ToBoolean(Decryptline4);
					LegacyMode = bline4;
					
					SelectedClientMD5 = Decryptline5;
					
					SelectedClientScriptMD5 = Decryptline6;
					
					SelectedClientDesc = Decryptline7;
					
					bool bline9 = Convert.ToBoolean(Decryptline9);
					FixScriptMapMode = bline9;
			
					bool bline10 = Convert.ToBoolean(Decryptline10);
					AlreadyHasSecurity = bline10;
					
					CustomArgs = Decryptline11;
					
					checkBox1.Checked = UsesPlayerName;
					checkBox2.Checked = UsesID;
					checkBox3.Checked = LegacyMode;
					checkBox6.Checked = FixScriptMapMode;
					checkBox7.Checked = AlreadyHasSecurity;
					textBox3.Text = SelectedClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
					textBox2.Text = SelectedClientMD5.ToUpper(CultureInfo.InvariantCulture);
					textBox1.Text = SelectedClientDesc;
					textBox4.Text = CustomArgs;
					textBox5.Text = Warning;
            	}
			}
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
        	{
            	sfd.Filter = "Novetus Clientinfo files (*.nov)|*.nov";
            	sfd.FilterIndex = 2;
            	sfd.FileName = "clientinfo.nov";
            	sfd.Title = "Save clientinfo.nov";
            	
            	if (sfd.ShowDialog() == DialogResult.OK)
            	{
            		string[] lines = { 
            			SecurityFuncs.Base64Encode(UsesPlayerName.ToString()),
            			SecurityFuncs.Base64Encode(UsesID.ToString()),
            			SecurityFuncs.Base64Encode(Warning.ToString()),
            			SecurityFuncs.Base64Encode(LegacyMode.ToString()),
            			SecurityFuncs.Base64Encode(SelectedClientMD5.ToString()),
            			SecurityFuncs.Base64Encode(SelectedClientScriptMD5.ToString()),
            			SecurityFuncs.Base64Encode(SelectedClientDesc.ToString()),
            			SecurityFuncs.Base64Encode(Locked.ToString()),
            			SecurityFuncs.Base64Encode(FixScriptMapMode.ToString()),
            			SecurityFuncs.Base64Encode(AlreadyHasSecurity.ToString()),
            			SecurityFuncs.Base64Encode(CustomArgs.ToString())
            		};
            		File.WriteAllText(sfd.FileName, SecurityFuncs.Base64Encode(string.Join("|",lines)));
            		SelectedClientInfoPath = Path.GetDirectoryName(sfd.FileName);
            	}     
			}

			label9.Text = "v2";
		}
		
		void TextBox4TextChanged(object sender, EventArgs e)
		{
			CustomArgs = textBox4.Text;		
		}
		
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			Warning = textBox5.Text;			
		}

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientScriptDocumentation csd = new ClientScriptDocumentation();
            csd.Show();
        }

        private void AddClientinfoText(string text)
        {
            textBox4.Paste(text);
        }

        //tags
        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientinfoText("<client></client>");
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientinfoText("<server></server>");
        }

        private void soloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientinfoText("<solo></solo>");
        }

        private void studioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientinfoText("<studio></studio>");
        }

        private void no3dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientinfoText("<no3d></no3d>");
        }

        //variables

        private void variableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem senderitem = (ToolStripMenuItem)sender;
            AddClientinfoText(senderitem.Text);
        }

        private void saveAsTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text file (*.txt)|*.txt";
                sfd.FilterIndex = 2;
                sfd.FileName = "clientinfo.txt";
                sfd.Title = "Save clientinfo.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = {
                        UsesPlayerName.ToString(),
                        UsesID.ToString(),
                        Warning.ToString(),
                        LegacyMode.ToString(),
                        SelectedClientDesc.ToString(),
                        FixScriptMapMode.ToString(),
                        AlreadyHasSecurity.ToString(),
                        CustomArgs.ToString()
                    };
                    File.WriteAllLines(sfd.FileName, lines);
                    SelectedClientInfoPath = Path.GetDirectoryName(sfd.FileName);
                }
            }
        }
    }
}
