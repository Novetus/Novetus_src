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

				ClientManagement.UpdateRichPresence(GlobalVars.LauncherState.LoadingURI, true);
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
			ClientManagement.UpdateStatus(label1, "Initializing...");

			if (GlobalVars.UserConfiguration.URIQuickConfigure)
			{
				ClientManagement.UpdateStatus(label1, "Loading Player Configuration Menu....");
				QuickConfigure main = new QuickConfigure();
				main.ShowDialog();
			}
			else
            {
				FileManagement.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
				ClientManagement.ReadClientValues();
				LocalVars.ReadyToLaunch = true;
			}

			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
		}

        void StartGame()
		{
			try
			{
				ClientManagement.LaunchRBXClient(ScriptType.Client, false, true, new EventHandler(ClientExited), label1);
				Visible = false;
			}
			catch (Exception ex)
            {
				Util.LogExceptions(ex);
				Close();
			}
		}
		
		void ClientExited(object sender, EventArgs e)
		{
			if (!GlobalVars.LocalPlayMode && GlobalVars.GameOpened != ScriptType.Server)
			{
				GlobalVars.GameOpened = ScriptType.None;
			}

			if (GlobalVars.Proxy.HasStarted())
			{
				GlobalVars.Proxy.Stop();
			}
			ClientManagement.UpdateRichPresence(ClientManagement.GetStateForType(GlobalVars.GameOpened));
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
				ClientManagement.UpdateStatus(label1, "Ready to launch.");
				Visible = true;
				CenterToScreen();
				if (GlobalVars.UserConfiguration.DiscordPresence)
				{
					ClientManagement.UpdateStatus(label1, "Starting Discord Rich Presence...");
					StartDiscord();
				}
				ClientManagement.UpdateStatus(label1, "Launching Game...");
				LocalFuncs.SetupURIValues();
				StartGame();
			}
		}
        #endregion
    }
    #endregion
}
