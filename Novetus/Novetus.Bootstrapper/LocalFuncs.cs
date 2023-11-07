#region Usings
#if !BASICLAUNCHER
using NLog;
#endif
using Novetus.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
#endregion

namespace Novetus.Bootstrapper
{
    #region Local Funcs
    public class LocalFuncs
    {
        public static void LaunchApplicationExt(string filePath, string appName, string args = "")
        {
            Util.LogPrint("Starting " + appName);
            try
            {
                Process client = new Process();
                client.StartInfo.FileName = filePath + @"\\" + appName;
                client.StartInfo.Arguments = args;
                client.StartInfo.UseShellExecute = true;
                if (SecurityFuncs.IsElevated)
                {
                    client.StartInfo.Verb = "runas";
                }
                client.Start();
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LaunchApplication(string appName, string args = "")
        {
            LaunchApplicationExt(GlobalPaths.BinDir, appName, args);
        }
    }
    #endregion
}
