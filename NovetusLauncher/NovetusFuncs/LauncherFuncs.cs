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
            ini.IniWriteValue(section, "ShowHatsOnExtra", GlobalVars.Custom_Extra_ShowHats.ToString());
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
            string Decryptline1, Decryptline2, Decryptline3, Decryptline4,
                Decryptline5, Decryptline6, Decryptline7, Decryptline9, Decryptline10,
                Decryptline11, Decryptline12, Decryptline13, Decryptline14, Decryptline15,
                Decryptline16, Decryptline17, Decryptline20, Decryptline21;//, Decryptline22;

            IniFile ini = new IniFile(cfgpath);

            string section = "Config";

            Decryptline1 = ini.IniReadValue(section, "CloseOnLaunch", GlobalVars.CloseOnLaunch.ToString());
            Decryptline2 = ini.IniReadValue(section, "UserID", GlobalVars.UserID.ToString());
            Decryptline3 = ini.IniReadValue(section, "PlayerName", GlobalVars.PlayerName.ToString());
            Decryptline4 = ini.IniReadValue(section, "SelectedClient", GlobalVars.SelectedClient.ToString());
            Decryptline5 = ini.IniReadValue(section, "Map", GlobalVars.Map.ToString());
            Decryptline6 = ini.IniReadValue(section, "RobloxPort", GlobalVars.RobloxPort.ToString());
            Decryptline7 = ini.IniReadValue(section, "PlayerLimit", GlobalVars.PlayerLimit.ToString());
            Decryptline9 = ini.IniReadValue(section, "ShowHatsOnExtra", GlobalVars.Custom_Extra_ShowHats.ToString());
            Decryptline10 = ini.IniReadValue(section, "UPnP", GlobalVars.UPnP.ToString());
            Decryptline11 = ini.IniReadValue(section, "ItemMakerDisableHelpMessage", GlobalVars.DisabledHelp.ToString());
            Decryptline12 = ini.IniReadValue(section, "PlayerTripcode", GenerateAndReturnTripcode());
            Decryptline13 = ini.IniReadValue(section, "DiscordRichPresence", GlobalVars.DiscordPresence.ToString());
            Decryptline14 = ini.IniReadValue(section, "MapPath", GlobalVars.MapPath.ToString());
            Decryptline15 = ini.IniReadValue(section, "MapPathSnip", GlobalVars.MapPathSnip.ToString());
            Decryptline16 = ini.IniReadValue(section, "GraphicsMode", GetIntForGraphicsMode(GlobalVars.GraphicsMode).ToString());
            Decryptline17 = ini.IniReadValue(section, "ReShade", GlobalVars.ReShade.ToString());
            Decryptline20 = ini.IniReadValue(section, "QualityLevel", GetIntForQualityLevel(GlobalVars.QualityLevel).ToString());
            Decryptline21 = ini.IniReadValue(section, "OldLayout", GlobalVars.OldLayout.ToString());

            try
            {
                bool bline1 = Convert.ToBoolean(Decryptline1);
                GlobalVars.CloseOnLaunch = bline1;

                if (Decryptline2.Equals("0"))
                {
                    GeneratePlayerID();
                    Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    int iline2 = Convert.ToInt32(Decryptline2);
                    GlobalVars.UserID = iline2;
                }

                GlobalVars.PlayerName = Decryptline3;

                GlobalVars.SelectedClient = Decryptline4;

                GlobalVars.Map = Decryptline5;

                int iline6 = Convert.ToInt32(Decryptline6);
                GlobalVars.RobloxPort = iline6;

                int iline7 = Convert.ToInt32(Decryptline7);
                GlobalVars.PlayerLimit = iline7;

                bool bline9 = Convert.ToBoolean(Decryptline9);
                GlobalVars.Custom_Extra_ShowHats = bline9;

                bool bline10 = Convert.ToBoolean(Decryptline10);
                GlobalVars.UPnP = bline10;

                bool bline11 = Convert.ToBoolean(Decryptline11);
                GlobalVars.DisabledHelp = bline11;

                if (string.IsNullOrWhiteSpace(SecurityFuncs.Base64Decode(Decryptline12)))
                {
                    GenerateTripcode();
                    Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, true);
                }
                else
                {
                    string sdecrypt12 = SecurityFuncs.Base64Decode(Decryptline12);
                    GlobalVars.PlayerTripcode = sdecrypt12;
                }

                bool bline13 = Convert.ToBoolean(Decryptline13);
                GlobalVars.DiscordPresence = bline13;

                GlobalVars.MapPath = Decryptline14;
                GlobalVars.MapPathSnip = Decryptline15;
                int iline16 = Convert.ToInt32(Decryptline16);
                GlobalVars.GraphicsMode = GetGraphicsModeForInt(iline16);
                bool bline17 = Convert.ToBoolean(Decryptline17);
                GlobalVars.ReShade = bline17;
                int iline20 = Convert.ToInt32(Decryptline20);
                GlobalVars.QualityLevel = GetQualityLevelForInt(iline20);
                bool bline21 = Convert.ToBoolean(Decryptline21);
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

            ini.IniWriteValue(section, "Hat1", GlobalVars.Custom_Hat1ID_Offline.ToString());
            ini.IniWriteValue(section, "Hat2", GlobalVars.Custom_Hat2ID_Offline.ToString());
            ini.IniWriteValue(section, "Hat3", GlobalVars.Custom_Hat3ID_Offline.ToString());
            ini.IniWriteValue(section, "Face", GlobalVars.Custom_Face_Offline.ToString());
            ini.IniWriteValue(section, "Head", GlobalVars.Custom_Head_Offline.ToString());
            ini.IniWriteValue(section, "TShirt", GlobalVars.Custom_T_Shirt_Offline.ToString());
            ini.IniWriteValue(section, "Shirt", GlobalVars.Custom_Shirt_Offline.ToString());
            ini.IniWriteValue(section, "Pants", GlobalVars.Custom_Pants_Offline.ToString());
            ini.IniWriteValue(section, "Icon", GlobalVars.Custom_Icon_Offline.ToString());
            ini.IniWriteValue(section, "Extra", GlobalVars.Custom_Extra.ToString());

            string section2 = "Colors";

            ini.IniWriteValue(section2, "HeadColorID", GlobalVars.HeadColorID.ToString());
            ini.IniWriteValue(section2, "HeadColorString", GlobalVars.ColorMenu_HeadColor.ToString());
            ini.IniWriteValue(section2, "TorsoColorID", GlobalVars.TorsoColorID.ToString());
            ini.IniWriteValue(section2, "TorsoColorString", GlobalVars.ColorMenu_TorsoColor.ToString());
            ini.IniWriteValue(section2, "LeftArmColorID", GlobalVars.LeftArmColorID.ToString());
            ini.IniWriteValue(section2, "LeftArmColorString", GlobalVars.ColorMenu_LeftArmColor.ToString());
            ini.IniWriteValue(section2, "RightArmColorID", GlobalVars.RightArmColorID.ToString());
            ini.IniWriteValue(section2, "RightArmColorString", GlobalVars.ColorMenu_RightArmColor.ToString());
            ini.IniWriteValue(section2, "LeftLegColorID", GlobalVars.LeftLegColorID.ToString());
            ini.IniWriteValue(section2, "LeftLegColorString", GlobalVars.ColorMenu_LeftLegColor.ToString());
            ini.IniWriteValue(section2, "RightLegColorID", GlobalVars.RightLegColorID.ToString());
            ini.IniWriteValue(section2, "RightLegColorString", GlobalVars.ColorMenu_RightLegColor.ToString());

            string section3 = "Other";

            ini.IniWriteValue(section3, "CharacterID", GlobalVars.CharacterID.ToString());
            ini.IniWriteValue(section3, "ExtraSelectionIsHat", GlobalVars.Custom_Extra_SelectionIsHat.ToString());
        }
        else
        {
            //READ

            string Decryptline1, Decryptline2, Decryptline3, Decryptline4, 
                Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, 
                Decryptline10, Decryptline11, Decryptline12, Decryptline13, Decryptline14, 
                Decryptline15, Decryptline16, Decryptline17, Decryptline18, Decryptline19, 
                Decryptline20, Decryptline21, Decryptline22, Decryptline23, Decryptline24;

            IniFile ini = new IniFile(cfgpath);

            string section = "Items";

            Decryptline1 = ini.IniReadValue(section, "Hat1", GlobalVars.Custom_Hat1ID_Offline.ToString());
            Decryptline2 = ini.IniReadValue(section, "Hat2", GlobalVars.Custom_Hat2ID_Offline.ToString());
            Decryptline3 = ini.IniReadValue(section, "Hat3", GlobalVars.Custom_Hat3ID_Offline.ToString());
            Decryptline16 = ini.IniReadValue(section, "Face", GlobalVars.Custom_Face_Offline.ToString());
            Decryptline17 = ini.IniReadValue(section, "Head", GlobalVars.Custom_Head_Offline.ToString());
            Decryptline18 = ini.IniReadValue(section, "TShirt", GlobalVars.Custom_T_Shirt_Offline.ToString());
            Decryptline19 = ini.IniReadValue(section, "Shirt", GlobalVars.Custom_Shirt_Offline.ToString());
            Decryptline20 = ini.IniReadValue(section, "Pants", GlobalVars.Custom_Pants_Offline.ToString());
            Decryptline21 = ini.IniReadValue(section, "Icon", GlobalVars.Custom_Icon_Offline.ToString());
            Decryptline23 = ini.IniReadValue(section, "Extra", GlobalVars.Custom_Extra.ToString());

            string section2 = "Colors";

            Decryptline4 = ini.IniReadValue(section2, "HeadColorID", GlobalVars.HeadColorID.ToString());
            Decryptline10 = ini.IniReadValue(section2, "HeadColorString", GlobalVars.ColorMenu_HeadColor.ToString());
            Decryptline5 = ini.IniReadValue(section2, "TorsoColorID", GlobalVars.TorsoColorID.ToString());
            Decryptline11 = ini.IniReadValue(section2, "TorsoColorString", GlobalVars.ColorMenu_TorsoColor.ToString());
            Decryptline6 = ini.IniReadValue(section2, "LeftArmColorID", GlobalVars.LeftArmColorID.ToString());
            Decryptline12 = ini.IniReadValue(section2, "LeftArmColorString", GlobalVars.ColorMenu_LeftArmColor.ToString());
            Decryptline7 = ini.IniReadValue(section2, "RightArmColorID", GlobalVars.RightArmColorID.ToString());
            Decryptline13 = ini.IniReadValue(section2, "RightArmColorString", GlobalVars.ColorMenu_RightArmColor.ToString());
            Decryptline8 = ini.IniReadValue(section2, "LeftLegColorID", GlobalVars.LeftLegColorID.ToString());
            Decryptline14 = ini.IniReadValue(section2, "LeftLegColorString", GlobalVars.ColorMenu_LeftLegColor.ToString());
            Decryptline9 = ini.IniReadValue(section2, "RightLegColorID", GlobalVars.RightLegColorID.ToString());
            Decryptline15 = ini.IniReadValue(section2, "RightLegColorString", GlobalVars.ColorMenu_RightLegColor.ToString());

            string section3 = "Other";

            Decryptline22 = ini.IniReadValue(section3, "CharacterID", GlobalVars.CharacterID.ToString());
            Decryptline24 = ini.IniReadValue(section3, "ExtraSelectionIsHat", GlobalVars.Custom_Extra_SelectionIsHat.ToString());

            try
            {
                GlobalVars.Custom_Hat1ID_Offline = Decryptline1;
                GlobalVars.Custom_Hat2ID_Offline = Decryptline2;
                GlobalVars.Custom_Hat3ID_Offline = Decryptline3;

                int iline4 = Convert.ToInt32(Decryptline4);
                GlobalVars.HeadColorID = iline4;

                int iline5 = Convert.ToInt32(Decryptline5);
                GlobalVars.TorsoColorID = iline5;

                int iline6 = Convert.ToInt32(Decryptline6);
                GlobalVars.LeftArmColorID = iline6;

                int iline7 = Convert.ToInt32(Decryptline7);
                GlobalVars.RightArmColorID = iline7;

                int iline8 = Convert.ToInt32(Decryptline8);
                GlobalVars.LeftLegColorID = iline8;

                int iline9 = Convert.ToInt32(Decryptline9);
                GlobalVars.RightLegColorID = iline9;

                GlobalVars.ColorMenu_HeadColor = Decryptline10;
                GlobalVars.ColorMenu_TorsoColor = Decryptline11;
                GlobalVars.ColorMenu_LeftArmColor = Decryptline12;
                GlobalVars.ColorMenu_RightArmColor = Decryptline13;
                GlobalVars.ColorMenu_LeftLegColor = Decryptline14;
                GlobalVars.ColorMenu_RightLegColor = Decryptline15;

                GlobalVars.Custom_Face_Offline = Decryptline16;
                GlobalVars.Custom_Head_Offline = Decryptline17;
                GlobalVars.Custom_T_Shirt_Offline = Decryptline18;
                GlobalVars.Custom_Shirt_Offline = Decryptline19;
                GlobalVars.Custom_Pants_Offline = Decryptline20;
                GlobalVars.Custom_Icon_Offline = Decryptline21;

                GlobalVars.CharacterID = Decryptline22;

                GlobalVars.Custom_Extra = Decryptline23;

                bool bline24 = Convert.ToBoolean(Decryptline24);
                GlobalVars.Custom_Extra_SelectionIsHat = bline24;
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
            string Decryptline2, Decryptline3, Decryptline4;

            IniFile ini = new IniFile(cfgpath);

            string section = "GENERAL";

            int FPS = GlobalVars.ReShadeFPSDisplay ? 1 : 0;
            Decryptline2 = ini.IniReadValue(section, "ShowFPS", FPS.ToString());
            Decryptline3 = ini.IniReadValue(section, "ShowFrameTime", FPS.ToString());
            int PerformanceMode = GlobalVars.ReShadePerformanceMode ? 1 : 0;
            Decryptline4 = ini.IniReadValue(section, "PerformanceMode", PerformanceMode.ToString());

            if (setglobals)
            {
                try
                {
                    switch(Convert.ToInt32(Decryptline2))
                    {
                        case int showFPSLine when showFPSLine == 1 && Convert.ToInt32(Decryptline3) == 1:
                            GlobalVars.ReShadeFPSDisplay = true;
                            break;
                        case int showFPSLine when showFPSLine == 0 && Convert.ToInt32(Decryptline3) == 0:
                        default:
                            GlobalVars.ReShadeFPSDisplay = false;
                            break;
                    }

                    switch (Convert.ToInt32(Decryptline4))
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
		GlobalVars.Custom_Extra_ShowHats = false;
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
		GlobalVars.Custom_Hat1ID_Offline = "NoHat.rbxm";
		GlobalVars.Custom_Hat2ID_Offline = "NoHat.rbxm";
		GlobalVars.Custom_Hat3ID_Offline = "NoHat.rbxm";
		GlobalVars.Custom_Face_Offline = "DefaultFace.rbxm";
		GlobalVars.Custom_Head_Offline = "DefaultHead.rbxm";
		GlobalVars.Custom_T_Shirt_Offline = "NoTShirt.rbxm";
		GlobalVars.Custom_Shirt_Offline = "NoShirt.rbxm";
		GlobalVars.Custom_Pants_Offline = "NoPants.rbxm";
		GlobalVars.Custom_Icon_Offline = "NBC";
		GlobalVars.Custom_Extra = "NoExtra.rbxm";
		GlobalVars.HeadColorID = 24;
		GlobalVars.TorsoColorID = 23;
		GlobalVars.LeftArmColorID = 24;
		GlobalVars.RightArmColorID = 24;
		GlobalVars.LeftLegColorID = 119;
		GlobalVars.RightLegColorID = 119;
		GlobalVars.CharacterID = "";
		GlobalVars.ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
		GlobalVars.ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
		GlobalVars.ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
		GlobalVars.Custom_Extra_SelectionIsHat = false;
		ReloadLoadtextValue();
	}
		
	public static void ReloadLoadtextValue()
	{
		string hat1 = (!GlobalVars.Custom_Hat1ID_Offline.EndsWith("-Solo.rbxm")) ? GlobalVars.Custom_Hat1ID_Offline : "NoHat.rbxm";
		string hat2 = (!GlobalVars.Custom_Hat2ID_Offline.EndsWith("-Solo.rbxm")) ? GlobalVars.Custom_Hat2ID_Offline : "NoHat.rbxm";
		string hat3 = (!GlobalVars.Custom_Hat3ID_Offline.EndsWith("-Solo.rbxm")) ? GlobalVars.Custom_Hat3ID_Offline : "NoHat.rbxm";
		string extra = (!GlobalVars.Custom_Extra.EndsWith("-Solo.rbxm")) ? GlobalVars.Custom_Extra : "NoExtra.rbxm";
			
		GlobalVars.loadtext = "'" + hat1 + "','" +
		hat2 + "','" +
		hat3 + "'," +
		GlobalVars.HeadColorID + "," +
		GlobalVars.TorsoColorID + "," +
		GlobalVars.LeftArmColorID + "," +
		GlobalVars.RightArmColorID + "," +
		GlobalVars.LeftLegColorID + "," +
		GlobalVars.RightLegColorID + ",'" +
		GlobalVars.Custom_T_Shirt_Offline + "','" +
		GlobalVars.Custom_Shirt_Offline + "','" +
		GlobalVars.Custom_Pants_Offline + "','" +
		GlobalVars.Custom_Face_Offline + "','" +
		GlobalVars.Custom_Head_Offline + "','" +
		GlobalVars.Custom_Icon_Offline + "','" +
		extra + "'";
			
		GlobalVars.sololoadtext = "'" + GlobalVars.Custom_Hat1ID_Offline + "','" +
		GlobalVars.Custom_Hat2ID_Offline + "','" +
		GlobalVars.Custom_Hat3ID_Offline + "'," +
		GlobalVars.HeadColorID + "," +
		GlobalVars.TorsoColorID + "," +
		GlobalVars.LeftArmColorID + "," +
		GlobalVars.RightArmColorID + "," +
		GlobalVars.LeftLegColorID + "," +
		GlobalVars.RightLegColorID + ",'" +
		GlobalVars.Custom_T_Shirt_Offline + "','" +
		GlobalVars.Custom_Shirt_Offline + "','" +
		GlobalVars.Custom_Pants_Offline + "','" +
		GlobalVars.Custom_Face_Offline + "','" +
		GlobalVars.Custom_Head_Offline + "','" +
		GlobalVars.Custom_Icon_Offline + "','" +
		GlobalVars.Custom_Extra + "'";
    }
		
	public static void ReadClientValues(string clientpath)
	{
		string line1;
		string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline9, Decryptline10, Decryptline11, Decryptline12;

		using (StreamReader reader = new StreamReader(clientpath)) {
			line1 = reader.ReadLine();
		}
			
		string ConvertedLine = SecurityFuncs.Base64Decode(line1);
		string[] result = ConvertedLine.Split('|');
		Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
		Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
		Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
		Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
		Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
		Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
		Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
		Decryptline9 = SecurityFuncs.Base64Decode(result[8]);
		Decryptline10 = SecurityFuncs.Base64Decode(result[9]);
		Decryptline11 = SecurityFuncs.Base64Decode(result[10]);
        try
        {
            Decryptline12 = SecurityFuncs.Base64Decode(result[11]);
        }
        catch
        {
            //fake this option until we properly apply it.
            Decryptline11 = "False";
            Decryptline12 = SecurityFuncs.Base64Decode(result[10]);
        }

        bool bline1 = Convert.ToBoolean(Decryptline1);
        GlobalVars.SelectedClientInfo.UsesPlayerName = bline1;
			
		bool bline2 = Convert.ToBoolean(Decryptline2);
        GlobalVars.SelectedClientInfo.UsesID = bline2;

        GlobalVars.SelectedClientInfo.Warning = Decryptline3;
			
		bool bline4 = Convert.ToBoolean(Decryptline4);
        GlobalVars.SelectedClientInfo.LegacyMode = bline4;

        GlobalVars.SelectedClientInfo.ClientMD5 = Decryptline5;
        GlobalVars.SelectedClientInfo.ScriptMD5 = Decryptline6;
        GlobalVars.SelectedClientInfo.Description = Decryptline7;
			
		bool bline9 = Convert.ToBoolean(Decryptline9);
        GlobalVars.SelectedClientInfo.Fix2007 = bline9;
			
		bool bline10 = Convert.ToBoolean(Decryptline10);
        GlobalVars.SelectedClientInfo.AlreadyHasSecurity = bline10;

        bool bline11 = Convert.ToBoolean(Decryptline11);
        GlobalVars.SelectedClientInfo.NoGraphicsOptions = bline11;

        GlobalVars.SelectedClientInfo.CommandLineArgs = Decryptline12;
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
