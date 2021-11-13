#region Usings
using NLog;
using System;
#endregion

namespace NovetusLauncher
{
    #region Novetus Launcher Main Class
    internal sealed class NovetusLauncherEntryPoint
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

			var config = new NLog.Config.LoggingConfiguration();
			var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.ConfigDir + "\\Launcher-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
			config.AddRuleForAllLevels(logfile);
			LogManager.Configuration = config;

			GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName);
			GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
			GlobalVars.ColorsLoaded = GlobalFuncs.InitColors();
			if (args.Length == 0)
			{
				RunLauncher();
			}
			else
			{
				CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

				if (CommandLine["sdk"] != null)
				{
					System.Windows.Forms.Application.Run(new NovetusSDK());
				}
			}
		}

		static void RunLauncher()
        {
			try
			{
				switch (GlobalVars.UserConfiguration.LauncherStyle)
				{
					case Settings.Style.Compact:
						System.Windows.Forms.Application.Run(new LauncherFormCompact());
						break;
					case Settings.Style.Extended:
						System.Windows.Forms.Application.Run(new LauncherFormExtended());
						break;
					case Settings.Style.Stylish:
					default:
						System.Windows.Forms.Application.Run(new LauncherFormStylish());
						break;
				}
			}
			catch (Exception ex)
			{
				GlobalFuncs.LogExceptions(ex);
			}
		}
	}
	#endregion
}
