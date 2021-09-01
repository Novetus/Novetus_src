#region Usings
using NLog;
using System;
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
				log.Error("EXEPTION THROWN: " + (!string.IsNullOrWhiteSpace(eventArgs.Exception.Message) ? eventArgs.Exception.Message : "N/A"));
				log.Error("EXCEPTION INFO: " + (eventArgs.Exception != null ? eventArgs.Exception.ToString() : "N/A"));
				log.Error("INNER EXCEPTION: " + (eventArgs.Exception.InnerException != null ? eventArgs.Exception.InnerException.ToString() : "N/A"));
				log.Error("STACK TRACE: " + (!string.IsNullOrWhiteSpace(eventArgs.Exception.StackTrace) ? eventArgs.Exception.StackTrace : "N/A"));
				log.Error("TARGET SITE: " + (eventArgs.Exception.TargetSite != null  ? eventArgs.Exception.TargetSite.ToString() : "N/A"));
				log.Error("FOOTPRINTS: " + (!string.IsNullOrWhiteSpace(eventArgs.Exception.GetExceptionFootprints()) ? eventArgs.Exception.GetExceptionFootprints() : "N/A"));
			};

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName);
			GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
			if (args.Length == 0)
			{
				switch (GlobalVars.UserConfiguration.LauncherStyle)
                {
					case Settings.Style.Compact:
						Application.Run(new LauncherFormCompact());
						break;
					case Settings.Style.Extended:
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
