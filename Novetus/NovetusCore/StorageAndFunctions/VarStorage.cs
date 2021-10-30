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

    #region Game Server Definition
    public class GameServer
    {
        public GameServer(string name, string ip, string port, string client)
        {
            ServerName = SecurityFuncs.Base64DecodeOld(name);
            ServerIP = SecurityFuncs.Base64DecodeOld(ip);
            ServerPort = Convert.ToInt32(SecurityFuncs.Base64DecodeOld(port));
            ServerClient = SecurityFuncs.Base64DecodeOld(client);
        }

        public bool IsValid()
        {
            if (!string.IsNullOrWhiteSpace(ServerName) &&
                !string.IsNullOrWhiteSpace(ServerClient) &&
                !string.IsNullOrWhiteSpace(ServerIP) &&
                !string.IsNullOrWhiteSpace(ServerPort.ToString()) &&
                GlobalFuncs.IsClientValid(ServerClient) &&
                (!ServerIP.Equals("localhost") || !ServerIP.Equals("127.0.0.1")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ServerName { get; set; }
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public string ServerClient { get; set; }
    }
    #endregion
}
#endregion
