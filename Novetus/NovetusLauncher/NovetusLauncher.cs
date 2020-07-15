#region Usings
using NLog;
using System;
using System.IO;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Novetus Launcher Main Class
    internal sealed class NovetusLauncher
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			var config = new NLog.Config.LoggingConfiguration();
			var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.ConfigDir + "\\Launcher-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
			config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);  
			LogManager.Configuration = config;

			//https://stackify.com/csharp-catch-all-exceptions/
			AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
			{
				Logger log = LogManager.GetCurrentClassLogger();
				log.Error(eventArgs.Exception);
			};

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName);
			GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
			if (args.Length == 0)
			{
				switch (GlobalVars.UserConfiguration.LauncherStyle)
                {
					case Settings.UIOptions.Style.Compact:
						Application.Run(new LauncherFormCompact());
						break;
					default:
						Application.Run(new LauncherFormExtended());
						break;
				}
			}
			else
			{
				CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

				if (CommandLine["sdk"] != null)
				{
					Application.Run(new NovetusSDK());
				}
			}
		}
	}
    #endregion
}
