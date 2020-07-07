#region Usings
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
#endregion

#region Launcher State
public enum LauncherState
{
    InLauncher = 0,
    InMPGame = 1,
    InSoloGame = 2,
    InStudio = 3,
    InCustomization = 4,
    InEasterEggGame = 5,
    LoadingURI = 6
}
#endregion

#region Launcher Functions
public class LauncherFuncs
{
    public LauncherFuncs()
	{
	}

    public static void ReadInfoFile(string infopath, bool cmd = false)
    {
        //READ
        string versionbranch, defaultclient, defaultmap, regclient1,
            regclient2, issnapshot, snapshottemplate, snapshotrevision;

        INIFile ini = new INIFile(infopath);

        string section = "ProgramInfo";

        //not using the GlobalVars definitions as those are empty until we fill them in.
        versionbranch = ini.IniReadValue(section, "Branch", "0.0");
        defaultclient = ini.IniReadValue(section, "DefaultClient", "2009E");
        defaultmap = ini.IniReadValue(section, "DefaultMap", "Dev - Baseplate2048.rbxl");
        regclient1 = ini.IniReadValue(section, "UserAgentRegisterClient1", "2007M");
        regclient2 = ini.IniReadValue(section, "UserAgentRegisterClient2", "2009L");
        issnapshot = ini.IniReadValue(section, "IsSnapshot", "False");
        snapshottemplate = ini.IniReadValue(section, "SnapshotTemplate", "%version% Snapshot (%build%.%revision%.%snapshot-revision%)");
        snapshotrevision = ini.IniReadValue(section, "SnapshotRevision", "1");

        try
        {
            GlobalVars.IsSnapshot = Convert.ToBoolean(issnapshot);
            if (GlobalVars.IsSnapshot == true)
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
            ini.IniWriteValue(section, "ItemMakerDisableHelpMessage", GlobalVars.UserConfiguration.DisabledItemMakerHelp.ToString());
            ini.IniWriteValue(section, "PlayerTripcode", SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.PlayerTripcode.ToString()));
            ini.IniWriteValue(section, "DiscordRichPresence", GlobalVars.UserConfiguration.DiscordPresence.ToString());
            ini.IniWriteValue(section, "MapPath", GlobalVars.UserConfiguration.MapPath.ToString());
            ini.IniWriteValue(section, "MapPathSnip", GlobalVars.UserConfiguration.MapPathSnip.ToString());
            ini.IniWriteValue(section, "GraphicsMode", Settings.GraphicsOptions.GetIntForMode(GlobalVars.UserConfiguration.GraphicsMode).ToString());
            ini.IniWriteValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
            ini.IniWriteValue(section, "QualityLevel", Settings.QualityOptions.GetIntForLevel(GlobalVars.UserConfiguration.QualityLevel).ToString());
            ini.IniWriteValue(section, "Layout", Settings.UIOptions.GetIntForStyle(GlobalVars.UserConfiguration.LauncherLayout).ToString());
            ini.IniWriteValue(section, "AssetLocalizerSaveBackups", GlobalVars.UserConfiguration.AssetLocalizerSaveBackups.ToString());
        }
        else
        {
            //READ
            string closeonlaunch, userid, name, selectedclient,
                map, port, limit, upnp,
                disablehelpmessage, tripcode, discord, mappath, mapsnip,
                graphics, reshade, qualitylevel, layout, savebackups;

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
            disablehelpmessage = ini.IniReadValue(section, "ItemMakerDisableHelpMessage", GlobalVars.UserConfiguration.DisabledItemMakerHelp.ToString());
            tripcode = ini.IniReadValue(section, "PlayerTripcode", GenerateAndReturnTripcode());
            discord = ini.IniReadValue(section, "DiscordRichPresence", GlobalVars.UserConfiguration.DiscordPresence.ToString());
            mappath = ini.IniReadValue(section, "MapPath", GlobalVars.UserConfiguration.MapPath.ToString());
            mapsnip = ini.IniReadValue(section, "MapPathSnip", GlobalVars.UserConfiguration.MapPathSnip.ToString());
            graphics = ini.IniReadValue(section, "GraphicsMode", Settings.GraphicsOptions.GetIntForMode(GlobalVars.UserConfiguration.GraphicsMode).ToString());
            reshade = ini.IniReadValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
            qualitylevel = ini.IniReadValue(section, "QualityLevel", Settings.QualityOptions.GetIntForLevel(GlobalVars.UserConfiguration.QualityLevel).ToString());
            layout = ini.IniReadValue(section, "Layout", Settings.UIOptions.GetIntForStyle(GlobalVars.UserConfiguration.LauncherLayout).ToString());
            savebackups = ini.IniReadValue(section, "AssetLocalizerSaveBackups", GlobalVars.UserConfiguration.AssetLocalizerSaveBackups.ToString());

            try
            {
                GlobalVars.UserConfiguration.CloseOnLaunch = Convert.ToBoolean(closeonlaunch);

                if (userid.Equals("0"))
                {
                    GeneratePlayerID();
                    Config(GlobalPaths.ConfigDir + "\\" + GlobalVars.ConfigName, true);
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
                    Config(GlobalPaths.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    GlobalVars.UserConfiguration.PlayerTripcode = SecurityFuncs.Base64Decode(tripcode);
                }

                GlobalVars.UserConfiguration.DiscordPresence = Convert.ToBoolean(discord);

                GlobalVars.UserConfiguration.MapPath = mappath;
                GlobalVars.UserConfiguration.MapPathSnip = mapsnip;

                GlobalVars.UserConfiguration.GraphicsMode = Settings.GraphicsOptions.GetModeForInt(Convert.ToInt32(graphics));
                GlobalVars.UserConfiguration.ReShade = Convert.ToBoolean(reshade);
                GlobalVars.UserConfiguration.QualityLevel = Settings.QualityOptions.GetLevelForInt(Convert.ToInt32(qualitylevel));
                GlobalVars.UserConfiguration.LauncherLayout = Settings.UIOptions.GetStyleForInt(Convert.ToInt32(layout));
                GlobalVars.UserConfiguration.AssetLocalizerSaveBackups = Convert.ToBoolean(savebackups);
            }
            catch (Exception)
            {
                Config(cfgpath, true);
            }
        }

        if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization))
        {
            Customization(GlobalPaths.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, true);
        }
        else
        {
            Customization(GlobalPaths.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, write);
        }

        ReShade(GlobalPaths.ConfigDir, "ReShade.ini", write);
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

            try
            {
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

                bool bline24 = Convert.ToBoolean(extraishat);
                GlobalVars.UserCustomization.ExtraSelectionIsHat = bline24;

                bool bline9 = Convert.ToBoolean(showhatsonextra);
                GlobalVars.UserCustomization.ShowHatsInExtra = bline9;
            }
            catch (Exception)
            {
                Customization(cfgpath, true);
            }
        }

        ReloadLoadtextValue();
    }

    public static void ReShadeValues(string cfgpath, bool write, bool setglobals)
    {
        if (write)
        {
            //WRITE
            INIFile ini = new INIFile(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.UserConfiguration.ReShadeFPSDisplay ? 1 : 0;
            ini.IniWriteValue(section, "ShowFPS", FPS.ToString());
            ini.IniWriteValue(section, "ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.UserConfiguration.ReShadePerformanceMode ? 1 : 0;
            ini.IniWriteValue(section, "PerformanceMode", PerformanceMode.ToString());
        }
        else
        {
            //READ
            string framerate, frametime, performance;

            INIFile ini = new INIFile(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.UserConfiguration.ReShadeFPSDisplay ? 1 : 0;
            framerate = ini.IniReadValue(section, "ShowFPS", FPS.ToString());
            frametime = ini.IniReadValue(section, "ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.UserConfiguration.ReShadePerformanceMode ? 1 : 0;
            performance = ini.IniReadValue(section, "PerformanceMode", PerformanceMode.ToString());

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

    public static void ReadClientValues(string clientpath)
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

        bool bline1 = Convert.ToBoolean(usesplayername);
        GlobalVars.SelectedClientInfo.UsesPlayerName = bline1;

        bool bline2 = Convert.ToBoolean(usesid);
        GlobalVars.SelectedClientInfo.UsesID = bline2;

        GlobalVars.SelectedClientInfo.Warning = warning;

        bool bline4 = Convert.ToBoolean(legacymode);
        GlobalVars.SelectedClientInfo.LegacyMode = bline4;

        GlobalVars.SelectedClientInfo.ClientMD5 = clientmd5;
        GlobalVars.SelectedClientInfo.ScriptMD5 = scriptmd5;
        GlobalVars.SelectedClientInfo.Description = desc;

        bool bline9 = Convert.ToBoolean(fix2007);
        GlobalVars.SelectedClientInfo.Fix2007 = bline9;

        bool bline10 = Convert.ToBoolean(alreadyhassecurity);
        GlobalVars.SelectedClientInfo.AlreadyHasSecurity = bline10;

        bool bline11 = Convert.ToBoolean(nographicsoptions);
        GlobalVars.SelectedClientInfo.NoGraphicsOptions = bline11;

        GlobalVars.SelectedClientInfo.CommandLineArgs = commandlineargs;
    }

    public static void ReShade(string cfgpath, string cfgname, bool write)
    {
        string fullpath = cfgpath + "\\" + cfgname;

        if (!File.Exists(fullpath))
        {
            File.Copy(GlobalPaths.ConfigDir + "\\ReShade_default.ini", fullpath, true);
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

            if (!File.Exists(fulldirpath))
            {
                File.Copy(fullpath, fulldirpath, true);
                ReShadeValues(fulldirpath, write, false);
            }
            else
            {
                ReShadeValues(fulldirpath, write, false);
            }

            string fulldllpath = dir.FullName + @"\opengl32.dll";

            if (GlobalVars.UserConfiguration.ReShade)
            {
                if (!File.Exists(fulldllpath))
                {
                    File.Copy(GlobalPaths.ConfigDirData + "\\opengl32.dll", fulldllpath, true);
                }
            }
            else
            {
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
        GlobalVars.UserConfiguration.QualityLevel = Settings.QualityOptions.Level.Ultra;
        GlobalVars.UserConfiguration.LauncherLayout = Settings.UIOptions.Style.Extended;
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
        ReloadLoadtextValue();
	}
		
	public static void ReloadLoadtextValue()
	{
		string hat1 = (!GlobalVars.UserCustomization.Hat1.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Hat1 : "NoHat.rbxm";
		string hat2 = (!GlobalVars.UserCustomization.Hat2.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Hat2 : "NoHat.rbxm";
		string hat3 = (!GlobalVars.UserCustomization.Hat3.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Hat3 : "NoHat.rbxm";
		string extra = (!GlobalVars.UserCustomization.Extra.EndsWith("-Solo.rbxm")) ? GlobalVars.UserCustomization.Extra : "NoExtra.rbxm";
			
		GlobalVars.loadtext = "'" + hat1 + "','" +
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
			
		GlobalVars.sololoadtext = "'" + GlobalVars.UserCustomization.Hat1 + "','" +
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

    public static Image LoadImage(string fileFullName)
    {
        Stream fileStream = File.OpenRead(fileFullName);
        Image image = Image.FromStream(fileStream);

        // PropertyItems seem to get lost when fileStream is closed to quickly (?); perhaps
        // this is the reason Microsoft didn't want to close it in the first place.
        PropertyItem[] items = image.PropertyItems;

        fileStream.Close();

        foreach (PropertyItem item in items)
        {
            image.SetPropertyItem(item);
        }

        return image;
    }

    public static void UpdateRichPresence(LauncherState state, string mapname, bool initial = false)
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
                case LauncherState.InLauncher:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_inlauncher;
                    GlobalVars.presence.state = "In Launcher";
                    GlobalVars.presence.details = "Selected " + GlobalVars.UserConfiguration.SelectedClient;
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In Launcher";
                    break;
                case LauncherState.InMPGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    break;
                case LauncherState.InSoloGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Solo Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Solo Game";
                    break;
                case LauncherState.InStudio:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_instudio;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Studio";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Studio";
                    break;
                case LauncherState.InCustomization:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_incustomization;
                    GlobalVars.presence.details = "Customizing " + GlobalVars.UserConfiguration.PlayerName;
                    GlobalVars.presence.state = "In Character Customization";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "In Character Customization";
                    break;
                case LauncherState.InEasterEggGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Reading a message.";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "Reading a message.";
                    break;
                case LauncherState.LoadingURI:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Joining a " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.ProgramInformation.Version;
                    GlobalVars.presence.smallImageText = "Joining a " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    break;
                default:
                    break;
            }

            IDiscordRPC.UpdatePresence(ref GlobalVars.presence);
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
            case Settings.QualityOptions.Level.VeryLow:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 1;
                MaterialQuality = 1;
                AASamples = 1;
                Bevels = 2;
                Shadows_2008 = 2;
                Shadows_2007 = false;
                break;
            case Settings.QualityOptions.Level.Low:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 5;
                MaterialQuality = 1;
                AASamples = 1;
                Bevels = 2;
                Shadows_2008 = 2;
                Shadows_2007 = false;
                break;
            case Settings.QualityOptions.Level.Medium:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 10;
                MaterialQuality = 2;
                AASamples = 4;
                Bevels = 2;
                Shadows_2007 = false;
                break;
            case Settings.QualityOptions.Level.High:
                MeshDetail = 75;
                ShadingQuality = 75;
                GFXQualityLevel = 15;
                AASamples = 4;
                break;
            case Settings.QualityOptions.Level.Ultra:
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
            luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
        }
        else
        {
            luafile = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
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
}
#endregion
