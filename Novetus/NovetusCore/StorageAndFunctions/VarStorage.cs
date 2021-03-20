#region Usings
using System;
using System.Drawing;
#endregion

#region Variable Storage
public class VarStorage
{
    #region Asset Cache Definition
    public class AssetCacheDef
    {
        public AssetCacheDef(string clas, string[] id, string[] ext,
            string[] dir, string[] gamedir)
        {
            Class = clas;
            Id = id;
            Ext = ext;
            Dir = dir;
            GameDir = gamedir;
        }

        public string Class { get; set; }
        public string[] Id { get; set; }
        public string[] Ext { get; set; }
        public string[] Dir { get; set; }
        public string[] GameDir { get; set; }
    }
    #endregion

    #region Part Colors
    public class PartColors
    {
        public int ColorID { get; set; }
        public Color ButtonColor { get; set; }
    }

    #endregion

    #region Game Server Definition
    public class GameServer
    {
        public GameServer(string name, string ip, string port, string client, string players, string maxPlayers)
        {
            ServerName = name;
            ServerIP = ip;
            ServerPort = Convert.ToInt32(port);
            ServerClient = client;
            ServerPlayers = Convert.ToInt32(players);
            ServerMaxPlayers = Convert.ToInt32(maxPlayers);
        }

        public string ServerName { get; set; }
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public string ServerClient { get; set; }
        public int ServerPlayers { get; set; }
        public int ServerMaxPlayers { get; set; }
    }
    #endregion
}
#endregion
