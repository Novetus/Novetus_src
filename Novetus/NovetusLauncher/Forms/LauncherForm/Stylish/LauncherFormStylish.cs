using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace NovetusLauncher
{
    public partial class LauncherFormStylish : Form
    {
        public LauncherFormStylish()
        {
            InitializeComponent();
        }

        private void LauncherFormStylish_Load(object sender, EventArgs e)
        {
            try
            {
                //splash loader
                CryptoRandom rand = new CryptoRandom();
                int randColor = rand.Next(0, 3);

                if (randColor == 0)
                {
                    splashLabel.BackColor = Color.FromArgb(245, 135, 13);
                }
                else if (randColor == 1)
                {
                    splashLabel.BackColor = Color.FromArgb(255, 3, 2);
                }
                else if (randColor == 2)
                {
                    splashLabel.BackColor = Color.FromArgb(238, 154, 181);
                }

                launcherFormStylishInterface1.mapsBox.BackColor = Color.FromArgb(237, 237, 237);

                launcherFormStylishInterface1.launcherForm.SplashLabel = splashLabel;
                launcherFormStylishInterface1.launcherForm.InitForm();

                if (File.Exists(GlobalPaths.RootPath + "\\changelog.txt"))
                {
                    launcherFormStylishInterface1.changelogBox.Text = File.ReadAllText(GlobalPaths.RootPath + "\\changelog.txt");
                }

                if (File.Exists(GlobalPaths.RootPath + "\\README-AND-CREDITS.TXT"))
                {
                    launcherFormStylishInterface1.readmeBox.Text = File.ReadAllText(GlobalPaths.RootPath + "\\README-AND-CREDITS.TXT");
                }

                launcherFormStylishInterface1.versionLabel.Content = Application.ProductVersion;
                launcherFormStylishInterface1.versionNovetusLabel.Content = GlobalVars.ProgramInformation.Version;

                if (launcherFormStylishInterface1.playTab != null && launcherFormStylishInterface1.playTab.IsSelected)
                {
                    if (launcherFormStylishInterface1.mapsBox.Nodes.Count == 0)
                    {
                        launcherFormStylishInterface1.launcherForm.RefreshMaps();
                        launcherFormStylishInterface1.LoadMapDesc();
                    }
                }

                ReadConfigValues(true);

                LocalVars.launcherInitState = false;
                CenterToScreen();
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
            }
        }

        void LauncherFormStylish_Close(object sender, CancelEventArgs e)
        {
            WriteConfigValues();

            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                DiscordRPC.Shutdown();
            }
            Application.Exit();
        }

        void splashLabel_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderSimple(e.Graphics, splashLabel.DisplayRectangle, Color.White, ButtonBorderStyle.Solid, 1);
        }

        void DrawBorderSimple(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style, int width)
        {
            //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            ControlPaint.DrawBorder(graphics, bounds, 
                color, width, style, 
                color, width, style, 
                color, width, style, 
                color, width, style);
        }

        public void ReadConfigValues(bool initial = false)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);

            //CloseOnLaunchCheckbox.Checked = GlobalVars.UserConfiguration.CloseOnLaunch;
            launcherFormStylishInterface1.userIDBox.Text = GlobalVars.UserConfiguration.UserID.ToString();
            launcherFormStylishInterface1.tripcodeLabel.Content = GlobalVars.UserConfiguration.PlayerTripcode.ToString();
            launcherFormStylishInterface1.maxPlayersBox.Text = GlobalVars.UserConfiguration.PlayerLimit.ToString();
            launcherFormStylishInterface1.userNameBox.Text = GlobalVars.UserConfiguration.PlayerName;
            //SelectedClientLabel.Text = GlobalVars.UserConfiguration.SelectedClient;
            //SelectedMapLabel.Text = GlobalVars.UserConfiguration.Map;
            //Tree.SelectedNode = TreeNodeHelper.SearchTreeView(GlobalVars.UserConfiguration.Map, Tree.Nodes);
            //Tree.Focus();
            launcherFormStylishInterface1.joinPortBox.Text = GlobalVars.JoinPort.ToString();
            launcherFormStylishInterface1.serverPortBox.Text = GlobalVars.UserConfiguration.RobloxPort.ToString();
            //DiscordPresenceCheckbox.Checked = GlobalVars.UserConfiguration.DiscordPresence;
            launcherFormStylishInterface1.uPnPBox.IsChecked = GlobalVars.UserConfiguration.UPnP;
            launcherFormStylishInterface1.NotifBox.IsChecked = GlobalVars.UserConfiguration.ShowServerNotifications;
            launcherFormStylishInterface1.browserNameBox.Text = GlobalVars.UserConfiguration.ServerBrowserServerName;
            launcherFormStylishInterface1.browserAddressBox.Text = GlobalVars.UserConfiguration.ServerBrowserServerAddress;

            ReadClientValues(initial);
        }

        public void WriteConfigValues(bool ShowBox = false)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            ReadClientValues();
            if (ShowBox)
            {
                MessageBox.Show("Config Saved!", "Novetus - Config Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void ReadClientValues(bool initial = false)
        {
            GlobalFuncs.ReadClientValues(null, initial);

            launcherFormStylishInterface1.userNameBox.IsEnabled = GlobalVars.SelectedClientInfo.UsesPlayerName;

            launcherFormStylishInterface1.userIDBox.IsEnabled = GlobalVars.SelectedClientInfo.UsesID;
            launcherFormStylishInterface1.regenerateIDButton.IsEnabled = GlobalVars.SelectedClientInfo.UsesID;

            if (!string.IsNullOrWhiteSpace(GlobalVars.SelectedClientInfo.Warning))
            {
                launcherFormStylishInterface1.clientWarningBox.Text = GlobalVars.SelectedClientInfo.Warning;
            }
            else
            {
                launcherFormStylishInterface1.clientWarningBox.Text = "";
            }

            launcherFormStylishInterface1.clientDescBox.Text = GlobalVars.SelectedClientInfo.Description;
            //SelectedClientLabel.Text = GlobalVars.UserConfiguration.SelectedClient;
        }
    }
}
