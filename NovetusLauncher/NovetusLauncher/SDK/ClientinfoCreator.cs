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
		private ClientInfo loadedClientInfo = new ClientInfo();
		private string SelectedClientInfoPath = "";
		private bool Locked = false;
		private bool IsVersion2 = false;
		
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
			checkBox1.Checked = loadedClientInfo.UsesPlayerName;
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			checkBox2.Checked = loadedClientInfo.UsesID;
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			loadedClientInfo.Description = textBox1.Text;
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

			checkBox4.Visible = GlobalVars.AdminMode;
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			checkBox3.Checked = loadedClientInfo.LegacyMode;
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			textBox2.Text = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
			loadedClientInfo.ClientMD5 = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
		}
		
		void TextBox3TextChanged(object sender, EventArgs e)
		{
			textBox3.Text = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
			loadedClientInfo.ScriptMD5 = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
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
        			
    		if (!loadedClientInfo.LegacyMode)
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
				textBox2.BackColor = System.Drawing.Color.Lime;
				loadedClientInfo.ClientMD5 = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
        	}
        	else
        	{
        		MessageBox.Show("Cannot load '" + ClientName.Trim('/') + "'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
					
        	string ClientScriptMD5 = File.Exists(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") ? SecurityFuncs.CalculateMD5(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") : "";
        			
			if (!string.IsNullOrWhiteSpace(ClientScriptMD5))
        	{
        		textBox3.Text = ClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
				textBox3.BackColor = System.Drawing.Color.Lime;
				loadedClientInfo.ScriptMD5 = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
			}
			else
        	{
        		MessageBox.Show("Cannot load '" + GlobalVars.ScriptName + ".lua'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for script", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
			
			MessageBox.Show("MD5s generated.","Novetus Launcher - Novetus Client SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			Locked = true;
		}
		
		void CheckBox6CheckedChanged(object sender, EventArgs e)
		{
			checkBox6.Checked = loadedClientInfo.Fix2007;	
		}
		
		void CheckBox7CheckedChanged(object sender, EventArgs e)
		{
			checkBox7.Checked = loadedClientInfo.AlreadyHasSecurity;
		}

		void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			checkBox5.Checked = loadedClientInfo.NoGraphicsOptions;
		}

		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			label9.Text = "Not Loaded";
			loadedClientInfo.UsesPlayerName = false;
			loadedClientInfo.UsesID = false;
			loadedClientInfo.Warning = "";
			loadedClientInfo.LegacyMode = false;
			loadedClientInfo.Fix2007 = false;
			loadedClientInfo.AlreadyHasSecurity = false;
			loadedClientInfo.Description = "";
			loadedClientInfo.ClientMD5 = "";
			loadedClientInfo.ScriptMD5 = "";
			SelectedClientInfoPath = "";
			loadedClientInfo.CommandLineArgs = "";
			Locked = false;
			checkBox1.Checked = loadedClientInfo.UsesPlayerName;
			checkBox2.Checked = loadedClientInfo.UsesID;
			checkBox3.Checked = loadedClientInfo.LegacyMode;
			checkBox4.Checked = Locked;
			checkBox6.Checked = loadedClientInfo.Fix2007;
			checkBox7.Checked = loadedClientInfo.AlreadyHasSecurity;
			checkBox5.Checked = loadedClientInfo.NoGraphicsOptions;
			textBox3.Text = loadedClientInfo.ScriptMD5.ToUpper(CultureInfo.InvariantCulture);
			textBox2.Text = loadedClientInfo.ClientMD5.ToUpper(CultureInfo.InvariantCulture);
			textBox1.Text = loadedClientInfo.Description;
			textBox4.Text = loadedClientInfo.CommandLineArgs;
			textBox5.Text = loadedClientInfo.Warning;
			textBox2.BackColor = System.Drawing.SystemColors.Control;
			textBox3.BackColor = System.Drawing.SystemColors.Control;
		}
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
        	{
				ofd.Filter = "Novetus Clientinfo files (*.nov)|*.nov";
            	ofd.FilterIndex = 1;
            	ofd.FileName = "clientinfo.nov";
            	ofd.Title = "Load clientinfo.nov";
            	if (ofd.ShowDialog() == DialogResult.OK)
            	{
					string line1;
					string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, Decryptline10, Decryptline11, Decryptline12;
					
					using(StreamReader reader = new StreamReader(ofd.FileName)) 
					{
						SelectedClientInfoPath = Path.GetDirectoryName(ofd.FileName);
    					line1 = reader.ReadLine();
					}

					string ConvertedLine = "";

					try
					{
						IsVersion2 = true;
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
					Decryptline12 = "";
					try
					{
						if (IsVersion2)
						{
							Decryptline12 = SecurityFuncs.Base64Decode(result[11]);
						}
					}
					catch (Exception)
					{
						label9.Text = "v2 (DEV)";
						IsVersion2 = false;
					}

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
						bool bline8 = Convert.ToBoolean(Decryptline8);
    					Locked = bline8;
    					checkBox4.Checked = Locked;
    				}

					bool bline1 = Convert.ToBoolean(Decryptline1);
					loadedClientInfo.UsesPlayerName = bline1;

					bool bline2 = Convert.ToBoolean(Decryptline2);
					loadedClientInfo.UsesID = bline2;

					loadedClientInfo.Warning = Decryptline3;
					
					bool bline4 = Convert.ToBoolean(Decryptline4);
					loadedClientInfo.LegacyMode = bline4;

					loadedClientInfo.ClientMD5 = Decryptline5;

					loadedClientInfo.ScriptMD5 = Decryptline6;

					loadedClientInfo.Description = Decryptline7;
					
					bool bline9 = Convert.ToBoolean(Decryptline9);
					loadedClientInfo.Fix2007 = bline9;
			
					bool bline10 = Convert.ToBoolean(Decryptline10);
					loadedClientInfo.AlreadyHasSecurity = bline10;

					if (IsVersion2)
					{
						bool bline11 = Convert.ToBoolean(Decryptline11);
						loadedClientInfo.NoGraphicsOptions = bline11;
						loadedClientInfo.CommandLineArgs = Decryptline12;
					}
					else
                    {
						//Agin, fake it.
						loadedClientInfo.NoGraphicsOptions = false;
						loadedClientInfo.CommandLineArgs = Decryptline11;
					}
					
					checkBox1.Checked = loadedClientInfo.UsesPlayerName;
					checkBox2.Checked = loadedClientInfo.UsesID;
					checkBox3.Checked = loadedClientInfo.LegacyMode;
					checkBox6.Checked = loadedClientInfo.Fix2007;
					checkBox7.Checked = loadedClientInfo.AlreadyHasSecurity;
					checkBox5.Checked = loadedClientInfo.NoGraphicsOptions;
					textBox3.Text = loadedClientInfo.ScriptMD5.ToUpper(CultureInfo.InvariantCulture);
					textBox2.Text = loadedClientInfo.ClientMD5.ToUpper(CultureInfo.InvariantCulture);
					textBox1.Text = loadedClientInfo.Description;
					textBox4.Text = loadedClientInfo.CommandLineArgs;
					textBox5.Text = loadedClientInfo.Warning;
            	}
			}
			textBox2.BackColor = System.Drawing.SystemColors.Control;
			textBox3.BackColor = System.Drawing.SystemColors.Control;
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
        	{
            	sfd.Filter = "Novetus Clientinfo files (*.nov)|*.nov";
            	sfd.FilterIndex = 1;
            	sfd.FileName = "clientinfo.nov";
            	sfd.Title = "Save clientinfo.nov";
            	
            	if (sfd.ShowDialog() == DialogResult.OK)
            	{
            		string[] lines = { 
            			SecurityFuncs.Base64Encode(loadedClientInfo.UsesPlayerName.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.UsesID.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.Warning.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.LegacyMode.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.ClientMD5.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.ScriptMD5.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.Description.ToString()),
            			SecurityFuncs.Base64Encode(Locked.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.Fix2007.ToString()),
            			SecurityFuncs.Base64Encode(loadedClientInfo.AlreadyHasSecurity.ToString()),
						SecurityFuncs.Base64Encode(loadedClientInfo.NoGraphicsOptions.ToString()),
						SecurityFuncs.Base64Encode(loadedClientInfo.CommandLineArgs.ToString())
            		};
            		File.WriteAllText(sfd.FileName, SecurityFuncs.Base64Encode(string.Join("|",lines)));
            		SelectedClientInfoPath = Path.GetDirectoryName(sfd.FileName);
            	}     
			}

			label9.Text = "v2";
			textBox2.BackColor = System.Drawing.SystemColors.Control;
			textBox3.BackColor = System.Drawing.SystemColors.Control;
		}
		
		void TextBox4TextChanged(object sender, EventArgs e)
		{
			loadedClientInfo.CommandLineArgs = textBox4.Text;		
		}
		
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			loadedClientInfo.Warning = textBox5.Text;			
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
                sfd.FilterIndex = 1;
                sfd.FileName = "clientinfo.txt";
                sfd.Title = "Save clientinfo.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = {
						loadedClientInfo.UsesPlayerName.ToString(),
						loadedClientInfo.UsesID.ToString(),
						loadedClientInfo.Warning.ToString(),
						loadedClientInfo.LegacyMode.ToString(),
						loadedClientInfo.Description.ToString(),
						loadedClientInfo.Fix2007.ToString(),
						loadedClientInfo.AlreadyHasSecurity.ToString(),
						loadedClientInfo.NoGraphicsOptions.ToString(),
						loadedClientInfo.CommandLineArgs.ToString()
                    };
                    File.WriteAllLines(sfd.FileName, lines);
                }
            }
        }
    }
}
