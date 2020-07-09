#region Usings
using System;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
#endregion

namespace NovetusLauncher
{
    #region Client SDK
    public partial class ClientinfoEditor : Form
	{
        #region Private Variables
        private FileFormat.ClientInfo SelectedClientInfo = new FileFormat.ClientInfo();
		private string SelectedClientInfoPath = "";
		private bool Locked = false;
        #endregion

        #region Constructor
        public ClientinfoEditor()
		{
			InitializeComponent();
		}
        #endregion

        #region Form Events
        void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.UsesPlayerName = checkBox1.Checked;
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.UsesID = checkBox2.Checked;
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.Description = textBox1.Text;
		}
		
		void ClientinfoCreatorLoad(object sender, EventArgs e)
		{
			checkBox4.Visible = GlobalVars.AdminMode;
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.LegacyMode = checkBox3.Checked;
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			textBox2.Text = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
			SelectedClientInfo.ClientMD5 = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
		}
		
		void TextBox3TextChanged(object sender, EventArgs e)
		{
			textBox3.Text = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
			SelectedClientInfo.ScriptMD5 = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
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
        			
    		if (!SelectedClientInfo.LegacyMode)
        	{
        		ClientName = "\\RobloxApp_Client.exe";
        	}
        	else
        	{
        		ClientName = "\\RobloxApp.exe";
        	}
    				
    		string ClientMD5 = File.Exists(SelectedClientInfoPath + ClientName) ? SecurityFuncs.GenerateMD5(SelectedClientInfoPath + ClientName) : "";
        			
        	if (!string.IsNullOrWhiteSpace(ClientMD5))
        	{
        		textBox2.Text = ClientMD5.ToUpper(CultureInfo.InvariantCulture);
				textBox2.BackColor = System.Drawing.Color.Lime;
				SelectedClientInfo.ClientMD5 = textBox2.Text.ToUpper(CultureInfo.InvariantCulture);
        	}
        	else
        	{
        		MessageBox.Show("Cannot load '" + ClientName.Trim('/') + "'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for client", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
					
        	string ClientScriptMD5 = File.Exists(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") ? SecurityFuncs.GenerateMD5(SelectedClientInfoPath + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
        			
			if (!string.IsNullOrWhiteSpace(ClientScriptMD5))
        	{
        		textBox3.Text = ClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
				textBox3.BackColor = System.Drawing.Color.Lime;
				SelectedClientInfo.ScriptMD5 = textBox3.Text.ToUpper(CultureInfo.InvariantCulture);
			}
			else
        	{
        		MessageBox.Show("Cannot load '" + GlobalPaths.ScriptName + ".lua'. Please make sure you selected the directory","Novetus Launcher - Error while generating MD5 for script", MessageBoxButtons.OK, MessageBoxIcon.Error);
        	}
			
			MessageBox.Show("MD5s generated.","Novetus Launcher - Novetus Client SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			Locked = true;
		}
		
		void CheckBox6CheckedChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.Fix2007 = checkBox6.Checked;	
		}
		
		void CheckBox7CheckedChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.AlreadyHasSecurity = checkBox7.Checked;
		}

		void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.NoGraphicsOptions = checkBox5.Checked;
		}

		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			label9.Text = "Not Loaded";
			SDKFuncs.NewClientinfo(SelectedClientInfo, Locked);
			SelectedClientInfoPath = "";
			checkBox1.Checked = SelectedClientInfo.UsesPlayerName;
			checkBox2.Checked = SelectedClientInfo.UsesID;
			checkBox3.Checked = SelectedClientInfo.LegacyMode;
			checkBox4.Checked = Locked;
			checkBox6.Checked = SelectedClientInfo.Fix2007;
			checkBox7.Checked = SelectedClientInfo.AlreadyHasSecurity;
			checkBox5.Checked = SelectedClientInfo.NoGraphicsOptions;
			textBox3.Text = SelectedClientInfo.ScriptMD5.ToUpper(CultureInfo.InvariantCulture);
			textBox2.Text = SelectedClientInfo.ClientMD5.ToUpper(CultureInfo.InvariantCulture);
			textBox1.Text = SelectedClientInfo.Description;
			textBox4.Text = SelectedClientInfo.CommandLineArgs;
			textBox5.Text = SelectedClientInfo.Warning;
			textBox2.BackColor = System.Drawing.SystemColors.Control;
			textBox3.BackColor = System.Drawing.SystemColors.Control;
		}
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			string clientinfopath = SDKFuncs.LoadClientinfoAndGetPath(SelectedClientInfo, Locked, label9.Text, checkBox4.Checked);

			if (!string.IsNullOrWhiteSpace(clientinfopath))
			{
				SelectedClientInfoPath = clientinfopath;

				checkBox1.Checked = SelectedClientInfo.UsesPlayerName;
				checkBox2.Checked = SelectedClientInfo.UsesID;
				checkBox3.Checked = SelectedClientInfo.LegacyMode;
				checkBox6.Checked = SelectedClientInfo.Fix2007;
				checkBox7.Checked = SelectedClientInfo.AlreadyHasSecurity;
				checkBox5.Checked = SelectedClientInfo.NoGraphicsOptions;
				textBox3.Text = SelectedClientInfo.ScriptMD5.ToUpper(CultureInfo.InvariantCulture);
				textBox2.Text = SelectedClientInfo.ClientMD5.ToUpper(CultureInfo.InvariantCulture);
				textBox1.Text = SelectedClientInfo.Description;
				textBox4.Text = SelectedClientInfo.CommandLineArgs;
				textBox5.Text = SelectedClientInfo.Warning;
			}

			textBox2.BackColor = System.Drawing.SystemColors.Control;
			textBox3.BackColor = System.Drawing.SystemColors.Control;
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			string clientinfopath = SDKFuncs.SaveClientinfoAndGetPath(SelectedClientInfo, Locked);

			if (!string.IsNullOrWhiteSpace(clientinfopath))
			{
				SelectedClientInfoPath = clientinfopath;
			}

			label9.Text = "v2";
			textBox2.BackColor = System.Drawing.SystemColors.Control;
			textBox3.BackColor = System.Drawing.SystemColors.Control;
		}

		private void saveAsTextFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SDKFuncs.SaveClientinfoAndGetPath(SelectedClientInfo, Locked, true);
		}

		void TextBox4TextChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.CommandLineArgs = textBox4.Text;		
		}
		
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			SelectedClientInfo.Warning = textBox5.Text;			
		}

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientScriptDocumentation csd = new ClientScriptDocumentation();
            csd.Show();
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

        private void variableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem senderitem = (ToolStripMenuItem)sender;
            AddClientinfoText(senderitem.Text);
        }
		#endregion

		#region Functions
		private void AddClientinfoText(string text)
		{
			textBox4.Paste(text);
		}
		#endregion
	}
    #endregion
}
