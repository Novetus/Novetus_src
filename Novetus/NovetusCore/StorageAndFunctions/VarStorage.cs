#region Usings
using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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
            ServerStatus = PingServer(ServerIP, ServerPort);
        }

        public bool IsValid()
        {
            if (!string.IsNullOrWhiteSpace(ServerName) &&
                !string.IsNullOrWhiteSpace(ServerClient) &&
                !string.IsNullOrWhiteSpace(ServerIP) &&
                !string.IsNullOrWhiteSpace(ServerPort.ToString()) &&
                GlobalFuncs.IsClientValid(ServerClient) &&
                (!ServerIP.Equals("localhost") || !ServerIP.Equals("127.0.0.1")) &&
                 !GetStatusString().Equals("Offline"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Modified from https://stackoverflow.com/questions/22903861/how-to-check-remote-ip-and-port-is-available
        public static bool PingServer(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new UdpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
#if URI || LAUNCHER || CMD
                GlobalFuncs.LogExceptions(ex);
#endif
                return false;
            }
        }

        public string GetStatusString()
        {
            return (ServerStatus ? "Online" : "Offline");
        }

        public string ServerName { get; set; }
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public string ServerClient { get; set; }
        public bool ServerStatus { get; set; }
    }
    #endregion
}
#endregion
