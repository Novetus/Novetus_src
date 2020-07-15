#region Usings
using NLog;
using System;
using System.IO;
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
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.ConfigDir + "\\URI-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
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
                log.Error("TARGET SITE: " + (eventArgs.Exception.TargetSite != null ? eventArgs.Exception.TargetSite.ToString() : "N/A"));
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
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
