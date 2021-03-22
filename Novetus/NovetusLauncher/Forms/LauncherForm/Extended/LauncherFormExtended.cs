#region Usings
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;
using Mono.Nat;
using System.Globalization;
using System.Linq;
#endregion

namespace NovetusLauncher
{
	#region LauncherForm - Extended
	public partial class LauncherFormExtended : Form
	{
		LauncherFormShared launcherForm = null;

		#region Constructor
		public LauncherFormExtended()
		{
			_fieldsTreeCache = new TreeView();
            InitializeComponent();

			//*vomits*
			launcherForm = new LauncherFormShared();
			launcherForm.Parent = this;
			launcherForm.FormStyle = Settings.UIOptions.Style.Extended;
			launcherForm.ConsoleBox = richTextBox1;
			launcherForm.Tabs = tabControl1;
			launcherForm.MapDescBox = textBox4;
			launcherForm.ServerInfo = textBox3;
			launcherForm.Tree = treeView1;
			launcherForm._TreeCache = _fieldsTreeCache;
			launcherForm.TabPageHost = "tabPage2";
			launcherForm.TabPageMaps = "tabPage4";
			launcherForm.TabPageClients = "tabPage3";
			launcherForm.TabPageSaved = "tabPage6";
			launcherForm.ServerBox = listBox3;
			launcherForm.PortBox = listBox4;
			launcherForm.ClientBox = listBox2;
			launcherForm.SplashLabel = label12;
			launcherForm.SearchBar = SearchBar;
			launcherForm.StyleSelectorBox = comboBox3;
			launcherForm.ChangelogBox = richTextBox2;
			launcherForm.ReadmeBox = richTextBox3;

			Size = new Size(745, 377);
			panel2.Size = new Size(646, 272);
		}
        #endregion

		#region Form Events
		async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			await launcherForm.ChangeTabs();
		}

		void Button1Click(object sender, EventArgs e)
		{
			launcherForm.StartGame(ScriptType.Client);
		}

        void Button2Click(object sender, EventArgs e)
		{
			launcherForm.StartGame(ScriptType.Server);
		}

        void Button3Click(object sender, EventArgs e)
		{
			launcherForm.StartGame(ScriptType.Studio);
		}

        void Button18Click(object sender, EventArgs e)
        {
			launcherForm.StartGame(ScriptType.Server, true);
        }

        void Button19Click(object sender, EventArgs e)
        {
            launcherForm.StartGame(ScriptType.Solo);
        }

        private void button35_Click(object sender, EventArgs e)
        {
			launcherForm.StartGame(ScriptType.Studio, false, true);
		}

        void MainFormLoad(object sender, EventArgs e)
		{
			launcherForm.InitForm();
		}

        void MainFormClose(object sender, CancelEventArgs e)
        {
			launcherForm.CloseEvent();
        }
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.IP = textBox1.Text;
			checkBox3.Enabled = false;
			GlobalVars.LocalPlayMode = false;
			label37.Text = GlobalVars.IP;
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.CloseOnLaunch = checkBox1.Checked;
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			launcherForm.GeneratePlayerID();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			launcherForm.WriteConfigValues(true);
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.PlayerName = textBox2.Text;
		}
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			string ourselectedclient = GlobalVars.UserConfiguration.SelectedClient;
			GlobalVars.UserConfiguration.SelectedClient = listBox2.SelectedItem.ToString();
			if (!ourselectedclient.Equals(GlobalVars.UserConfiguration.SelectedClient))
			{
				launcherForm.ReadClientValues(true);
			}
			else
			{
				launcherForm.ReadClientValues();
			}
			GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");

			FormCollection fc = Application.OpenForms;

			foreach (Form frm in fc)
			{
				//iterate through
				if (frm.Name == "CustomGraphicsOptions")
				{
					frm.Close();
					break;
				}
			}
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.LocalPlayMode = checkBox3.Checked;
		}
		
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			int parsedValue;
			if (int.TryParse(textBox5.Text, out parsedValue))
			{
				if (textBox5.Text.Equals(""))
				{
					GlobalVars.UserConfiguration.UserID = 0;
				}
				else
				{
					GlobalVars.UserConfiguration.UserID = Convert.ToInt32(textBox5.Text);
				}
			}
			else
			{
				GlobalVars.UserConfiguration.UserID = 0;
			}
		}
		
		void Button8Click(object sender, EventArgs e)
		{
			CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
			ccustom.Show();
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			launcherForm.ResetConfigValues();
			MessageBox.Show("Config Reset!");
		}
		
		void ListBox3SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalVars.IP = listBox3.SelectedItem.ToString();
			textBox1.Text = GlobalVars.IP;
			checkBox3.Enabled = false;
			GlobalVars.LocalPlayMode = false;
			label37.Text = GlobalVars.IP;
		}
		
		void ListBox4SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(listBox4.SelectedItem.ToString());
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalPaths.ConfigDir + "\\servers.txt", GlobalVars.IP + Environment.NewLine);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalPaths.ConfigDir + "\\ports.txt", GlobalVars.UserConfiguration.RobloxPort + Environment.NewLine);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			if (listBox3.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox3.SelectedItem.ToString() }, GlobalPaths.ConfigDir + "\\servers.txt", GlobalPaths.ConfigDir + "\\servers.tmp");
				listBox3.Items.Clear();
				string[] lines_server = File.ReadAllLines(GlobalPaths.ConfigDir + "\\servers.txt");
				listBox3.Items.AddRange(lines_server);
			}
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			if (listBox4.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox4.SelectedItem.ToString() }, GlobalPaths.ConfigDir + "\\ports.txt", GlobalPaths.ConfigDir + "\\ports.tmp");
				listBox4.Items.Clear();
				string[] lines_ports = File.ReadAllLines(GlobalPaths.ConfigDir + "\\ports.txt");
				listBox4.Items.AddRange(lines_ports);
			}
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			File.Create(GlobalPaths.ConfigDir + "\\servers.txt").Dispose();
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines(GlobalPaths.ConfigDir + "\\servers.txt");
			listBox3.Items.AddRange(lines_server);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			File.Create(GlobalPaths.ConfigDir + "\\ports.txt").Dispose();
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines(GlobalPaths.ConfigDir + "\\ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalPaths.ConfigDir + "\\servers.txt", GlobalVars.IP + Environment.NewLine);
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines(GlobalPaths.ConfigDir + "\\servers.txt");
			listBox3.Items.AddRange(lines_server);			
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalPaths.ConfigDir + "\\ports.txt", GlobalVars.UserConfiguration.RobloxPort + Environment.NewLine);
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines(GlobalPaths.ConfigDir + "\\ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
			launcherForm.ProcessConsole(e);
        }
		
		void Button21Click(object sender, EventArgs e)
		{
			if (SecurityFuncs.IsElevated)
			{
				try
      			{
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = GlobalPaths.ClientDir + @"\\" + GlobalVars.ProgramInformation.RegisterClient1 + @"\\RobloxApp_studio.exe";
                    startInfo.Arguments = "/regserver";
                    startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();

                    Process process2 = new Process();
                    ProcessStartInfo startInfo2 = new ProcessStartInfo();
                    startInfo2.FileName = GlobalPaths.ClientDir + @"\\" + GlobalVars.ProgramInformation.RegisterClient2 + @"\\RobloxApp_studio.exe";
                    startInfo2.Arguments = "/regserver";
                    startInfo2.Verb = "runas";
                    process2.StartInfo = startInfo2;
                    process2.Start();

                    GlobalFuncs.ConsolePrint("UserAgent Library successfully installed and registered!", 3, richTextBox1);
					MessageBox.Show("UserAgent Library successfully installed and registered!", "Novetus - Register UserAgent Library", MessageBoxButtons.OK, MessageBoxIcon.Information);
      			}
      			catch (Exception ex)
                {
        			GlobalFuncs.ConsolePrint("ERROR - Failed to register. (" + ex.Message + ")", 2, richTextBox1);
					MessageBox.Show("Failed to register. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      			}
			}
			else
			{
				GlobalFuncs.ConsolePrint("ERROR - Failed to register. (Did not run as Administrator)", 2, richTextBox1);
				MessageBox.Show("Failed to register. (Error: Did not run as Administrator)","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void NumericUpDown1ValueChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(numericUpDown1.Value);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
			label38.Text = GlobalVars.UserConfiguration.RobloxPort.ToString();
		}
		
		void NumericUpDown2ValueChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(numericUpDown2.Value);
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
			label38.Text = GlobalVars.UserConfiguration.RobloxPort.ToString();
		}
		
		void NumericUpDown3ValueChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(numericUpDown3.Value);
		}
		
		void Button7Click(object sender, EventArgs e)
		{
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			GlobalVars.UserConfiguration.RobloxPort = GlobalVars.DefaultRobloxPort;
		}
		
		void Button23Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalPaths.ConfigDir + "\\ports.txt", GlobalVars.UserConfiguration.RobloxPort + Environment.NewLine);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			GlobalVars.UserConfiguration.RobloxPort = GlobalVars.DefaultRobloxPort;
		}
		
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode.Nodes.Count == 0)
			{
				GlobalVars.UserConfiguration.Map = treeView1.SelectedNode.Text.ToString();
                GlobalVars.UserConfiguration.MapPathSnip = treeView1.SelectedNode.FullPath.ToString().Replace(@"\", @"\\");
                GlobalVars.UserConfiguration.MapPath = GlobalPaths.BasePath + @"\\" + GlobalVars.UserConfiguration.MapPathSnip;
				label28.Text = GlobalVars.UserConfiguration.Map;

                if (File.Exists(GlobalPaths.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalPaths.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = treeView1.SelectedNode.Text.ToString();
                }
            }
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", GlobalPaths.MapsDir.Replace(@"\\",@"\"));
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.UPnP = checkBox4.Checked;
		}

		void CheckBox4Click(object sender, EventArgs e)
		{
			switch (checkBox4.Checked)
			{
				case false:
					MessageBox.Show("Novetus will now restart.", "Novetus - UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
				default:
					MessageBox.Show("Novetus will now restart." + Environment.NewLine + "Make sure to check if your router has UPnP functionality enabled. Please note that some routers may not support UPnP, and some ISPs will block the UPnP protocol. This may not work for all users.", "Novetus - UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
			}

			launcherForm.WriteConfigValues();
			Application.Restart();
		}

		void Button24Click(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			_fieldsTreeCache.Nodes.Clear();
        	string mapdir = GlobalPaths.MapsDir;
			string[] fileexts = new string[] { ".rbxl", ".rbxlx" };
			TreeNodeHelper.ListDirectory(treeView1, mapdir, fileexts);
			TreeNodeHelper.CopyNodes(treeView1.Nodes,_fieldsTreeCache.Nodes);
			treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, treeView1.Nodes);
			treeView1.Focus();
            if (File.Exists(GlobalPaths.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
            {
                textBox4.Text = File.ReadAllText(GlobalPaths.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
            }
            else
            {
                textBox4.Text = treeView1.SelectedNode.Text.ToString();
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            AddonLoader addon = new AddonLoader();
            addon.setFileListDisplay(10);
            try
            {
                addon.LoadAddon();
                if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
                {
                    GlobalFuncs.ConsolePrint("AddonLoader - " + addon.getInstallOutcome(), 3, richTextBox1);
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
                {
                    GlobalFuncs.ConsolePrint("AddonLoader - " + addon.getInstallOutcome(), 2, richTextBox1);
                }
            }

            if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
            {
                MessageBox.Show(addon.getInstallOutcome());
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(GlobalPaths.AssetCacheDir))
            {
                Directory.Delete(GlobalPaths.AssetCacheDir, true);
                GlobalFuncs.ConsolePrint("Asset cache cleared!", 3, richTextBox1);
                MessageBox.Show("Asset cache cleared!");
            }
            else
            {
                MessageBox.Show("There is no asset cache to clear.");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
			GlobalVars.UserConfiguration.DiscordPresence = checkBox2.Checked;
		}

		void CheckBox2Click(object sender, EventArgs e)
		{
			switch (checkBox2.Checked)
			{
				case false:
					MessageBox.Show("Novetus will now restart.", "Novetus - Discord Rich Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
				default:
					MessageBox.Show("Novetus will now restart." + Environment.NewLine + "Make sure the Discord app is open so this change can take effect.", "Novetus - Discord Rich Presence", MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
			}

			launcherForm.WriteConfigValues();
			Application.Restart();
		}

		private void button27_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage7;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage8;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void button34_Click(object sender, EventArgs e)
        {
			launcherForm.LoadLauncher();
        }

        private void label8_Click(object sender, EventArgs e)
        {
			launcherForm.EasterEggLogic();
		}

		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.ReShade = checkBox5.Checked;
		}

		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.ReShadeFPSDisplay = checkBox6.Checked;
		}

		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.ReShadePerformanceMode = checkBox7.Checked;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			switch (comboBox1.SelectedIndex)
			{
				case 1:
					GlobalVars.UserConfiguration.GraphicsMode = Settings.GraphicsOptions.Mode.OpenGL;
					break;
				case 2:
					GlobalVars.UserConfiguration.GraphicsMode = Settings.GraphicsOptions.Mode.DirectX;
					break;
				default:
					GlobalVars.UserConfiguration.GraphicsMode = Settings.GraphicsOptions.Mode.Automatic;
					break;
			}

			GlobalFuncs.ReadClientValues(richTextBox1);
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboBox2.SelectedIndex)
			{
				case 1:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.VeryLow;
					break;
				case 2:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.Low;
					break;
				case 3:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.Medium;
					break;
				case 4:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.High;
					break;
				case 5:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.Ultra;
					break;
				case 6:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.Custom;
					break;
				default:
					GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.Automatic;
					break;
			}

			GlobalFuncs.ReadClientValues(richTextBox1);

			if (comboBox2.SelectedIndex != 6)
			{
				//https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp

				FormCollection fc = Application.OpenForms;

				foreach (Form frm in fc)
				{
					//iterate through
					if (frm.Name == "CustomGraphicsOptions")
					{
						frm.Close();
						break;
					}
				}
			}
		}

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			launcherForm.SwitchStyles();
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			launcherForm.SearchMaps();
		}

		private void button36_Click(object sender, EventArgs e)
		{
			if (GlobalVars.UserConfiguration.QualityLevel == Settings.GraphicsOptions.Level.Custom)
			{
				CustomGraphicsOptions opt = new CustomGraphicsOptions();
				opt.Show();
			}
			else
			{
				MessageBox.Show("You do not have the 'Custom' option selected. Please select it before continuing.");
			}
		}

		private void checkBox8_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.WebServer = checkBox8.Checked;
		}

		void CheckBox8Click(object sender, EventArgs e)
		{
			switch (checkBox8.Checked)
			{
				case false:
					MessageBox.Show("Novetus will now restart.", "Novetus - UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
				default:
					MessageBox.Show("Novetus will now restart." + Environment.NewLine + "Make sure to check if your router has UPnP functionality enabled. Please note that some routers may not support UPnP, and some ISPs will block the UPnP protocol. This may not work for all users.", "Novetus - UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
			}

			launcherForm.WriteConfigValues();
			Application.Restart();
		}

		private void button37_Click(object sender, EventArgs e)
		{
			ServerBrowser browser = new ServerBrowser();
			browser.Show();
		}

		private void checkBox9_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.ShowServerNotifications = checkBox9.Checked;
		}
		#endregion
    }
    #endregion
}
