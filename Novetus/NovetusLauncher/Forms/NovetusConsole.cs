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

namespace NovetusLauncher
{
    public partial class NovetusConsole : Form
    {
        static LauncherFormShared ConsoleForm;
        bool helpMode = false;
        string[] argList;

        public NovetusConsole(string[] args)
        {
            ConsoleForm = new LauncherFormShared();
            argList = args;
            InitializeComponent();
        }

        private void NovetusConsole_Load(object sender, EventArgs e)
        {
            FileManagement.CreateInitialFileListIfNeededMulti();
            Util.ConsolePrint("Novetus version " + GlobalVars.ProgramInformation.Version + " loaded. Initializing config.", 4);
            Util.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 4);
            if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName))
            {
                Util.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName + " not found. Creating one with default values.", 5);
                ConsoleForm.WriteConfigValues();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization))
            {
                Util.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigNameCustomization + " not found. Creating one with default values.", 5);
                ConsoleForm.WriteCustomizationValues();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\servers.txt"))
            {
                Util.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\servers.txt not found. Creating empty file.", 5);
                File.Create(GlobalPaths.ConfigDir + "\\servers.txt").Dispose();
            }
            if (!File.Exists(GlobalPaths.ConfigDir + "\\ports.txt"))
            {
                Util.ConsolePrint("WARNING - " + GlobalPaths.ConfigDir + "\\ports.txt not found. Creating empty file.", 5);
                File.Create(GlobalPaths.ConfigDir + "\\ports.txt").Dispose();
            }

            FileManagement.CreateAssetCacheDirectories();
            NovetusFuncs.SetupAdminPassword();

            Util.InitUPnP();
            Util.StartDiscord();

            if (argList.Length > 0)
            {
                //DO ARGS HERE
                ConsoleProcessArguments(argList);
            }
        }

        private void ConsoleProcessArguments(string[] args)
        {
            CommandLineArguments.Arguments ConsoleArgs = new CommandLineArguments.Arguments(args);

            if (ConsoleArgs["help"] != null)
            {
                helpMode = true;
                ConsoleHelp();
            }

            bool CMDMode = (ConsoleArgs["cmd"] != null && !helpMode);

            if (CMDMode)
            {
                //cmd mode
            }
        }

        public void ConsoleProcessCommands(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd))
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
                            Util.ConsolePrint("Please specify 'save', 'load', or 'reset'.", 4);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify 'save', 'load', or 'reset'.", 4);
                    }
                    break;
                case string help when string.Compare(help, "help", true, CultureInfo.InvariantCulture) == 0:
                    ConsoleHelp();
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
                case string altserverip when altserverip.Contains("altserverip", StringComparison.InvariantCultureIgnoreCase) == true:
                    try
                    {
                        string[] vals = altserverip.Split(' ');

                        if (vals[1].Equals("none", StringComparison.InvariantCultureIgnoreCase))
                        {
                            GlobalVars.UserConfiguration.AlternateServerIP = "";
                            Util.ConsolePrint("Alternate Server IP removed.", 4);
                        }
                        else
                        {
                            GlobalVars.UserConfiguration.AlternateServerIP = vals[1];
                            Util.ConsolePrint("Alternate Server IP set to " + vals[1], 4);
                        }
                    }
                    catch (Exception)
                    {
                        Util.ConsolePrint("Please specify the IP address you would like to set Novetus to.", 2);
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
                default:
                    Util.ConsolePrint("Command is either not registered or valid", 2);
                    break;
            }
        }

        public void ConsoleHelp()
        {
            ClearConsole();
            Util.ConsolePrint("Help:", 3, true);
            Util.ConsolePrint("---------", 1, true);
            Util.ConsolePrint("Commands:", 3, true);
            Util.ConsolePrint("---------", 1, true);
            Util.ConsolePrint("+ client | Launches client with launcher settings", 4, true);
            Util.ConsolePrint("+ solo | Launches client in Play Solo mode with launcher settings", 4, true);
            Util.ConsolePrint("+ server 3d | Launches server with launcher settings", 4, true);
            Util.ConsolePrint("+ server no3d | Launches server in NoGraphics mode with launcher settings", 4, true);
            Util.ConsolePrint("+ studio map | Launches Roblox Studio with the selected map", 4, true);
            Util.ConsolePrint("+ studio nomap | Launches Roblox Studio without the selected map", 4, true);
            Util.ConsolePrint("+ sdk | Launches the Novetus SDK Launcher", 4, true);
            Util.ConsolePrint("+ dlldelete | Toggle the deletion of opengl32.dll when ReShade is off.", 4, true);
            Util.ConsolePrint("+ altserverip <IP> | Sets the alternate server IP for server info. Replace <IP> with your specified IP or specify 'none' to remove the current alternate server IP", 4, true);
            Util.ConsolePrint("+ clear | Clears all text in this window.", 4, true);
            Util.ConsolePrint("+ help | Clears all text and shows this list.", 4, true);
            Util.ConsolePrint("---------", 1, true);
            Util.ConsolePrint("Command-Line Parameters:", 3, true);
            Util.ConsolePrint("GLOBAL - Affects launcher session.", 5, true);
            Util.ConsolePrint("CONSOLE - Affects console only.", 5, true);
            Util.ConsolePrint("---------", 1, true);
            Util.ConsolePrint("- sdk (GLOBAL) | Launches the Novetus SDK Launcher.", 4, true);
            Util.ConsolePrint("- cmd (GLOBAL) | Launches the Novetus Console only and puts it into NovetusCMD mode.", 4, true);
            Util.ConsolePrint("- nofilelist (GLOBAL) | Disables file list generation", 4, true);
            Util.ConsolePrint("- help (CONSOLE) | Clears all text and shows this list.", 4, true);
            Util.ConsolePrint("---------", 1, true);
            Util.ConsolePrint("= config save | Saves the config file", 4, true);
            Util.ConsolePrint("= config load | Reloads the config file", 4, true);
            Util.ConsolePrint("= config reset | Resets the config file", 4, true);
            Util.ConsolePrint(GlobalVars.Important2, 0, true, true);
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
            ConsoleForm.CloseEventInternal();
        }
    }
}
