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
                    LocalFuncs.ConsolePrint("UPnP: Service initialized", 3);
				}
				catch (Exception ex)
                {
                    LocalFuncs.ConsolePrint("UPnP: Unable to initialize NetFuncs. Reason - " + ex.Message, 2);
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
                    LocalFuncs.ConsolePrint("UPnP: Port " + port + " opened on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
                    LocalFuncs.ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
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
                    LocalFuncs.ConsolePrint("UPnP: Port " + port + " closed on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex)
                {
                    LocalFuncs.ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		private static void DeviceFound(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
                LocalFuncs.ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' registered.", 3);
				StartUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
				StartUPnP(device, Protocol.Udp, GlobalVars.WebServerPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.WebServerPort);
			}
			catch (Exception ex)
            {
                LocalFuncs.ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2);
			}
		}
 
		private static void DeviceLost(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
                LocalFuncs.ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' disconnected.", 3);
 				StopUPnP(device, Protocol.Udp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.UserConfiguration.RobloxPort);
				StopUPnP(device, Protocol.Udp, GlobalVars.WebServerPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.WebServerPort);
 			}
			catch (Exception ex)
            {
                LocalFuncs.ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2);
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
                    LocalFuncs.ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3);
      			}
      			catch (Exception ex)
                {
                    LocalFuncs.ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
                LocalFuncs.ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }
        
        static void StopWebServer()
        {
        	if (SecurityFuncs.IsElevated)
			{
				try
      			{
                    LocalFuncs.ConsolePrint("WebServer: Server has stopped on port: " + GlobalVars.WebServer.Port.ToString(), 2);
        			GlobalVars.WebServer.Stop();
      			}
      			catch (Exception ex)
                {
                    LocalFuncs.ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
                LocalFuncs.ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }
        #endregion

        #region Loading/Saving files
        static void WriteConfigValues()
		{
			GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            LocalFuncs.ConsolePrint("Config Saved.", 3);
		}

        static void ReadConfigValues()
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
            LocalFuncs.ConsolePrint("Config loaded.", 3);
            ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
        }

        static void ReadClientValues(string ClientName)
        {
            string clientpath = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\clientinfo.nov";

            if (!File.Exists(clientpath))
            {
                LocalFuncs.ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
                GlobalVars.UserConfiguration.SelectedClient = GlobalVars.ProgramInformation.DefaultClient;
                ReadClientValues(ClientName);
            }
            else
            {
                GlobalFuncs.ReadClientValues(clientpath);
                LocalFuncs.ConsolePrint("Client '" + GlobalVars.UserConfiguration.SelectedClient + "' successfully loaded.", 3);
            }
        }
        #endregion

        #region Main Program Function
		public static void Main(string[] args)
		{
            GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, true);
            Console.Title = "Novetus " + GlobalVars.ProgramInformation.Version + " CMD";

            LocalFuncs.ConsolePrint("NovetusCMD version " + GlobalVars.ProgramInformation.Version + " loaded.", 1);
            LocalFuncs.ConsolePrint("Novetus path: " + GlobalPaths.BasePath, 1);

            if (args.Length == 0)
            {
                LocalFuncs.ConsolePrint("Help: Command Line Arguments", 3);
                LocalFuncs.ConsolePrint("---------", 1);
                LocalFuncs.ConsolePrint("General", 3);
                LocalFuncs.ConsolePrint("-no3d | Launches server in NoGraphics mode", 4);
                LocalFuncs.ConsolePrint("-script <path to script> | Loads an additional server script.", 4);
                LocalFuncs.ConsolePrint("-outputinfo | Outputs all information about the running server to a text file.", 4);
                LocalFuncs.ConsolePrint("-overrideconfig | Override the launcher settings.", 4);
                LocalFuncs.ConsolePrint("-debug | Disables launching of the server for debugging purposes.", 4);
                LocalFuncs.ConsolePrint("-nowebserver | Disables launching of the web server.", 4);
                LocalFuncs.ConsolePrint("---------", 1);
                LocalFuncs.ConsolePrint("Custom server options", 3);
                LocalFuncs.ConsolePrint("-overrideconfig must be added in order for the below commands to function.", 5);
                LocalFuncs.ConsolePrint("-upnp | Turns on NetFuncs.", 4);
                LocalFuncs.ConsolePrint("-map <map filename> | Sets the map.", 4);
                LocalFuncs.ConsolePrint("-client <client name> | Sets the client.", 4);
                LocalFuncs.ConsolePrint("-port <port number> | Sets the server port.", 4);
                LocalFuncs.ConsolePrint("-maxplayers <number of players> | Sets the number of players.", 4);
                LocalFuncs.ConsolePrint("---------", 1);
            }
            else
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["no3d"] != null)
                {
                    LocalVars.StartInNo3D = true;
                    LocalFuncs.ConsolePrint("NovetusCMD will now launch the server in No3D mode.", 4);
                }

                if (CommandLine["overrideconfig"] != null)
                {
                    LocalVars.OverrideINI = true;
                    LocalFuncs.ConsolePrint("NovetusCMD will no longer grab values from the INI file.", 4);

                    if (CommandLine["upnp"] != null)
                    {
                        GlobalVars.UserConfiguration.UPnP = true;
                        LocalFuncs.ConsolePrint("NovetusCMD will now use UPnP for port forwarding.", 4);
                    }

                    if (CommandLine["map"] != null)
                    {
                        GlobalVars.UserConfiguration.MapPath = CommandLine["map"];
                    }
                    else
                    {
                        LocalFuncs.ConsolePrint("NovetusCMD will launch the server with the default map.", 4);
                    }

                    if (CommandLine["client"] != null)
                    {
                        GlobalVars.UserConfiguration.SelectedClient = CommandLine["client"];
                    }
                    else
                    {
                        LocalFuncs.ConsolePrint("NovetusCMD will launch the server with the default client.", 4);
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
                    LocalVars.RequestToOutputInfo = true;
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
                        LocalFuncs.ConsolePrint("NovetusCMD detected a custom script. Loading " + GlobalPaths.AddonScriptPath, 4);
                    }
                    else
                    {
                        LocalFuncs.ConsolePrint("NovetusCMD cannot load '" + CommandLine["script"] + "' as it doesn't use a rbxasset path or URL.", 2);
                    }
                }
            }

            if (!LocalVars.OverrideINI)
            {
                LocalFuncs.ConsolePrint("NovetusCMD is now loading all server configurations from the INI file.", 5);

                if (!File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName))
                {
                    LocalFuncs.ConsolePrint("WARNING 2 - " + GlobalPaths.ConfigName + " not found. Creating one with default values.", 5);
                    WriteConfigValues();
                }

                ReadConfigValues();
            }
            else
            {
                ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
            }

    		InitUPnP();

            if (!LocalVars.NoWebServer)
            {
                StartWebServer();
            }
    		
    		AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramClose);

            LocalFuncs.ConsolePrint("Launching a " + GlobalVars.UserConfiguration.SelectedClient + " server on " + GlobalVars.UserConfiguration.Map + " with " + GlobalVars.UserConfiguration.PlayerLimit + " players.", 1);

            if (!LocalVars.DebugMode)
            {
                StartServer(LocalVars.StartInNo3D);
            }
            else
            {
                LocalFuncs.CreateTXT();
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
            if (LocalVars.ProcessID != 0)
            {
                if (GlobalFuncs.ProcessExists(LocalVars.ProcessID))
                {
                    Process proc = Process.GetProcessById(LocalVars.ProcessID);
                    proc.Kill();
                }
            }
        }
        #endregion

        #region Client Loading (TODO MAKE THIS METHOD GLOBAL)
        static void StartServer(bool no3d)
		{
			string luafile = "";
			if (!GlobalVars.SelectedClientInfo.Fix2007)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalPaths.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua";
			}
            string mapfile = GlobalVars.UserConfiguration.MapPath;
            string rbxexe = "";
			if (GlobalVars.SelectedClientInfo.LegacyMode)
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_server.exe";
			}
			string quote = "\"";
			string args = "";
            if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
                if (!GlobalVars.SelectedClientInfo.Fix2007)
                {
                    args = quote + mapfile + "\" -script \"" + GlobalFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptFuncs.Generator.GetScriptFuncForType(ScriptType.Server) + "; " + (!string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? "dofile('" + GlobalPaths.AddonScriptPath + "');" : "") + quote + (no3d ? " -no3d" : "");
                }
                else
				{
					ScriptFuncs.Generator.GenerateScriptForClient(ScriptType.Server);
					args = "-script " + quote + luafile + quote + (no3d ? " -no3d" : "") + " " + quote + mapfile + quote;
				}
			}
			else
			{
                if (!no3d)
				{
					args = ScriptFuncs.ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<server>", "</server>", mapfile, luafile, rbxexe);
				}
				else
				{
					args = ScriptFuncs.ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<no3d>", "</no3d>", mapfile, luafile, rbxexe);
				}
			}
            try
            {
                LocalFuncs.ConsolePrint("Server Loaded.", 4);
                Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
                ReadClientValues(GlobalVars.UserConfiguration.SelectedClient);
				client.Exited += new EventHandler(ServerExited);
                client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptType.Server, GlobalVars.UserConfiguration.Map);
                LocalVars.ProcessID = client.Id;
                LocalFuncs.CreateTXT();
			}
			catch (Exception ex)
            {
                LocalFuncs.ConsolePrint("ERROR - Failed to launch Novetus. (" + ex.Message + ")", 2);
			}
		}

        static void ServerExited(object sender, EventArgs e)
		{
            Environment.Exit(0);
		}
        #endregion
    }
    #endregion
}