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

        #region Configuration
        public class Config
        {
            public Config()
            {
                SelectedClient = "";
                Map = "";
                CloseOnLaunch = false;
                UserID = 0;
                PlayerName = "Player";
                RobloxPort = 53640;
                PlayerLimit = 12;
                UPnP = false;
                DisabledAssetSDKHelp = false;
                DiscordPresence = true;
                MapPath = "";
                MapPathSnip = "";
                GraphicsMode = Settings.Mode.Automatic;
                ReShade = false;
                QualityLevel = Settings.Level.Automatic;
                LauncherStyle = Settings.Style.Stylish;
                ReShadeFPSDisplay = false;
                ReShadePerformanceMode = false;
                AssetSDKFixerSaveBackups = true;
                AlternateServerIP = "";
                DisableReshadeDelete = false;
                ShowServerNotifications = false;
                ServerBrowserServerName = "Novetus";
                ServerBrowserServerAddress = "localhost";
                Priority = ProcessPriorityClass.RealTime;
                FirstServerLaunch = true;
                NewGUI = false;
                URIQuickConfigure = true;
                BootstrapperShowUI = true;
                WebProxyInitialSetupRequired = true;
                WebProxyEnabled = false;
            }

            public string SelectedClient { get; set; }
            public string Map { get; set; }
            public bool CloseOnLaunch { get; set; }
            public int UserID { get; set; }
            public string PlayerName { get; set; }
            public int RobloxPort { get; set; }
            public int PlayerLimit { get; set; }
            public bool UPnP { get; set; }
            public bool DisabledAssetSDKHelp { get; set; }
            public bool DiscordPresence { get; set; }
            public string MapPath { get; set; }
            public string MapPathSnip { get; set; }
            public Settings.Mode GraphicsMode { get; set; }
            public bool ReShade { get; set; }
            public Settings.Level QualityLevel { get; set; }
            public Settings.Style LauncherStyle { get; set; }
            public bool ReShadeFPSDisplay { get; set; }
            public bool ReShadePerformanceMode { get; set; }
            public bool AssetSDKFixerSaveBackups { get; set; }
            public string AlternateServerIP { get; set; }
            public bool DisableReshadeDelete { get; set; }
            public bool ShowServerNotifications { get; set; }
            public string ServerBrowserServerName { get; set; }
            public string ServerBrowserServerAddress { get; set; }
            public ProcessPriorityClass Priority { get; set; }
            public bool FirstServerLaunch { get; set; }
            public bool NewGUI { get; set; }
            public bool URIQuickConfigure { get; set; }
            public bool BootstrapperShowUI { get; set; }
            public bool WebProxyInitialSetupRequired { get; set; }
            public bool WebProxyEnabled { get; set; }
        }
        #endregion

        #region Customization Configuration
        public class CustomizationConfig
        {
            public CustomizationConfig()
            {
                Hat1 = "NoHat.rbxm";
                Hat2 = "NoHat.rbxm";
                Hat3 = "NoHat.rbxm";
                Face = "DefaultFace.rbxm";
                Head = "DefaultHead.rbxm";
                TShirt = "NoTShirt.rbxm";
                Shirt = "NoShirt.rbxm";
                Pants = "NoPants.rbxm";
                Icon = "NBC";
                Extra = "NoExtra.rbxm";
                HeadColorID = 24;
                TorsoColorID = 23;
                LeftArmColorID = 24;
                RightArmColorID = 24;
                LeftLegColorID = 119;
                RightLegColorID = 119;
                HeadColorString = "Color [A=255, R=245, G=205, B=47]";
                TorsoColorString = "Color [A=255, R=13, G=105, B=172]";
                LeftArmColorString = "Color [A=255, R=245, G=205, B=47]";
                RightArmColorString = "Color [A=255, R=245, G=205, B=47]";
                LeftLegColorString = "Color [A=255, R=164, G=189, B=71]";
                RightLegColorString = "Color [A=255, R=164, G=189, B=71]";
                ExtraSelectionIsHat = false;
                ShowHatsInExtra = false;
                CharacterID = "";
            }

            public string Hat1 { get; set; }
            public string Hat2 { get; set; }
            public string Hat3 { get; set; }
            public string Face { get; set; }
            public string Head { get; set; }
            public string TShirt { get; set; }
            public string Shirt { get; set; }
            public string Pants { get; set; }
            public string Icon { get; set; }
            public string Extra { get; set; }
            public int HeadColorID { get; set; }
            public int TorsoColorID { get; set; }
            public int LeftArmColorID { get; set; }
            public int RightArmColorID { get; set; }
            public int LeftLegColorID { get; set; }
            public int RightLegColorID { get; set; }
            public string HeadColorString { get; set; }
            public string TorsoColorString { get; set; }
            public string LeftArmColorString { get; set; }
            public string RightArmColorString { get; set; }
            public string LeftLegColorString { get; set; }
            public string RightLegColorString { get; set; }
            public bool ExtraSelectionIsHat { get; set; }
            public bool ShowHatsInExtra { get; set; }
            public string CharacterID { get; set; }
        }
        #endregion

        #region Program Information
        public class ProgramInfo
        {
            public ProgramInfo()
            {
                Version = "";
                Branch = "";
                DefaultClient = "";
                RegisterClient1 = "";
                RegisterClient2 = "";
                DefaultMap = "";
                //HACK
#if NET4
                NetVersion = ".NET 4.0";
#elif NET481
                NetVersion = ".NET 4.8";
#endif
                InitialBootup = true;
            }

            public string Version { get; set; }
            public string Branch { get; set; }
            public string DefaultClient { get; set; }
            public string RegisterClient1 { get; set; }
            public string RegisterClient2 { get; set; }
            public string DefaultMap { get; set; }
            public string NetVersion { get; set; }
            public bool InitialBootup { get; set; }
        }
        #endregion
    }
    #endregion

    #region Part Color Options
    public class PartColor
    {
        public string ColorName;
        public int ColorID;
        public string ColorRGB;
        [XmlIgnore]
        public Color ColorObject;
        [XmlIgnore]
        public string ColorGroup;
        [XmlIgnore]
        public string ColorRawName;
        [XmlIgnore]
        public Bitmap ColorImage;
    }

    [XmlRoot("PartColors")]
    public class PartColors
    {
        [XmlArray("ColorList")]
        public PartColor[] ColorList;
    }

    public class PartColorLoader
    {
        public static PartColor[] GetPartColors()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PartColors));
                PartColors colors;

                using (FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName, FileMode.Open))
                {
                    colors = (PartColors)serializer.Deserialize(fs);
                }

                foreach (var item in colors.ColorList)
                {
                    string colorFixed = Regex.Replace(item.ColorRGB, @"[\[\]\{\}\(\)\<\> ]", "");
                    string[] rgbValues = colorFixed.Split(',');
                    item.ColorObject = Color.FromArgb(Convert.ToInt32(rgbValues[0]), Convert.ToInt32(rgbValues[1]), Convert.ToInt32(rgbValues[2]));

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

                return colors.ColorList;
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
    public class Provider
    {
        public string Name;
        public string URL;
        public string Icon;
    }

    [XmlRoot("ContentProviders")]
    public class ContentProviders
    {
        [XmlArray("Providers")]
        public Provider[] Providers;
    }

    public class OnlineClothing
    {
        public static Provider[] GetContentProviders()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ContentProviders));
                ContentProviders providers;

                using (FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ContentProviderXMLName, FileMode.Open))
                {
                    providers = (ContentProviders)serializer.Deserialize(fs);
                }

                return providers.Providers;
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
            string dir = CopyToItemDir ? ItemDir + "\\" + ItemNameFixed : GlobalPaths.extradir + "\\icons\\" + GlobalVars.UserConfiguration.PlayerName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Util.FixedFileCopy(openFileDialog1.FileName, dir + ".png", true);

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
        public static void ReadInfoFile(string infopath, bool other = false, string exepath = "")
        {
            //READ
            string versionbranch, defaultclient, defaultmap, regclient1,
                regclient2, extendedversionnumber, extendedversiontemplate,
                extendedversionrevision, extendedversioneditchangelog, isLite,
                initialBootup;

            INIFile ini = new INIFile(infopath);

            string section = "ProgramInfo";

            //not using the GlobalVars definitions as those are empty until we fill them in.
            versionbranch = ini.IniReadValue(section, "Branch", "0.0");
            defaultclient = ini.IniReadValue(section, "DefaultClient", "2009E");
            defaultmap = ini.IniReadValue(section, "DefaultMap", "Dev - Baseplate2048.rbxl");
            regclient1 = ini.IniReadValue(section, "UserAgentRegisterClient1", "2007M");
            regclient2 = ini.IniReadValue(section, "UserAgentRegisterClient2", "2009L");
            extendedversionnumber = ini.IniReadValue(section, "ExtendedVersionNumber", "False");
            extendedversioneditchangelog = ini.IniReadValue(section, "ExtendedVersionEditChangelog", "False");
            extendedversiontemplate = ini.IniReadValue(section, "ExtendedVersionTemplate", "%version%");
            extendedversionrevision = ini.IniReadValue(section, "ExtendedVersionRevision", "-1");
            isLite = ini.IniReadValue(section, "IsLite", "False");
            initialBootup = ini.IniReadValue(section, "InitialBootup", "True");

            try
            {
                GlobalVars.ExtendedVersionNumber = Convert.ToBoolean(extendedversionnumber);
                if (GlobalVars.ExtendedVersionNumber)
                {
                    if (other)
                    {
                        if (!string.IsNullOrWhiteSpace(exepath))
                        {
                            var versionInfo = FileVersionInfo.GetVersionInfo(exepath);
                            GlobalVars.ProgramInformation.Version = extendedversiontemplate.Replace("%version%", versionbranch)
                                .Replace("%build%", versionInfo.ProductBuildPart.ToString())
                                .Replace("%revision%", versionInfo.FilePrivatePart.ToString())
                                .Replace("%extended-revision%", (!extendedversionrevision.Equals("-1") ? extendedversionrevision : ""));
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        GlobalVars.ProgramInformation.Version = extendedversiontemplate.Replace("%version%", versionbranch)
                            .Replace("%build%", Assembly.GetExecutingAssembly().GetName().Version.Build.ToString())
                            .Replace("%revision%", Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString())
                            .Replace("%extended-revision%", (!extendedversionrevision.Equals("-1") ? extendedversionrevision : ""));
                    }

                    bool changelogedit = Convert.ToBoolean(extendedversioneditchangelog);

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
                GlobalVars.ProgramInformation.InitialBootup = Convert.ToBoolean(initialBootup);
                GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
                GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
                GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
                GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                ReadInfoFile(infopath, other);
            }
        }

        public static void TurnOffInitialSequence()
        {
            //READ
            INIFile ini = new INIFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName);
            string section = "ProgramInfo";

            string initialBootup = ini.IniReadValue(section, "InitialBootup", "True");
            if (Convert.ToBoolean(initialBootup) == true)
            {
                ini.IniWriteValue(section, "InitialBootup", "False");
            }
        }

        public static string ConfigUseOldValIfExists(INIFile ini, string section, string oldKey, string newKey, string val, bool write)
        {
            if (write)
            {
                if (!ini.IniValueExists(newKey))
                {
                    if (GlobalVars.ProgramInformation.InitialBootup)
                    {
                        if (ini.IniValueExists(oldKey))
                        {
                            ini.IniWriteValue(section, oldKey, val);
                        }
                        else
                        {
                            ini.IniWriteValue(section, newKey, val);
                        }
                    }
                    else
                    {
                        ini.IniWriteValue(section, oldKey, val);
                    }
                }
                else
                {
                    ini.IniWriteValue(section, newKey, val);
                }

                return "";
            }
            else
            {
                if (ini.IniValueExists(newKey))
                {
                    return ini.IniReadValue(section, newKey, val);
                }
                else
                {
                    return ini.IniReadValue(section, oldKey, val);
                }
            }
        }

        private static int ValueInt(string val, int defaultVal)
        {
            int res;
            if (int.TryParse(val, out res))
            {
                return Convert.ToInt32(val);
            }
            else
            {
                return defaultVal;
            }
        }

        private static bool ValueBool(string val, bool defaultVal)
        {
            bool res;
            if (bool.TryParse(val, out res))
            {
                return Convert.ToBoolean(val);
            }
            else
            {
                return defaultVal;
            }
        }

        public static void Config(string cfgpath, bool write, bool doubleCheck = false)
        {
            bool forcewrite = false;

            if (!File.Exists(cfgpath))
            {
                // force write mode on if the file doesn't exist.
                write = true;
                forcewrite = true;
            }

            if (write)
            {
                if (Util.IsWineRunning())
                {
                    GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Extended;
                }

                //WRITE
                INIFile ini = new INIFile(cfgpath);

                string section = "Config";

                ini.IniWriteValue(section, "CloseOnLaunch", GlobalVars.UserConfiguration.CloseOnLaunch.ToString());
                ini.IniWriteValue(section, "UserID", GlobalVars.UserConfiguration.UserID.ToString());
                ini.IniWriteValue(section, "PlayerName", GlobalVars.UserConfiguration.PlayerName.ToString());
                ini.IniWriteValue(section, "SelectedClient", GlobalVars.UserConfiguration.SelectedClient.ToString());
                ini.IniWriteValue(section, "Map", GlobalVars.UserConfiguration.Map.ToString());
                ini.IniWriteValue(section, "RobloxPort", GlobalVars.UserConfiguration.RobloxPort.ToString());
                ini.IniWriteValue(section, "PlayerLimit", GlobalVars.UserConfiguration.PlayerLimit.ToString());
                ini.IniWriteValue(section, "UPnP", GlobalVars.UserConfiguration.UPnP.ToString());
                ini.IniWriteValue(section, "DiscordRichPresence", GlobalVars.UserConfiguration.DiscordPresence.ToString());
                ini.IniWriteValue(section, "MapPath", GlobalVars.UserConfiguration.MapPath.ToString());
                ini.IniWriteValue(section, "MapPathSnip", GlobalVars.UserConfiguration.MapPathSnip.ToString());
                ini.IniWriteValue(section, "GraphicsMode", ((int)GlobalVars.UserConfiguration.GraphicsMode).ToString());
                ini.IniWriteValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
                ini.IniWriteValue(section, "QualityLevel", ((int)GlobalVars.UserConfiguration.QualityLevel).ToString());
                ini.IniWriteValue(section, "Style", ((int)GlobalVars.UserConfiguration.LauncherStyle).ToString());
                ini.IniWriteValue(section, "AlternateServerIP", GlobalVars.UserConfiguration.AlternateServerIP.ToString());
                ini.IniWriteValue(section, "DisableReshadeDelete", GlobalVars.UserConfiguration.DisableReshadeDelete.ToString());
                ini.IniWriteValue(section, "ShowServerNotifications", GlobalVars.UserConfiguration.ShowServerNotifications.ToString());
                ini.IniWriteValue(section, "ServerBrowserServerName", GlobalVars.UserConfiguration.ServerBrowserServerName.ToString());
                ini.IniWriteValue(section, "ServerBrowserServerAddress", GlobalVars.UserConfiguration.ServerBrowserServerAddress.ToString());
                ini.IniWriteValue(section, "ClientLaunchPriority", ((int)GlobalVars.UserConfiguration.Priority).ToString());
                ini.IniWriteValue(section, "FirstServerLaunch", GlobalVars.UserConfiguration.FirstServerLaunch.ToString());
                ini.IniWriteValue(section, "NewGUI", GlobalVars.UserConfiguration.NewGUI.ToString());
                ini.IniWriteValue(section, "URIQuickConfigure", GlobalVars.UserConfiguration.URIQuickConfigure.ToString());
                ini.IniWriteValue(section, "BootstrapperShowUI", GlobalVars.UserConfiguration.BootstrapperShowUI.ToString());
                ini.IniWriteValue(section, "WebProxyInitialSetupRequired", GlobalVars.UserConfiguration.WebProxyInitialSetupRequired.ToString());
                ini.IniWriteValue(section, "WebProxyEnabled", GlobalVars.UserConfiguration.WebProxyEnabled.ToString());
                ConfigUseOldValIfExists(ini, section, "ItemMakerDisableHelpMessage", "AssetSDKDisableHelpMessage", GlobalVars.UserConfiguration.DisabledAssetSDKHelp.ToString(), write);
                ConfigUseOldValIfExists(ini, section, "AssetLocalizerSaveBackups", "AssetSDKFixerSaveBackups", GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups.ToString(), write);

                if (forcewrite)
                {
                    // try again....
                    Config(cfgpath, false, doubleCheck);
                }
            }
            else
            {
                try
                {
                    //READ
                    string closeonlaunch, userid, name, selectedclient,
                    map, port, limit, upnp,
                    disablehelpmessage, discord, mappath, mapsnip,
                    graphics, reshade, qualitylevel, style, savebackups, altIP,
                    disReshadeDel, showNotifs, SB_Name, SB_Address, priority,
                    firstServerLaunch, newgui, quickconfigure, bootstrapper,
                    webproxysetup, webproxy;

                    INIFile ini = new INIFile(cfgpath);

                    string section = "Config";

                    closeonlaunch = ini.IniReadValue(section, "CloseOnLaunch", GlobalVars.UserConfiguration.CloseOnLaunch.ToString());
                    userid = ini.IniReadValue(section, "UserID", GlobalVars.UserConfiguration.UserID.ToString());
                    name = ini.IniReadValue(section, "PlayerName", GlobalVars.UserConfiguration.PlayerName.ToString());
                    selectedclient = ini.IniReadValue(section, "SelectedClient", GlobalVars.UserConfiguration.SelectedClient.ToString());
                    map = ini.IniReadValue(section, "Map", GlobalVars.UserConfiguration.Map.ToString());
                    port = ini.IniReadValue(section, "RobloxPort", GlobalVars.UserConfiguration.RobloxPort.ToString());
                    limit = ini.IniReadValue(section, "PlayerLimit", GlobalVars.UserConfiguration.PlayerLimit.ToString());
                    upnp = ini.IniReadValue(section, "UPnP", GlobalVars.UserConfiguration.UPnP.ToString());
                    discord = ini.IniReadValue(section, "DiscordRichPresence", GlobalVars.UserConfiguration.DiscordPresence.ToString());
                    mappath = ini.IniReadValue(section, "MapPath", GlobalVars.UserConfiguration.MapPath.ToString());
                    mapsnip = ini.IniReadValue(section, "MapPathSnip", GlobalVars.UserConfiguration.MapPathSnip.ToString());
                    graphics = ini.IniReadValue(section, "GraphicsMode", ((int)GlobalVars.UserConfiguration.GraphicsMode).ToString());
                    reshade = ini.IniReadValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
                    qualitylevel = ini.IniReadValue(section, "QualityLevel", ((int)GlobalVars.UserConfiguration.QualityLevel).ToString());
                    style = ini.IniReadValue(section, "Style", ((int)GlobalVars.UserConfiguration.LauncherStyle).ToString());
                    altIP = ini.IniReadValue(section, "AlternateServerIP", GlobalVars.UserConfiguration.AlternateServerIP.ToString());
                    disReshadeDel = ini.IniReadValue(section, "DisableReshadeDelete", GlobalVars.UserConfiguration.DisableReshadeDelete.ToString());
                    showNotifs = ini.IniReadValue(section, "ShowServerNotifications", GlobalVars.UserConfiguration.ShowServerNotifications.ToString());
                    SB_Name = ini.IniReadValue(section, "ServerBrowserServerName", GlobalVars.UserConfiguration.ServerBrowserServerName.ToString());
                    SB_Address = ini.IniReadValue(section, "ServerBrowserServerAddress", GlobalVars.UserConfiguration.ServerBrowserServerAddress.ToString());
                    priority = ini.IniReadValue(section, "ClientLaunchPriority", ((int)GlobalVars.UserConfiguration.Priority).ToString());
                    firstServerLaunch = ini.IniReadValue(section, "FirstServerLaunch", GlobalVars.UserConfiguration.FirstServerLaunch.ToString());
                    newgui = ini.IniReadValue(section, "NewGUI", GlobalVars.UserConfiguration.NewGUI.ToString());
                    quickconfigure = ini.IniReadValue(section, "URIQuickConfigure", GlobalVars.UserConfiguration.URIQuickConfigure.ToString());
                    bootstrapper = ini.IniReadValue(section, "BootstrapperShowUI", GlobalVars.UserConfiguration.BootstrapperShowUI.ToString());
                    webproxysetup = ini.IniReadValue(section, "WebProxyInitialSetupRequired", GlobalVars.UserConfiguration.WebProxyInitialSetupRequired.ToString());
                    webproxy = ini.IniReadValue(section, "WebProxyEnabled", GlobalVars.UserConfiguration.WebProxyEnabled.ToString());
                    disablehelpmessage = ConfigUseOldValIfExists(ini, section, "ItemMakerDisableHelpMessage", "AssetSDKDisableHelpMessage", GlobalVars.UserConfiguration.DisabledAssetSDKHelp.ToString(), write);
                    savebackups = ConfigUseOldValIfExists(ini, section, "AssetLocalizerSaveBackups", "AssetSDKFixerSaveBackups", GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups.ToString(), write);

                    FileFormat.Config DefaultConfiguration = new FileFormat.Config();
                    DefaultConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
                    DefaultConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
                    DefaultConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
                    DefaultConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;

                    GlobalVars.UserConfiguration.CloseOnLaunch = ValueBool(closeonlaunch, DefaultConfiguration.CloseOnLaunch);

                    if (userid.Equals("0"))
                    {
                        NovetusFuncs.GeneratePlayerID();
                        Config(cfgpath, true);
                    }
                    else
                    {
                        GlobalVars.UserConfiguration.UserID = ValueInt(userid, DefaultConfiguration.UserID);
                    }

                    GlobalVars.UserConfiguration.PlayerName = name;
                    GlobalVars.UserConfiguration.SelectedClient = selectedclient;
                    GlobalVars.UserConfiguration.Map = map;
                    GlobalVars.UserConfiguration.RobloxPort = ValueInt(port, DefaultConfiguration.RobloxPort);
                    GlobalVars.UserConfiguration.PlayerLimit = ValueInt(limit, DefaultConfiguration.PlayerLimit);
                    GlobalVars.UserConfiguration.UPnP = ValueBool(upnp, DefaultConfiguration.UPnP);
                    GlobalVars.UserConfiguration.DisabledAssetSDKHelp = ValueBool(disablehelpmessage, DefaultConfiguration.DisabledAssetSDKHelp);
                    GlobalVars.UserConfiguration.DiscordPresence = ValueBool(discord, DefaultConfiguration.DiscordPresence);
                    GlobalVars.UserConfiguration.MapPathSnip = mapsnip;
                    GlobalVars.UserConfiguration.GraphicsMode = (Settings.Mode)ValueInt(graphics, Convert.ToInt32(DefaultConfiguration.GraphicsMode));
                    GlobalVars.UserConfiguration.ReShade = ValueBool(reshade, DefaultConfiguration.ReShade);
                    GlobalVars.UserConfiguration.QualityLevel = (Settings.Level)ValueInt(qualitylevel, Convert.ToInt32(DefaultConfiguration.QualityLevel));
                    GlobalVars.UserConfiguration.LauncherStyle = (Settings.Style)ValueInt(style, Convert.ToInt32(DefaultConfiguration.LauncherStyle));
                    GlobalVars.UserConfiguration.AssetSDKFixerSaveBackups = ValueBool(savebackups, DefaultConfiguration.AssetSDKFixerSaveBackups);
                    GlobalVars.UserConfiguration.AlternateServerIP = altIP;
                    GlobalVars.UserConfiguration.DisableReshadeDelete = ValueBool(disReshadeDel, DefaultConfiguration.DisableReshadeDelete);
                    GlobalVars.UserConfiguration.ShowServerNotifications = ValueBool(showNotifs, DefaultConfiguration.ShowServerNotifications);
                    GlobalVars.UserConfiguration.ServerBrowserServerName = SB_Name;
                    GlobalVars.UserConfiguration.ServerBrowserServerAddress = SB_Address;
                    GlobalVars.UserConfiguration.Priority = (ProcessPriorityClass)ValueInt(priority, Convert.ToInt32(DefaultConfiguration.Priority));
                    GlobalVars.UserConfiguration.FirstServerLaunch = ValueBool(firstServerLaunch, DefaultConfiguration.FirstServerLaunch);
                    GlobalVars.UserConfiguration.NewGUI = ValueBool(newgui, DefaultConfiguration.NewGUI);
                    GlobalVars.UserConfiguration.URIQuickConfigure = ValueBool(quickconfigure, DefaultConfiguration.URIQuickConfigure);
                    GlobalVars.UserConfiguration.BootstrapperShowUI = ValueBool(bootstrapper, DefaultConfiguration.BootstrapperShowUI);
                    GlobalVars.UserConfiguration.WebProxyInitialSetupRequired = ValueBool(webproxysetup, DefaultConfiguration.WebProxyInitialSetupRequired);
                    GlobalVars.UserConfiguration.WebProxyEnabled = ValueBool(webproxy, DefaultConfiguration.WebProxyEnabled);

                    string oldMapath = Path.GetDirectoryName(GlobalVars.UserConfiguration.MapPath);
                    //update the map path if the file doesn't exist and write to config.
                    if (oldMapath.Equals(GlobalPaths.MapsDir.Replace(@"\\", @"\")) && File.Exists(mappath))
                    {
                        GlobalVars.UserConfiguration.MapPath = mappath;
                    }
                    else
                    {
                        GlobalVars.UserConfiguration.MapPath = GlobalPaths.BasePath + @"\\" + GlobalVars.UserConfiguration.MapPathSnip;
                        Config(cfgpath, true);
                    }

                    if (ResetMapIfNecessary())
                    {
                        Config(cfgpath, true);
                    }
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    Config(cfgpath, true);
                }
            }

            if (!forcewrite)
            {
                //Powered by https://github.com/davcs86/csharp-uhwid
                string curval = UHWIDEngine.AdvancedUid;
                if (!GlobalVars.PlayerTripcode.Equals(curval))
                {
                    GlobalVars.PlayerTripcode = curval;
                }

#if !BASICLAUNCHER
                if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization))
                {
                    Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
                }
                else
                {
                    Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, write);
                }

                ReShade(GlobalPaths.ConfigDir, "ReShade.ini", write);
#endif
            }
        }

        public static bool ResetMapIfNecessary()
        {
            if (!File.Exists(GlobalVars.UserConfiguration.MapPath))
            {
                GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
                GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
                GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
                return true;
            }

            return false;
        }

        public static void Customization(string cfgpath, bool write)
        {
            if (write)
            {
                //WRITE
                INIFile ini = new INIFile(cfgpath);

                string section = "Items";

                ini.IniWriteValue(section, "Hat1", GlobalVars.UserCustomization.Hat1.ToString());
                ini.IniWriteValue(section, "Hat2", GlobalVars.UserCustomization.Hat2.ToString());
                ini.IniWriteValue(section, "Hat3", GlobalVars.UserCustomization.Hat3.ToString());
                ini.IniWriteValue(section, "Face", GlobalVars.UserCustomization.Face.ToString());
                ini.IniWriteValue(section, "Head", GlobalVars.UserCustomization.Head.ToString());
                ini.IniWriteValue(section, "TShirt", GlobalVars.UserCustomization.TShirt.ToString());
                ini.IniWriteValue(section, "Shirt", GlobalVars.UserCustomization.Shirt.ToString());
                ini.IniWriteValue(section, "Pants", GlobalVars.UserCustomization.Pants.ToString());
                ini.IniWriteValue(section, "Icon", GlobalVars.UserCustomization.Icon.ToString());
                ini.IniWriteValue(section, "Extra", GlobalVars.UserCustomization.Extra.ToString());

                string section2 = "Colors";

                ini.IniWriteValue(section2, "HeadColorID", GlobalVars.UserCustomization.HeadColorID.ToString());
                ini.IniWriteValue(section2, "HeadColorString", GlobalVars.UserCustomization.HeadColorString.ToString());
                ini.IniWriteValue(section2, "TorsoColorID", GlobalVars.UserCustomization.TorsoColorID.ToString());
                ini.IniWriteValue(section2, "TorsoColorString", GlobalVars.UserCustomization.TorsoColorString.ToString());
                ini.IniWriteValue(section2, "LeftArmColorID", GlobalVars.UserCustomization.LeftArmColorID.ToString());
                ini.IniWriteValue(section2, "LeftArmColorString", GlobalVars.UserCustomization.LeftArmColorString.ToString());
                ini.IniWriteValue(section2, "RightArmColorID", GlobalVars.UserCustomization.RightArmColorID.ToString());
                ini.IniWriteValue(section2, "RightArmColorString", GlobalVars.UserCustomization.RightArmColorString.ToString());
                ini.IniWriteValue(section2, "LeftLegColorID", GlobalVars.UserCustomization.LeftLegColorID.ToString());
                ini.IniWriteValue(section2, "LeftLegColorString", GlobalVars.UserCustomization.LeftLegColorString.ToString());
                ini.IniWriteValue(section2, "RightLegColorID", GlobalVars.UserCustomization.RightLegColorID.ToString());
                ini.IniWriteValue(section2, "RightLegColorString", GlobalVars.UserCustomization.RightLegColorString.ToString());

                string section3 = "Other";

                ini.IniWriteValue(section3, "CharacterID", GlobalVars.UserCustomization.CharacterID.ToString());
                ini.IniWriteValue(section3, "ExtraSelectionIsHat", GlobalVars.UserCustomization.ExtraSelectionIsHat.ToString());
                ini.IniWriteValue(section3, "ShowHatsOnExtra", GlobalVars.UserCustomization.ShowHatsInExtra.ToString());
            }
            else
            {
                //READ

                try
                {
                    string hat1, hat2, hat3, face,
                        head, tshirt, shirt, pants, icon,
                        extra, headcolorid, headcolorstring, torsocolorid, torsocolorstring,
                        larmid, larmstring, rarmid, rarmstring, llegid,
                        llegstring, rlegid, rlegstring, characterid, extraishat, showhatsonextra;

                    INIFile ini = new INIFile(cfgpath);

                    string section = "Items";

                    hat1 = ini.IniReadValue(section, "Hat1", GlobalVars.UserCustomization.Hat1.ToString());
                    hat2 = ini.IniReadValue(section, "Hat2", GlobalVars.UserCustomization.Hat2.ToString());
                    hat3 = ini.IniReadValue(section, "Hat3", GlobalVars.UserCustomization.Hat3.ToString());
                    face = ini.IniReadValue(section, "Face", GlobalVars.UserCustomization.Face.ToString());
                    head = ini.IniReadValue(section, "Head", GlobalVars.UserCustomization.Head.ToString());
                    tshirt = ini.IniReadValue(section, "TShirt", GlobalVars.UserCustomization.TShirt.ToString());
                    shirt = ini.IniReadValue(section, "Shirt", GlobalVars.UserCustomization.Shirt.ToString());
                    pants = ini.IniReadValue(section, "Pants", GlobalVars.UserCustomization.Pants.ToString());
                    icon = ini.IniReadValue(section, "Icon", GlobalVars.UserCustomization.Icon.ToString());
                    extra = ini.IniReadValue(section, "Extra", GlobalVars.UserCustomization.Extra.ToString());

                    string section2 = "Colors";

                    headcolorid = ini.IniReadValue(section2, "HeadColorID", GlobalVars.UserCustomization.HeadColorID.ToString());
                    headcolorstring = ini.IniReadValue(section2, "HeadColorString", GlobalVars.UserCustomization.HeadColorString.ToString());
                    torsocolorid = ini.IniReadValue(section2, "TorsoColorID", GlobalVars.UserCustomization.TorsoColorID.ToString());
                    torsocolorstring = ini.IniReadValue(section2, "TorsoColorString", GlobalVars.UserCustomization.TorsoColorString.ToString());
                    larmid = ini.IniReadValue(section2, "LeftArmColorID", GlobalVars.UserCustomization.LeftArmColorID.ToString());
                    larmstring = ini.IniReadValue(section2, "LeftArmColorString", GlobalVars.UserCustomization.LeftArmColorString.ToString());
                    rarmid = ini.IniReadValue(section2, "RightArmColorID", GlobalVars.UserCustomization.RightArmColorID.ToString());
                    rarmstring = ini.IniReadValue(section2, "RightArmColorString", GlobalVars.UserCustomization.RightArmColorString.ToString());
                    llegid = ini.IniReadValue(section2, "LeftLegColorID", GlobalVars.UserCustomization.LeftLegColorID.ToString());
                    llegstring = ini.IniReadValue(section2, "LeftLegColorString", GlobalVars.UserCustomization.LeftLegColorString.ToString());
                    rlegid = ini.IniReadValue(section2, "RightLegColorID", GlobalVars.UserCustomization.RightLegColorID.ToString());
                    rlegstring = ini.IniReadValue(section2, "RightLegColorString", GlobalVars.UserCustomization.RightLegColorString.ToString());

                    string section3 = "Other";

                    characterid = ini.IniReadValue(section3, "CharacterID", GlobalVars.UserCustomization.CharacterID.ToString());
                    extraishat = ini.IniReadValue(section3, "ExtraSelectionIsHat", GlobalVars.UserCustomization.ExtraSelectionIsHat.ToString());
                    showhatsonextra = ini.IniReadValue(section3, "ShowHatsOnExtra", GlobalVars.UserCustomization.ShowHatsInExtra.ToString());

                    FileFormat.CustomizationConfig DefaultCustomization = new FileFormat.CustomizationConfig();

                    GlobalVars.UserCustomization.Hat1 = hat1;
                    GlobalVars.UserCustomization.Hat2 = hat2;
                    GlobalVars.UserCustomization.Hat3 = hat3;

                    GlobalVars.UserCustomization.HeadColorID = ValueInt(headcolorid, DefaultCustomization.HeadColorID);
                    GlobalVars.UserCustomization.TorsoColorID = ValueInt(torsocolorid, DefaultCustomization.TorsoColorID);
                    GlobalVars.UserCustomization.LeftArmColorID = ValueInt(larmid, DefaultCustomization.LeftArmColorID);
                    GlobalVars.UserCustomization.RightArmColorID = ValueInt(rarmid, DefaultCustomization.RightArmColorID);
                    GlobalVars.UserCustomization.LeftLegColorID = ValueInt(llegid, DefaultCustomization.LeftLegColorID);
                    GlobalVars.UserCustomization.RightLegColorID = ValueInt(rlegid, DefaultCustomization.RightArmColorID);

                    GlobalVars.UserCustomization.HeadColorString = headcolorstring;
                    GlobalVars.UserCustomization.TorsoColorString = torsocolorstring;
                    GlobalVars.UserCustomization.LeftArmColorString = larmstring;
                    GlobalVars.UserCustomization.RightArmColorString = rarmstring;
                    GlobalVars.UserCustomization.LeftLegColorString = llegstring;
                    GlobalVars.UserCustomization.RightLegColorString = rlegstring;

                    GlobalVars.UserCustomization.Face = face;
                    GlobalVars.UserCustomization.Head = head;
                    GlobalVars.UserCustomization.TShirt = tshirt;
                    GlobalVars.UserCustomization.Shirt = shirt;
                    GlobalVars.UserCustomization.Pants = pants;
                    GlobalVars.UserCustomization.Icon = icon;

                    GlobalVars.UserCustomization.CharacterID = characterid;
                    GlobalVars.UserCustomization.Extra = extra;
                    GlobalVars.UserCustomization.ExtraSelectionIsHat = ValueBool(extraishat, DefaultCustomization.ExtraSelectionIsHat);
                    GlobalVars.UserCustomization.ShowHatsInExtra = ValueBool(showhatsonextra, DefaultCustomization.ShowHatsInExtra);
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    Customization(cfgpath, true);
                }
            }

            ReloadLoadoutValue();
        }

        public static void ReShade(string cfgpath, string cfgname, bool write)
        {
            string fullpath = cfgpath + "\\" + cfgname;

            if (!File.Exists(fullpath))
            {
                Util.FixedFileCopy(GlobalPaths.ConfigDir + "\\ReShade_default.ini", fullpath, false);
                ReShadeValues(fullpath, write, true);
            }
            else
            {
                ReShadeValues(fullpath, write, true);
            }

            string clientdir = GlobalPaths.ClientDir;
            DirectoryInfo dinfo = new DirectoryInfo(clientdir);
            DirectoryInfo[] Dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo dir in Dirs)
            {
                string fulldirpath = dir.FullName + @"\" + cfgname;
                string dllfilename = "opengl32.dll";
                string fulldllpath = dir.FullName + @"\" + dllfilename;

                if (GlobalVars.UserConfiguration.ReShade)
                {
                    if (!File.Exists(fulldirpath))
                    {
                        Util.FixedFileCopy(fullpath, fulldirpath, false);
                        ReShadeValues(fulldirpath, write, false);
                    }
                    else
                    {
                        ReShadeValues(fulldirpath, write, false);
                    }

                    if (!File.Exists(fulldllpath))
                    {
                        Util.FixedFileCopy(GlobalPaths.DataDir + "\\" + dllfilename, fulldllpath, false);
                    }
                }
                else
                {
                    Util.FixedFileDelete(fulldirpath);

                    if (!GlobalVars.UserConfiguration.DisableReshadeDelete)
                    {
                        Util.FixedFileDelete(fulldllpath);
                    }
                }
            }
        }

        public static void ReShadeValues(string cfgpath, bool write, bool setglobals)
        {
            if (write)
            {
                //WRITE
                INIFile ini = new INIFile(cfgpath);

                string section = "GENERAL";
                string section2 = "OVERLAY";

                int FPS = GlobalVars.UserConfiguration.ReShadeFPSDisplay ? 1 : 0;
                ini.IniWriteValue(section2, "ShowFPS", FPS.ToString());
                ini.IniWriteValue(section2, "ShowFrameTime", FPS.ToString());
                int PerformanceMode = GlobalVars.UserConfiguration.ReShadePerformanceMode ? 1 : 0;
                ini.IniWriteValue(section, "PerformanceMode", PerformanceMode.ToString());
            }
            else
            {
                //READ
                string framerate, frametime, performance;

                INIFile ini = new INIFile(cfgpath);

                string section = "GENERAL";
                string section2 = "OVERLAY";

                int FPS = GlobalVars.UserConfiguration.ReShadeFPSDisplay ? 1 : 0;
                framerate = ini.IniReadValue(section2, "ShowFPS", FPS.ToString());
                frametime = ini.IniReadValue(section2, "ShowFrameTime", FPS.ToString());
                int PerformanceMode = GlobalVars.UserConfiguration.ReShadePerformanceMode ? 1 : 0;
                performance = ini.IniReadValue(section, "PerformanceMode", PerformanceMode.ToString());

                if (setglobals)
                {
                    try
                    {
                        switch (ValueInt(framerate, 0))
                        {
                            case int showFPSLine when showFPSLine == 1 && Convert.ToInt32(frametime) == 1:
                                GlobalVars.UserConfiguration.ReShadeFPSDisplay = true;
                                break;
                            default:
                                GlobalVars.UserConfiguration.ReShadeFPSDisplay = false;
                                break;
                        }

                        switch (ValueInt(performance, 0))
                        {
                            case 1:
                                GlobalVars.UserConfiguration.ReShadePerformanceMode = true;
                                break;
                            default:
                                GlobalVars.UserConfiguration.ReShadePerformanceMode = false;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.LogExceptions(ex);
                        ReShadeValues(cfgpath, true, setglobals);
                    }
                }
            }
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
            bool WebProxySetupComplete = GlobalVars.UserConfiguration.WebProxyInitialSetupRequired;
            bool WebProxy = GlobalVars.UserConfiguration.WebProxyEnabled;

            GlobalVars.UserConfiguration = new FileFormat.Config();
            GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
            GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
            GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
            GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
#if LAUNCHER
        GlobalVars.UserConfiguration.LauncherStyle = style;
#endif
            GlobalVars.UserConfiguration.WebProxyInitialSetupRequired = WebProxySetupComplete;
            GlobalVars.UserConfiguration.WebProxyEnabled = WebProxy;
            NovetusFuncs.GeneratePlayerID();
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

            if (!GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%localizeonlineclothing%"))
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

            if (!GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%localizeonlineclothing%"))
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

        public static void ReloadLoadoutValue(bool localizeOnlineClothing = false)
        {
            string hat1 = (!GlobalVars.UserCustomization.Hat1.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Hat1 : "NoHat.rbxm";
            string hat2 = (!GlobalVars.UserCustomization.Hat2.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Hat2 : "NoHat.rbxm";
            string hat3 = (!GlobalVars.UserCustomization.Hat3.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Hat3 : "NoHat.rbxm";
            string extra = (!GlobalVars.UserCustomization.Extra.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Extra : "NoExtra.rbxm";

            GlobalVars.Loadout = "'" + hat1 + "','" +
            hat2 + "','" +
            hat3 + "'," +
            GlobalVars.UserCustomization.HeadColorID + "," +
            GlobalVars.UserCustomization.TorsoColorID + "," +
            GlobalVars.UserCustomization.LeftArmColorID + "," +
            GlobalVars.UserCustomization.RightArmColorID + "," +
            GlobalVars.UserCustomization.LeftLegColorID + "," +
            GlobalVars.UserCustomization.RightLegColorID + ",'" +
            GlobalVars.UserCustomization.TShirt + "','" +
            GlobalVars.UserCustomization.Shirt + "','" +
            GlobalVars.UserCustomization.Pants + "','" +
            GlobalVars.UserCustomization.Face + "','" +
            GlobalVars.UserCustomization.Head + "','" +
            GlobalVars.UserCustomization.Icon + "','" +
            extra + "'";

            GlobalVars.soloLoadout = "'" + GlobalVars.UserCustomization.Hat1 + "','" +
            GlobalVars.UserCustomization.Hat2 + "','" +
            GlobalVars.UserCustomization.Hat3 + "'," +
            GlobalVars.UserCustomization.HeadColorID + "," +
            GlobalVars.UserCustomization.TorsoColorID + "," +
            GlobalVars.UserCustomization.LeftArmColorID + "," +
            GlobalVars.UserCustomization.RightArmColorID + "," +
            GlobalVars.UserCustomization.LeftLegColorID + "," +
            GlobalVars.UserCustomization.RightLegColorID + ",'" +
            GlobalVars.UserCustomization.TShirt + "','" +
            GlobalVars.UserCustomization.Shirt + "','" +
            GlobalVars.UserCustomization.Pants + "','" +
            GlobalVars.UserCustomization.Face + "','" +
            GlobalVars.UserCustomization.Head + "','" +
            GlobalVars.UserCustomization.Icon + "','" +
            GlobalVars.UserCustomization.Extra + "'";

            if (localizeOnlineClothing)
            {
                GlobalVars.TShirtTextureID = GetItemTextureID(GlobalVars.UserCustomization.TShirt, "TShirt", new AssetCacheDefBasic("ShirtGraphic", new string[] { "Graphic" }));
                GlobalVars.ShirtTextureID = GetItemTextureID(GlobalVars.UserCustomization.Shirt, "Shirt", new AssetCacheDefBasic("Shirt", new string[] { "ShirtTemplate" }));
                GlobalVars.PantsTextureID = GetItemTextureID(GlobalVars.UserCustomization.Pants, "Pants", new AssetCacheDefBasic("Pants", new string[] { "PantsTemplate" }));
                GlobalVars.FaceTextureID = GetItemTextureID(GlobalVars.UserCustomization.Face, "Face", new AssetCacheDefBasic("Decal", new string[] { "Texture" }));

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

                if (lineCount != fileCount)
                {
                    Util.ConsolePrint("WARNING - Initial File List is not relevant to file path. Regenerating.", 5);
                    Thread t = new Thread(CreateInitialFileList);
                    t.IsBackground = true;
                    t.Start();
                }
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