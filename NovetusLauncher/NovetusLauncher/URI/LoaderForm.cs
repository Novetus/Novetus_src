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


namespace NovetusLauncher
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
			string[] lines = File.ReadAllLines(GlobalVars.ConfigDir + "\\info.txt");
            GlobalVars.Version = lines[0];
            GlobalVars.DefaultClient = lines[1];
    		GlobalVars.DefaultMap = lines[2];
    		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
            GlobalVars.Map = GlobalVars.DefaultMap;
			QuickConfigure main = new QuickConfigure();
			main.ShowDialog();
			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);			
		}

        void StartDiscord()
        {
            handlers = new DiscordRpc.EventHandlers();
            handlers.readyCallback = ReadyCallback;
            handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;
            handlers.joinCallback += JoinCallback;
            handlers.spectateCallback += SpectateCallback;
            handlers.requestCallback += RequestCallback;
            DiscordRpc.Initialize(GlobalVars.appid, ref handlers, true, "");

            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.LoadingURI, true);
        }

        void StartGame()
		{
			string ExtractedArg = GlobalVars.SharedArgs.Replace("novetus://", "").Replace("novetus", "").Replace(":", "").Replace("/", "").Replace("?", "");
			string ConvertedArg = SecurityFuncs.Base64Decode(ExtractedArg);
			string[] SplitArg = ConvertedArg.Split('|');
			string ip = SecurityFuncs.Base64Decode(SplitArg[0]);
			string port = SecurityFuncs.Base64Decode(SplitArg[1]);
			string client = SecurityFuncs.Base64Decode(SplitArg[2]);
            GlobalVars.SelectedClient = client;
            GlobalVars.IP = ip;
			GlobalVars.RobloxPort = Convert.ToInt32(port);
			ReadClientValues(client);
			string luafile = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
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
			if (GlobalVars.CustomArgs.Equals("%args%"))
			{
				if (!GlobalVars.FixScriptMapMode)
				{
					args = "-script " + quote + "dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptGenerator.ScriptType.Client, client) + quote;
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Client, client);
					args = "-script " + quote + luafile + quote;
				}
			}
			else
			{
				args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<client>", "</client>", "", luafile, rbxexe);
			}
			try
			{
				if (GlobalVars.AdminMode != true)
				{
					if (GlobalVars.AlreadyHasSecurity != true)
					{
						if (SecurityFuncs.checkClientMD5(client) == true)
						{
							if (SecurityFuncs.checkScriptMD5(client) == true)
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
			catch (Exception) when (!Env.Debugging)
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
            SecurityFuncs.RenameWindow(clientproc, ScriptGenerator.ScriptType.Client);
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InMPGame);
            this.Visible = false;
		}
		
		void ClientExited(object sender, EventArgs e)
		{
            LauncherFuncs.UpdateRichPresence(LauncherFuncs.LauncherState.InLauncher);
            this.Close();
		}
		
		private void CheckIfFinished(object state)
    	{
			if (GlobalVars.ReadyToLaunch == false)
			{
				System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
			}
			else
			{
                if (GlobalVars.DiscordPresence)
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
			string clientpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +  "\\clients\\" + ClientName + "\\clientinfo.nov";
			
			if (!File.Exists(clientpath))
			{
				MessageBox.Show("No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.","Novetus Launcher - Error while loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
				GlobalVars.SelectedClient = GlobalVars.DefaultClient;
			}
			
			LauncherFuncs.ReadClientValues(clientpath);
		}
	}
}
