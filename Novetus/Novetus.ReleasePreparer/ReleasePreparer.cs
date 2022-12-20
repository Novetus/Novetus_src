#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
#endregion

namespace Novetus.ReleasePreparer
{
    #region ReleasePreparer
    class ReleasePreparer
    {
        static void Main(string[] args)
        {
            string novpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\Novetus";

            if (args.Length > 0)
            {
                if (args.Contains("-snapshot"))
                {
                    string infopath = novpath + @"\\config\\info.ini";
                    string currver = GetBranch(infopath);
                    TurnOnInitialSequence(infopath);

                    string pathbeta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\betaversion.txt";
                    Console.WriteLine("Creating " + pathbeta);
                    if (!File.Exists(pathbeta))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(pathbeta))
                        {
                            sw.Write(currver);
                        }
                    }
                    Console.WriteLine("Created " + pathbeta);
                }
                else if (args.Contains("-release"))
                {
                    DoRelease(novpath);
                }
            }
            else
            {
                DoRelease(novpath);
            }
        }

        public static void DoRelease(string novpath)
        {
            string infopath = novpath + @"\\config\\info.ini";
            string currbranch = GetBranch(infopath);
            TurnOnInitialSequence(infopath);

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\releaseversion.txt";
            Console.WriteLine("Creating " + path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(currbranch);
                }
            }
            Console.WriteLine("Created " + path);
        }

        public static void FixedFileCopy(string src, string dest, bool overwrite)
        {
            if (File.Exists(dest))
            {
                File.SetAttributes(dest, FileAttributes.Normal);
            }

            File.Copy(src, dest, overwrite);
            File.SetAttributes(dest, FileAttributes.Normal);
        }

        public static void FixedFileDelete(string src)
        {
            if (File.Exists(src))
            {
                File.SetAttributes(src, FileAttributes.Normal);
                File.Delete(src);
            }
        }

        public static string GetBranch(string infopath)
        {
            INIFile ini = new INIFile(infopath);
            return GetBranch(ini, infopath);
        }

        public static string GetBranch(INIFile ini, string infopath)
        {
            //READ
            string versionbranch, extendedVersionNumber, extendedVersionTemplate, extendedVersionRevision, isLite;
            string section = "ProgramInfo";
            versionbranch = ini.IniReadValue(section, "Branch", "0.0");
            extendedVersionNumber = ini.IniReadValue(section, "ExtendedVersionNumber", "False");
            extendedVersionTemplate = ini.IniReadValue(section, "ExtendedVersionTemplate", "%version%");
            extendedVersionRevision = ini.IniReadValue(section, "ExtendedVersionRevision", "-1");
            isLite = ini.IniReadValue(section, "IsLite", "False");

            string novpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\Novetus\\bin\\Novetus.exe";

            if (!extendedVersionNumber.Equals("False"))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(novpath);
                return extendedVersionTemplate.Replace("%version%", versionbranch)
                            .Replace("%build%", versionInfo.ProductBuildPart.ToString())
                            .Replace("%revision%", versionInfo.FilePrivatePart.ToString())
                            .Replace("%extended-revision%", (!extendedVersionRevision.Equals("-1") ? extendedVersionRevision : ""))
                            .Replace("%lite%", (!isLite.Equals("False") ? " (Lite)" : ""));
            }
            else
            {
                return versionbranch;
            }
        }

        public static void SetToLite(string infopath)
        {
            INIFile ini = new INIFile(infopath);
            string section = "ProgramInfo";
            string isLite = ini.IniReadValue(section, "IsLite", "False");

            try
            {
                if (!isLite.Equals("True"))
                {
                    ini.IniWriteValue(section, "IsLite", "True");
                }
            }
            catch (Exception)
            {
                SetToLite(infopath);
            }
        }

        public static void TurnOnInitialSequence(string infopath)
        {
            //READ
            INIFile ini = new INIFile(infopath);
            string section = "ProgramInfo";

            string initialBootup = ini.IniReadValue(section, "InitialBootup", "True");
            if (Convert.ToBoolean(initialBootup) == false)
            {
                ini.IniWriteValue(section, "InitialBootup", "True");
            }
        }
    }
    #endregion
}
