#region Usings
using NLog;
using System;
using System.IO;
using System.Windows.Forms;
#endregion

namespace Novetus.Bootstrapper
{
    static class NovetusLaunch
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists(LocalPaths.FixedLogDir))
            {
                Directory.CreateDirectory(LocalPaths.FixedLogDir);
            }

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = LocalPaths.FixedLogDir + "\\Bootstrapper-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
            config.AddRuleForAllLevels(logfile);
            LogManager.Configuration = config;

            Application.Run(new NovetusLaunchForm());
        }
    }
}
