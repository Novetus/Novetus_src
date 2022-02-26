#region Usings
using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
#endregion

namespace Novetus.Bootstrapper
{
    #region Local Funcs
    public class LocalFuncs
    {
        public static void LaunchApplicationExt(string filePath, string appName, string args = "")
        {
            GlobalFuncs.LogPrint("Starting " + appName);
            try
            {
                Process client = new Process();
                client.StartInfo.FileName = filePath + @"\\" + appName;
                client.StartInfo.Arguments = args;
                if (SecurityFuncs.IsElevated)
                {
                    client.StartInfo.Verb = "runas";
                }
                client.Start();
            }
            catch (Exception ex)
            {
                GlobalFuncs.LogExceptions(ex);
            }
        }

        public static void LaunchApplication(string appName, string args = "")
        {
            LaunchApplicationExt(LocalPaths.FixedBinDir, appName, args);
        }
    }
    #endregion
}
