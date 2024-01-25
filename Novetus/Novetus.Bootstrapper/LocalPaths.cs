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
        #region File Names
        public static readonly string LauncherName = "Novetus.exe";
        public static readonly string URIName = "NovetusURI.exe";
        public static readonly string DependencyLauncherName = "Novetus_Dependency_Installer.exe";
        #endregion

        #region File Paths
        public static readonly string LauncherPath = GlobalPaths.BinDir + "\\" + LauncherName;
        public static readonly string InfoPath = GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName;
        public static readonly string VersionTermList = GlobalPaths.ConfigDir + "\\" + GlobalPaths.TermListFileName;
        public static readonly string ConfigPath = GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName;
        #endregion
    }
    #endregion
}