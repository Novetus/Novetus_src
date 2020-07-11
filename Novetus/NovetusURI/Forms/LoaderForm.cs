#region Usings
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
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

				GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.LoadingURI, "", true);
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
			QuickConfigure main = new QuickConfigure();
			main.ShowDialog();
			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);			
		}

        void StartGame()
		{
			GlobalFuncs.LaunchRBXClient(ScriptType.Client, false, true, ClientExited, label1);
		}
		
		void ClientExited(object sender, EventArgs e)
		{
            GlobalFuncs.UpdateRichPresence(GlobalVars.LauncherState.InLauncher, "");
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
				Visible = true;
				if (GlobalVars.UserConfiguration.DiscordPresence)
				{
					label1.Text = "Starting Discord Rich Presence...";
					StartDiscord();
				}
				label1.Text = "Launching Game...";
				LocalFuncs.SetupURIValues();
				StartGame();
			}
		}
		#endregion
	}
    #endregion
}
