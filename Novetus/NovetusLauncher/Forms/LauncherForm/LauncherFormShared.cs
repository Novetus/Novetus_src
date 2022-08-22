#region Usings
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Special Names Definition
    public class SpecialName
    {
        public SpecialName(string text)
        {
            if (text.Contains('|'))
            {
                string[] subs = text.Split('|');
                NameText = subs[0];
                NameID = Convert.ToInt32(subs[1]);
            }
        }

        //text
        public string NameText { get; set; }
        //id
        public int NameID { get; set; }
    }
    #endregion

    #region LauncherForm - Shared
    public class LauncherFormShared
    {
        #region Variables
        public DiscordRPC.EventHandlers handlers;
        public List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        public int LastNodeIndex = 0;
        public string LastSearchText;
        public bool HideMasterAddressWarning;

        //CONTROLS
        public Form Parent = null;
        public Settings.Style FormStyle = Settings.Style.None;
        public RichTextBox ConsoleBox, ChangelogBox, ReadmeBox = null;
        public TabControl Tabs = null;
        public TabPage ConsolePage = null;
        public TextBox MapDescBox, ServerInfo, SearchBar, PlayerIDTextBox, PlayerNameTextBox, ClientDescriptionBox, IPBox,
            ServerBrowserNameBox, ServerBrowserAddressBox = null;
        public TreeView Tree, _TreeCache = null;
        public ListBox ServerBox, PortBox, ClientBox = null;
        public Label SplashLabel, ProductVersionLabel, NovetusVersionLabel, PlayerTripcodeLabel, IPLabel, PortLabel,
            SelectedClientLabel, SelectedMapLabel, ClientWarningLabel = null;
        public ComboBox StyleSelectorBox = null;
        public CheckBox CloseOnLaunchCheckbox, DiscordPresenceCheckbox, uPnPCheckBox, ShowServerNotifsCheckBox, LocalPlayCheckBox = null;
        public Button RegeneratePlayerIDButton = null;
        public NumericUpDown PlayerLimitBox, HostPortBox, JoinPortBox = null;
        public string TabPageHost, TabPageMaps, TabPageClients, TabPageSaved, OldIP = "";
        private ToolTip contextToolTip;
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
                    GlobalFuncs.LogExceptions(ex);
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
                    string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " opened on '" + IP + "' (" + protocol.ToString() + ")", 3, ConsoleBox);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.LogExceptions(ex);
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
                    string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " closed on '" + IP + "' (" + protocol.ToString() + ")", 3, ConsoleBox);
                }
                catch (Exception ex)
                {
                    GlobalFuncs.LogExceptions(ex);
                    GlobalFuncs.ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2, ConsoleBox);
                }
            }
        }

        public void DeviceFound(object sender, DeviceEventArgs args)
        {
            try
            {
                INatDevice device = args.Device;
                string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                GlobalFuncs.ConsolePrint("UPnP: Device '" + IP + "' registered.", 3, ConsoleBox);
                StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
                StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
                GlobalFuncs.ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2, ConsoleBox);
            }
        }

        public void DeviceLost(object sender, DeviceEventArgs args)
        {
            try
            {
                INatDevice device = args.Device;
                string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                GlobalFuncs.ConsolePrint("UPnP: Device '" + IP + "' disconnected.", 3, ConsoleBox);
                StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
                StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
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

                GlobalFuncs.UpdateRichPresence(GlobalFuncs.GetStateForType(GlobalVars.GameOpened), true);
            }
        }
        #endregion

        #region Form Event Functions
        public void InitForm()
        {
            GlobalFuncs.CreateInitialFileListIfNeededMulti();

            HideMasterAddressWarning = false;

            if (FormStyle != Settings.Style.Stylish)
            {
                Parent.Text = "Novetus " + GlobalVars.ProgramInformation.Version;
            }
            GlobalFuncs.ConsolePrint("Novetus version " + GlobalVars.ProgramInformation.Version + " loaded. Initializing config.", 4, ConsoleBox);
            GlobalFuncs.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 4, ConsoleBox);

            if (FormStyle != Settings.Style.Stylish)
            {
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

            if (FormStyle == Settings.Style.Stylish)
            {
                Parent.Text = "Novetus " + GlobalVars.ProgramInformation.Version + " [CLIENT: " + 
                    GlobalVars.UserConfiguration.SelectedClient + " | MAP: " + 
                    GlobalVars.UserConfiguration.Map + "]";
            }

            GlobalFuncs.CreateAssetCacheDirectories();

            SetupImportantData();

            Splash splash = SplashReader.GetSplash();

            SplashLabel.Text = splash.SplashText;

            if (!string.IsNullOrWhiteSpace(splash.SplashContext))
            {
                contextToolTip = new ToolTip();
                contextToolTip.ToolTipIcon = ToolTipIcon.Info;
                contextToolTip.ToolTipTitle = "Context";
                contextToolTip.SetToolTip(SplashLabel, splash.SplashContext);
            }

            if (FormStyle != Settings.Style.Stylish)
            {
                ProductVersionLabel.Text = Application.ProductVersion;
                NovetusVersionLabel.Text = GlobalVars.ProgramInformation.Version;
                
                ReadConfigValues(true);
            }

            InitUPnP();
            StartDiscord();

            if (FormStyle != Settings.Style.Stylish)
            {
                LocalVars.launcherInitState = false;
            }

            if (GlobalVars.RequestToOutputInfo)
            {
                GlobalFuncs.CreateTXT(ConsoleBox);
            }
        }

        public void CloseEvent(CancelEventArgs e)
        {
            if (GlobalVars.GameOpened != ScriptType.None)
            {
                switch (GlobalVars.GameOpened)
                {
                    case ScriptType.Server:
                        ShowCloseError("A server is open.", "Server", e);
                        break;
                    default:
                        ShowCloseError("A game is open.", "Game", e);
                        break;
                }
            }
            else
            {
                if (GlobalVars.AdminMode)
                {
                    DialogResult closeNovetus = MessageBox.Show("You are in Admin Mode.\nAre you sure you want to quit Novetus?", "Novetus - Admin Mode Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (closeNovetus == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        CloseEventInternal();
                    }
                }
                else
                {
                    CloseEventInternal();
                }
            }
        }

        public void ShowCloseError(string text, string title, CancelEventArgs e)
        {
            DialogResult closeNovetus = MessageBox.Show(text + "\nYou must close the game before closing Novetus.", "Novetus - " + title + " is Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (closeNovetus == DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        public void CloseEventInternal()
        {
            if (!GlobalVars.LocalPlayMode)
            {
                WriteConfigValues();
            }
            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                DiscordRPC.Shutdown();
            }
            if (GlobalVars.RequestToOutputInfo)
            {
                GlobalFuncs.FixedFileDelete(GlobalPaths.BasePath + "\\" + GlobalVars.ServerInfoFileName);
            }
            Application.Exit();
        }

        public static void LoadServerInformation(TextBox box)
        {
            string[] lines1 = {
                        SecurityFuncs.Base64Encode(!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP),
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
                       "IP: " + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP),
                       "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                       "Map: " + GlobalVars.UserConfiguration.Map,
                       "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                       "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                       "Online URI Link:",
                       URI,
                       "Local URI Link:",
                       URI2
                       };

            foreach (string str in text)
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    box.AppendText(str + Environment.NewLine);
                }
            }

            box.SelectionStart = 0;
            box.ScrollToCaret();
        }

        public void ChangeTabs()
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
                    LoadServerInformation(ServerInfo);
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
                    RefreshMaps();
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

        public void StartGame(ScriptType gameType, bool no3d = false, bool nomap = false, bool console = false)
        {
            if (!console)
            {
                if (gameType == ScriptType.Studio)
                {
                    DialogResult result = MessageBox.Show("If you want to test out your place, you will have to save your place in Novetus's map folder, then launch your place in Play Solo." +
                        "\n\nPress Yes to launch Studio with a map, or No to launch Studio without a map.", "Novetus - Launch Roblox Studio", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
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

                if (gameType == ScriptType.Server)
                {
                    if (FormStyle == Settings.Style.Stylish)
                    {
                        DialogResult result = MessageBox.Show("You have the option to launch your server with or without graphics. Launching the server without graphics enables better performance.\n" +
                            "However, launching the server with no graphics may cause some elements in later clients may be disabled, such as Dialog boxes. This feature may also make your server unstable.\n\n" +
                            "Press Yes to launch a server with graphics, or No to launch a Server in No3D Mode.", "Novetus - Launch Server", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        bool no3dLegacy = false;

                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.No:
                                no3dLegacy = true;
                                no3d = no3dLegacy;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (FormStyle != Settings.Style.Stylish && no3d)
                    {
                        DialogResult result = MessageBox.Show("Launching the server without graphics enables better performance.\n" +
                                "However, launching the server with no graphics may cause some elements in later clients may be disabled, such as Dialog boxes. " +
                                "This feature may also make your server unstable.",
                                "Novetus - No3D Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            default:
                                break;
                        }
                    }
                }
            }

            if (gameType == ScriptType.Client && GlobalVars.LocalPlayMode && FormStyle != Settings.Style.Stylish)
            {
                GeneratePlayerID();
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
                    GlobalFuncs.LaunchRBXClient(ScriptType.Server, no3d, false, new EventHandler(ServerExited), ConsoleBox);
                    break;
                case ScriptType.Solo:
                    GlobalFuncs.LaunchRBXClient(ScriptType.Solo, false, false, new EventHandler(SoloExited), ConsoleBox);
                    break;
                case ScriptType.Studio:
                    GlobalFuncs.LaunchRBXClient(ScriptType.Studio, false, nomap, new EventHandler(ClientExitedBase), ConsoleBox);
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
            if (!GlobalVars.LocalPlayMode && GlobalVars.GameOpened != ScriptType.Server)
            {
                GlobalVars.GameOpened = ScriptType.None;
            }
            ClientExitedBase(sender, e);
        }

        void SoloExited(object sender, EventArgs e)
        {
            if (GlobalVars.GameOpened != ScriptType.Studio)
            {
                GlobalVars.GameOpened = ScriptType.None;
            }
            ClientExitedBase(sender, e);
        }

        void ServerExited(object sender, EventArgs e)
        {
            GlobalVars.GameOpened = ScriptType.None;
            GlobalFuncs.PingMasterServer(false, "The server has removed itself from the master server list.", ConsoleBox);
            ClientExitedBase(sender, e);
        }

        void EasterEggExited(object sender, EventArgs e)
        {
            GlobalVars.GameOpened = ScriptType.None;
            SplashLabel.Text = LocalVars.prevsplash;
            if (GlobalVars.AdminMode)
            {
                LocalVars.Clicks = 0;
            }
            ClientExitedBase(sender, e);
        }

        void ClientExitedBase(object sender, EventArgs e)
        {
            GlobalFuncs.UpdateRichPresence(GlobalFuncs.GetStateForType(GlobalVars.GameOpened));

            if (GlobalVars.UserConfiguration.CloseOnLaunch)
            {
                Parent.Visible = true;
            }
        }

        // FINALLY. https://stackoverflow.com/questions/11530643/treeview-search
        public TreeNode SearchMapsInternal(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return null;
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
                    return selectedNode;
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
                    return selectedNode;
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
                MessageBox.Show("The map '" + searchText + "' cannot be found. Please try another term.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void SearchMaps()
        {
            TreeNode node = SearchMapsInternal(SearchBar.Text);

            if (node != null)
            {
                Tree.SelectedNode = node;
                Tree.SelectedNode.Expand();
                Tree.Select();
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
                    ConsoleBox.AppendText(Environment.NewLine, System.Drawing.Color.White);
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

        public void SwapToConsole(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemtilde)
            {
                Tabs.SelectedTab = ConsolePage;
                e.Handled = true;
            }
        }

        public void SetupImportantData()
        {
            CryptoRandom random = new CryptoRandom();
            string Name1 = SecurityFuncs.GenerateName(random.Next(4, 12));
            string Name2 = SecurityFuncs.GenerateName(random.Next(4, 12));
            LocalVars.important = Name1 + Name2;
            LocalVars.important2 = SecurityFuncs.Encipher(LocalVars.important, random.Next(2, 9));
        }

        public void ConsoleProcessCommands(string cmd)
        {
            switch (cmd)
            {
                case string server when server.Contains("server", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = server.Split(' ');

                        if (vals[1].Equals("3d", StringComparison.InvariantCultureIgnoreCase))
                        {
                            StartGame(ScriptType.Server, false, false, true);
                        }
                        else if (vals[1].Equals("no3d", StringComparison.InvariantCultureIgnoreCase))
                        {
                            StartGame(ScriptType.Server, true, false, true);
                        }
                        else
                        {
                            StartGame(ScriptType.Server, false, false, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalFuncs.LogExceptions(ex);
                        StartGame(ScriptType.Server, false, false, true);
                    }
                    break;
                case string client when string.Compare(client, "client", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Client);
                    break;
                case string solo when string.Compare(solo, "solo", true, CultureInfo.InvariantCulture) == 0:
                    StartGame(ScriptType.Solo);
                    break;
                case string studio when studio.Contains("studio", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = studio.Split(' ');

                        if (vals[1].Equals("map", StringComparison.InvariantCultureIgnoreCase))
                        {
                            StartGame(ScriptType.Studio, false, false, true);
                        }
                        else if (vals[1].Equals("nomap", StringComparison.InvariantCultureIgnoreCase))
                        {
                            StartGame(ScriptType.Studio, false, true, true);
                        }
                        else
                        {
                            StartGame(ScriptType.Studio, false, false, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalFuncs.LogExceptions(ex);
                        StartGame(ScriptType.Studio, false, false, true);
                    }
                    break;
                case string config when config.Contains("config", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = config.Split(' ');

                        if (vals[1].Equals("save", StringComparison.InvariantCultureIgnoreCase))
                        {
                            WriteConfigValues();
                        }
                        else if (vals[1].Equals("load", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ReadConfigValues();
                        }
                        else if (vals[1].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ResetConfigValues();
                        }
                        else
                        {
                            GlobalFuncs.ConsolePrint("Please specify 'save', 'load', or 'reset'.", 4, ConsoleBox);
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalFuncs.LogExceptions(ex);
                        GlobalFuncs.ConsolePrint("Please specify 'save', 'load', or 'reset'.", 4, ConsoleBox);
                    }
                    break;
                case string help when string.Compare(help, "help", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleHelp();
                    break;
                case string sdk when string.Compare(sdk, "sdk", true, CultureInfo.InvariantCulture) == 0:
                    LoadLauncher();
                    break;
                case string dlldelete when string.Compare(dlldelete, "dlldelete", true, CultureInfo.InvariantCulture) == 0:
                    if (GlobalVars.UserConfiguration.DisableReshadeDelete == true)
                    {
                        GlobalVars.UserConfiguration.DisableReshadeDelete = false;
                        GlobalFuncs.ConsolePrint("ReShade DLL deletion enabled.", 4, ConsoleBox);
                    }
                    else
                    {
                        GlobalVars.UserConfiguration.DisableReshadeDelete = true;
                        GlobalFuncs.ConsolePrint("ReShade DLL deletion disabled.", 4, ConsoleBox);
                    }
                    break;
                case string altserverip when altserverip.Contains("altserverip", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = altserverip.Split(' ');

                        if (vals[1].Equals("none", StringComparison.InvariantCultureIgnoreCase))
                        {
                            GlobalVars.UserConfiguration.AlternateServerIP = "";
                            GlobalFuncs.ConsolePrint("Alternate Server IP removed.", 4, ConsoleBox);
                        }
                        else
                        {
                            GlobalVars.UserConfiguration.AlternateServerIP = vals[1];
                            GlobalFuncs.ConsolePrint("Alternate Server IP set to " + vals[1], 4, ConsoleBox);
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalFuncs.LogExceptions(ex);
                        GlobalFuncs.ConsolePrint("Please specify the IP address you would like to set Novetus to.", 4, ConsoleBox);
                    }
                    break;
                case string important when string.Compare(important, LocalVars.important, true, CultureInfo.InvariantCulture) == 0:
                    GlobalVars.AdminMode = true;
                    GlobalFuncs.ConsolePrint("ADMIN MODE ENABLED.", 4, ConsoleBox);
                    GlobalFuncs.ConsolePrint("YOU ARE GOD.", 2, ConsoleBox);
                    break;
                case string decode when string.Compare(decode, "decode", true, CultureInfo.InvariantCulture) == 0:
                    Decoder de = new Decoder();
                    de.Show();
                    GlobalFuncs.ConsolePrint("???", 2, ConsoleBox);
                    break;
                default:
                    GlobalFuncs.ConsolePrint("Command is either not registered or valid", 2, ConsoleBox);
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
            GlobalFuncs.ConsolePrint("Help:", 3, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= client | Launches client with launcher settings", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= solo | Launches client in Play Solo mode with launcher settings", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= server 3d | Launches server with launcher settings", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= server no3d | Launches server in NoGraphics mode with launcher settings", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= studio map | Launches Roblox Studio with the selected map", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= studio nomap | Launches Roblox Studio without the selected map", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= sdk | Launches the Novetus SDK Launcher", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= dlldelete | Toggle the deletion of opengl32.dll when ReShade is off.", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= altserverip <IP> | Sets the alternate server IP for server info. Replace <IP> with your specified IP or specify 'none' to remove the current alternate server IP", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("---------", 1, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= config save | Saves the config file", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= config load | Reloads the config file", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint("= config reset | Resets the config file", 4, ConsoleBox, true);
            GlobalFuncs.ConsolePrint(LocalVars.important2, 0, ConsoleBox, true, true);
        }

        public void SwitchStyles()
        {
            if (LocalVars.launcherInitState)
                return;

            if (GlobalVars.AdminMode)
            {
                DialogResult closeNovetus = MessageBox.Show("You are in Admin Mode.\nAre you sure you want to switch styles?", "Novetus - Admin Mode Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (closeNovetus == DialogResult.No)
                {
                    return;
                }
            }

            if (GlobalVars.GameOpened != ScriptType.None)
            {
                MessageBox.Show("You must close the currently open client before changing styles.", "Novetus - Client is Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (StyleSelectorBox.SelectedIndex)
            {
                case 0:
                    if (FormStyle != Settings.Style.Extended)
                    {
                        GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Extended;
                        CloseEventInternal();
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        Application.Exit();
                    }
                    break;
                case 1:
                    if (FormStyle != Settings.Style.Compact)
                    {
                        GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Compact;
                        CloseEventInternal();
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        Application.Exit();
                    }
                    break;
                case 2:
                    if (FormStyle != Settings.Style.Stylish)
                    {
                        GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Stylish;
                        CloseEventInternal();
                        System.Diagnostics.Process.Start(Application.ExecutablePath);
                        Application.Exit();
                    }
                    break;
                default:
                    break;
            }
        }

        public void ReadConfigValues(bool initial = false)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);

            CloseOnLaunchCheckbox.Checked = GlobalVars.UserConfiguration.CloseOnLaunch;
            PlayerIDTextBox.Text = GlobalVars.UserConfiguration.UserID.ToString();
            PlayerTripcodeLabel.Text = GlobalVars.PlayerTripcode.ToString();
            PlayerLimitBox.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.PlayerLimit);
            PlayerNameTextBox.Text = GlobalVars.UserConfiguration.PlayerName;
            SelectedClientLabel.Text = GlobalVars.UserConfiguration.SelectedClient;
            SelectedMapLabel.Text = GlobalVars.UserConfiguration.Map;
            Tree.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, Tree.Nodes);
            Tree.Focus();
            JoinPortBox.Value = Convert.ToDecimal(GlobalVars.JoinPort);
            HostPortBox.Value = Convert.ToDecimal(GlobalVars.UserConfiguration.RobloxPort);
            IPLabel.Text = GlobalVars.IP;
            PortLabel.Text = GlobalVars.JoinPort.ToString();
            DiscordPresenceCheckbox.Checked = GlobalVars.UserConfiguration.DiscordPresence;
            uPnPCheckBox.Checked = GlobalVars.UserConfiguration.UPnP;
            ShowServerNotifsCheckBox.Checked = GlobalVars.UserConfiguration.ShowServerNotifications;
            ServerBrowserNameBox.Text = GlobalVars.UserConfiguration.ServerBrowserServerName;
            ServerBrowserAddressBox.Text = GlobalVars.UserConfiguration.ServerBrowserServerAddress;

            switch (GlobalVars.UserConfiguration.LauncherStyle)
            {
                case Settings.Style.Compact:
                    StyleSelectorBox.SelectedIndex = 1;
                    break;
                case Settings.Style.Extended:
                    StyleSelectorBox.SelectedIndex = 0;
                    break;
                case Settings.Style.Stylish:
                default:
                    StyleSelectorBox.SelectedIndex = 2;
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
                MessageBox.Show("Config Saved!", "Novetus - Config Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void WriteCustomizationValues()
        {
            GlobalFuncs.Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
            GlobalFuncs.ConsolePrint("Config Saved.", 3, ConsoleBox);
        }

        public void ResetConfigValues(bool ShowBox = false)
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

            GlobalFuncs.ResetConfigValues(FormStyle);
            WriteConfigValues();
            ReadConfigValues();
            if (ShowBox)
            {
                MessageBox.Show("Config Reset!", "Novetus - Config Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool GenerateIfInvalid()
        {
            string clientpath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\clientinfo.nov";

            if (!File.Exists(clientpath))
            {
                try
                {
                    MessageBox.Show("No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.\n\nNovetus will attempt to generate one.", "Novetus - Client Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GlobalFuncs.GenerateDefaultClientInfo(Path.GetDirectoryName(clientpath));
                }
                catch (Exception ex)
                {
                    GlobalFuncs.LogExceptions(ex);
                    MessageBox.Show("Failed to generate default clientinfo.nov. Info: " + ex.Message + "\n\nLoading default client '" + GlobalVars.ProgramInformation.DefaultClient + "'", "Novetus - Client Info Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
                    return false;
                }
            }

            return true;
        }

        public void ReadClientValues(bool initial = false)
        {
            //reset clients
            if (!GenerateIfInvalid())
            {
                if (Tabs.SelectedTab == Tabs.TabPages[TabPageClients])
                {
                    ClientBox.SelectedItem = GlobalVars.UserConfiguration.SelectedClient;
                }
            }

            GlobalFuncs.ReadClientValues(ConsoleBox, initial);

            PlayerNameTextBox.Enabled = GlobalVars.SelectedClientInfo.UsesPlayerName;

            PlayerIDTextBox.Enabled = GlobalVars.SelectedClientInfo.UsesID;
            RegeneratePlayerIDButton.Enabled = GlobalVars.SelectedClientInfo.UsesID;

            switch (GlobalVars.SelectedClientInfo.UsesID)
            {
                case true:
                    if (GlobalVars.IP.Equals("localhost"))
                    {
                        LocalPlayCheckBox.Enabled = true;
                    }
                    break;
                case false:
                    LocalPlayCheckBox.Enabled = false;
                    GlobalVars.LocalPlayMode = false;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(GlobalVars.SelectedClientInfo.Warning))
            {
                ClientWarningLabel.Text = GlobalVars.SelectedClientInfo.Warning;
            }
            else
            {
                ClientWarningLabel.Text = "";
            }

            ClientDescriptionBox.Text = GlobalVars.SelectedClientInfo.Description;
            SelectedClientLabel.Text = GlobalVars.UserConfiguration.SelectedClient;
        }

        public void GeneratePlayerID()
        {
            GlobalFuncs.GeneratePlayerID();
            PlayerIDTextBox.Text = Convert.ToString(GlobalVars.UserConfiguration.UserID);
        }

        public async void InstallAddon()
        {
            ModManager addon = new ModManager(ModManager.ModMode.ModInstallation, ConsoleBox);
            addon.setFileListDisplay(10);
            try
            {
                await addon.LoadMod();
                if (!string.IsNullOrWhiteSpace(addon.getOutcome()))
                {
                    GlobalFuncs.ConsolePrint("ModManager - " + addon.getOutcome(), 3, ConsoleBox);
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
                if (!string.IsNullOrWhiteSpace(addon.getOutcome()))
                {
                    GlobalFuncs.ConsolePrint("ModManager - " + addon.getOutcome(), 2, ConsoleBox);
                }
            }

            if (!string.IsNullOrWhiteSpace(addon.getOutcome()))
            {
                MessageBoxIcon boxicon = MessageBoxIcon.Information;

                if (addon.getOutcome().Contains("Error"))
                {
                    boxicon = MessageBoxIcon.Error;
                }

                MessageBox.Show(addon.getOutcome(), "Novetus - Mod Installed", MessageBoxButtons.OK, boxicon);
            }
        }

        public void ClearAssetCache()
        {
            if (Directory.Exists(GlobalPaths.AssetCacheDir))
            {
                Directory.Delete(GlobalPaths.AssetCacheDir, true);
                GlobalFuncs.CreateAssetCacheDirectories();
                GlobalFuncs.ConsolePrint("Asset cache cleared!", 3, ConsoleBox);
                MessageBox.Show("Asset cache cleared!", "Novetus - Asset Cache Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("There is no asset cache to clear.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshMaps()
        {
            GlobalFuncs.ResetMapIfNecessary();

            Tree.Nodes.Clear();
            _TreeCache.Nodes.Clear();
            string mapdir = GlobalPaths.MapsDir;
            string[] filePaths = GlobalFuncs.FindAllFiles(GlobalPaths.MapsDir);

            foreach (string path in filePaths)
            {
                GlobalFuncs.RenameFileWithInvalidChars(path);
            }

            string[] fileexts = new string[] { ".rbxl", ".rbxlx" };
            TreeNodeHelper.ListDirectory(Tree, mapdir, fileexts);
            TreeNodeHelper.CopyNodes(Tree.Nodes, _TreeCache.Nodes);
            Tree.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, Tree.Nodes);
            if (FormStyle == Settings.Style.Stylish)
            {
                Tree.SelectedNode.BackColor = SystemColors.Highlight;
                Tree.SelectedNode.ForeColor = SystemColors.HighlightText;
            }
            Tree.Focus();

            if (FormStyle != Settings.Style.Stylish)
            {
                LoadMapDesc();
            }
        }

        public void RestartLauncherAfterSetting(CheckBox box, string title, string subText)
        {
            RestartLauncherAfterSetting(box.Checked, title, subText);
        }

        public void RestartLauncherAfterSetting(bool check, string title, string subText)
        {
            if (GlobalVars.AdminMode)
            {
                DialogResult closeNovetus = MessageBox.Show("You are in Admin Mode.\nAre you sure you want to apply this setting?", "Novetus - Admin Mode Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (closeNovetus == DialogResult.No)
                {
                    return;
                }
            }

            if (GlobalVars.GameOpened != ScriptType.None)
            {
                MessageBox.Show("You must close the currently open client before this setting can be applied.", "Novetus - Client is Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (check)
            {
                case false:
                    MessageBox.Show("Novetus will now restart.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show("Novetus will now restart." + Environment.NewLine + subText, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            CloseEventInternal();
            Application.Restart();
        }

        public void SelectMap()
        {
            if (Tree.SelectedNode.Nodes.Count == 0)
            {
                GlobalVars.UserConfiguration.Map = Tree.SelectedNode.Text.ToString();
                GlobalVars.UserConfiguration.MapPathSnip = Tree.SelectedNode.FullPath.ToString().Replace(@"\", @"\\");
                GlobalVars.UserConfiguration.MapPath = GlobalPaths.BasePath + @"\\" + GlobalVars.UserConfiguration.MapPathSnip;

                if (FormStyle != Settings.Style.Stylish)
                {
                    SelectedMapLabel.Text = GlobalVars.UserConfiguration.Map;
                    LoadMapDesc();
                }
            }
        }

        private void LoadMapDesc()
        {
            if (Tree.SelectedNode == null)
                return;

            if (File.Exists(GlobalPaths.RootPath + @"\\" + Tree.SelectedNode.FullPath.Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
            {
                MapDescBox.Text = File.ReadAllText(GlobalPaths.RootPath + @"\\" + Tree.SelectedNode.FullPath.Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
            }
            else
            {
                MapDescBox.Text = Tree.SelectedNode.Text;
            }
        }

        public void AddIPPortListing(ListBox box, string file, object val)
        {
            File.AppendAllText(file, val + Environment.NewLine);

            if (box != null)
            {
                box.Items.Clear();
                string[] lines = File.ReadAllLines(file);
                box.Items.AddRange(lines);
            }
        }

        public void ResetIPPortListing(ListBox box, string file)
        {
            File.Create(file).Dispose();

            if (box != null)
            {
                box.Items.Clear();
                string[] lines = File.ReadAllLines(file);
                box.Items.AddRange(lines);
            }
        }

        public void RemoveIPPortListing(ListBox box, string file, string file_tmp)
        {
            if (box != null)
            {
                if (box.SelectedIndex >= 0)
                {
                    TextLineRemover.RemoveTextLines(new List<string> { box.SelectedItem.ToString() }, file, file_tmp);
                    box.Items.Clear();
                    string[] lines = File.ReadAllLines(file);
                    box.Items.AddRange(lines);
                }
            }
            else
            {
                //requires a ListBox.
                return;
            }
        }

        public void SelectIPListing()
        {
            GlobalVars.IP = ServerBox.SelectedItem.ToString();
            IPBox.Text = GlobalVars.IP;
            LocalPlayCheckBox.Enabled = false;
            GlobalVars.LocalPlayMode = false;
            IPLabel.Text = GlobalVars.IP;
        }

        public void SelectPortListing()
        {
            GlobalVars.JoinPort = Convert.ToInt32(PortBox.SelectedItem.ToString());
            JoinPortBox.Value = Convert.ToDecimal(GlobalVars.JoinPort);
        }

        public void ResetCurPort(NumericUpDown box, int value)
        {
            box.Value = Convert.ToDecimal(GlobalVars.DefaultRobloxPort);
            value = GlobalVars.DefaultRobloxPort;
        }

        public void ChangeJoinPort()
        {
            GlobalVars.JoinPort = Convert.ToInt32(JoinPortBox.Value);
            PortLabel.Text = GlobalVars.JoinPort.ToString();
        }

        public void ChangeServerPort()
        {
            GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(HostPortBox.Value);
        }

        public void ChangeClient()
        {
            if (ClientBox.Items.Count == 0)
                return;

            string clientdir = GlobalPaths.ClientDir;
            DirectoryInfo dinfo = new DirectoryInfo(clientdir);
            DirectoryInfo[] Dirs = dinfo.GetDirectories();
            List<string> clientNameList = new List<string>();
            foreach (DirectoryInfo dir in Dirs)
            {
                clientNameList.Add(dir.Name);
            }

            if (ClientBox.Items.Count == (clientNameList.Count - 1))
                return;

            if (ClientBox.SelectedItem == null)
                return;

            string ourselectedclient = GlobalVars.UserConfiguration.SelectedClient;

            if (GlobalVars.GameOpened != ScriptType.None && !ourselectedclient.Equals(ClientBox.SelectedItem.ToString()))
            {
                MessageBox.Show("You must close the currently open client before changing clients.", "Novetus - Client is Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GlobalVars.UserConfiguration.SelectedClient = ClientBox.SelectedItem.ToString();

            if (!string.IsNullOrWhiteSpace(ourselectedclient))
            {
                if (!ourselectedclient.Equals(GlobalVars.UserConfiguration.SelectedClient))
                {
                    ReadClientValues(true);
                }
                else
                {
                    ReadClientValues();
                }
            }
            else
            {
                return;
            }

            GlobalFuncs.UpdateRichPresence(GlobalFuncs.GetStateForType(GlobalVars.GameOpened));

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

        public int GetSpecialNameID(string text)
        {
            string[] names = File.ReadAllLines(GlobalPaths.ConfigDir + "\\names-special.txt");
            int returnname = 0;
            List<SpecialName> specialnames = new List<SpecialName>();

            foreach (var name in names)
            {
                specialnames.Add(new SpecialName(name));
            }

            foreach (var specialname in specialnames)
            {
                if (specialname.NameText.Equals(text, StringComparison.InvariantCultureIgnoreCase))
                {
                    returnname = specialname.NameID;
                    break;
                }
            }

            return returnname;
        }

        public void ChangeName()
        {
            GlobalVars.UserConfiguration.PlayerName = PlayerNameTextBox.Text;
            int autoNameID = GetSpecialNameID(GlobalVars.UserConfiguration.PlayerName);
            if (LocalVars.launcherInitState == false && autoNameID > 0)
            {
                PlayerIDTextBox.Text = autoNameID.ToString();
            }
        }

        public void ChangeUserID()
        {
            int parsedValue;
            if (int.TryParse(PlayerIDTextBox.Text, out parsedValue))
            {
                if (PlayerIDTextBox.Text.Equals(""))
                {
                    GlobalVars.UserConfiguration.UserID = 0;
                }
                else
                {
                    GlobalVars.UserConfiguration.UserID = Convert.ToInt32(PlayerIDTextBox.Text);
                }
            }
            else
            {
                GlobalVars.UserConfiguration.UserID = 0;
            }
        }

        public void ShowMasterServerWarning()
        {
            if (!HideMasterAddressWarning)
            {
                DialogResult res = MessageBox.Show("Due to Novetus' open nature when it comes to hosting master servers, hosting on a public master server may leave your server (and potentially computer) open for security vulnerabilities." +
                "\nTo protect yourself against this, host under a VPN, use a host name, or use a trustworthy master server that is hosted privately or an official server." +
                "\n\nDo you trust the master server you're about to input in?", "Novetus - Master Server Security Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (res)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                    default:
                        ServerBrowserAddressBox.Text = "localhost";
                        break;
                }

                HideMasterAddressWarning = true;
            }
        }

        public void AddNewMap()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Roblox Level (*.rbxl)|*.rbxl|Roblox Level (*.rbxlx)|*.rbxlx";
                ofd.FilterIndex = 1;
                ofd.Title = "Load Roblox map";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!Directory.Exists(GlobalPaths.MapsDirCustom))
                    {
                        Directory.CreateDirectory(GlobalPaths.MapsDirCustom);
                    }

                    string mapname = Path.GetFileName(ofd.FileName);
                    bool success = true;

                    try
                    {
                        GlobalFuncs.FixedFileCopy(ofd.FileName, GlobalPaths.MapsDirCustom + @"\\" + mapname, true, true);
                    }
                    catch (Exception ex)
                    {
                        GlobalFuncs.LogExceptions(ex);
                        MessageBox.Show("Novetus has experienced an error when adding your map file: " + ex.Message + "\n\nYour file has not been added. Please try again.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        success = false;
                    }
                    finally
                    {
                        if (success)
                        {
                            RefreshMaps();
                            MessageBox.Show("The map '" + mapname + "' was successfully added to Novetus! Look in the 'Custom' folder for it!" , "Novetus - Map Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        public void LoadSettings()
        {
            LauncherFormSettings im = new LauncherFormSettings();
            im.FormClosing += SettingsExited;
            im.Show();
        }

        void SettingsExited(object sender, FormClosingEventArgs e)
        {
            GlobalFuncs.ReadClientValues(ConsoleBox);
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
