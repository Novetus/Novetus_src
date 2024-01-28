#region Usings
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using NLog;
using Novetus.Core;
#endregion

namespace NovetusURI
{
    #region URI Loader
    public partial class LoaderForm : Form
	{
        #region Constructor
        public LoaderForm()
		{
			InitializeComponent();
		}
        #endregion

        #region Form Events
        void LoaderFormLoad(object sender, EventArgs e)
		{
			Client.UpdateStatus(label1, "Initializing...");

			if (GlobalVars.UserConfiguration.ReadSettingBool("URIQuickConfigure"))
			{
				Client.UpdateStatus(label1, "Loading Player Configuration Menu....");
				QuickConfigure main = new QuickConfigure();
				main.ShowDialog();
			}
			else
            {
				Client.ReadClientValues();
				LocalVars.ReadyToLaunch = true;
			}

			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(CheckIfFinished), null, 1, 0);
		}

        void StartGame()
		{
			try
			{
				Client.LaunchRBXClient(ScriptType.Client, false, true, new EventHandler(ClientExited), label1);
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

			if (GlobalVars.UserConfiguration.ReadSettingBool("WebProxyEnabled"))
			{
				GlobalVars.Proxy.Stop();
			}
			Client.UpdateRichPresence(Client.GetStateForType(GlobalVars.GameOpened));
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
				Client.UpdateStatus(label1, "Ready to launch.");
				Visible = true;
				CenterToScreen();
				if (GlobalVars.UserConfiguration.ReadSettingBool("DiscordRichPresence"))
				{
					Client.UpdateStatus(label1, "Starting Discord Rich Presence...");
					DiscordRPC.StartDiscord();
				}
				Client.UpdateStatus(label1, "Launching Game...");
				LocalFuncs.SetupURIValues();
				StartGame();
			}
		}
        #endregion
    }
    #endregion
}
