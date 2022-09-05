#region Usings
using NLog;
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
            GlobalFuncs.Config(LocalPaths.ConfigPath, true);
            GlobalFuncs.LogPrint("Starting " + appName);
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
                GlobalFuncs.LogExceptions(ex);
                MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LaunchApplication(string appName, string args = "")
        {
            LaunchApplicationExt(LocalPaths.FixedBinDir, appName, args);
        }
    }
    #endregion
}
