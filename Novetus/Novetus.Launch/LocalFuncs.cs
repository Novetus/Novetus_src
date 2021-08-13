#region Usings
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
#endregion

namespace Novetus.Launch
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
            LaunchApplicationExt(LocalPaths.BinPath, appName, args);
        }

        public static string GetVersion(string infopath)
        {
            //READ
            string version;
            INIFile ini = new INIFile(infopath);
            string section = "ProgramInfo";
            bool extendedversionnumber = Convert.ToBoolean(ini.IniReadValue(section, "ExtendedVersionNumber", "False"));
            version = (extendedversionnumber ? ini.IniReadValue(section, "ExtendedVersionTemplate", "%version%") : ini.IniReadValue(section, "Branch", "0.0"));

            var versionInfo = FileVersionInfo.GetVersionInfo(LocalPaths.BinPath + "\\" + LocalPaths.LauncherName);
            string extendedversionrevision = ini.IniReadValue(section, "ExtendedVersionRevision", "1");
            version = version.Replace("%version%", ini.IniReadValue(section, "Branch", "0.0"))
                        .Replace("%build%", versionInfo.ProductBuildPart.ToString())
                        .Replace("%revision%", versionInfo.FilePrivatePart.ToString())
                        .Replace("%extended-revision%", extendedversionrevision);
            return version;
        }
    }
    #endregion
}
