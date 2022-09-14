#region Usings
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Reflection;
#endregion

#region Client Management
public class ClientManagement
{
    public static void ReadClientValues(bool initial = false)
    {
        ReadClientValues(GlobalVars.UserConfiguration.SelectedClient, initial);
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
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
                    Util.FixedFileCopy(dir, fullpath, false);
                }
            }
        }

        ChangeGameSettings(ClientName);
    }

    //Modified from https://stackoverflow.com/questions/4286487/is-there-any-lorem-ipsum-generator-in-c
    public static string LoremIpsum(int minWords, int maxWords,
    int minSentences, int maxSentences,
    int numParagraphs)
    {

        var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
        "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
        "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

        var rand = new Random();
        int numSentences = rand.Next(maxSentences - minSentences)
            + minSentences + 1;
        int numWords = rand.Next(maxWords - minWords) + minWords + 1;

        StringBuilder result = new StringBuilder();

        for (int p = 0; p < numParagraphs; p++)
        {
            result.Append("lorem ipsum ");
            for (int s = 0; s < numSentences; s++)
            {
                for (int w = 0; w < numWords; w++)
                {
                    if (w > 0) { result.Append(" "); }
                    result.Append(words[rand.Next(words.Length)]);
                }
                result.Append(". ");
            }
        }

        return result.ToString();
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

        string ClientScriptMD5 = File.Exists(path + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") ? SecurityFuncs.GenerateMD5(path + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";

        if (!string.IsNullOrWhiteSpace(ClientScriptMD5))
        {
            DefaultClientInfo.ScriptMD5 = ClientScriptMD5.ToUpper(CultureInfo.InvariantCulture);
        }
        /*else
        {
            IOException clientNotFoundEX = new IOException("Could not find script file for MD5 generation. You must have a CSMPFunctions.lua script in your client's content/scripts folder.");
            throw clientNotFoundEX;
        }*/

        string desc = "This client information file for '" + GlobalVars.UserConfiguration.SelectedClient +
            "' was pre-generated by Novetus for your convienence. You will need to load this clientinfo.nov file into the Client SDK for additional options. "
            + LoremIpsum(1, 128, 1, 6, 1);

        DefaultClientInfo.Description = desc;

        string[] lines = {
                    SecurityFuncs.Base64Encode(DefaultClientInfo.UsesPlayerName.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.UsesID.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.Warning.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.LegacyMode.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.ClientMD5.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.ScriptMD5.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.Description.ToString()),
                    SecurityFuncs.Base64Encode(placeholder.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.Fix2007.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.AlreadyHasSecurity.ToString()),
                    SecurityFuncs.Base64Encode(((int)DefaultClientInfo.ClientLoadOptions).ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.SeperateFolders.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.UsesCustomClientEXEName.ToString()),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.CustomClientEXEName.ToString().Replace("\\", "")),
                    SecurityFuncs.Base64Encode(DefaultClientInfo.CommandLineArgs.ToString())
                };

        File.WriteAllText(path + "\\clientinfo.nov", SecurityFuncs.Base64Encode(string.Join("|", lines)));
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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
            usescustomname, customname;

        using (StreamReader reader = new StreamReader(clientpath))
        {
            file = reader.ReadLine();
        }

        string ConvertedLine = SecurityFuncs.Base64Decode(file);
        string[] result = ConvertedLine.Split('|');
        usesplayername = SecurityFuncs.Base64Decode(result[0]);
        usesid = SecurityFuncs.Base64Decode(result[1]);
        warning = SecurityFuncs.Base64Decode(result[2]);
        legacymode = SecurityFuncs.Base64Decode(result[3]);
        clientmd5 = SecurityFuncs.Base64Decode(result[4]);
        scriptmd5 = SecurityFuncs.Base64Decode(result[5]);
        desc = SecurityFuncs.Base64Decode(result[6]);
        fix2007 = SecurityFuncs.Base64Decode(result[8]);
        alreadyhassecurity = SecurityFuncs.Base64Decode(result[9]);
        clientloadoptions = SecurityFuncs.Base64Decode(result[10]);
        folders = "False";
        usescustomname = "False";
        customname = "";
        try
        {
            commandlineargs = SecurityFuncs.Base64Decode(result[11]);

            bool parsedValue;
            if (bool.TryParse(commandlineargs, out parsedValue))
            {
                folders = SecurityFuncs.Base64Decode(result[11]);
                commandlineargs = SecurityFuncs.Base64Decode(result[12]);
                bool parsedValue2;
                if (bool.TryParse(commandlineargs, out parsedValue2))
                {
                    usescustomname = SecurityFuncs.Base64Decode(result[12]);
                    customname = SecurityFuncs.Base64Decode(result[13]);
                    commandlineargs = SecurityFuncs.Base64Decode(result[14]);
                }
            }
        }
        catch (Exception)
        {
            //fake this option until we properly apply it.
            clientloadoptions = "2";
            commandlineargs = SecurityFuncs.Base64Decode(result[10]);
        }

        info.UsesPlayerName = Convert.ToBoolean(usesplayername);
        info.UsesID = Convert.ToBoolean(usesid);
        info.Warning = warning;
        info.LegacyMode = Convert.ToBoolean(legacymode);
        info.ClientMD5 = clientmd5;
        info.ScriptMD5 = scriptmd5;
        info.Description = desc;
        info.Fix2007 = Convert.ToBoolean(fix2007);
        info.AlreadyHasSecurity = Convert.ToBoolean(alreadyhassecurity);
        if (clientloadoptions.Equals("True") || clientloadoptions.Equals("False"))
        {
            info.ClientLoadOptions = Settings.GetClientLoadOptionsForBool(Convert.ToBoolean(clientloadoptions));
        }
        else
        {
            info.ClientLoadOptions = (Settings.ClientLoadOptions)Convert.ToInt32(clientloadoptions);
        }

        info.SeperateFolders = Convert.ToBoolean(folders);
        info.UsesCustomClientEXEName = Convert.ToBoolean(usescustomname);
        info.CustomClientEXEName = customname;
        info.CommandLineArgs = commandlineargs;
    }

    public static GlobalVars.LauncherState GetStateForType(ScriptType type)
    {
        switch (type)
        {
            case ScriptType.Client:
                return GlobalVars.LauncherState.InMPGame;
            case ScriptType.Solo:
                return GlobalVars.LauncherState.InSoloGame;
            case ScriptType.Studio:
                return GlobalVars.LauncherState.InStudio;
            case ScriptType.EasterEgg:
                return GlobalVars.LauncherState.InEasterEggGame;
            default:
                return GlobalVars.LauncherState.InLauncher;
        }
    }

    public static void UpdateRichPresence(GlobalVars.LauncherState state, bool initial = false)
    {
        string mapname = "";
        if (GlobalVars.GameOpened != ScriptType.Client)
        {
            mapname = GlobalVars.UserConfiguration.Map;
        }

        UpdateRichPresence(state, GlobalVars.UserConfiguration.SelectedClient, mapname, initial);
    }

    public static void UpdateRichPresence(GlobalVars.LauncherState state, string mapname, bool initial = false)
    {
        UpdateRichPresence(state, GlobalVars.UserConfiguration.SelectedClient, mapname, initial);
    }

    public static void UpdateRichPresence(GlobalVars.LauncherState state, string clientname, string mapname, bool initial = false)
    {
        if (GlobalVars.UserConfiguration.DiscordPresence)
        {
            if (initial)
            {
                GlobalVars.presence.largeImageKey = GlobalVars.imagekey_large;
                GlobalVars.presence.startTimestamp = SecurityFuncs.UnixTimeNow();
            }

            string ValidMapname = (string.IsNullOrWhiteSpace(mapname) ? "Place1" : mapname);

            switch (state)
            {
                case GlobalVars.LauncherState.InLauncher:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_inlauncher;
                    GlobalVars.presence.state = "In Launcher";
                    GlobalVars.presence.details = "Selected " + clientname;
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In Launcher";
                    break;
                case GlobalVars.LauncherState.InMPGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + clientname + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + clientname + " Multiplayer Game";
                    break;
                case GlobalVars.LauncherState.InSoloGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + clientname + " Solo Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + clientname + " Solo Game";
                    break;
                case GlobalVars.LauncherState.InStudio:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_instudio;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + clientname + " Studio";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + clientname + " Studio";
                    break;
                case GlobalVars.LauncherState.InCustomization:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_incustomization;
                    GlobalVars.presence.details = "Customizing " + GlobalVars.UserConfiguration.PlayerName;
                    GlobalVars.presence.state = "In Character Customization";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In Character Customization";
                    break;
                case GlobalVars.LauncherState.InEasterEggGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Reading a message.";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "Reading a message.";
                    break;
                case GlobalVars.LauncherState.LoadingURI:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Joining a " + clientname + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "Joining a " + clientname + " Multiplayer Game";
                    break;
                default:
                    break;
            }

            DiscordRPC.UpdatePresence(ref GlobalVars.presence);
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
                string fullFilePath = Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + file;
                Util.FixedFileDelete(fullFilePath);
            }

            if (GlobalVars.UserConfiguration.QualityLevel != Settings.Level.Custom)
            {
                int GraphicsMode = 0;

                if (info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                        info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomatic)
                {
                    GraphicsMode = 1;
                }
                else
                {
                    if (info.ClientLoadOptions != Settings.ClientLoadOptions.Client_2007_NoGraphicsOptions ||
                        info.ClientLoadOptions != Settings.ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions)
                    {

                        switch (GlobalVars.UserConfiguration.GraphicsMode)
                        {
                            case Settings.Mode.OpenGLStable:
                                switch (info.ClientLoadOptions)
                                {
                                    case Settings.ClientLoadOptions.Client_2007:
                                    case Settings.ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
                                    case Settings.ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                                        GraphicsMode = 2;
                                        break;
                                    case Settings.ClientLoadOptions.Client_2008AndUp:
                                    case Settings.ClientLoadOptions.Client_2008AndUp_QualityLevel21:
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
                if (info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                        info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_QualityLevel21)
                {
                    GFXQualityLevel = 21;
                }
                int MaterialQuality = 3;
                int AASamples = 8;
                int Bevels = 1;
                int Shadows_2008 = 1;
                int AA = 1;
                bool Shadows_2007 = true;

                switch (GlobalVars.UserConfiguration.QualityLevel)
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
                        if (info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomatic ||
                            info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                            info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_QualityLevel21 ||
                            info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL)
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

                if (info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                        info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomatic)
                {
                    GraphicsMode = 1;
                }
                else
                {
                    if (info.ClientLoadOptions != Settings.ClientLoadOptions.Client_2007_NoGraphicsOptions ||
                        info.ClientLoadOptions != Settings.ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions)
                    {

                        switch (GlobalVars.UserConfiguration.GraphicsMode)
                        {
                            case Settings.Mode.OpenGLStable:
                                switch (info.ClientLoadOptions)
                                {
                                    case Settings.ClientLoadOptions.Client_2007:
                                    case Settings.ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
                                    case Settings.ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                                        GraphicsMode = 2;
                                        break;
                                    case Settings.ClientLoadOptions.Client_2008AndUp:
                                    case Settings.ClientLoadOptions.Client_2008AndUp_QualityLevel21:
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
                        Util.FixedFileCopy(dir, Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                            .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                            .Replace("-", ".xml"), true);
                    }
                }
            }
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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

            if (info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomaticQL21 ||
                    info.ClientLoadOptions == Settings.ClientLoadOptions.Client_2008AndUp_ForceAutomatic)
            {
                GraphicsMode = 1;
            }
            else
            {
                if (info.ClientLoadOptions != Settings.ClientLoadOptions.Client_2007_NoGraphicsOptions ||
                    info.ClientLoadOptions != Settings.ClientLoadOptions.Client_2008AndUp_NoGraphicsOptions)
                {
                    switch (GlobalVars.UserConfiguration.GraphicsMode)
                    {
                        case Settings.Mode.OpenGLStable:
                            switch (info.ClientLoadOptions)
                            {
                                case Settings.ClientLoadOptions.Client_2007:
                                case Settings.ClientLoadOptions.Client_2008AndUp_LegacyOpenGL:
                                case Settings.ClientLoadOptions.Client_2008AndUp_HasCharacterOnlyShadowsLegacyOpenGL:
                                    GraphicsMode = 2;
                                    break;
                                case Settings.ClientLoadOptions.Client_2008AndUp:
                                case Settings.ClientLoadOptions.Client_2008AndUp_QualityLevel21:
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                    catch (Exception ex)
                    {
                        Util.LogExceptions(ex);
#else
		            catch (Exception)
		            {
#endif
                        return;
                    }

                    try
                    {
                        if (GraphicsMode != 0)
                        {
                            RobloxXML.EditRenderSettings(doc, "graphicsMode", GraphicsMode.ToString(), XMLTypes.Token);
                        }

                        if (!onlyGraphicsMode)
                        {
                            RobloxXML.EditRenderSettings(doc, "maxMeshDetail", MeshDetail.ToString(), XMLTypes.Float);
                            RobloxXML.EditRenderSettings(doc, "maxShadingQuality", ShadingQuality.ToString(), XMLTypes.Float);
                            RobloxXML.EditRenderSettings(doc, "minMeshDetail", MeshDetail.ToString(), XMLTypes.Float);
                            RobloxXML.EditRenderSettings(doc, "minShadingQuality", ShadingQuality.ToString(), XMLTypes.Float);
                            RobloxXML.EditRenderSettings(doc, "AluminumQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "CompoundMaterialQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "CorrodedMetalQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "DiamondPlateQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "GrassQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "IceQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "PlasticQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "SlateQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            // fix truss detail. We're keeping it at 0.
                            RobloxXML.EditRenderSettings(doc, "TrussDetail", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "WoodQuality", MaterialQuality.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "Antialiasing", AA.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "AASamples", AASamples.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "Bevels", Bevels.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "Shadow", Shadows_2008.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "Shadows", Shadows_2007.ToString().ToLower(), XMLTypes.Bool);
                            RobloxXML.EditRenderSettings(doc, "shadows", Shadows_2007.ToString().ToLower(), XMLTypes.Bool);
                            RobloxXML.EditRenderSettings(doc, "_skinFile", !string.IsNullOrWhiteSpace(Style_2007) ? @"Styles\" + Style_2007 : "", XMLTypes.String);
                            RobloxXML.EditRenderSettings(doc, "QualityLevel", GFXQualityLevel.ToString(), XMLTypes.Token);
                            RobloxXML.EditRenderSettings(doc, "FullscreenSizePreference", fullRes.ToString(), XMLTypes.Vector2Int16);
                            RobloxXML.EditRenderSettings(doc, "FullscreenSize", fullRes.ToString(), XMLTypes.Vector2Int16);
                            RobloxXML.EditRenderSettings(doc, "WindowSizePreference", winRes.ToString(), XMLTypes.Vector2Int16);
                            RobloxXML.EditRenderSettings(doc, "WindowSize", winRes.ToString(), XMLTypes.Vector2Int16);
                            RobloxXML.EditRenderSettings(doc, "Resolution", ModernResolution.ToString(), XMLTypes.Token);
                        }
                    }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                    catch (Exception ex)
                    {
                        Util.LogExceptions(ex);
#else
		            catch (Exception)
		            {
#endif
                        return;
                    }
                    finally
                    {
                        doc.Save(dir);
                        Util.FixedFileCopy(dir, Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                            .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                            .Replace("-", ".xml"), true);
                    }
                }
            }
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            return;
        }
    }

    public static string GetGenLuaFileName(string ClientName, ScriptType type)
    {
        string luafile = "";

        bool rbxasset = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%userbxassetforgeneration%");

        if (!rbxasset)
        {
            if (GlobalVars.SelectedClientInfo.SeperateFolders)
            {
                luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua";
            }
            else
            {
                luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua";
            }
        }
        else
        {
            luafile = @"rbxasset://scripts\\" + GlobalPaths.ScriptGenName + ".lua";
        }

        return luafile;
    }

    public static string GetLuaFileName(ScriptType type)
    {
        return GetLuaFileName(GlobalVars.UserConfiguration.SelectedClient, type);
    }

    public static string GetLuaFileName(string ClientName, ScriptType type)
    {
        string luafile = "";

        if (!GlobalVars.SelectedClientInfo.Fix2007)
        {
            bool HasGenerateScript = false;

            foreach (string line in GlobalVars.SelectedClientInfo.CommandLineArgs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Contains("%generatescript%"))
                {
                    HasGenerateScript = true;
                }
            }

            if (HasGenerateScript)
            {
                luafile = ScriptFuncs.Generator.GetGeneratedScriptName(ClientName, type);
            }
            else
            {
                luafile = "rbxasset://scripts\\\\" + GlobalPaths.ScriptName + ".lua";
            }
        }
        else
        {
            luafile = GetGenLuaFileName(ClientName, type);
        }

        return luafile;
    }

    public static string GetClientSeperateFolderName(ScriptType type)
    {
        string rbxfolder = "";
        switch (type)
        {
            case ScriptType.Client:
            case ScriptType.Solo:
            case ScriptType.EasterEgg:
                rbxfolder = "client";
                break;
            case ScriptType.Server:
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
        return GetClientEXEDir(GlobalVars.UserConfiguration.SelectedClient, type);
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
                case ScriptType.EasterEgg:
                    rbxexe = BasePath + @"\\" + GetClientSeperateFolderName(type) + @"\\RobloxApp_client.exe";
                    break;
                case ScriptType.Server:
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
                    rbxexe = BasePath + @"\\RobloxApp_client.exe";
                    break;
                case ScriptType.Server:
                    rbxexe = BasePath + @"\\RobloxApp_server.exe";
                    break;
                case ScriptType.Studio:
                    rbxexe = BasePath + @"\\RobloxApp_studio.exe";
                    break;
                case ScriptType.Solo:
                case ScriptType.EasterEgg:
                    rbxexe = BasePath + @"\\RobloxApp_solo.exe";
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

#if URI
    public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e, Label label)
#else
    public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e)
#endif
    {
#if URI
        LaunchRBXClient(GlobalVars.UserConfiguration.SelectedClient, type, no3d, nomap, e, label);
#else
        LaunchRBXClient(GlobalVars.UserConfiguration.SelectedClient, type, no3d, nomap, e);
#endif
    }

#if URI
    public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e, Label label)
#else
    public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e)
#endif
    {
        switch (type)
        {
            case ScriptType.Client:
                FileManagement.ReloadLoadoutValue(true);
                if (!GlobalVars.LocalPlayMode && GlobalVars.GameOpened != ScriptType.Server)
                {
                    goto default;
                }
                break;
            case ScriptType.Server:
                if (GlobalVars.GameOpened == ScriptType.Server)
                {
                    Util.ConsolePrint("ERROR - Failed to launch Novetus. (A server is already running.)", 2);

#if LAUNCHER
                    MessageBox.Show("Failed to launch Novetus. (Error: A server is already running.)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    return;
                }
                else if (GlobalVars.UserConfiguration.FirstServerLaunch)
                {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                    string hostingTips = "For your first time hosting a server, make sure your server's port forwarded (set up in your router), going through a tunnel server, or running from UPnP.\n" +
                        "If your port is forwarded or you are going through a tunnel server, make sure your port is set up as UDP, not TCP.\n" +
                        "Roblox does NOT use TCP, only UDP. However, if your server doesn't work with just UDP, feel free to set up TCP too as that might help the issue in some cases.";
#pragma warning restore CS0219 // Variable is assigned but its value is never used
#if LAUNCHER
                    MessageBox.Show(hostingTips, "Novetus - Hosting Tips", MessageBoxButtons.OK, MessageBoxIcon.Information);
#elif CMD
                    Util.ConsolePrint(hostingTips + "\nPress any key to continue...", 4);
                    Console.ReadKey();
#endif
                    GlobalVars.UserConfiguration.FirstServerLaunch = false;
                }
                else
                {
                    goto default;
                }
                break;
            case ScriptType.Solo:
                FileManagement.ReloadLoadoutValue(true);
                goto default;
            default:
                if (GlobalVars.GameOpened != ScriptType.None)
                {
                    Util.ConsolePrint("ERROR - Failed to launch Novetus. (A game is already running.)", 2);

#if LAUNCHER
                    MessageBox.Show("Failed to launch Novetus. (Error: A game is already running.)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    return;
                }
                break;
        }

        ReadClientValues(ClientName);
        string luafile = GetLuaFileName(ClientName, type);
        string rbxexe = GetClientEXEDir(ClientName, type);
        string mapfile = type.Equals(ScriptType.EasterEgg) ?
            GlobalPaths.DataDir + "\\Appreciation.rbxl" :
            (nomap ? (type.Equals(ScriptType.Studio) ? GlobalPaths.ConfigDir + "\\Place1.rbxl" : "") : GlobalVars.UserConfiguration.MapPath);
        string mapname = type.Equals(ScriptType.EasterEgg) ? "" : (nomap ? "" : GlobalVars.UserConfiguration.Map);
        FileFormat.ClientInfo info = GetClientInfoValues(ClientName);
        string quote = "\"";
        string args = "";
        GlobalVars.ValidatedExtraFiles = 0;

        if (!GlobalVars.AdminMode && !info.AlreadyHasSecurity)
        {
            string validstart = "<validate>";
            string validend = "</validate>";

            foreach (string line in info.CommandLineArgs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Contains(validstart) && line.Contains(validend))
                {
                    string extractedFile = ScriptFuncs.ClientScript.GetArgsFromTag(line, validstart, validend);
                    if (!string.IsNullOrWhiteSpace(extractedFile))
                    {
                        try
                        {
                            string[] parsedFileParams = extractedFile.Split('|');
                            string filePath = parsedFileParams[0];
                            string fileMD5 = parsedFileParams[1];
                            string fullFilePath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\" + filePath;

                            if (!SecurityFuncs.CheckMD5(fileMD5, fullFilePath))
                            {
#if URI
                                UpdateStatus(label, "The client has been detected as modified.");
#elif LAUNCHER || CMD
                                Util.ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified.)", 2);
#endif

#if LAUNCHER
                                MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified.)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif

#if URI
                                throw new IOException("The client has been detected as modified.");
#else
                                return;
#endif
                            }
                            else
                            {
                                GlobalVars.ValidatedExtraFiles += 1;
                            }
                        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                        catch (Exception ex)
                        {
                            Util.LogExceptions(ex);
#else
							catch (Exception)
							{
#endif
                            continue;
                        }
                    }
                }
            }
        }

        if (info.CommandLineArgs.Contains("%args%"))
        {
            if (!info.Fix2007)
            {
                args = quote
                    + mapfile
                    + "\" -script \" dofile('" + luafile + "'); "
                    + ScriptFuncs.Generator.GetScriptFuncForType(ClientName, type)
                    + quote
                    + (no3d ? " -no3d" : "");
            }
            else
            {
                ScriptFuncs.Generator.GenerateScriptForClient(ClientName, type);
                args = "-script "
                    + quote
                    + luafile
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
            args = ScriptFuncs.ClientScript.CompileScript(ClientName, info.CommandLineArgs,
                ScriptFuncs.ClientScript.GetTagFromType(type, false, no3d),
                ScriptFuncs.ClientScript.GetTagFromType(type, true, no3d),
                mapfile,
                luafile,
                rbxexe);
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
                        if (SecurityFuncs.checkClientMD5(ClientName) && SecurityFuncs.checkScriptMD5(ClientName))
                        {
                            OpenClient(type, rbxexe, args, ClientName, mapname, e);
                        }
                        else
                        {
#if URI
                            UpdateStatus(label, "The client has been detected as modified.");
#elif LAUNCHER || CMD
                            Util.ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified.)", 2);
#endif

#if LAUNCHER
                            MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified.)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif

#if URI
                            throw new IOException("The client has been detected as modified.");
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

            switch (type)
            {
                case ScriptType.Client:
                    if (!GlobalVars.LocalPlayMode && GlobalVars.GameOpened != ScriptType.Server)
                    {
                        goto default;
                    }
                    break;
                case ScriptType.Studio:
                    break;
                case ScriptType.Server:
                    NovetusFuncs.PingMasterServer(true, "Server will now display on the defined master server.");
                    goto default;
                default:
                    GlobalVars.GameOpened = type;
                    break;
            }

            GlobalVars.ValidatedExtraFiles = 0;
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
#else
        catch (Exception)
#endif
        {
#if URI
            UpdateStatus(label, "Error: " + ex.Message);
#elif LAUNCHER || CMD
            Util.ConsolePrint("ERROR - Failed to launch Novetus. (Error: " + ex.Message + ")", 2);
#endif

#if URI || LAUNCHER
            MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            Util.LogExceptions(ex);
#endif

#if URI
            //toss the exception back to the URI
            throw new Exception(ex.Message);
#endif
        }
    }

    public static void OpenClient(ScriptType type, string rbxexe, string args, string clientname, string mapname, EventHandler e, bool customization = false)
    {
        Process client = new Process();
        client.StartInfo.FileName = rbxexe;
        client.StartInfo.WorkingDirectory = Path.GetDirectoryName(rbxexe);
        client.StartInfo.Arguments = args;
        if (e != null)
        {
            client.EnableRaisingEvents = true;
            client.Exited += e;
        }
        client.Start();
        client.PriorityClass = GlobalVars.UserConfiguration.Priority;

        if (!customization)
        {
            SecurityFuncs.RenameWindow(client, type, clientname, mapname);
            if (e != null)
            {
                UpdateRichPresence(GetStateForType(type), clientname, mapname);
            }
        }

#if CMD
        GlobalVars.ProcessID = client.Id;
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

#region Script Functions
public class ScriptFuncs
{
    #region Script Generator/Signer
    public class Generator
    {
        public static void SignGeneratedScript(string scriptFileName, bool newSigFormat = false, bool encodeInBase64 = true)
        {
            string privateKeyPath = Path.GetDirectoryName(scriptFileName) + "//privatekey.txt";

            if (File.Exists(privateKeyPath))
            {
                //init vars
                string format = (newSigFormat ? "--rbxsig" : "") + "%{0}%{1}";
                byte[] blob = Encoding.Default.GetBytes(File.ReadAllText(privateKeyPath));

                if (encodeInBase64)
                {
                    blob = Convert.FromBase64String(Encoding.Default.GetString(blob));
                }

                //create cryptography providers
                var shaCSP = new SHA1CryptoServiceProvider();
                var rsaCSP = new RSACryptoServiceProvider();
                rsaCSP.ImportCspBlob(blob);

                // sign script
                string script = "\r\n" + File.ReadAllText(scriptFileName);
                byte[] signature = rsaCSP.SignData(Encoding.Default.GetBytes(script), shaCSP);
                // override file.
                Util.FixedFileDelete(scriptFileName);
                File.WriteAllText(scriptFileName, string.Format(format, Convert.ToBase64String(signature), script));
            }
            else
            {
                //create the signature file if it doesn't exist
                var signingRSACSP = new RSACryptoServiceProvider(1024);
                byte[] privateKeyBlob = signingRSACSP.ExportCspBlob(true);
                signingRSACSP.Dispose();

                // save our text file in the script's directory
                File.WriteAllText(privateKeyPath, encodeInBase64 ? Convert.ToBase64String(privateKeyBlob) : Encoding.Default.GetString(privateKeyBlob));

                // try signing again.
                SignGeneratedScript(scriptFileName, encodeInBase64);
            }
        }

        public static string GetScriptFuncForType(ScriptType type)
        {
            return GetScriptFuncForType(GlobalVars.UserConfiguration.SelectedClient, type);
        }

        public static string GetScriptFuncForType(string ClientName, ScriptType type)
        {
            FileFormat.ClientInfo info = ClientManagement.GetClientInfoValues(ClientName);

            string rbxexe = "";
            if (info.LegacyMode)
            {
                rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp.exe";
            }
            else if (info.SeperateFolders)
            {
                rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\client\\RobloxApp_client.exe";
            }
            else if (info.UsesCustomClientEXEName)
            {
                rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + info.CustomClientEXEName;
            }
            else
            {
                rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_client.exe";
            }

#if LAUNCHER
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
            string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
            string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
            string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
            string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";

            switch (type)
            {
                case ScriptType.Client:
                    return "_G.CSConnect("
                        + (info.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
                        + GlobalVars.IP + "',"
                        + GlobalVars.JoinPort + ",'"
                        + (info.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
                        + GlobalVars.Loadout + ","
                        + md5s + ",'"
                        + GlobalVars.PlayerTripcode
                        + ((GlobalVars.ValidatedExtraFiles > 0) ? "'," + GlobalVars.ValidatedExtraFiles.ToString() + "," : "',0,")
                        + GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
                case ScriptType.Server:
                    return "_G.CSServer("
                        + GlobalVars.UserConfiguration.RobloxPort + ","
                        + GlobalVars.UserConfiguration.PlayerLimit + ","
                        + md5s + ","
                        + GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower()
                        + ((GlobalVars.ValidatedExtraFiles > 0) ? "," + GlobalVars.ValidatedExtraFiles.ToString() + "," : ",0,")
                        + GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
                case ScriptType.Solo:
                case ScriptType.EasterEgg:
                    return "_G.CSSolo("
                        + (info.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
                        + (info.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
                        + GlobalVars.soloLoadout + ","
                        + GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
                case ScriptType.Studio:
                    return "_G.CSStudio("
                        + GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
                default:
                    return "";
            }
        }

        public static string GetNameForType(ScriptType type)
        {
            switch (type)
            {
                case ScriptType.Client:
                    return "Client";
                case ScriptType.Server:
                    return "Server";
                case ScriptType.Solo:
                    return "Play Solo";
                case ScriptType.Studio:
                    return "Studio";
                case ScriptType.EasterEgg:
                    return "A message from Bitl";
                default:
                    return "N/A";
            }
        }

        public static void GenerateScriptForClient(ScriptType type)
        {
            GenerateScriptForClient(GlobalVars.UserConfiguration.SelectedClient, type);
        }

        public static void GenerateScriptForClient(string ClientName, ScriptType type)
        {
            bool shouldUseLoadFile = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%useloadfile%");
            string execScriptMethod = shouldUseLoadFile ? "loadfile" : "dofile";

            string[] code = {
                               "--Load Script",
							   //scriptcontents,
							   (GlobalVars.SelectedClientInfo.SeperateFolders ? "" +
                                    execScriptMethod + "('rbxasset://../../content/scripts/" + GlobalPaths.ScriptName + ".lua')" + (shouldUseLoadFile ? "()" : "") :
                                    execScriptMethod + "('rbxasset://scripts/" + GlobalPaths.ScriptName + ".lua')" + (shouldUseLoadFile ? "()" : "")),
                               GetScriptFuncForType(type),
                            };

            if (GlobalVars.SelectedClientInfo.SeperateFolders)
            {
                string scriptsFolder = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + ClientManagement.GetClientSeperateFolderName(type) + @"\\content\\scripts";
                if (!Directory.Exists(scriptsFolder))
                {
                    Directory.CreateDirectory(scriptsFolder);
                }
            }

            string outputPath = (GlobalVars.SelectedClientInfo.SeperateFolders ?
                        GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + ClientManagement.GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua" :
                        GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua");

            File.WriteAllLines(outputPath, code);

            bool shouldSign = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%signgeneratedjoinscript%");
            bool shouldUseNewSigFormat = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%usenewsignformat%");

            if (shouldSign)
            {
                SignGeneratedScript(outputPath, shouldUseNewSigFormat);
            }
        }

        public static string GetGeneratedScriptName(string ClientName, ScriptType type)
        {
            GenerateScriptForClient(ClientName, type);
            return ClientManagement.GetGenLuaFileName(ClientName, type);
        }
    }
    #endregion

    #region ClientScript Parser
    public class ClientScript
    {
        public static string GetArgsFromTag(string code, string tag, string endtag)
        {
            try
            {
                int pFrom = code.IndexOf(tag) + tag.Length;
                int pTo = code.LastIndexOf(endtag);
                string result = code.Substring(pFrom, pTo - pFrom);
                return result;
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
#else
			catch (Exception)
			{
#endif
                return "%donothing%";
            }
        }

        public static ScriptType GetTypeFromTag(string tag)
        {
            switch (tag)
            {
                case string client when client.Contains("client"):
                    return ScriptType.Client;
                case string server when server.Contains("server"):
                case string no3d when no3d.Contains("no3d"):
                    return ScriptType.Server;
                case string solo when solo.Contains("solo"):
                    return ScriptType.Solo;
                case string studio when studio.Contains("studio"):
                    return ScriptType.Studio;
                default:
                    return ScriptType.None;
            }
        }

        public static string GetTagFromType(ScriptType type, bool endtag, bool no3d)
        {
            switch (type)
            {
                case ScriptType.Client:
                    return endtag ? "</client>" : "<client>";
                case ScriptType.Server:
                    return no3d ? (endtag ? "</no3d>" : "<no3d>") : (endtag ? "</server>" : "<server>");
                case ScriptType.Solo:
                    return endtag ? "</solo>" : "<solo>";
                case ScriptType.Studio:
                    return endtag ? "</studio>" : "<studio>";
                default:
                    return "";
            }
        }

        public static int ConvertIconStringToInt()
        {
            switch (GlobalVars.UserCustomization.Icon)
            {
                case "BC":
                    return 1;
                case "TBC":
                    return 2;
                case "OBC":
                    return 3;
                case "NBC":
                default:
                    return 0;
            }
        }

        public static string GetFolderAndMapName(string source, string seperator)
        {
            try
            {
                string result = source.Substring(0, source.IndexOf(seperator));

                if (File.Exists(GlobalPaths.MapsDir + @"\\" + result + @"\\" + source))
                {
                    return result + @"\\" + source;
                }
                else
                {
                    return source;
                }
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
#else
			catch (Exception)
			{
#endif
                return source;
            }
        }

        public static string GetFolderAndMapName(string source)
        {
            return GetFolderAndMapName(source, " -");
        }

        public static string GetRawArgsForType(ScriptType type, string ClientName, string luafile)
        {
            FileFormat.ClientInfo info = ClientManagement.GetClientInfoValues(ClientName);

            if (!info.Fix2007)
            {
                return Generator.GetScriptFuncForType(ClientName, type);
            }
            else
            {
                return luafile;
            }
        }

        public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
        {
            return CompileScript(GlobalVars.UserConfiguration.SelectedClient, code, tag, endtag, mapfile, luafile, rbxexe, usesharedtags);
        }

        public static string CompileScript(string ClientName, string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
        {
            string start = tag;
            string end = endtag;

            FileFormat.ClientInfo info = ClientManagement.GetClientInfoValues(ClientName);

            ScriptType type = GetTypeFromTag(start);

            //we must have the ending tag before we continue.
            if (string.IsNullOrWhiteSpace(end))
            {
                return "";
            }

            if (usesharedtags)
            {
                string sharedstart = "<shared>";
                string sharedend = "</shared>";

                if (code.Contains(sharedstart) && code.Contains(sharedend))
                {
                    start = sharedstart;
                    end = sharedend;
                }
            }

            if (info.Fix2007)
            {
                Generator.GenerateScriptForClient(type);
            }

            string extractedCode = GetArgsFromTag(code, start, end);

            if (extractedCode.Contains("%donothing%"))
            {
                return "";
            }

#if LAUNCHER
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
            string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
            string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
            string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
            string md5sd = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
            string md5s = "'" + info.ClientMD5 + "','" + md5dir + "','" + info.ScriptMD5 + "'";
            string compiled = extractedCode.Replace("%version%", GlobalVars.ProgramInformation.Version)
                    .Replace("%mapfile%", mapfile)
                    .Replace("%luafile%", luafile)
                    .Replace("%charapp%", GlobalVars.UserCustomization.CharacterID)
                    .Replace("%ip%", GlobalVars.IP)
                    .Replace("%port%", GlobalVars.UserConfiguration.RobloxPort.ToString())
                    .Replace("%joinport%", GlobalVars.JoinPort.ToString())
                    .Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
                    .Replace("%icone%", ConvertIconStringToInt().ToString())
                    .Replace("%icon%", GlobalVars.UserCustomization.Icon)
                    .Replace("%id%", GlobalVars.UserConfiguration.UserID.ToString())
                    .Replace("%face%", GlobalVars.UserCustomization.Face)
                    .Replace("%head%", GlobalVars.UserCustomization.Head)
                    .Replace("%tshirt%", GlobalVars.UserCustomization.TShirt)
                    .Replace("%shirt%", GlobalVars.UserCustomization.Shirt)
                    .Replace("%pants%", GlobalVars.UserCustomization.Pants)
                    .Replace("%hat1%", GlobalVars.UserCustomization.Hat1)
                    .Replace("%hat2%", GlobalVars.UserCustomization.Hat2)
                    .Replace("%hat3%", GlobalVars.UserCustomization.Hat3)
                    .Replace("%faced%", GlobalVars.UserCustomization.Face.Contains("http://") ? GlobalVars.UserCustomization.Face : GlobalPaths.faceGameDir + GlobalVars.UserCustomization.Face)
                    .Replace("%headd%", GlobalPaths.headGameDir + GlobalVars.UserCustomization.Head)
                    .Replace("%tshirtd%", GlobalVars.UserCustomization.TShirt.Contains("http://") ? GlobalVars.UserCustomization.TShirt : GlobalPaths.tshirtGameDir + GlobalVars.UserCustomization.TShirt)
                    .Replace("%shirtd%", GlobalVars.UserCustomization.Shirt.Contains("http://") ? GlobalVars.UserCustomization.Shirt : GlobalPaths.shirtGameDir + GlobalVars.UserCustomization.Shirt)
                    .Replace("%pantsd%", GlobalVars.UserCustomization.Pants.Contains("http://") ? GlobalVars.UserCustomization.Pants : GlobalPaths.pantsGameDir + GlobalVars.UserCustomization.Pants)
                    .Replace("%hat1d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat1)
                    .Replace("%hat2d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat2)
                    .Replace("%hat3d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat3)
                    .Replace("%headcolor%", GlobalVars.UserCustomization.HeadColorID.ToString())
                    .Replace("%torsocolor%", GlobalVars.UserCustomization.TorsoColorID.ToString())
                    .Replace("%larmcolor%", GlobalVars.UserCustomization.LeftArmColorID.ToString())
                    .Replace("%llegcolor%", GlobalVars.UserCustomization.LeftLegColorID.ToString())
                    .Replace("%rarmcolor%", GlobalVars.UserCustomization.RightArmColorID.ToString())
                    .Replace("%rlegcolor%", GlobalVars.UserCustomization.RightLegColorID.ToString())
                    .Replace("%md5launcher%", md5dir)
                    .Replace("%md5script%", info.ScriptMD5)
                    .Replace("%md5exe%", info.ClientMD5)
                    .Replace("%md5scriptd%", md5script)
                    .Replace("%md5exed%", md5exe)
                    .Replace("%md5s%", md5s)
                    .Replace("%md5sd%", md5sd)
                    .Replace("%limit%", GlobalVars.UserConfiguration.PlayerLimit.ToString())
                    .Replace("%extra%", GlobalVars.UserCustomization.Extra)
                    .Replace("%hat4%", GlobalVars.UserCustomization.Extra)
                    .Replace("%extrad%", GlobalPaths.extraGameDir + GlobalVars.UserCustomization.Extra)
                    .Replace("%hat4d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Extra)
                    .Replace("%mapfiled%", GlobalPaths.BaseGameDir + GlobalVars.UserConfiguration.MapPathSnip.Replace(@"\\", @"\").Replace(@"/", @"\"))
                    .Replace("%mapfilec%", extractedCode.Contains("%mapfilec%") ? NovetusFuncs.CopyMapToRBXAsset() : "")
                    .Replace("%tripcode%", GlobalVars.PlayerTripcode)
                    .Replace("%scripttype%", Generator.GetNameForType(type))
                    .Replace("%notifications%", GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower())
                    .Replace("%loadout%", code.Contains("<solo>") ? GlobalVars.soloLoadout : GlobalVars.Loadout)
                    .Replace("%doublequote%", "\"")
                    .Replace("%validatedextrafiles%", GlobalVars.ValidatedExtraFiles.ToString())
                    .Replace("%argstring%", GetRawArgsForType(type, ClientName, luafile))
                    .Replace("%tshirttexid%", GlobalVars.TShirtTextureID)
                    .Replace("%shirttexid%", GlobalVars.ShirtTextureID)
                    .Replace("%pantstexid%", GlobalVars.PantsTextureID)
                    .Replace("%facetexid%", GlobalVars.FaceTextureID)
                    .Replace("%tshirttexidlocal%", GlobalVars.TShirtTextureLocal)
                    .Replace("%shirttexidlocal%", GlobalVars.ShirtTextureLocal)
                    .Replace("%pantstexidlocal%", GlobalVars.PantsTextureLocal)
                    .Replace("%facetexlocal%", GlobalVars.FaceTextureLocal)
                    .Replace("%newgui%", GlobalVars.UserConfiguration.NewGUI.ToString().ToLower());

            if (compiled.Contains("%disabled%"))
            {
                MessageBox.Show("This option has been disabled for this client.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            return compiled;
        }
    }
    #endregion
}
#endregion
