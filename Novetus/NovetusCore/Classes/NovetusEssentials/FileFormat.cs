using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using static Novetus.Core.FileFormat.ClientInfo;
using static Novetus.Core.FileFormat.ClientInfoLegacy;

namespace Novetus.Core
{
    #region File Formats
    public class FileFormat
    {
        #region Client Information
        [JsonObject(MemberSerialization.OptIn)]
        public class ClientInfo
        {
            public class ClientSpecificDictionary
            {
                private Dictionary<ScriptType, string> _dict = new Dictionary<ScriptType, string>();

                public Dictionary<ScriptType, string> Dict
                {
                    get
                    {
                        return _dict;
                    }
                    set
                    {
                        _dict = value;
                    }
                }
            }

            public enum ClientTag
            {
                NoGraphicsOptions,
                ForceLegacyOpenGL,
                HasQualityLevel21,
                ForceAutomatic,
                HasCharacterOnlyShadows,
                RequiresWebProxy,
                RequiresOnlineScripts,
                RequiresCurrentMinimumNovetusVersion
            }

            public ClientInfo()
            {
                Name = "";
                UsesPlayerName = true;
                UsesID = true;
                Description = "";
                Warning = "";
                ClientPaths = new ClientSpecificDictionary();
                ClientMD5s = new ClientSpecificDictionary();
                ScriptPaths = new ClientSpecificDictionary();
                ScriptMD5s = new ClientSpecificDictionary();
                CommandLineArgs = new ClientSpecificDictionary();
                ClientTags = new List<ClientTag>();
            }

            [JsonProperty]
            [JsonRequired]
            public string Name { get; set; }

            [JsonProperty]
            public bool UsesPlayerName { get; set; }

            [JsonProperty]
            public bool UsesID { get; set; }

            [JsonProperty]
            public string Description { get; set; }

            [JsonProperty]
            public string Warning { get; set; }

            [JsonProperty]
            [JsonRequired]
            public ClientSpecificDictionary ClientPaths { get; set; }

            [JsonProperty]
            [JsonRequired]
            public ClientSpecificDictionary ClientMD5s { get; set; }

            [JsonProperty]
            public ClientSpecificDictionary ScriptPaths { get; set; }

            [JsonProperty]
            public ClientSpecificDictionary ScriptMD5s { get; set; }

            [JsonProperty]
            [JsonRequired]
            public ClientSpecificDictionary CommandLineArgs { get; set; }

            [JsonProperty]
            public List<ClientTag> ClientTags { get; set; }
        }
        #endregion

        //TODO: move this to legacy clientinfo creator when we move to JSON.
        #region Client Information (Legacy)
        public class ClientInfoLegacy
        {
            public enum ClientLoadOptionsLegacy
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

            public ClientInfoLegacy()
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
                ClientLoadOptions = ClientLoadOptionsLegacy.Client_2008AndUp;
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
            public ClientLoadOptionsLegacy ClientLoadOptions { get; set; }
            public string CommandLineArgs { get; set; }

            public static ClientLoadOptionsLegacy GetClientLoadOptionsForBool(bool level)
            {
                switch (level)
                {
                    case false:
                        return ClientLoadOptionsLegacy.Client_2008AndUp;
                    default:
                        return ClientLoadOptionsLegacy.Client_2007_NoGraphicsOptions;
                }
            }

            public static string GetPathForClientLoadOptions(ClientLoadOptionsLegacy level)
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
                    case ClientLoadOptionsLegacy.Client_2008AndUp_QualityLevel21:
                    case ClientLoadOptionsLegacy.Client_2008AndUp_LegacyOpenGL:
                    case ClientLoadOptionsLegacy.Client_2008AndUp_NoGraphicsOptions:
                    case ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomatic:
                    case ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomaticQL21:
                    case ClientLoadOptionsLegacy.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                    case ClientLoadOptionsLegacy.Client_2008AndUp:
                        return localAppdataRobloxPath;
                    default:
                        return appdataRobloxPath;
                }
            }
        }
        #endregion

        #region ConfigBase
        public class ConfigBase
        {
            public JSONFile JSON;
            private string Section { get; set; }
            private string Path { get; set; }
            private string FileName { get; set; }
            public string FullPath { get; }

            public virtual Dictionary<string, string> ValueDefaults { get; set; }

            public ConfigBase(string section, string path, string fileName, bool overwriteFileExistsCheck = false)
            {
                Section = section;
                Path = path;
                FileName = fileName;
                FullPath = Path + "\\" + FileName;

                FilePreLoadEvent();

                bool fileExists = (File.Exists(FullPath) && overwriteFileExistsCheck) ? false : File.Exists(FullPath);

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
                    catch (Exception)
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
                if (result)
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
            public Config(bool overwriteFileExistsCheck = false) : base("Config", GlobalPaths.ConfigDir, GlobalPaths.ConfigName, overwriteFileExistsCheck) { }

            public Config(string filename, bool overwriteFileExistsCheck = false) : base("Config", GlobalPaths.ConfigDir, filename, overwriteFileExistsCheck) { }

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
                    {"CloseOnLaunch", "False"},
                    {"UserID", NovetusFuncs.GeneratePlayerID().ToString()},
                    {"PlayerName", userName},
                    {"RobloxPort", "53640"},
                    {"PlayerLimit", "12"},
                    {"UPnP", "False"},
                    {"DisabledAssetSDKHelp", "False"},
                    {"DiscordRichPresence", "True"},
                    {"MapPath", GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap},
                    {"MapPathSnip", GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap},
                    {"GraphicsMode", ((int)Settings.Mode.Automatic).ToString()},
                    {"QualityLevel", ((int)Settings.Level.Automatic).ToString()},
                    {"LauncherStyle", (Util.IsWineRunning() ? ((int)Settings.Style.Extended).ToString() : ((int)Settings.Style.Stylish).ToString())},
                    {"AssetSDKFixerSaveBackups", "True"},
                    {"AlternateServerIP", ""},
                    {"ShowServerNotifications", "False"},
                    {"ServerBrowserServerName", "Novetus"},
                    {"ServerBrowserServerAddress", ""},
                    {"Priority", ((int)ProcessPriorityClass.RealTime).ToString()},
                    {"FirstServerLaunch", "True"},
                    {"NewGUI", "False"},
                    {"URIQuickConfigure", "True"},
                    {"BootstrapperShowUI", "True"},
                    {"WebProxyInitialSetupRequired", "True"},
                    {"WebProxyEnabled", "False"}
                };
            }
        }
        #endregion

        #region Customization Configuration
        public class CustomizationConfig : ConfigBase
        {
            public int[] DefaultColors = { 24, 23, 24, 24, 119, 119 };

            public CustomizationConfig(bool overwriteFileExistsCheck = false) : base("Customization", GlobalPaths.ConfigDir, GlobalPaths.ConfigNameCustomization, overwriteFileExistsCheck) { }
            public CustomizationConfig(string filename, bool overwriteFileExistsCheck = false) : base("Customization", GlobalPaths.ConfigDir, filename, overwriteFileExistsCheck) { }

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
                    {"HeadColorID", DefaultColors[0].ToString()},
                    {"TorsoColorID", DefaultColors[1].ToString()},
                    {"LeftArmColorID", DefaultColors[2].ToString()},
                    {"RightArmColorID", DefaultColors[3].ToString()},
                    {"LeftLegColorID", DefaultColors[4].ToString()},
                    {"RightLegColorID", DefaultColors[5].ToString()},
                    {"HeadColorString", "Color [A=255, R=245, G=205, B=47]"},
                    {"TorsoColorString", "Color [A=255, R=13, G=105, B=172]"},
                    {"LeftArmColorString", "Color [A=255, R=245, G=205, B=47]"},
                    {"RightArmColorString", "Color [A=255, R=245, G=205, B=47]"},
                    {"LeftLegColorString", "Color [A=255, R=164, G=189, B=71]"},
                    {"RightLegColorString", "Color [A=255, R=164, G=189, B=71]"},
                    {"ExtraSelectionIsHat", "False"},
                    {"ShowHatsInExtra", "False"},
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
                NetVersion = ".NET 4.5.1";
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
}
