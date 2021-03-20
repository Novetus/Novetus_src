using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace NovetusLauncher
{
    public partial class ServerBrowser : Form
    {
        List<VarStorage.GameServer> serverList = new List<VarStorage.GameServer>();
        private int selectedServer;

        public ServerBrowser()
        {
            InitializeComponent();
        }

        private void MasterServerRefreshButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MasterServerBox.Text))
            {
                try
                {
                    serverList.Clear();

                    //https://stackoverflow.com/questions/2471209/how-to-read-a-file-from-internet#2471245
                    //https://stackoverflow.com/questions/10826260/is-there-a-way-to-read-from-a-website-one-line-at-a-time
                    WebClient client = new WebClient();
                    using (Stream stream = client.OpenRead("http://" + MasterServerBox.Text + "/serverlist.txt"))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] serverInfo = line.Split('|');
                                VarStorage.GameServer gameServer = new VarStorage.GameServer(serverInfo[0], serverInfo[1], serverInfo[2], serverInfo[3], serverInfo[4], serverInfo[5]);
                                serverList.Add(gameServer);
                            }
                        }
                    }

                    ServerListView.BeginUpdate();
                    ServerListView.Clear();

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

                    var ColumnPlayers = new ColumnHeader();
                    ColumnPlayers.Text = "Player Count";
                    ColumnPlayers.TextAlign = HorizontalAlignment.Center;
                    ColumnPlayers.Width = 81;
                    ServerListView.Columns.Add(ColumnPlayers);

                    foreach (var server in serverList)
                    {
                        var serverItem = new ListViewItem(server.ServerName);

                        var serverClient = new ListViewSubItem(serverItem, server.ServerClient);
                        serverItem.SubItems.Add(serverClient);

                        var serverPlayers = new ListViewSubItem(serverItem, server.ServerPlayers.ToString() + "/" + server.ServerMaxPlayers.ToString());
                        serverItem.SubItems.Add(serverPlayers);

                        ServerListView.Items.Add(serverItem);
                    }
                    ServerListView.EndUpdate();
                }
                catch (Exception)
                {

                }
            }
            else
            {

            }
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (serverList[selectedServer] != null)
                {
                    MessageBox.Show(ServerListView.Items[selectedServer].Text + " = " + serverList[selectedServer].ServerName);
                }
                else
                {
                    MessageBox.Show("broke");
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
    }
}
