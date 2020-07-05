/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 10:24 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

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

public enum QualityLevel
{
    VeryLow = 1,
    Low = 2,
    Medium = 3,
    High = 4,
    Ultra = 5
}

public enum GraphicsMode
{
    None = 0,
    OpenGL = 1,
    DirectX = 2
}

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
                var versionInfo = FileVersionInfo.GetVersionInfo(GlobalVars.RootPathLauncher + "\\Novetus.exe");
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

            string changelog = GlobalVars.BasePath + "\\changelog.txt";
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
            GlobalVars.SelectedClient = GlobalVars.DefaultClient;
            GlobalVars.Map = GlobalVars.DefaultMap;
            GlobalVars.MapPath = GlobalVars.MapsDir + @"\\" + GlobalVars.DefaultMap;
            GlobalVars.MapPathSnip = GlobalVars.MapsDirBase + @"\\" + GlobalVars.DefaultMap;
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

            ini.IniWriteValue(section, "CloseOnLaunch", GlobalVars.CloseOnLaunch.ToString());
            ini.IniWriteValue(section, "UserID", GlobalVars.UserID.ToString());
            ini.IniWriteValue(section, "PlayerName", GlobalVars.PlayerName.ToString());
            ini.IniWriteValue(section, "SelectedClient", GlobalVars.SelectedClient.ToString());
            ini.IniWriteValue(section, "Map", GlobalVars.Map.ToString());
            ini.IniWriteValue(section, "RobloxPort", GlobalVars.RobloxPort.ToString());
            ini.IniWriteValue(section, "PlayerLimit", GlobalVars.PlayerLimit.ToString());
            ini.IniWriteValue(section, "UPnP", GlobalVars.UPnP.ToString());
            ini.IniWriteValue(section, "ItemMakerDisableHelpMessage", GlobalVars.DisabledHelp.ToString());
            ini.IniWriteValue(section, "PlayerTripcode", SecurityFuncs.Base64Encode(GlobalVars.PlayerTripcode.ToString()));
            ini.IniWriteValue(section, "DiscordRichPresence", GlobalVars.DiscordPresence.ToString());
            ini.IniWriteValue(section, "MapPath", GlobalVars.MapPath.ToString());
            ini.IniWriteValue(section, "MapPathSnip", GlobalVars.MapPathSnip.ToString());
            ini.IniWriteValue(section, "GraphicsMode", GetIntForGraphicsMode(GlobalVars.GraphicsMode).ToString());
            ini.IniWriteValue(section, "ReShade", GlobalVars.ReShade.ToString());
            ini.IniWriteValue(section, "QualityLevel", GetIntForQualityLevel(GlobalVars.QualityLevel).ToString());
            ini.IniWriteValue(section, "OldLayout", GlobalVars.OldLayout.ToString());
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

            closeonlaunch = ini.IniReadValue(section, "CloseOnLaunch", GlobalVars.CloseOnLaunch.ToString());
            userid = ini.IniReadValue(section, "UserID", GlobalVars.UserID.ToString());
            name = ini.IniReadValue(section, "PlayerName", GlobalVars.PlayerName.ToString());
            selectedclient = ini.IniReadValue(section, "SelectedClient", GlobalVars.SelectedClient.ToString());
            map = ini.IniReadValue(section, "Map", GlobalVars.Map.ToString());
            port = ini.IniReadValue(section, "RobloxPort", GlobalVars.RobloxPort.ToString());
            limit = ini.IniReadValue(section, "PlayerLimit", GlobalVars.PlayerLimit.ToString());
            upnp = ini.IniReadValue(section, "UPnP", GlobalVars.UPnP.ToString());
            disablehelpmessage = ini.IniReadValue(section, "ItemMakerDisableHelpMessage", GlobalVars.DisabledHelp.ToString());
            tripcode = ini.IniReadValue(section, "PlayerTripcode", GenerateAndReturnTripcode());
            discord = ini.IniReadValue(section, "DiscordRichPresence", GlobalVars.DiscordPresence.ToString());
            mappath = ini.IniReadValue(section, "MapPath", GlobalVars.MapPath.ToString());
            mapsnip = ini.IniReadValue(section, "MapPathSnip", GlobalVars.MapPathSnip.ToString());
            graphics = ini.IniReadValue(section, "GraphicsMode", GetIntForGraphicsMode(GlobalVars.GraphicsMode).ToString());
            reshade = ini.IniReadValue(section, "ReShade", GlobalVars.ReShade.ToString());
            qualitylevel = ini.IniReadValue(section, "QualityLevel", GetIntForQualityLevel(GlobalVars.QualityLevel).ToString());
            oldlayout = ini.IniReadValue(section, "OldLayout", GlobalVars.OldLayout.ToString());

            try
            {
                bool bline1 = Convert.ToBoolean(closeonlaunch);
                GlobalVars.CloseOnLaunch = bline1;

                if (userid.Equals("0"))
                {
                    GeneratePlayerID();
                    Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    int iline2 = Convert.ToInt32(userid);
                    GlobalVars.UserID = iline2;
                }

                GlobalVars.PlayerName = name;

                GlobalVars.SelectedClient = selectedclient;

                GlobalVars.Map = map;

                int iline6 = Convert.ToInt32(port);
                GlobalVars.RobloxPort = iline6;

                int iline7 = Convert.ToInt32(limit);
                GlobalVars.PlayerLimit = iline7;

                bool bline10 = Convert.ToBoolean(upnp);
                GlobalVars.UPnP = bline10;

                bool bline11 = Convert.ToBoolean(disablehelpmessage);
                GlobalVars.DisabledHelp = bline11;

                if (string.IsNullOrWhiteSpace(SecurityFuncs.Base64Decode(tripcode)))
                {
                    GenerateTripcode();
                    Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    string sdecrypt12 = SecurityFuncs.Base64Decode(tripcode);
                    GlobalVars.PlayerTripcode = sdecrypt12;
                }

                bool bline13 = Convert.ToBoolean(discord);
                GlobalVars.DiscordPresence = bline13;

                GlobalVars.MapPath = mappath;
                GlobalVars.MapPathSnip = mapsnip;
                int iline16 = Convert.ToInt32(graphics);
                GlobalVars.GraphicsMode = GetGraphicsModeForInt(iline16);
                bool bline17 = Convert.ToBoolean(reshade);
                GlobalVars.ReShade = bline17;
                int iline20 = Convert.ToInt32(qualitylevel);
                GlobalVars.QualityLevel = GetQualityLevelForInt(iline20);
                bool bline21 = Convert.ToBoolean(oldlayout);
                GlobalVars.OldLayout = bline21;
            }
            catch (Exception)
            {
                Config(cfgpath, true);
            }
        }

        if (!File.Exists(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization))
        {
            Customization(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, true);
        }
        else
        {
            Customization(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigNameCustomization, write);
        }

        ReShade(GlobalVars.ConfigDir, "ReShade.ini", write);
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
            File.Copy(GlobalVars.ConfigDir + "\\ReShade_default.ini", fullpath, true);
            ReShadeValues(fullpath, write, true);
        }
        else
        {
            ReShadeValues(fullpath, write, true);
        }

        string clientdir = GlobalVars.ClientDir;
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

            if (GlobalVars.ReShade)
            {
                if (!File.Exists(fulldllpath))
                {
                    File.Copy(GlobalVars.ConfigDirData + "\\opengl32.dll", fulldllpath, true);
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
		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
		GlobalVars.Map = GlobalVars.DefaultMap;
        GlobalVars.CloseOnLaunch = false;
        GeneratePlayerID();
        GlobalVars.PlayerName = "Player";
		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
		GlobalVars.RobloxPort = 53640;
		GlobalVars.PlayerLimit = 12;
		GlobalVars.UPnP = false;
        //GlobalVars.UDP = true;
        GlobalVars.DisabledHelp = false;
        GlobalVars.DiscordPresence = true;
        GlobalVars.MapPath = GlobalVars.MapsDir + @"\\" + GlobalVars.DefaultMap;
        GlobalVars.MapPathSnip = GlobalVars.MapsDirBase + @"\\" + GlobalVars.DefaultMap;
        GlobalVars.GraphicsMode = GraphicsMode.OpenGL;
        GlobalVars.ReShade = false;
        GlobalVars.QualityLevel = QualityLevel.Ultra;
        GlobalVars.OldLayout = false;
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
		GlobalVars.UserID = randomID;
	}

    public static void GenerateTripcode()
    {
        GlobalVars.PlayerTripcode = SecurityFuncs.RandomString();
    }

    public static string GenerateAndReturnTripcode()
    {
        GenerateTripcode();
        return GlobalVars.PlayerTripcode;
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
        if (GlobalVars.DiscordPresence)
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
                    GlobalVars.presence.details = "Selected " + GlobalVars.SelectedClient;
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In Launcher";
                    break;
                case LauncherState.InMPGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.SelectedClient + " Multiplayer Game";
                    break;
                case LauncherState.InSoloGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Solo Game";
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.SelectedClient + " Solo Game";
                    break;
                case LauncherState.InStudio:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_instudio;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "In " + GlobalVars.SelectedClient + " Studio";
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In " + GlobalVars.SelectedClient + " Studio";
                    break;
                case LauncherState.InCustomization:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_incustomization;
                    GlobalVars.presence.details = "Customizing " + GlobalVars.PlayerName;
                    GlobalVars.presence.state = "In Character Customization";
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "In Character Customization";
                    break;
                case LauncherState.InEasterEggGame:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Reading a message.";
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "Reading a message.";
                    break;
                case LauncherState.LoadingURI:
                    GlobalVars.presence.smallImageKey = GlobalVars.image_ingame;
                    GlobalVars.presence.details = ValidMapname;
                    GlobalVars.presence.state = "Joining a " + GlobalVars.SelectedClient + " Multiplayer Game";
                    GlobalVars.presence.largeImageText = GlobalVars.PlayerName + " | Novetus " + GlobalVars.Version;
                    GlobalVars.presence.smallImageText = "Joining a " + GlobalVars.SelectedClient + " Multiplayer Game";
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
            switch (GlobalVars.GraphicsMode)
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

        switch (GlobalVars.QualityLevel)
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
            luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
        }

        return luafile;
    }

    public static string GetClientEXEDir(ScriptType type)
    {
        string rbxexe = "";
        if (GlobalVars.SelectedClientInfo.LegacyMode)
        {
            rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
        }
        else
        {
            switch (type)
            {
                case ScriptType.Client:
                    rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_client.exe";
                    break;
                case ScriptType.Server:
                    rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_server.exe";
                    break;
                case ScriptType.Studio:
                    rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_studio.exe";
                    break;
                case ScriptType.Solo:
                case ScriptType.EasterEgg:
                    rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_solo.exe";
                    break;
                case ScriptType.None:
                default:
                    rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
                    break;
            }
        }

        return rbxexe;
    }
}
