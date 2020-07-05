#region Usings
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
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

    public static void ReadInfoFile(string infopath, bool cmd = false, bool versiononly = false)
    {
        string[] lines = File.ReadAllLines(infopath); //File is in System.IO
        GlobalVars.IsSnapshot = Convert.ToBoolean(lines[5]);
        if (GlobalVars.IsSnapshot == true)
        {
            if (cmd)
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(Directories.RootPathLauncher + "\\Novetus.exe");
                GlobalVars.Version = lines[6].Replace("%version%", lines[0])
                    .Replace("%build%", versionInfo.ProductBuildPart.ToString())
                    .Replace("%revision%", versionInfo.FilePrivatePart.ToString())
                    .Replace("%snapshot-revision%", lines[7]);
            }
            else
            {
                GlobalVars.Version = lines[6].Replace("%version%", lines[0])
                    .Replace("%build%", Assembly.GetExecutingAssembly().GetName().Version.Build.ToString())
                    .Replace("%revision%", Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString())
                    .Replace("%snapshot-revision%", lines[7]);
            }

            string changelog = Directories.BasePath + "\\changelog.txt";
            if (File.Exists(changelog))
            {
                string[] changelogedit = File.ReadAllLines(changelog);
                if (!changelogedit[0].Equals(GlobalVars.Version))
                {
                    changelogedit[0] = GlobalVars.Version;
                    File.WriteAllLines(changelog, changelogedit);
                }
            }
        }
        else
        {
            GlobalVars.Version = lines[0];
        }

        GlobalVars.Branch = lines[0];
        if (!versiononly)
        {
            GlobalVars.DefaultClient = lines[1];
            GlobalVars.DefaultMap = lines[2];
            GlobalVars.RegisterClient1 = lines[3];
            GlobalVars.RegisterClient2 = lines[4];
            GlobalVars.UserConfiguration.SelectedClient = GlobalVars.DefaultClient;
            GlobalVars.UserConfiguration.Map = GlobalVars.DefaultMap;
            GlobalVars.UserConfiguration.MapPath = Directories.MapsDir + @"\\" + GlobalVars.DefaultMap;
            GlobalVars.UserConfiguration.MapPathSnip = Directories.MapsDirBase + @"\\" + GlobalVars.DefaultMap;
        }
    }

    public static QualityLevel GetQualityLevelForInt(int level)
    {
        switch (level)
        {
            case 1:
                return QualityLevel.VeryLow;
            case 2:
                return QualityLevel.Low;
            case 3:
                return QualityLevel.Medium;
            case 4:
                return QualityLevel.High;
            case 5:
            default:
                return QualityLevel.Ultra;
        }
    }

    public static int GetIntForQualityLevel(QualityLevel level)
    {
        switch (level)
        {
            case QualityLevel.VeryLow:
                return 1;
            case QualityLevel.Low:
                return 2;
            case QualityLevel.Medium:
                return 3;
            case QualityLevel.High:
                return 4;
            case QualityLevel.Ultra:
            default:
                return 5;
        }
    }

    public static GraphicsMode GetGraphicsModeForInt(int level)
    {
        switch (level)
        {
            case 1:
                return GraphicsMode.OpenGL;
            case 2:
                return GraphicsMode.DirectX;
            default:
                return GraphicsMode.None;
        }
    }

    public static int GetIntForGraphicsMode(GraphicsMode level)
    {
        switch (level)
        {
            case GraphicsMode.OpenGL:
                return 1;
            case GraphicsMode.DirectX:
                return 2;
            default:
                return 0;
        }
    }

    public static void Config(string cfgpath, bool write)
    {
        if (write)
        {
            //WRITE
            IniFile ini = new IniFile(cfgpath);

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
            ini.IniWriteValue(section, "GraphicsMode", GetIntForGraphicsMode(GlobalVars.UserConfiguration.GraphicsMode).ToString());
            ini.IniWriteValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
            ini.IniWriteValue(section, "QualityLevel", GetIntForQualityLevel(GlobalVars.UserConfiguration.QualityLevel).ToString());
            ini.IniWriteValue(section, "OldLayout", GlobalVars.UserConfiguration.OldLayout.ToString());
        }
        else
        {
            //READ
            string closeonlaunch, userid, name, selectedclient,
                map, port, limit, upnp,
                disablehelpmessage, tripcode, discord, mappath, mapsnip,
                graphics, reshade, qualitylevel, oldlayout;

            IniFile ini = new IniFile(cfgpath);

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
            graphics = ini.IniReadValue(section, "GraphicsMode", GetIntForGraphicsMode(GlobalVars.UserConfiguration.GraphicsMode).ToString());
            reshade = ini.IniReadValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
            qualitylevel = ini.IniReadValue(section, "QualityLevel", GetIntForQualityLevel(GlobalVars.UserConfiguration.QualityLevel).ToString());
            oldlayout = ini.IniReadValue(section, "OldLayout", GlobalVars.UserConfiguration.OldLayout.ToString());

            try
            {
                bool bline1 = Convert.ToBoolean(closeonlaunch);
                GlobalVars.UserConfiguration.CloseOnLaunch = bline1;

                if (userid.Equals("0"))
                {
                    GeneratePlayerID();
                    Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    int iline2 = Convert.ToInt32(userid);
                    GlobalVars.UserConfiguration.UserID = iline2;
                }

                GlobalVars.UserConfiguration.PlayerName = name;

                GlobalVars.UserConfiguration.SelectedClient = selectedclient;

                GlobalVars.UserConfiguration.Map = map;

                int iline6 = Convert.ToInt32(port);
                GlobalVars.UserConfiguration.RobloxPort = iline6;

                int iline7 = Convert.ToInt32(limit);
                GlobalVars.UserConfiguration.PlayerLimit = iline7;

                bool bline10 = Convert.ToBoolean(upnp);
                GlobalVars.UserConfiguration.UPnP = bline10;

                bool bline11 = Convert.ToBoolean(disablehelpmessage);
                GlobalVars.UserConfiguration.DisabledItemMakerHelp = bline11;

                if (string.IsNullOrWhiteSpace(SecurityFuncs.Base64Decode(tripcode)))
                {
                    GenerateTripcode();
                    Config(Directories.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    string sdecrypt12 = SecurityFuncs.Base64Decode(tripcode);
                    GlobalVars.UserConfiguration.PlayerTripcode = sdecrypt12;
                }

                bool bline13 = Convert.ToBoolean(discord);
                GlobalVars.UserConfiguration.DiscordPresence = bline13;

                GlobalVars.UserConfiguration.MapPath = mappath;
                GlobalVars.UserConfiguration.MapPathSnip = mapsnip;
                int iline16 = Convert.ToInt32(graphics);
                GlobalVars.UserConfiguration.GraphicsMode = GetGraphicsModeForInt(iline16);
                bool bline17 = Convert.ToBoolean(reshade);
                GlobalVars.UserConfiguration.ReShade = bline17;
                int iline20 = Convert.ToInt32(qualitylevel);
                GlobalVars.UserConfiguration.QualityLevel = GetQualityLevelForInt(iline20);
                bool bline21 = Convert.ToBoolean(oldlayout);
                GlobalVars.UserConfiguration.OldLayout = bline21;
            }
            catch (Exception)
            {
                Config(cfgpath, true);
            }
        }

        if (!File.Exists(Directories.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization))
        {
            Customization(Directories.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, true);
        }
        else
        {
            Customization(Directories.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, write);
        }

        ReShade(Directories.ConfigDir, "ReShade.ini", write);
    }

    public static void Customization(string cfgpath, bool write)
    {
        if (write)
        {
            //WRITE
            IniFile ini = new IniFile(cfgpath);

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

            IniFile ini = new IniFile(cfgpath);

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

                int iline4 = Convert.ToInt32(headcolorid);
                GlobalVars.UserCustomization.HeadColorID = iline4;

                int iline5 = Convert.ToInt32(torsocolorid);
                GlobalVars.UserCustomization.TorsoColorID = iline5;

                int iline6 = Convert.ToInt32(larmid);
                GlobalVars.UserCustomization.LeftArmColorID = iline6;

                int iline7 = Convert.ToInt32(rarmid);
                GlobalVars.UserCustomization.RightArmColorID = iline7;

                int iline8 = Convert.ToInt32(llegid);
                GlobalVars.UserCustomization.LeftLegColorID = iline8;

                int iline9 = Convert.ToInt32(rlegid);
                GlobalVars.UserCustomization.RightLegColorID = iline9;

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
            IniFile ini = new IniFile(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.ReShadeFPSDisplay ? 1 : 0;
            ini.IniWriteValue(section, "ShowFPS", FPS.ToString());
            ini.IniWriteValue(section, "ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.ReShadePerformanceMode ? 1 : 0;
            ini.IniWriteValue(section, "PerformanceMode", PerformanceMode.ToString());
        }
        else
        {
            //READ
            string framerate, frametime, performance;

            IniFile ini = new IniFile(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.ReShadeFPSDisplay ? 1 : 0;
            framerate = ini.IniReadValue(section, "ShowFPS", FPS.ToString());
            frametime = ini.IniReadValue(section, "ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.ReShadePerformanceMode ? 1 : 0;
            performance = ini.IniReadValue(section, "PerformanceMode", PerformanceMode.ToString());

            if (setglobals)
            {
                try
                {
                    switch(Convert.ToInt32(framerate))
                    {
                        case int showFPSLine when showFPSLine == 1 && Convert.ToInt32(frametime) == 1:
                            GlobalVars.ReShadeFPSDisplay = true;
                            break;
                        default:
                            GlobalVars.ReShadeFPSDisplay = false;
                            break;
                    }

                    switch (Convert.ToInt32(performance))
                    {
                        case 1:
                            GlobalVars.ReShadePerformanceMode = true;
                            break;
                        default:
                            GlobalVars.ReShadePerformanceMode = false;
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
            File.Copy(Directories.ConfigDir + "\\ReShade_default.ini", fullpath, true);
            ReShadeValues(fullpath, write, true);
        }
        else
        {
            ReShadeValues(fullpath, write, true);
        }

        string clientdir = Directories.ClientDir;
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
                    File.Copy(Directories.ConfigDirData + "\\opengl32.dll", fulldllpath, true);
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
		GlobalVars.UserConfiguration.SelectedClient = GlobalVars.DefaultClient;
		GlobalVars.UserConfiguration.Map = GlobalVars.DefaultMap;
        GlobalVars.UserConfiguration.CloseOnLaunch = false;
        GeneratePlayerID();
        GlobalVars.UserConfiguration.PlayerName = "Player";
		GlobalVars.UserConfiguration.RobloxPort = 53640;
		GlobalVars.UserConfiguration.PlayerLimit = 12;
		GlobalVars.UserConfiguration.UPnP = false;
        GlobalVars.UserConfiguration.DisabledItemMakerHelp = false;
        GlobalVars.UserConfiguration.DiscordPresence = true;
        GlobalVars.UserConfiguration.MapPath = Directories.MapsDir + @"\\" + GlobalVars.DefaultMap;
        GlobalVars.UserConfiguration.MapPathSnip = Directories.MapsDirBase + @"\\" + GlobalVars.DefaultMap;
        GlobalVars.UserConfiguration.GraphicsMode = GraphicsMode.OpenGL;
        GlobalVars.UserConfiguration.ReShade = false;
        GlobalVars.UserConfiguration.QualityLevel = QualityLevel.Ultra;
        GlobalVars.UserConfiguration.OldLayout = false;
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
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In Launcher";
                    break;
                case LauncherState.InMPGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    break;
                case LauncherState.InSoloGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Solo Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Solo Game";
                    break;
                case LauncherState.InStudio:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_instudio;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.UserConfiguration.SelectedClient + " Studio";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.UserConfiguration.SelectedClient + " Studio";
                    break;
                case LauncherState.InCustomization:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_incustomization;
                    GlobalVars.presence.details = "Customizing " + GlobalVars.UserConfiguration.PlayerName;
                    GlobalVars.presence.state = "In Character Customization";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In Character Customization";
                    break;
                case LauncherState.InEasterEggGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Reading a message.";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "Reading a message.";
                    break;
                case LauncherState.LoadingURI:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Joining a " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.UserConfiguration.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "Joining a " + GlobalVars.UserConfiguration.SelectedClient + " Multiplayer Game";
                    break;
                default:
                    break;
            }

            DiscordRpc.UpdatePresence(ref GlobalVars.presence);
        }
    }

    public static string ChangeGameSettings()
    {
        string result = "";

        if (!GlobalVars.SelectedClientInfo.NoGraphicsOptions)
        {
            switch (GlobalVars.UserConfiguration.GraphicsMode)
            {
                case GraphicsMode.OpenGL:
                    result += "xpcall( function() settings().Rendering.graphicsMode = 2 end, function( err ) settings().Rendering.graphicsMode = 4 end );";
                    break;
                case GraphicsMode.DirectX:
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
            case QualityLevel.VeryLow:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 1;
                MaterialQuality = 1;
                AASamples = 1;
                Bevels = 2;
                Shadows_2008 = 2;
                Shadows_2007 = false;
                break;
            case QualityLevel.Low:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 5;
                MaterialQuality = 1;
                AASamples = 1;
                Bevels = 2;
                Shadows_2008 = 2;
                Shadows_2007 = false;
                break;
            case QualityLevel.Medium:
                MeshDetail = 50;
                ShadingQuality = 50;
                GFXQualityLevel = 10;
                MaterialQuality = 2;
                AASamples = 4;
                Bevels = 2;
                Shadows_2007 = false;
                break;
            case QualityLevel.High:
                MeshDetail = 75;
                ShadingQuality = 75;
                GFXQualityLevel = 15;
                AASamples = 4;
                break;
            case QualityLevel.Ultra:
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
            luafile = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
        }

        return luafile;
    }

    public static string GetClientEXEDir(ScriptType type)
    {
        string rbxexe = "";
        if (GlobalVars.SelectedClientInfo.LegacyMode)
        {
            rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
        }
        else
        {
            switch (type)
            {
                case ScriptType.Client:
                    rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_client.exe";
                    break;
                case ScriptType.Server:
                    rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_server.exe";
                    break;
                case ScriptType.Studio:
                    rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_studio.exe";
                    break;
                case ScriptType.Solo:
                case ScriptType.EasterEgg:
                    rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_solo.exe";
                    break;
                case ScriptType.None:
                default:
                    rbxexe = Directories.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
                    break;
            }
        }

        return rbxexe;
    }
}
#endregion

#region Splash Reader
public static class SplashReader
{
    private static string RandomSplash()
    {
        string[] splashes = File.ReadAllLines(Directories.ConfigDir + "\\splashes.txt");
        string splash = "";

        try
        {
            splash = splashes[new CryptoRandom().Next(0, splashes.Length - 1)];
        }
        catch (Exception)
        {
            try
            {
                splash = splashes[0];
            }
            catch (Exception)
            {
                splash = "missingno";
                return splash;
            }
        }

        CryptoRandom random = new CryptoRandom();

        string formattedsplash = splash
            .Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
            .Replace("%nextversion%", (Convert.ToDouble(GlobalVars.Branch) + 0.1).ToString())
            .Replace("%randomtext%", SecurityFuncs.RandomString(random.Next(2, 32)));

        return formattedsplash;
    }

    public static string GetSplash()
    {
        DateTime today = DateTime.Now;
        string splash = "";

        switch (today)
        {
            case DateTime christmaseve when christmaseve.Month.Equals(12) && christmaseve.Day.Equals(24):
            case DateTime christmasday when christmasday.Month.Equals(12) && christmasday.Day.Equals(25):
                splash = "Merry Christmas!";
                break;
            case DateTime newyearseve when newyearseve.Month.Equals(12) && newyearseve.Day.Equals(31):
            case DateTime newyearsday when newyearsday.Month.Equals(1) && newyearsday.Day.Equals(1):
                splash = "Happy New Year!";
                break;
            case DateTime halloween when halloween.Month.Equals(10) && halloween.Day.Equals(31):
                splash = "Happy Halloween!";
                break;
            case DateTime bitlbirthday when bitlbirthday.Month.Equals(6) && bitlbirthday.Day.Equals(10):
                splash = "Happy Birthday, Bitl!";
                break;
            case DateTime robloxbirthday when robloxbirthday.Month.Equals(8) && robloxbirthday.Day.Equals(27):
                splash = "Happy Birthday, ROBLOX!";
                break;
            case DateTime novetusbirthday when novetusbirthday.Month.Equals(10) && novetusbirthday.Day.Equals(27):
                splash = "Happy Birthday, Novetus!";
                break;
            case DateTime leiferikson when leiferikson.Month.Equals(10) && leiferikson.Day.Equals(9):
                splash = "Happy Leif Erikson Day! HINGA DINGA DURGEN!";
                break;
            case DateTime smokeweedeveryday when smokeweedeveryday.Month.Equals(4) && smokeweedeveryday.Day.Equals(20):
                CryptoRandom random = new CryptoRandom();
                if (random.Next(0, 1) == 1)
                {
                    splash = "smoke weed every day";
                }
                else
                {
                    splash = "4/20 lol";
                }
                break;
            case DateTime erikismyhero when erikismyhero.Month.Equals(2) && erikismyhero.Day.Equals(11):
                splash = "RIP Erik Cassel";
                break;
            default:
                splash = RandomSplash();
                break;
        }

        return splash;
    }
}
#endregion
