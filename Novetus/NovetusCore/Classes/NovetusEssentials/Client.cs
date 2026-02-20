#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Reflection;
using System.ServiceModel;
#endregion

namespace Novetus.Core
{
    #region Client Management
    public class Client
    {
        public static void ReadClientValues(bool initial = false)
        {
            ReadClientValues(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), initial);
        }

        public static void ReadClientValues(string ClientName, bool initial = false)
        {
            string name = ClientName;
            if (string.IsNullOrWhiteSpace(name))
            {
                if (!string.IsNullOrWhiteSpace(GlobalVars.ProgramInformation.DefaultClient))
                {
                    name = GlobalVars.ProgramInformation.DefaultClient;
                }
                else
                {
                    return;
                }
            }

            string clientpath = GlobalPaths.ClientDir + @"\\" + name + @"\\clientinfo.nov";

            if (!File.Exists(clientpath))
            {
                try
                {
                    Util.ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available. Novetus will attempt to generate one.", 2);
                    GenerateDefaultClientInfo(Path.GetDirectoryName(clientpath));
                    ReadClientValues(name, initial);
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    Util.ConsolePrint("ERROR - Failed to generate default clientinfo.nov. Info: " + ex.Message, 2);
                    Util.ConsolePrint("Loading default client '" + GlobalVars.ProgramInformation.DefaultClient + "'", 4);
                    name = GlobalVars.ProgramInformation.DefaultClient;
                    ReadClientValues(name, initial);
                }
            }
            else
            {
                LoadClientValues(clientpath);

                if (initial)
                {
                    Util.ConsolePrint("Client '" + name + "' successfully loaded.", 3);
                }
            }

            string terms = "_" + ClientName + "_default";
            string[] dirs = Directory.GetFiles(GlobalPaths.ConfigDirClients);

            foreach (string dir in dirs)
            {
                if (dir.Contains(terms) && dir.EndsWith(".xml"))
                {
                    string fullpath = dir.Replace("_default", "");

                    if (!File.Exists(fullpath))
                    {
                        IOSafe.File.Copy(dir, fullpath, false);
                    }
                }
            }

            ChangeGameSettings(ClientName);
        }

        //https://stackoverflow.com/questions/63879676/open-all-exe-files-in-a-directory-c-sharp
        public static List<string> GetAllExecutables(string path)
        {
            return Directory.Exists(path)
                   ? Directory.GetFiles(path, "*.exe").ToList()
                   : new List<string>(); // or null
        }

        public static void GenerateDefaultClientInfo(string path)
        {
            FileFormat.ClientInfo DefaultClientInfo = new FileFormat.ClientInfo();
            bool placeholder = false;

            string ClientName = "";
            List<string> exeList = GetAllExecutables(path);

            if (File.Exists(path + "\\RobloxApp_client.exe"))
            {
                ClientName = "\\RobloxApp_client.exe";
            }
            else if (File.Exists(path + "\\client\\RobloxApp_client.exe"))
            {
                ClientName = "\\client\\RobloxApp_client.exe";
                DefaultClientInfo.SeperateFolders = true;
            }
            else if (File.Exists(path + "\\RobloxApp.exe"))
            {
                ClientName = "\\RobloxApp.exe";
                DefaultClientInfo.LegacyMode = true;
            }
            else if (exeList.Count > 0)
            {
                string FirstEXE = exeList[0].Replace(path, "").Replace(@"\", "");
                ClientName = @"\\" + FirstEXE;
                DefaultClientInfo.CustomClientEXEName = ClientName;
                DefaultClientInfo.UsesCustomClientEXEName = true;
            }
            else
            {
                IOException clientNotFoundEX = new IOException("Could not find client exe file. Your client must have a .exe file to function.");
                throw clientNotFoundEX;
            }

            string ClientMD5 = File.Exists(path + ClientName) ? SecurityFuncs.GenerateMD5(path + ClientName) : "";

            if (!string.IsNullOrWhiteSpace(ClientMD5))
            {
                DefaultClientInfo.ClientMD5 = ClientMD5.ToUpper(CultureInfo.InvariantCulture);
            }
            else
            {
                IOException clientNotFoundEX = new IOException("Could not find client exe for MD5 generation. It must be named either RobloxApp.exe or RobloxApp_client.exe in order to function.");
                throw clientNotFoundEX;
            }

            string desc = "This client information file for '" + GlobalVars.UserConfiguration.ReadSetting("SelectedClient") +
                "' was pre-generated by Novetus for your convienence. You will need to load this clientinfo.nov file into the Client SDK for additional options. "
                + Util.LoremIpsum(1, 128, 1, 6, 1);

            DefaultClientInfo.Description = desc;

            string[] lines = {
                    SecurityFuncs.Encode(DefaultClientInfo.UsesPlayerName.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.UsesID.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.Warning.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.LegacyMode.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.ClientMD5.ToString()),
                    SecurityFuncs.Encode("null"),
                    SecurityFuncs.Encode(DefaultClientInfo.Description.ToString()),
                    SecurityFuncs.Encode(placeholder.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.Fix2007.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.AlreadyHasSecurity.ToString()),
                    SecurityFuncs.Encode(((int)DefaultClientInfo.ClientLoadOptions).ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.SeperateFolders.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.UsesCustomClientEXEName.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.CustomClientEXEName.ToString().Replace("\\", "")),
                    SecurityFuncs.Encode(DefaultClientInfo.CommandLineArgs.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.ClientLaunchTime.ToString()),
                    SecurityFuncs.Encode(DefaultClientInfo.ClientInfoRevision.ToString())
                };

            File.WriteAllText(path + "\\clientinfo.nov", SecurityFuncs.Encode(string.Join("|", lines)));
        }

        //NOT FOR SDK.
        public static FileFormat.ClientInfo GetClientInfoValues(string ClientName)
        {
            string name = ClientName;

            try
            {
                FileFormat.ClientInfo info = new FileFormat.ClientInfo();
                string clientpath = GlobalPaths.ClientDir + @"\\" + name + @"\\clientinfo.nov";
                LoadClientValues(info, clientpath);
                return info;
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return null;
            }
        }

        public static void LoadClientValues(string clientpath)
        {
            LoadClientValues(GlobalVars.SelectedClientInfo, clientpath);
        }

        public static void LoadClientValues(FileFormat.ClientInfo info, string clientpath)
        {
            string file, usesplayername, usesid, warning,
                legacymode, clientmd5, scriptmd5,
                desc, fix2007, alreadyhassecurity,
                clientloadoptions, commandlineargs, folders,
                usescustomname, customname, script, launchtime,
                revision;

            using (StreamReader reader = new StreamReader(clientpath))
            {
                file = reader.ReadLine();
            }

            string ConvertedLine = SecurityFuncs.Decode(file);
            string[] result = ConvertedLine.Split('|');
            usesplayername = SecurityFuncs.Decode(result[0]);
            usesid = SecurityFuncs.Decode(result[1]);
            warning = SecurityFuncs.Decode(result[2]);
            legacymode = SecurityFuncs.Decode(result[3]);
            clientmd5 = SecurityFuncs.Decode(result[4]);
            scriptmd5 = SecurityFuncs.Decode(result[5]);
            desc = SecurityFuncs.Decode(result[6]);
            fix2007 = SecurityFuncs.Decode(result[8]);
            alreadyhassecurity = SecurityFuncs.Decode(result[9]);
            clientloadoptions = SecurityFuncs.Decode(result[10]);
            folders = "False";
            usescustomname = "False";
            customname = "";
            script = "";
            launchtime = "0.05";
            revision = "0";
            try
            {
                commandlineargs = SecurityFuncs.Decode(result[11]);

                bool parsedValue;
                if (bool.TryParse(commandlineargs, out parsedValue))
                {
                    folders = SecurityFuncs.Decode(result[11]);
                    commandlineargs = SecurityFuncs.Decode(result[12]);
                    bool parsedValue2;
                    if (bool.TryParse(commandlineargs, out parsedValue2))
                    {
                        bool useslaunchtime = false;

                        usescustomname = SecurityFuncs.Decode(result[12]);
                        customname = SecurityFuncs.Decode(result[13]);
                        commandlineargs = SecurityFuncs.Decode(result[14]);
                        try
                        {
                            script = SecurityFuncs.Decode(result[15]);
                            launchtime = SecurityFuncs.Decode(result[16]);
                            //clearing script md5, we house the script now. 
                            scriptmd5 = SecurityFuncs.GenerateMD5(clientpath);
                            useslaunchtime = true;
                        }
                        catch (Exception)
                        {
                        }

                        if (useslaunchtime)
                        {
                            try
                            {
                                revision = SecurityFuncs.Decode(result[17]);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //fake this option until we properly apply it.
                clientloadoptions = "2";
                commandlineargs = SecurityFuncs.Decode(result[10]);
            }

            info.UsesPlayerName = ConvertSafe.ToBooleanSafe(usesplayername);
            info.UsesID = ConvertSafe.ToBooleanSafe(usesid);
            info.Warning = warning;
            info.LegacyMode = ConvertSafe.ToBooleanSafe(legacymode);
            info.ClientMD5 = clientmd5;
            info.ClientInfoRevision = ConvertSafe.ToInt32Safe(revision);
            info.ScriptMD5 = scriptmd5;
            info.Description = desc;
            info.Fix2007 = ConvertSafe.ToBooleanSafe(fix2007);
            info.AlreadyHasSecurity = ConvertSafe.ToBooleanSafe(alreadyhassecurity);
            if (clientloadoptions.Equals("True") || clientloadoptions.Equals("False"))
            {
                info.ClientLoadOptions = FileFormat.ClientInfo.GetClientLoadOptionsForBool(ConvertSafe.ToBooleanSafe(clientloadoptions));
            }
            else
            {
                info.ClientLoadOptions = (FileFormat.ClientInfo.ClientLoadOptionsLegacy)ConvertSafe.ToInt32Safe(clientloadoptions);
            }

            info.SeperateFolders = ConvertSafe.ToBooleanSafe(folders);
            info.UsesCustomClientEXEName = ConvertSafe.ToBooleanSafe(usescustomname);
            info.CustomClientEXEName = customname;
            info.CommandLineArgs = commandlineargs;
            info.LaunchScript = script;
            info.ClientLaunchTime = ConvertSafe.ToDoubleSafe(launchtime);
        }

        public static GlobalVars.LauncherState GetStateForType(ScriptType type)
        {
            switch (type)
            {
                case ScriptType.Client:
                    return GlobalVars.LauncherState.InMPGame;
                case ScriptType.Solo:
                case ScriptType.SoloServer:
                    return GlobalVars.LauncherState.InSoloGame;
                case ScriptType.Studio:
                    return GlobalVars.LauncherState.InStudio;
                case ScriptType.OutfitView:
                    return GlobalVars.LauncherState.InCustomization;
                default:
                    return GlobalVars.LauncherState.InLauncher;
            }
        }

        public static void UpdateRichPresence(GlobalVars.LauncherState state, bool initial = false)
        {
            string mapname = "";
            if (GlobalVars.GameOpened != ScriptType.Client || GlobalVars.GameOpened != ScriptType.Solo || GlobalVars.GameOpened != ScriptType.OutfitView)
            {
                mapname = GlobalVars.UserConfiguration.ReadSetting("Map");
            }

            UpdateRichPresence(state, GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), mapname, initial);
        }

        public static void UpdateRichPresence(GlobalVars.LauncherState state, string mapname, bool initial = false)
        {
            UpdateRichPresence(state, GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), mapname, initial);
        }

        public static void UpdateRichPresence(GlobalVars.LauncherState state, string clientname, string mapname, bool initial = false)
        {
            if (GlobalVars.UserConfiguration.ReadSettingBool("DiscordRichPresence"))
            {
                if (initial)
                {
                    GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
                    var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                    GlobalVars.presence.startTimestamp = (long)timeSpan.TotalSeconds;
                }

                string ValidMapname = (string.IsNullOrWhiteSpace(mapname) ? "Place1" : mapname);

                switch (state)
                {
                    case GlobalVars.LauncherState.InLauncher:
                        GlobalVars.presence.smallImageKey = GlobalVars.image_inlauncher;
                        GlobalVars.presence.state = "In Launcher";
                        GlobalVars.presence.details = "Selected " + clientname;
                        GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " | Novetus " + GlobalVars.ProgramInformation.Version;
                        GlobalVars.presence.smallImageText = "In Launcher";
                        break;
                    case GlobalVars.LauncherState.InMPGame:
                        GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                        GlobalVars.presence.details = ValidMapname;
                        GlobalVars.presence.state = "In " + clientname + " Multiplayer Game";
                        GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " | Novetus " + GlobalVars.ProgramInformation.Version;
                        GlobalVars.presence.smallImageText = "In " + clientname + " Multiplayer Game";
                        break;
                    case GlobalVars.LauncherState.InSoloGame:
                        GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                        GlobalVars.presence.details = ValidMapname;
                        GlobalVars.presence.state = "In " + clientname + " Solo Game";
                        GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " | Novetus " + GlobalVars.ProgramInformation.Version;
                        GlobalVars.presence.smallImageText = "In " + clientname + " Solo Game";
                        break;
                    case GlobalVars.LauncherState.InStudio:
                        GlobalVars.presence.smallImageKey = GlobalVars.image_instudio;
                        GlobalVars.presence.details = ValidMapname;
                        GlobalVars.presence.state = "In " + clientname + " Studio";
                        GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " | Novetus " + GlobalVars.ProgramInformation.Version;
                        GlobalVars.presence.smallImageText = "In " + clientname + " Studio";
                        break;
                    case GlobalVars.LauncherState.InCustomization:
                        GlobalVars.presence.smallImageKey = GlobalVars.image_incustomization;
                        GlobalVars.presence.details = "Customizing " + GlobalVars.UserConfiguration.ReadSetting("PlayerName");
                        GlobalVars.presence.state = "In Character Customization";
                        GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " | Novetus " + GlobalVars.ProgramInformation.Version;
                        GlobalVars.presence.smallImageText = "In Character Customization";
                        break;
                    case GlobalVars.LauncherState.LoadingURI:
                        GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                        GlobalVars.presence.details = ValidMapname;
                        GlobalVars.presence.state = "Joining a " + clientname + " Multiplayer Game";
                        GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.ReadSetting("PlayerName") + " | Novetus " + GlobalVars.ProgramInformation.Version;
                        GlobalVars.presence.smallImageText = "Joining a " + clientname + " Multiplayer Game";
                        break;
                    default:
                        break;
                }

                IDiscordRPC.UpdatePresence(ref GlobalVars.presence);
            }
        }

        public static void ChangeGameSettings(string ClientName)
        {
            try
            {
                FileFormat.ClientInfo info = GetClientInfoValues(ClientName);

                string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.FileDeleteFilterName;
                string[] fileListToDelete = File.ReadAllLines(filterPath);

                foreach (string file in fileListToDelete)
                {
                    string fullFilePath = FileFormat.ClientInfo.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + file;
                    IOSafe.File.Delete(fullFilePath);
                }

                if (GlobalVars.UserConfiguration.ReadSettingInt("QualityLevel") != (int)Settings.Level.Custom)
                {
                    int GraphicsMode = 0;

                    if (info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomaticQL21 ||
                            info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomatic)
                    {
                        GraphicsMode = 1;
                    }
                    else
                    {
                        if (info.ClientLoadOptions != FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2007_NoGraphicsOptions ||
                            info.ClientLoadOptions != FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_NoGraphicsOptions)
                        {

                            switch ((Settings.Mode)GlobalVars.UserConfiguration.ReadSettingInt("GraphicsMode"))
                            {
                                case Settings.Mode.OpenGLStable:
                                    switch (info.ClientLoadOptions)
                                    {
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2007:
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_LegacyOpenGL:
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                                            GraphicsMode = 2;
                                            break;
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp:
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_QualityLevel21:
                                            GraphicsMode = 4;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case Settings.Mode.OpenGLExperimental:
                                    GraphicsMode = 4;
                                    break;
                                case Settings.Mode.DirectX:
                                    GraphicsMode = 3;
                                    break;
                                default:
                                    GraphicsMode = 1;
                                    break;
                            }
                        }
                    }

                    //default values are ultra settings
                    int MeshDetail = 100;
                    int ShadingQuality = 100;
                    int GFXQualityLevel = 19;
                    if (info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomaticQL21 ||
                            info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_QualityLevel21)
                    {
                        GFXQualityLevel = 21;
                    }
                    int MaterialQuality = 3;
                    int AASamples = 8;
                    int Bevels = 1;
                    int Shadows_2008 = 1;
                    int AA = 1;
                    bool Shadows_2007 = true;

                    switch ((Settings.Level)GlobalVars.UserConfiguration.ReadSettingInt("QualityLevel"))
                    {
                        case Settings.Level.Automatic:
                            //set everything to automatic. Some ultra settings will still be enabled.
                            AA = 0;
                            Bevels = 0;
                            Shadows_2008 = 0;
                            GFXQualityLevel = 0;
                            MaterialQuality = 0;
                            Shadows_2007 = false;
                            break;
                        case Settings.Level.VeryLow:
                            AA = 2;
                            MeshDetail = 50;
                            ShadingQuality = 50;
                            GFXQualityLevel = 1;
                            MaterialQuality = 1;
                            AASamples = 1;
                            Bevels = 2;
                            Shadows_2008 = 2;
                            Shadows_2007 = false;
                            break;
                        case Settings.Level.Low:
                            AA = 2;
                            MeshDetail = 50;
                            ShadingQuality = 50;
                            GFXQualityLevel = 5;
                            MaterialQuality = 1;
                            AASamples = 1;
                            Bevels = 2;
                            Shadows_2008 = 2;
                            Shadows_2007 = false;
                            break;
                        case Settings.Level.Medium:
                            MeshDetail = 75;
                            ShadingQuality = 75;
                            GFXQualityLevel = 10;
                            MaterialQuality = 2;
                            AASamples = 4;
                            Bevels = 2;
                            if (info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomatic ||
                                info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomaticQL21 ||
                                info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_QualityLevel21 ||
                                info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL)
                            {
                                Shadows_2008 = 3;
                            }
                            Shadows_2007 = false;
                            break;
                        case Settings.Level.High:
                            MeshDetail = 75;
                            ShadingQuality = 75;
                            GFXQualityLevel = 15;
                            AASamples = 4;
                            break;
                        case Settings.Level.Ultra:
                        default:
                            break;
                    }

                    ApplyClientSettings(info, ClientName, GraphicsMode, MeshDetail, ShadingQuality, MaterialQuality, AA, AASamples, Bevels,
                        Shadows_2008, Shadows_2007, "", GFXQualityLevel, "800x600", "1024x768", 0);
                }
                else
                {
                    //save graphics mode.
                    int GraphicsMode = 0;

                    if (info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomaticQL21 ||
                            info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomatic)
                    {
                        GraphicsMode = 1;
                    }
                    else
                    {
                        if (info.ClientLoadOptions != FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2007_NoGraphicsOptions ||
                            info.ClientLoadOptions != FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_NoGraphicsOptions)
                        {

                            switch ((Settings.Mode)GlobalVars.UserConfiguration.ReadSettingInt("GraphicsMode"))
                            {
                                case Settings.Mode.OpenGLStable:
                                    switch (info.ClientLoadOptions)
                                    {
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2007:
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_LegacyOpenGL:
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                                            GraphicsMode = 2;
                                            break;
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp:
                                        case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_QualityLevel21:
                                            GraphicsMode = 4;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case Settings.Mode.OpenGLExperimental:
                                    GraphicsMode = 4;
                                    break;
                                case Settings.Mode.DirectX:
                                    GraphicsMode = 3;
                                    break;
                                default:
                                    GraphicsMode = 1;
                                    break;
                            }
                        }
                    }

                    ApplyClientSettings(info, ClientName, GraphicsMode, 0, 0, 0, 0, 0, 0, 0, false, "", 0, "800x600", "1024x768", 0, true);

                    //just copy the file.
                    string terms = "_" + ClientName;
                    string[] dirs = Directory.GetFiles(GlobalPaths.ConfigDirClients);

                    foreach (string dir in dirs)
                    {
                        if (dir.Contains(terms) && !dir.Contains("_default"))
                        {
                            IOSafe.File.Copy(dir, FileFormat.ClientInfo.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                                .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                                .Replace("-", ".xml"), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return;
            }
        }

        //oh god....
        //we're using this one for custom graphics quality. Better than the latter.
        public static void ApplyClientSettings_custom(FileFormat.ClientInfo info, string ClientName, int MeshDetail, int ShadingQuality, int MaterialQuality,
            int AA, int AASamples, int Bevels, int Shadows_2008, bool Shadows_2007, string Style_2007, int GFXQualityLevel, string WindowResolution, string FullscreenResolution,
            int ModernResolution)
        {
            try
            {
                int GraphicsMode = 0;

                if (info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomaticQL21 ||
                        info.ClientLoadOptions == FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_ForceAutomatic)
                {
                    GraphicsMode = 1;
                }
                else
                {
                    if (info.ClientLoadOptions != FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2007_NoGraphicsOptions ||
                        info.ClientLoadOptions != FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_NoGraphicsOptions)
                    {
                        switch ((Settings.Mode)GlobalVars.UserConfiguration.ReadSettingInt("GraphicsMode"))
                        {
                            case Settings.Mode.OpenGLStable:
                                switch (info.ClientLoadOptions)
                                {
                                    case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2007:
                                    case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_LegacyOpenGL:
                                    case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                                        GraphicsMode = 2;
                                        break;
                                    case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp:
                                    case FileFormat.ClientInfo.ClientLoadOptionsLegacy.Client_2008AndUp_QualityLevel21:
                                        GraphicsMode = 4;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Settings.Mode.OpenGLExperimental:
                                GraphicsMode = 4;
                                break;
                            case Settings.Mode.DirectX:
                                GraphicsMode = 3;
                                break;
                            default:
                                GraphicsMode = 1;
                                break;
                        }
                    }
                }

                ApplyClientSettings(info, ClientName, GraphicsMode, MeshDetail, ShadingQuality, MaterialQuality,
                AA, AASamples, Bevels, Shadows_2008, Shadows_2007, Style_2007, GFXQualityLevel, WindowResolution, FullscreenResolution, ModernResolution);
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return;
            }
        }

        //it's worse.
        public static void ApplyClientSettings(FileFormat.ClientInfo info, string ClientName, int GraphicsMode, int MeshDetail, int ShadingQuality, int MaterialQuality,
            int AA, int AASamples, int Bevels, int Shadows_2008, bool Shadows_2007, string Style_2007, int GFXQualityLevel, string WindowResolution, string FullscreenResolution,
            int ModernResolution, bool onlyGraphicsMode = false)
        {
            try
            {
                string winRes = WindowResolution;
                string fullRes = FullscreenResolution;

                string terms = "_" + ClientName;
                string[] dirs = Directory.GetFiles(GlobalPaths.ConfigDirClients);

                foreach (string dir in dirs)
                {
                    if (dir.Contains(terms) && !dir.Contains("_default"))
                    {
                        string oldfile = "";
                        string fixedfile = "";
                        XDocument doc = null;

                        try
                        {
                            oldfile = File.ReadAllText(dir);
                            fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile));
                            doc = XDocument.Parse(fixedfile);
                        }
                        catch (Exception ex)
                        {
                            Util.LogExceptions(ex);
                            return;
                        }

                        try
                        {
                            if (GraphicsMode != 0)
                            {
                                RobloxXML.EditRenderSettings(doc, "graphicsMode", GraphicsMode.ToString(), RobloxXML.XMLTypes.Token);
                            }

                            if (!onlyGraphicsMode)
                            {
                                RobloxXML.EditRenderSettings(doc, "maxMeshDetail", MeshDetail.ToString(), RobloxXML.XMLTypes.Float);
                                RobloxXML.EditRenderSettings(doc, "maxShadingQuality", ShadingQuality.ToString(), RobloxXML.XMLTypes.Float);
                                RobloxXML.EditRenderSettings(doc, "minMeshDetail", MeshDetail.ToString(), RobloxXML.XMLTypes.Float);
                                RobloxXML.EditRenderSettings(doc, "minShadingQuality", ShadingQuality.ToString(), RobloxXML.XMLTypes.Float);
                                RobloxXML.EditRenderSettings(doc, "AluminumQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "CompoundMaterialQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "CorrodedMetalQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "DiamondPlateQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "GrassQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "IceQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "PlasticQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "SlateQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                // fix truss detail. We're keeping it at 0.
                                RobloxXML.EditRenderSettings(doc, "TrussDetail", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "WoodQuality", MaterialQuality.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "Antialiasing", AA.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "AASamples", AASamples.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "Bevels", Bevels.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "Shadow", Shadows_2008.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "Shadows", Shadows_2007.ToString().ToLower(), RobloxXML.XMLTypes.Bool);
                                RobloxXML.EditRenderSettings(doc, "shadows", Shadows_2007.ToString().ToLower(), RobloxXML.XMLTypes.Bool);
                                RobloxXML.EditRenderSettings(doc, "_skinFile", !string.IsNullOrWhiteSpace(Style_2007) ? @"Styles\" + Style_2007 : "", RobloxXML.XMLTypes.String);
                                RobloxXML.EditRenderSettings(doc, "QualityLevel", GFXQualityLevel.ToString(), RobloxXML.XMLTypes.Token);
                                RobloxXML.EditRenderSettings(doc, "FullscreenSizePreference", fullRes.ToString(), RobloxXML.XMLTypes.Vector2Int16);
                                RobloxXML.EditRenderSettings(doc, "FullscreenSize", fullRes.ToString(), RobloxXML.XMLTypes.Vector2Int16);
                                RobloxXML.EditRenderSettings(doc, "WindowSizePreference", winRes.ToString(), RobloxXML.XMLTypes.Vector2Int16);
                                RobloxXML.EditRenderSettings(doc, "WindowSize", winRes.ToString(), RobloxXML.XMLTypes.Vector2Int16);
                                RobloxXML.EditRenderSettings(doc, "Resolution", ModernResolution.ToString(), RobloxXML.XMLTypes.Token);
                            }
                        }
                        catch (Exception ex)
                        {
                            Util.LogExceptions(ex);
                            return;
                        }
                        finally
                        {
                            doc.Save(dir);
                            IOSafe.File.Copy(dir, FileFormat.ClientInfo.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                                .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                                .Replace("-", ".xml"), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return;
            }
        }

        public static string GetGenLuaName(string ClientName, ScriptType type)
        {
            string luafile = "";
            FileFormat.ClientInfo info = GetClientInfoValues(ClientName);

            bool rbxasset = info.CommandLineArgs.Contains("%userbxassetforgeneration%");

            string genLuaFileName = GlobalPaths.ScriptGenName;

            if (type == ScriptType.Solo)
            {
                genLuaFileName = GlobalPaths.ScriptGenSoloName;
            }
            else if (type == ScriptType.SoloServer)
            {
                genLuaFileName = GlobalPaths.ScriptGenSoloServerName;
            }

            if (!rbxasset)
            {
                if (info.SeperateFolders)
                {
                    luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + genLuaFileName + ".lua";
                }
                else
                {
                    luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + genLuaFileName + ".lua";
                }
            }
            else
            {
                luafile = @"rbxasset://scripts\\" + genLuaFileName + ".lua";
            }

            return luafile;
        }

        public static string GetGenLuaFileName(string ClientName, ScriptType type)
        {
            string luafile = "";
            FileFormat.ClientInfo info = GetClientInfoValues(ClientName);

            string genLuaFileName = GlobalPaths.ScriptGenName;

            if (type == ScriptType.Solo)
            {
                genLuaFileName = GlobalPaths.ScriptGenSoloName;
            }
            else if (type == ScriptType.SoloServer)
            {
                genLuaFileName = GlobalPaths.ScriptGenSoloServerName;
            }

            if (info.SeperateFolders)
            {
                luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + genLuaFileName + ".lua";
            }
            else
            {
                luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + genLuaFileName + ".lua";
            }

            return luafile;
        }

        public static string GetLaunchScriptName(string ClientName, ScriptType type)
        {
            return "rbxasset://scripts\\\\" + GlobalPaths.ScriptName + ".lua";
        }

        public static string GetLaunchScriptFileName(string ClientName, ScriptType type)
        {
            string luafile = "";
            FileFormat.ClientInfo info = GetClientInfoValues(ClientName);

            if (info.SeperateFolders)
            {
                luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua";
            }
            else
            {
                luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua";
            }

            return luafile;
        }

        public static string GetLuaFileName(ScriptType type)
        {
            return GetLuaFileName(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), type);
        }

        public static string GetLuaFileName(string ClientName, ScriptType type)
        {
            string luafile = "";
            FileFormat.ClientInfo info = GetClientInfoValues(ClientName);

            if (!GlobalVars.SelectedClientInfo.Fix2007)
            {
                luafile = GetLaunchScriptName(ClientName, type);
            }
            else
            {
                luafile = GetGenLuaName(ClientName, type);
            }

            return luafile;
        }

        public static void ResetScripts(bool fullReset = false)
        {
            ResetScripts(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), GlobalVars.GameOpened, fullReset);
        }

        public static void ResetScripts(string ClientName, ScriptType type, bool fullReset = false)
        {
            if (File.Exists(GetLaunchScriptFileName(ClientName, type)))
            {
                Util.ConsolePrint("Removed Client Launch Script", 4);
                IOSafe.File.Delete(GetLaunchScriptFileName(ClientName, type));
            }

            if (GlobalVars.SelectedClientInfo.Fix2007 && fullReset)
            {
                if (File.Exists(GetGenLuaFileName(ClientName, type)))
                {
                    Util.ConsolePrint("Removed Generated Load Script", 4);
                    IOSafe.File.Delete(GetGenLuaFileName(ClientName, type));
                }

                if (File.Exists(GetGenLuaFileName(ClientName, ScriptType.SoloServer)))
                {
                    // JUST IN CASE: remove the solo server script too.
                    Util.ConsolePrint("Removed Generated Load Solo Server Script", 4);
                    IOSafe.File.Delete(GetGenLuaFileName(ClientName, ScriptType.SoloServer));
                }
            }
        }

        public static string GetClientSeperateFolderName(ScriptType type)
        {
            string rbxfolder = "";
            switch (type)
            {
                case ScriptType.Client:
                case ScriptType.Solo:
                case ScriptType.OutfitView:
                    rbxfolder = "client";
                    break;
                case ScriptType.Server:
                case ScriptType.SoloServer:
                    rbxfolder = "server";
                    break;
                case ScriptType.Studio:
                    rbxfolder = "studio";
                    break;
                case ScriptType.None:
                default:
                    rbxfolder = "";
                    break;
            }

            return rbxfolder;
        }

        public static string GetClientEXEDir(ScriptType type)
        {
            return GetClientEXEDir(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), type);
        }

        public static string GetClientEXEDir(string ClientName, ScriptType type)
        {
            string rbxexe = "";
            string BasePath = GlobalPaths.ClientDir + @"\\" + ClientName;
            if (GlobalVars.SelectedClientInfo.LegacyMode)
            {
                rbxexe = BasePath + @"\\RobloxApp.exe";
            }
            else if (GlobalVars.SelectedClientInfo.UsesCustomClientEXEName)
            {
                rbxexe = BasePath + @"\\" + GlobalVars.SelectedClientInfo.CustomClientEXEName;
            }
            else if (GlobalVars.SelectedClientInfo.SeperateFolders)
            {
                switch (type)
                {
                    case ScriptType.Client:
                    case ScriptType.Solo:
                        rbxexe = BasePath + @"\\" + GetClientSeperateFolderName(type) + @"\\RobloxApp_client.exe";
                        break;
                    case ScriptType.OutfitView:
                        rbxexe = BasePath + @"\\" + GetClientSeperateFolderName(type) + @"\\RobloxApp_preview.exe";
                        break;
                    case ScriptType.Server:
                    case ScriptType.SoloServer:
                        rbxexe = BasePath + @"\\" + GetClientSeperateFolderName(type) + @"\\RobloxApp_server.exe";
                        break;
                    case ScriptType.Studio:
                        rbxexe = BasePath + @"\\" + GetClientSeperateFolderName(type) + @"\\RobloxApp_studio.exe";
                        break;
                    case ScriptType.None:
                    default:
                        rbxexe = BasePath + @"\\RobloxApp.exe";
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case ScriptType.Client:
                    case ScriptType.Solo:
                        rbxexe = BasePath + @"\\RobloxApp_client.exe";
                        break;
                    case ScriptType.Server:
                    case ScriptType.SoloServer:
                        rbxexe = BasePath + @"\\RobloxApp_server.exe";
                        break;
                    case ScriptType.Studio:
                        rbxexe = BasePath + @"\\RobloxApp_studio.exe";
                        break;
                    case ScriptType.OutfitView:
                        rbxexe = BasePath + @"\\RobloxApp_preview.exe";
                        break;
                    case ScriptType.None:
                    default:
                        rbxexe = BasePath + @"\\RobloxApp.exe";
                        break;
                }
            }

            return rbxexe;
        }

#if URI
        public static void UpdateStatus(Label label, string status)
        {
            Util.LogPrint(status);
            if (label != null)
            {
                label.Text = status;
            }
        }
#endif

#if LAUNCHER
    public static void DecompressMap()
    {
        DecompressMap(ScriptType.None, false);
    }

    public static void DecompressMap(ScriptType type, bool nomap)
    {
        bool isMapActuallyCompressed = (GlobalVars.UserConfiguration.ReadSetting("Map").Contains(".bz2") &&
                GlobalVars.UserConfiguration.ReadSetting("MapPath").Contains(".bz2"));

        if (!isMapActuallyCompressed)
        {
            GlobalVars.isMapCompressed = false;
            return;
        }

        bool isMapDecompressedAlready = File.Exists(GlobalVars.UserConfiguration.ReadSetting("MapPath").Replace(".bz2", ""));

        if (isMapDecompressedAlready)
            return;

        bool doesntUseMap = nomap;
        switch (type)
        {
            case ScriptType.Client:
            case ScriptType.Solo:
            case ScriptType.OutfitView:
                doesntUseMap = true;
                break;
        }

        if (!doesntUseMap && File.Exists(GlobalVars.UserConfiguration.ReadSetting("MapPath")))
        {
            Util.Decompress(GlobalVars.UserConfiguration.ReadSetting("MapPath"), true);
            GlobalVars.isMapCompressed = true;
        }
    }

    public static void ResetDecompressedMap()
    {
        if (GlobalVars.isMapCompressed)
        {
            IOSafe.File.Delete(GlobalVars.UserConfiguration.ReadSetting("MapPath").Replace(".bz2", ""));
            GlobalVars.isMapCompressed = false;
        }
    }
#endif

#if URI
        public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e, Label label)
#else
        public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e)
#endif
        {
#if URI
            LaunchRBXClient(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), type, no3d, nomap, e, label);
#else
            LaunchRBXClient(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), type, no3d, nomap, e);
#endif
        }

#if URI
        public static bool ValidateFiles(string line, string validstart, string validend, Label label)
#else
        public static bool ValidateFiles(string line, string validstart, string validend)
#endif
        {
            string extractedFile = Script.ClientScript.GetArgsFromTag(line, validstart, validend);
            if (!string.IsNullOrWhiteSpace(extractedFile))
            {
                string[] parsedFileParams = extractedFile.Split('|');
                string filePath = parsedFileParams[0];
                string fileMD5 = parsedFileParams[1];
                string fullFilePath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.ReadSetting("SelectedClient") + @"\\" + filePath;

                if (!CheckMD5(fileMD5, fullFilePath))
                {
#if URI
                    UpdateStatus(label, "The client has been detected as modified. [Client.ValidateFiles]");
#elif LAUNCHER
                    Util.ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified. [Client.ValidateFiles])", 2);
#endif

#if LAUNCHER
                    if (!GlobalVars.isConsoleOnly)
                    {
                        MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified [Client.ValidateFiles])", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
#endif
                }
                else
                {
                    GlobalVars.ValidatedExtraFiles++;
                    return true;
                }
            }

            return false;
        }

        public static bool CheckMD5(string MD5Hash, string path)
        {
            if (!File.Exists(path))
                return false;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    string clientMD5 = BitConverter.ToString(hash).Replace("-", "");
                    if (clientMD5.Equals(MD5Hash))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool checkClientMD5(string client)
        {
            if (!GlobalVars.AdminMode)
            {
                if (!GlobalVars.SelectedClientInfo.AlreadyHasSecurity)
                {
                    string rbxexe = "";
                    string BasePath = GlobalPaths.BasePath + "\\clients\\" + client;
                    if (GlobalVars.SelectedClientInfo.LegacyMode)
                    {
                        rbxexe = BasePath + "\\RobloxApp.exe";
                    }
                    else if (GlobalVars.SelectedClientInfo.SeperateFolders)
                    {
                        rbxexe = BasePath + "\\client\\RobloxApp_client.exe";
                    }
                    else if (GlobalVars.SelectedClientInfo.UsesCustomClientEXEName)
                    {
                        rbxexe = BasePath + @"\\" + GlobalVars.SelectedClientInfo.CustomClientEXEName;
                    }
                    else
                    {
                        rbxexe = BasePath + "\\RobloxApp_client.exe";
                    }
                    return CheckMD5(GlobalVars.SelectedClientInfo.ClientMD5, rbxexe);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public static bool checkScriptMD5(string client)
        {
            if (!GlobalVars.AdminMode)
            {
                if (!GlobalVars.SelectedClientInfo.AlreadyHasSecurity)
                {
                    string rbxscript = GlobalPaths.BasePath + "\\clients\\" + client + "\\clientinfo.nov";
                    return CheckMD5(GlobalVars.SelectedClientInfo.ScriptMD5, rbxscript);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

#if URI
        public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e, Label label)
#else
        public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e)
#endif
        {
            if (GlobalVars.AdminMode || GlobalVars.UserConfiguration.ReadSettingBool("AdditionalDebug"))
            {
                Util.ConsolePrint("Starting Script Type " + type, 4);
            }

#if LAUNCHER
            DecompressMap(type, nomap);
#endif

            switch (type)
            {
                case ScriptType.Server:
                    if (GlobalVars.UserConfiguration.ReadSettingBool("FirstServerLaunch"))
                    {
#if LAUNCHER
                        string hostingTips = "Tips for your first time:\n\nMake sure your server's port forwarded (set up in your router), going through a tunnel server, or running from UPnP.\n" +
                            "If your port is forwarded or you are going through a tunnel server, make sure your port is set up as UDP, not TCP.\n" +
                            "Roblox does NOT use TCP, only UDP. However, if your server doesn't work with just UDP, feel free to set up TCP too as that might help the issue in some cases.";

                        if (!GlobalVars.isConsoleOnly)
                        {
                            MessageBox.Show(hostingTips, "Novetus - Hosting Tips", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Util.ConsolePrint("Tips: " + hostingTips, 4);
                        }
#endif
                        GlobalVars.UserConfiguration.SaveSettingBool("FirstServerLaunch", false);
                    }
                    break;
                default:
                    break;
            }

            FileFormat.ClientInfo info = GetClientInfoValues(ClientName);
            GlobalVars.ClientLoadDelay = DateTime.Now.AddMinutes(info.ClientLaunchTime + Math.Min(GlobalVars.UserConfiguration.ReadSettingDouble("ClientLaunchTimeOffset"), 2.0D));
            if (GlobalVars.AdminMode || GlobalVars.UserConfiguration.ReadSettingBool("AdditionalDebug"))
            {
                Util.ConsolePrint("Delay time set to " + GlobalVars.ClientLoadDelay, 4);
            }
            ReadClientValues(ClientName);
            // delete any extra scripts, then make a new one
            ResetScripts();
            Script.Generator.GenerateLaunchScriptForClient(ClientName, type);
            string rbxexe = GetClientEXEDir(ClientName, type);
            bool is3DView = (type.Equals(ScriptType.OutfitView));
            string mapfilepath = nomap ? (type.Equals(ScriptType.Studio) ? GlobalPaths.DataDir + "\\Place1.rbxl" : "") : GlobalVars.UserConfiguration.ReadSetting("MapPath").Replace(".bz2", "");
            string mapfilename = nomap ? "" : GlobalVars.UserConfiguration.ReadSetting("Map").Replace(".bz2", "");
            string mapfile = (GlobalVars.EasterEggMode && type != ScriptType.Solo) ? GlobalPaths.DataDir + "\\Appreciation.rbxl" :
                (is3DView ? GlobalPaths.BasePath + "\\clients\\" + ClientName + "\\content\\fonts\\3DView.rbxl" : mapfilepath);
            string mapname = ((GlobalVars.EasterEggMode && type != ScriptType.Solo) || is3DView) ? "" : mapfilename;
            string quote = "\"";
            string args = "";
            GlobalVars.ValidatedExtraFiles = 0;

            bool v1 = false;

            if (info.CommandLineArgs.Contains("<") &&
                info.CommandLineArgs.Contains("</") &&
                info.CommandLineArgs.Contains(">"))
            {
                v1 = true;
            }

            if (!GlobalVars.AdminMode && !info.AlreadyHasSecurity)
            {
                string validstart = "<validate>";
                string validend = "</validate>";
                string validv2 = "validate=";

                foreach (string line in info.CommandLineArgs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (v1 && line.Contains(validstart) && line.Contains(validend))
                    {
                        bool validated = false;

#if URI
                        validated = ValidateFiles(line, validstart, validend, label);
#else
                        validated = ValidateFiles(line, validstart, validend);
#endif

                        if (!validated)
                            return;
                    }
                    else if (line.Contains(validv2))
                    {
                        bool validated = false;
#if URI
                        validated = ValidateFiles(line, validv2, "", label);
#else
                        validated = ValidateFiles(line, validv2, "");
#endif
                        if (!validated)
                            return;
                    }
                }
            }

            switch (type)
            {
                case ScriptType.Studio:
                    break;
                case ScriptType.Server:
                    NovetusFuncs.PingMasterServer(true, "Server will now display on the defined master server, if available.");
                    goto default;
                default:
                    GlobalVars.GameOpened = type;
                    break;
            }

            FileManagement.ReloadLoadoutValue();

            if (info.CommandLineArgs.Contains("%args%"))
            {
                if (!info.Fix2007)
                {
                    args = quote
                        + mapfile
                        + "\" -script \" dofile('" + GetLaunchScriptName(ClientName, type) + "'); "
                        + Script.Generator.GetScriptFuncForType(ClientName, type)
                        + quote
                        + (no3d ? " -no3d" : "");
                }
                else
                {
                    Script.Generator.GenerateScriptForClient(ClientName, type);
                    args = "-script "
                        + quote
                        + GetGenLuaName(ClientName, type)
                        + quote
                        + (no3d ? " -no3d" : "")
                        + " "
                        + quote
                        + mapfile
                        + quote;
                }
            }
            else
            {
                args = Script.ClientScript.CompileScript(ClientName, info.CommandLineArgs,
                    Script.ClientScript.GetTagFromType(type, false, v1),
                    Script.ClientScript.GetTagFromType(type, true, v1),
                    mapfile,
                    GetLaunchScriptName(ClientName, type),
                    rbxexe,
                    no3d,
                    type);
            }

            if (args == "")
                return;

            try
            {
                Util.ConsolePrint("Client Loaded.", 4);

                if (type.Equals(ScriptType.Client))
                {
                    if (!GlobalVars.AdminMode)
                    {
                        if (info.AlreadyHasSecurity != true)
                        {
                            if (checkClientMD5(ClientName) && checkScriptMD5(ClientName))
                            {
                                OpenClient(type, rbxexe, args, ClientName, mapname, e);
                            }
                            else
                            {
#if URI
                                UpdateStatus(label, "The client has been detected as modified. [Client.LaunchRBXClient]");
#elif LAUNCHER
                            Util.ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified. [Client.LaunchRBXClient])", 2);
#endif

#if LAUNCHER
                            if (!GlobalVars.isConsoleOnly)
                            {
                                MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified. [Client.LaunchRBXClient])", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
#endif

#if URI
                                throw new IOException("The client has been detected as modified. [Client.LaunchRBXClient]");
#else
                                return;
#endif
                            }
                        }
                        else
                        {
                            OpenClient(type, rbxexe, args, ClientName, mapname, e);
                        }
                    }
                    else
                    {
                        OpenClient(type, rbxexe, args, ClientName, mapname, e);
                    }
                }
                else
                {
                    OpenClient(type, rbxexe, args, ClientName, mapname, e);
                }

                GlobalVars.ValidatedExtraFiles = 0;
            }
            catch (Exception ex)
            {
#if URI
                UpdateStatus(label, "Error: " + ex.Message);
#elif LAUNCHER
                Util.ConsolePrint("ERROR - Failed to launch Novetus. (Error: " + ex.Message + ")", 2);
#endif

#if URI || LAUNCHER
                if (!GlobalVars.isConsoleOnly)
                {
                    MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#endif
                Util.LogExceptions(ex);

#if URI
                //toss the exception back to the URI
                throw new Exception(ex.Message);
#endif
            }
        }

        public static void OpenClient(ScriptType type, string rbxexe, string args, string clientname, string mapname, EventHandler e, bool customization = false)
        {
            if (GlobalVars.AdminMode || GlobalVars.UserConfiguration.ReadSettingBool("AdditionalDebug"))
            {
                Util.ConsolePrint("Starting Process " + rbxexe + " " + args, 4);
            }
            else
            {
                Util.ConsolePrint("Starting Process " + rbxexe, 4);
            }

            Process client = new Process();
            client.StartInfo.FileName = rbxexe;
            client.StartInfo.WorkingDirectory = Path.GetDirectoryName(rbxexe);
            client.StartInfo.Arguments = args;
            client.StartInfo.CreateNoWindow = false;
            client.StartInfo.UseShellExecute = false;
            if (e != null)
            {
                client.EnableRaisingEvents = true;
                client.Exited += e;
            }
            client.Start();
            client.PriorityClass = (ProcessPriorityClass)GlobalVars.UserConfiguration.ReadSettingInt("Priority");

            if (!customization)
            {
                SecurityFuncs.RenameWindow(client, type, clientname, mapname);
                if (e != null)
                {
                    UpdateRichPresence(GetStateForType(type), clientname, mapname);
                }
            }

            //TODO: make a command that uses this.
#if CMD_LEGACY
            NovetusFuncs.CreateTXT();
#endif
        }

        public static bool IsClientValid(string client)
        {
            string clientdir = GlobalPaths.ClientDir;
            DirectoryInfo dinfo = new DirectoryInfo(clientdir);
            DirectoryInfo[] Dirs = dinfo.GetDirectories();
            foreach (DirectoryInfo dir in Dirs)
            {
                if (dir.Name == client)
                {
                    return true;
                }
            }

            return false;
        }
    }
#endregion
}
