#region Usings
#if LAUNCHER || CMD || URI
using NLog;
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
#endregion

#region Global Functions
public class GlobalFuncs
{
    public static void ReadInfoFile(string infopath, bool other = false, string exepath = "")
    {
        //READ
        string versionbranch, defaultclient, defaultmap, regclient1,
            regclient2, extendedversionnumber, extendedversiontemplate, 
            extendedversionrevision, extendedversioneditchangelog;

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
            GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
            GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
            GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
            GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
        }
        catch (Exception ex)
        {
#if URI || LAUNCHER || CMD
            LogExceptions(ex);
#endif
            ReadInfoFile(infopath, other);
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
            ini.IniWriteValue(section, "GraphicsMode", ((int)GlobalVars.UserConfiguration.GraphicsMode).ToString());
            ini.IniWriteValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
            ini.IniWriteValue(section, "QualityLevel", ((int)GlobalVars.UserConfiguration.QualityLevel).ToString());
            ini.IniWriteValue(section, "Style", ((int)GlobalVars.UserConfiguration.LauncherStyle).ToString());
            ini.IniWriteValue(section, "AssetLocalizerSaveBackups", GlobalVars.UserConfiguration.AssetLocalizerSaveBackups.ToString());
            ini.IniWriteValue(section, "AlternateServerIP", GlobalVars.UserConfiguration.AlternateServerIP.ToString());
            ini.IniWriteValue(section, "DisableReshadeDelete", GlobalVars.UserConfiguration.DisableReshadeDelete.ToString());
            ini.IniWriteValue(section, "ShowServerNotifications", GlobalVars.UserConfiguration.ShowServerNotifications.ToString());
            ini.IniWriteValue(section, "ServerBrowserServerName", GlobalVars.UserConfiguration.ServerBrowserServerName.ToString());
            ini.IniWriteValue(section, "ServerBrowserServerAddress", GlobalVars.UserConfiguration.ServerBrowserServerAddress.ToString());
            ini.IniWriteValue(section, "ClientLaunchPriority", ((int)GlobalVars.UserConfiguration.Priority).ToString());
        }
        else
        {
            try
            {
                //READ
                string closeonlaunch, userid, name, selectedclient,
                map, port, limit, upnp,
                disablehelpmessage, tripcode, discord, mappath, mapsnip,
                graphics, reshade, qualitylevel, style, savebackups, altIP, 
                disReshadeDel, showNotifs, SB_Name, SB_Address, priority;

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
                graphics = ini.IniReadValue(section, "GraphicsMode", ((int)GlobalVars.UserConfiguration.GraphicsMode).ToString());
                reshade = ini.IniReadValue(section, "ReShade", GlobalVars.UserConfiguration.ReShade.ToString());
                qualitylevel = ini.IniReadValue(section, "QualityLevel", ((int)GlobalVars.UserConfiguration.QualityLevel).ToString());
                style = ini.IniReadValue(section, "Style", ((int)GlobalVars.UserConfiguration.LauncherStyle).ToString());
                savebackups = ini.IniReadValue(section, "AssetLocalizerSaveBackups", GlobalVars.UserConfiguration.AssetLocalizerSaveBackups.ToString());
                altIP = ini.IniReadValue(section, "AlternateServerIP", GlobalVars.UserConfiguration.AlternateServerIP.ToString());
                disReshadeDel = ini.IniReadValue(section, "DisableReshadeDelete", GlobalVars.UserConfiguration.DisableReshadeDelete.ToString());
                showNotifs = ini.IniReadValue(section, "ShowServerNotifications", GlobalVars.UserConfiguration.ShowServerNotifications.ToString());
                SB_Name = ini.IniReadValue(section, "ServerBrowserServerName", GlobalVars.UserConfiguration.ServerBrowserServerName.ToString());
                SB_Address = ini.IniReadValue(section, "ServerBrowserServerAddress", GlobalVars.UserConfiguration.ServerBrowserServerAddress.ToString());
                priority = ini.IniReadValue(section, "ClientLaunchPriority", ((int)GlobalVars.UserConfiguration.Priority).ToString());

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

                GlobalVars.UserConfiguration.GraphicsMode = (Settings.Mode)Convert.ToInt32(graphics);
                GlobalVars.UserConfiguration.ReShade = Convert.ToBoolean(reshade);
                GlobalVars.UserConfiguration.QualityLevel = (Settings.Level)Convert.ToInt32(qualitylevel);
                GlobalVars.UserConfiguration.LauncherStyle = (Settings.Style)Convert.ToInt32(style);
                GlobalVars.UserConfiguration.AssetLocalizerSaveBackups = Convert.ToBoolean(savebackups);
                GlobalVars.UserConfiguration.AlternateServerIP = altIP;
                GlobalVars.UserConfiguration.DisableReshadeDelete = Convert.ToBoolean(disReshadeDel);
                GlobalVars.UserConfiguration.ShowServerNotifications = Convert.ToBoolean(showNotifs);
                GlobalVars.UserConfiguration.ServerBrowserServerName = SB_Name;
                GlobalVars.UserConfiguration.ServerBrowserServerAddress = SB_Address;
                GlobalVars.UserConfiguration.Priority = (ProcessPriorityClass)Convert.ToInt32(priority);
            }
            catch (Exception ex)
            {
#if URI || LAUNCHER || CMD
                LogExceptions(ex);
#endif
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
            catch (Exception ex)
            {
#if URI || LAUNCHER || CMD
                LogExceptions(ex);
#endif
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
            FixedFileCopy(GlobalPaths.ConfigDir + "\\ReShade_default.ini", fullpath, false);
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
                    FixedFileCopy(fullpath, fulldirpath, false);
                    ReShadeValues(fulldirpath, write, false);
                }
                else
                {
                    ReShadeValues(fulldirpath, write, false);
                }

                if (!File.Exists(fulldllpath))
                {
                    FixedFileCopy(GlobalPaths.ConfigDirData + "\\" + dllfilename, fulldllpath, false);
                }
            }
            else
            {
                FixedFileDelete(fulldirpath);

                if (!GlobalVars.UserConfiguration.DisableReshadeDelete)
                {
                    FixedFileDelete(fulldllpath);
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
                catch (Exception ex)
                {
#if URI || LAUNCHER || CMD
                LogExceptions(ex);
#endif
                    ReShadeValues(cfgpath, true, setglobals);
                }
            }
        }
    }

#if LAUNCHER
    public static void ReadClientValues(RichTextBox box, bool initial = false)
#else
    public static void ReadClientValues(bool initial = false)
#endif
    {
#if LAUNCHER
        ReadClientValues(GlobalVars.UserConfiguration.SelectedClient, box, initial);
#else
        ReadClientValues(GlobalVars.UserConfiguration.SelectedClient, initial);
#endif
    }

#if LAUNCHER
    public static void ReadClientValues(string ClientName, RichTextBox box, bool initial = false)
#else
    public static void ReadClientValues(string ClientName, bool initial = false)
#endif
    {
        string name = ClientName;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = GlobalVars.ProgramInformation.DefaultClient;
        }
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
            ReadClientValues(name, box, initial);
#else
            ReadClientValues(name, initial);
#endif
        }
        else
        {
            LoadClientValues(clientpath);

            if (initial)
            {
#if LAUNCHER
            ConsolePrint("Client '" + name + "' successfully loaded.", 3, box);
#elif CMD
            GlobalFuncs.ConsolePrint("Client '" + name + "' successfully loaded.", 3);
#elif URI
#endif
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
                    FixedFileCopy(dir, fullpath, false);
                }
            }
        }

        ChangeGameSettings(ClientName);
    }

    public static void FixedFileCopy(string src, string dest, bool overwrite, bool overwritewarning = false)
    {
        if (File.Exists(dest))
        {
            if (overwrite && overwritewarning)
            {
                if (ShowOverrideWarning(dest) == DialogResult.No)
                {
                    return;
                }
            }

            File.SetAttributes(dest, FileAttributes.Normal);
        }

        File.Copy(src, dest, overwrite);
        File.SetAttributes(dest, FileAttributes.Normal);
    }

    public static void FixedFileDelete(string src)
    {
        if (File.Exists(src))
        {
            File.SetAttributes(src, FileAttributes.Normal);
            File.Delete(src);
        }
    }

    public static void FixedFileMove(string src, string dest, bool overwrite, bool overwritewarning = false)
    {
        if (src.Equals(dest))
            return;

        if (!File.Exists(dest))
        {
            File.SetAttributes(src, FileAttributes.Normal);
            File.Move(src, dest);
        }
        else
        {
            if (overwrite)
            {
                if (overwritewarning)
                {
                    if (ShowOverrideWarning(dest) == DialogResult.No)
                    {
                        return;
                    }
                }

                FixedFileDelete(dest);
                File.SetAttributes(src, FileAttributes.Normal);
                File.Move(src, dest);
            }
            else
            {
                throw new IOException("Cannot create a file when that file already exists. FixedFileMove cannot override files with overwrite disabled.");
            }
        }
    }

    private static DialogResult ShowOverrideWarning(string dest)
    {
        DialogResult box = MessageBox.Show("A file with a similar name was detected in the directory as '" + dest +
                        "'.\n\nWould you like to override it?", "Novetus - Override Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        return box;
    }

    //modified from the following:
    //https://stackoverflow.com/questions/28887314/performance-of-image-loading
    //https://stackoverflow.com/questions/2479771/c-why-am-i-getting-the-process-cannot-access-the-file-because-it-is-being-u
    public static Image LoadImage(string fileFullName, string fallbackFileFullName = "")
    {
        if (string.IsNullOrWhiteSpace(fileFullName))
            return null;

        Image image = null;

        try
        {
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(fileFullName)))
            {
                image = Image.FromStream(ms);
            }

            // PropertyItems seem to get lost when fileStream is closed to quickly (?); perhaps
            // this is the reason Microsoft didn't want to close it in the first place.
            PropertyItem[] items = image.PropertyItems;

            foreach (PropertyItem item in items)
            {
                image.SetPropertyItem(item);
            }
        }
        catch (Exception ex)
        {
#if URI || LAUNCHER || CMD
                LogExceptions(ex);
#endif
            if (!string.IsNullOrWhiteSpace(fallbackFileFullName))
                image = LoadImage(fallbackFileFullName);
        }

        return image;
    }

    public static string CopyMapToRBXAsset()
    {
        string clientcontentpath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\temp.rbxl";
        FixedFileCopy(GlobalVars.UserConfiguration.MapPath, clientcontentpath, true);
        return GlobalPaths.AltBaseGameDir + "temp.rbxl";
    }

    //NOT FOR SDK.
    public static FileFormat.ClientInfo GetClientInfoValues(string ClientName)
    {
        string name = ClientName;
        FileFormat.ClientInfo info = new FileFormat.ClientInfo();
        string clientpath = GlobalPaths.ClientDir + @"\\" + name + @"\\clientinfo.nov";
        LoadClientValues(info, clientpath);
        return info;
    }

    //https://social.msdn.microsoft.com/Forums/vstudio/en-US/b0c31115-f6f0-4de5-a62d-d766a855d4d1/directorygetfiles-with-searchpattern-to-get-all-dll-and-exe-files-in-one-call?forum=netfxbcl
    public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
        string[] searchPatterns = searchPattern.Split('|');
        List<string> files = new List<string>();
        foreach (string sp in searchPatterns)
            files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
        files.Sort();
        return files.ToArray();
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
            clientloadoptions, commandlineargs;

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
        try
        {
            commandlineargs = SecurityFuncs.Base64Decode(result[11]);
        }
        catch
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
        
        info.CommandLineArgs = commandlineargs;
    }

#if LAUNCHER
    public static void ResetConfigValues(bool IsInCompact = false)
#else
    public static void ResetConfigValues()
#endif
    {
        GlobalVars.UserConfiguration = new FileFormat.Config();
        GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
        GlobalVars.UserConfiguration.Map = GlobalVars.ProgramInformation.DefaultMap;
        GlobalVars.UserConfiguration.MapPath = GlobalPaths.MapsDir + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
        GlobalVars.UserConfiguration.MapPathSnip = GlobalPaths.MapsDirBase + @"\\" + GlobalVars.ProgramInformation.DefaultMap;
#if LAUNCHER
        if (IsInCompact)
        {
            GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Compact;
        }
        else
        {
            GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Extended;
        }
#else
        GlobalVars.UserConfiguration.LauncherStyle = Settings.Style.Extended;
#endif
        GeneratePlayerID();
        GenerateTripcode();
        ResetCustomizationValues();
	}
		
	public static void ResetCustomizationValues()
	{
        GlobalVars.UserCustomization = new FileFormat.CustomizationConfig();
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
        FileFormat.ClientInfo info = GetClientInfoValues(ClientName);

        string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.FileDeleteFilterName;
        string[] fileListToDelete = File.ReadAllLines(filterPath);

        foreach (string file in fileListToDelete)
        {
            string fullFilePath = Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + file;
            FixedFileDelete(fullFilePath);
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

            try
            {
                foreach (string dir in dirs)
                {
                    if (dir.Contains(terms) && !dir.Contains("_default"))
                    {
                        FixedFileCopy(dir, Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                            .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                            .Replace("-", ".xml"), true);
                    }
                }
            }
            catch (Exception ex)
            {
#if URI || LAUNCHER || CMD
                LogExceptions(ex);
#endif
                return;
            }
        }
    }

    //oh god....
    //we're using this one for custom graphics quality. Better than the latter.
    public static void ApplyClientSettings_custom(FileFormat.ClientInfo info, string ClientName, int MeshDetail, int ShadingQuality, int MaterialQuality,
        int AA, int AASamples, int Bevels, int Shadows_2008, bool Shadows_2007, string Style_2007, int GFXQualityLevel, string WindowResolution, string FullscreenResolution, 
        int ModernResolution)
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
#if URI || LAUNCHER || CMD
                        LogExceptions(ex);
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
                            RobloxXML.EditRenderSettings(doc, "TrussDetail", "0", XMLTypes.Token);
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
                    catch (Exception ex)
                    {
#if URI || LAUNCHER || CMD
                        LogExceptions(ex);
#endif
                        return;
                    }
                    finally
                    {
                        doc.Save(dir);
                        FixedFileCopy(dir, Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                            .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                            .Replace("-", ".xml"), true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
#if URI || LAUNCHER || CMD
            LogExceptions(ex);
#endif
            return;
        }
    }

    public static string GetLuaFileName()
    {
        return GetLuaFileName(GlobalVars.UserConfiguration.SelectedClient);
    }

    public static string GetLuaFileName(string ClientName)
    {
        string luafile = "";

        if (!GlobalVars.SelectedClientInfo.Fix2007)
        {
            luafile = "rbxasset://scripts\\\\" + GlobalPaths.ScriptName + ".lua";
        }
        else
        {
            luafile = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua";
        }

        return luafile;
    }

    public static string GetClientEXEDir(ScriptType type)
    {
        return GetClientEXEDir(GlobalVars.UserConfiguration.SelectedClient, type);
    }

    public static string GetClientEXEDir(string ClientName, ScriptType type)
    {
        string rbxexe = "";
        if (GlobalVars.SelectedClientInfo.LegacyMode)
        {
            rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp.exe";
        }
        else
        {
            switch (type)
            {
                case ScriptType.Client:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_client.exe";
                    break;
                case ScriptType.Server:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_server.exe";
                    break;
                case ScriptType.Studio:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_studio.exe";
                    break;
                case ScriptType.Solo:
                case ScriptType.EasterEgg:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_solo.exe";
                    break;
                case ScriptType.None:
                default:
                    rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp.exe";
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

        string luafile = GetLuaFileName(ClientName);
        string rbxexe = GetClientEXEDir(ClientName, type);
        string mapfile = type.Equals(ScriptType.EasterEgg) ? GlobalPaths.ConfigDirData + "\\Appreciation.rbxl" : (nomap ? "" : GlobalVars.UserConfiguration.MapPath);
        string mapname = type.Equals(ScriptType.EasterEgg) ? "" : (nomap ? "" : GlobalVars.UserConfiguration.Map);
        FileFormat.ClientInfo info = GetClientInfoValues(ClientName);
        string quote = "\"";
        string args = "";

        if (info.CommandLineArgs.Equals("%args%"))
        {
            if (!info.Fix2007)
            {
                args = quote 
                    + mapfile 
                    + "\" -script \" dofile('" + luafile + "'); " 
                    + ScriptFuncs.Generator.GetScriptFuncForType(ClientName, type) 
                    + (!string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? " dofile('" + GlobalPaths.AddonScriptPath + "');" : "") 
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
                    if (info.AlreadyHasSecurity != true)
                    {
                        if (SecurityFuncs.checkClientMD5(ClientName) && SecurityFuncs.checkScriptMD5(ClientName))
                        {
                            OpenClient(type, rbxexe, args, ClientName, mapname, e);
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
#if URI || LAUNCHER || CMD
            LogExceptions(ex);
#endif
        }
    }

    private static void OpenClient(ScriptType type, string rbxexe, string args, string clientname, string mapname, EventHandler e)
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
        client.PriorityClass = GlobalVars.UserConfiguration.Priority;
        SecurityFuncs.RenameWindow(client, type, clientname, mapname);
        if (e != null)
        {
            UpdateRichPresence(GetStateForType(type), clientname, mapname);
        }
#if CMD
        GlobalVars.ProcessID = client.Id;
        CreateTXT();
#endif
    }

#if LAUNCHER
    public static void  ConsolePrint(string text, int type, RichTextBox box, bool noLog = false)
    {
        if (box == null)
            return;

        box.AppendText("[" + DateTime.Now.ToShortTimeString() + "] - ", Color.White);

        Logger log = LogManager.GetCurrentClassLogger();

        switch (type)
        {
            case 2:
                box.AppendText(text, Color.Red);
                if (!noLog)
                    log.Error(text);
                break;
            case 3:
                box.AppendText(text, Color.Lime);
                if (!noLog)
                    log.Info(text);
                break;
            case 4:
                box.AppendText(text, Color.Aqua);
                if (!noLog)
                    log.Info(text);
                break;
            case 5:
                box.AppendText(text, Color.Yellow);
                if (!noLog)
                    log.Warn(text);
                break;
            case 6:
                box.AppendText(text, Color.LightSalmon);
                if (!noLog)
                    log.Info(text);
                break;
            case 1:
            default:
                box.AppendText(text, Color.White);
                if (!noLog)
                    log.Info(text);
                break;
        }

        box.AppendText(Environment.NewLine, Color.White);
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
                        SecurityFuncs.Base64Encode((!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : IP)),
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
                   "IP: " + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : IP),
                   "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                   "Map: " + GlobalVars.UserConfiguration.Map,
                   "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                   "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                   "Online URI Link:",
                   URI,
                   "Local URI Link:",
                   URI2
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

        /*
        if (!Directory.Exists(GlobalPaths.AssetCacheDirScriptAssets))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirScriptAssets);
        }*/
    }

    public static string MultiLine(params string[] args)
    {
        return string.Join(Environment.NewLine, args);
    }

    public static string RemoveEmptyLines(string lines)
    {
        return Regex.Replace(lines, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
    }

    // Credit to Carrot for the original code. Rewote it to be smaller.
    public static string CryptStringWithByte(string word)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(word);
        string result = "";
        for (int i = 0; i < bytes.Length; i++) { result += Convert.ToChar(0x55 ^ bytes[i]); }
        return result;
    }

    //https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    //https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
    private static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
    public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
    {
        if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
        if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }
        if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

        // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
        int mag = (int)Math.Log(value, 1024);

        // 1L << (mag * 10) == 2 ^ (10 * mag) 
        // [i.e. the number of bytes in the unit corresponding to mag]
        decimal adjustedSize = (decimal)value / (1L << (mag * 10));

        // make adjustment when the value is large enough that
        // it would round up to 1000 or more
        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }

        return string.Format("{0:n" + decimalPlaces + "} {1}",
            adjustedSize,
            SizeSuffixes[mag]);
    }

    //https://stackoverflow.com/questions/11927116/getting-files-recursively-skip-files-directories-that-cannot-be-read
    public static string[] FindAllFiles(string rootDir)
    {
        var pathsToSearch = new Queue<string>();
        var foundFiles = new List<string>();

        pathsToSearch.Enqueue(rootDir);

        while (pathsToSearch.Count > 0)
        {
            var dir = pathsToSearch.Dequeue();

            try
            {
                var files = Directory.GetFiles(dir);
                foreach (var file in Directory.GetFiles(dir))
                {
                    foundFiles.Add(file);
                }

                foreach (var subDir in Directory.GetDirectories(dir))
                {
                    pathsToSearch.Enqueue(subDir);
                }

            }
            catch (Exception ex/* TODO: catch correct exception */)
            {
                // Swallow.  Gulp!
#if URI || LAUNCHER || CMD
                LogExceptions(ex);
#endif
            }
        }

        return foundFiles.ToArray();
    }

    //https://stackoverflow.com/questions/66667263/i-want-to-remove-special-characters-from-file-name-without-affecting-extension-i
    //https://stackoverflow.com/questions/3218910/rename-a-file-in-c-sharp

    public static bool FileHasInvalidChars(string path)
    {
        string fileName = Path.GetFileName(path);

        if (Regex.Match(fileName, @"[^\w-.'_!()& ]") != Match.Empty)
        {
            return true;
        }

        return false;
    }

    public static void RenameFileWithInvalidChars(string path)
    {
        if (!FileHasInvalidChars(path))
            return;

        string pathWithoutFilename = Path.GetDirectoryName(path);
        string fileName = Path.GetFileName(path);
        fileName = Regex.Replace(fileName, @"[^\w-.'_!()& ]", "");
        string finalPath = pathWithoutFilename + "\\" + fileName;

        FixedFileMove(path, finalPath, false);
    }

#if LAUNCHER || CMD || URI
    public static void LogExceptions(Exception ex)
    {
        Logger log = LogManager.GetCurrentClassLogger();
        log.Error("EXCEPTION|MESSAGE: " + (ex.Message != null ? ex.Message.ToString() : "N/A"));
        log.Error("EXCEPTION|STACK TRACE: " + (!string.IsNullOrWhiteSpace(ex.StackTrace) ? ex.StackTrace : "N/A"));
        log.Error("EXCEPTION|ADDITIONAL INFO: " + (ex != null ? ex.ToString() : "N/A"));
    }
#endif

    //http://stevenhollidge.blogspot.com/2012/06/async-taskdelay.html
    public static Task Delay(int milliseconds)
    {
        var tcs = new TaskCompletionSource<object>();
        new System.Threading.Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
        return tcs.Task;
    }
}
#endregion
