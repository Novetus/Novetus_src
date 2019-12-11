/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 5/14/2017
 * Time: 9:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Net;


namespace NovetusLauncher
{
	/// <summary>
	/// Description of ServerInfo.
	/// </summary>
	public partial class ServerInfo : Form
	{
		public ServerInfo()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ServerInfoLoad(object sender, EventArgs e)
		{
            string IP = SecurityFuncs.GetExternalIPAddress();
            string[] lines1 = {
                        SecurityFuncs.Base64Encode(IP),
                        SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.SelectedClient)
                    };
            string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1));
            string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.SelectedClient)
                    };
            string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2));
            string text = GlobalVars.MultiLine(
                       "Client: " + GlobalVars.SelectedClient,
                       "IP: " + IP,
                       "Port: " + GlobalVars.RobloxPort.ToString(),
                       "Map: " + GlobalVars.Map,
                       "Players: " + GlobalVars.PlayerLimit,
                       "Version: Novetus " + GlobalVars.Version,
                       "Online URI Link:",
                       URI,
                       "Local URI Link:",
                       URI2,
                       GlobalVars.IsWebServerOn == true ? "Web Server URL:" : "",
                       GlobalVars.IsWebServerOn == true ? "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString() : "",
                       GlobalVars.IsWebServerOn == true ? "Local Web Server URL:" : "",
                       GlobalVars.IsWebServerOn == true ? GlobalVars.LocalWebServerURI : ""
                   );
            textBox1.AppendText(GlobalVars.RemoveEmptyLines(text));
        			
		}
	}
}
