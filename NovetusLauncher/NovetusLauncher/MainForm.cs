/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 10/7/2016
 * Time: 3:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Reflection;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		DiscordRpc.EventHandlers handlers;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		//TODO: add upnp shit here

		public void ReadyCallback()
        {
            ConsolePrint("Discord RPC: Ready", 3);
        }

        public void DisconnectedCallback(int errorCode, string message)
        {
            ConsolePrint("Discord RPC: Disconnected. Reason - " + errorCode + ": " + message, 2);
        }

        public void ErrorCallback(int errorCode, string message)
        {
            ConsolePrint("Discord RPC: Error. Reason - " + errorCode + ": " + message, 2);
        }

        public void JoinCallback(string secret)
        {
        }

        public void SpectateCallback(string secret)
        {
        }

        public void RequestCallback(DiscordRpc.JoinRequest request)
        {
        }
        
        void StartDiscord()
        {
        	handlers = new DiscordRpc.EventHandlers();
            handlers.readyCallback = ReadyCallback;
            handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;
            handlers.joinCallback += JoinCallback;
            handlers.spectateCallback += SpectateCallback;
            handlers.requestCallback += RequestCallback;
            DiscordRpc.Initialize(GlobalVars.appid, ref handlers, true, "");
			
            GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
            GlobalVars.presence.state = "In Launcher";
            GlobalVars.presence.details = "Selected " + GlobalVars.SelectedClient;
            GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In Launcher";
            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
        }		
		
		void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
     		if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
     		{
        		string mapdir = GlobalVars.MapsDir;
				DirectoryInfo dinfo = new DirectoryInfo(mapdir);
				FileInfo[] Files = dinfo.GetFiles("*.rbxl");
				foreach( FileInfo file in Files )
				{
   					listBox1.Items.Add(file.Name);
				}
				listBox1.SelectedItem = GlobalVars.Map;
				listBox2.Items.Clear();
				listBox3.Items.Clear();
     			listBox4.Items.Clear();
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])//your specific tabname
     		{
        		string clientdir = GlobalVars.ClientDir;
				DirectoryInfo dinfo = new DirectoryInfo(clientdir);
				DirectoryInfo[] Dirs = dinfo.GetDirectories();
				foreach( DirectoryInfo dir in Dirs )
				{
   					listBox2.Items.Add(dir.Name);
				}
				listBox2.SelectedItem = GlobalVars.SelectedClient;
				listBox1.Items.Clear();
				listBox3.Items.Clear();
     			listBox4.Items.Clear();
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])//your specific tabname
     		{
     			string[] lines_server = File.ReadAllLines("servers.txt");
				string[] lines_ports = File.ReadAllLines("ports.txt");
				listBox3.Items.AddRange(lines_server);
				listBox4.Items.AddRange(lines_ports);
     			listBox1.Items.Clear();
     			listBox2.Items.Clear();
     		}
     		else
     		{
     			listBox1.Items.Clear();
     			listBox2.Items.Clear();
     			listBox3.Items.Clear();
     			listBox4.Items.Clear();
     		}
		}
		void Button1Click(object sender, EventArgs e)
		{
			if (GlobalVars.LocalPlayMode == true)
			{
				GeneratePlayerID();
			}
			
			WriteConfigValues();
			StartClient();
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Close();
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			StartServer(false);
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Close();
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("If you want to test out your place, you will have to save your place in Novetus's map folder, then launch your place in Play Solo.","Novetus - Launch ROBLOX Studio", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
			if (result == DialogResult.Cancel)
				return;
			
			WriteConfigValues();
			StartStudio();
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Close();
			}
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			string[] lines = File.ReadAllLines("info.txt"); //File is in System.IO
			string version = lines[0];
			string[] defaultclient = File.ReadAllLines("info.txt");
    		string defcl = defaultclient[1];
    		GlobalVars.DefaultClient = defcl;
    		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
    		ConsolePrint("Novetus version " + version + " loaded. Initializing config.", 4);
    		if (File.Exists("changelog.txt"))
			{
    			richTextBox2.Text = File.ReadAllText("changelog.txt");
    		}
    		else
    		{
    			ConsolePrint("ERROR 4 - changelog.txt not found.", 2);
    		}
			if (!File.Exists("config.txt"))
			{
				ConsolePrint("WARNING 1 - config.txt not found. Creating one with default values.", 5);
				WriteConfigValues();
			}
			if (!File.Exists("servers.txt"))
			{
				ConsolePrint("WARNING 2 - servers.txt not found. Creating empty file.", 5);
				File.Create("servers.txt").Dispose();
			}
			if (!File.Exists("ports.txt"))
			{
				ConsolePrint("WARNING 3 - ports.txt not found. Creating empty file.", 5);
				File.Create("ports.txt").Dispose();
			}
			label5.Text = GlobalVars.BasePath;
			label8.Text = Application.ProductVersion;
			GlobalVars.IP = "localhost";
    		GlobalVars.Map = "Baseplate.rbxl";
    		GlobalVars.important = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
    		label11.Text = version;
    		GlobalVars.Version = version;
    		ReadConfigValues();
    		StartDiscord();
		}
		
		void MainFormClose(object sender, CancelEventArgs e)
        {
			WriteConfigValues();
            DiscordRpc.Shutdown();
        }
		
		void ReadConfigValues()
		{
			LauncherFuncs.ReadConfigValues(GlobalVars.BasePath + "\\config.txt");
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				checkBox1.Checked = true;
			}
			else if (GlobalVars.CloseOnLaunch == false)
			{
				checkBox1.Checked = false;
			}
			
			if (GlobalVars.UserID == 0)
			{
				GeneratePlayerID();
				WriteConfigValues();
			}
			else
			{
				textBox5.Text = GlobalVars.UserID.ToString();
			}
			
			if (GlobalVars.PlayerLimit == 0)
			{
				//We need at least a limit of 12 players.
				GlobalVars.PlayerLimit = 12;
				numericUpDown3.Value = Convert.ToDecimal(GlobalVars.PlayerLimit);
			}
			else
			{
				numericUpDown3.Value = Convert.ToDecimal(GlobalVars.PlayerLimit);
			}
			
			textBox2.Text = GlobalVars.PlayerName;
			label26.Text = GlobalVars.SelectedClient;
			label28.Text = GlobalVars.Map;
			listBox1.SelectedItem = GlobalVars.Map;
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
			label37.Text = GlobalVars.IP;
			label38.Text = GlobalVars.RobloxPort.ToString();
			checkBox2.Checked = GlobalVars.DisableTeapotTurret;
			ConsolePrint("Config loaded.", 3);
			ReadClientValues(GlobalVars.SelectedClient);
		}
		
		void WriteConfigValues()
		{
			LauncherFuncs.WriteConfigValues(GlobalVars.BasePath + "\\config.txt");
			ConsolePrint("Config Saved.", 3);
		}
		
		void ReadClientValues(string ClientName)
		{
			string clientpath = GlobalVars.ClientDir + @"\\" + ClientName + @"\\clientinfo.txt";
			
			if (!File.Exists(clientpath))
			{
				ConsolePrint("ERROR 1 - No clientinfo.txt detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
				MessageBox.Show("No clientinfo.txt detected with the client you chose. The client either cannot be loaded, or it is not available.","Novetus - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
				GlobalVars.SelectedClient = GlobalVars.DefaultClient;
			}
			
			LauncherFuncs.ReadClientValues(clientpath);
			
			if (GlobalVars.UsesPlayerName == true)
			{
				textBox2.Enabled = true;
			}
			else if (GlobalVars.UsesPlayerName == false)
			{
				textBox2.Enabled = false;
			}
			
			if (GlobalVars.UsesID == true)
			{
				textBox5.Enabled = true;
				button4.Enabled = true;
				if (GlobalVars.IP.Equals("localhost"))
				{
					checkBox3.Enabled = true;
				}
			}
			else if (GlobalVars.UsesID == false)
			{
				textBox5.Enabled = false;
				button4.Enabled = false;
				checkBox3.Enabled = false;
				GlobalVars.LocalPlayMode = false;
			}
			
			if (GlobalVars.LoadsAssetsOnline == false)
			{
				label30.Visible = false;
			}
			else if (GlobalVars.LoadsAssetsOnline == true)
			{
				label30.Visible = true;
			}
			
			textBox6.Text = GlobalVars.SelectedClientDesc;
			label26.Text = GlobalVars.SelectedClient;
			ConsolePrint("Client '" + GlobalVars.SelectedClient + "' successfully loaded.", 3);
		}
		
		void GeneratePlayerID()
		{
			LauncherFuncs.GeneratePlayerID();
			textBox5.Text = Convert.ToString(GlobalVars.UserID);
		}
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.IP = textBox1.Text;
			checkBox3.Enabled = false;
			GlobalVars.LocalPlayMode = false;
			label37.Text = GlobalVars.IP;
		}
		
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalVars.Map = listBox1.SelectedItem.ToString();
			label28.Text = GlobalVars.Map;
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				GlobalVars.CloseOnLaunch = true;
			}
			else if (checkBox1.Checked == false)
			{
				GlobalVars.CloseOnLaunch = false;
			}
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			GeneratePlayerID();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			MessageBox.Show("Config Saved!");
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			GlobalVars.PlayerName = textBox2.Text;
		}
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalVars.SelectedClient = listBox2.SelectedItem.ToString();
			ReadClientValues(GlobalVars.SelectedClient);
		}
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox3.Checked == true)
			{
				GlobalVars.LocalPlayMode = true;
			}
			else if (checkBox3.Checked == false)
			{
				GlobalVars.LocalPlayMode = false;
			}
		}
		
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			int parsedValue;
			if (int.TryParse(textBox5.Text, out parsedValue))
			{
				if (textBox5.Text.Equals(""))
				{
					GlobalVars.UserID = 0;
				}
				else
				{
					GlobalVars.UserID = Convert.ToInt32(textBox5.Text);
				}
			}
			else
			{
				GlobalVars.UserID = 0;
			}
		}
		
		void Button8Click(object sender, EventArgs e)
		{
			CharacterCustomization ccustom = new CharacterCustomization();
			ccustom.Show();
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			ResetConfigValues();
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
			GlobalVars.RobloxPort = Convert.ToInt32(listBox4.SelectedItem.ToString());
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
		}
		
		void Button10Click(object sender, EventArgs e)
		{
			File.AppendAllText("servers.txt", GlobalVars.IP + Environment.NewLine);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			File.AppendAllText("ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			if (listBox3.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox3.SelectedItem.ToString() }, "servers.txt", "servers.tmp");
				listBox3.Items.Clear();
				string[] lines_server = File.ReadAllLines("servers.txt");
				listBox3.Items.AddRange(lines_server);
			}
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			if (listBox4.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox4.SelectedItem.ToString() }, "ports.txt", "ports.tmp");
				listBox4.Items.Clear();
				string[] lines_ports = File.ReadAllLines("ports.txt");
				listBox4.Items.AddRange(lines_ports);
			}
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			File.Create("servers.txt").Dispose();
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines("servers.txt");
			listBox3.Items.AddRange(lines_server);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			File.Create("ports.txt").Dispose();
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines("ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			File.AppendAllText("servers.txt", GlobalVars.IP + Environment.NewLine);
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines("servers.txt");
			listBox3.Items.AddRange(lines_server);			
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			File.AppendAllText("ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines("ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void Button18Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			StartServer(true);
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Close();
			}						
		}
		
		void Button19Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			StartSolo();
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Close();
			}
		}
		
		void Button20Click(object sender, EventArgs e)
		{
			ServerInfo infopanel = new ServerInfo();
			infopanel.Show();
		}
		
		void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
			//Command proxy
            
            int totalLines = richTextBox1.Lines.Length;
            if (totalLines > 0)
            {
				string lastLine = richTextBox1.Lines[totalLines - 1];
            
            	if (e.KeyCode == Keys.Enter)
            	{
            		richTextBox1.AppendText(Environment.NewLine);
            		ConsoleProcessCommands(lastLine);
            		e.Handled = true;
            	}
            }
            
            if ( e.Modifiers == Keys.Control )
			{
				switch(e.KeyCode)
				{
				case Keys.C:
				case Keys.X:
				case Keys.V:
				case Keys.Z:
					e.Handled = true;
					break;
				default:
					break;
				}
			}
        }
		
		void ResetConfigValues()
		{
			LauncherFuncs.ResetConfigValues();
			WriteConfigValues();
			ReadConfigValues();
		}
		
		void ConsolePrint(string text, int type)
		{
			richTextBox1.AppendText("[" + DateTime.Now.ToShortTimeString() + "]", Color.White);
			richTextBox1.AppendText(" - ", Color.White);
			if (type == 1)
			{
				richTextBox1.AppendText(text, Color.White);
			}
			else if (type == 2)
			{
				richTextBox1.AppendText(text, Color.Red);
			}
			else if (type == 3)
			{
				richTextBox1.AppendText(text, Color.Lime);
			}
			else if (type == 4)
			{
				richTextBox1.AppendText(text, Color.Aqua);
			}
			else if (type == 5)
			{
				richTextBox1.AppendText(text, Color.Yellow);
			}
			
			richTextBox1.AppendText(Environment.NewLine);
		}
		
		void StartClient()
		{
			string luafile = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
			
			string rbxexe = "";
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_client.exe";
			}
			string quote = "\"";
			string args = "";
			string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
			if (!GlobalVars.FixScriptMapMode)
			{
				if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
				{
					args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false)
				{
					args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
				}
			}
			else
			{
				ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Client);
				args = "-script " + quote + luafile + quote;
			}
			try
			{
				ConsolePrint("Client Loaded.", 4);
				if (!GlobalVars.AlreadyHasSecurity)
				{
					if (SecurityFuncs.checkClientMD5(GlobalVars.SelectedClient) == true)
					{
						if (SecurityFuncs.checkScriptMD5(GlobalVars.SelectedClient) == true)
						{
							Process client = new Process();
							client.StartInfo.FileName = rbxexe;
							client.StartInfo.Arguments = args;
							client.EnableRaisingEvents = true;
							ReadClientValues(GlobalVars.SelectedClient);
							client.Exited += new EventHandler(ClientExited);
							client.Start();
							GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
            				GlobalVars.presence.details = "";
            				GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Game";
            				GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            				GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In " + GlobalVars.SelectedClient + " Game";
            				DiscordRpc.UpdatePresence(ref GlobalVars.presence);
						}
					}
				}
				else
				{
					Process client = new Process();
					client.StartInfo.FileName = rbxexe;
					client.StartInfo.Arguments = args;
					client.EnableRaisingEvents = true;
					ReadClientValues(GlobalVars.SelectedClient);
					client.Exited += new EventHandler(ClientExited);
					client.Start();
					GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
            		GlobalVars.presence.details = "";
            		GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Game";
            		GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            		GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In " + GlobalVars.SelectedClient + " Game";
            		DiscordRpc.UpdatePresence(ref GlobalVars.presence);
				}
			}
			catch (Exception ex)
			{
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				DialogResult result2 = MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void ClientExited(object sender, EventArgs e)
		{
			GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
            GlobalVars.presence.state = "In Launcher";
            GlobalVars.presence.details = "Selected " + GlobalVars.SelectedClient;
            GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In Launcher";
            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
		}
		
		void StartSolo()
		{
			string luafile = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
			string mapfile = GlobalVars.MapsDir + @"\\" + GlobalVars.Map;
			string rbxexe = "";
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_studio.exe";
			}
			string quote = "\"";
			string args = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ");" + quote;
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'Player','" + GlobalVars.loadtext + ");" + quote;
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); _G.CSSolo(0,'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ");" + quote;
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false )
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); _G.CSSolo(0,'Player','" + GlobalVars.loadtext + ");" + quote;
				}
			}
			else
			{
				ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Solo);
				args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
			}
			try
			{
				ConsolePrint("Play Solo Loaded.", 4);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
				ReadClientValues(GlobalVars.SelectedClient);
				client.Exited += new EventHandler(StudioExited);
				client.Start();
				GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
				GlobalVars.presence.details = GlobalVars.Map;
            	GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Solo Game";
            	GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            	GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In " + GlobalVars.SelectedClient + " Solo Game";
            	DiscordRpc.UpdatePresence(ref GlobalVars.presence);
			}
			catch (Exception ex)
			{
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				DialogResult result2 = MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StartServer(bool no3d)
		{
			string luafile = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
			string mapfile = GlobalVars.MapsDir + @"\\" + GlobalVars.Map;
			string rbxexe = "";
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_server.exe";
			}
			string quote = "\"";
			string args = "";
			string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
			if (!GlobalVars.FixScriptMapMode)
			{
				args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); _G.CSServer(" + GlobalVars.RobloxPort + "," + GlobalVars.PlayerLimit + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "'," + GlobalVars.DisableTeapotTurret.ToString().ToLower() + "); " + quote + (no3d ? " -no3d" : "");
			}
			else
			{
				ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Server);
				args = "-script " + quote + luafile + quote + (no3d ? " -no3d" : "") + " " + quote + mapfile + quote;
			}
			try
			{
				//when we add upnp, change this
				ConsolePrint("Server Loaded.", 4);
				Process.Start(rbxexe, args);
			}
			catch (Exception ex)
			{
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				DialogResult result2 = MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StartStudio()
		{
			string luafile = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
			string mapfile = GlobalVars.MapsDir + @"\\" + GlobalVars.Map;
			string rbxexe = "";
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_studio.exe";
			}
			string quote = "\"";
			string args = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				args = quote + mapfile + "\" -script \"dofile('" + luafile + "');" + quote;
			}
			else
			{
				ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Studio);
				args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
			}
			try
			{
				ConsolePrint("Studio Loaded.", 4);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
				ReadClientValues(GlobalVars.SelectedClient);
				client.Exited += new EventHandler(StudioExited);
				client.Start();
				GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
				GlobalVars.presence.details = GlobalVars.Map;
            	GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Studio";
            	GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            	GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In " + GlobalVars.SelectedClient + " Studio";
            	DiscordRpc.UpdatePresence(ref GlobalVars.presence);
			}
			catch (Exception ex)
			{
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				DialogResult result2 = MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StudioExited(object sender, EventArgs e)
		{
			GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
            GlobalVars.presence.state = "In Launcher";
            GlobalVars.presence.details = "Selected " + GlobalVars.SelectedClient;
            GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | In Launcher";
            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
		}
		
		void ConsoleProcessCommands(string command)
		{
			if (command.Equals("server"))
			{
				StartServer(false);
			}
			else if (command.Equals("server no3d"))
			{
				StartServer(true);
			}
			else if (command.Equals("no3d"))
			{
				StartServer(true);
			}
			else if (command.Equals("client"))
			{
				StartClient();
			}
			else if (command.Equals("client solo"))
			{
				StartSolo();
			}
			else if (command.Equals("solo"))
			{
				StartSolo();
			}
			else if (command.Equals("studio"))
			{
				StartStudio();
			}
			else if (command.Equals("config save"))
			{
				WriteConfigValues();
			}
			else if (command.Equals("config load"))
			{
				ReadConfigValues();
			}
			else if (command.Equals("config reset"))
			{
				ResetConfigValues();
			}
			else if (command.Equals("help"))
			{
				ConsoleHelp(0);
			}
			else if (command.Equals("help config"))
			{
				ConsoleHelp(1);
			}
			else if (command.Equals("config"))
			{
				ConsoleHelp(1);
			}
			else if (command.Equals("help sdk"))
			{
				ConsoleHelp(2);
			}
			else if (command.Equals("sdk"))
			{
				ConsoleHelp(2);
			}
			else if (command.Equals("sdk clientinfo"))
			{
				ClientinfoEditor cie = new ClientinfoEditor();
				cie.Show();
			}
			else if (command.Equals("sdk itemmaker"))
			{
				ItemMaker im = new ItemMaker();
				im.Show();
			}
			else if (command.Equals("clientinfo"))
			{
				ClientinfoEditor cie = new ClientinfoEditor();
				cie.Show();
			}
			else if (command.Equals("itemmaker"))
			{
				ItemMaker im = new ItemMaker();
				im.Show();
			}
			else if (command.Equals(GlobalVars.important))
			{
				GlobalVars.AdminMode = true;
				ConsolePrint("ADMIN MODE ENABLED.", 4);
				ConsolePrint("YOU ARE GOD.", 2);
			}
			else
			{
				ConsolePrint("ERROR 3 - Command is either not registered or valid", 2);
			}
			
		}
		
		void ConsoleHelp(int page)
		{
			if (page == 1)
			{
				ConsolePrint("Help: config", 3);
				ConsolePrint("-------------------------", 1);
				ConsolePrint("= save | Saves the config file", 4);
				ConsolePrint("= load | Reloads the config file", 4);
				ConsolePrint("= reset | Resets the config file", 4);
			}
			else if (page == 2)
			{
				ConsolePrint("Help: sdk", 3);
				ConsolePrint("-------------------------", 1);
				ConsolePrint("= clientinfo | Launches the Novetus Client SDK", 4);
				ConsolePrint("= itemmaker | Launches the Novetus Item SDK", 4);
			}
			else
			{
				ConsolePrint("Help: all", 3);
				ConsolePrint("---------", 1);
				ConsolePrint("= client | Launches client with launcher settings", 3);
				ConsolePrint("-- solo | Launches client in Play Solo mode with launcher settings", 4);
				ConsolePrint("= server |Launches server with launcher settings", 3);
				ConsolePrint("-- no3d | Launches server in NoGraphics mode with launcher settings", 4);
				ConsolePrint("= studio | Launches Roblox Studio with launcher settings", 3);
				ConsolePrint("---------", 1);
				ConsolePrint("= sdk", 3);
				ConsolePrint("-- clientinfo | Launches the Novetus Client SDK", 4);
				ConsolePrint("-- itemmaker | Launches the Novetus Item SDK", 4);
				ConsolePrint("---------", 1);
				ConsolePrint("= config", 3);
				ConsolePrint("-- save | Saves the config file", 4);
				ConsolePrint("-- load | Reloads the config file", 4);
				ConsolePrint("-- reset | Resets the config file", 4);
				ConsolePrint("---------", 1);
			}
		}
		
		void Button21Click(object sender, EventArgs e)
		{
			if (SecurityFuncs.IsElevated)
			{
				try
      			{
     				string loadstring = GlobalVars.BasePath + "/" + System.AppDomain.CurrentDomain.FriendlyName;
        			SecurityFuncs.RegisterURLProtocol("Novetus", loadstring, "Novetus URI");
        			ConsolePrint("URI Successfully Installed!", 3);
					DialogResult result1 = MessageBox.Show("URI Successfully Installed!","Novetus - Install URI", MessageBoxButtons.OK, MessageBoxIcon.Information);
      			}
      			catch (Exception ex)
      			{
        			ConsolePrint("ERROR 5 - Failed to install URI. (" + ex.Message + ")", 2);
					DialogResult result2 = MessageBox.Show("Failed to install URI. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      			}
			}
			else
			{
				ConsolePrint("ERROR 5 - Failed to install URI. (Did not run as Administrator)", 2);
					DialogResult result2 = MessageBox.Show("Failed to install URI. (Error: Did not run as Administrator)","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void NumericUpDown1ValueChanged(object sender, EventArgs e)
		{
			GlobalVars.RobloxPort = Convert.ToInt32(numericUpDown1.Value);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
			label38.Text = GlobalVars.RobloxPort.ToString();
		}
		
		void NumericUpDown2ValueChanged(object sender, EventArgs e)
		{
			GlobalVars.RobloxPort = Convert.ToInt32(numericUpDown2.Value);
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
		}
		
		void NumericUpDown3ValueChanged(object sender, EventArgs e)
		{
			GlobalVars.PlayerLimit = Convert.ToInt32(numericUpDown3.Value);
		}
		
		void Button7Click(object sender, EventArgs e)
		{
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			GlobalVars.RobloxPort = GlobalVars.DefaultRobloxPort;
		}
		
		void Button23Click(object sender, EventArgs e)
		{
			File.AppendAllText("ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			GlobalVars.RobloxPort = GlobalVars.DefaultRobloxPort;
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox2.Checked == true)
			{
				GlobalVars.DisableTeapotTurret = true;
			}
			else if (checkBox2.Checked == false)
			{
				GlobalVars.DisableTeapotTurret = false;
			}
		}
	}
}
