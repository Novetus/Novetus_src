#region Usings
using Nini.Config;
using NLog;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

#region Global Functions
public class GlobalFuncs
{
    public static void ReadInfoFile(string infopath, bool cmd = false)
    {
        //READ
        string versionbranch, defaultclient, defaultmap, regclient1,
            regclient2, issnapshot, snapshottemplate, snapshotrevision;

        IniConfigSource ini = new IniConfigSource(infopath);

        string section = "ProgramInfo";

        //not using the GlobalVars definitions as those are empty until we fill them in.
        versionbranch = ini.Configs[section].Get("Branch", "0.0");
        defaultclient = ini.Configs[section].Get("DefaultClient", "2009E");
        defaultmap = ini.Configs[section].Get("DefaultMap", "Dev - Baseplate2048.rbxl");
        regclient1 = ini.Configs[section].Get("UserAgentRegisterClient1", "2007M");
        regclient2 = ini.Configs[section].Get("UserAgentRegisterClient2", "2009L");
        issnapshot = ini.Configs[section].Get("IsSnapshot", "False");
        snapshottemplate = ini.Configs[section].Get("SnapshotTemplate", "%version% Snapshot (%build%.%revision%.%snapshot-revision%)");
        snapshotrevision = ini.Configs[section].Get("SnapshotRevision", "1");

        try
        {
            GlobalVars.IsSnapshot = Convert.ToBoolean(issnapshot);
            if (GlobalVars.IsSnapshot)
            {
                if (cmd)
                {
                    var versionInfo = FileVersionInfo.GetVersionInfo(GlobalPaths.RootPathLauncher + "\\Novetus.exe");
                    GlobalVars.ProgramInformation.Version = snapshottemplate.Replace("%version%", versionbranch)
                        .Replace("%build%", versionInfo.ProductBuildPart.ToString())
                        .Replace("%revision%", versionInfo.FilePrivatePart.ToString())
                        .Replace("%snapshot-revision%", snapshotrevision);
                }
                else
                {
                    GlobalVars.ProgramInformation.Version = snapshottemplate.Replace("%version%", versionbranch)
                        .Replace("%build%", Assembly.GetExecutingAssembly().GetName().Version.Build.ToString())
                        .Replace("%revision%", Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString())
                        .Replace("%snapshot-revision%", snapshotrevision);
                }

                string changelog = GlobalPaths.BasePath + "\\changelog.txt";
                if (File.Exists(changelog))
                {
                    string[] changelogedit = File.ReadAllLines(changelog);
                    if (!changelogedit[0].Equals(GlobalVars.ProgramInformation.Version))
                    {
                        changelogedit[0] = GlobalVars.ProgramInformation.Version;
                        File.WriteAllLines(changelog, changelogedit);
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
            GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
            GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
            GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
            GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
        }
        catch (Exception)
        {
            ReadInfoFile(infopath, cmd);
        }
    }

    public static void Config(string cfgpath, bool write)
    {
        if (write)
        {
            if (!File.Exists(cfgpath))
            {
                IniConfigSource createINI = new IniConfigSource();
                createINI.AddConfig("Config");
                createINI.Save(cfgpath);
                Config(cfgpath, true);
            }

            //WRITE
            IniConfigSource ini = new IniConfigSource(cfgpath);

            string section = "Config";

            ini.Configs[section].Set("CloseOnLaunch", GlobalVars.UserConfiguration.CloseOnLaunch.ToString());
            ini.Configs[section].Set("UserID", GlobalVars.UserConfiguration.UserID.ToString());
            ini.Configs[section].Set("PlayerName", GlobalVars.UserConfiguration.PlayerName.ToString());
            ini.Configs[section].Set("SelectedClient", GlobalVars.UserConfiguration.SelectedClient.ToString());
            ini.Configs[section].Set("Map", GlobalVars.UserConfiguration.Map.ToString());
            ini.Configs[section].Set("RobloxPort", GlobalVars.UserConfiguration.RobloxPort.ToString());
            ini.Configs[section].Set("PlayerLimit", GlobalVars.UserConfiguration.PlayerLimit.ToString());
            ini.Configs[section].Set("UPnP", GlobalVars.UserConfiguration.UPnP.ToString());
            ini.Configs[section].Set("ItemMakerDisableHelpMessage", GlobalVars.UserConfiguration.DisabledItemMakerHelp.ToString());
            ini.Configs[section].Set("PlayerTripcode", SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.PlayerTripcode.ToString()));
            ini.Configs[section].Set("DiscordRichPresence", GlobalVars.UserConfiguration.DiscordPresence.ToString());
            ini.Configs[section].Set("MapPath", GlobalVars.UserConfiguration.MapPath.ToString());
            ini.Configs[section].Set("MapPathSnip", GlobalVars.UserConfiguration.MapPathSnip.ToString());
            ini.Configs[section].Set("GraphicsMode", Settings.GraphicsOptions.GetIntForMode(GlobalVars.UserConfiguration.GraphicsMode).ToString());
            ini.Configs[section].Set("ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
            ini.Configs[section].Set("QualityLevel", Settings.GraphicsOptions.GetIntForLevel(GlobalVars.UserConfiguration.QualityLevel).ToString());
            ini.Configs[section].Set("Style", Settings.UIOptions.GetIntForStyle(GlobalVars.UserConfiguration.LauncherStyle).ToString());
            ini.Configs[section].Set("AssetLocalizerSaveBackups", GlobalVars.UserConfiguration.AssetLocalizerSaveBackups.ToString());

            ini.Save();
        }
        else
        {
            try
            {
                //READ
                string closeonlaunch, userid, name, selectedclient,
                map, port, limit, upnp,
                disablehelpmessage, tripcode, discord, mappath, mapsnip,
                graphics, reshade, qualitylevel, style, savebackups;

                IniConfigSource ini = new IniConfigSource(cfgpath);

                string section = "Config";

                closeonlaunch = ini.Configs[section].Get("CloseOnLaunch", GlobalVars.UserConfiguration.CloseOnLaunch.ToString());
                userid = ini.Configs[section].Get("UserID", GlobalVars.UserConfiguration.UserID.ToString());
                name = ini.Configs[section].Get("PlayerName", GlobalVars.UserConfiguration.PlayerName.ToString());
                selectedclient = ini.Configs[section].Get("SelectedClient", GlobalVars.UserConfiguration.SelectedClient.ToString());
                map = ini.Configs[section].Get("Map", GlobalVars.UserConfiguration.Map.ToString());
                port = ini.Configs[section].Get("RobloxPort", GlobalVars.UserConfiguration.RobloxPort.ToString());
                limit = ini.Configs[section].Get("PlayerLimit", GlobalVars.UserConfiguration.PlayerLimit.ToString());
                upnp = ini.Configs[section].Get("UPnP", GlobalVars.UserConfiguration.UPnP.ToString());
                disablehelpmessage = ini.Configs[section].Get("ItemMakerDisableHelpMessage", GlobalVars.UserConfiguration.DisabledItemMakerHelp.ToString());
                tripcode = ini.Configs[section].Get("PlayerTripcode", GenerateAndReturnTripcode());
                discord = ini.Configs[section].Get("DiscordRichPresence", GlobalVars.UserConfiguration.DiscordPresence.ToString());
                mappath = ini.Configs[section].Get("MapPath", GlobalVars.UserConfiguration.MapPath.ToString());
                mapsnip = ini.Configs[section].Get("MapPathSnip", GlobalVars.UserConfiguration.MapPathSnip.ToString());
                graphics = ini.Configs[section].Get("GraphicsMode", Settings.GraphicsOptions.GetIntForMode(GlobalVars.UserConfiguration.GraphicsMode).ToString());
                reshade = ini.Configs[section].Get("ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
                qualitylevel = ini.Configs[section].Get("QualityLevel", Settings.GraphicsOptions.GetIntForLevel(GlobalVars.UserConfiguration.QualityLevel).ToString());
                style = ini.Configs[section].Get("Style", Settings.UIOptions.GetIntForStyle(GlobalVars.UserConfiguration.LauncherStyle).ToString());
                savebackups = ini.Configs[section].Get("AssetLocalizerSaveBackups", GlobalVars.UserConfiguration.AssetLocalizerSaveBackups.ToString());
            
                GlobalVars.UserConfiguration.CloseOnLaunch = Convert.ToBoolean(closeonlaunch);

                if (userid.Equals("0"))
                {
                    GeneratePlayerID();
                    Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
                }
                else
                {
                    GlobalVars.UserConfiguration.UserID = Convert.ToInt32(userid);
                }

                GlobalVars.UserConfiguration.PlayerName = name;
                GlobalVars.UserConfiguration.SelectedClient = selectedclient;
                GlobalVars.UserConfiguration.Map = map;
                GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(port);
                GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(limit);
                GlobalVars.UserConfiguration.UPnP = Convert.ToBoolean(upnp);
                GlobalVars.UserConfiguration.DisabledItemMakerHelp = Convert.ToBoolean(disablehelpmessage);

                if (string.IsNullOrWhiteSpace(SecurityFuncs.Base64Decode(tripcode)))
                {
                    GenerateTripcode();
                    Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
                }
                else
                {
                    GlobalVars.UserConfiguration.PlayerTripcode = SecurityFuncs.Base64Decode(tripcode);
                }

                GlobalVars.UserConfiguration.DiscordPresence = Convert.ToBoolean(discord);
                GlobalVars.UserConfiguration.MapPathSnip = mapsnip;

                //update the map path if the file doesn't exist and write to config.
                if (File.Exists(mappath))
                {
                    GlobalVars.UserConfiguration.MapPath = mappath;
                }
                else
                {
                    GlobalVars.UserConfiguration.MapPath = GlobalPaths.BasePath + @"\\" + GlobalVars.UserConfiguration.MapPathSnip;
                    Config(cfgpath, true);
                }

                GlobalVars.UserConfiguration.GraphicsMode = Settings.GraphicsOptions.GetModeForInt(Convert.ToInt32(graphics));
                GlobalVars.UserConfiguration.ReShade = Convert.ToBoolean(reshade);
                GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.GetLevelForInt(Convert.ToInt32(qualitylevel));
                GlobalVars.UserConfiguration.LauncherStyle = Settings.UIOptions.GetStyleForInt(Convert.ToInt32(style));
                GlobalVars.UserConfiguration.AssetLocalizerSaveBackups = Convert.ToBoolean(savebackups);
            }
            catch (Exception)
            {
                Config(cfgpath, true);
            }
        }

        if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization))
        {
            Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, true);
        }
        else
        {
            Customization(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization, write);
        }

        ReShade(GlobalPaths.ConfigDir, "ReShade.ini", write);
    }

    public static void Customization(string cfgpath, bool write)
    {
        if (write)
        {
            if (!File.Exists(cfgpath))
            {
                IniConfigSource createINI = new IniConfigSource();
                createINI.AddConfig("Items");
                createINI.AddConfig("Colors");
                createINI.AddConfig("Other");
                createINI.Save(cfgpath);
                Customization(cfgpath, true);
            }

            //WRITE
            IniConfigSource ini = new IniConfigSource(cfgpath);

            string section = "Items";

            ini.Configs[section].Set("Hat1", GlobalVars.UserCustomization.Hat1.ToString());
            ini.Configs[section].Set("Hat2", GlobalVars.UserCustomization.Hat2.ToString());
            ini.Configs[section].Set("Hat3", GlobalVars.UserCustomization.Hat3.ToString());
            ini.Configs[section].Set("Face", GlobalVars.UserCustomization.Face.ToString());
            ini.Configs[section].Set("Head", GlobalVars.UserCustomization.Head.ToString());
            ini.Configs[section].Set("TShirt", GlobalVars.UserCustomization.TShirt.ToString());
            ini.Configs[section].Set("Shirt", GlobalVars.UserCustomization.Shirt.ToString());
            ini.Configs[section].Set("Pants", GlobalVars.UserCustomization.Pants.ToString());
            ini.Configs[section].Set("Icon", GlobalVars.UserCustomization.Icon.ToString());
            ini.Configs[section].Set("Extra", GlobalVars.UserCustomization.Extra.ToString());

            string section2 = "Colors";
            
            ini.Configs[section2].Set("HeadColorID", GlobalVars.UserCustomization.HeadColorID.ToString());
            ini.Configs[section2].Set("HeadColorString", GlobalVars.UserCustomization.HeadColorString.ToString());
            ini.Configs[section2].Set("TorsoColorID", GlobalVars.UserCustomization.TorsoColorID.ToString());
            ini.Configs[section2].Set("TorsoColorString", GlobalVars.UserCustomization.TorsoColorString.ToString());
            ini.Configs[section2].Set("LeftArmColorID", GlobalVars.UserCustomization.LeftArmColorID.ToString());
            ini.Configs[section2].Set("LeftArmColorString", GlobalVars.UserCustomization.LeftArmColorString.ToString());
            ini.Configs[section2].Set("RightArmColorID", GlobalVars.UserCustomization.RightArmColorID.ToString());
            ini.Configs[section2].Set("RightArmColorString", GlobalVars.UserCustomization.RightArmColorString.ToString());
            ini.Configs[section2].Set("LeftLegColorID", GlobalVars.UserCustomization.LeftLegColorID.ToString());
            ini.Configs[section2].Set("LeftLegColorString", GlobalVars.UserCustomization.LeftLegColorString.ToString());
            ini.Configs[section2].Set("RightLegColorID", GlobalVars.UserCustomization.RightLegColorID.ToString());
            ini.Configs[section2].Set("RightLegColorString", GlobalVars.UserCustomization.RightLegColorString.ToString());

            string section3 = "Other";

            ini.Configs[section3].Set("CharacterID", GlobalVars.UserCustomization.CharacterID.ToString());
            ini.Configs[section3].Set("ExtraSelectionIsHat", GlobalVars.UserCustomization.ExtraSelectionIsHat.ToString());
            ini.Configs[section3].Set("ShowHatsOnExtra", GlobalVars.UserCustomization.ShowHatsInExtra.ToString());

            ini.Save();
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

                IniConfigSource ini = new IniConfigSource(cfgpath);

                string section = "Items";
            
                hat1 = ini.Configs[section].Get("Hat1", GlobalVars.UserCustomization.Hat1.ToString());
                hat2 = ini.Configs[section].Get("Hat2", GlobalVars.UserCustomization.Hat2.ToString());
                hat3 = ini.Configs[section].Get("Hat3", GlobalVars.UserCustomization.Hat3.ToString());
                face = ini.Configs[section].Get("Face", GlobalVars.UserCustomization.Face.ToString());
                head = ini.Configs[section].Get("Head", GlobalVars.UserCustomization.Head.ToString());
                tshirt = ini.Configs[section].Get("TShirt", GlobalVars.UserCustomization.TShirt.ToString());
                shirt = ini.Configs[section].Get("Shirt", GlobalVars.UserCustomization.Shirt.ToString());
                pants = ini.Configs[section].Get("Pants", GlobalVars.UserCustomization.Pants.ToString());
                icon = ini.Configs[section].Get("Icon", GlobalVars.UserCustomization.Icon.ToString());
                extra = ini.Configs[section].Get("Extra", GlobalVars.UserCustomization.Extra.ToString());

                string section2 = "Colors";

                headcolorid = ini.Configs[section2].Get("HeadColorID", GlobalVars.UserCustomization.HeadColorID.ToString());
                headcolorstring = ini.Configs[section2].Get("HeadColorString", GlobalVars.UserCustomization.HeadColorString.ToString());
                torsocolorid = ini.Configs[section2].Get("TorsoColorID", GlobalVars.UserCustomization.TorsoColorID.ToString());
                torsocolorstring = ini.Configs[section2].Get("TorsoColorString", GlobalVars.UserCustomization.TorsoColorString.ToString());
                larmid = ini.Configs[section2].Get("LeftArmColorID", GlobalVars.UserCustomization.LeftArmColorID.ToString());
                larmstring = ini.Configs[section2].Get("LeftArmColorString", GlobalVars.UserCustomization.LeftArmColorString.ToString());
                rarmid = ini.Configs[section2].Get("RightArmColorID", GlobalVars.UserCustomization.RightArmColorID.ToString());
                rarmstring = ini.Configs[section2].Get("RightArmColorString", GlobalVars.UserCustomization.RightArmColorString.ToString());
                llegid = ini.Configs[section2].Get("LeftLegColorID", GlobalVars.UserCustomization.LeftLegColorID.ToString());
                llegstring = ini.Configs[section2].Get("LeftLegColorString", GlobalVars.UserCustomization.LeftLegColorString.ToString());
                rlegid = ini.Configs[section2].Get("RightLegColorID", GlobalVars.UserCustomization.RightLegColorID.ToString());
                rlegstring = ini.Configs[section2].Get("RightLegColorString", GlobalVars.UserCustomization.RightLegColorString.ToString());

                string section3 = "Other";

                characterid = ini.Configs[section3].Get("CharacterID", GlobalVars.UserCustomization.CharacterID.ToString());
                extraishat = ini.Configs[section3].Get("ExtraSelectionIsHat", GlobalVars.UserCustomization.ExtraSelectionIsHat.ToString());
                showhatsonextra = ini.Configs[section3].Get("ShowHatsOnExtra", GlobalVars.UserCustomization.ShowHatsInExtra.ToString());

                GlobalVars.UserCustomization.Hat1 = hat1;
                GlobalVars.UserCustomization.Hat2 = hat2;
                GlobalVars.UserCustomization.Hat3 = hat3;

                GlobalVars.UserCustomization.HeadColorID = Convert.ToInt32(headcolorid);
                GlobalVars.UserCustomization.TorsoColorID = Convert.ToInt32(torsocolorid);
                GlobalVars.UserCustomization.LeftArmColorID = Convert.ToInt32(larmid);
                GlobalVars.UserCustomization.RightArmColorID = Convert.ToInt32(rarmid);
                GlobalVars.UserCustomization.LeftLegColorID = Convert.ToInt32(llegid);
                GlobalVars.UserCustomization.RightLegColorID = Convert.ToInt32(rlegid);

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
                GlobalVars.UserCustomization.ExtraSelectionIsHat = Convert.ToBoolean(extraishat);
                GlobalVars.UserCustomization.ShowHatsInExtra = Convert.ToBoolean(showhatsonextra);
            }
            catch (Exception)
            {
                Customization(cfgpath, true);
            }
        }

        ReloadLoadoutValue();
    }

    public static void ReShadeValues(string cfgpath, bool write, bool setglobals)
    {
        if (write)
        {
            //WRITE
            IniConfigSource ini = new IniConfigSource(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.UserConfiguration.ReShadeFPSDisplay ? 1 : 0;
            ini.Configs[section].Set("ShowFPS", FPS.ToString());
            ini.Configs[section].Set("ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.UserConfiguration.ReShadePerformanceMode ? 1 : 0;
            ini.Configs[section].Set("PerformanceMode", PerformanceMode.ToString());

            ini.Save();
        }
        else
        {
            //READ
            string framerate, frametime, performance;

            IniConfigSource ini = new IniConfigSource(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.UserConfiguration.ReShadeFPSDisplay ? 1 : 0;
            framerate = ini.Configs[section].Get("ShowFPS", FPS.ToString());
            frametime = ini.Configs[section].Get("ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.UserConfiguration.ReShadePerformanceMode ? 1 : 0;
            performance = ini.Configs[section].Get("PerformanceMode", PerformanceMode.ToString());

            if (setglobals)
            {
                try
                {
                    switch(Convert.ToInt32(framerate))
                    {
                        case int showFPSLine when showFPSLine == 1 && Convert.ToInt32(frametime) == 1:
                            GlobalVars.UserConfiguration.ReShadeFPSDisplay = true;
                            break;
                        default:
                            GlobalVars.UserConfiguration.ReShadeFPSDisplay = false;
                            break;
                    }

                    switch (Convert.ToInt32(performance))
                    {
                        case 1:
                            GlobalVars.UserConfiguration.ReShadePerformanceMode = true;
                            break;
                        default:
                            GlobalVars.UserConfiguration.ReShadePerformanceMode = false;
                            break;
                    }
                }
                catch (Exception)
                {
                    ReShadeValues(cfgpath, true, setglobals);
                }
            }
        }
    }

#if LAUNCHER
    public static void ReadClientValues(RichTextBox box)
#else
    public static void ReadClientValues()
#endif
    {
#if LAUNCHER
        ReadClientValues(GlobalVars.UserConfiguration.SelectedClient, box);
#else
        ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
#endif
    }

#if LAUNCHER
    public static void ReadClientValues(string ClientName, RichTextBox box)
#else
    public static void ReadClientValues(string ClientName)
#endif
    {
        string name = ClientName;
        string clientpath = GlobalPaths.ClientDir + @"\\" + name + @"\\clientinfo.nov";

        if (!File.Exists(clientpath))
        {
#if LAUNCHER
            ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2, box);
#elif CMD
             GlobalFuncs.ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
#elif URI
#endif
            name = GlobalVars.ProgramInformation.DefaultClient;
#if LAUNCHER
            ReadClientValues(name, box);
#else
            ReadClientValues(name);
#endif
        }
        else
        {
            LoadClientValues(clientpath);
#if LAUNCHER
            ConsolePrint("Client '" + name + "' successfully loaded.", 3, box);
#elif CMD
            GlobalFuncs.ConsolePrint("Client '" + name + "' successfully loaded.", 3);
#elif URI
#endif
        }
    }

    public static void FixedFileCopy(string src, string dest, bool overwrite)
    {
        if (File.Exists(dest))
        {
            File.SetAttributes(dest, FileAttributes.Normal);
        }

        File.Copy(src, dest, overwrite);
    }

    public static string CopyMapToRBXAsset()
    {
        string clientcontentpath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\temp.rbxl";
        FixedFileCopy(GlobalVars.UserConfiguration.MapPath, clientcontentpath, true);
        return GlobalPaths.AltBaseGameDir + "temp.rbxl";
    }

    public static void LoadClientValues(string clientpath)
    {
        string file, usesplayername, usesid, warning, 
            legacymode, clientmd5, scriptmd5, 
            desc, fix2007, alreadyhassecurity, 
            nographicsoptions, commandlineargs;

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
        nographicsoptions = SecurityFuncs.Base64Decode(result[10]);
        try
        {
            commandlineargs = SecurityFuncs.Base64Decode(result[11]);
        }
        catch
        {
            //fake this option until we properly apply it.
            nographicsoptions = "False";
            commandlineargs = SecurityFuncs.Base64Decode(result[10]);
        }

        GlobalVars.SelectedClientInfo.UsesPlayerName = Convert.ToBoolean(usesplayername);
        GlobalVars.SelectedClientInfo.UsesID = Convert.ToBoolean(usesid);
        GlobalVars.SelectedClientInfo.Warning = warning;
        GlobalVars.SelectedClientInfo.LegacyMode = Convert.ToBoolean(legacymode);
        GlobalVars.SelectedClientInfo.ClientMD5 = clientmd5;
        GlobalVars.SelectedClientInfo.ScriptMD5 = scriptmd5;
        GlobalVars.SelectedClientInfo.Description = desc;
        GlobalVars.SelectedClientInfo.Fix2007 = Convert.ToBoolean(fix2007);
        GlobalVars.SelectedClientInfo.AlreadyHasSecurity = Convert.ToBoolean(alreadyhassecurity);
        GlobalVars.SelectedClientInfo.NoGraphicsOptions = Convert.ToBoolean(nographicsoptions);
        GlobalVars.SelectedClientInfo.CommandLineArgs = commandlineargs;
    }

    public static void ReShade(string cfgpath, string cfgname, bool write)
    {
        string fullpath = cfgpath + "\\" + cfgname;

        if (!File.Exists(fullpath))
        {
            File.Copy(GlobalPaths.ConfigDir + "\\ReShade_default.ini", fullpath);
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
            string fulldllpath = dir.FullName + @"\opengl32.dll";

            if (GlobalVars.UserConfiguration.ReShade)
            {
                if (!File.Exists(fulldirpath))
                {
                    File.Copy(fullpath, fulldirpath);
                    ReShadeValues(fulldirpath, write, false);
                }
                else
                {
                    ReShadeValues(fulldirpath, write, false);
                }

                if (!File.Exists(fulldllpath))
                {
                    File.Copy(GlobalPaths.ConfigDirData + "\\opengl32.dll", fulldllpath);
                }
            }
            else
            {
                if (File.Exists(fulldirpath))
                {
                    File.Delete(fulldirpath);
                }

                if (File.Exists(fulldllpath))
                {
                    File.Delete(fulldllpath);
                }
            }
        }
    }

    public static void ResetConfigValues()
	{
		GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
		GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
        GlobalVars.UserConfiguration.CloseOnLaunch = false;
        GeneratePlayerID();
        GlobalVars.UserConfiguration.PlayerName = "Player";
		GlobalVars.UserConfiguration.RobloxPort = 53640;
		GlobalVars.UserConfiguration.PlayerLimit = 12;
		GlobalVars.UserConfiguration.UPnP = false;
        GlobalVars.UserConfiguration.DisabledItemMakerHelp = false;
        GlobalVars.UserConfiguration.DiscordPresence = true;
        GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
        GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
        GlobalVars.UserConfiguration.GraphicsMode = Settings.GraphicsOptions.Mode.OpenGL;
        GlobalVars.UserConfiguration.ReShade = false;
        GlobalVars.UserConfiguration.QualityLevel = Settings.GraphicsOptions.Level.Ultra;
        GlobalVars.UserConfiguration.LauncherStyle = Settings.UIOptions.Style.Extended;
        ResetCustomizationValues();
	}
		
	public static void ResetCustomizationValues()
	{
		GlobalVars.UserCustomization.Hat1 = "NoHat.rbxm";
		GlobalVars.UserCustomization.Hat2 = "NoHat.rbxm";
		GlobalVars.UserCustomization.Hat3 = "NoHat.rbxm";
		GlobalVars.UserCustomization.Face = "DefaultFace.rbxm";
		GlobalVars.UserCustomization.Head = "DefaultHead.rbxm";
		GlobalVars.UserCustomization.TShirt = "NoTShirt.rbxm";
		GlobalVars.UserCustomization.Shirt = "NoShirt.rbxm";
		GlobalVars.UserCustomization.Pants = "NoPants.rbxm";
		GlobalVars.UserCustomization.Icon = "NBC";
		GlobalVars.UserCustomization.Extra = "NoExtra.rbxm";
		GlobalVars.UserCustomization.HeadColorID = 24;
		GlobalVars.UserCustomization.TorsoColorID = 23;
		GlobalVars.UserCustomization.LeftArmColorID = 24;
		GlobalVars.UserCustomization.RightArmColorID = 24;
		GlobalVars.UserCustomization.LeftLegColorID = 119;
		GlobalVars.UserCustomization.RightLegColorID = 119;
		GlobalVars.UserCustomization.CharacterID = "";
		GlobalVars.UserCustomization.HeadColorString = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.UserCustomization.TorsoColorString = "Color [A=255, R=13, G=105, B=172]";
		GlobalVars.UserCustomization.LeftArmColorString = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.UserCustomization.RightArmColorString = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.UserCustomization.LeftLegColorString = "Color [A=255, R=164, G=189, B=71]";
		GlobalVars.UserCustomization.RightLegColorString = "Color [A=255, R=164, G=189, B=71]";
		GlobalVars.UserCustomization.ExtraSelectionIsHat = false;
        GlobalVars.UserCustomization.ShowHatsInExtra = false;
        ReloadLoadoutValue();
	}
		
	public static void ReloadLoadoutValue()
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
    }
		
	public static void GeneratePlayerID()
	{
		CryptoRandom random = new CryptoRandom();
		int randomID = 0;
		int randIDmode = random.Next(0, 8);
        int idlimit = 0;

        switch (randIDmode)
        {
            case 0:
                idlimit = 9;
                break;
            case 1:
                idlimit = 99;
                break;
            case 2:
                idlimit = 999;
                break;
            case 3:
                idlimit = 9999;
                break;
            case 4:
                idlimit = 99999;
                break;
            case 5:
                idlimit = 999999;
                break;
            case 6:
                idlimit = 9999999;
                break;
            case 7:
                idlimit = 99999999;
                break;
            case 8:
            default:
                break;
        }

        if (idlimit > 0)
        {
            randomID = random.Next(0, idlimit);
        }
        else
        {
            randomID = random.Next();
        }

		//2147483647 is max id.
		GlobalVars.UserConfiguration.UserID = randomID;
	}

    public static void GenerateTripcode()
    {
        GlobalVars.UserConfiguration.PlayerTripcode = SecurityFuncs.RandomString();
    }

    public static string GenerateAndReturnTripcode()
    {
        GenerateTripcode();
        return GlobalVars.UserConfiguration.PlayerTripcode;
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

    public static void UpdateRichPresence(GlobalVars.LauncherState state, string mapname, bool initial = false)
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
                    GlobalVars.presence.details = "Selected " + GlobalVars.UserConfiguration.SelectedClient;
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In Launcher";
                    break;
                case GlobalVars.LauncherState.InMPGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    break;
                case GlobalVars.LauncherState.InSoloGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Solo Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Solo Game";
                    break;
                case GlobalVars.LauncherState.InStudio:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_instudio;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Studio";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Studio";
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
                    GlobalVars.presence.state = "Joining a " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "Joining a " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    break;
                default:
                    break;
            }

            DiscordRPC.UpdatePresence(ref GlobalVars.presence);
        }
    }

    public static string ChangeGameSettings()
    {
        string result = "";

        if (!GlobalVars.SelectedClientInfo.NoGraphicsOptions)
        {
            switch (GlobalVars.UserConfiguration.GraphicsMode)
            {
                case Settings.GraphicsOptions.Mode.OpenGL:
                    result += "xpcall( function() settings().Rendering.graphicsMode = 2 end, function( err ) settings().Rendering.graphicsMode = 4 end );";
                    break;
                case Settings.GraphicsOptions.Mode.DirectX:
                    result += "pcall(function() settings().Rendering.graphicsMode = 3 end);";
                    break;
                default:
                    break;
            }
        }

        //default values are ultra settings
        int MeshDetail = 100;
        int ShadingQuality = 100;
        int GFXQualityLevel = 19;
        int MaterialQuality = 3;
        int AASamples = 8;
        int Bevels = 1;
        int Shadows_2008 = 1;
        bool Shadows_2007 = true;

        switch (GlobalVars.UserConfiguration.QualityLevel)
        {
            case Settings.GraphicsOptions.Level.VeryLow:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 1;
                MaterialQuality = 1;
                AASamples = 1;
                Bevels = 2;
                Shadows_2008 = 2;
                Shadows_2007 = false;
                break;
            case Settings.GraphicsOptions.Level.Low:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 5;
                MaterialQuality = 1;
                AASamples = 1;
                Bevels = 2;
                Shadows_2008 = 2;
                Shadows_2007 = false;
                break;
            case Settings.GraphicsOptions.Level.Medium:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 10;
                MaterialQuality = 2;
                AASamples = 4;
                Bevels = 2;
                Shadows_2007 = false;
                break;
            case Settings.GraphicsOptions.Level.High:
                MeshDetail = 75;
                ShadingQuality = 75;
                GFXQualityLevel = 15;
                AASamples = 4;
                break;
            case Settings.GraphicsOptions.Level.Ultra:
            default:
                break;
        }

        result += " pcall(function() settings().Rendering.maxMeshDetail = " + MeshDetail.ToString() + " end);"
                + " pcall(function() settings().Rendering.maxShadingQuality = " + ShadingQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.minMeshDetail = " + MeshDetail.ToString() + " end);"
                + " pcall(function() settings().Rendering.minShadingQuality = " + ShadingQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.AluminumQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.CompoundMaterialQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.CorrodedMetalQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.DiamondPlateQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.GrassQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.IceQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.PlasticQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.SlateQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.TrussDetail = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.WoodQuality = " + MaterialQuality.ToString() + " end);"
                + " pcall(function() settings().Rendering.Antialiasing = 1 end);"
                + " pcall(function() settings().Rendering.AASamples = " + AASamples.ToString() + " end);"
                + " pcall(function() settings().Rendering.Bevels = " + Bevels.ToString() + " end);"
                + " pcall(function() settings().Rendering.Shadow = " + Shadows_2008.ToString() + " end);"
                + " pcall(function() settings().Rendering.Shadows = " + Shadows_2007.ToString().ToLower() + " end);"
                + " pcall(function() settings().Rendering.QualityLevel = " + GFXQualityLevel.ToString() + " end);";

        return result;
    }

    public static string GetLuaFileName()
    {
        string luafile = "";

        if (!GlobalVars.SelectedClientInfo.Fix2007)
        {
            luafile = "rbxasset://scripts\\\\" + GlobalPaths.ScriptName + ".lua";
        }
        else
        {
            luafile = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua";
        }

        return luafile;
    }

    public static string GetClientEXEDir(ScriptType type)
    {
        string rbxexe = "";
        if (GlobalVars.SelectedClientInfo.LegacyMode)
        {
            rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
        }
        else
        {
            switch (type)
            {
                case ScriptType.Client:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_client.exe";
                    break;
                case ScriptType.Server:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_server.exe";
                    break;
                case ScriptType.Studio:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_studio.exe";
                    break;
                case ScriptType.Solo:
                case ScriptType.EasterEgg:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_solo.exe";
                    break;
                case ScriptType.None:
                default:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
                    break;
            }
        }

        return rbxexe;
    }

#if URI
    public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e, Label label)
#elif LAUNCHER
    public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e, RichTextBox box)
#else
    public static void LaunchRBXClient(ScriptType type, bool no3d, bool nomap, EventHandler e)
#endif
    {
#if URI
        LaunchRBXClient(GlobalVars.UserConfiguration.SelectedClient, type, no3d, nomap, e, label);
#elif LAUNCHER
        LaunchRBXClient(GlobalVars.UserConfiguration.SelectedClient, type, no3d, nomap, e, box);
#else
        LaunchRBXClient(GlobalVars.UserConfiguration.SelectedClient, type, no3d, nomap, e);
#endif
    }

#if URI
    public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e, Label label)
#elif LAUNCHER
    public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e, RichTextBox box)
#else
    public static void LaunchRBXClient(string ClientName, ScriptType type, bool no3d, bool nomap, EventHandler e)
#endif
    {

#if LAUNCHER
        ReadClientValues(ClientName, box);
#else
        ReadClientValues(ClientName);
#endif

        string luafile = GetLuaFileName();
        string rbxexe = GetClientEXEDir(type);
        string mapfile = type.Equals(ScriptType.EasterEgg) ? GlobalPaths.ConfigDirData + "\\Appreciation.rbxl" : (nomap ? "" : GlobalVars.UserConfiguration.MapPath);
        string mapname = type.Equals(ScriptType.EasterEgg) ? "" : (nomap ? "" : GlobalVars.UserConfiguration.Map);
        string quote = "\"";
        string args = "";

        if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
        {
            if (!GlobalVars.SelectedClientInfo.Fix2007)
            {
                args = quote 
                    + mapfile 
                    + "\" -script \"" 
                    + ChangeGameSettings() 
                    + " dofile('" + luafile + "'); " 
                    + ScriptFuncs.Generator.GetScriptFuncForType(type) 
                    + "; " 
                    + (!string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? " dofile('" + GlobalPaths.AddonScriptPath + "');" : "") 
                    + quote 
                    + (no3d ? " -no3d" : "");
            }
            else
            {
                ScriptFuncs.Generator.GenerateScriptForClient(type);
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
            args = ScriptFuncs.ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, 
                ScriptFuncs.ClientScript.GetTagFromType(type, false, no3d), 
                ScriptFuncs.ClientScript.GetTagFromType(type, true, no3d),
                mapfile, 
                luafile, 
                rbxexe);
        }

        try
        {
#if LAUNCHER
            ConsolePrint("Client Loaded.", 4, box);
#elif CMD
            ConsolePrint("Client Loaded.", 4);
#elif URI
#endif

            if (type.Equals(ScriptType.Client))
            {
                if (!GlobalVars.AdminMode)
                {
                    if (GlobalVars.SelectedClientInfo.AlreadyHasSecurity != true)
                    {
                        if (SecurityFuncs.checkClientMD5(GlobalVars.UserConfiguration.SelectedClient) && SecurityFuncs.checkScriptMD5(GlobalVars.UserConfiguration.SelectedClient))
                        {
                            OpenClient(type, rbxexe, args, mapname, e);
                        }
                        else
                        {
#if URI
                            if (label != null)
                            {
                                label.Text = "The client has been detected as modified.";
                            }
#elif LAUNCHER
                            if (box != null)
                            {
                                ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified.)", 2, box);
                            }
#elif CMD
                            ConsolePrint("ERROR - Failed to launch Novetus. (The client has been detected as modified.)", 2);
#endif

#if URI || LAUNCHER
                            MessageBox.Show("Failed to launch Novetus. (Error: The client has been detected as modified.)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                        }
                    }
                    else
                    {
                        OpenClient(type, rbxexe, args, mapname, e);
                    }
                }
                else
                {
                    OpenClient(type, rbxexe, args, mapname, e);
                }
            }
            else
            {
                OpenClient(type, rbxexe, args, mapname, e);
            }
        }
#if URI || LAUNCHER || CMD
        catch (Exception ex)
#else
        catch (Exception)
#endif
        {
#if URI
            if (label != null)
            {
                label.Text = "Error: " + ex.Message;
            }
#elif LAUNCHER
            if (box != null)
            {
                ConsolePrint("ERROR - Failed to launch Novetus. (Error: " + ex.Message + ")", 2, box);
            }
#elif CMD
            ConsolePrint("ERROR - Failed to launch Novetus. (Error: " + ex.Message + ")", 2);
#endif

#if URI || LAUNCHER
            MessageBox.Show("Failed to launch Novetus. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
        }
    }

    private static void OpenClient(ScriptType type, string rbxexe, string args, string mapname, EventHandler e)
    {
        Process client = new Process();
        client.StartInfo.FileName = rbxexe;
        client.StartInfo.Arguments = args;
        if (e != null)
        {
            client.EnableRaisingEvents = true;
            client.Exited += e;
        }
        client.Start();
        client.PriorityClass = ProcessPriorityClass.RealTime;
        SecurityFuncs.RenameWindow(client, type, mapname);
        if (e != null)
        {
            UpdateRichPresence(GetStateForType(type), mapname);
        }
#if CMD
        GlobalVars.ProcessID = client.Id;
        CreateTXT();
#endif
    }

#if LAUNCHER
    public static void  ConsolePrint(string text, int type, RichTextBox box)
    {
        if (box == null)
            return;

        box.AppendText("[" + DateTime.Now.ToShortTimeString() + "] - ", Color.White);

        Logger log = LogManager.GetCurrentClassLogger();

        switch (type)
        {
            case 2:
                box.AppendText(text, Color.Red);
                log.Error(text);
                break;
            case 3:
                box.AppendText(text, Color.Lime);
                log.Info(text);
                break;
            case 4:
                box.AppendText(text, Color.Aqua);
                log.Info(text);
                break;
            case 5:
                box.AppendText(text, Color.Yellow);
                log.Warn(text);
                break;
            case 6:
                box.AppendText(text, Color.LightSalmon);
                log.Info(text);
                break;
            case 1:
            default:
                box.AppendText(text, Color.White);
                log.Info(text);
                break;
        }

        box.AppendText(Environment.NewLine);
    }
#elif CMD
    public static void ConsolePrint(string text, int type, bool notime = false)
    {
        if (!notime)
        {
            ConsoleText("[" + DateTime.Now.ToShortTimeString() + "] - ", ConsoleColor.White);
        }

        Logger log = LogManager.GetCurrentClassLogger();

        switch (type)
        {
            case 2:
                ConsoleText(text, ConsoleColor.Red);
                log.Error(text);
                break;
            case 3:
                ConsoleText(text, ConsoleColor.Green);
                log.Info(text);
                break;
            case 4:
                ConsoleText(text, ConsoleColor.Cyan);
                log.Info(text);
                break;
            case 5:
                ConsoleText(text, ConsoleColor.Yellow);
                log.Warn(text);
                break;
            case 1:
            default:
                ConsoleText(text, ConsoleColor.White);
                log.Info(text);
                break;
        }

        ConsoleText(Environment.NewLine, ConsoleColor.White);
    }

    public static void ConsoleText(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }

    public static async void CreateTXT()
    {
        if (GlobalVars.RequestToOutputInfo)
        {
            string IP = await SecurityFuncs.GetExternalIPAddressAsync();
            string[] lines1 = {
                        SecurityFuncs.Base64Encode(IP),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
            string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1, true));
            string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
            string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2, true));

            string text = GlobalFuncs.MultiLine(
                   "Process ID: " + (GlobalVars.ProcessID == 0 ? "N/A" : GlobalVars.ProcessID.ToString()),
                   "Don't copy the Process ID when sharing the server.",
                   "--------------------",
                   "Server Info:",
                   "Client: " + GlobalVars.UserConfiguration.SelectedClient,
                   "IP: " + IP,
                   "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                   "Map: " + GlobalVars.UserConfiguration.Map,
                   "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                   "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                   "Online URI Link:",
                   URI,
                   "Local URI Link:",
                   URI2,
                   GlobalVars.IsWebServerOn ? "Web Server URL:" : "",
                   GlobalVars.IsWebServerOn ? "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString() : "",
                   GlobalVars.IsWebServerOn ? "Local Web Server URL:" : "",
                   GlobalVars.IsWebServerOn ? GlobalVars.LocalWebServerURI : ""
               );

            File.WriteAllText(GlobalPaths.BasePath + "\\" + GlobalVars.ServerInfoFileName, GlobalFuncs.RemoveEmptyLines(text));
            GlobalFuncs.ConsolePrint("Server Information sent to file " + GlobalPaths.BasePath + "\\" + GlobalVars.ServerInfoFileName, 4);
        }
    }
#endif

    public static void CreateAssetCacheDirectories()
    {
        if (!Directory.Exists(GlobalPaths.AssetCacheDirFonts))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirFonts);
        }

        if (!Directory.Exists(GlobalPaths.AssetCacheDirSky))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirSky);
        }

        if (!Directory.Exists(GlobalPaths.AssetCacheDirSounds))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirSounds);
        }

        if (!Directory.Exists(GlobalPaths.AssetCacheDirTexturesGUI))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirTexturesGUI);
        }

        if (!Directory.Exists(GlobalPaths.AssetCacheDirScripts))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirScripts);
        }
    }

    public static string MultiLine(params string[] args)
    {
        return string.Join(Environment.NewLine, args);
    }

    public static string RemoveEmptyLines(string lines)
    {
        return Regex.Replace(lines, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
    }

    //task.delay is only available on net 4.5.......
    public static async void Delay(int miliseconds)
    {
        await TaskEx.Delay(miliseconds);
    }
}
#endregion
