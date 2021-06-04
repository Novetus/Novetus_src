#region Usings
using System.IO;
using System.Reflection;
#endregion

namespace Novetus.Launch
{
    #region Local Paths

    public class LocalPaths
    {
        #region Base Paths
        public static readonly string RootPathLauncher = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string BasePathLauncher = RootPathLauncher.Replace(@"\", @"\\");
        public static readonly string BinPath = BasePathLauncher + @"\\bin";
        public static readonly string ConfigPath = BasePathLauncher + @"\\config";
        #endregion

        #region File Names
        public static readonly string LauncherName = "Novetus.exe";
        public static readonly string CMDName = "NovetusCMD.exe";
        public static readonly string URIName = "NovetusURI.exe";
        public static readonly string DependencyLauncherName = "Novetus_dependency_installer.bat";
        public static readonly string LauncherInfoFile = "info.ini";
        #endregion
    }
    #endregion
}