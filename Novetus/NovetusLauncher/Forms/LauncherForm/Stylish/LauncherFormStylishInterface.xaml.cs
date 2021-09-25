using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovetusLauncher
{
    /// <summary>
    /// Interaction logic for LauncherFormStylishInterface.xaml
    /// </summary>
    /// 
    public partial class LauncherFormStylishInterface : System.Windows.Controls.UserControl
    {
        public LauncherFormShared launcherForm;
        private System.Windows.Forms.TreeView _fieldsTreeCache;
        public LauncherFormStylish FormParent;
        private bool hostPanelOpen;

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

            hostPanelOpen = true;
        }

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
                            launcherForm.RefreshMaps();
                            LoadMapDesc();
                            clientListBox.Items.Clear();
                            clientWarningBox.Text = "";
                            clientDescBox.Text = "";
                        }

                        if (clientTab != null && clientTab.IsSelected)
                        {
                            string clientdir = GlobalPaths.ClientDir;
                            DirectoryInfo dinfo = new DirectoryInfo(clientdir);
                            DirectoryInfo[] Dirs = dinfo.GetDirectories();
                            foreach (DirectoryInfo dir in Dirs)
                            {
                                clientListBox.Items.Add(new ClientListItem() { ClientName = dir.Name });
                            }

                            foreach (object o in clientListBox.Items)
                            {
                                if ((o is ClientListItem) && (o as ClientListItem).ClientName.Contains(GlobalVars.UserConfiguration.SelectedClient))
                                {
                                    clientListBox.SelectedItem = o;
                                    break;
                                }
                            }

                            mapsBox.Nodes.Clear();
                            _fieldsTreeCache.Nodes.Clear();
                            mapsDescBox.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
            }

            e.Handled = true;
        }

        public void LoadMapDesc()
        {
            if (File.Exists(GlobalPaths.RootPath + @"\\" + mapsBox.SelectedNode.FullPath.Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
            {
                mapsDescBox.Text = mapsBox.SelectedNode.Text + ": " + File.ReadAllText(GlobalPaths.RootPath + @"\\" + mapsBox.SelectedNode.FullPath.Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
            }
            else
            {
                mapsDescBox.Text = mapsBox.SelectedNode.Text;
            }
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

            FormParent.Text = "Novetus " + GlobalVars.ProgramInformation.Version + " [CLIENT: " +
                    GlobalVars.UserConfiguration.SelectedClient + " | MAP: " +
                    GlobalVars.UserConfiguration.Map + "]";
        }

        private void clientListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

            string ourselectedclient = GlobalVars.UserConfiguration.SelectedClient;

            ClientListItem cli = (ClientListItem)clientListBox.SelectedItem ?? null;
            GlobalVars.UserConfiguration.SelectedClient = (cli != null) ? cli.ToString() : "";

            if (!string.IsNullOrWhiteSpace(ourselectedclient))
            {
                if (!ourselectedclient.Equals(GlobalVars.UserConfiguration.SelectedClient))
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

            FormParent.Text = "Novetus " + GlobalVars.ProgramInformation.Version + " [CLIENT: " +
                    GlobalVars.UserConfiguration.SelectedClient + " | MAP: " +
                    GlobalVars.UserConfiguration.Map + "]";

            GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");

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

            GlobalFuncs.LaunchCharacterCustomization();
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

        private void serverInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void regenerateIDButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalFuncs.GeneratePlayerID();
            userIDBox.Text = Convert.ToString(GlobalVars.UserConfiguration.UserID);
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

            GlobalVars.UserConfiguration.PlayerName = userNameBox.Text;
            int autoNameID = launcherForm.GetSpecialNameID(GlobalVars.UserConfiguration.PlayerName);
            if (LocalVars.launcherInitState == false && autoNameID > 0)
            {
                userIDBox.Text = autoNameID.ToString();
            }
        }

        private void userIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            int parsedValue;
            if (int.TryParse(userIDBox.Text, out parsedValue))
            {
                if (userIDBox.Text.Equals(""))
                {
                    GlobalVars.UserConfiguration.UserID = 0;
                }
                else
                {
                    GlobalVars.UserConfiguration.UserID = Convert.ToInt32(userIDBox.Text);
                }
            }
            else
            {
                GlobalVars.UserConfiguration.UserID = 0;
            }
        }

        private void ipAddressBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.IP = ipAddressBox.Text;
        }

        private void joinPortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.JoinPort = Convert.ToInt32(joinPortBox.Text);
        }

        private void serverPortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(serverPortBox.Text);
        }

        private void maxPlayersBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(maxPlayersBox.Text);
        }

        private void uPnPBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.UPnP = (bool)uPnPBox.IsChecked;
        }

        private void uPnPBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.UPnP = (bool)uPnPBox.IsChecked;
        }

        private void NotifBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.ShowServerNotifications = (bool)NotifBox.IsChecked;
        }

        private void NotifBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.ShowServerNotifications = (bool)NotifBox.IsChecked;
        }

        private void browserNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.ServerBrowserServerName = browserNameBox.Text;
        }

        private void browserAddressBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.ServerBrowserServerAddress = browserAddressBox.Text;
        }

        private void discordRichPresenceBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.DiscordPresence = (bool)discordRichPresenceBox.IsChecked;
        }

        private void discordRichPresenceBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;
            GlobalVars.UserConfiguration.DiscordPresence = (bool)discordRichPresenceBox.IsChecked;
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

            styleBox.Text = styleBox.SelectedItem.ToString();

            switch (styleBox.SelectedIndex)
            {
                case 0:
                    GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Extended;
                    FormParent.CloseEvent();
                    System.Windows.Forms.Application.Restart();
                    break;
                case 1:
                    GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Compact;
                    FormParent.CloseEvent();
                    System.Windows.Forms.Application.Restart();
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

        private void serverOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleServerOptions();
        }

        public void ToggleServerOptions()
        {
            if (!hostPanelOpen)
            {
                hostBox.Visibility = Visibility.Visible;
                mapsLabelBox.Width = 352;
                mapsLabelBox.Margin = new Thickness(95, 10, 0, 0);
                mapsGroupBox.Width = 352;
                mapsGroupBox.Margin = new Thickness(95, 10, 0, 0);
                formHost.Width = 190;
                formHost.Margin = new Thickness(103.166, 64, 154, 76);
                searchBox.Width = 207;
                searchBox.Margin = new Thickness(103.166, 42, 0, 0);
                mapsLabel.Margin = new Thickness(253.166, 9, 0, 0);
                joinButton.Margin = new Thickness(122, 191, 0, 0);
                serverBrowserButton.Margin = new Thickness(100, 225, 0, 0);
                playSoloButton.Margin = new Thickness(218, 191, 0, 0);

                hostPanelOpen = true;
            }
            else
            {
                hostBox.Visibility = Visibility.Hidden;
                mapsLabelBox.Width = 509;
                mapsLabelBox.Margin = new Thickness(-62, 10, 0, 0);
                mapsGroupBox.Width = 509;
                mapsGroupBox.Margin = new Thickness(-62, 10, 0, 0);
                formHost.Width = 348;
                formHost.Margin = new Thickness(-55, 64, 154, 76);
                searchBox.Width = 365;
                searchBox.Margin = new Thickness(-55, 42, 0, 0);
                mapsLabel.Margin = new Thickness(155, 9, 0, 0);
                joinButton.Margin = new Thickness(32, 191, 0, 0);
                serverBrowserButton.Margin = new Thickness(10, 225, 0, 0);
                playSoloButton.Margin = new Thickness(128, 191, 0, 0);

                hostPanelOpen = false;
            }
        }
    }

    //i hate these classes......
    public class ClientListItem
    {
        public string ClientName { get; set; }

        public override string ToString() { return ClientName; }
    }

    public class StyleListItem
    {
        public string StyleName { get; set; }

        public override string ToString() { return StyleName; }
    }
}
