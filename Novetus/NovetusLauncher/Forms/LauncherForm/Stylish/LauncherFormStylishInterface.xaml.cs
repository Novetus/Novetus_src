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
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            try
            {
                if (e.Source is TabItem)
                {
                    if (e.Source == playTab && playTab.IsSelected)
                    {
                        launcherForm.RefreshMaps();
                        LoadMapDesc();
                        clientListBox.Items.Clear();
                        clientWarningBox.Text = "";
                        clientDescBox.Text = "";
                    }
                    else if (e.Source == clientTab && clientTab.IsSelected)
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
                    else
                    {
                        clientListBox.Items.Clear();
                        clientWarningBox.Text = "";
                        clientDescBox.Text = "";
                        mapsBox.Nodes.Clear();
                        _fieldsTreeCache.Nodes.Clear();
                        mapsDescBox.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
            }
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

            CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
            ccustom.Show();
        }

        private void joinButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void playSoloButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void serverBrowserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StudioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ServerButton_Click(object sender, RoutedEventArgs e)
        {

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
    }

    public class ClientListItem
    {
        public string ClientName { get; set; }

        public override string ToString() { return ClientName; }
    }
}
