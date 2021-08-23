#region Usings
using System;
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
            if (args.Length > 0)
            {
                string novpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\Novetus";

                if (args.Contains("-lite"))
                {
                    string litepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\Novetus-Lite";

                    if (!Directory.Exists(litepath))
                    {
                        Console.WriteLine("Novetus Lite does not exist. Creating " + litepath);
                        Directory.CreateDirectory(litepath);

                        //https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp

                        Console.WriteLine("Creating directories...");
                        //Now Create all of the directories
                        foreach (string dirPath in Directory.GetDirectories(novpath, "*",
                            SearchOption.AllDirectories)
                            .Where(m => !m.Contains("Maps released by year"))
                            .Where(c06 => !c06.Contains("2006S"))
                            .Where(c07s => !c07s.Contains("2007M-Shaders"))
                            .Where(c07Es => !c07Es.Contains("2007E-Shaders"))
                            .Where(c06s => !c06s.Contains("2006S-Shaders"))
                            .Where(c09eHD => !c09eHD.Contains("2009E-HD"))
                            .Where(music => !music.Contains("OldSoundtrack")))
                        {
                            Directory.CreateDirectory(dirPath.Replace(novpath, litepath));
                            Console.WriteLine("D: " + dirPath.Replace(novpath, litepath));
                        }

                        Console.WriteLine("Copying files...");
                        //Copy all the files & Replaces any files with the same name
                        foreach (string newPath in Directory.GetFiles(novpath, "*.*",
                            SearchOption.AllDirectories)
                            .Where(m => !m.Contains("Maps released by year"))
                            .Where(c06 => !c06.Contains("2006S"))
                            .Where(c07s => !c07s.Contains("2007M-Shaders"))
                            .Where(c07Es => !c07Es.Contains("2007E-Shaders"))
                            .Where(c06s => !c06s.Contains("2006S-Shaders"))
                            .Where(c09eHD => !c09eHD.Contains("2009E-HD"))
                            .Where(music => !music.Contains("OldSoundtrack")))
                        {
                            FixedFileCopy(newPath, newPath.Replace(novpath, litepath), true);
                            Console.WriteLine("F: " + newPath.Replace(novpath, litepath));
                        }

                        Console.WriteLine("Overwriting files with lite alternatives...");
                        string litefiles = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\litefiles";

                        foreach (string newPath in Directory.GetFiles(litefiles, "*.*",
                            SearchOption.AllDirectories))
                        {
                            FixedFileCopy(newPath, newPath.Replace(litefiles, litepath), true);
                            Console.WriteLine("OW: " + newPath.Replace(litefiles, litepath));
                        }
                    }

                    string infopathlite = litepath + @"\\config\\info.ini";
                    Console.WriteLine("Editing " + infopathlite);
                    SetBranch(infopathlite);
                    string currbranchlite = GetBranch(infopathlite);

                    string pathlite = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\releasenomapsversion.txt";
                    Console.WriteLine("Creating " + pathlite);
                    if (!File.Exists(pathlite))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(pathlite))
                        {
                            sw.Write(currbranchlite);
                        }
                    }
                    Console.WriteLine("Created " + pathlite);
                }
                else if (args.Contains("-snapshot"))
                {
                    string infopath = novpath + @"\\changelog.txt";
                    string currver = File.ReadLines(infopath).First();

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
                    string infopath = novpath + @"\\config\\info.ini";
                    string currbranch = GetBranch(infopath);

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
            }
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
            //READ
            string versionbranch;
            INIFile ini = new INIFile(infopath);
            string section = "ProgramInfo";
            versionbranch = ini.IniReadValue(section, "Branch", "0.0");
            return versionbranch;
        }

        public static void SetBranch(string infopath)
        {
            //READ
            string versionbranch;

            INIFile ini = new INIFile(infopath);

            string section = "ProgramInfo";

            versionbranch = ini.IniReadValue(section, "Branch", "0.0");

            try
            {
                if (!versionbranch.Contains("(Lite)"))
                {
                    ini.IniWriteValue(section, "Branch", versionbranch + " (Lite)");
                }
            }
            catch (Exception)
            {
                SetBranch(infopath);
            }
        }
    }
    #endregion
}
