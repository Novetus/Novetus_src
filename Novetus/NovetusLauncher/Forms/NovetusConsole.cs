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
        FileFormat.Config savedConfig;

        public NovetusConsole(string[] args)
        {
            ConsoleForm = new LauncherFormShared();
            argList = args;
            InitializeComponent();
        }

        private void NovetusConsole_Load(object sender, EventArgs e)
        {
            Util.ConsolePrint("Novetus version " + GlobalVars.ProgramInformation.Version + " loaded.", 4);
            Util.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 4);
            NovetusFuncs.SetupAdminPassword();

            if (argList.Length > 0)
            {
                //DO ARGS HERE
                ConsoleProcessArguments();
            }
        }

        private void ConsoleProcessArguments()
        {
            CommandLineArguments.Arguments ConsoleArgs = new CommandLineArguments.Arguments(argList);

            if (ConsoleArgs["help"] != null)
            {
                helpMode = true;
                ConsoleHelp();
            }

            if (ConsoleArgs["cmdonly"] != null && ConsoleArgs["cmdmode"] != null && !helpMode)
            {
                //cmd mode
                savedConfig = GlobalVars.UserConfiguration;
                disableCommands = true;
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
                        GlobalVars.UserConfiguration.SelectedClient = ConsoleArgs["client"];
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
                                    GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(ConsoleArgs["hostport"]);
                                    GlobalVars.Proxy.UpdateEndPoint();
                                }

                                if (ConsoleArgs["upnp"] != null)
                                {
                                    GlobalVars.UserConfiguration.UPnP = Convert.ToBoolean(ConsoleArgs["upnp"]);

                                    if (GlobalVars.UserConfiguration.UPnP)
                                    {
                                        Util.ConsolePrint("Novetus will now use UPnP for port forwarding.", 4);
                                    }
                                    else
                                    {
                                        Util.ConsolePrint("Novetus will not use UPnP for port forwarding. Make sure the port " + GlobalVars.UserConfiguration.RobloxPort + " is properly forwarded or you are running a LAN redirection tool.", 4);
                                    }
                                }

                                if (ConsoleArgs["notifications"] != null)
                                {
                                    GlobalVars.UserConfiguration.ShowServerNotifications = Convert.ToBoolean(ConsoleArgs["notifications"]);

                                    if (GlobalVars.UserConfiguration.ShowServerNotifications)
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
                                    GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(ConsoleArgs["maxplayers"]);
                                }

                                if (ConsoleArgs["serverbrowsername"] != null)
                                {
                                    GlobalVars.UserConfiguration.ServerBrowserServerName = ConsoleArgs["serverbrowsername"];
                                }

                                if (ConsoleArgs["serverbrowseraddress"] != null)
                                {
                                    GlobalVars.UserConfiguration.ServerBrowserServerAddress = ConsoleArgs["serverbrowseraddress"];
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
                            {
                                MapArg(ConsoleArgs);
                            }
                            break;
                        case ScriptType.EasterEgg:
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
                GlobalVars.UserConfiguration.Map = ConsoleArgs["map"];
                GlobalVars.UserConfiguration.MapPath = ConsoleArgs["map"];
                Util.ConsolePrint("Novetus will now launch the client with the map " + GlobalVars.UserConfiguration.MapPath, 4);
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
                    break;
                case string client when string.Compare(client, "client", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleForm.StartGame(ScriptType.Client);
                    break;
                case string solo when string.Compare(solo, "solo", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleForm.StartGame(ScriptType.Solo);
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
                    break;
                case string config when config.Contains("config", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = config.Split(' ');

                        if (vals[1].Equals("save", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.WriteConfigValues();
                        }
                        else if (vals[1].Equals("load", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.ReadConfigValues();
                        }
                        else if (vals[1].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ConsoleForm.ResetConfigValues();
                        }
                        else
                        {
                            Util.ConsolePrint("Please specify 'save', 'load', or 'reset'.", 2);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify 'save', 'load', or 'reset'.", 2);
                    }
                    break;
                case string help when string.Compare(help, "help", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleHelp();
                    break;
                case string documentation when string.Compare(documentation, "documentation", true, CultureInfo.InvariantCulture) == 0:
                    ClientScriptDoc();
                    break;
                case string sdk when string.Compare(sdk, "sdk", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleForm.LoadLauncher();
                    break;
                case string dlldelete when string.Compare(dlldelete, "dlldelete", true, CultureInfo.InvariantCulture) == 0:
                    if (GlobalVars.UserConfiguration.DisableReshadeDelete == true)
                    {
                        GlobalVars.UserConfiguration.DisableReshadeDelete = false;
                        Util.ConsolePrint("ReShade DLL deletion enabled.", 4);
                    }
                    else
                    {
                        GlobalVars.UserConfiguration.DisableReshadeDelete = true;
                        Util.ConsolePrint("ReShade DLL deletion disabled.", 4);
                    }
                    break;
                case string altip when altip.Contains("altip", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = altip.Split(' ');

                        if (vals[1].Equals("none", StringComparison.InvariantCultureIgnoreCase))
                        {
                            GlobalVars.UserConfiguration.AlternateServerIP = "";
                            Util.ConsolePrint("Alternate Server IP removed.", 4);
                        }
                        else
                        {
                            GlobalVars.UserConfiguration.AlternateServerIP = vals[1];
                            Util.ConsolePrint("Alternate Server IP set to " + GlobalVars.UserConfiguration.AlternateServerIP, 4);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify the IP address you would like to set Novetus to. Type 'none' to disable this.", 2);
                    }
                    break;
                case string clear when clear.Contains("clear", StringComparison.InvariantCultureIgnoreCase) == true:
                    ClearConsole();
                    break;
                case string important when string.Compare(important, GlobalVars.Important, true, CultureInfo.InvariantCulture) == 0:
                    GlobalVars.AdminMode = true;
                    Util.ConsolePrint("ADMIN MODE ENABLED.", 4);
                    Util.ConsolePrint("YOU ARE GOD.", 2);
                    break;
                case string decode when (string.Compare(decode, "decode", true, CultureInfo.InvariantCulture) == 0 || string.Compare(decode, "decrypt", true, CultureInfo.InvariantCulture) == 0):
                    Decoder de = new Decoder();
                    de.Show();
                    Util.ConsolePrint("???", 2);
                    break;
                case string proxy when proxy.Contains("proxy", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = proxy.Split(' ');

                        if (vals[1].Equals("on", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (GlobalVars.Proxy.HasStarted())
                            {
                                Util.ConsolePrint("The web proxy is already on and running.", 2);
                                return;
                            }

                            if (GlobalVars.UserConfiguration.WebProxyInitialSetupRequired)
                            {
                                // this is wierd and really dumb if we are just using console mode..... 
                                GlobalVars.Proxy.DoSetup();
                            }
                            else
                            {
                                if (!GlobalVars.UserConfiguration.WebProxyEnabled)
                                {
                                    GlobalVars.UserConfiguration.WebProxyEnabled = true;
                                }
                            }

                            GlobalVars.Proxy.Start();
                        }
                        else if (vals[1].Equals("off", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (!GlobalVars.Proxy.HasStarted())
                            {
                                Util.ConsolePrint("The web proxy is already turned off.", 2);
                                return;
                            }

                            if (!GlobalVars.UserConfiguration.WebProxyEnabled)
                            {
                                Util.ConsolePrint("The web proxy is disabled. Please turn it on in order to use this command.", 2);
                                return;
                            }

                            GlobalVars.Proxy.Stop();
                        }
                        else if (vals[1].Equals("disable", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (!GlobalVars.Proxy.HasStarted() && !GlobalVars.UserConfiguration.WebProxyEnabled)
                            {
                                Util.ConsolePrint("The web proxy is already disabled.", 2);
                                return;
                            }

                            if (GlobalVars.UserConfiguration.WebProxyEnabled)
                            {
                                GlobalVars.UserConfiguration.WebProxyEnabled = false;
                            }

                            if (GlobalVars.Proxy.HasStarted())
                            {
                                GlobalVars.Proxy.Stop();
                            }

                            Util.ConsolePrint("The web proxy has been disabled. To re-enable it, use the 'proxy on' command.", 2);
                        }
                        else
                        {
                            Util.ConsolePrint("Please specify 'on', 'off', or 'disable'.", 2);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify 'on' or 'off', or 'disable'.", 2);
                    }
                    break;
                default:
                    Util.ConsolePrint("Command is either not registered or valid", 2);
                    break;
            }
        }

        public void ConsoleHelp()
        {
            ClearConsole();
            Util.ConsolePrint("Help:", 3, true, true);
            Util.ReadTextFileWithColor(GlobalPaths.BasePath + "\\" + GlobalPaths.ConsoleHelpFileName);
            Util.ConsolePrint(GlobalVars.Important2, 0, true, true);
            ScrollToTop();
        }

        public void ClientScriptDoc()
        {
            ClearConsole();
            Util.ConsolePrint("ClientScript Documentation:", 3, true, true);
            Util.ReadTextFileWithColor(GlobalPaths.BasePath + "\\" + GlobalPaths.ClientScriptDocumentationFileName);
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

            //Command proxy

            int totalLines = ConsoleBox.Lines.Length;
            if (totalLines > 0)
            {
                string lastLine = ConsoleBox.Lines[totalLines - 1];

                if (e.KeyCode == Keys.Enter)
                {
                    ConsoleBox.AppendText(Environment.NewLine, Color.White);
                    ConsoleProcessCommands(lastLine);
                    e.Handled = true;
                }
            }

            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.X:
                    case Keys.Z:
                        e.Handled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearConsole()
        {
            ConsoleBox.Text = "";
            ConsoleBox.SelectionStart = 0;
            ConsoleBox.ScrollToCaret();
        }

        private void ScrollToTop()
        {
            ConsoleBox.SelectionStart = 0;
            ConsoleBox.ScrollToCaret();
        }

        private void ConsoleClose(object sender, FormClosingEventArgs e)
        {
            CommandLineArguments.Arguments ConsoleArgs = new CommandLineArguments.Arguments(argList);
            if (ConsoleArgs["cmdonly"] != null && ConsoleArgs["cmdmode"] != null && !helpMode)
            {
                GlobalVars.UserConfiguration = savedConfig;
            }
            ConsoleForm.CloseEventInternal();
        }
    }
}
