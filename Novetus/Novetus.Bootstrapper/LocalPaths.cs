#region Usings
using Novetus.Core;
using System.IO;
using System.Reflection;
#endregion

namespace Novetus.Bootstrapper
{
    #region Local Paths

    public class LocalPaths
    {
        public static readonly string FixedBinDir = GlobalPaths.BasePathLauncher + @"\\bin";
        public static readonly string FixedConfigDir = GlobalPaths.BasePathLauncher + @"\\config";
        public static readonly string FixedLogDir = GlobalPaths.BasePathLauncher + @"\\logs";
        public static readonly string FixedDataDir = FixedBinDir + @"\\data";

        #region File Names
        public static readonly string LauncherName = "Novetus.exe";
        public static readonly string URIName = "NovetusURI.exe";
        public static readonly string DependencyLauncherName = "Novetus_dependency_installer.bat";
        #endregion

        #region File Paths
        public static readonly string LauncherPath = FixedBinDir + "\\" + LauncherName;
        public static readonly string InfoPath = FixedConfigDir + "\\" + GlobalPaths.InfoName;
        public static readonly string ConfigPath = FixedConfigDir + "\\" + GlobalPaths.ConfigName;
        #endregion
    }
    #endregion
}