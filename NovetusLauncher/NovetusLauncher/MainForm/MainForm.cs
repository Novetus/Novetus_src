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
using System.ComponentModel;
using System.Reflection;
using Mono.Nat;
using System.Globalization;

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
			_fieldsTreeCache = new TreeView();
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //

            InitializeComponent();

			Size = new Size(745, 377);
			panel2.Size = new Size(646, 272);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        #region UPnP
        public void InitUPnP()
		{
			if (GlobalVars.UserConfiguration.UPnP == true)
			{
				try
				{
					UPnP.InitUPnP(DeviceFound,DeviceLost);
					ConsolePrint("UPnP: Service initialized", 3);
				}
				catch (Exception ex)
                {
					ConsolePrint("UPnP: Unable to initialize UPnP. Reason - " + ex.Message, 2);
				}
			}
		}
		
		public void StartUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UserConfiguration.UPnP == true)
			{
				try
				{
					UPnP.StartUPnP(device,protocol,port);
					ConsolePrint("UPnP: Port " + port + " opened on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
					ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		public void StopUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UserConfiguration.UPnP == true)
			{
				try
				{
					UPnP.StopUPnP(device,protocol,port);
					ConsolePrint("UPnP: Port " + port + " closed on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
					ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		private void DeviceFound(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
				ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' registered.", 3);
				StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Udp, GlobalVars.WebServerPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.WebServerPort);
			}
			catch (Exception ex)
            {
				ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2);
			}
		}
 
		private void DeviceLost(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
 				ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' disconnected.", 3);
 				StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Udp, GlobalVars.WebServerPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.WebServerPort);
 			}
			catch (Exception ex)
            {
				ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2);
			}
		}
        #endregion

        #region Discord
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
            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                handlers = new DiscordRpc.EventHandlers();
                handlers.readyCallback = ReadyCallback;
                handlers.disconnectedCallback += DisconnectedCallback;
                handlers.errorCallback += ErrorCallback;
                handlers.joinCallback += JoinCallback;
                handlers.spectateCallback += SpectateCallback;
                handlers.requestCallback += RequestCallback;
                DiscordRpc.Initialize(GlobalVars.appid, ref handlers, true, "");

                LauncherFuncs.UpdateRichPresence(LauncherState.InLauncher, "", true);
            }
        }
		#endregion

		#region Web Server
		//udp clients will connect to the web server alongside the game.
		void StartWebServer()
		{
			if (SecurityFuncs.IsElevated)
			{
				try
				{
					GlobalVars.WebServer = new SimpleHTTPServer(Directories.ServerDir, GlobalVars.WebServerPort);
					ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3);
				}
				catch (Exception ex)
				{
					ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (" + ex.Message + ")", 2);
					label17.Visible = false;
				}
			}
			else
			{
				ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (Did not run as Administrator)", 2);
				label17.Visible = false;
			}
		}

		void StopWebServer()
		{
			if (SecurityFuncs.IsElevated)
			{
				try
				{
					ConsolePrint("WebServer: Server has stopped on port: " + GlobalVars.WebServer.Port.ToString(), 2);
					GlobalVars.WebServer.Stop();
				}
				catch (Exception ex)
				{
					ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (" + ex.Message + ")", 2);
				}
			}
			else
			{
				ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
		}
		#endregion

		async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (tabControl1.SelectedTab)
			{
				case TabPage pg2 when pg2 == tabControl1.TabPages["tabPage2"]:
					treeView1.Nodes.Clear();
					_fieldsTreeCache.Nodes.Clear();
					textBox4.Text = "";
					listBox2.Items.Clear();
					listBox3.Items.Clear();
					listBox4.Items.Clear();
					//since we are async, DO THESE first or we'll clear out random stuff.
					textBox3.Text = "Loading...";
					string IP = await SecurityFuncs.GetExternalIPAddressAsync();
					textBox3.Text = "";
					string[] lines1 = {
						SecurityFuncs.Base64Encode(IP),
						SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
						SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
					};
					string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1), true);
					string[] lines2 = {
						SecurityFuncs.Base64Encode("localhost"),
						SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
						SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
					};
					string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2), true);
					string[] text = {
					   "Client: " + GlobalVars.UserConfiguration.SelectedClient,
					   "IP: " + IP,
					   "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
					   "Map: " + GlobalVars.UserConfiguration.Map,
					   "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
					   "Version: Novetus " + GlobalVars.Version,
					   "Online URI Link:",
					   URI,
					   "Local URI Link:",
					   URI2,
					   GlobalVars.IsWebServerOn == true ? "Web Server URL:" : "",
					   GlobalVars.IsWebServerOn == true ? "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString() : "",
					   GlobalVars.IsWebServerOn == true ? "Local Web Server URL:" : "",
					   GlobalVars.IsWebServerOn == true ? GlobalVars.LocalWebServerURI : ""
					   };

					foreach (string str in text)
					{
						if (!string.IsNullOrWhiteSpace(str))
						{
							textBox3.AppendText(str);
							textBox3.AppendText(Environment.NewLine);
						}
					}
					textBox3.SelectionStart = 0;
					textBox3.ScrollToCaret();
					break;
				case TabPage pg4 when pg4 == tabControl1.TabPages["tabPage4"]:
					string mapdir = Directories.MapsDir;
					TreeNodeHelper.ListDirectory(treeView1, mapdir, ".rbxl");
					TreeNodeHelper.CopyNodes(treeView1.Nodes, _fieldsTreeCache.Nodes);
					treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, treeView1.Nodes);
					treeView1.Focus();
					textBox3.Text = "";
					listBox2.Items.Clear();
					listBox3.Items.Clear();
					listBox4.Items.Clear();
					break;
				case TabPage pg3 when pg3 == tabControl1.TabPages["tabPage3"]:
					string clientdir = Directories.ClientDir;
					DirectoryInfo dinfo = new DirectoryInfo(clientdir);
					DirectoryInfo[] Dirs = dinfo.GetDirectories();
					foreach (DirectoryInfo dir in Dirs)
					{
						listBox2.Items.Add(dir.Name);
					}
					listBox2.SelectedItem = GlobalVars.UserConfiguration.SelectedClient;
					treeView1.Nodes.Clear();
					_fieldsTreeCache.Nodes.Clear();
					textBox4.Text = "";
					textBox3.Text = "";
					listBox3.Items.Clear();
					listBox4.Items.Clear();
					break;
				case TabPage pg6 when pg6 == tabControl1.TabPages["tabPage6"]:
					string[] lines_server = File.ReadAllLines(Directories.ConfigDir + "\\servers.txt");
					string[] lines_ports = File.ReadAllLines(Directories.ConfigDir + "\\ports.txt");
					listBox3.Items.AddRange(lines_server);
					listBox4.Items.AddRange(lines_ports);
					treeView1.Nodes.Clear();
					_fieldsTreeCache.Nodes.Clear();
					textBox4.Text = "";
					textBox3.Text = "";
					listBox2.Items.Clear();
					break;
				default:
					treeView1.Nodes.Clear();
					_fieldsTreeCache.Nodes.Clear();
					textBox4.Text = "";
					textBox3.Text = "";
					listBox2.Items.Clear();
					listBox3.Items.Clear();
					listBox4.Items.Clear();
					break;
			}
		}

		void Button1Click(object sender, EventArgs e)
		{
            if (LocalVars.LocalPlayMode == true)
            {
                GeneratePlayerID();
                GenerateTripcode();
            }
            else
            {
                WriteConfigValues();
            }

            StartClient();

            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
            {
                Visible = false;
            }
        }

        void Button2Click(object sender, EventArgs e)
		{
            WriteConfigValues();
            StartServer(false);

            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
            {
                Visible = false;
            }
        }

        void Button3Click(object sender, EventArgs e)
		{
            DialogResult result = MessageBox.Show("If you want to test out your place, you will have to save your place in Novetus's map folder, then launch your place in Play Solo.", "Novetus - Launch ROBLOX Studio", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel)
                return;

            WriteConfigValues();
            StartStudio(false);
            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
            {
                Visible = false;
            }
        }

        void Button18Click(object sender, EventArgs e)
        {
            WriteConfigValues();
            StartServer(true);

            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
            {
                Visible = false;
            }
        }

        void Button19Click(object sender, EventArgs e)
        {
            WriteConfigValues();
            StartSolo();

            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
            {
                Visible = false;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("If you want to test out your place, you will have to save your place in Novetus's map folder, then launch your place in Play Solo.", "Novetus - Launch ROBLOX Studio", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel)
                return;

            WriteConfigValues();
            StartStudio(true);
            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
            {
                Visible = false;
            }
        }

        void MainFormLoad(object sender, EventArgs e)
		{
            Text = "Novetus " + GlobalVars.Version;
    		ConsolePrint("Novetus version " + GlobalVars.Version + " loaded. Initializing config.", 4);
            ConsolePrint("Novetus path: " + Directories.BasePath, 4);
            if (File.Exists(Directories.RootPath + "\\changelog.txt"))
			{
    			richTextBox2.Text = File.ReadAllText(Directories.RootPath + "\\changelog.txt");
    		}
    		else
    		{
    			ConsolePrint("ERROR - " + Directories.RootPath + "\\changelog.txt not found.", 2);
    		}

            if (File.Exists(Directories.RootPath + "\\credits.txt"))
            {
                richTextBox3.Text = File.ReadAllText(Directories.RootPath + "\\credits.txt");
            }
            else
            {
                ConsolePrint("ERROR - " + Directories.RootPath + "\\credits.txt not found.", 2);
            }

            if (!File.Exists(Directories.ConfigDir + "\\" + GlobalVars.ConfigName))
			{
				ConsolePrint("WARNING - " + Directories.ConfigDir + "\\" + GlobalVars.ConfigName + " not found. Creating one with default values.", 5);
				WriteConfigValues();
			}
			if (!File.Exists(Directories.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization))
			{
				ConsolePrint("WARNING - " + Directories.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization + " not found. Creating one with default values.", 5);
				WriteCustomizationValues();
			}
			if (!File.Exists(Directories.ConfigDir + "\\servers.txt"))
			{
				ConsolePrint("WARNING - " + Directories.ConfigDir + "\\servers.txt not found. Creating empty file.", 5);
				File.Create(Directories.ConfigDir + "\\servers.txt").Dispose();
			}
			if (!File.Exists(Directories.ConfigDir + "\\ports.txt"))
			{
				ConsolePrint("WARNING - " + Directories.ConfigDir + "\\ports.txt not found. Creating empty file.", 5);
				File.Create(Directories.ConfigDir + "\\ports.txt").Dispose();
			}

            if (!Directory.Exists(Directories.AssetCacheDirFonts))
            {
                Directory.CreateDirectory(Directories.AssetCacheDirFonts);
            }

            if (!Directory.Exists(Directories.AssetCacheDirSky))
            {
                Directory.CreateDirectory(Directories.AssetCacheDirSky);
            }

            if (!Directory.Exists(Directories.AssetCacheDirSounds))
            {
                Directory.CreateDirectory(Directories.AssetCacheDirSounds);
            }

            if (!Directory.Exists(Directories.AssetCacheDirTexturesGUI))
            {
                Directory.CreateDirectory(Directories.AssetCacheDirTexturesGUI);
            }

            if (!Directory.Exists(Directories.AssetCacheDirScripts))
            {
                Directory.CreateDirectory(Directories.AssetCacheDirScripts);
            }

			label8.Text = Application.ProductVersion;
    		GlobalVars.important = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
            label11.Text = GlobalVars.Version;
    		
    		label12.Text = SplashReader.GetSplash();
            LocalVars.prevsplash = label12.Text;

            ReadConfigValues();
    		InitUPnP();
    		StartDiscord();
    		StartWebServer();
		}

        void MainFormClose(object sender, CancelEventArgs e)
        {
            if (LocalVars.LocalPlayMode != true)
            {
                WriteConfigValues();
            }
            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                DiscordRpc.Shutdown();
            }
			if (GlobalVars.IsWebServerOn)
			{
				StopWebServer();
			}
        }
		
		void ReadConfigValues()
		{
			LauncherFuncs.Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, false);

			checkBox1.Checked = GlobalVars.UserConfiguration.CloseOnLaunch;
            textBox5.Text = GlobalVars.UserConfiguration.UserID.ToString();
            label18.Text = GlobalVars.UserConfiguration.PlayerTripcode.ToString();
            numericUpDown3.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.PlayerLimit);
            textBox2.Text = GlobalVars.UserConfiguration.PlayerName;
			label26.Text = GlobalVars.UserConfiguration.SelectedClient;
			label28.Text = GlobalVars.UserConfiguration.Map;
			treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, treeView1.Nodes);
            treeView1.Focus();
            numericUpDown1.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
			label37.Text = GlobalVars.IP;
			label38.Text = GlobalVars.UserConfiguration.RobloxPort.ToString();
			checkBox2.Checked = GlobalVars.UserConfiguration.DiscordPresence;
			checkBox5.Checked = GlobalVars.UserConfiguration.ReShade;
			checkBox6.Checked = GlobalVars.ReShadeFPSDisplay;
			checkBox7.Checked = GlobalVars.ReShadePerformanceMode;

			switch (GlobalVars.UserConfiguration.GraphicsMode)
			{
				case GraphicsMode.DirectX:
					comboBox1.SelectedIndex = 1;
					break;
				case GraphicsMode.OpenGL:
				default:
					comboBox1.SelectedIndex = 0;
					break;
			}

			switch (GlobalVars.UserConfiguration.QualityLevel)
			{
				case QualityLevel.VeryLow:
					comboBox2.SelectedIndex = 0;
					break;
				case QualityLevel.Low:
					comboBox2.SelectedIndex = 1;
					break;
				case QualityLevel.Medium:
					comboBox2.SelectedIndex = 2;
					break;
				case QualityLevel.High:
					comboBox2.SelectedIndex = 3;
					break;
				case QualityLevel.Ultra:
				default:
					comboBox2.SelectedIndex = 4;
					break;
			}

			ConsolePrint("Config loaded.", 3);
			ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
		}
		
		void WriteConfigValues()
		{
			LauncherFuncs.Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, true);
            ConsolePrint("Config Saved.", 3);
		}

		void WriteCustomizationValues()
		{
			LauncherFuncs.Customization(Directories.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, true);
			ConsolePrint("Config Saved.", 3);
		}
		
		void ReadClientValues(string ClientName)
		{
            string clientpath = Directories.ClientDir + @"\\" + ClientName + @"\\clientinfo.nov";

			if (!File.Exists(clientpath))
			{
				ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
				MessageBox.Show("No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", "Novetus - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
				GlobalVars.UserConfiguration.SelectedClient = GlobalVars.DefaultClient;
				ReadClientValues(ClientName);
			}
			else
			{
				LauncherFuncs.ReadClientValues(clientpath);

				switch (GlobalVars.SelectedClientInfo.UsesPlayerName)
                {
					case true:
						textBox2.Enabled = true;
						break;
					case false:
						textBox2.Enabled = false;
						break;
				}

				switch (GlobalVars.SelectedClientInfo.UsesID)
				{
					case true:
						textBox5.Enabled = true;
						button4.Enabled = true;
						if (GlobalVars.IP.Equals("localhost"))
						{
							checkBox3.Enabled = true;
						}
						break;
					case false:
						textBox5.Enabled = false;
						button4.Enabled = false;
						checkBox3.Enabled = false;
						LocalVars.LocalPlayMode = false;
						break;
				}

				if (!string.IsNullOrWhiteSpace(GlobalVars.SelectedClientInfo.Warning))
				{
					label30.Text = GlobalVars.SelectedClientInfo.Warning;
					label30.Visible = true;
				}
				else
				{
					label30.Visible = false;
				}

				textBox6.Text = GlobalVars.SelectedClientInfo.Description;
				label26.Text = GlobalVars.UserConfiguration.SelectedClient;
				ConsolePrint("Client '" + GlobalVars.UserConfiguration.SelectedClient + "' successfully loaded.", 3);
			}
		}
		
		void GeneratePlayerID()
		{
			LauncherFuncs.GeneratePlayerID();
			textBox5.Text = Convert.ToString(GlobalVars.UserConfiguration.UserID);
		}

        void GenerateTripcode()
        {
            LauncherFuncs.GenerateTripcode();
            label18.Text = GlobalVars.UserConfiguration.PlayerTripcode;
        }
		
		void TextBox1TextChanged(object sender, EventArgs e)
		{
			GlobalVars.IP = textBox1.Text;
			checkBox3.Enabled = false;
			LocalVars.LocalPlayMode = false;
			label37.Text = GlobalVars.IP;
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.CloseOnLaunch = checkBox1.Checked;
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
			GlobalVars.UserConfiguration.PlayerName = textBox2.Text;
		}
		
		void ListBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.SelectedClient = listBox2.SelectedItem.ToString();
			ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
            LauncherFuncs.UpdateRichPresence(LauncherState.InLauncher, "");
        }
		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
			LocalVars.LocalPlayMode = checkBox3.Checked;
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
			LocalVars.LocalPlayMode = false;
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
			File.AppendAllText(Directories.ConfigDir + "\\servers.txt", GlobalVars.IP + Environment.NewLine);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			File.AppendAllText(Directories.ConfigDir + "\\ports.txt", GlobalVars.UserConfiguration.RobloxPort + Environment.NewLine);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			if (listBox3.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox3.SelectedItem.ToString() }, Directories.ConfigDir + "\\servers.txt", Directories.ConfigDir + "\\servers.tmp");
				listBox3.Items.Clear();
				string[] lines_server = File.ReadAllLines(Directories.ConfigDir + "\\servers.txt");
				listBox3.Items.AddRange(lines_server);
			}
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			if (listBox4.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox4.SelectedItem.ToString() }, Directories.ConfigDir + "\\ports.txt", Directories.ConfigDir + "\\ports.tmp");
				listBox4.Items.Clear();
				string[] lines_ports = File.ReadAllLines(Directories.ConfigDir + "\\ports.txt");
				listBox4.Items.AddRange(lines_ports);
			}
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			File.Create(Directories.ConfigDir + "\\servers.txt").Dispose();
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines(Directories.ConfigDir + "\\servers.txt");
			listBox3.Items.AddRange(lines_server);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			File.Create(Directories.ConfigDir + "\\ports.txt").Dispose();
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines(Directories.ConfigDir + "\\ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			File.AppendAllText(Directories.ConfigDir + "\\servers.txt", GlobalVars.IP + Environment.NewLine);
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines(Directories.ConfigDir + "\\servers.txt");
			listBox3.Items.AddRange(lines_server);			
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			File.AppendAllText(Directories.ConfigDir + "\\ports.txt", GlobalVars.UserConfiguration.RobloxPort + Environment.NewLine);
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines(Directories.ConfigDir + "\\ports.txt");
			listBox4.Items.AddRange(lines_ports);
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
				case Keys.X:
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
			richTextBox1.AppendText("[" + DateTime.Now.ToShortTimeString() + "] - ", Color.White);

			switch (type)
			{
				case 2:
					richTextBox1.AppendText(text, Color.Red);
					break;
				case 3:
					richTextBox1.AppendText(text, Color.Lime);
					break;
				case 4:
					richTextBox1.AppendText(text, Color.Aqua);
					break;
				case 5:
					richTextBox1.AppendText(text, Color.Yellow);
					break;
				case 6:
					richTextBox1.AppendText(text, Color.LightSalmon);
					break;
				case 1:
				default:
					richTextBox1.AppendText(text, Color.White);
					break;
			}

			richTextBox1.AppendText(Environment.NewLine);
		}

		//Rewrite these into one function. Preferably global.
		void StartClient()
		{
			string luafile = LauncherFuncs.GetLuaFileName();
			string rbxexe = LauncherFuncs.GetClientEXEDir(ScriptType.Client);

			string quote = "\"";
			string args = "";
			if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
				if (!GlobalVars.SelectedClientInfo.Fix2007)
				{
					args = "-script " + quote + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.Client) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.Client);
					args = "-script " + quote + luafile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<client>", "</client>", "", luafile, rbxexe);
			}
			try
			{
				ConsolePrint("Client Loaded.", 4);
				if (GlobalVars.AdminMode != true)
				{
					if (GlobalVars.SelectedClientInfo.AlreadyHasSecurity != true)
					{
						if (SecurityFuncs.checkClientMD5(GlobalVars.UserConfiguration.SelectedClient) == true)
						{
							if (SecurityFuncs.checkScriptMD5(GlobalVars.UserConfiguration.SelectedClient) == true)
							{
								OpenClient(rbxexe,args);
							}
							else
							{
								ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified.)", 2);
								MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified.)","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified.)", 2);
							MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified.)","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					else
					{
						OpenClient(rbxexe,args);
					}
				}
				else
				{
					OpenClient(rbxexe,args);
				}
			}
			catch (Exception ex)
            {
				ConsolePrint("ERROR - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void OpenClient(string rbxexe, string args)
		{
			Process client = new Process();
			client.StartInfo.FileName = rbxexe;
			client.StartInfo.Arguments = args;
			client.EnableRaisingEvents = true;
            ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
			client.Exited += new EventHandler(ClientExited);
			client.Start();
            client.PriorityClass = ProcessPriorityClass.RealTime;
            SecurityFuncs.RenameWindow(client, ScriptType.Client, GlobalVars.UserConfiguration.Map);
            LauncherFuncs.UpdateRichPresence(LauncherState.InMPGame, GlobalVars.UserConfiguration.Map);
		}
		
		void ClientExited(object sender, EventArgs e)
		{
            LauncherFuncs.UpdateRichPresence(LauncherState.InLauncher, "");
            if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
			{
				Visible = true;
			}
		}

		void ServerExited(object sender, EventArgs e)
		{
			if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
			{
				Visible = true;
			}
		}

		void EasterEggExited(object sender, EventArgs e)
		{
			LauncherFuncs.UpdateRichPresence(LauncherState.InLauncher, "");
			label12.Text = LocalVars.prevsplash;
			if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
			{
				Visible = true;
			}
		}

		void StartSolo()
		{
            string luafile = LauncherFuncs.GetLuaFileName();
            string rbxexe = LauncherFuncs.GetClientEXEDir(ScriptType.Solo);
            string mapfile = GlobalVars.UserConfiguration.MapPath;
            string quote = "\"";
			string args = "";
			if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
				if (!GlobalVars.SelectedClientInfo.Fix2007)
				{
					args = quote + mapfile + "\" -script \"" + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.Solo) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.Solo);
					args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<solo>", "</solo>", mapfile, luafile, rbxexe);
			}
			try
			{
				ConsolePrint("Play Solo Loaded.", 4);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
                ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
				client.Exited += new EventHandler(ClientExited);
				client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptType.Solo, GlobalVars.UserConfiguration.Map);
                LauncherFuncs.UpdateRichPresence(LauncherState.InSoloGame, GlobalVars.UserConfiguration.Map);
			}
			catch (Exception ex)
            {
				ConsolePrint("ERROR - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StartServer(bool no3d)
		{
            string luafile = LauncherFuncs.GetLuaFileName();
            string rbxexe = LauncherFuncs.GetClientEXEDir(ScriptType.Server);
            string mapfile = GlobalVars.UserConfiguration.MapPath;
            string quote = "\"";
			string args = "";
			if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
				if (!GlobalVars.SelectedClientInfo.Fix2007)
				{
                    args = quote + mapfile + "\" -script \"" + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.Server) + "; " + (!string.IsNullOrWhiteSpace(GlobalVars.AddonScriptPath) ? LauncherFuncs.ChangeGameSettings() + " dofile('" + GlobalVars.AddonScriptPath + "');" : "") + quote + (no3d ? " -no3d" : "");
                }
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.Server);
					args = "-script " + quote + luafile + quote + (no3d ? " -no3d" : "") + " " + quote + mapfile + quote;
				}
			}
			else
			{
				if (!no3d)
				{
					args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<server>", "</server>", mapfile, luafile, rbxexe);
				}
				else
				{
					args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<no3d>", "</no3d>", mapfile, luafile, rbxexe);
				}
			}
			try
			{
				//when we add upnp, change this
				ConsolePrint("Server Loaded.", 4);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
                ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
				client.Exited += new EventHandler(ServerExited);
				client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptType.Server, GlobalVars.UserConfiguration.Map);
			}
			catch (Exception ex)
            {
				ConsolePrint("ERROR - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StartStudio(bool nomap)
		{
            string luafile = LauncherFuncs.GetLuaFileName();
            string rbxexe = LauncherFuncs.GetClientEXEDir(ScriptType.Studio);
            string mapfile = (nomap ? "" : GlobalVars.UserConfiguration.MapPath);
            string mapname = (nomap ? "" : GlobalVars.UserConfiguration.Map);
            string quote = "\"";
			string args = "";
			if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
				if (!GlobalVars.SelectedClientInfo.Fix2007)
				{
					args = quote + mapfile + "\" -script \"" + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.Studio) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.Studio);
					args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<studio>", "</studio>", mapfile, luafile, rbxexe);
			}
			try
			{
				ConsolePrint("Studio Loaded.", 4);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
                ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
				client.Exited += new EventHandler(ClientExited);
				client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptType.Studio, mapname);
                LauncherFuncs.UpdateRichPresence(LauncherState.InStudio, mapname);
			}
			catch (Exception ex)
            {
				ConsolePrint("ERROR - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void StartEasterEgg()
		{
			label12.Text = "<3";
			string luafile = LauncherFuncs.GetLuaFileName();
			string rbxexe = LauncherFuncs.GetClientEXEDir(ScriptType.EasterEgg);
			string mapfile = Directories.ConfigDirData + "\\Appreciation.rbxl";
			string quote = "\"";
			string args = "";
			if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
				if (!GlobalVars.SelectedClientInfo.Fix2007)
				{
					args = quote + mapfile + "\" -script \"" + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.EasterEgg) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.EasterEgg);
					args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<solo>", "</solo>", mapfile, luafile, rbxexe);
			}
			try
			{
				ConsolePrint("Easter Egg Loaded.", 6);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
				ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
				client.Start();
				client.Exited += new EventHandler(EasterEggExited);
				client.PriorityClass = ProcessPriorityClass.RealTime;
				SecurityFuncs.RenameWindow(client, ScriptType.EasterEgg, "");
				LauncherFuncs.UpdateRichPresence(LauncherState.InEasterEggGame, "");
			}
			catch (Exception ex)
			{
				ConsolePrint("ERROR - Failed to launch Easter Egg. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Easter Egg. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void ConsoleProcessCommands(string cmd)
		{
			switch(cmd)
            {
				case string server3d when string.Compare(server3d, "server 3d", true, CultureInfo.InvariantCulture) == 0:
					StartServer(false);
					break;
				case string serverno3d when string.Compare(serverno3d, "server no3d", true, CultureInfo.InvariantCulture) == 0:
					StartServer(false);
					break;
				case string client when string.Compare(client, "client", true, CultureInfo.InvariantCulture) == 0:
					StartClient();
					break;
				case string solo when string.Compare(solo, "solo", true, CultureInfo.InvariantCulture) == 0:
					StartSolo();
					break;
				case string studiomap when string.Compare(studiomap, "studio map", true, CultureInfo.InvariantCulture) == 0:
					StartStudio(false);
					break;
				case string studionomap when string.Compare(studionomap, "studio nomap", true, CultureInfo.InvariantCulture) == 0:
					StartStudio(true);
					break;
				case string configsave when string.Compare(configsave, "config save", true, CultureInfo.InvariantCulture) == 0:
					WriteConfigValues();
					break;
				case string configload when string.Compare(configload, "config load", true, CultureInfo.InvariantCulture) == 0:
					ReadConfigValues();
					break;
				case string configreset when string.Compare(configreset, "config reset", true, CultureInfo.InvariantCulture) == 0:
					ResetConfigValues();
					break;
				case string help when string.Compare(help, "help", true, CultureInfo.InvariantCulture) == 0:
					ConsoleHelp();
					break;
				case string sdk when string.Compare(sdk, "sdk", true, CultureInfo.InvariantCulture) == 0:
					LoadLauncher();
					break;
				case string webserverstart when string.Compare(webserverstart, "webserver start", true, CultureInfo.InvariantCulture) == 0:
					if (GlobalVars.IsWebServerOn == false)
					{
						StartWebServer();
					}
					else
					{
						ConsolePrint("WebServer: There is already a web server on.", 2);
					}
					break;
				case string webserverstop when string.Compare(webserverstop, "webserver stop", true, CultureInfo.InvariantCulture) == 0:
					if (GlobalVars.IsWebServerOn == true)
					{
						StopWebServer();
					}
					else
					{
						ConsolePrint("WebServer: There is no web server on.", 2);
					}
					break;
				case string webserverrestart when string.Compare(webserverrestart, "webserver restart", true, CultureInfo.InvariantCulture) == 0:
					try
					{
						ConsolePrint("WebServer: Restarting...", 4);
						StopWebServer();
						StartWebServer();
					}
					catch (Exception ex)
					{
						ConsolePrint("WebServer: Cannot restart web server. (" + ex.Message + ")", 2);
					}
					break;
				case string important when string.Compare(important, GlobalVars.important, true, CultureInfo.InvariantCulture) == 0:
					GlobalVars.AdminMode = true;
					ConsolePrint("ADMIN MODE ENABLED.", 4);
					ConsolePrint("YOU ARE GOD.", 2);
					break;
				default:
					ConsolePrint("ERROR 3 - Command is either not registered or valid", 2);
					break;
            }
		}

        void LoadLauncher()
        {
            NovetusSDK im = new NovetusSDK();
            im.Show();
            ConsolePrint("Novetus SDK Launcher Loaded.", 4);
        }

        void ConsoleHelp()
		{
			ConsolePrint("Help:", 3);
			ConsolePrint("---------", 1);
			ConsolePrint("= client | Launches client with launcher settings", 3);
			ConsolePrint("= solo | Launches client in Play Solo mode with launcher settings", 3);
			ConsolePrint("= server 3d | Launches server with launcher settings", 3);
			ConsolePrint("= server no3d | Launches server in NoGraphics mode with launcher settings", 3);
			ConsolePrint("= studio map | Launches Roblox Studio with the selected map",34);
			ConsolePrint("= studio nomap | Launches Roblox Studio without the selected map", 3);
			ConsolePrint("= sdk | Launches the Novetus SDK Launcher", 3);
			ConsolePrint("---------", 1);
			ConsolePrint("= config save | Saves the config file", 3);
			ConsolePrint("= config load | Reloads the config file", 3);
			ConsolePrint("= config reset | Resets the config file", 3);
			ConsolePrint("---------", 1);
			ConsolePrint("= webserver restart | Restarts the web server", 3);
			ConsolePrint("= webserver stop | Stops a web server if there is one on.", 3);
			ConsolePrint("= webserver start | Starts a web server if there isn't one on yet.", 3);
			ConsolePrint("---------", 1);
		}
		
		void Button21Click(object sender, EventArgs e)
		{
			if (SecurityFuncs.IsElevated)
			{
				try
      			{
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = Directories.ClientDir + @"\\" + GlobalVars.RegisterClient1 + @"\\RobloxApp_studio.exe";
                    startInfo.Arguments = "/regserver";
                    startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();

                    Process process2 = new Process();
                    ProcessStartInfo startInfo2 = new ProcessStartInfo();
                    startInfo2.FileName = Directories.ClientDir + @"\\" + GlobalVars.RegisterClient2 + @"\\RobloxApp_studio.exe";
                    startInfo2.Arguments = "/regserver";
                    startInfo2.Verb = "runas";
                    process2.StartInfo = startInfo2;
                    process2.Start();

                    ConsolePrint("UserAgent Library successfully installed and registered!", 3);
					MessageBox.Show("UserAgent Library successfully installed and registered!", "Novetus - Register UserAgent Library", MessageBoxButtons.OK, MessageBoxIcon.Information);
      			}
      			catch (Exception ex)
                {
        			ConsolePrint("ERROR - Failed to register. (" + ex.Message + ")", 2);
					MessageBox.Show("Failed to register. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      			}
			}
			else
			{
				ConsolePrint("ERROR - Failed to register. (Did not run as Administrator)", 2);
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
			numericUpDown1.Value = Convert.ToDecimal(LocalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(LocalVars.DefaultRobloxPort);
			GlobalVars.UserConfiguration.RobloxPort = LocalVars.DefaultRobloxPort;
		}
		
		void Button23Click(object sender, EventArgs e)
		{
			File.AppendAllText(Directories.ConfigDir + "\\ports.txt", GlobalVars.UserConfiguration.RobloxPort + Environment.NewLine);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			numericUpDown1.Value = Convert.ToDecimal(LocalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(LocalVars.DefaultRobloxPort);
			GlobalVars.UserConfiguration.RobloxPort = LocalVars.DefaultRobloxPort;
		}
		
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode.Nodes.Count == 0)
			{
				GlobalVars.UserConfiguration.Map = treeView1.SelectedNode.Text.ToString();
                GlobalVars.UserConfiguration.MapPathSnip = treeView1.SelectedNode.FullPath.ToString().Replace(@"\", @"\\");
                GlobalVars.UserConfiguration.MapPath = Directories.BasePath + @"\\" + GlobalVars.UserConfiguration.MapPathSnip;
				label28.Text = GlobalVars.UserConfiguration.Map;

                if (File.Exists(Directories.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(Directories.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = treeView1.SelectedNode.Text.ToString();
                }
            }
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", Directories.MapsDir.Replace(@"\\",@"\"));
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.UPnP = checkBox4.Checked;
		}
		
		void CheckBox4Click(object sender, EventArgs e)
		{
			MessageBox.Show("Please restart the Novetus launcher for this option to take effect." + Environment.NewLine + "Make sure to check if your router has UPnP functionality enabled. Please note that some routers may not support UPnP, and some ISPs will block the UPnP protocol. This may not work for all users.","Novetus - UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		void Button24Click(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			_fieldsTreeCache.Nodes.Clear();
        	string mapdir = Directories.MapsDir;
			TreeNodeHelper.ListDirectory(treeView1, mapdir, ".rbxl");
			TreeNodeHelper.CopyNodes(treeView1.Nodes,_fieldsTreeCache.Nodes);
			treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, treeView1.Nodes);
			treeView1.Focus();
            if (File.Exists(Directories.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt"))
            {
                textBox4.Text = File.ReadAllText(Directories.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt");
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
                    ConsolePrint("AddonLoader - " + addon.getInstallOutcome(), 3);
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
                {
                    ConsolePrint("AddonLoader - " + addon.getInstallOutcome(), 2);
                }
            }

            if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
            {
                MessageBox.Show(addon.getInstallOutcome());
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Directories.AssetCacheDir))
            {
                Directory.Delete(Directories.AssetCacheDir, true);
                ConsolePrint("Asset cache cleared!", 3);
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
			MessageBox.Show("Restart the launcher to apply changes.");
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
            LoadLauncher();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (LocalVars.Clicks < 10)
            {
                LocalVars.Clicks += 1;

				switch(LocalVars.Clicks)
                {
					case 1:
						label12.Text = "Hi " + GlobalVars.UserConfiguration.PlayerName + "!";
						break;
					case 3:
						label12.Text = "How are you doing today?";
						break;
					case 6:
						label12.Text = "I just wanted to say something.";
						break;
					case 9:
						label12.Text = "Just wait a little on the last click, OK?";
						break;
					case 10:
						WriteConfigValues();
						StartEasterEgg();

						if (GlobalVars.UserConfiguration.CloseOnLaunch == true)
						{
							Visible = false;
						}
						break;
					default:
						break;
                }
            }
        }

		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.UserConfiguration.ReShade = checkBox5.Checked;
		}

		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.ReShadeFPSDisplay = checkBox6.Checked;
		}

		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			GlobalVars.ReShadePerformanceMode = checkBox7.Checked;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			switch (comboBox1.SelectedIndex)
			{
				case 1:
					GlobalVars.UserConfiguration.GraphicsMode = GraphicsMode.DirectX;
					break;
				default:
					GlobalVars.UserConfiguration.GraphicsMode = GraphicsMode.OpenGL;
					break;
			}
		}

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
			switch (comboBox2.SelectedIndex)
			{
				case 0:
					GlobalVars.UserConfiguration.QualityLevel = QualityLevel.VeryLow;
					break;
				case 1:
					GlobalVars.UserConfiguration.QualityLevel = QualityLevel.Low;
					break;
				case 2:
					GlobalVars.UserConfiguration.QualityLevel = QualityLevel.Medium;
					break;
				case 3:
					GlobalVars.UserConfiguration.QualityLevel = QualityLevel.High;
					break;
				case 4:
				default:
					GlobalVars.UserConfiguration.QualityLevel = QualityLevel.Ultra;
					break;
			}
		}

        private void button36_Click(object sender, EventArgs e)
        {
			GlobalVars.UserConfiguration.OldLayout = true;
			WriteConfigValues();
			Application.Restart();
        }
    }
}
