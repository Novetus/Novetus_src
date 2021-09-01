#region Usings
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
            Process client = new Process();
            client.StartInfo.FileName = filePath + @"\\" + appName;
            client.StartInfo.Arguments = args;
            client.Start();
        }

        public static void LaunchApplication(string appName, string args = "")
        {
            LaunchApplicationExt(LocalPaths.FixedBinDir, appName, args);
        }
    }
    #endregion
}
