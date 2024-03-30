#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
#endregion

namespace Novetus.Core
{
    #region File Management
    public class FileManagement
    {
        public static string CreateVersionName(string termspath, int revision)
        {
            string rev = revision.ToString();

            if (rev.Length > 0 && rev.Length >= 5)
            {
                string posString = rev.Substring(rev.Length - 4);

                int pos1 = int.Parse(posString.Substring(0, 2));
                int pos2 = int.Parse(posString.Substring(posString.Length - 2));

                List<string> termList = new List<string>();
                termList.AddRange(File.ReadAllLines(termspath));

                string firstTerm = (termList.ElementAtOrDefault(pos1 - 1) != null) ? termList[pos1 - 1] : termList.First();
                string lastTerm = (termList.ElementAtOrDefault(pos2 - 1) != null) ? termList[pos2 - 1] : termList.Last();

                return firstTerm + " " + lastTerm + " (" + revision + ")";
            }

            return "Invalid Revision (0)";
        }

        public static void ReadInfoFile(string infopath, string termspath, string exepath = "")
        {
            //READ
            string versionbranch, defaultclient, defaultmap, regclient1,
                regclient2, extendedversionnumber, extendedversiontemplate,
                extendedversionrevision, isSnapshot,
                initialBootup;

            string verNumber = "Invalid File";
            string section = "ProgramInfo";

            JSONFile json = new JSONFile(infopath, section, false);

            //not using the GlobalVars definitions as those are empty until we fill them in.
            versionbranch = json.JsonReadValue(section, "Branch", "0.0");
            defaultclient = json.JsonReadValue(section, "DefaultClient", "2009E");
            defaultmap = json.JsonReadValue(section, "DefaultMap", "Dev - Baseplate2048.rbxl");
            regclient1 = json.JsonReadValue(section, "UserAgentRegisterClient1", "2007M");
            regclient2 = json.JsonReadValue(section, "UserAgentRegisterClient2", "2009L");
            extendedversionnumber = json.JsonReadValue(section, "ExtendedVersionNumber", "False");
            extendedversiontemplate = json.JsonReadValue(section, "ExtendedVersionTemplate", "%version%");
            extendedversionrevision = json.JsonReadValue(section, "ExtendedVersionRevision", "-1");
            isSnapshot = json.JsonReadValue(section, "IsSnapshot", "False");
            initialBootup = json.JsonReadValue(section, "InitialBootup", "True");

            try
            {
                GlobalVars.ExtendedVersionNumber = ConvertSafe.ToBooleanSafe(extendedversionnumber);
                if (GlobalVars.ExtendedVersionNumber)
                {
                    if (!string.IsNullOrWhiteSpace(exepath))
                    {
                        var versionInfo = FileVersionInfo.GetVersionInfo(exepath);
                        verNumber = CreateVersionName(termspath, versionInfo.FilePrivatePart);
                        GlobalVars.ProgramInformation.Version = extendedversiontemplate.Replace("%version%", versionbranch)
                            .Replace("%build%", versionInfo.ProductBuildPart.ToString())
                            .Replace("%revision%", versionInfo.FilePrivatePart.ToString())
                            .Replace("%extended-revision%", (!extendedversionrevision.Equals("-1") ? extendedversionrevision : ""))
                            .Replace("%version-name%", verNumber);
                    }
                    else
                    {
                        verNumber = CreateVersionName(termspath, Assembly.GetExecutingAssembly().GetName().Version.Revision);
                        GlobalVars.ProgramInformation.Version = extendedversiontemplate.Replace("%version%", versionbranch)
                            .Replace("%build%", Assembly.GetExecutingAssembly().GetName().Version.Build.ToString())
                            .Replace("%revision%", Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString())
                            .Replace("%extended-revision%", (!extendedversionrevision.Equals("-1") ? extendedversionrevision : ""))
                            .Replace("%version-name%", verNumber);
                    }

                    bool changelogedit = ConvertSafe.ToBooleanSafe(isSnapshot);

                    if (changelogedit)
                    {
                        string changelog = GlobalPaths.BasePath + "\\changelog.txt";
                        if (File.Exists(changelog))
                        {
                            string[] changeloglines = File.ReadAllLines(changelog);
                            if (!changeloglines[0].Equals(GlobalVars.ProgramInformation.Version))
                            {
                                changeloglines[0] = GlobalVars.ProgramInformation.Version;
                                File.WriteAllLines(changelog, changeloglines);
                            }
                        }
                    }
                }
                else
                {
                    GlobalVars.ProgramInformation.Version = versionbranch;
                }

                GlobalVars.ProgramInformation.Branch = versionbranch;
                GlobalVars.ProgramInformation.DefaultClient = defaultclient;
                GlobalVars.ProgramInformation.DefaultMap = defaultmap;
                GlobalVars.ProgramInformation.RegisterClient1 = regclient1;
                GlobalVars.ProgramInformation.RegisterClient2 = regclient2;
                GlobalVars.ProgramInformation.InitialBootup = ConvertSafe.ToBooleanSafe(initialBootup);
                GlobalVars.ProgramInformation.VersionName = verNumber;
                GlobalVars.ProgramInformation.IsSnapshot = ConvertSafe.ToBooleanSafe(isSnapshot);
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                //ReadInfoFile(infopath, termspath, exepath);
            }
        }

        public static void TurnOffInitialSequence()
        {
            //READ
            string section = "ProgramInfo";
            JSONFile json = new JSONFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, section, false);

            string initialBootup = json.JsonReadValue(section, "InitialBootup", "True");
            if (ConvertSafe.ToBooleanSafe(initialBootup) == true)
            {
                json.JsonWriteValue(section, "InitialBootup", "False");
            }
        }

        public static void ResetMap()
        {
            GlobalVars.UserConfiguration.SaveSetting("Map", GlobalVars.ProgramInformation.DefaultMap);
            GlobalVars.UserConfiguration.SaveSetting("MapPath", GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap);
            GlobalVars.UserConfiguration.SaveSetting("MapPathSnip", GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap);
        }

        public static bool ResetMapIfNecessary()
        {
            if (!File.Exists(GlobalVars.UserConfiguration.ReadSetting("MapPath")))
            {
                ResetMap();
                return true;
            }

            return false;
        }

        public static bool InitColors()
        {
            try
            {
                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
                {
                    GlobalVars.PartColorList = PartColor.GetPartColors();
                    GlobalVars.PartColorListConv = new List<PartColor>();
                    GlobalVars.PartColorListConv.AddRange(GlobalVars.PartColorList);
                    return true;
                }
                else
                {
                    goto Failure;
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                goto Failure;
            }

        Failure:
            return false;
        }

        public static bool HasColorsChanged()
        {
            try
            {
                PartColor[] tempList;

                if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
                {
                    tempList = PartColor.GetPartColors();
                    if (tempList.Length != GlobalVars.PartColorList.Length)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    goto Failure;
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                goto Failure;
            }

        Failure:
            return false;
        }

#if LAUNCHER
        public static void ResetConfigValues(Settings.Style style)
#else
        public static void ResetConfigValues()
#endif
        {
            bool WebProxySetupComplete = GlobalVars.UserConfiguration.ReadSettingBool("WebProxyInitialSetupRequired");
            bool WebProxy = GlobalVars.UserConfiguration.ReadSettingBool("WebProxyEnabled");

            GlobalVars.UserConfiguration = new FileFormat.Config(true);
            GlobalVars.UserConfiguration.SaveSetting("SelectedClient", GlobalVars.ProgramInformation.DefaultClient);
            ResetMap();
#if LAUNCHER
            GlobalVars.UserConfiguration.SaveSettingInt("LauncherStyle", (int)style);
#endif
            GlobalVars.UserConfiguration.SaveSettingBool("WebProxyInitialSetupRequired", WebProxySetupComplete);
            GlobalVars.UserConfiguration.SaveSettingBool("WebProxyEnabled", WebProxy);
            GlobalVars.UserConfiguration.SaveSettingInt("UserID", NovetusFuncs.GeneratePlayerID());
            ResetCustomizationValues();
        }

        public static void ResetCustomizationValues()
        {
            GlobalVars.UserCustomization = new FileFormat.CustomizationConfig(true);
            ReloadLoadoutValue();
        }

        public static string GetItemTextureLocalPath(string item, string nameprefix)
        {
            //don't bother, we're offline.
            if (GlobalVars.ExternalIP.Equals("localhost"))
                return "";

            if (!GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%localizeContentProviderLoader%"))
                return "";

            if (item.Contains("http://") || item.Contains("https://"))
            {
                string peram = "id=";
                string fullname = nameprefix + "Temp.png";

                if (item.Contains(peram))
                {
                    string id = item.After(peram);
                    fullname = id + ".png";
                }
                else
                {
                    return item;
                }

                Downloader download = new Downloader(item, fullname, "", GlobalPaths.AssetCacheDirAssets);

                try
                {
                    string path = download.GetFullDLPath();
                    download.InitDownloadNoDialog(path);
                    return GlobalPaths.AssetCacheAssetsGameDir + download.fileName;
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                }
            }

            return "";
        }

        public static string GetItemTextureID(string item, string name, AssetCacheDefBasic assetCacheDef)
        {
            //don't bother, we're offline.
            if (GlobalVars.ExternalIP.Equals("localhost"))
                return "";

            if (!GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%localizeContentProviderLoader%"))
                return "";

            if (item.Contains("http://") || item.Contains("https://"))
            {
                string peram = "id=";
                if (!item.Contains(peram))
                {
                    return item;
                }

                Downloader download = new Downloader(item, name + "Temp.rbxm", "", GlobalPaths.AssetCacheDirAssets);

                try
                {
                    string path = download.GetFullDLPath();
                    download.InitDownloadNoDialog(path);
                    string oldfile = File.ReadAllText(path);
                    string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile)).Replace("&#9;", "\t").Replace("#9;", "\t");
                    XDocument doc = null;
                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };
                    Stream filestream = Util.GenerateStreamFromString(fixedfile);
                    using (XmlReader xmlReader = XmlReader.Create(filestream, xmlReaderSettings))
                    {
                        xmlReader.MoveToContent();
                        doc = XDocument.Load(xmlReader);
                    }

                    return RobloxXML.GetURLInNodes(doc, assetCacheDef.Class, assetCacheDef.Id[0], item);
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                }
            }

            return "";
        }

        public static void ReloadLoadoutValue(bool localizeContentProviderLoader = false)
        {
            string hat1 = (!GlobalVars.UserCustomization.ReadSetting("Hat1").EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.ReadSetting("Hat1") : "NoHat.rbxm";
            string hat2 = (!GlobalVars.UserCustomization.ReadSetting("Hat2").EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.ReadSetting("Hat2") : "NoHat.rbxm";
            string hat3 = (!GlobalVars.UserCustomization.ReadSetting("Hat3").EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.ReadSetting("Hat3") : "NoHat.rbxm";
            string extra = (!GlobalVars.UserCustomization.ReadSetting("Extra").EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.ReadSetting("Extra") : "NoExtra.rbxm";

            string baseClothing = GlobalVars.UserCustomization.ReadSettingInt("HeadColorID") + "," +
            GlobalVars.UserCustomization.ReadSettingInt("TorsoColorID") + "," +
            GlobalVars.UserCustomization.ReadSettingInt("LeftArmColorID") + "," +
            GlobalVars.UserCustomization.ReadSettingInt("RightArmColorID") + "," +
            GlobalVars.UserCustomization.ReadSettingInt("LeftLegColorID") + "," +
            GlobalVars.UserCustomization.ReadSettingInt("RightLegColorID") + ",'" +
            GlobalVars.UserCustomization.ReadSetting("TShirt") + "','" +
            GlobalVars.UserCustomization.ReadSetting("Shirt") + "','" +
            GlobalVars.UserCustomization.ReadSetting("Pants") + "','" +
            GlobalVars.UserCustomization.ReadSetting("Face") + "','" +
            GlobalVars.UserCustomization.ReadSetting("Head") + "','" +
            GlobalVars.UserCustomization.ReadSetting("Icon") + "','";

            GlobalVars.Loadout = "'" + hat1 + "','" +
            hat2 + "','" +
            hat3 + "'," +
            baseClothing +
            extra + "'";

            if (localizeContentProviderLoader)
            {
                GlobalVars.TShirtTextureID = GetItemTextureID(GlobalVars.UserCustomization.ReadSetting("TShirt"), "TShirt", new AssetCacheDefBasic("ShirtGraphic", new string[] { "Graphic" }));
                GlobalVars.ShirtTextureID = GetItemTextureID(GlobalVars.UserCustomization.ReadSetting("Shirt"), "Shirt", new AssetCacheDefBasic("Shirt", new string[] { "ShirtTemplate" }));
                GlobalVars.PantsTextureID = GetItemTextureID(GlobalVars.UserCustomization.ReadSetting("Pants"), "Pants", new AssetCacheDefBasic("Pants", new string[] { "PantsTemplate" }));
                GlobalVars.FaceTextureID = GetItemTextureID(GlobalVars.UserCustomization.ReadSetting("Face"), "Face", new AssetCacheDefBasic("Decal", new string[] { "Texture" }));

                GlobalVars.TShirtTextureLocal = GetItemTextureLocalPath(GlobalVars.TShirtTextureID, "TShirt");
                GlobalVars.ShirtTextureLocal = GetItemTextureLocalPath(GlobalVars.ShirtTextureID, "Shirt");
                GlobalVars.PantsTextureLocal = GetItemTextureLocalPath(GlobalVars.PantsTextureID, "Pants");
                GlobalVars.FaceTextureLocal = GetItemTextureLocalPath(GlobalVars.FaceTextureID, "Face");
            }
        }

        public static void CreateAssetCacheDirectories()
        {
            if (!Directory.Exists(GlobalPaths.AssetCacheDirAssets))
            {
                Directory.CreateDirectory(GlobalPaths.AssetCacheDirAssets);
            }
        }

        public static void CreateInitialFileListIfNeededMulti()
        {
            if (GlobalVars.NoFileList)
                return;

            string filePath = GlobalPaths.ConfigDir + "\\InitialFileList.txt";

            if (!File.Exists(filePath))
            {
                Util.ConsolePrint("WARNING - No file list detected. Generating Initial File List.", 5);
                Thread t = new Thread(CreateInitialFileList);
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                int lineCount = File.ReadLines(filePath).Count();
                int fileCount = 0;

                string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.InitialFileListIgnoreFilterName;
                string[] fileListToIgnore = File.ReadAllLines(filterPath);

                DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.RootPath);
                FileInfo[] Files = dinfo.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo file in Files)
                {
                    DirectoryInfo localdinfo = new DirectoryInfo(file.DirectoryName);
                    string directory = localdinfo.Name;
                    if (!fileListToIgnore.Contains(file.Name, StringComparer.InvariantCultureIgnoreCase) && !fileListToIgnore.Contains(directory, StringComparer.InvariantCultureIgnoreCase))
                    {
                        fileCount++;
                    }
                    else
                    {
                        continue;
                    }
                }

                //MessageBox.Show(lineCount + "\n" + fileCount);
                // commenting this because frankly the CreateInitialFileList thread should be called upon inital bootup of novetus.
                /*if (lineCount != fileCount)
                {
                    Util.ConsolePrint("WARNING - Initial File List is not relevant to file path. Regenerating.", 5);
                    Thread t = new Thread(CreateInitialFileList);
                    t.IsBackground = true;
                    t.Start();
                }*/
            }
        }

        private static void CreateInitialFileList()
        {
            string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.InitialFileListIgnoreFilterName;
            string[] fileListToIgnore = File.ReadAllLines(filterPath);
            string FileName = GlobalPaths.ConfigDir + "\\InitialFileList.txt";

            File.Create(FileName).Close();

            using (var txt = File.CreateText(FileName))
            {
                DirectoryInfo dinfo = new DirectoryInfo(GlobalPaths.RootPath);
                FileInfo[] Files = dinfo.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo file in Files)
                {
                    DirectoryInfo localdinfo = new DirectoryInfo(file.DirectoryName);
                    string directory = localdinfo.Name;
                    if (!fileListToIgnore.Contains(file.Name, StringComparer.InvariantCultureIgnoreCase) && !fileListToIgnore.Contains(directory, StringComparer.InvariantCultureIgnoreCase))
                    {
                        txt.WriteLine(file.FullName);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            Util.ConsolePrint("File list generation finished.", 4);
        }
    }
    #endregion
}