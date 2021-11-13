#region Usings
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using NLog;
#endregion

namespace NovetusURI
{
    #region URI Loader
    public partial class LoaderForm : Form
	{
        #region Private Variables
        private DiscordRPC.EventHandlers handlers;
        #endregion

        #region Discord
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

		public void RequestCallback(DiscordRPC.JoinRequest request)
		{
		}

		void StartDiscord()
		{
			if (GlobalVars.UserConfiguration.DiscordPresence)
			{
				handlers = new DiscordRPC.EventHandlers();
				handlers.readyCallback = ReadyCallback;
				handlers.disconnectedCallback += DisconnectedCallback;
				handlers.errorCallback += ErrorCallback;
				handlers.joinCallback += JoinCallback;
				handlers.spectateCallback += SpectateCallback;
				handlers.requestCallback += RequestCallback;
				DiscordRPC.Initialize(GlobalVars.appid, ref handlers, true, "");

				GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.LoadingURI, true);
			}
		}
        #endregion

        #region Constructor
        public LoaderForm()
		{
			InitializeComponent();
		}
        #endregion

        #region Form Events
        void LoaderFormLoad(object sender, EventArgs e)
		{
			GlobalFuncs.LogPrint("Booting Quick Configure....");
			QuickConfigure main = new QuickConfigure();
			main.ShowDialog();
			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);			
		}

        void StartGame()
		{
			GlobalFuncs.LaunchRBXClient(ScriptType.Client, false, true, new EventHandler(ClientExited), label1);
			Visible = false;
		}
		
		void ClientExited(object sender, EventArgs e)
		{
			if (!GlobalVars.LocalPlayMode && GlobalVars.GameOpened != ScriptType.Server)
			{
				GlobalVars.GameOpened = ScriptType.None;
			}
			GlobalFuncs.UpdateRichPresence(GlobalFuncs.GetStateForType(GlobalVars.GameOpened));
            Close();
		}

		private void CheckIfFinished(object state)
		{
			if (!LocalVars.ReadyToLaunch)
			{
				System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
			}
			else
			{
				GlobalFuncs.LogPrint("Ready to launch.");
				Visible = true;
				CenterToScreen();
				if (GlobalVars.UserConfiguration.DiscordPresence)
				{
					GlobalFuncs.LogPrint("Starting Discord Rich Presence...");
					label1.Text = "Starting Discord Rich Presence...";
					StartDiscord();
				}
				GlobalFuncs.LogPrint("Launching Game...");
				label1.Text = "Launching Game...";
				LocalFuncs.SetupURIValues();
				StartGame();
			}
		}
        #endregion
    }
    #endregion
}
