#region Usings
using NLog;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace NovetusURI
{
    #region Novetus URI Main Class
    internal sealed class NovetusURI
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.ConfigDir + "\\URI-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
            config.AddRuleForAllLevels(logfile);
            LogManager.Configuration = config;

            GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName);
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
            GlobalVars.ColorsLoaded = GlobalFuncs.InitColors();
            if (args.Length == 0)
            {
                Application.Run(new InstallForm());
            }
            else
            {
                foreach (string s in args)
                {
                    LocalVars.SharedArgs = s;
                }

                Application.Run(new LoaderForm());
            }
        }
    }
    #endregion
}
