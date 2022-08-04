#region Usings
#if LAUNCHER || CMD || URI || BASICLAUNCHER
using NLog;
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
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

        GlobalVars.ProgramInformation.IsLite = Convert.ToBoolean(isLite);

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
                            .Replace("%extended-revision%", (!extendedversionrevision.Equals("-1") ? extendedversionrevision : ""))
                            .Replace("%lite%", (GlobalVars.ProgramInformation.IsLite ? " (Lite)" : ""));
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
                        .Replace("%extended-revision%", (!extendedversionrevision.Equals("-1") ? extendedversionrevision : ""))
                        .Replace("%lite%", (GlobalVars.ProgramInformation.IsLite ? " (Lite)" : ""));
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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

        if(!File.Exists(cfgpath))
        {
            // force write mode on if the file doesn't exist.
            write = true;
            forcewrite = true;
        }

        if (write)
        {
            if (IsWineRunning())
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
                firstServerLaunch, newgui;

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
                    GeneratePlayerID();
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
                Config(cfgpath, true);
            }
        }

        if (!forcewrite)
        {
            string curval = GenerateAndReturnTripcode();
            if (!GlobalVars.PlayerTripcode.Equals(curval))
            {
                GlobalVars.PlayerTripcode = curval;
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
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
                    FixedFileCopy(GlobalPaths.DataDir + "\\" + dllfilename, fulldllpath, false);
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
                    switch(ValueInt(framerate, 0))
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                catch (Exception ex)
                {
                    LogExceptions(ex);
#else
		        catch (Exception)
		        {
#endif
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            goto Failure;
        }

    Failure:
        return false;
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
#if LAUNCHER
                ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available. Novetus will attempt to generate one.", 2, box);
#elif CMD
                ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available. Novetus will attempt to generate one.", 2);
#endif
                GenerateDefaultClientInfo(Path.GetDirectoryName(clientpath));

#if LAUNCHER
                ReadClientValues(name, box, initial);
#else
                ReadClientValues(name, initial);
#endif
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif

#if LAUNCHER
                ConsolePrint("ERROR - Failed to generate default clientinfo.nov. Info: " + ex.Message, 2, box);
                ConsolePrint("Loading default client '" + GlobalVars.ProgramInformation.DefaultClient + "'", 4, box);
#elif CMD
                ConsolePrint("ERROR - Failed to generate default clientinfo.nov. Info: " + ex.Message, 2);
                ConsolePrint("Loading default client '" + GlobalVars.ProgramInformation.DefaultClient + "'", 4);
#endif
                name = GlobalVars.ProgramInformation.DefaultClient;
#if LAUNCHER
                ReadClientValues(name, box, initial);
#else
                ReadClientValues(name, initial);
#endif
            }
        }
        else
        {
            LoadClientValues(clientpath);

            if (initial)
            {
#if LAUNCHER
            ConsolePrint("Client '" + name + "' successfully loaded.", 3, box);
#elif CMD
            ConsolePrint("Client '" + name + "' successfully loaded.", 3);
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
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
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            return null;
        }
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

#if LAUNCHER
    public static void ResetConfigValues(Settings.Style style)
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
        GlobalVars.UserConfiguration.LauncherStyle = style;
#endif
        GeneratePlayerID();
        ResetCustomizationValues();
	}
		
	public static void ResetCustomizationValues()
	{
        GlobalVars.UserCustomization = new FileFormat.CustomizationConfig();
        ReloadLoadoutValue();
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

            Downloader download = new Downloader(item, fullname, "", GlobalPaths.AssetCacheDirTextures);

            try
            {
                string path = download.GetFullDLPath();
                download.InitDownloadNoDialog(path);
                return GlobalPaths.AssetCacheTexturesGameDir + download.fileName;
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
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

            Downloader download = new Downloader(item, name + "Temp.rbxm", "", GlobalPaths.AssetCacheDirFonts);

            try
            {
                string path = download.GetFullDLPath();
                download.InitDownloadNoDialog(path);
                string oldfile = File.ReadAllText(path);
                string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile)).Replace("&#9;", "\t").Replace("#9;", "\t");
                XDocument doc = null;
                XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };
                Stream filestream = GenerateStreamFromString(fixedfile);
                using (XmlReader xmlReader = XmlReader.Create(filestream, xmlReaderSettings))
                {
                    xmlReader.MoveToContent();
                    doc = XDocument.Load(xmlReader);
                }

                return RobloxXML.GetURLInNodes(doc, assetCacheDef.Class, assetCacheDef.Id[0], item);
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
            }
        }

        return "";
    }

    

    public static void GeneratePlayerID()
	{
		int randomID = SecurityFuncs.GenerateRandomNumber();
        //2147483647 is max id.
        GlobalVars.UserConfiguration.UserID = randomID;
	}

    public static string GenerateAndReturnTripcode()
    {
        //Powered by https://github.com/davcs86/csharp-uhwid
        return UHWID.UHWIDEngine.AdvancedUid;
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
                        FixedFileCopy(dir, Settings.GetPathForClientLoadOptions(info.ClientLoadOptions) + @"\" + Path.GetFileName(dir).Replace(terms, "")
                            .Replace(dir.Substring(dir.LastIndexOf('-') + 1), "")
                            .Replace("-", ".xml"), true);
                    }
                }
            }
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
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
            LogExceptions(ex);
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
                        LogExceptions(ex);
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                    catch (Exception ex)
                    {
                        LogExceptions(ex);
#else
		            catch (Exception)
		            {
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
            return;
        }
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
            luafile = "rbxasset://scripts\\\\" + GlobalPaths.ScriptName + ".lua";
        }
        else
        {
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
        switch (type)
        {
            case ScriptType.Client:
                ReloadLoadoutValue(true);
                if (!GlobalVars.LocalPlayMode && GlobalVars.GameOpened != ScriptType.Server)
                {
                    goto default;
                }
                break;
            case ScriptType.Server:
                if (GlobalVars.GameOpened == ScriptType.Server)
                {
#if LAUNCHER
                    if (box != null)
                    {
                        ConsolePrint("ERROR - Failed to launch Novetus. (A server is already running.)", 2, box);
                    }
#elif CMD
                    ConsolePrint("ERROR - Failed to launch Novetus. (A server is already running.)", 2);
#endif

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
                    ConsolePrint(hostingTips + "\nPress any key to continue...", 4);
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
                ReloadLoadoutValue(true);
                goto default;
            default:
                if (GlobalVars.GameOpened != ScriptType.None)
                {
#if LAUNCHER
                    if (box != null)
                    {
                        ConsolePrint("ERROR - Failed to launch Novetus. (A game is already running.)", 2, box);
                    }
#elif CMD
                    ConsolePrint("ERROR - Failed to launch Novetus. (A game is already running.)", 2);
#endif

#if LAUNCHER
                    MessageBox.Show("Failed to launch Novetus. (Error: A game is already running.)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    return;
                }
                break;
        }

#if LAUNCHER
        ReadClientValues(ClientName, box);
#else
        ReadClientValues(ClientName);
#endif

        string luafile = GetLuaFileName(ClientName, type);
        string rbxexe = GetClientEXEDir(ClientName, type);
        string mapfile = type.Equals(ScriptType.EasterEgg) ? GlobalPaths.DataDir + "\\Appreciation.rbxl" : (nomap ? "" : GlobalVars.UserConfiguration.MapPath);
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
                                return;
                            }
                            else
                            {
                                GlobalVars.ValidatedExtraFiles += 1;
                            }
                        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                        catch (Exception ex)
                        {
                            LogExceptions(ex);
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
                            return;
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
#if LAUNCHER
                    PingMasterServer(true, "Server will now display on the defined master server.", box);
#elif CMD
                    PingMasterServer(true, "Server will now display on the defined master server.");
#endif
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            LogExceptions(ex);
#endif
        }
    }

#if LAUNCHER
    public static void PingMasterServer(bool online, string reason, RichTextBox box)
#else
    public static void PingMasterServer(bool online, string reason)
#endif
    {
        if (online)
        {
            GlobalVars.ServerID = SecurityFuncs.RandomString(30) + SecurityFuncs.GenerateRandomNumber();
            GlobalVars.PingURL = "http://" + GlobalVars.UserConfiguration.ServerBrowserServerAddress +
            "/list.php?name=" + GlobalVars.UserConfiguration.ServerBrowserServerName +
            "&ip=" + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP) +
            "&port=" + GlobalVars.UserConfiguration.RobloxPort +
            "&client=" + GlobalVars.UserConfiguration.SelectedClient +
            "&version=" + GlobalVars.ProgramInformation.Version +
            "&id=" + GlobalVars.ServerID;
        }
        else
        {
            GlobalVars.PingURL = "http://" + GlobalVars.UserConfiguration.ServerBrowserServerAddress +
            "/delist.php?id=" + GlobalVars.ServerID;
            GlobalVars.ServerID = "N/A";
        }

#if LAUNCHER
        ConsolePrint("Pinging master server. " + reason, 4, box);
#elif CMD
        ConsolePrint("Pinging master server. " + reason, 4);
#endif

#if LAUNCHER
        Task.Factory.StartNew(() => TryPing(box));
#else
        Task.Factory.StartNew(() => TryPing());
#endif
    }

#if LAUNCHER
    public static void TryPing(RichTextBox box)
#else
    private static void TryPing()
#endif
    {
        string response = HttpGet(GlobalVars.PingURL);

        if (!string.IsNullOrWhiteSpace(response))
        {
#if LAUNCHER
            ConsolePrint(response, response.Contains("ERROR:") ? 2 : 4, box);
#elif CMD
            ConsolePrint(response, response.Contains("ERROR:") ? 2 : 4);
#endif

            if (response.Contains("ERROR:"))
            {
                GlobalVars.ServerID = "N/A";
            }
        }

        if (!GlobalVars.ServerID.Equals("N/A"))
        {
#if LAUNCHER
            ConsolePrint("Your server's ID is " + GlobalVars.ServerID, 4, box);
#elif CMD
            ConsolePrint("Your server's ID is " + GlobalVars.ServerID, 4);
#endif
        }

        GlobalVars.PingURL = "";
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
        CreateTXT();
#endif
    }

#if LAUNCHER
    public static void ConsolePrint(string text, int type, RichTextBox box, bool noLog = false, bool noTime = false)
    {
        if (box == null)
            return;

        if (!noTime)
        {
            box.AppendText("[" + DateTime.Now.ToShortTimeString() + "] - ", Color.White);
        }

        switch (type)
        {
            case 1:
                box.AppendText(text, Color.White);
                if (!noLog)
                    LogPrint(text);
                break;
            case 2:
                box.AppendText(text, Color.Red);
                if (!noLog)
                    LogPrint(text, 2);
                break;
            case 3:
                box.AppendText(text, Color.Lime);
                if (!noLog)
                    LogPrint(text);
                break;
            case 4:
                box.AppendText(text, Color.Aqua);
                if (!noLog)
                    LogPrint(text);
                break;
            case 5:
                box.AppendText(text, Color.Yellow);
                if (!noLog)
                    LogPrint(text, 3);
                break;
            case 6:
                box.AppendText(text, Color.LightSalmon);
                if (!noLog)
                    LogPrint(text);
                break;
            case 0:
            default:
                box.AppendText(text, Color.Black);
                if (!noLog)
                    LogPrint(text);
                break;
        }

        box.AppendText(Environment.NewLine, Color.White);
    }
#elif CMD
    public static void ConsolePrint(string text, int type, bool notime = false, bool noLog = false)
    {
        if (!notime)
        {
            ConsoleText("[" + DateTime.Now.ToShortTimeString() + "] - ", ConsoleColor.White);
        }

        switch (type)
        {
            case 2:
                ConsoleText(text, ConsoleColor.Red);
                if (!noLog)
                    LogPrint(text, 2);
                break;
            case 3:
                ConsoleText(text, ConsoleColor.Green);
                if (!noLog)
                    LogPrint(text);
                break;
            case 4:
                ConsoleText(text, ConsoleColor.Cyan);
                if (!noLog)
                    LogPrint(text);
                break;
            case 5:
                ConsoleText(text, ConsoleColor.Yellow);
                if (!noLog)
                    LogPrint(text, 3);
                break;
            case 1:
            default:
                ConsoleText(text, ConsoleColor.White);
                if (!noLog)
                    LogPrint(text);
                break;
        }

        ConsoleText(Environment.NewLine, ConsoleColor.White);
    }

    public static void ConsoleText(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }

    public static void CreateTXT()
    {
        if (GlobalVars.RequestToOutputInfo)
        {
            string[] lines1 = {
                        SecurityFuncs.Base64Encode(!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
            string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1), true);
            string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
            string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2), true);

            string[] text = {
                   "Process ID: " + (GlobalVars.ProcessID == 0 ? "N/A" : GlobalVars.ProcessID.ToString()),
                   "Don't copy the Process ID when sharing the server.",
                   "--------------------",
                   "Server Info:",
                   "Client: " + GlobalVars.UserConfiguration.SelectedClient,
                   "IP: " + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP),
                   "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                   "Map: " + GlobalVars.UserConfiguration.Map,
                   "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                   "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                   "Online URI Link:",
                   URI,
                   "Local URI Link:",
                   URI2
                   };

            string txt = GlobalPaths.BasePath + "\\" + GlobalVars.ServerInfoFileName;
            File.WriteAllLines(txt, text);
            ConsolePrint("Server Information sent to file " + txt, 4);
        }
    }
#endif

    public static void LogPrint(string text, int type = 1)
    {
        Logger log = LogManager.GetCurrentClassLogger();

        switch (type)
        {
            case 2:
                log.Error(text);
                break;
            case 3:
                log.Warn(text);
                break;
            default:
                log.Info(text);
                break;
        }
    }

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

        if (!Directory.Exists(GlobalPaths.AssetCacheDirTextures))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirTextures);
        }

        if (!Directory.Exists(GlobalPaths.AssetCacheDirTexturesGUI))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirTexturesGUI);
        }

        if (!Directory.Exists(GlobalPaths.AssetCacheDirScripts))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirScripts);
        }
        
        if (!Directory.Exists(GlobalPaths.AssetCacheDirAssets))
        {
            Directory.CreateDirectory(GlobalPaths.AssetCacheDirAssets);
        }
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                LogExceptions(ex);
#else
		    catch (Exception)
		    {
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
        try
        {
            if (!FileHasInvalidChars(path))
                return;

            string pathWithoutFilename = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            fileName = Regex.Replace(fileName, @"[^\w-.'_!()& ]", "");
            string finalPath = pathWithoutFilename + "\\" + fileName;

            FixedFileMove(path, finalPath, File.Exists(finalPath));
        }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
        catch (Exception ex)
        {
            LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
        }
    }

#if LAUNCHER || CMD || URI || BASICLAUNCHER
    public static void LogExceptions(Exception ex)
    {
        LogPrint("EXCEPTION|MESSAGE: " + (ex.Message != null ? ex.Message.ToString() : "N/A"), 2);
        LogPrint("EXCEPTION|STACK TRACE: " + (!string.IsNullOrWhiteSpace(ex.StackTrace) ? ex.StackTrace : "N/A"), 2);
        LogPrint("EXCEPTION|ADDITIONAL INFO: " + (ex != null ? ex.ToString() : "N/A"), 2);
    }
#endif

    //http://stevenhollidge.blogspot.com/2012/06/async-taskdelay.html
    public static Task Delay(int milliseconds)
    {
        var tcs = new TaskCompletionSource<object>();
        new System.Threading.Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
        return tcs.Task;
    }

#if LAUNCHER || URI
    public static void LaunchCharacterCustomization()
    {
        //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp
        FormCollection fc = Application.OpenForms;

        foreach (Form frm in fc)
        {
            //iterate through
            if (frm.Name == "CharacterCustomizationExtended" ||
                frm.Name == "CharacterCustomizationCompact")
            {
                frm.Close();
                break;
            }
        }

        switch (GlobalVars.UserConfiguration.LauncherStyle)
        {
            case Settings.Style.Extended:
                CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
                ccustom.Show();
                break;
            case Settings.Style.Compact:
                CharacterCustomizationCompact ccustom2 = new CharacterCustomizationCompact();
                ccustom2.Show();
                break;
            case Settings.Style.Stylish:
            default:
                CharacterCustomizationExtended ccustom3 = new CharacterCustomizationExtended();
                ccustom3.Show();
                break;
        }
    }
#endif

    //https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request

    private static string HttpGetInternal(string uri)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
    public static string HttpGet(string uri)
    {
        int tries = 0;
        int triesMax = 5;
        string exceptionMessage = "";

        while (tries < triesMax)
        {
            tries++;
            try
            {
                return HttpGetInternal(uri);
            }
            catch (Exception ex)
            {
#if URI || LAUNCHER || CMD || BASICLAUNCHER
                LogExceptions(ex);
#endif
                exceptionMessage = ex.Message;
                continue;
            }
        }

        return "ERROR: " + exceptionMessage;
    }

    public static void DrawBorderSimple(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style, int width)
    {
        //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        ControlPaint.DrawBorder(graphics, bounds,
            color, width, style,
            color, width, style,
            color, width, style,
            color, width, style);
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

    public static bool IsIPValid(string IP)
    {
        IPAddress address;
        if (IPAddress.TryParse(IP, out address))
        {
            switch (address.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:
                    return true;
                case System.Net.Sockets.AddressFamily.InterNetworkV6:
                default:
                    break;
            }
        }

        return false;
    }

    //converted from https://facreationz.wordpress.com/2014/12/11/c-know-if-running-under-wine/
    public static bool IsWineRunning()
    {
        string processName = "winlogon";
        var p = Process.GetProcessesByName(processName).Count();
        return (p <= 0);
    }

    public static string FixURLString(string str, string str2)
    {
        string fixedStr = str.ToLower().Replace("?version=1&amp;id=", "?id=")
                    .Replace("?version=1&id=", "?id=")
                    .Replace("&amp;", "&")
                    .Replace("amp;", "&");

        string baseurl = fixedStr.Before("/asset/?id=");

        if (baseurl == "")
        {
            baseurl = fixedStr.Before("/asset?id=");
            if (baseurl == "")
            {
                baseurl = fixedStr.Before("/item.aspx?id=");
            }
        }

        string fixedUrl = fixedStr.Replace(baseurl + "/asset/?id=", str2)
                    .Replace(baseurl + "/asset?id=", str2)
                    .Replace(baseurl + "/item.aspx?id=", str2);

        //...because scripts mess it up.
        string id = fixedUrl.After("id=");
        string fixedID = Regex.Replace(id, "[^0-9]", "");

        //really fucking hacky.
        string finalUrl = fixedUrl.Before("id=") + "id=" + fixedID;

        return finalUrl;
    }

    public static void CreateInitialFileListIfNeededMulti()
    {
        if (GlobalVars.NoFileList)
            return;

        string filePath = GlobalPaths.ConfigDir + "\\InitialFileList.txt";

        if (!File.Exists(filePath))
        {
            Thread t = new Thread(CreateInitialFileList);
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
                Thread t = new Thread(CreateInitialFileList);
                t.Start();
            }
        }
    }

    private static void CreateInitialFileList()
    {
        string filterPath = GlobalPaths.ConfigDir + @"\\" + GlobalPaths.InitialFileListIgnoreFilterName;
        string[] fileListToIgnore = File.ReadAllLines(filterPath);
        string FileName = GlobalPaths.ConfigDir + "\\InitialFileList.txt";

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
    }
}
#endregion
