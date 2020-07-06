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
using System.Windows.Forms;
using System.Reflection;

namespace NovetusURI
{
	/// <summary>
	/// Description of LoaderForm.
	/// </summary>
	public partial class LoaderForm : Form
	{
        DiscordRpc.EventHandlers handlers;

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

        public void ReadyCallback()
        {
        }

        public void DisconnectedCallback(int errorCode, string message)
        {
        }

        public void ErrorCallback(int errorCode, string message)
        {
        }

        public void JoinCallback(string secret)
        {
        }

        public void SpectateCallback(string secret)
        {
        }

        public void RequestCallback(DiscordRpc.JoinRequest request)
        {
        }

        void LoaderFormLoad(object sender, EventArgs e)
		{
			QuickConfigure main = new QuickConfigure();
			main.ShowDialog();
			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);			
		}

        void StartDiscord()
        {
            if (GlobalVars.UserConfiguration.DiscordPresence)
            {
                handlers = new DiscordRpc.EventHandlers();
                handlers.readyCallback = ReadyCallback;
                handlers.disconnectedCallback += DisconnectedCallback;
                handlers.errorCallback += ErrorCallback;
                handlers.joinCallback += JoinCallback;
                handlers.spectateCallback += SpectateCallback;
                handlers.requestCallback += RequestCallback;
                DiscordRpc.Initialize(GlobalVars.appid, ref handlers, true, "");

                LauncherFuncs.UpdateRichPresence(LauncherState.LoadingURI, "", true);
            }
        }

        void StartGame()
		{
			string ExtractedArg = GlobalVars.SharedArgs.Replace("novetus://", "").Replace("novetus", "").Replace(":", "").Replace("/", "").Replace("?", "");
			string ConvertedArg = SecurityFuncs.Base64DecodeOld(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			string ip = SecurityFuncs.Base64Decode(SplitArg[0]);
			string port = SecurityFuncs.Base64Decode(SplitArg[1]);
			string client = SecurityFuncs.Base64Decode(SplitArg[2]);
            GlobalVars.UserConfiguration.SelectedClient = client;
            GlobalVars.IP = ip;
			GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(port);
			ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
			string luafile = "";
			if (!GlobalVars.SelectedClientInfo.Fix2007)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
			string rbxexe = "";
			if (GlobalVars.SelectedClientInfo.LegacyMode == true)
			{
				rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_client.exe";
			}
			string quote = "\"";
			string args = "";
			if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
				if (!GlobalVars.SelectedClientInfo.Fix2007)
				{
					args = "-script " + quote + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.Client) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.Client);
					args = "-script " + quote + luafile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<client>", "</client>", "", luafile, rbxexe);
			}
			try
			{
				if (GlobalVars.AdminMode != true)
				{
					if (GlobalVars.SelectedClientInfo.AlreadyHasSecurity != true)
					{
						if (SecurityFuncs.checkClientMD5(GlobalVars.UserConfiguration.SelectedClient) == true)
						{
							if (SecurityFuncs.checkScriptMD5(GlobalVars.UserConfiguration.SelectedClient) == true)
							{
								LaunchClient(rbxexe,args);
							}
							else
							{
								label1.Text = "The client has been detected as modified.";
							}
						}
						else
						{
							label1.Text = "The client has been detected as modified.";
						}
					}
					else
					{
						LaunchClient(rbxexe,args);
					}
				}
				else
				{
					LaunchClient(rbxexe,args);
				}
			}
			catch (Exception)
            {
				label1.Text = "The client has been detected as modified.";
			}
		}
		
		private void LaunchClient(string rbxexe, string args)
		{
			Process clientproc = new Process();
			clientproc.StartInfo.FileName = rbxexe;
			clientproc.StartInfo.Arguments = args;
			clientproc.EnableRaisingEvents = true;
            clientproc.Exited += new EventHandler(ClientExited);
			clientproc.Start();
            clientproc.PriorityClass = ProcessPriorityClass.RealTime;
            SecurityFuncs.RenameWindow(clientproc, ScriptType.Client, "");
            LauncherFuncs.UpdateRichPresence(LauncherState.InMPGame, "");
            this.Visible = false;
		}
		
		void ClientExited(object sender, EventArgs e)
		{
            LauncherFuncs.UpdateRichPresence(LauncherState.InLauncher, "");
            this.Close();
		}
		
		private void CheckIfFinished(object state)
    	{
			if (LocalVars.ReadyToLaunch == false)
			{
				System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
			}
			else
			{
				Visible = true;
                if (GlobalVars.UserConfiguration.DiscordPresence)
                {
                    label1.Text = "Starting Discord Rich Presence...";
                    StartDiscord();
                }
				label1.Text = "Launching Game...";
				StartGame();
			}
    	}

		void ReadClientValues(string ClientName)
		{
			string clientpath = Directories.ClientDir + @"\\" + ClientName + @"\\clientinfo.nov";

			if (!File.Exists(clientpath))
			{
				MessageBox.Show("No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", "Novetus Launcher - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
				GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
				ReadClientValues(ClientName);
			}
			else
			{
				LauncherFuncs.ReadClientValues(clientpath);
			}
		}
	}
}
