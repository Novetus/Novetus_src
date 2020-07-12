#region Usings
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
