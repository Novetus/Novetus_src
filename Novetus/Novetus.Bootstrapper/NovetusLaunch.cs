#region Usings
#if !BASICLAUNCHER
using NLog;
#endif
using Novetus.Core;
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
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists(GlobalPaths.LogDir))
            {
                Directory.CreateDirectory(GlobalPaths.LogDir);
            }

            Application.Run(new NovetusLaunchForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            DumpExcretion.CreateMiniDump("NovetusLaunchForm");
        }
    }
}
