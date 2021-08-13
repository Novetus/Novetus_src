#region Usings
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region LauncherForm - Compact
    public partial class LauncherFormCompact : Form
    {
        #region Constructor
        public LauncherFormCompact()
        {
            _fieldsTreeCache = new TreeView();
            InitializeComponent();
            InitCompactForm();
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

        void Button3Click_legacy(object sender, EventArgs e)
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
            launcherForm.ChangeName();
        }

        void ListBox2SelectedIndexChanged(object sender, EventArgs e)
        {
            launcherForm.ChangeClient();
        }

        void CheckBox3CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.LocalPlayMode = checkBox3.Checked;
        }

        void TextBox5TextChanged(object sender, EventArgs e)
        {
            launcherForm.ChangeUserID();
        }

        void Button8Click(object sender, EventArgs e)
        {
            CharacterCustomizationCompact ccustom = new CharacterCustomizationCompact();
            ccustom.Show();
        }

        void Button9Click(object sender, EventArgs e)
        {
            launcherForm.ResetConfigValues(true);
        }

        void ListBox3SelectedIndexChanged(object sender, EventArgs e)
        {
            launcherForm.SelectIPListing();
        }

        void ListBox4SelectedIndexChanged(object sender, EventArgs e)
        {
            launcherForm.SelectPortListing();
        }

        void Button10Click(object sender, EventArgs e)
        {
            launcherForm.AddIPPortListing(null, GlobalPaths.ConfigDir + "\\servers.txt", GlobalVars.IP);
        }

        void Button11Click(object sender, EventArgs e)
        {
            launcherForm.AddIPPortListing(null, GlobalPaths.ConfigDir + "\\ports.txt", GlobalVars.JoinPort);
        }

        void Button12Click(object sender, EventArgs e)
        {
            launcherForm.RemoveIPPortListing(listBox3, GlobalPaths.ConfigDir + "\\servers.txt", GlobalPaths.ConfigDir + "\\servers.tmp");
        }

        void Button13Click(object sender, EventArgs e)
        {
            launcherForm.RemoveIPPortListing(listBox4, GlobalPaths.ConfigDir + "\\ports.txt", GlobalPaths.ConfigDir + "\\ports.tmp");
        }

        void Button14Click(object sender, EventArgs e)
        {
            launcherForm.ResetIPPortListing(listBox3, GlobalPaths.ConfigDir + "\\servers.txt");
        }

        void Button15Click(object sender, EventArgs e)
        {
            launcherForm.ResetIPPortListing(listBox4, GlobalPaths.ConfigDir + "\\ports.txt");
        }

        void Button16Click(object sender, EventArgs e)
        {
            launcherForm.AddIPPortListing(listBox3, GlobalPaths.ConfigDir + "\\servers.txt", GlobalVars.IP);
        }

        void Button17Click(object sender, EventArgs e)
        {
            launcherForm.AddIPPortListing(listBox4, GlobalPaths.ConfigDir + "\\ports.txt", GlobalVars.JoinPort);
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
            launcherForm.ChangeJoinPort();
        }

        void NumericUpDown2ValueChanged(object sender, EventArgs e)
        {
            launcherForm.ChangeServerPort();
        }

        void NumericUpDown3ValueChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(numericUpDown3.Value);
        }

        void Button7Click(object sender, EventArgs e)
        {
            launcherForm.ResetCurPort(numericUpDown1, GlobalVars.JoinPort);
        }

        void Button22Click(object sender, EventArgs e)
        {
            launcherForm.ResetCurPort(numericUpDown2, GlobalVars.UserConfiguration.RobloxPort);
        }

        void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {
            launcherForm.SelectMap();
        }

        void Button6Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", GlobalPaths.MapsDir.Replace(@"\\", @"\"));
        }

        void CheckBox4CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.UPnP = checkBox4.Checked;
        }

        void CheckBox4Click(object sender, EventArgs e)
        {
            launcherForm.RestartLauncherAfterSetting(checkBox4, "Novetus - UPnP", "Make sure to check if your router has UPnP functionality enabled.\n" +
                "Please note that some routers may not support UPnP, and some ISPs will block the UPnP protocol.\nThis may not work for all users.");
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
            launcherForm.RestartLauncherAfterSetting(checkBox2, "Novetus - Discord Rich Presence", "Make sure the Discord app is open so this change can take effect.");
        }

        void SettingsButtonClick(object sender, EventArgs e)
        {
            LauncherFormCompactSettings im = new LauncherFormCompactSettings();
            im.FormClosing += SettingsExited;
            im.Show();
        }

        void SettingsExited(object sender, FormClosingEventArgs e)
        {
            GlobalFuncs.ReadClientValues(richTextBox1);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            launcherForm.LoadLauncher();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            launcherForm.EasterEggLogic();
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
            ServerBrowser browser = new ServerBrowser();
            browser.Show();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ShowServerNotifications = checkBox9.Checked;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ServerBrowserServerName = textBox7.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ServerBrowserServerAddress = textBox8.Text;
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            launcherForm.ShowMasterServerWarning();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            launcherForm.AddNewMap();
        }
        #endregion
    }
    #endregion
}
