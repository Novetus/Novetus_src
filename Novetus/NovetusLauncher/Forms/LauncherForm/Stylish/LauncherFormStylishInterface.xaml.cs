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
        private LauncherFormStylish FormParent;

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
            try
            {
                if (e.Source is System.Windows.Controls.TabControl)
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
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
            }

            e.Handled = true;
        }

        public void LoadMapDesc()
        {
            if (File.Exists(GlobalPaths.RootPath + @"\\" + mapsBox.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt"))
            {
                mapsDescBox.Text = File.ReadAllText(GlobalPaths.RootPath + @"\\" + mapsBox.SelectedNode.FullPath.ToString().Replace(".rbxl", "").Replace(".rbxlx", "") + "_desc.txt");
            }
            else
            {
                mapsDescBox.Text = mapsBox.SelectedNode.Text.ToString();
            }
        }

        private void mapsBox_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (mapsBox.SelectedNode != null)
            {
                mapsBox.SelectedNode.BackColor = System.Drawing.SystemColors.Control;
                mapsBox.SelectedNode.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void mapsBox_AfterSelect(object sender, TreeViewEventArgs e)
        {
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
    }

    public class ClientListItem
    {
        public string ClientName { get; set; }

        public override string ToString() { return ClientName; }
    }
}
