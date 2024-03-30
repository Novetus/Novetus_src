#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
#endregion

namespace NovetusLauncher
{
    #region LauncherForm - Stylish - Interface
    public partial class LauncherFormStylishInterface : System.Windows.Controls.UserControl
    {
        #region Variables
        public LauncherFormShared launcherForm;
        private System.Windows.Forms.TreeView _fieldsTreeCache;
        public LauncherFormStylish FormParent;
        public bool HideMasterAddressWarning;
        #endregion

        #region Constructor
        public LauncherFormStylishInterface(LauncherFormStylish parent)
        {
            _fieldsTreeCache = new System.Windows.Forms.TreeView();
            InitializeComponent();
            FormParent = parent;
            InitStylishForm(FormParent);
        }

        private void InitStylishForm(Form parent)
        {
            launcherForm = new LauncherFormShared();
            launcherForm.Parent = parent;
            launcherForm.FormStyle = Settings.Style.Stylish;
            launcherForm.Tree = mapsBox;
            launcherForm._TreeCache = _fieldsTreeCache;
            HideMasterAddressWarning = false;
        }
        #endregion

        #region Form Events
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Source is System.Windows.Controls.TabControl)
                {
                    if (IsLoaded)
                    {
                        if (playTab != null && playTab.IsSelected)
                        {
                            mapsBox.Nodes.Clear();
                            _fieldsTreeCache.Nodes.Clear();
                            mapsDescBox.Text = "";

                            launcherForm.RefreshMaps();
                            LoadMapDesc();
                        }

                        if (clientTab != null && clientTab.IsSelected)
                        {
                            clientListBox.Items.Clear();
                            clientWarningBox.Text = "";
                            clientDescBox.Text = "";

                            string clientdir = GlobalPaths.ClientDir;
                            DirectoryInfo dinfo = new DirectoryInfo(clientdir);
                            DirectoryInfo[] Dirs = dinfo.GetDirectories();
                            foreach (DirectoryInfo dir in Dirs)
                            {
                                clientListBox.Items.Add(new ClientListItem() { ClientName = dir.Name });
                            }

                            foreach (object o in clientListBox.Items)
                            {
                                if ((o is ClientListItem) && (o as ClientListItem).ClientName.Contains(GlobalVars.UserConfiguration.ReadSetting("SelectedClient")))
                                {
                                    clientListBox.SelectedItem = o;
                                    break;
                                }
                            }
                        }

                        if (serverTab != null && serverTab.IsSelected)
                        {
                            serverInfoBox.Text = "";

                            string[] text = NovetusFuncs.LoadServerInformation();
                            foreach (string str in text)
                            {
                                if (!string.IsNullOrWhiteSpace(str))
                                {
                                    serverInfoBox.AppendText(str + Environment.NewLine);
                                }
                            }

                            serverInfoBox.Select(0, 0);
                            serverInfoBox.ScrollToLine(serverInfoBox.GetLineIndexFromCharacterIndex(serverInfoBox.SelectionStart));
                            serverInfoBox.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
            }

            e.Handled = true;
        }

        private void mapsBox_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (mapsBox.SelectedNode != null)
            {
                mapsBox.SelectedNode.BackColor = System.Drawing.SystemColors.Control;
                mapsBox.SelectedNode.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void mapsBox_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (mapsBox.SelectedNode != null)
            {
                mapsBox.SelectedNode.BackColor = System.Drawing.SystemColors.Highlight;
                mapsBox.SelectedNode.ForeColor = System.Drawing.SystemColors.HighlightText;
            }

            launcherForm.SelectMap();
            LoadMapDesc();

            launcherForm.RefreshStylishTitle();
        }

        private void clientListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeClient();
        }

        public void ChangeClient()
        {
            if (!IsLoaded)
                return;

            if (clientListBox.Items.Count == 0)
                return;

            string clientdir = GlobalPaths.ClientDir;
            DirectoryInfo dinfo = new DirectoryInfo(clientdir);
            DirectoryInfo[] Dirs = dinfo.GetDirectories();
            List<string> clientNameList = new List<string>();
            foreach (DirectoryInfo dir in Dirs)
            {
                clientNameList.Add(dir.Name);
            }

            if (clientListBox.Items.Count == (clientNameList.Count - 1))
                return;

            if (clientListBox.SelectedItem == null)
                return;

            string ourselectedclient = GlobalVars.UserConfiguration.ReadSetting("SelectedClient");
            ClientListItem cli = (ClientListItem)clientListBox.SelectedItem ?? null;

            GlobalVars.UserConfiguration.SaveSetting("SelectedClient", (cli != null) ? cli.ToString() : "");

            if (!string.IsNullOrWhiteSpace(ourselectedclient))
            {
                if (!ourselectedclient.Equals(GlobalVars.UserConfiguration.ReadSetting("SelectedClient")))
                {
                    FormParent.ReadClientValues(true);
                }
                else
                {
                    FormParent.ReadClientValues();
                }
            }
            else
            {
                return;
            }

            launcherForm.RefreshStylishTitle();

            Client.UpdateRichPresence(Client.GetStateForType(GlobalVars.GameOpened));

            FormCollection fc = System.Windows.Forms.Application.OpenForms;

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

        private void customizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            NovetusFuncs.LaunchCharacterCustomization();
        }

        private void joinButton_Click(object sender, RoutedEventArgs e)
        {
            launcherForm.StartGame(ScriptType.Client);
        }

        private void playSoloButton_Click(object sender, RoutedEventArgs e)
        {
            launcherForm.StartGame(ScriptType.Solo);
        }

        private void serverBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            ServerBrowser browser = new ServerBrowser();
            browser.Show();
        }

        private void StudioButton_Click(object sender, RoutedEventArgs e)
        {
            launcherForm.StartGame(ScriptType.Studio);
        }

        private void ServerButton_Click(object sender, RoutedEventArgs e)
        {
            launcherForm.StartGame(ScriptType.Server);
        }

        private void ServerOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/7929646/how-to-programmatically-select-a-tabitem-in-wpf-tabcontrol
            // 1 is host tab.
            Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 1));
        }

        private void regenerateIDButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.UserConfiguration.SaveSettingInt("UserID", NovetusFuncs.GeneratePlayerID());
            userIDBox.Text = GlobalVars.UserConfiguration.ReadSetting("UserID");
        }

        private void addMapButton_Click(object sender, RoutedEventArgs e)
        {
            launcherForm.AddNewMap();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            TreeNode node = launcherForm.SearchMapsInternal(searchBox.Text);

            if (node != null)
            {
                mapsBox.SelectedNode = node;
                mapsBox.SelectedNode.Expand();
                mapsBox.Select();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            launcherForm.RefreshMaps();
            LoadMapDesc();
        }

        private void versionLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoaded)
                return;

            launcherForm.EasterEggLogic();
        }

        private void userNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            GlobalVars.UserConfiguration.SaveSetting("PlayerName", userNameBox.Text);
            int autoNameID = launcherForm.GetSpecialNameID(GlobalVars.UserConfiguration.ReadSetting("PlayerName"));
            if (LocalVars.launcherInitState == false && autoNameID > 0)
            {
                userIDBox.Text = autoNameID.ToString();
            }
        }

        private void userIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            GlobalVars.UserConfiguration.SaveSettingInt("UserID", ConvertSafe.ToInt32Safe(userIDBox.Text));
        }

        private void ipAddressBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.CurrentServer.SetValues(ipAddressBox.Text);
        }

        private void serverPortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingInt("RobloxPort", ConvertSafe.ToInt32Safe(serverPortBox.Text));
        }

        private void maxPlayersBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingInt("PlayerLimit", ConvertSafe.ToInt32Safe(maxPlayersBox.Text));
        }

        private void uPnPBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingBool("UPnP", (bool)uPnPBox.IsChecked);
        }

        private void uPnPBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingBool("UPnP", (bool)uPnPBox.IsChecked);
        }

        private void NotifBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingBool("ShowServerNotifications", (bool)NotifBox.IsChecked);
        }

        private void NotifBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingBool("ShowServerNotifications", (bool)NotifBox.IsChecked);
        }

        private void browserNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSetting("ServerBrowserServerName", browserNameBox.Text);
        }

        private void browserAddressBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSetting("ServerBrowserServerAddress", browserAddressBox.Text);
        }

        private void discordRichPresenceBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingBool("DiscordRichPresence", (bool)discordRichPresenceBox.IsChecked);
        }

        private void discordRichPresenceBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.SaveSettingBool("DiscordRichPresence", (bool)discordRichPresenceBox.IsChecked);
        }

        private void minimizeOnLaunchBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
        }

        private void minimizeOnLaunchBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
        }

        private void resetConfigButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            FormParent.ResetConfigValues(true);
        }

        private void saveConfigButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            FormParent.WriteConfigValues(true);
        }

        private void novetusSDKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            launcherForm.LoadLauncher();
        }

        private void settingsButtons_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            launcherForm.LoadSettings();
        }

        private void modInstallerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            launcherForm.InstallAddon();
        }

        private void resetAssetCacheButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            launcherForm.ClearAssetCache();
        }

        private void styleBox_DropDownClosed(object sender, EventArgs e)
        {
            if (!IsLoaded)
                return;

            if (LocalVars.launcherInitState)
                return;

            if (GlobalVars.AdminMode)
            {
                DialogResult closeNovetus = System.Windows.Forms.MessageBox.Show("You are in Admin Mode.\nAre you sure you want to switch styles?", "Novetus - Admin Mode Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (closeNovetus == DialogResult.No)
                {
                    return;
                }
            }

            if (GlobalVars.GameOpened != ScriptType.None)
            {
                System.Windows.Forms.MessageBox.Show("You must close the currently open client before changing styles.", "Novetus - Client is Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            styleBox.Text = styleBox.SelectedItem.ToString();

            switch (styleBox.SelectedIndex)
            {
                case 0:
                    GlobalVars.UserConfiguration.SaveSettingInt("LauncherStyle", (int)Settings.Style.Extended);
                    launcherForm.RestartApp();
                    break;
                case 1:
                    GlobalVars.UserConfiguration.SaveSettingInt("LauncherStyle", (int)Settings.Style.Compact);
                    launcherForm.RestartApp();
                    break;
                default:
                    break;
            }
        }

        private void discordRichPresenceBox_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            launcherForm.RestartLauncherAfterSetting((bool)discordRichPresenceBox.IsChecked, "Novetus - Discord Rich Presence", "Make sure the Discord app is open so this change can take effect.");
        }

        private void uPnPBox_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            launcherForm.RestartLauncherAfterSetting((bool)uPnPBox.IsChecked, "Novetus - UPnP", "Make sure to check if your router has UPnP functionality enabled.\n" +
                "Please note that some routers may not support UPnP, and some ISPs will block the UPnP protocol.\nThis may not work for all users.");
        }

        private void browserAddressBox_Mouse(object sender, MouseButtonEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (!HideMasterAddressWarning)
            {
                DialogResult res = System.Windows.Forms.MessageBox.Show("Due to Novetus' open nature when it comes to hosting master servers, hosting on a public master server may leave your server (and potentially computer) open for security vulnerabilities." +
                    "\nTo protect yourself against this, host under a VPN, use a host name, or use a trustworthy master server that is hosted privately or an official server." +
                    "\n\nDo you trust the master server you're about to input in?", "Novetus - Master Server Security Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (res)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                    default:
                        browserAddressBox.Text = "";
                        break;
                }

                HideMasterAddressWarning = true;
            }
        }

        #endregion

        #region Functions
        public void LoadMapDesc()
        {
            if (mapsBox.SelectedNode == null)
                return;

            if (File.Exists(GlobalPaths.RootPath + @"\\" + mapsBox.SelectedNode.FullPath.Replace(".rbxl", "").Replace(".rbxlx", "").Replace(".bz2", "") + "_desc.txt"))
            {
                mapsDescBox.Text = mapsBox.SelectedNode.Text + ": " + File.ReadAllText(GlobalPaths.RootPath + @"\\" + mapsBox.SelectedNode.FullPath.Replace(".rbxl", "").Replace(".rbxlx", "").Replace(".bz2", "") + "_desc.txt");
            }
            else
            {
                mapsDescBox.Text = mapsBox.SelectedNode.Text;
            }
        }
        #endregion
    }
    #endregion
}
