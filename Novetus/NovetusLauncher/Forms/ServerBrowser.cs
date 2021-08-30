#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Server Browser
    public partial class ServerBrowser : Form
    {
        #region Private Variables
        List<VarStorage.GameServer> serverList = new List<VarStorage.GameServer>();
        private int selectedServer;
        #endregion

        #region Constructor
        public ServerBrowser()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private async void MasterServerRefreshButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MasterServerBox.Text))
            {
                try
                {
                    serverList.Clear();

                    await LoadServerInfoFromFile("http://" + MasterServerBox.Text + "/serverlist.txt");

                    ServerListView.BeginUpdate();
                    ServerListView.Clear();

                    if (serverList.Count > 0)
                    {
                        var ColumnName = new ColumnHeader();
                        ColumnName.Text = "Name";
                        ColumnName.TextAlign = HorizontalAlignment.Center;
                        ColumnName.Width = 284;
                        ServerListView.Columns.Add(ColumnName);

                        var ColumnClient = new ColumnHeader();
                        ColumnClient.Text = "Client";
                        ColumnClient.TextAlign = HorizontalAlignment.Center;
                        ColumnClient.Width = 75;
                        ServerListView.Columns.Add(ColumnClient);

                        foreach (var server in serverList)
                        {
                            var serverItem = new ListViewItem(server.ServerName);

                            var serverClient = new ListViewItem.ListViewSubItem(serverItem, server.ServerClient);
                            serverItem.SubItems.Add(serverClient);

                            ServerListView.Items.Add(serverItem);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There are no servers available on this master server.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ServerListView.EndUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load servers. (" + ex + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServerListView.Items.Count > 0 && ServerListView.Items[selectedServer] != null && serverList[selectedServer] != null)
                {
                    VarStorage.GameServer curServer = serverList[selectedServer];
                    if (ServerListView.Items[selectedServer].Text == curServer.ServerName)
                    {
                        string oldIP = GlobalVars.IP;
                        int oldPort = GlobalVars.JoinPort;
                        GlobalVars.IP = curServer.ServerIP;
                        GlobalVars.JoinPort = curServer.ServerPort;
                        GlobalFuncs.LaunchRBXClient(curServer.ServerClient, ScriptType.Client, false, true, null, null);
                        GlobalVars.IP = oldIP;
                        GlobalVars.JoinPort = oldPort;
                    }
                }
                else
                {
                    MessageBox.Show("Select a server before joining it.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ServerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ServerListView.SelectedIndices.Count <= 0)
                {
                    return;
                }
                int intselectedindex = ServerListView.SelectedIndices[0];
                if (intselectedindex >= 0)
                {
                    selectedServer = ServerListView.Items[intselectedindex].Index;
                }
            }
            catch (Exception)
            {

            }
        }

        private void ServerBrowser_Load(object sender, EventArgs e)
        {
            MasterServerBox.Text = GlobalVars.UserConfiguration.ServerBrowserServerAddress;
        }

        private void MasterServerBox_TextChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ServerBrowserServerAddress = MasterServerBox.Text;
        }
        #endregion

        #region Functions
        async Task LoadServerInfoFromFile(string url)
        {
            //https://stackoverflow.com/questions/2471209/how-to-read-a-file-from-internet#2471245
            //https://stackoverflow.com/questions/10826260/is-there-a-way-to-read-from-a-website-one-line-at-a-time
            //https://stackoverflow.com/questions/856885/httpwebrequest-to-url-with-dot-at-the-end
            MethodInfo getSyntax = typeof(UriParser).GetMethod("GetSyntax", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            FieldInfo flagsField = typeof(UriParser).GetField("m_Flags", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (getSyntax != null && flagsField != null)
            {
                foreach (string scheme in new[] { "http", "https" })
                {
                    UriParser parser = (UriParser)getSyntax.Invoke(null, new object[] { scheme });
                    if (parser != null)
                    {
                        int flagsValue = (int)flagsField.GetValue(parser);
                        // Clear the CanonicalizeAsFilePath attribute
                        if ((flagsValue & 0x1000000) != 0)
                            flagsField.SetValue(parser, flagsValue & ~0x1000000);
                    }
                }
            }

            WebClient client = new WebClient();
            Uri uri = new Uri(url);
            using (Stream stream = await client.OpenReadTaskAsync(uri))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        string DecodedLine = SecurityFuncs.Base64DecodeOld(line);
                        string[] serverInfo = DecodedLine.Split('|');
                        VarStorage.GameServer gameServer = new VarStorage.GameServer(serverInfo[0], serverInfo[1], serverInfo[2], serverInfo[3]);
                        serverList.Add(gameServer);
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}
