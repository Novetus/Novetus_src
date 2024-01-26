#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
#endregion

namespace Novetus.Core
{
    #region File Formats
    public class FileFormat
    {
        #region Client Information
        public class ClientInfo
        {
            public ClientInfo()
            {
                UsesPlayerName = true;
                UsesID = true;
                Description = "";
                Warning = "";
                LegacyMode = false;
                ClientMD5 = "";
                ScriptMD5 = "";
                Fix2007 = false;
                AlreadyHasSecurity = false;
                ClientLoadOptions = Settings.ClientLoadOptions.Client_2008AndUp;
                SeperateFolders = false;
                UsesCustomClientEXEName = false;
                CustomClientEXEName = "";
                CommandLineArgs = "%args%";
            }

            public bool UsesPlayerName { get; set; }
            public bool UsesID { get; set; }
            public string Description { get; set; }
            public string Warning { get; set; }
            public bool LegacyMode { get; set; }
            public string ClientMD5 { get; set; }
            public string ScriptMD5 { get; set; }
            public bool Fix2007 { get; set; }
            public bool AlreadyHasSecurity { get; set; }
            public bool SeperateFolders { get; set; }
            public bool UsesCustomClientEXEName { get; set; }
            public string CustomClientEXEName { get; set; }
            public Settings.ClientLoadOptions ClientLoadOptions { get; set; }
            public string CommandLineArgs { get; set; }
        }
        #endregion

        #region ConfigBase
        public class ConfigBase
        {
            public JSONFile JSON;
            private string Section { get; set; }
            private string Path { get; set; }
            private string FileName { get; set; }
            public string FullPath { get;}

            public virtual Dictionary<string, string> ValueDefaults { get; set; }

            public ConfigBase(string section, string path, string fileName)
            {
                Section = section;
                Path = path;
                FileName = fileName;
                FullPath = Path + "\\" + FileName;

                FilePreLoadEvent();

                bool fileExists = File.Exists(FullPath);

                if (!fileExists)
                {
                    CreateFile();
                }
                else
                {
                    JSON = new JSONFile(FullPath, Section, false);
                }
            }

            public void CreateFile()
            {
                DefineDefaults();

                if (ValueDefaults.Count == 0)
                {
                    ValueDefaults = new Dictionary<string, string>()
                    {
                        {"Error", "There are no default values in your ConfigBase class!"}
                    };
                }

                JSON = new JSONFile(FullPath, Section, true, ValueDefaults);
                GenerateDefaults();
            }

            public virtual void FilePreLoadEvent()
            {
                //fill dictionary here
            }

            public virtual void DefineDefaults()
            {
                //fill dictionary here
            }

            public void GenerateDefaults()
            {
                foreach (string key in ValueDefaults.Keys) 
                { 
                    var value = ValueDefaults[key];

                    if (value != null) 
                    {
                        SaveSetting(key, value);
                    }
                }
            }

            public void LoadAllSettings(string inputPath)
            {
                File.SetAttributes(Path, FileAttributes.Normal);
                File.Replace(inputPath, Path, null);
            }

            public void SaveSetting(string name)
            {
                SaveSetting(Section, name, "");
            }

            public void SaveSetting(string name, string value)
            {
                SaveSetting(Section, name, value);
            }

            public void SaveSetting(string section, string name, string value)
            {
                SaveSettingEvent();
                JSON.JsonReload();
                JSON.JsonWriteValue(section, name, value);
            }

            public void SaveSettingInt(string name, int value)
            {
                SaveSettingInt(Section, name, value);
            }

            public void SaveSettingInt(string section, string name, int value)
            {
                SaveSetting(section, name, value.ToString());
            }

            public void SaveSettingBool(string name, bool value)
            {
                SaveSettingBool(Section, name, value);
            }

            public void SaveSettingBool(string section, string name, bool value)
            {
                SaveSetting(section, name, value.ToString());
            }

            public virtual void SaveSettingEvent()
            {
                //save setting event goes in here.
            }

            public string ReadSetting(string section, string name)
            {
                string value = JSON.JsonReadValue(section, name);

                if (!string.IsNullOrWhiteSpace(value))
                {
                    ReadSettingEvent();
                    return JSON.JsonReadValue(section, name);
                }
                else
                {
                    string defaultval;

                    try
                    {
                        defaultval = ValueDefaults[name];
                    }
                    catch(Exception)
                    {
                        defaultval = "";
                    }

                    SaveSetting(section, name, defaultval);
                    ReadSettingEvent();
                    return JSON.JsonReadValue(section, name);
                }
            }

            public string ReadSetting(string name)
            {
                return ReadSetting(Section, name);
            }

            public int ReadSettingInt(string section, string name)
            {
                bool result = int.TryParse(ReadSetting(section, name), out int value);
                if(result)
                {
                    return value;
                }

                return 0;
            }

            public int ReadSettingInt(string name)
            {
                return ReadSettingInt(Section, name);
            }

            public bool ReadSettingBool(string section, string name)
            {
                bool result = bool.TryParse(ReadSetting(section, name), out bool value);
                if (result)
                {
                    return value;
                }

                return false;
            }

            public bool ReadSettingBool(string name)
            {
                return ReadSettingBool(Section, name);
            }

            public virtual void ReadSettingEvent()
            {
                //read setting event.
            }
        }

        #endregion

        #region Configuration
        public class Config : ConfigBase
        {
            public Config() : base("Config", GlobalPaths.ConfigDir, GlobalPaths.ConfigName) { }

            public Config(string filename) : base("Config", GlobalPaths.ConfigDir, filename) { }

            public override void FilePreLoadEvent()
            {
                FileManagement.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName,
                GlobalPaths.ConfigDir + "\\" + GlobalPaths.TermListFileName);
            }

            public override void DefineDefaults()
            {
                string userName = "";

                try 
                {
                    string[] termspath = File.ReadAllLines(GlobalPaths.ConfigDir + "\\" + GlobalPaths.TermListFileName);
                    var r = new CryptoRandom();
                    var randomLineNumber = r.Next(0, termspath.Length - 1);
                    var line = termspath[randomLineNumber];
                    userName = line + NovetusFuncs.GenerateRandomNumber();
                }
                catch (Exception) 
                {
                    userName = "Player";
                }

                ValueDefaults = new Dictionary<string, string>(){
                    {"SelectedClient", GlobalVars.ProgramInformation.DefaultClient},
                    {"Map", GlobalVars.ProgramInformation.DefaultMap},
                    {"CloseOnLaunch", Util.BoolValue(false)},
                    {"UserID", Util.IntValue(NovetusFuncs.GeneratePlayerID())},
                    {"PlayerName", userName},
                    {"RobloxPort", Util.IntValue(53640)},
                    {"PlayerLimit", Util.IntValue(12)},
                    {"UPnP", Util.BoolValue(false)},
                    {"DisabledAssetSDKHelp", Util.BoolValue(false)},
                    {"DiscordRichPresence", Util.BoolValue(true)},
                    {"MapPath", GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap},
                    {"MapPathSnip", GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap},
                    {"GraphicsMode", Util.IntValue((int)Settings.Mode.Automatic)},
                    {"QualityLevel", Util.IntValue((int)Settings.Level.Automatic)},
                    {"LauncherStyle", (Util.IsWineRunning() ? Util.IntValue((int)Settings.Style.Extended) : Util.IntValue((int)Settings.Style.Stylish))},
                    {"AssetSDKFixerSaveBackups", Util.BoolValue(true)},
                    {"AlternateServerIP", ""},
                    {"ShowServerNotifications", Util.BoolValue(false)},
                    {"ServerBrowserServerName", "Novetus"},
                    {"ServerBrowserServerAddress", ""},
                    {"Priority", Util.IntValue((int)ProcessPriorityClass.RealTime)},
                    {"FirstServerLaunch", Util.BoolValue(true)},
                    {"NewGUI", Util.BoolValue(false)},
                    {"URIQuickConfigure", Util.BoolValue(true)},
                    {"BootstrapperShowUI", Util.BoolValue(true)},
                    {"WebProxyInitialSetupRequired", Util.BoolValue(true)},
                    {"WebProxyEnabled", Util.BoolValue(false)}
                };
            }
        }
        #endregion

        #region Customization Configuration
        public class CustomizationConfig : ConfigBase
        {
            public CustomizationConfig() : base("Customization", GlobalPaths.ConfigDir, GlobalPaths.ConfigNameCustomization) { }
            public CustomizationConfig(string filename) : base("Customization", GlobalPaths.ConfigDir, filename) { }

            public override void DefineDefaults()
            {
                ValueDefaults = new Dictionary<string, string>(){
                    {"Hat1", "NoHat.rbxm"},
                    {"Hat2", "NoHat.rbxm"},
                    {"Hat3", "NoHat.rbxm"},
                    {"Face", "DefaultFace.rbxm"},
                    {"Head", "DefaultHead.rbxm"},
                    {"TShirt", "NoTShirt.rbxm"},
                    {"Shirt", "NoShirt.rbxm"},
                    {"Pants", "NoPants.rbxm"},
                    {"Icon", "NBC"},
                    {"Extra", "NoExtra.rbxm"},
                    {"HeadColorID", Util.IntValue(24)},
                    {"TorsoColorID", Util.IntValue(23)},
                    {"LeftArmColorID", Util.IntValue(24)},
                    {"RightArmColorID", Util.IntValue(24)},
                    {"LeftLegColorID", Util.IntValue(119)},
                    {"RightLegColorID", Util.IntValue(119)},
                    {"HeadColorString", "Color [A=255, R=245, G=205, B=47]"},
                    {"TorsoColorString", "Color [A=255, R=13, G=105, B=172]"},
                    {"LeftArmColorString", "Color [A=255, R=245, G=205, B=47]"},
                    {"RightArmColorString", "Color [A=255, R=245, G=205, B=47]"},
                    {"LeftLegColorString", "Color [A=255, R=164, G=189, B=71]"},
                    {"RightLegColorString", "Color [A=255, R=164, G=189, B=71]"},
                    {"ExtraSelectionIsHat", Util.BoolValue(false)},
                    {"ShowHatsInExtra", Util.BoolValue(false)},
                    {"CharacterID", ""}
                };
            }
        }
        #endregion

        #region Program Information
        public class ProgramInfo
        {
            // Defaults are hacky but fixes the errors on intital startup.
            public ProgramInfo()
            {
                Version = "";
                Branch = "";
                DefaultClient = "";
                RegisterClient1 = "";
                RegisterClient2 = "";
                DefaultMap = "";
                VersionName = "";
                //HACK
                NetVersion = ".NET Framework 4.5.1";
                InitialBootup = true;
                IsSnapshot = false;
            }

            public string Version { get; set; }
            public string Branch { get; set; }
            public string DefaultClient { get; set; }
            public string RegisterClient1 { get; set; }
            public string RegisterClient2 { get; set; }
            public string DefaultMap { get; set; }
            public string VersionName { get; set; }
            public string NetVersion { get; set; }
            public bool InitialBootup { get; set; }
            public bool IsSnapshot { get; set; }
        }
        #endregion
    }
    #endregion

    #region Part Color Options
    [JsonObject(MemberSerialization.OptIn)]
    public class PartColor
    {
        [JsonProperty]
        public string ColorName;
        [JsonProperty]
        public int ColorID;
        [JsonProperty]
        public string ColorRGB;
        public Color ColorObject;
        public string ColorGroup;
        public string ColorRawName;
        public Bitmap ColorImage;
    }

    public class PartColorLoader
    {
        public static PartColor[] GetPartColors()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
            {
                List<PartColor> colors = JsonConvert.DeserializeObject<List<PartColor>>(File.ReadAllText(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName));

                foreach (var item in colors)
                {
                    string colorFixed = Regex.Replace(item.ColorRGB, @"[\[\]\{\}\(\)\<\> ]", "");
                    string[] rgbValues = colorFixed.Split(',');
                    item.ColorObject = Color.FromArgb(ConvertSafe.ToInt32Safe(rgbValues[0]), ConvertSafe.ToInt32Safe(rgbValues[1]), ConvertSafe.ToInt32Safe(rgbValues[2]));

                    if (!(item.ColorName.Contains("[") && item.ColorName.Contains("]")))
                    {
                        item.ColorRawName = item.ColorName;
                        item.ColorName = "[Uncategorized]" + item.ColorName;
                    }
                    else
                    {
                        item.ColorRawName = item.ColorName;
                    }

                    int pFrom = item.ColorName.IndexOf("[");
                    int pTo = item.ColorName.IndexOf("]");
                    item.ColorGroup = item.ColorName.Substring(pFrom + 1, pTo - pFrom - 1);
                    item.ColorName = item.ColorName.Replace(item.ColorGroup, "").Replace("[", "").Replace("]", "");
                    item.ColorImage = GeneratePartColorIcon(item, 128);
                }

                return colors.ToArray();
            }
            else
            {
                return null;
            }
        }

        //make faster
        public static void AddPartColorsToListView(PartColor[] PartColorList, ListView ColorView, int imgsize, bool showIDs = false)
        {
            try
            {
                ImageList ColorImageList = new ImageList();
                ColorImageList.ImageSize = new Size(imgsize, imgsize);
                ColorImageList.ColorDepth = ColorDepth.Depth32Bit;
                ColorView.LargeImageList = ColorImageList;
                ColorView.SmallImageList = ColorImageList;

                foreach (var item in PartColorList)
                {
                    var lvi = new ListViewItem(item.ColorName);
                    lvi.Tag = item.ColorID;

                    if (showIDs)
                    {
                        lvi.Text = lvi.Text + " (" + item.ColorID + ")";
                    }

                    var group = ColorView.Groups.Cast<ListViewGroup>().FirstOrDefault(g => g.Header == item.ColorGroup);

                    if (group == null)
                    {
                        group = new ListViewGroup(item.ColorGroup);
                        ColorView.Groups.Add(group);
                    }

                    lvi.Group = group;

                    if (item.ColorImage != null)
                    {
                        ColorImageList.Images.Add(item.ColorName, item.ColorImage);
                        lvi.ImageIndex = ColorImageList.Images.IndexOfKey(item.ColorName);
                    }

                    ColorView.Items.Add(lvi);
                }

                /*foreach (var group in ColorView.Groups.Cast<ListViewGroup>())
                {
                    group.Header = group.Header + " (" + group.Items.Count + ")";
                }*/
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
            }
        }

        public static Bitmap GeneratePartColorIcon(PartColor color, int imgsize)
        {
            try
            {
                Bitmap Bmp = new Bitmap(imgsize, imgsize, PixelFormat.Format32bppArgb);
                using (Graphics gfx = Graphics.FromImage(Bmp))
                using (SolidBrush brush = new SolidBrush(color.ColorObject))
                {
                    gfx.FillRectangle(brush, 0, 0, imgsize, imgsize);
                }

                return Bmp;
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return null;
            }
        }

        public static PartColor FindPartColorByName(PartColor[] colors, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
            {
                return colors.SingleOrDefault(item => query.Contains(item.ColorName));
            }
            else
            {
                return null;
            }
        }

        public static PartColor FindPartColorByID(PartColor[] colors, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
            {
                return colors.SingleOrDefault(item => query.Contains(item.ColorID.ToString()));
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    #region Content Provider Options
    [JsonObject(MemberSerialization.OptIn)]
    public class Provider
    {
        [JsonProperty]
        public string Name;
        [JsonProperty]
        public string URL;
        [JsonProperty]
        public string Icon;
    }

    public class ContentProviderLoader
    {
        public static Provider[] GetContentProviders()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                List<Provider> providers = JsonConvert.DeserializeObject<List<Provider>>(File.ReadAllText(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName));
                return providers.ToArray();
            }
            else
            {
                return null;
            }
        }

        public static Provider FindContentProviderByName(Provider[] providers, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                return providers.SingleOrDefault(item => query.Contains(item.Name));
            }
            else
            {
                return null;
            }
        }

        public static Provider FindContentProviderByURL(Provider[] providers, string query)
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                return providers.SingleOrDefault(item => query.Contains(item.URL));
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    #region Settings
    public class Settings
    {
        public enum Mode
        {
            Automatic = 0,
            OpenGLStable = 1,
            OpenGLExperimental = 2,
            DirectX = 3
        }

        public enum Level
        {
            Automatic = 0,
            VeryLow = 1,
            Low = 2,
            Medium = 3,
            High = 4,
            Ultra = 5,
            Custom = 6
        }

        public enum Style
        {
            None = 0,
            Extended = 1,
            Compact = 2,
            Stylish = 3
        }

        public enum ClientLoadOptions
        {
            Client_2007_NoGraphicsOptions = 0,
            Client_2007 = 1,
            Client_2008AndUp = 2,
            Client_2008AndUp_LegacyOpenGL = 3,
            Client_2008AndUp_QualityLevel21 = 4,
            Client_2008AndUp_NoGraphicsOptions = 5,
            Client_2008AndUp_ForceAutomatic = 6,
            Client_2008AndUp_ForceAutomaticQL21 = 7,
            Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL = 8
        }

        public static ClientLoadOptions GetClientLoadOptionsForBool(bool level)
        {
            switch (level)
            {
                case false:
                    return ClientLoadOptions.Client_2008AndUp;
                default:
                    return ClientLoadOptions.Client_2007_NoGraphicsOptions;
            }
        }

        public static string GetPathForClientLoadOptions(ClientLoadOptions level)
        {
            string localAppdataRobloxPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Roblox";
            string appdataRobloxPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Roblox";

            if (!Directory.Exists(localAppdataRobloxPath))
            {
                Directory.CreateDirectory(localAppdataRobloxPath);
            }

            if (!Directory.Exists(appdataRobloxPath))
            {
                Directory.CreateDirectory(appdataRobloxPath);
            }

            switch (level)
            {
                case ClientLoadOptions.Client_2008AndUp_QualityLevel21:
                case ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
                case ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions:
                case ClientLoadOptions.Client_2008AndUp_ForceAutomatic:
                case ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21:
                case ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                case ClientLoadOptions.Client_2008AndUp:
                    return localAppdataRobloxPath;
                default:
                    return appdataRobloxPath;
            }
        }
    }

    #endregion

    #region Icon Loader

    public class IconLoader
    {
        private OpenFileDialog openFileDialog1;
        private string installOutcome = "";
        public bool CopyToItemDir = false;
        public string ItemDir = "";
        public string ItemName = "";
        public string ItemPath = "";

        public IconLoader()
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select an icon .png file",
                Filter = "Portable Network Graphics image (*.png)|*.png",
                Title = "Open icon .png"
            };
        }

        public void setInstallOutcome(string text)
        {
            installOutcome = text;
        }

        public string getInstallOutcome()
        {
            return installOutcome;
        }

        public void LoadImage()
        {
            string ItemNameFixed = ItemName.Replace(" ", "");
            string dir = CopyToItemDir ? ItemDir + "\\" + ItemNameFixed : GlobalPaths.extradir + "\\icons\\" + GlobalVars.UserConfiguration.ReadSetting("PlayerName");

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IOSafe.File.Copy(openFileDialog1.FileName, dir + ".png", true);

                    if (CopyToItemDir)
                    {
                        ItemPath = ItemDir + "\\" + ItemNameFixed + ".png";
                    }

                    installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
                }
                catch (Exception ex)
                {
                    installOutcome = "Error when installing icon: " + ex.Message;
                    Util.LogExceptions(ex);
                }
            }
        }
    }
    #endregion

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

        public static bool ResetMapIfNecessary()
        {
            if (!File.Exists(GlobalVars.UserConfiguration.ReadSetting("MapPath")))
            {
                GlobalVars.UserConfiguration.SaveSetting("Map", GlobalVars.ProgramInformation.DefaultMap);
                GlobalVars.UserConfiguration.SaveSetting("MapPath", GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap);
                GlobalVars.UserConfiguration.SaveSetting("MapPathSnip", GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap);
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
                    GlobalVars.PartColorList = PartColorLoader.GetPartColors();
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
                    tempList = PartColorLoader.GetPartColors();
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

            GlobalVars.UserConfiguration = new FileFormat.Config();
            GlobalVars.UserConfiguration.SaveSetting("SelectedClient", GlobalVars.ProgramInformation.DefaultClient);
            GlobalVars.UserConfiguration.SaveSetting("Map", GlobalVars.ProgramInformation.DefaultMap);
            GlobalVars.UserConfiguration.SaveSetting("MapPath", GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap);
            GlobalVars.UserConfiguration.SaveSetting("MapPathSnip", GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap);
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
            GlobalVars.UserCustomization = new FileFormat.CustomizationConfig();
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