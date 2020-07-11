#region Usings
using System;
using Mono.Nat;
using System.Diagnostics;
using System.IO;
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
                    GlobalFuncs.ConsolePrint("UPnP: Unable to initialize NetFuncs. Reason - " + ex.Message, 2);
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
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " opened on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
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
                    GlobalFuncs.ConsolePrint("UPnP: Port " + port + " closed on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		private static void DeviceFound(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
                GlobalFuncs.ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' registered.", 3);
				StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Udp, GlobalVars.WebServerPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.WebServerPort);
			}
			catch (Exception ex)
            {
                GlobalFuncs.ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2);
			}
		}
 
		private static void DeviceLost(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
                GlobalFuncs.ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' disconnected.", 3);
 				StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Udp, GlobalVars.WebServerPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.WebServerPort);
 			}
			catch (Exception ex)
            {
                GlobalFuncs.ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2);
			}
		}
        #endregion

        #region Web Server
        static void StartWebServer()
        {
        	if (SecurityFuncs.IsElevated)
			{
				try
      			{
     				GlobalVars.WebServer = new SimpleHTTPServer(GlobalPaths.ServerDir, GlobalVars.WebServerPort);
                    GlobalFuncs.ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3);
      			}
      			catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
                GlobalFuncs.ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }
        
        static void StopWebServer()
        {
        	if (SecurityFuncs.IsElevated)
			{
				try
      			{
                    GlobalFuncs.ConsolePrint("WebServer: Server has stopped on port: " + GlobalVars.WebServer.Port.ToString(), 2);
        			GlobalVars.WebServer.Stop();
      			}
      			catch (Exception ex)
                {
                    GlobalFuncs.ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
                GlobalFuncs.ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }
        #endregion

        #region Loading/Saving files
        static void WriteConfigValues()
		{
			GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            GlobalFuncs.ConsolePrint("Config Saved.", 3);
		}

        static void ReadConfigValues()
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
            GlobalFuncs.ConsolePrint("Config loaded.", 3);
            GlobalFuncs.ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
        }
        #endregion

        #region Main Program Function
		public static void Main(string[] args)
		{
            GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, true);
            Console.Title = "Novetus " + GlobalVars.ProgramInformation.Version + " CMD";

            GlobalFuncs.ConsolePrint("NovetusCMD version " + GlobalVars.ProgramInformation.Version + " loaded.", 1);
            GlobalFuncs.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 1);

            if (args.Length == 0)
            {
                GlobalFuncs.ConsolePrint("Help: Command Line Arguments", 3);
                GlobalFuncs.ConsolePrint("---------", 1);
                GlobalFuncs.ConsolePrint("General", 3);
                GlobalFuncs.ConsolePrint("-no3d | Launches server in NoGraphics mode", 4);
                GlobalFuncs.ConsolePrint("-script <path to script> | Loads an additional server script.", 4);
                GlobalFuncs.ConsolePrint("-outputinfo | Outputs all information about the running server to a text file.", 4);
                GlobalFuncs.ConsolePrint("-overrideconfig | Override the launcher settings.", 4);
                GlobalFuncs.ConsolePrint("-debug | Disables launching of the server for debugging purposes.", 4);
                GlobalFuncs.ConsolePrint("-nowebserver | Disables launching of the web server.", 4);
                GlobalFuncs.ConsolePrint("---------", 1);
                GlobalFuncs.ConsolePrint("Custom server options", 3);
                GlobalFuncs.ConsolePrint("-overrideconfig must be added in order for the below commands to function.", 5);
                GlobalFuncs.ConsolePrint("-upnp | Turns on NetFuncs.", 4);
                GlobalFuncs.ConsolePrint("-map <map filename> | Sets the map.", 4);
                GlobalFuncs.ConsolePrint("-client <client name> | Sets the client.", 4);
                GlobalFuncs.ConsolePrint("-port <port number> | Sets the server port.", 4);
                GlobalFuncs.ConsolePrint("-maxplayers <number of players> | Sets the number of players.", 4);
                GlobalFuncs.ConsolePrint("---------", 1);
            }
            else
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["no3d"] != null)
                {
                    LocalVars.StartInNo3D = true;
                    GlobalFuncs.ConsolePrint("NovetusCMD will now launch the server in No3D mode.", 4);
                }

                if (CommandLine["overrideconfig"] != null)
                {
                    LocalVars.OverrideINI = true;
                    GlobalFuncs.ConsolePrint("NovetusCMD will no longer grab values from the INI file.", 4);

                    if (CommandLine["upnp"] != null)
                    {
                        GlobalVars.UserConfiguration.UPnP = true;
                        GlobalFuncs.ConsolePrint("NovetusCMD will now use UPnP for port forwarding.", 4);
                    }

                    if (CommandLine["map"] != null)
                    {
                        GlobalVars.UserConfiguration.MapPath = CommandLine["map"];
                    }
                    else
                    {
                        GlobalFuncs.ConsolePrint("NovetusCMD will launch the server with the default map.", 4);
                    }

                    if (CommandLine["client"] != null)
                    {
                        GlobalVars.UserConfiguration.SelectedClient = CommandLine["client"];
                    }
                    else
                    {
                        GlobalFuncs.ConsolePrint("NovetusCMD will launch the server with the default client.", 4);
                    }

                    if (CommandLine["port"] != null)
                    {
                        GlobalVars.UserConfiguration.RobloxPort = Convert.ToInt32(CommandLine["port"]);
                    }

                    if (CommandLine["maxplayers"] != null)
                    {
                        GlobalVars.UserConfiguration.PlayerLimit = Convert.ToInt32(CommandLine["maxplayers"]);
                    }
                }

                if (CommandLine["outputinfo"] != null)
                {
                    GlobalVars.RequestToOutputInfo = true;
                }

                if (CommandLine["debug"] != null)
                {
                    LocalVars.DebugMode = true;
                }

                if (CommandLine["nowebserver"] != null)
                {
                    LocalVars.NoWebServer = true;
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

            if (!LocalVars.OverrideINI)
            {
                GlobalFuncs.ConsolePrint("NovetusCMD is now loading all server configurations from the INI file.", 5);

                if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName))
                {
                    GlobalFuncs.ConsolePrint("WARNING 2 - " + GlobalPaths.ConfigName + " not found. Creating one with default values.", 5);
                    WriteConfigValues();
                }

                ReadConfigValues();
            }
            else
            {
                GlobalFuncs.ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
            }

    		InitUPnP();

            if (!LocalVars.NoWebServer)
            {
                StartWebServer();
            }
    		
    		AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramClose);

            GlobalFuncs.ConsolePrint("Launching a " + GlobalVars.UserConfiguration.SelectedClient + " server on " + GlobalVars.UserConfiguration.Map + " with " + GlobalVars.UserConfiguration.PlayerLimit + " players.", 1);

            if (!LocalVars.DebugMode)
            {
                StartServer(LocalVars.StartInNo3D);
            }
            else
            {
                GlobalFuncs.CreateTXT();
            }
			Console.ReadKey();
		}

        static void ProgramClose(object sender, EventArgs e)
        {
            WriteConfigValues();
            if (GlobalVars.IsWebServerOn)
            {
                StopWebServer();
            }
            if (GlobalVars.ProcessID != 0)
            {
                if (LocalFuncs.ProcessExists(GlobalVars.ProcessID))
                {
                    Process proc = Process.GetProcessById(GlobalVars.ProcessID);
                    proc.Kill();
                }
            }
        }
        #endregion

        #region Client Loading (TODO MAKE THIS METHOD GLOBAL)
        static void StartServer(bool no3d)
		{
            GlobalFuncs.LaunchRBXClient(ScriptType.Server, no3d, false, ServerExited);
		}

        static void ServerExited(object sender, EventArgs e)
		{
            Environment.Exit(0);
		}
        #endregion
    }
    #endregion
}