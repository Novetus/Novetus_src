#region Usings
using System;
using Mono.Nat;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NLog;
using System.Threading;
#endregion

namespace NovetusCMD
{
    #region Novetus CMD Main Class
    public static class NovetusCMD
	{
        #region UPnP
        public static void InitUPnP()
		{
			if (GlobalVars.UserConfiguration.UPnP)
			{
				try
				{
					NetFuncs.InitUPnP(DeviceFound,DeviceLost);
                    GlobalFuncs.ConsolePrint("UPnP: Service initialized", 3);
				}
				catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to initialize UPnP. Reason - " + ex.Message, 2);
                    GlobalFuncs.LogExceptions(ex);
				}
			}
		}
		
		public static void StartUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UserConfiguration.UPnP)
			{
				try
				{
					NetFuncs.StartUPnP(device,protocol,port);
                    string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " opened on '" + IP + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
                    GlobalFuncs.LogExceptions(ex);
                }
			}
		}
		
		public static void StopUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UserConfiguration.UPnP)
			{
				try
				{
					NetFuncs.StopUPnP(device,protocol,port);
                    string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " closed on '" + IP + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2);
                    GlobalFuncs.LogExceptions(ex);
                }
			}
		}
		
		private static void DeviceFound(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
                string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                GlobalFuncs.ConsolePrint("UPnP: Device '" + IP + "' registered.", 3);
				StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
			}
			catch (Exception ex)
            {
                GlobalFuncs.ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2);
                GlobalFuncs.LogExceptions(ex);
            }
		}
 
		private static void DeviceLost(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
                string IP = !string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : device.GetExternalIP().ToString();
                GlobalFuncs.ConsolePrint("UPnP: Device '" + IP + "' disconnected.", 3);
 				StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
 			}
			catch (Exception ex)
            {
                GlobalFuncs.ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2);
                GlobalFuncs.LogExceptions(ex);
            }
		}
        #endregion

        #region Loading/Saving files
        static void WriteConfigValues()
		{
			GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            GlobalFuncs.ReadClientValues();
            GlobalFuncs.ConsolePrint("Config Saved.", 3);
		}

        static void ReadConfigValues(bool initial = false)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
            GlobalFuncs.ConsolePrint("Config loaded.", 3);
            GlobalFuncs.ReadClientValues(initial);
        }
        #endregion

        #region Main Program Function
		public static void Main(string[] args)
		{
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = GlobalPaths.ConfigDir + "\\CMD-log-" + DateTime.Today.ToString("MM-dd-yyyy") + ".log" };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;

            LoadCMDArgs(args);

            if (!LocalVars.PrintHelp)
            {
                GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, true,
                    GlobalPaths.RootPathLauncher + "\\Novetus.exe");
                Console.Title = "Novetus " + GlobalVars.ProgramInformation.Version + " CMD";

                GlobalFuncs.ConsolePrint("NovetusCMD version " + GlobalVars.ProgramInformation.Version + " loaded.", 1);
                GlobalFuncs.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 1);

                GlobalFuncs.ConsolePrint("NovetusCMD is now loading main server configurations from the INI file.", 5);

                if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName))
                {
                    GlobalFuncs.ConsolePrint("WARNING 2 - " + GlobalPaths.ConfigName + " not found. Creating one with default values.", 5);
                    WriteConfigValues();
                }

                ReadConfigValues(true);
                LoadOverrideINIArgs(args);
                InitUPnP();

                AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramClose);

                GlobalFuncs.ConsolePrint("Launching a " + GlobalVars.UserConfiguration.SelectedClient + " server on " + GlobalVars.UserConfiguration.Map + " with " + GlobalVars.UserConfiguration.PlayerLimit + " players.", 1);

                switch (LocalVars.DebugMode)
                {
                    case true:
                        GlobalFuncs.CreateTXT();
                        break;
                    case false:
                    default:
                        StartServer(LocalVars.StartInNo3D);
                        break;
                }
            }
            else
            {
                LocalFuncs.CommandInfo();
            }

            Console.ReadKey();
        }

        static void ProgramClose(object sender, EventArgs e)
        {
            if (GlobalVars.ProcessID != 0)
            {
                if (LocalFuncs.ProcessExists(GlobalVars.ProcessID))
                {
                    Process proc = Process.GetProcessById(GlobalVars.ProcessID);
                    proc.Kill();
                }
            }

            if (!LocalVars.OverrideINI)
            {
                WriteConfigValues();
            }
            Application.Exit();
        }

        static void LoadCMDArgs(string[] args)
        {
            if (args.Length > 0)
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["help"] != null)
                {
                    LocalVars.PrintHelp = true;
                }

                if (CommandLine["no3d"] != null)
                {
                    LocalVars.StartInNo3D = true;
                    GlobalFuncs.ConsolePrint("NovetusCMD will now launch the server in No3D mode.", 4);
                }

                if (CommandLine["outputinfo"] != null)
                {
                    GlobalVars.RequestToOutputInfo = true;
                }

                if (CommandLine["debug"] != null)
                {
                    LocalVars.DebugMode = true;
                }

                if (CommandLine["script"] != null)
                {
                    if (CommandLine["script"].Contains("rbxasset:") || CommandLine["script"].Contains("http:"))
                    {
                        GlobalPaths.AddonScriptPath = CommandLine["script"].Replace(@"\", @"\\");
                        GlobalFuncs.ConsolePrint("NovetusCMD detected a custom script. Loading " + GlobalPaths.AddonScriptPath, 4);
                    }
                    else
                    {
                        GlobalFuncs.ConsolePrint("NovetusCMD cannot load '" + CommandLine["script"] + "' as it doesn't use a rbxasset path or URL.", 2);
                    }
                }
            }
        }

        static void LoadOverrideINIArgs(string[] args)
        {
            if (args.Length > 0)
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["upnp"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.UPnP = true;
                    GlobalFuncs.ConsolePrint("NovetusCMD will now use UPnP for port forwarding.", 4);
                }

                if (CommandLine["notifications"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.ShowServerNotifications = Convert.ToBoolean(CommandLine["notifications"]);

                    if (GlobalVars.UserConfiguration.ShowServerNotifications)
                    {
                        GlobalFuncs.ConsolePrint("NovetusCMD will show notifications on player join/leave.", 4);
                    }
                    else
                    {
                        GlobalFuncs.ConsolePrint("NovetusCMD will no longer show notifications on player join/leave.", 4);
                    }
                }

                if (CommandLine["map"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.Map = CommandLine["map"];
                    GlobalVars.UserConfiguration.MapPath = CommandLine["map"];
                    GlobalFuncs.ConsolePrint("NovetusCMD will now launch the server with the map " + GlobalVars.UserConfiguration.MapPath, 4);
                }
                else
                {
                    GlobalFuncs.ConsolePrint("NovetusCMD will launch the server with the default map.", 4);
                }

                if (CommandLine["client"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.SelectedClient = CommandLine["client"];
                }
                else
                {
                    GlobalFuncs.ConsolePrint("NovetusCMD will launch the server with the default client.", 4);
                }

                if (CommandLine["port"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(CommandLine["port"]);
                }

                if (CommandLine["maxplayers"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(CommandLine["maxplayers"]);
                }

                if (CommandLine["serverbrowsername"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.ServerBrowserServerName = CommandLine["serverbrowsername"];
                }

                if (CommandLine["serverbrowseraddress"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalVars.UserConfiguration.ServerBrowserServerAddress = CommandLine["serverbrowseraddress"];
                }
            }
        }
        #endregion

        #region Client Loading
        static void StartServer(bool no3d)
		{
            GlobalFuncs.LaunchRBXClient(ScriptType.Server, no3d, false, new EventHandler(ServerExited));
		}

        static void ServerExited(object sender, EventArgs e)
		{
            GlobalVars.GameOpened = ScriptType.None;
            GlobalFuncs.PingMasterServer(0, "The server has removed itself from the master server list.");
            Environment.Exit(0);
		}
        #endregion
    }
    #endregion
}