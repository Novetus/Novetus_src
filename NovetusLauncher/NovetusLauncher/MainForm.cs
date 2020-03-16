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
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void InitUPnP()
		{
			if (GlobalVars.UPnP == true)
			{
				try
				{
					UPnP.InitUPnP(DeviceFound,DeviceLost);
					ConsolePrint("UPnP: Service initialized", 3);
				}
				catch (Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("UPnP: Unable to initialize UPnP. Reason - " + ex.Message, 2);
				}
			}
		}
		
		public void StartUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UPnP == true)
			{
				try
				{
					UPnP.StartUPnP(device,protocol,port);
					ConsolePrint("UPnP: Port " + port + " opened on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		public void StopUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UPnP == true)
			{
				try
				{
					UPnP.StopUPnP(device,protocol,port);
					ConsolePrint("UPnP: Port " + port + " closed on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex) when (!Env.Debugging)
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
				StartUPnP(device, Protocol.Udp, GlobalVars.RobloxPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.RobloxPort);
				StartUPnP(device, Protocol.Udp, GlobalVars.WebServer_Port);
				StartUPnP(device, Protocol.Tcp, GlobalVars.WebServer_Port);
			}
			catch (Exception ex) when (!Env.Debugging)
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
 				StopUPnP(device, Protocol.Udp, GlobalVars.RobloxPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.RobloxPort);
				StopUPnP(device, Protocol.Udp, GlobalVars.WebServer_Port);
				StopUPnP(device, Protocol.Tcp, GlobalVars.WebServer_Port);
 			}
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2);
			}
		}

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
            if (GlobalVars.DiscordPresence)
            {
                handlers = new DiscordRpc.EventHandlers();
                handlers.readyCallback = ReadyCallback;
                handlers.disconnectedCallback += DisconnectedCallback;
                handlers.errorCallback += ErrorCallback;
                handlers.joinCallback += JoinCallback;
                handlers.spectateCallback += SpectateCallback;
                handlers.requestCallback += RequestCallback;
                DiscordRpc.Initialize(GlobalVars.appid, ref handlers, true, "");

                LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InLauncher, true);
            }
        }

        void StartWebServer()
        {
        	if (SecurityFuncs.IsElevated)
			{
				try
      			{
     				GlobalVars.WebServer = new SimpleHTTPServer(GlobalVars.DataPath, GlobalVars.WebServer_Port);
        			ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3);
      			}
      			catch (Exception ex) when (!Env.Debugging)
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
      			catch (Exception ex) when (!Env.Debugging)
                {
        			ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
				ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }

        async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
            {
                textBox3.Text = "Loading...";
                string IP = await SecurityFuncs.GetExternalIPAddressAsync();
                textBox3.Text = "";
                string[] lines1 = {
                        SecurityFuncs.Base64Encode(IP),
                        SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.SelectedClient)
                    };
                string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1));
                string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.SelectedClient)
                    };
                string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2));
                string[] text = {
                       "Client: " + GlobalVars.SelectedClient,
                       "IP: " + IP,
                       "Port: " + GlobalVars.RobloxPort.ToString(),
                       "Map: " + GlobalVars.Map,
                       "Players: " + GlobalVars.PlayerLimit,
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
                treeView1.Nodes.Clear();
                _fieldsTreeCache.Nodes.Clear();
                textBox4.Text = "";
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])//your specific tabname
     		{
        		string mapdir = GlobalVars.MapsDir;
				TreeNodeHelper.ListDirectory(treeView1, mapdir, ".rbxl");
				TreeNodeHelper.CopyNodes(treeView1.Nodes,_fieldsTreeCache.Nodes);
				treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.Map, treeView1.Nodes);
				treeView1.Focus();
                textBox3.Text = "";
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
				treeView1.Nodes.Clear();
				_fieldsTreeCache.Nodes.Clear();
                textBox4.Text = "";
                textBox3.Text = "";
                listBox3.Items.Clear();
     			listBox4.Items.Clear();
     		}
     		else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])//your specific tabname
     		{
     			string[] lines_server = File.ReadAllLines(GlobalVars.ConfigDir + "\\servers.txt");
				string[] lines_ports = File.ReadAllLines(GlobalVars.ConfigDir + "\\ports.txt");
				listBox3.Items.AddRange(lines_server);
				listBox4.Items.AddRange(lines_ports);
     			treeView1.Nodes.Clear();
     			_fieldsTreeCache.Nodes.Clear();
                textBox4.Text = "";
                textBox3.Text = "";
                listBox2.Items.Clear();
     		}
     		else
     		{
     			treeView1.Nodes.Clear();
     			_fieldsTreeCache.Nodes.Clear();
                textBox4.Text = "";
                textBox3.Text = "";
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
                GenerateTripcode();
            }
            else
            {
                WriteConfigValues();
            }
			
			StartClient();
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = false;
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			StartServer(false);
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = false;
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
				this.Visible = false;
			}
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
            string[] lines = File.ReadAllLines(GlobalVars.ConfigDir + "\\info.txt"); //File is in System.IO
			string version = lines[0];
    		GlobalVars.DefaultClient = lines[1];
    		GlobalVars.DefaultMap = lines[2];
            GlobalVars.RegisterClient1 = lines[3];
            GlobalVars.RegisterClient2 = lines[4];
            GlobalVars.SelectedClient = GlobalVars.DefaultClient;
    		GlobalVars.Map = GlobalVars.DefaultMap;
    		this.Text = "Novetus " + version;
    		ConsolePrint("Novetus version " + version + " loaded. Initializing config.", 4);
    		if (File.Exists("changelog.txt"))
			{
    			richTextBox2.Text = File.ReadAllText("changelog.txt");
    		}
    		else
    		{
    			ConsolePrint("ERROR 4 - changelog.txt not found.", 2);
    		}
			if (!File.Exists(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName))
			{
				ConsolePrint("WARNING 1 - " + GlobalVars.ConfigName + " not found. Creating one with default values.", 5);
				WriteConfigValues();
			}
			if (!File.Exists(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization))
			{
				ConsolePrint("WARNING 2 - " + GlobalVars.ConfigNameCustomization + " not found. Creating one with default values.", 5);
				WriteCustomizationValues();
			}
			if (!File.Exists(GlobalVars.ConfigDir + "\\servers.txt"))
			{
				ConsolePrint("WARNING 3 - servers.txt not found. Creating empty file.", 5);
				File.Create(GlobalVars.ConfigDir + "\\servers.txt").Dispose();
			}
			if (!File.Exists(GlobalVars.ConfigDir + "\\ports.txt"))
			{
				ConsolePrint("WARNING 4 - ports.txt not found. Creating empty file.", 5);
				File.Create(GlobalVars.ConfigDir + "\\ports.txt").Dispose();
			}

            if (!Directory.Exists(GlobalVars.AssetCacheDirFonts))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirFonts);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirSky))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirSky);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirSounds))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirSounds);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirTexturesGUI))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirTexturesGUI);
            }

            if (!Directory.Exists(GlobalVars.AssetCacheDirScripts))
            {
                Directory.CreateDirectory(GlobalVars.AssetCacheDirScripts);
            }

            label5.Text = GlobalVars.BasePath;
			label8.Text = Application.ProductVersion;
    		GlobalVars.important = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
            label11.Text = version;
    		GlobalVars.Version = version;
    		
    		label12.Text = SplashReader.GetSplash();
    		
    		ReadConfigValues();
    		InitUPnP();
    		StartDiscord();
    		StartWebServer();
		}

        void MainFormClose(object sender, CancelEventArgs e)
        {
            if (GlobalVars.LocalPlayMode != true)
            {
                WriteConfigValues();
            }
            if (GlobalVars.DiscordPresence)
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
			LauncherFuncs.ReadConfigValues(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName);

            checkBox1.Checked = GlobalVars.CloseOnLaunch;
            textBox5.Text = GlobalVars.UserID.ToString();
            label18.Text = GlobalVars.PlayerTripcode.ToString();
            numericUpDown3.Value = Convert.ToDecimal(GlobalVars.PlayerLimit);
            textBox2.Text = GlobalVars.PlayerName;
			label26.Text = GlobalVars.SelectedClient;
			label28.Text = GlobalVars.Map;
			treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.Map, treeView1.Nodes);
            treeView1.Focus();
            //GlobalVars.Map = treeView1.SelectedNode.Text.ToString();
           // GlobalVars.MapPath = treeView1.SelectedNode.FullPath.ToString().Replace(@"\", @"\\");
            numericUpDown1.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.RobloxPort);
			label37.Text = GlobalVars.IP;
			label38.Text = GlobalVars.RobloxPort.ToString();
			checkBox4.Checked = GlobalVars.UPnP;
            checkBox2.Checked = GlobalVars.DiscordPresence;
			ConsolePrint("Config loaded.", 3);
			ReadClientValues(GlobalVars.SelectedClient);
		}
		
		void WriteConfigValues()
		{
			LauncherFuncs.WriteConfigValues(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName);
			ConsolePrint("Config Saved.", 3);
		}
		
		void WriteCustomizationValues()
		{
			LauncherFuncs.WriteCustomizationValues(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization);
			ConsolePrint("Config Saved.", 3);
		}
		
		void ReadClientValues(string ClientName)
		{
			string clientpath = GlobalVars.ClientDir + @"\\" + ClientName + @"\\clientinfo.nov";
			
			if (!File.Exists(clientpath))
			{
				ConsolePrint("ERROR 1 - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
				MessageBox.Show("No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.","Novetus - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			
			if (!string.IsNullOrWhiteSpace(GlobalVars.Warning))
			{
				label30.Text = GlobalVars.Warning;
				label30.Visible = true;
			}
			else
			{
				label30.Visible = false;
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

        void GenerateTripcode()
        {
            LauncherFuncs.GenerateTripcode();
            label18.Text = GlobalVars.PlayerTripcode;
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
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InLauncher);
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
			File.AppendAllText(GlobalVars.ConfigDir + "\\servers.txt", GlobalVars.IP + Environment.NewLine);
		}
		
		void Button11Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalVars.ConfigDir + "\\ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
		}
		
		void Button12Click(object sender, EventArgs e)
		{
			if (listBox3.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox3.SelectedItem.ToString() }, GlobalVars.ConfigDir + "\\servers.txt", GlobalVars.ConfigDir + "\\servers.tmp");
				listBox3.Items.Clear();
				string[] lines_server = File.ReadAllLines(GlobalVars.ConfigDir + "\\servers.txt");
				listBox3.Items.AddRange(lines_server);
			}
		}
		
		void Button13Click(object sender, EventArgs e)
		{
			if (listBox4.SelectedIndex >= 0)
			{
				TextLineRemover.RemoveTextLines(new List<string> { listBox4.SelectedItem.ToString() }, GlobalVars.ConfigDir + "\\ports.txt", GlobalVars.ConfigDir + "\\ports.tmp");
				listBox4.Items.Clear();
				string[] lines_ports = File.ReadAllLines(GlobalVars.ConfigDir + "\\ports.txt");
				listBox4.Items.AddRange(lines_ports);
			}
		}
		
		void Button14Click(object sender, EventArgs e)
		{
			File.Create(GlobalVars.ConfigDir + "\\servers.txt").Dispose();
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines(GlobalVars.ConfigDir + "\\servers.txt");
			listBox3.Items.AddRange(lines_server);
		}
		
		void Button15Click(object sender, EventArgs e)
		{
			File.Create(GlobalVars.ConfigDir + "\\ports.txt").Dispose();
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines(GlobalVars.ConfigDir + "\\ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void Button16Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalVars.ConfigDir + "\\servers.txt", GlobalVars.IP + Environment.NewLine);
			listBox3.Items.Clear();
			string[] lines_server = File.ReadAllLines(GlobalVars.ConfigDir + "\\servers.txt");
			listBox3.Items.AddRange(lines_server);			
		}
		
		void Button17Click(object sender, EventArgs e)
		{
			File.AppendAllText(GlobalVars.ConfigDir + "\\ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
			listBox4.Items.Clear();
			string[] lines_ports = File.ReadAllLines(GlobalVars.ConfigDir + "\\ports.txt");
			listBox4.Items.AddRange(lines_ports);
		}
		
		void Button18Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			StartServer(true);
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = false;
			}						
		}
		
		void Button19Click(object sender, EventArgs e)
		{
			WriteConfigValues();
			StartSolo();
			
			if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = false;
			}
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

        string GetLuaFileName()
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

            return luafile;
        }

        string GetClientEXEDir(ScriptGenerator.ScriptType type)
        {
            string rbxexe = "";
            if (GlobalVars.LegacyMode == true)
            {
                rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
            }
            else
            {
                switch (type)
                {
                    case ScriptGenerator.ScriptType.Client:
                        rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_client.exe";
                        break;
                    case ScriptGenerator.ScriptType.Server:
                        rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_server.exe";
                        break;
                    case ScriptGenerator.ScriptType.Studio:
                        rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_studio.exe";
                        break;
                    case ScriptGenerator.ScriptType.Solo:
                        rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_solo.exe";
                        break;
                    case ScriptGenerator.ScriptType.None:
                    default:
                        rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
                        break;
                }
            }

            return rbxexe;
        }
		
		void StartClient()
		{
			string luafile = GetLuaFileName();
			string rbxexe = GetClientEXEDir(ScriptGenerator.ScriptType.Client);

			string quote = "\"";
			string args = "";
			if (GlobalVars.CustomArgs.Equals("%args%"))
			{
				if (!GlobalVars.FixScriptMapMode)
				{
					args = "-script " + quote + "dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptGenerator.ScriptType.Client, GlobalVars.SelectedClient) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Client, GlobalVars.SelectedClient);
					args = "-script " + quote + luafile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<client>", "</client>", "", luafile, rbxexe);
			}
			try
			{
				ConsolePrint("Client Loaded.", 4);
				if (GlobalVars.AdminMode != true)
				{
					if (GlobalVars.AlreadyHasSecurity != true)
					{
						if (SecurityFuncs.checkClientMD5(GlobalVars.SelectedClient) == true)
						{
							if (SecurityFuncs.checkScriptMD5(GlobalVars.SelectedClient) == true)
							{
								OpenClient(rbxexe,args);
							}
							else
							{
								ConsolePrint("ERROR 4 - Failed to launch Novetus. (The client has been detected as modified.)", 2);
								MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified.)","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							ConsolePrint("ERROR 4 - Failed to launch Novetus. (The client has been detected as modified.)", 2);
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
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void OpenClient(string rbxexe, string args)
		{
			Process client = new Process();
			client.StartInfo.FileName = rbxexe;
			client.StartInfo.Arguments = args;
			client.EnableRaisingEvents = true;
            ReadClientValues(GlobalVars.SelectedClient);
			client.Exited += new EventHandler(ClientExited);
			client.Start();
            client.PriorityClass = ProcessPriorityClass.RealTime;
            SecurityFuncs.RenameWindow(client, ScriptGenerator.ScriptType.Client);
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InMPGame);
        }
		
		void ClientExited(object sender, EventArgs e)
		{
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InLauncher);
            if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = true;
			}
		}
		
		void StartSolo()
		{
            string luafile = GetLuaFileName();
            string rbxexe = GetClientEXEDir(ScriptGenerator.ScriptType.Solo);
            string mapfile = GlobalVars.MapPath;
            string quote = "\"";
			string args = "";
			if (GlobalVars.CustomArgs.Equals("%args%"))
			{
				if (!GlobalVars.FixScriptMapMode)
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptGenerator.ScriptType.Solo, GlobalVars.SelectedClient) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Solo, GlobalVars.SelectedClient);
					args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<solo>", "</solo>", mapfile, luafile, rbxexe);
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
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptGenerator.ScriptType.Solo);
                LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InSoloGame);
            }
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StartServer(bool no3d)
		{
            string luafile = GetLuaFileName();
            string rbxexe = GetClientEXEDir(ScriptGenerator.ScriptType.Server);
            string mapfile = GlobalVars.MapPath;
            string quote = "\"";
			string args = "";
			if (GlobalVars.CustomArgs.Equals("%args%"))
			{
				if (!GlobalVars.FixScriptMapMode)
				{
                    args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptGenerator.ScriptType.Server, GlobalVars.SelectedClient) + "; " + (!string.IsNullOrWhiteSpace(GlobalVars.AddonScriptPath) ? "dofile('" + GlobalVars.AddonScriptPath + "');" : "") + quote + (no3d ? " -no3d" : "");
                }
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Server, GlobalVars.SelectedClient);
					args = "-script " + quote + luafile + quote + (no3d ? " -no3d" : "") + " " + quote + mapfile + quote;
				}
			}
			else
			{
				if (!no3d)
				{
					args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<server>", "</server>", mapfile, luafile, rbxexe);
				}
				else
				{
					args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<no3d>", "</no3d>", mapfile, luafile, rbxexe);
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
                ReadClientValues(GlobalVars.SelectedClient);
				client.Exited += new EventHandler(ServerExited);
				client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptGenerator.ScriptType.Server);
			}
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void ServerExited(object sender, EventArgs e)
		{
            if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = true;
			}
		}
		
		void StartStudio()
		{
            string luafile = GetLuaFileName();
            string rbxexe = GetClientEXEDir(ScriptGenerator.ScriptType.Studio);
            string mapfile = GlobalVars.MapPath;
            string quote = "\"";
			string args = "";
			if (GlobalVars.CustomArgs.Equals("%args%"))
			{
				if (!GlobalVars.FixScriptMapMode)
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptGenerator.ScriptType.Studio, GlobalVars.SelectedClient) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Studio, GlobalVars.SelectedClient);
					args = "-script " + quote + luafile + quote + " " + quote + mapfile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<studio>", "</studio>", mapfile, luafile, rbxexe);
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
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptGenerator.ScriptType.Studio);
                LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InStudio);
            }
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
				MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void StudioExited(object sender, EventArgs e)
		{
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InLauncher);
            if (GlobalVars.CloseOnLaunch == true)
			{
				this.Visible = true;
			}
		}
		
		void ConsoleProcessCommands(string command)
		{
			if (string.Compare(command,"server",true, CultureInfo.InvariantCulture) == 0)
			{
				StartServer(false);
			}
			else if (string.Compare(command,"server no3d",true, CultureInfo.InvariantCulture) == 0)
			{
				StartServer(true);
			}
			else if (string.Compare(command,"no3d",true, CultureInfo.InvariantCulture) == 0)
			{
				StartServer(true);
			}
			else if (string.Compare(command,"client",true, CultureInfo.InvariantCulture) == 0)
			{
				StartClient();
			}
			else if (string.Compare(command,"client solo",true, CultureInfo.InvariantCulture) == 0)
			{
				StartSolo();
			}
			else if (string.Compare(command,"solo",true, CultureInfo.InvariantCulture) == 0)
			{
				StartSolo();
			}
			else if (string.Compare(command,"studio",true, CultureInfo.InvariantCulture) == 0)
            {
				StartStudio();
			}
			else if (string.Compare(command,"config save",true, CultureInfo.InvariantCulture) == 0)
			{
				WriteConfigValues();
			}
			else if (string.Compare(command,"config load",true, CultureInfo.InvariantCulture) == 0)
            {
				ReadConfigValues();
			}
			else if (string.Compare(command,"config reset",true, CultureInfo.InvariantCulture) == 0)
            {
				ResetConfigValues();
			}
			else if (string.Compare(command,"help",true, CultureInfo.InvariantCulture) == 0)
            {
				ConsoleHelp(0);
			}
			else if (string.Compare(command,"help config",true, CultureInfo.InvariantCulture) == 0)
            {
				ConsoleHelp(1);
			}
			else if (string.Compare(command,"config",true, CultureInfo.InvariantCulture) == 0)
            {
				ConsoleHelp(1);
			}
			else if (string.Compare(command,"help sdk",true, CultureInfo.InvariantCulture) == 0)
            {
				ConsoleHelp(2);
			}
			else if (string.Compare(command,"sdk",true, CultureInfo.InvariantCulture) == 0)
            {
                LoadLauncher();
			}
			else if (string.Compare(command,"sdk clientinfo",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadClientSDK();
			}
			else if (string.Compare(command,"sdk itemmaker",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadItemSDK();
			}
			else if (string.Compare(command,"clientinfo",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadClientSDK();
			}
			else if (string.Compare(command,"itemmaker",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadItemSDK();
			}
			else if (string.Compare(command,"sdk clientsdk",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadClientSDK();
			}
			else if (string.Compare(command,"sdk itemsdk",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadItemSDK();
			}
			else if (string.Compare(command,"clientsdk",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadClientSDK();
			}
			else if (string.Compare(command,"itemsdk",true, CultureInfo.InvariantCulture) == 0)
            {
				LoadItemSDK();
			}
            else if (string.Compare(command, "assetlocalizer", true, CultureInfo.InvariantCulture) == 0)
            {
                LoadAssetLocalizer();
            }
            else if (string.Compare(command, "sdk assetlocalizer", true, CultureInfo.InvariantCulture) == 0)
            {
                LoadAssetLocalizer();
            }
            else if (string.Compare(command, "splashtester", true, CultureInfo.InvariantCulture) == 0)
            {
                LoadSplashTester();
            }
            else if (string.Compare(command, "sdk splashtester", true, CultureInfo.InvariantCulture) == 0)
            {
                LoadSplashTester();
            }
            else if (string.Compare(command, "obj2meshv1gui", true, CultureInfo.InvariantCulture) == 0)
            {
                LoadOBJ2MeshV1GUI();
            }
            else if (string.Compare(command, "sdk obj2meshv1gui", true, CultureInfo.InvariantCulture) == 0)
            {
                LoadOBJ2MeshV1GUI();
            }
            else if (string.Compare(command,"charcustom",true, CultureInfo.InvariantCulture) == 0)
            {
				CharacterCustomization cc = new CharacterCustomization();
				cc.Show();
				ConsolePrint("Avatar Customization Loaded.", 4);
			}
			else if (string.Compare(command,"webserver",true, CultureInfo.InvariantCulture) == 0)
            {
				ConsoleHelp(3);
			}
			else if (string.Compare(command,"webserver start",true, CultureInfo.InvariantCulture) == 0)
            {
				if (GlobalVars.IsWebServerOn == false)
				{
					StartWebServer();
				}
				else
				{
					ConsolePrint("WebServer: There is already a web server open.", 2);
				}
			}
			else if (string.Compare(command,"webserver stop",true, CultureInfo.InvariantCulture) == 0)
            {
				if (GlobalVars.IsWebServerOn == true)
				{
					StopWebServer();
				}
				else
				{
					ConsolePrint("WebServer: There is no web server open.", 2);
				}
			}
			else if (string.Compare(command,"webserver restart",true, CultureInfo.InvariantCulture) == 0)
            {
				try
				{
					ConsolePrint("WebServer: Restarting...", 4);
					StopWebServer();
					StartWebServer();
				}
				catch(Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("WebServer: Cannot restart web server. (" + ex.Message + ")", 2);
				}
			}
			else if (string.Compare(command,"start",true, CultureInfo.InvariantCulture) == 0)
            {
				if (GlobalVars.IsWebServerOn == false)
				{
					StartWebServer();
				}
				else
				{
					ConsolePrint("WebServer: There is already a web server open.", 2);
				}
			}
			else if (string.Compare(command,"stop",true, CultureInfo.InvariantCulture) == 0)
            {
				if (GlobalVars.IsWebServerOn == true)
				{
					StopWebServer();
				}
				else
				{
					ConsolePrint("WebServer: There is no web server open.", 2);
				}
			}
			else if (string.Compare(command,"restart",true, CultureInfo.InvariantCulture) == 0)
            {
				try
				{
					ConsolePrint("WebServer: Restarting...", 4);
					StopWebServer();
					StartWebServer();
				}
				catch(Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("WebServer: Cannot restart web server. (" + ex.Message + ")", 2);
				}
			}
			else if (string.Compare(command,GlobalVars.important,true, CultureInfo.InvariantCulture) == 0)
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
		
		void LoadItemSDK()
		{
			ItemMaker im = new ItemMaker();
			im.Show();
			ConsolePrint("Novetus Item SDK Loaded.", 4);
		}
		
		void LoadClientSDK()
		{
			ClientinfoEditor cie = new ClientinfoEditor();
			cie.Show();
			ConsolePrint("Novetus Client SDK Loaded.", 4);
		}

        void LoadAssetLocalizer()
        {
            AssetLocalizer al = new AssetLocalizer();
            al.Show();
            ConsolePrint("Novetus Asset Localizer Loaded.", 4);
        }

        void LoadSplashTester()
        {
            SplashTester st = new SplashTester();
            st.Show();
            ConsolePrint("Splash Tester Loaded.", 4);
        }

        void LoadOBJ2MeshV1GUI()
        {
            Obj2MeshV1GUI obj = new Obj2MeshV1GUI();
            obj.Show();
            ConsolePrint("OBJ2MeshV1 GUI Loaded.", 4);
        }

        void LoadLauncher()
        {
            NovetusSDK im = new NovetusSDK();
            im.Show();
            ConsolePrint("Novetus SDK Launcher Loaded.", 4);
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
                ConsolePrint("-- sdk | Launches the Novetus SDK Launcher", 4);
                ConsolePrint("= clientinfo | Launches the Novetus Client SDK", 4);
				ConsolePrint("= itemmaker | Launches the Novetus Item SDK", 4);
                ConsolePrint("= assetlocalizer | Launches the Novetus Asset Localizer", 4);
                ConsolePrint("= splashtester | Launches the Splash Tester", 4);
                ConsolePrint("= obj2meshv1gui | Launches the OBJ2MeshV1 GUI", 4);
            }
			else if (page == 3)
			{
				ConsolePrint("Help: webserver", 3);
				ConsolePrint("-------------------------", 1);
				ConsolePrint("= restart | Restarts the web server", 4);
				ConsolePrint("= stop | Stops a web server if there is one on.", 4);
				ConsolePrint("= start | Starts a web server if there isn't one on yet.", 4);
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
                ConsolePrint("-- sdk | Launches the Novetus SDK Launcher", 4);
                ConsolePrint("-- clientinfo | Launches the Novetus Client SDK", 4);
				ConsolePrint("-- itemmaker | Launches the Novetus Item SDK", 4);
                ConsolePrint("-- assetlocalizer | Launches the Novetus Asset Localizer", 4);
                ConsolePrint("---------", 1);
				ConsolePrint("= config", 3);
				ConsolePrint("-- save | Saves the config file", 4);
				ConsolePrint("-- load | Reloads the config file", 4);
				ConsolePrint("-- reset | Resets the config file", 4);
				ConsolePrint("---------", 1);
				ConsolePrint("= webserver", 3);
				ConsolePrint("-- restart | Restarts the web server", 4);
				ConsolePrint("-- stop | Stops a web server if there is one on.", 4);
				ConsolePrint("-- start | Starts a web server if there isn't one on yet.", 4);
				ConsolePrint("---------", 1);
			}
		}
		
		void Button21Click(object sender, EventArgs e)
		{
			if (SecurityFuncs.IsElevated)
			{
				try
      			{
     				string loadstring = GlobalVars.BasePath + "/" + AppDomain.CurrentDomain.FriendlyName;
        			SecurityFuncs.RegisterURLProtocol("Novetus", loadstring, "Novetus URI");

                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = GlobalVars.ClientDir + @"\\" + GlobalVars.RegisterClient1 + @"\\RobloxApp_studio.exe";
                    startInfo.Arguments = "/regserver";
                    startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();

                    Process process2 = new Process();
                    ProcessStartInfo startInfo2 = new ProcessStartInfo();
                    startInfo2.FileName = GlobalVars.ClientDir + @"\\" + GlobalVars.RegisterClient2 + @"\\RobloxApp_studio.exe";
                    startInfo2.Arguments = "/regserver";
                    startInfo2.Verb = "runas";
                    process2.StartInfo = startInfo2;
                    process2.Start();

                    ConsolePrint("URI and Library Successfully Installed and Registered!", 3);
					MessageBox.Show("URI and Library Successfully Installed and Registered!","Novetus - Install URI", MessageBoxButtons.OK, MessageBoxIcon.Information);
      			}
      			catch (Exception ex) when (!Env.Debugging)
                {
        			ConsolePrint("ERROR 5 - Failed to register. (" + ex.Message + ")", 2);
					MessageBox.Show("Failed to register. (Error: " + ex.Message + ")","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      			}
			}
			else
			{
				ConsolePrint("ERROR 5 - Failed to register. (Did not run as Administrator)", 2);
				MessageBox.Show("Failed to register. (Error: Did not run as Administrator)","Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			label38.Text = GlobalVars.RobloxPort.ToString();
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
			File.AppendAllText(GlobalVars.ConfigDir + "\\ports.txt", GlobalVars.RobloxPort + Environment.NewLine);
		}
		
		void Button22Click(object sender, EventArgs e)
		{
			numericUpDown1.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			numericUpDown2.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
			GlobalVars.RobloxPort = GlobalVars.DefaultRobloxPort;
		}
		
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode.Nodes.Count == 0)
			{
				GlobalVars.Map = treeView1.SelectedNode.Text.ToString();
                GlobalVars.MapPath = GlobalVars.BasePath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(@"\", @"\\");
				label28.Text = GlobalVars.Map;

                if (File.Exists(GlobalVars.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt"))
                {
                    textBox4.Text = File.ReadAllText(GlobalVars.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt");
                }
                else
                {
                    textBox4.Text = treeView1.SelectedNode.Text.ToString();
                }
            }
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", GlobalVars.MapsDir.Replace(@"\\",@"\"));
		}
		
		void CheckBox4CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox4.Checked == true)
			{
				GlobalVars.UPnP = true;
			}
			else if (checkBox4.Checked == false)
			{
				GlobalVars.UPnP = false;
			}
		}
		
		void CheckBox4Click(object sender, EventArgs e)
		{
			MessageBox.Show("Please restart the Novetus launcher for this option to take effect.","Novetus - UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		void Button24Click(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			_fieldsTreeCache.Nodes.Clear();
        	string mapdir = GlobalVars.MapsDir;
			TreeNodeHelper.ListDirectory(treeView1, mapdir, ".rbxl");
			TreeNodeHelper.CopyNodes(treeView1.Nodes,_fieldsTreeCache.Nodes);
			treeView1.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.Map, treeView1.Nodes);
			treeView1.Focus();
            if (File.Exists(GlobalVars.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt"))
            {
                textBox4.Text = File.ReadAllText(GlobalVars.RootPath + @"\\" + treeView1.SelectedNode.FullPath.ToString().Replace(".rbxl", "") + "_desc.txt");
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
            catch (Exception) when (!Env.Debugging)
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
            if (Directory.Exists(GlobalVars.AssetCacheDir))
            {
                Directory.Delete(GlobalVars.AssetCacheDir, true);
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
            if (checkBox2.Checked == true && GlobalVars.DiscordPresence == false)
            {
                GlobalVars.DiscordPresence = true;
                MessageBox.Show("Restart the launcher to apply changes.");
            }
            else if (checkBox2.Checked == false && GlobalVars.DiscordPresence == true)
            {
                GlobalVars.DiscordPresence = false;
                MessageBox.Show("Restart the launcher to apply changes.");
            }
        }
    }
}
