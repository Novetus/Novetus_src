/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 11:45 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of LoaderForm.
	/// </summary>
	public partial class LoaderForm : Form
	{
		public LoaderForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void LoaderFormLoad(object sender, EventArgs e)
		{
			string[] defaultclient = File.ReadAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\info.txt");
    		string defcl = defaultclient[1];
    		GlobalVars.DefaultClient = defcl;
			QuickConfigure main = new QuickConfigure();
			main.ShowDialog();
			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);			
		}
		
		void StartGame()
		{
			string ExtractedArg = GlobalVars.SharedArgs.Replace("novetus://", "").Replace("novetus", "").Replace(":", "").Replace("/", "").Replace("?", "");
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			string ip = SecurityFuncs.Base64Decode(SplitArg[0]);
			string port = SecurityFuncs.Base64Decode(SplitArg[1]);
			string client = SecurityFuncs.Base64Decode(SplitArg[2]);
			ReadClientValues(client);
			string luafile = "rbxasset://scripts\\\\CSMPFunctions.lua";
			string rbxexe = "";
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +  "\\clients\\" + client + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +  "\\clients\\" + client + @"\\RobloxApp_client.exe";
			}
			string quote = "\"";
			string args = "";
			string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
			if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
			{
				args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + ip + "'," + port + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
			}
			else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
			{
				args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + ip + "'," + port + ",'Player','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
			}
			else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
			{
				args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(0,'" + ip + "'," + port + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
			}
			else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false)
			{
				args = "-script " + quote + "dofile('" + luafile + "'); _G.CSConnect(0,'" + ip + "'," + port + ",'Player','" + GlobalVars.loadtext + ",'" + GlobalVars.SelectedClientMD5 + "','" + md5dir + "','" + GlobalVars.SelectedClientScriptMD5 + "');" + quote;
			}
			try
			{
				if (SecurityFuncs.checkClientMD5(client) == true)
				{
					if (SecurityFuncs.checkScriptMD5(client) == true)
					{
						Process.Start(rbxexe, args);
						this.Close();
					}
				}
				else
				{
					label1.Text = "The client has been detected as modified.";
				}
			}
			catch (Exception)
			{
				label1.Text = "The client has been detected as modified.";
			}
		}
		
		private void CheckIfFinished(object state)
    	{
			if (GlobalVars.ReadyToLaunch == false)
			{
				System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
			}
			else
			{
				label1.Text = "Launching Game...";
				StartGame();
			}
    	}
		
		void ReadConfigValues()
		{
			LauncherFuncs.ReadConfigValues(GlobalVars.BasePath + "\\config.txt");
		}
		
		void ReadClientValues(string ClientName)
		{
			string clientpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +  "\\clients\\" + ClientName + "\\clientinfo.txt";
			
			if (!File.Exists(clientpath))
			{
				MessageBox.Show("No clientinfo.txt detected with the client you chose. The client either cannot be loaded, or it is not available.","Novetus Launcher - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
				GlobalVars.SelectedClient = GlobalVars.DefaultClient;
			}
			
			LauncherFuncs.ReadClientValues(clientpath);
		}
	}
}
