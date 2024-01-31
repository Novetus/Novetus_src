using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using Novetus.Core;

namespace NovetusLauncher
{
    public partial class NovetusConsole : Form
    {
        static LauncherFormShared ConsoleForm;
        static ScriptType loadMode = ScriptType.Server;
        bool helpMode = false;
        bool disableCommands = false;
        string[] argList;

        public NovetusConsole()
        {
            ConsoleForm = new LauncherFormShared();
            argList = LocalVars.cmdLineArray.ToArray();
            InitializeComponent();
        }

        //modified from https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
        public static string GenerateName(int len)
        {
            CryptoRandom r = new CryptoRandom();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        private void NovetusConsole_Load(object sender, EventArgs e)
        {
            Util.ConsolePrint("Novetus version " + GlobalVars.ProgramInformation.Version + " loaded.", 4);
            if (GlobalVars.ProgramInformation.IsSnapshot)
            {
                Util.ConsolePrint("Codename: " + GlobalVars.ProgramInformation.VersionName);
            }
            Util.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 4);
            CryptoRandom random = new CryptoRandom();
            string Name1 = GenerateName(random.Next(4, 12));
            string Name2 = GenerateName(random.Next(4, 12));
            GlobalVars.Important = Name1 + Name2;
            GlobalVars.Important2 = SecurityFuncs.Encipher(GlobalVars.Important, random.Next(2, 13));

            if (argList.Length > 0)
            {
                //DO ARGS HERE
                ConsoleProcessArguments();
            }
        }

        private void ConsoleProcessArguments()
        {
            CommandLineArguments.Arguments ConsoleArgs = new CommandLineArguments.Arguments(argList);

            if (ConsoleArgs["cmdonly"] != null)
            {
                if (ConsoleArgs["help"] != null)
                {
                    helpMode = true;
                    ConsoleHelp();
                    return;
                }

                //disableCommands = true;
                bool no3d = false;
                bool nomap = false;

                if (ConsoleArgs["headless"] != null)
                {
                    Visible = false;
                    ShowInTaskbar = false;
                    Opacity = 0;
                }

                if (ConsoleArgs["load"] != null)
                {
                    string error = "Load Mode '" + ConsoleArgs["load"] + "' is not available. Loading " + loadMode;

                    try
                    {
                        object check = Enum.Parse(typeof(ScriptType), ConsoleArgs["load"], true);

                        if (check == null || (ScriptType)check == ScriptType.None)
                        {
                            Util.ConsolePrint(error, 2);
                        }
                        else
                        {
                            loadMode = (ScriptType)check;
                            Util.ConsolePrint("Load Mode set to '" + loadMode + "'.", 3);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint(error, 2);
                    }

                    if (ConsoleArgs["client"] != null)
                    {
                        GlobalVars.UserConfiguration.SaveSetting("SelectedClient", ConsoleArgs["client"]);
                    }
                    else
                    {
                        Util.ConsolePrint("Novetus will launch the client defined in the INI file.", 4);
                    }

                    switch (loadMode)
                    {
                        case ScriptType.Client:
                            {
                                if (ConsoleArgs["join"] != null)
                                {
                                    GlobalVars.CurrentServer.SetValues(ConsoleArgs["join"]);
                                }
                            }
                            break;
                        case ScriptType.Server:
                            {
                                if (ConsoleArgs["no3d"] != null)
                                {
                                    no3d = true;
                                    Util.ConsolePrint("Novetus will now launch the server in No3D mode.", 4);
                                    Util.ConsolePrint("Launching the server without graphics enables better performance. " +
                                        "However, launching the server with no graphics may cause some elements in later clients may be disabled, such as Dialog boxes." +
                                        "This feature may also make your server unstable.", 5);
                                }

                                if (ConsoleArgs["hostport"] != null)
                                {
                                    GlobalVars.UserConfiguration.SaveSettingInt("RobloxPort", ConvertSafe.ToInt32Safe(ConsoleArgs["hostport"]));
                                }

                                if (ConsoleArgs["upnp"] != null)
                                {
                                    GlobalVars.UserConfiguration.SaveSettingBool("UPnP", ConvertSafe.ToBooleanSafe(ConsoleArgs["upnp"]));

                                    if (GlobalVars.UserConfiguration.ReadSettingBool("UPnP"))
                                    {
                                        Util.ConsolePrint("Novetus will now use UPnP for port forwarding.", 4);
                                    }
                                    else
                                    {
                                        Util.ConsolePrint("Novetus will not use UPnP for port forwarding. Make sure the port '" + GlobalVars.UserConfiguration.ReadSettingInt("RobloxPort") + "' is properly forwarded or you are running a LAN redirection tool.", 4);
                                    }
                                }

                                if (ConsoleArgs["notifications"] != null)
                                {
                                    GlobalVars.UserConfiguration.SaveSettingBool("ShowServerNotifications", ConvertSafe.ToBooleanSafe(ConsoleArgs["notifications"]));

                                    if (GlobalVars.UserConfiguration.ReadSettingBool("ShowServerNotifications"))
                                    {
                                        Util.ConsolePrint("Novetus will show notifications on player join/leave.", 4);
                                    }
                                    else
                                    {
                                        Util.ConsolePrint("Novetus will not show notifications on player join/leave.", 4);
                                    }
                                }

                                if (ConsoleArgs["maxplayers"] != null)
                                {
                                    GlobalVars.UserConfiguration.SaveSettingInt("PlayerLimit", ConvertSafe.ToInt32Safe(ConsoleArgs["maxplayers"]));
                                }

                                if (ConsoleArgs["serverbrowsername"] != null)
                                {
                                    GlobalVars.UserConfiguration.SaveSetting("ServerBrowserServerName", ConsoleArgs["serverbrowsername"]);
                                }

                                if (ConsoleArgs["serverbrowseraddress"] != null)
                                {
                                    GlobalVars.UserConfiguration.SaveSetting("ServerBrowserServerAddress", ConsoleArgs["serverbrowseraddress"]);
                                }

                                MapArg(ConsoleArgs);
                            }
                            break;
                        case ScriptType.Studio:
                            {
                                if (ConsoleArgs["nomap"] != null)
                                {
                                    nomap = true;
                                    Util.ConsolePrint("Novetus will now launch Studio with no map.", 4);
                                }
                                else
                                {
                                    MapArg(ConsoleArgs);
                                }
                            }
                            break;
                        case ScriptType.Solo:
                        default:
                            break;
                    }
                }

                ConsoleForm.StartGame(loadMode, no3d, nomap, true);
            }
        }

        public void MapArg (CommandLineArguments.Arguments ConsoleArgs)
        {
            if (ConsoleArgs["map"] != null)
            {
                GlobalVars.UserConfiguration.SaveSetting("Map", ConsoleArgs["map"]);
                GlobalVars.UserConfiguration.SaveSetting("MapPath", ConsoleArgs["map"]);
                Util.ConsolePrint("Novetus will now launch the client with the map " + GlobalVars.UserConfiguration.ReadSetting("MapPath"), 4);
            }
            else
            {
                Util.ConsolePrint("Novetus will launch the sclient with the map defined in the INI file.", 4);
            }
        }

        public void ConsoleProcessCommands(string cmd)
        {
            if (disableCommands)
                return;

            CommandBox.Text = "";

            Util.ConsolePrint("> " + cmd, 1, true);

            switch (cmd)
            {
                case string server when server.Contains("server", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = server.Split(' ');

                        if (vals[1].Equals("3d", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.StartGame(ScriptType.Server, false, false, true);
                        }
                        else if (vals[1].Equals("no3d", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.StartGame(ScriptType.Server, true, false, true);
                        }
                        else
                        {
                            ConsoleForm.StartGame(ScriptType.Server, false, false, true);
                        }
                    }
                    catch (Exception)
                    {
                        ConsoleForm.StartGame(ScriptType.Server, false, false, true);
                    }
                    ScrollToEnd();
                    break;
                case string client when string.Compare(client, "client", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleForm.StartGame(ScriptType.Client);
                    ScrollToEnd();
                    break;
                case string solo when string.Compare(solo, "solo", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleForm.StartGame(ScriptType.Solo);
                    ScrollToEnd();
                    break;
                case string studio when studio.Contains("studio", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = studio.Split(' ');

                        if (vals[1].Equals("map", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.StartGame(ScriptType.Studio, false, false, true);
                        }
                        else if (vals[1].Equals("nomap", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.StartGame(ScriptType.Studio, false, true, true);
                        }
                        else
                        {
                            ConsoleForm.StartGame(ScriptType.Studio, false, false, true);
                        }
                    }
                    catch (Exception)
                    {
                        ConsoleForm.StartGame(ScriptType.Studio, false, false, true);
                    }
                    ScrollToEnd();
                    break;
                case string config when config.Contains("config", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = config.Split(' ');

                        if (config.Contains("save", StringComparison.InvariantCultureIgnoreCase) || 
                            config.Contains("savesetting", StringComparison.InvariantCultureIgnoreCase) || 
                            config.Contains("saveval", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string name = vals[2];
                            string value = vals[3];
                            GlobalVars.UserConfiguration.SaveSetting(name, value);
                            Util.ConsolePrint("Value of '" + name + "': " + GlobalVars.UserConfiguration.ReadSetting(name), 4);
                            Util.ConsolePrint("Please restart any applications related to this value to reload it.", 4);
                        }
                        else if (config.Contains("read", StringComparison.InvariantCultureIgnoreCase) ||
                            config.Contains("readsetting", StringComparison.InvariantCultureIgnoreCase) ||
                            config.Contains("readval", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string name = vals[2];
                            Util.ConsolePrint("Value of '" + name + "': " + GlobalVars.UserConfiguration.ReadSetting(name), 4);
                        }
                        else
                        {
                            if (vals[1].Equals("load", StringComparison.InvariantCultureIgnoreCase))
                            {
                                ConsoleForm.ReadConfigValues();
                            }
                            else if (vals[1].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                            {
                                ConsoleForm.ResetConfigValues();
                            }
                            else
                            {
                                Util.ConsolePrint("Please specify 'save', 'read', 'load', or 'reset'.", 2);
                                Util.ConsolePrint("If specifying 'save', please provide the name and value for the setting you wish to save.", 2);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify 'save', 'read', 'load', or 'reset'.", 2);
                        Util.ConsolePrint("If specifying 'save', please provide the name and value for the setting you wish to save.", 2);
                    }
                    ScrollToEnd();
                    break;
                case string outfit when outfit.Contains("outfit", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = outfit.Split(' ');

                        if (outfit.Contains("save", StringComparison.InvariantCultureIgnoreCase) ||
                            outfit.Contains("savesetting", StringComparison.InvariantCultureIgnoreCase) ||
                            outfit.Contains("saveval", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string name = vals[2];
                            string value = vals[3];
                            GlobalVars.UserCustomization.SaveSetting(name, value);
                            Util.ConsolePrint("Value of '" + name + "': " + GlobalVars.UserConfiguration.ReadSetting(name), 4);
                            Util.ConsolePrint("Please restart any applications related to this value to reload it.", 4);
                        }
                        else if (outfit.Contains("read", StringComparison.InvariantCultureIgnoreCase) ||
                            outfit.Contains("readsetting", StringComparison.InvariantCultureIgnoreCase) ||
                            outfit.Contains("readval", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string name = vals[2];
                            Util.ConsolePrint("Value of '" + name + "': " + GlobalVars.UserCustomization.ReadSetting(name), 4);
                        }
                        else
                        {
                            if (vals[1].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                            {
                                FileManagement.ResetCustomizationValues();
                            }
                            else
                            {
                                Util.ConsolePrint("Please specify 'save', 'read', 'load', or 'reset'.", 2);
                                Util.ConsolePrint("If specifying 'save', please provide the name and value for the setting you wish to save.", 2);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify 'save', 'read', 'load', or 'reset'.", 2);
                        Util.ConsolePrint("If specifying 'save', please provide the name and value for the setting you wish to save.", 2);
                    }
                    ScrollToEnd();
                    break;
                case string help when string.Compare(help, "help", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleHelp();
                    break;
                case string documentation when string.Compare(documentation, "documentation", true, CultureInfo.InvariantCulture) == 0:
                    ClientScriptDoc();
                    ScrollToEnd();
                    break;
                case string sdk when string.Compare(sdk, "sdk", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleForm.LoadLauncher();
                    ScrollToEnd();
                    break;
                case string altip when altip.Contains("altip", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = altip.Split(' ');

                        if (vals[1].Equals("none", StringComparison.InvariantCultureIgnoreCase))
                        {
                            GlobalVars.UserConfiguration.SaveSetting("AlternateServerIP");
                            Util.ConsolePrint("Alternate Server IP removed.", 4);
                        }
                        else
                        {
                            GlobalVars.UserConfiguration.SaveSetting("AlternateServerIP", vals[1]);
                            Util.ConsolePrint("Alternate Server IP set to " + GlobalVars.UserConfiguration.ReadSetting("AlternateServerIP"), 4);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify the IP address you would like to set Novetus to. Type 'none' to disable this.", 2);
                    }
                    ScrollToEnd();
                    break;
                case string clear when clear.Contains("clear", StringComparison.InvariantCultureIgnoreCase) == true:
                    ClearConsole();
                    break;
                case string important when string.Compare(important, GlobalVars.Important, true, CultureInfo.InvariantCulture) == 0:
                    GlobalVars.AdminMode = true;
                    Util.ConsolePrint("ADMIN MODE ENABLED.", 4);
                    Util.ConsolePrint("YOU ARE GOD.", 2);
                    ScrollToEnd();
                    break;
                case string decode when (string.Compare(decode, "decode", true, CultureInfo.InvariantCulture) == 0 || string.Compare(decode, "decrypt", true, CultureInfo.InvariantCulture) == 0):
                    Util.ConsolePrint("???", 2);
                    Decoder de = new Decoder();
                    de.Show();
                    ScrollToEnd();
                    break;
                case string proxy when proxy.Contains("proxy", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = proxy.Split(' ');

                        if (vals[1].Equals("on", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.TurnProxyOn();
                        }
                        else if (vals[1].Equals("off", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.TurnProxyOff();
                        }
                        else if (vals[1].Equals("disable", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.DisableProxy();
                        }
                        else if (vals[1].Equals("extensions", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (vals[2].Equals("reload", StringComparison.InvariantCultureIgnoreCase))
                            {
                                ConsoleForm.ProxyExtensions(0);
                            }
                            else if (vals[2].Equals("list", StringComparison.InvariantCultureIgnoreCase))
                            {
                                ConsoleForm.ProxyExtensions(1);
                            }
                        }
                        else
                        {
                            Util.ConsolePrint("Please specify 'on', 'off', or 'disable'.", 2);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify 'on', 'off', or 'disable'.", 2);
                    }
                    ScrollToEnd();
                    break;
                case string cmdArgs when cmdArgs.Contains("commandline", StringComparison.InvariantCultureIgnoreCase) == true:
                    Util.ConsolePrint(LocalVars.cmdLineString, 3);
                    break;
                case string depend when (string.Compare(depend, "dependencies", true, CultureInfo.InvariantCulture) == 0):
                    ConsoleForm.CheckDependencies();
                    break;
                default:
                    Util.ConsolePrint("Command is either not registered or valid", 2);
                    ScrollToEnd();
                    break;
            }
        }

        public void ConsoleHelp()
        {
            ClearConsole();
            Util.ConsolePrint("Help:", 3, true, false);
            Util.ReadTextFileWithColor(GlobalPaths.MiscDir + "\\" + GlobalPaths.ConsoleHelpFileName, false);
            Util.ConsolePrint(GlobalVars.Important2, 0, true, false);
            ScrollToTop();
        }

        public void ClientScriptDoc()
        {
            ClearConsole();
            Util.ConsolePrint("ClientScript Documentation:", 3, true);
            Util.ReadTextFileWithColor(GlobalPaths.MiscDir + "\\" + GlobalPaths.ClientScriptDocumentationFileName, false);
            ScrollToTop();
        }

        private void ProcessConsole(object sender, KeyEventArgs e)
        {
            if (helpMode)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    ConsoleForm.CloseEventInternal();
                }
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                ConsoleProcessCommands(CommandBox.Text);
                e.Handled = true;
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (helpMode)
            {
                ConsoleForm.CloseEventInternal();
                return;
            }

            ConsoleProcessCommands(CommandBox.Text);
        }

        private void ClearConsole()
        {
            ConsoleBox.Text = "";
            ScrollToTop();
        }

        private void ScrollToTop()
        {
            ConsoleBox.SelectionStart = 0;
            ConsoleBox.ScrollToCaret();
        }

        private void ScrollToEnd()
        {
            ConsoleBox.SelectionStart = ConsoleBox.Text.Length;
            ConsoleBox.ScrollToCaret();
        }

        private void ConsoleClose(object sender, FormClosingEventArgs e)
        {
            CommandLineArguments.Arguments ConsoleArgs = new CommandLineArguments.Arguments(argList);

            ConsoleForm.CloseEvent(e);
        }
    }
}
