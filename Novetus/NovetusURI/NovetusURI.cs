#region Usings
using NLog;
using Novetus.Core;
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

            if (!Directory.Exists(GlobalPaths.LogDir))
            {
                Directory.CreateDirectory(GlobalPaths.LogDir);
            }

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.LogDir + "\\URI-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
            config.AddRuleForAllLevels(logfile);
            LogManager.Configuration = config;

            //FileManagement.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, 
                //GlobalPaths.ConfigDir + "\\" + GlobalPaths.TermListFileName);
            GlobalVars.ColorsLoaded = FileManagement.InitColors();
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
