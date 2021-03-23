#region Usings
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region LauncherForm - Shared
    public class LauncherFormShared
    {
        #region Variables
        public DiscordRPC.EventHandlers handlers;
        public List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        public int LastNodeIndex = 0;
        public string LastSearchText;
        bool isWPF = false;

        //CONTROLS
        public Form Parent = null;
        public Settings.UIOptions.Style FormStyle = Settings.UIOptions.Style.None;
        public RichTextBox ConsoleBox, ChangelogBox, ReadmeBox = null;
        public TabControl Tabs = null;
        public TextBox MapDescBox, ServerInfo, SearchBar, PlayerIDTextBox, PlayerNameTextBox, ClientDescriptionBox = null;
        public TreeView Tree, _TreeCache = null;
        public ListBox ServerBox, PortBox, ClientBox = null;
        public Label SplashLabel, ProductVersionLabel, NovetusVersionLabel, PlayerTripcodeLabel, IPLabel, PortLabel,
            SelectedClientLabel, SelectedMapLabel, ClientWarningLabel = null;
        public ComboBox StyleSelectorBox, GraphicsModeBox, GraphicsLevelBox = null;
        public CheckBox WebServerCheckbox, CloseOnLaunchCheckbox, DiscordPresenceCheckbox, ReShadeCheckbox, ReShadeFPSDisplayCheckBox, 
            ReShadePerformanceModeCheckBox, uPnPCheckBox, ShowServerNotifsCheckBox, LocalPlayCheckBox = null;
        public Button RegeneratePlayerIDButton = null;
        public NumericUpDown PlayerLimitBox, HostPortBox, JoinPortBox = null;
        public string TabPageHost, TabPageMaps, TabPageClients, TabPageSaved = "";
        #endregion

        #region Constructor
        public LauncherFormShared(bool WPF = false)
        {
            //TODO: add WPF support...
            isWPF = WPF;
        }
        #endregion

        #region UPnP
        public void InitUPnP()
        {
            if (GlobalVars.UserConfiguration.UPnP)
            {
                try
                {
                    NetFuncs.InitUPnP(DeviceFound, DeviceLost);
                    GlobalFuncs.ConsolePrint("UPnP: Service initialized", 3, ConsoleBox);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to initialize UPnP. Reason - " + ex.Message, 2, ConsoleBox);
                }
            }
        }

        public void StartUPnP(INatDevice device, Protocol protocol, int port)
        {
            if (GlobalVars.UserConfiguration.UPnP)
            {
                try
                {
                    NetFuncs.StartUPnP(device, protocol, port);
                    string IP = (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString());
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " opened on '" + IP + "' (" + protocol.ToString() + ")", 3, ConsoleBox);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2, ConsoleBox);
                }
            }
        }

        public void StopUPnP(INatDevice device, Protocol protocol, int port)
        {
            if (GlobalVars.UserConfiguration.UPnP)
            {
                try
                {
                    NetFuncs.StopUPnP(device, protocol, port);
                    string IP = (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString());
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " closed on '" + IP + "' (" + protocol.ToString() + ")", 3, ConsoleBox);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2, ConsoleBox);
                }
            }
        }

        public void DeviceFound(object sender, DeviceEventArgs args)
        {
            try
            {
                INatDevice device = args.Device;
                string IP = (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString());
                GlobalFuncs.ConsolePrint("UPnP: Device '" + IP + "' registered.", 3, ConsoleBox);
                StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
                StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
                StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.WebServerPort);
                StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.WebServerPort);
            }
            catch (Exception ex)
            {
                GlobalFuncs.ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2, ConsoleBox);
            }
        }

        public void DeviceLost(object sender, DeviceEventArgs args)
        {
            try
            {
                INatDevice device = args.Device;
                string IP = (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString());
                GlobalFuncs.ConsolePrint("UPnP: Device '" + IP + "' disconnected.", 3, ConsoleBox);
                StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
                StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
                StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.WebServerPort);
                StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.WebServerPort);
            }
            catch (Exception ex)
            {
                GlobalFuncs.ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2, ConsoleBox);
            }
        }
        #endregion

        #region Discord
        public void ReadyCallback()
        {
            GlobalFuncs.ConsolePrint("Discord RPC: Ready", 3, ConsoleBox);
        }

        public void DisconnectedCallback(int errorCode, string message)
        {
            GlobalFuncs.ConsolePrint("Discord RPC: Disconnected. Reason - " + errorCode + ": " + message, 2, ConsoleBox);
        }

        public void ErrorCallback(int errorCode, string message)
        {
            GlobalFuncs.ConsolePrint("Discord RPC: Error. Reason - " + errorCode + ": " + message, 2, ConsoleBox);
        }

        public void JoinCallback(string secret)
        {
        }

        public void SpectateCallback(string secret)
        {
        }

        public void RequestCallback(DiscordRPC.JoinRequest request)
        {
        }

        public void StartDiscord()
        {
            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                handlers = new DiscordRPC.EventHandlers();
                handlers.readyCallback = ReadyCallback;
                handlers.disconnectedCallback += DisconnectedCallback;
                handlers.errorCallback += ErrorCallback;
                handlers.joinCallback += JoinCallback;
                handlers.spectateCallback += SpectateCallback;
                handlers.requestCallback += RequestCallback;
                DiscordRPC.Initialize(GlobalVars.appid, ref handlers, true, "");

                GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "", true);
            }
        }
        #endregion

        #region Web Server
        //udp clients will connect to the web server alongside the game.
        public void StartWebServer()
        {
            if (SecurityFuncs.IsElevated)
            {
                try
                {
                    GlobalVars.WebServer = new SimpleHTTPServer(GlobalPaths.DataPath, GlobalVars.UserConfiguration.WebServerPort);
                    GlobalFuncs.ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3, ConsoleBox);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (" + ex.Message + ")", 2, ConsoleBox);
                }
            }
            else
            {
                GlobalFuncs.ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (Did not run as Administrator)", 2, ConsoleBox);
            }
        }

        public void StopWebServer()
        {
            if (SecurityFuncs.IsElevated)
            {
                try
                {
                    GlobalFuncs.ConsolePrint("WebServer: Server has stopped on port: " + GlobalVars.WebServer.Port.ToString(), 2, ConsoleBox);
                    GlobalVars.WebServer.Stop();
                }
                catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (" + ex.Message + ")", 2, ConsoleBox);
                }
            }
            else
            {
                GlobalFuncs.ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (Did not run as Administrator)", 2, ConsoleBox);
            }
        }
        #endregion

        #region Form Event Functions
        public void InitForm()
        {
            Parent.Text = "Novetus " + GlobalVars.ProgramInformation.Version;
            GlobalFuncs.ConsolePrint("Novetus version " + GlobalVars.ProgramInformation.Version + " loaded. Initializing config.", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 4, ConsoleBox);

            if (File.Exists(GlobalPaths.RootPath + "\\changelog.txt"))
            {
                ChangelogBox.Text = File.ReadAllText(GlobalPaths.RootPath + "\\changelog.txt");
            }
            else
            {
                GlobalFuncs.ConsolePrint("ERROR - " + GlobalPaths.RootPath + "\\changelog.txt not found.", 2, ConsoleBox);
            }

            if (File.Exists(GlobalPaths.RootPath + "\\README-AND-CREDITS.TXT"))
            {
                ReadmeBox.Text = File.ReadAllText(GlobalPaths.RootPath + "\\README-AND-CREDITS.TXT");
            }
            else
            {
                GlobalFuncs.ConsolePrint("ERROR - " + GlobalPaths.RootPath + "\\README-AND-CREDITS.TXT not found.", 2, ConsoleBox);
            }

            if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName))
            {
                GlobalFuncs.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName + " not found. Creating one with default values.", 5, ConsoleBox);
                WriteConfigValues();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization))
            {
                GlobalFuncs.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization + " not found. Creating one with default values.", 5, ConsoleBox);
                WriteCustomizationValues();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\servers.txt"))
            {
                GlobalFuncs.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\servers.txt not found. Creating empty file.", 5, ConsoleBox);
                File.Create(GlobalPaths.ConfigDir + "\\servers.txt").Dispose();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\ports.txt"))
            {
                GlobalFuncs.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\ports.txt not found. Creating empty file.", 5, ConsoleBox);
                File.Create(GlobalPaths.ConfigDir + "\\ports.txt").Dispose();
            }

            GlobalFuncs.CreateAssetCacheDirectories();

            ProductVersionLabel.Text = Application.ProductVersion;
            LocalVars.important = SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location);
            NovetusVersionLabel.Text = GlobalVars.ProgramInformation.Version;

            SplashLabel.Text = SplashReader.GetSplash();
            LocalVars.prevsplash = SplashLabel.Text;

            ReadConfigValues(true);
            InitUPnP();
            StartDiscord();
            if (GlobalVars.UserConfiguration.WebServer)
            {
                StartWebServer();
            }
        }

        public void CloseEvent()
        {
            if (!GlobalVars.LocalPlayMode)
            {
                WriteConfigValues();
            }
            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                DiscordRPC.Shutdown();
            }
            if (GlobalVars.IsWebServerOn)
            {
                StopWebServer();
            }
        }

        public async Task ChangeTabs()
        {
            switch (Tabs.SelectedTab)
            {
                case TabPage pg2 when pg2 == Tabs.TabPages[TabPageHost]:
                    Tree.Nodes.Clear();
                    _TreeCache.Nodes.Clear();
                    MapDescBox.Text = "";
                    ClientBox.Items.Clear();
                    ServerBox.Items.Clear();
                    PortBox.Items.Clear();
                    //since we are async, DO THESE first or we'll clear out random stuff.
                    ServerInfo.Text = "Loading...";
                    string IP = await SecurityFuncs.GetExternalIPAddressAsync();
                    ServerInfo.Text = "";
                    string[] lines1 = {
                        SecurityFuncs.Base64Encode((!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : IP)),
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
                       "IP: " + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : IP),
                       "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                       "Map: " + GlobalVars.UserConfiguration.Map,
                       "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                       "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                       "Online URI Link:",
                       URI,
                       "Local URI Link:",
                       URI2,
                       GlobalVars.IsWebServerOn ? "Web Server URL:" : "",
                       GlobalVars.IsWebServerOn ? "http://" + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : IP) + ":" + GlobalVars.WebServer.Port.ToString() : "",
                       GlobalVars.IsWebServerOn ? "Local Web Server URL:" : "",
                       GlobalVars.IsWebServerOn ? "http://localhost:" + (GlobalVars.WebServer.Port.ToString()).ToString() : ""
                       };

                    foreach (string str in text)
                    {
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            ServerInfo.AppendText(str + Environment.NewLine);
                        }
                    }
                    ServerInfo.SelectionStart = 0;
                    ServerInfo.ScrollToCaret();
                    break;
                case TabPage pg3 when pg3 == Tabs.TabPages[TabPageClients]:
                    string clientdir = GlobalPaths.ClientDir;
                    DirectoryInfo dinfo = new DirectoryInfo(clientdir);
                    DirectoryInfo[] Dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo dir in Dirs)
                    {
                        ClientBox.Items.Add(dir.Name);
                    }
                    ClientBox.SelectedItem = GlobalVars.UserConfiguration.SelectedClient;
                    Tree.Nodes.Clear();
                    _TreeCache.Nodes.Clear();
                    MapDescBox.Text = "";
                    ServerInfo.Text = "";
                    ServerBox.Items.Clear();
                    PortBox.Items.Clear();
                    break;
                case TabPage pg4 when pg4 == Tabs.TabPages[TabPageMaps]:
                    string mapdir = GlobalPaths.MapsDir;
                    string[] fileexts = new string[] { ".rbxl", ".rbxlx" };
                    TreeNodeHelper.ListDirectory(Tree, mapdir, fileexts);
                    TreeNodeHelper.CopyNodes(Tree.Nodes, _TreeCache.Nodes);
                    Tree.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, Tree.Nodes);
                    Tree.Focus();
                    ServerInfo.Text = "";
                    ClientBox.Items.Clear();
                    ServerBox.Items.Clear();
                    PortBox.Items.Clear();
                    break;
                case TabPage pg6 when pg6 == Tabs.TabPages[TabPageSaved]:
                    string[] lines_server = File.ReadAllLines(GlobalPaths.ConfigDir + "\\servers.txt");
                    string[] lines_ports = File.ReadAllLines(GlobalPaths.ConfigDir + "\\ports.txt");
                    ServerBox.Items.AddRange(lines_server);
                    PortBox.Items.AddRange(lines_ports);
                    Tree.Nodes.Clear();
                    _TreeCache.Nodes.Clear();
                    MapDescBox.Text = "";
                    ServerInfo.Text = "";
                    ClientBox.Items.Clear();
                    break;
                default:
                    Tree.Nodes.Clear();
                    _TreeCache.Nodes.Clear();
                    MapDescBox.Text = "";
                    ServerInfo.Text = "";
                    ClientBox.Items.Clear();
                    ServerBox.Items.Clear();
                    PortBox.Items.Clear();
                    break;
            }
        }

        public void StartGame(ScriptType gameType, bool no3d = false, bool nomap = false)
        {
            if (gameType == ScriptType.Studio)
            {
                if (FormStyle == Settings.UIOptions.Style.Extended)
                {
                    DialogResult result = MessageBox.Show("If you want to test out your place, you will have to save your place in Novetus's map folder, then launch your place in Play Solo.", "Novetus - Launch ROBLOX Studio", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.Cancel)
                        return;
                }
                else
                {
                    DialogResult result = MessageBox.Show("If you want to test out your place, you will have to save your place in Novetus's map folder, then launch your place in Play Solo." + Environment.NewLine + Environment.NewLine + "Press Yes to launch Studio with a map, or No to launch Studio without a map.", "Novetus - Launch ROBLOX Studio", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    bool nomapLegacy = false;

                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.No:
                            nomapLegacy = true;
                            nomap = nomapLegacy;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (gameType == ScriptType.Client && GlobalVars.LocalPlayMode)
            {
                GeneratePlayerID();
                GenerateTripcode();
            }
            else
            {
                WriteConfigValues();
            }

            switch (gameType)
            {
                case ScriptType.Client:
                    GlobalFuncs.LaunchRBXClient(ScriptType.Client, false, true, new EventHandler(ClientExited), ConsoleBox);
                    break;
                case ScriptType.Server:
                    GlobalFuncs.LaunchRBXClient(ScriptType.Server, no3d, false, new EventHandler(ClientExitedBase), ConsoleBox);
                    break;
                case ScriptType.Solo:
                    GlobalFuncs.LaunchRBXClient(ScriptType.Solo, false, false, new EventHandler(ClientExited), ConsoleBox);
                    break;
                case ScriptType.Studio:
                    GlobalFuncs.LaunchRBXClient(ScriptType.Studio, false, nomap, new EventHandler(ClientExited), ConsoleBox);
                    break;
                case ScriptType.EasterEgg:
                    GlobalFuncs.LaunchRBXClient(ScriptType.EasterEgg, false, false, new EventHandler(EasterEggExited), ConsoleBox);
                    break;
                case ScriptType.None:
                default:
                    break;
            }

            if (GlobalVars.UserConfiguration.CloseOnLaunch)
            {
                Parent.Visible = false;
            }
        }

        public void EasterEggLogic()
        {
            if (LocalVars.Clicks < 10)
            {
                LocalVars.Clicks += 1;

                switch (LocalVars.Clicks)
                {
                    case 1:
                        SplashLabel.Text = "Hi " + GlobalVars.UserConfiguration.PlayerName + "!";
                        break;
                    case 3:
                        SplashLabel.Text = "How are you doing today?";
                        break;
                    case 6:
                        SplashLabel.Text = "I just wanted to say something.";
                        break;
                    case 9:
                        SplashLabel.Text = "Just wait a little on the last click, OK?";
                        break;
                    case 10:
                        SplashLabel.Text = "Thank you. <3";
                        StartGame(ScriptType.EasterEgg);
                        break;
                    default:
                        break;
                }
            }
        }

        void ClientExited(object sender, EventArgs e)
        {
            GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");
            ClientExitedBase(sender, e);
        }

        void EasterEggExited(object sender, EventArgs e)
        {
            GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");
            SplashLabel.Text = LocalVars.prevsplash;
            ClientExitedBase(sender, e);
        }

        void ClientExitedBase(object sender, EventArgs e)
        {
            if (GlobalVars.UserConfiguration.CloseOnLaunch)
            {
                Parent.Visible = true;
            }
        }

        // FINALLY. https://stackoverflow.com/questions/11530643/treeview-search
        public void SearchMaps()
        {
            string searchText = SearchBar.Text;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return;
            };

            try
            {
                if (LastSearchText != searchText)
                {
                    //It's a new Search
                    CurrentNodeMatches.Clear();
                    LastSearchText = searchText;
                    LastNodeIndex = 0;
                    SearchNodes(searchText, Tree.Nodes[0]);
                }

                if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
                {
                    TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                    LastNodeIndex++;
                    Tree.SelectedNode = selectedNode;
                    Tree.SelectedNode.Expand();
                    Tree.Select();
                }
                else
                {
                    //It's a new Search
                    CurrentNodeMatches.Clear();
                    LastSearchText = searchText;
                    LastNodeIndex = 0;
                    SearchNodes(searchText, Tree.Nodes[0]);
                    TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                    LastNodeIndex++;
                    Tree.SelectedNode = selectedNode;
                    Tree.SelectedNode.Expand();
                    Tree.Select();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The map '" + searchText + "' cannot be found. Please try another term.");
            }
        }

        public void ProcessConsole(KeyEventArgs e)
        {
            //Command proxy

            int totalLines = ConsoleBox.Lines.Length;
            if (totalLines > 0)
            {
                string lastLine = ConsoleBox.Lines[totalLines - 1];

                if (e.KeyCode == Keys.Enter)
                {
                    ConsoleBox.AppendText(Environment.NewLine);
                    ConsoleProcessCommands(lastLine);
                    e.Handled = true;
                }
            }

            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
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

        public void ConsoleProcessCommands(string cmd)
        {
            switch (cmd)
            {
                case string server3d when string.Compare(server3d, "server 3d", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Server);
                    break;
                case string serverno3d when string.Compare(serverno3d, "server no3d", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Server, true);
                    break;
                case string client when string.Compare(client, "client", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Client);
                    break;
                case string solo when string.Compare(solo, "solo", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Solo);
                    break;
                case string studiomap when string.Compare(studiomap, "studio map", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Studio);
                    break;
                case string studionomap when string.Compare(studionomap, "studio nomap", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Studio, false, true);
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
                    if (!GlobalVars.IsWebServerOn)
                    {
                        StartWebServer();
                    }
                    else
                    {
                        GlobalFuncs.ConsolePrint("WebServer: There is already a web server on.", 2, ConsoleBox);
                    }
                    break;
                case string webserverstop when string.Compare(webserverstop, "webserver stop", true, CultureInfo.InvariantCulture) == 0:
                    if (GlobalVars.IsWebServerOn)
                    {
                        StopWebServer();
                    }
                    else
                    {
                        GlobalFuncs.ConsolePrint("WebServer: There is no web server on.", 2, ConsoleBox);
                    }
                    break;
                case string webserverrestart when string.Compare(webserverrestart, "webserver restart", true, CultureInfo.InvariantCulture) == 0:
                    try
                    {
                        GlobalFuncs.ConsolePrint("WebServer: Restarting...", 4, ConsoleBox);
                        StopWebServer();
                        StartWebServer();
                    }
                    catch (Exception ex)
                    {
                        GlobalFuncs.ConsolePrint("WebServer: Cannot restart web server. (" + ex.Message + ")", 2, ConsoleBox);
                    }
                    break;
                case string dlldeleteon when string.Compare(dlldeleteon, "dlldelete on", true, CultureInfo.InvariantCulture) == 0:
                    GlobalVars.UserConfiguration.DisableReshadeDelete = false;
                    GlobalFuncs.ConsolePrint("ReShade DLL deletion enabled.", 4, ConsoleBox);
                    break;
                case string dlldeleteoff when string.Compare(dlldeleteoff, "dlldelete off", true, CultureInfo.InvariantCulture) == 0:
                    GlobalVars.UserConfiguration.DisableReshadeDelete = true;
                    GlobalFuncs.ConsolePrint("ReShade DLL deletion disabled.", 4, ConsoleBox);
                    break;
                case string important when string.Compare(important, LocalVars.important, true, CultureInfo.InvariantCulture) == 0:
                    GlobalVars.AdminMode = true;
                    GlobalFuncs.ConsolePrint("ADMIN MODE ENABLED.", 4, ConsoleBox);
                    GlobalFuncs.ConsolePrint("YOU ARE GOD.", 2, ConsoleBox);
                    break;
                default:
                    GlobalFuncs.ConsolePrint("ERROR 3 - Command is either not registered or valid", 2, ConsoleBox);
                    break;
            }
        }

        public void LoadLauncher()
        {
            NovetusSDK im = new NovetusSDK();
            im.Show();
            GlobalFuncs.ConsolePrint("Novetus SDK Launcher Loaded.", 4, ConsoleBox);
        }

        public void ConsoleHelp()
        {
            GlobalFuncs.ConsolePrint("Help:", 3, ConsoleBox);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox);
            GlobalFuncs.ConsolePrint("= client | Launches client with launcher settings", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= solo | Launches client in Play Solo mode with launcher settings", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= server 3d | Launches server with launcher settings", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= server no3d | Launches server in NoGraphics mode with launcher settings", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= studio map | Launches Roblox Studio with the selected map", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= studio nomap | Launches Roblox Studio without the selected map", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= sdk | Launches the Novetus SDK Launcher", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox);
            GlobalFuncs.ConsolePrint("= config save | Saves the config file", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= config load | Reloads the config file", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= config reset | Resets the config file", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox);
            GlobalFuncs.ConsolePrint("= webserver restart | Restarts the web server", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= webserver stop | Stops a web server if there is one on.", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= webserver start | Starts a web server if there isn't one on yet.", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox);
            GlobalFuncs.ConsolePrint("= dlldelete off | Turn off the deletion of opengl32.dll when ReShade is off.", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("= dlldelete on | Turn on the deletion of opengl32.dll when ReShade is off.", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox);
        }

        public void SwitchStyles()
        {
            switch (StyleSelectorBox.SelectedIndex)
            {
                case 1:
                    if (FormStyle == Settings.UIOptions.Style.Extended)
                    {
                        GlobalVars.UserConfiguration.LauncherStyle = Settings.UIOptions.Style.Compact;
                        CloseEvent();
                        Application.Restart();
                    }
                    break;
                default:
                    if (FormStyle == Settings.UIOptions.Style.Compact)
                    {
                        GlobalVars.UserConfiguration.LauncherStyle = Settings.UIOptions.Style.Extended;
                        CloseEvent();
                        Application.Restart();
                    }
                    break;
            }
        }

        public void ReadConfigValues(bool initial = false)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);

            CloseOnLaunchCheckbox.Checked = GlobalVars.UserConfiguration.CloseOnLaunch;
            PlayerIDTextBox.Text = GlobalVars.UserConfiguration.UserID.ToString();
            PlayerTripcodeLabel.Text = GlobalVars.UserConfiguration.PlayerTripcode.ToString();
            PlayerLimitBox.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.PlayerLimit);
            PlayerNameTextBox.Text = GlobalVars.UserConfiguration.PlayerName;
            SelectedClientLabel.Text = GlobalVars.UserConfiguration.SelectedClient;
            SelectedMapLabel.Text = GlobalVars.UserConfiguration.Map;
            Tree.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, Tree.Nodes);
            Tree.Focus();
            JoinPortBox.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
            HostPortBox.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
            IPLabel.Text = GlobalVars.IP;
            PortLabel.Text = GlobalVars.UserConfiguration.RobloxPort.ToString();
            DiscordPresenceCheckbox.Checked = GlobalVars.UserConfiguration.DiscordPresence;
            ReShadeCheckbox.Checked = GlobalVars.UserConfiguration.ReShade;
            ReShadeFPSDisplayCheckBox.Checked = GlobalVars.UserConfiguration.ReShadeFPSDisplay;
            ReShadePerformanceModeCheckBox.Checked = GlobalVars.UserConfiguration.ReShadePerformanceMode;
            uPnPCheckBox.Checked = GlobalVars.UserConfiguration.UPnP;
            ShowServerNotifsCheckBox.Checked = GlobalVars.UserConfiguration.ShowServerNotifications;

            if (SecurityFuncs.IsElevated)
            {
                WebServerCheckbox.Enabled = true;
                WebServerCheckbox.Checked = GlobalVars.UserConfiguration.WebServer;
            }
            else
            {
                WebServerCheckbox.Enabled = false;
            }

            if (FormStyle == Settings.UIOptions.Style.Extended)
            {
                if (GraphicsModeBox != null)
                {
                    switch (GlobalVars.UserConfiguration.GraphicsMode)
                    {
                        case Settings.GraphicsOptions.Mode.OpenGL:
                            GraphicsModeBox.SelectedIndex = 1;
                            break;
                        case Settings.GraphicsOptions.Mode.DirectX:
                            GraphicsModeBox.SelectedIndex = 2;
                            break;
                        default:
                            GraphicsModeBox.SelectedIndex = 0;
                            break;
                    }
                }

                if (GraphicsLevelBox != null)
                {
                    switch (GlobalVars.UserConfiguration.QualityLevel)
                    {
                        case Settings.GraphicsOptions.Level.VeryLow:
                            GraphicsLevelBox.SelectedIndex = 1;
                            break;
                        case Settings.GraphicsOptions.Level.Low:
                            GraphicsLevelBox.SelectedIndex = 2;
                            break;
                        case Settings.GraphicsOptions.Level.Medium:
                            GraphicsLevelBox.SelectedIndex = 3;
                            break;
                        case Settings.GraphicsOptions.Level.High:
                            GraphicsLevelBox.SelectedIndex = 4;
                            break;
                        case Settings.GraphicsOptions.Level.Ultra:
                            GraphicsLevelBox.SelectedIndex = 5;
                            break;
                        case Settings.GraphicsOptions.Level.Custom:
                            GraphicsLevelBox.SelectedIndex = 6;
                            break;
                        default:
                            GraphicsLevelBox.SelectedIndex = 0;
                            break;
                    }
                }
            }

            switch (GlobalVars.UserConfiguration.LauncherStyle)
            {
                case Settings.UIOptions.Style.Compact:
                    StyleSelectorBox.SelectedIndex = 1;
                    break;
                case Settings.UIOptions.Style.Extended:
                default:
                    StyleSelectorBox.SelectedIndex = 0;
                    break;
            }

            GlobalFuncs.ConsolePrint("Config loaded.", 3, ConsoleBox);
            ReadClientValues(initial);
        }

        public void WriteConfigValues(bool ShowBox = false)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            GlobalFuncs.ReadClientValues(ConsoleBox);
            GlobalFuncs.ConsolePrint("Config Saved.", 3, ConsoleBox);
            if (ShowBox)
            {
                MessageBox.Show("Config Saved!");
            }
        }

        public void WriteCustomizationValues()
        {
            GlobalFuncs.Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
            GlobalFuncs.ConsolePrint("Config Saved.", 3, ConsoleBox);
        }

        public void ResetConfigValues()
        {
            //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp
            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name != Parent.Name)
                    f.Close();
            }

            GlobalFuncs.ResetConfigValues();
            WriteConfigValues();
            ReadConfigValues();
        }

        public void ReadClientValues(bool initial = false)
        {
            GlobalFuncs.ReadClientValues(ConsoleBox, initial);

            switch (GlobalVars.SelectedClientInfo.UsesPlayerName)
            {
                case true:
                    PlayerNameTextBox.Enabled = true;
                    break;
                case false:
                    PlayerNameTextBox.Enabled = false;
                    break;
            }

            switch (GlobalVars.SelectedClientInfo.UsesID)
            {
                case true:
                    PlayerIDTextBox.Enabled = true;
                    RegeneratePlayerIDButton.Enabled = true;
                    if (GlobalVars.IP.Equals("localhost"))
                    {
                        LocalPlayCheckBox.Enabled = true;
                    }
                    break;
                case false:
                    PlayerIDTextBox.Enabled = false;
                    RegeneratePlayerIDButton.Enabled = false;
                    LocalPlayCheckBox.Enabled = false;
                    GlobalVars.LocalPlayMode = false;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(GlobalVars.SelectedClientInfo.Warning))
            {
                ClientWarningLabel.Text = GlobalVars.SelectedClientInfo.Warning;
                ClientWarningLabel.Visible = true;
            }
            else
            {
                ClientWarningLabel.Visible = false;
            }

            ClientDescriptionBox.Text = GlobalVars.SelectedClientInfo.Description;
            SelectedClientLabel.Text = GlobalVars.UserConfiguration.SelectedClient;
        }

        public void GeneratePlayerID()
        {
            GlobalFuncs.GeneratePlayerID();
            PlayerIDTextBox.Text = Convert.ToString(GlobalVars.UserConfiguration.UserID);
        }

        public void GenerateTripcode()
        {
            GlobalFuncs.GenerateTripcode();
            PlayerTripcodeLabel.Text = GlobalVars.UserConfiguration.PlayerTripcode;
        }

        public void InstallAddon()
        {
            AddonLoader addon = new AddonLoader();
            addon.setFileListDisplay(10);
            try
            {
                addon.LoadAddon();
                if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
                {
                    GlobalFuncs.ConsolePrint("AddonLoader - " + addon.getInstallOutcome(), 3, ConsoleBox);
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
                {
                    GlobalFuncs.ConsolePrint("AddonLoader - " + addon.getInstallOutcome(), 2, ConsoleBox);
                }
            }

            if (!string.IsNullOrWhiteSpace(addon.getInstallOutcome()))
            {
                MessageBox.Show(addon.getInstallOutcome());
            }
        }

        public void ClearAssetCache()
        {
            if (Directory.Exists(GlobalPaths.AssetCacheDir))
            {
                Directory.Delete(GlobalPaths.AssetCacheDir, true);
                GlobalFuncs.ConsolePrint("Asset cache cleared!", 3, ConsoleBox);
                MessageBox.Show("Asset cache cleared!");
            }
            else
            {
                MessageBox.Show("There is no asset cache to clear.");
            }
        }

        public void RefreshMaps()
        {
            Tree.Nodes.Clear();
            _TreeCache.Nodes.Clear();
            string mapdir = GlobalPaths.MapsDir;
            string[] fileexts = new string[] { ".rbxl", ".rbxlx" };
            TreeNodeHelper.ListDirectory(Tree, mapdir, fileexts);
            TreeNodeHelper.CopyNodes(Tree.Nodes, _TreeCache.Nodes);
            Tree.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, Tree.Nodes);
            Tree.Focus();
            if (File.Exists(GlobalPaths.RootPath + @"\\" + Tree.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
            {
                MapDescBox.Text = File.ReadAllText(GlobalPaths.RootPath + @"\\" + Tree.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
            }
            else
            {
                MapDescBox.Text = Tree.SelectedNode.Text.ToString();
            }
        }

        public void RestartLauncherAfterSetting(CheckBox box, bool webServer = false)
        {
            string title = webServer ? "Novetus - Web Server" : "Novetus - UPnP";
            string subText = webServer ? "Make sure you are running the launcher in Administrator Mode in order for the Web Server to function." : 
                "Make sure to check if your router has UPnP functionality enabled.\n" + 
                "Please note that some routers may not support UPnP, and some ISPs will block the UPnP protocol.\nThis may not work for all users.";

            switch (box.Checked)
            {
                case false:
                    MessageBox.Show("Novetus will now restart.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    MessageBox.Show("Novetus will now restart." + Environment.NewLine + subText, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

            WriteConfigValues();
            Application.Restart();
        }

        public void SelectMap()
        {
            if (Tree.SelectedNode.Nodes.Count == 0)
            {
                GlobalVars.UserConfiguration.Map = Tree.SelectedNode.Text.ToString();
                GlobalVars.UserConfiguration.MapPathSnip = Tree.SelectedNode.FullPath.ToString().Replace(@"\", @"\\");
                GlobalVars.UserConfiguration.MapPath = GlobalPaths.BasePath + @"\\" + GlobalVars.UserConfiguration.MapPathSnip;
                SelectedMapLabel.Text = GlobalVars.UserConfiguration.Map;

                if (File.Exists(GlobalPaths.RootPath + @"\\" + Tree.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
                {
                    MapDescBox.Text = File.ReadAllText(GlobalPaths.RootPath + @"\\" + Tree.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
                }
                else
                {
                    MapDescBox.Text = Tree.SelectedNode.Text.ToString();
                }
            }
        }

        public void InstallRegServer()
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

                    GlobalFuncs.ConsolePrint("UserAgent Library successfully installed and registered!", 3, ConsoleBox);
                    MessageBox.Show("UserAgent Library successfully installed and registered!", "Novetus - Register UserAgent Library", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("ERROR - Failed to register. (" + ex.Message + ")", 2, ConsoleBox);
                    MessageBox.Show("Failed to register. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                GlobalFuncs.ConsolePrint("ERROR - Failed to register. (Did not run as Administrator)", 2, ConsoleBox);
                MessageBox.Show("Failed to register. (Error: Did not run as Administrator)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Helper Functions
        public void SearchNodes(string SearchText, TreeNode StartNode)
        {
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };

        }
        #endregion
    }
    #endregion
}
