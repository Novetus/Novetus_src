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
		#region Constructor
		public LauncherFormExtended()
		{
			_fieldsTreeCache = new TreeView();
            InitializeComponent();
			InitExtendedForm();

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
			launcherForm.InstallRegServer();
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
			launcherForm.SelectMap();
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
			launcherForm.RestartLauncherAfterSetting(checkBox4);
		}

		void Button24Click(object sender, EventArgs e)
		{
			launcherForm.RefreshMaps();
        }

        private void button25_Click(object sender, EventArgs e)
        {
			launcherForm.InstallAddon();
		}

        private void button26_Click(object sender, EventArgs e)
        {
			launcherForm.ClearAssetCache();
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
			launcherForm.RestartLauncherAfterSetting(checkBox8, true);
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
