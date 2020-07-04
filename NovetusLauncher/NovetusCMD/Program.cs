/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 6/15/2019
 * Time: 5:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Mono.Nat;
using System.Diagnostics;
using System.IO;
using static NovetusCMD.CommandLineArguments;

namespace NovetusCMD
{
    public static class Program
	{
        public static void InitUPnP()
		{
			if (GlobalVars.UPnP == true)
			{
				try
				{
					UPnP.InitUPnP(DeviceFound,DeviceLost);
					ConsolePrint("UPnP: Service initialized", 3);
				}
				catch (Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("UPnP: Unable to initialize UPnP. Reason - " + ex.Message, 2);
				}
			}
		}
		
		public static void StartUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UPnP == true)
			{
				try
				{
					UPnP.StartUPnP(device,protocol,port);
					ConsolePrint("UPnP: Port " + port + " opened on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("UPnP: Unable to open port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		public static void StopUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UPnP == true)
			{
				try
				{
					UPnP.StopUPnP(device,protocol,port);
					ConsolePrint("UPnP: Port " + port + " closed on '" + device.GetExternalIP() + "' (" + protocol.ToString() + ")", 3);
				}
				catch (Exception ex) when (!Env.Debugging)
                {
					ConsolePrint("UPnP: Unable to close port mapping. Reason - " + ex.Message, 2);
				}
			}
		}
		
		private static void DeviceFound(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
				ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' registered.", 3);
				StartUPnP(device, Protocol.Udp, GlobalVars.RobloxPort);
				StartUPnP(device, Protocol.Tcp, GlobalVars.RobloxPort);
				StartUPnP(device, Protocol.Udp, GlobalVars.WebServer_Port);
				StartUPnP(device, Protocol.Tcp, GlobalVars.WebServer_Port);
			}
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("UPnP: Unable to register device. Reason - " + ex.Message, 2);
			}
		}
 
		private static void DeviceLost(object sender, DeviceEventArgs args)
		{
			try
			{
				INatDevice device = args.Device;
 				ConsolePrint("UPnP: Device '" + device.GetExternalIP() + "' disconnected.", 3);
 				StopUPnP(device, Protocol.Udp, GlobalVars.RobloxPort);
				StopUPnP(device, Protocol.Tcp, GlobalVars.RobloxPort);
				StopUPnP(device, Protocol.Udp, GlobalVars.WebServer_Port);
				StopUPnP(device, Protocol.Tcp, GlobalVars.WebServer_Port);
 			}
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("UPnP: Unable to disconnect device. Reason - " + ex.Message, 2);
			}
		}
		
		static void StartWebServer()
        {
        	if (SecurityFuncs.IsElevated)
			{
				try
      			{
     				GlobalVars.WebServer = new SimpleHTTPServer(GlobalVars.ServerDir, GlobalVars.WebServer_Port);
        			ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3);
      			}
      			catch (Exception ex) when (!Env.Debugging)
                {
        			ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
				ConsolePrint("WebServer: Failed to launch WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }
        
        static void StopWebServer()
        {
        	if (SecurityFuncs.IsElevated)
			{
				try
      			{
        			ConsolePrint("WebServer: Server has stopped on port: " + GlobalVars.WebServer.Port.ToString(), 2);
        			GlobalVars.WebServer.Stop();
      			}
      			catch (Exception ex) when (!Env.Debugging)
                {
        			ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (" + ex.Message + ")", 2);
      			}
			}
			else
			{
				ConsolePrint("WebServer: Failed to stop WebServer. Some features may not function. (Did not run as Administrator)", 2);
			}
        }
        
        static void WriteConfigValues()
		{
			LauncherFuncs.Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, true);
			ConsolePrint("Config Saved.", 3);
		}
        
        static void ProgramClose(object sender, EventArgs e)
        {
			WriteConfigValues();
			if (GlobalVars.IsWebServerOn == true)
			{
				StopWebServer();
			}
            if (LocalVars.ProcessID != 0)
            {
                if (GlobalVars.ProcessExists(LocalVars.ProcessID))
                {
                    Process proc = Process.GetProcessById(LocalVars.ProcessID);
                    proc.Kill();
                }
            }
        }
		
		static void ReadConfigValues()
		{
			LauncherFuncs.Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, false);
            ConsolePrint("Config loaded.", 3);
			ReadClientValues(GlobalVars.SelectedClient);
		}
		
		static void ReadClientValues(string ClientName)
		{
            string clientpath = GlobalVars.ClientDir + @"\\" + ClientName + @"\\clientinfo.nov";

            if (!File.Exists(clientpath))
            {
                ConsolePrint("ERROR - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
                GlobalVars.SelectedClient = GlobalVars.DefaultClient;
                ReadClientValues(ClientName);
            }
            else
            {
                LauncherFuncs.ReadClientValues(clientpath);
                ConsolePrint("Client '" + GlobalVars.SelectedClient + "' successfully loaded.", 3);
            }
		}
		
		static string ProcessInput(string s)
    	{
       		return s;
    	}
		
		public static void Main(string[] args)
		{
            LauncherFuncs.ReadInfoFile(GlobalVars.ConfigDir + "\\" + GlobalVars.InfoName, true);
            Console.Title = "Novetus " + GlobalVars.Version + " CMD";

            ConsolePrint("NovetusCMD version " + GlobalVars.Version + " loaded.", 1);
            ConsolePrint("Novetus path: " + GlobalVars.BasePath, 1);

            if (args.Length == 0)
            {
                ConsolePrint("Help: Command Line Arguments", 3);
                ConsolePrint("---------", 1);
                ConsolePrint("General", 3);
                ConsolePrint("-no3d | Launches server in NoGraphics mode", 4);
                ConsolePrint("-script <path to script> | Loads an additional server script.", 4);
                ConsolePrint("-outputinfo | Outputs all information about the running server to a text file.", 4);
                ConsolePrint("-overrideconfig | Override the launcher settings.", 4);
                ConsolePrint("-debug | Disables launching of the server for debugging purposes.", 4);
                ConsolePrint("-nowebserver | Disables launching of the web server.", 4);
                ConsolePrint("---------", 1);
                ConsolePrint("Custom server options", 3);
                ConsolePrint("-overrideconfig must be added in order for the below commands to function.", 5);
                ConsolePrint("-upnp | Turns on UPnP.", 4);
                ConsolePrint("-map <map filename> | Sets the map.", 4);
                ConsolePrint("-client <client name> | Sets the client.", 4);
                ConsolePrint("-port <port number> | Sets the server port.", 4);
                ConsolePrint("-maxplayers <number of players> | Sets the number of players.", 4);
                ConsolePrint("---------", 1);
            }
            else
            {
                Arguments CommandLine = new Arguments(args);

                if (CommandLine["no3d"] != null)
                {
                    LocalVars.StartInNo3D = true;
                    ConsolePrint("NovetusCMD will now launch the server in No3D mode.", 4);
                }

                if (CommandLine["overrideconfig"] != null)
                {
                    LocalVars.OverrideINI = true;
                    ConsolePrint("NovetusCMD will no longer grab values from the INI file.", 4);

                    if (CommandLine["upnp"] != null)
                    {
                        GlobalVars.UPnP = true;
                        ConsolePrint("NovetusCMD will now use UPnP for port forwarding.", 4);
                    }

                    if (CommandLine["map"] != null)
                    {
                        GlobalVars.MapPath = CommandLine["map"];
                    }
                    else
                    {
                        ConsolePrint("NovetusCMD will launch the server with the default map.", 4);
                    }

                    if (CommandLine["client"] != null)
                    {
                        GlobalVars.SelectedClient = CommandLine["client"];
                    }
                    else
                    {
                        ConsolePrint("NovetusCMD will launch the server with the default client.", 4);
                    }

                    if (CommandLine["port"] != null)
                    {
                        GlobalVars.RobloxPort = Convert.ToInt32(CommandLine["port"]);
                    }

                    if (CommandLine["maxplayers"] != null)
                    {
                        GlobalVars.PlayerLimit = Convert.ToInt32(CommandLine["maxplayers"]);
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
                        GlobalVars.AddonScriptPath = CommandLine["script"].Replace(@"\", @"\\");
                        ConsolePrint("NovetusCMD detected a custom script. Loading " + GlobalVars.AddonScriptPath, 4);
                    }
                    else
                    {
                        ConsolePrint("NovetusCMD cannot load '" + CommandLine["script"] + "' as it doesn't use a rbxasset path or URL.", 2);
                    }
                }
            }

            if (!LocalVars.OverrideINI)
            {
                ConsolePrint("NovetusCMD is now loading all server configurations from the INI file.", 5);

                if (!File.Exists(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName))
                {
                    ConsolePrint("WARNING 2 - " + GlobalVars.ConfigName + " not found. Creating one with default values.", 5);
                    WriteConfigValues();
                }

                ReadConfigValues();
            }
            else
            {
                ReadClientValues(GlobalVars.SelectedClient);
            }

    		InitUPnP();

            if (!LocalVars.NoWebServer)
            {
                StartWebServer();
            }
    		
    		AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramClose);

            ConsolePrint("Launching a " + GlobalVars.SelectedClient + " server on " + GlobalVars.Map + " with " + GlobalVars.PlayerLimit + " players.", 1);

            if (!LocalVars.DebugMode)
            {
                StartServer(LocalVars.StartInNo3D);
            }
            else
            {
                CreateTXT();
            }
			Console.ReadKey();
		}
		
		static void StartServer(bool no3d)
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
            string mapfile = GlobalVars.MapPath;
            string rbxexe = "";
			if (GlobalVars.SelectedClientInfo.LegacyMode == true)
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_server.exe";
			}
			string quote = "\"";
			string args = "";
            if (GlobalVars.SelectedClientInfo.CommandLineArgs.Equals("%args%"))
			{
                if (!GlobalVars.SelectedClientInfo.Fix2007)
                {
                    args = quote + mapfile + "\" -script \"" + LauncherFuncs.ChangeGameSettings() + " dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptType.Server) + "; " + (!string.IsNullOrWhiteSpace(GlobalVars.AddonScriptPath) ? "dofile('" + GlobalVars.AddonScriptPath + "');" : "") + quote + (no3d ? " -no3d" : "");
                }
                else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptType.Server);
					args = "-script " + quote + luafile + quote + (no3d ? " -no3d" : "") + " " + quote + mapfile + quote;
				}
			}
			else
			{
                if (!no3d)
				{
					args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<server>", "</server>", mapfile, luafile, rbxexe);
				}
				else
				{
					args = ClientScript.CompileScript(GlobalVars.SelectedClientInfo.CommandLineArgs, "<no3d>", "</no3d>", mapfile, luafile, rbxexe);
				}
			}
            try
            {
				ConsolePrint("Server Loaded.", 4);
                Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
                ReadClientValues(GlobalVars.SelectedClient);
				client.Exited += new EventHandler(ServerExited);
                client.Start();
                client.PriorityClass = ProcessPriorityClass.RealTime;
                SecurityFuncs.RenameWindow(client, ScriptType.Server, GlobalVars.Map);
                LocalVars.ProcessID = client.Id;
                CreateTXT();
			}
			catch (Exception ex) when (!Env.Debugging)
            {
				ConsolePrint("ERROR - Failed to launch Novetus. (" + ex.Message + ")", 2);
			}
		}

        static void ServerExited(object sender, EventArgs e)
		{
            Environment.Exit(0);
		}

        static async void CreateTXT()
        {
            if (LocalVars.RequestToOutputInfo)
            {
                string IP = await SecurityFuncs.GetExternalIPAddressAsync();
                string[] lines1 = {
                        SecurityFuncs.Base64Encode(IP),
                        SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.SelectedClient)
                    };
                string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1, true));
                string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.SelectedClient)
                    };
                string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2, true));

                string text = GlobalVars.MultiLine(
                       "Process ID: " + (LocalVars.ProcessID == 0 ? "N/A" : LocalVars.ProcessID.ToString()),
                       "Don't copy the Process ID when sharing the server.",
                       "--------------------",
                       "Server Info:",
                       "Client: " + GlobalVars.SelectedClient,
                       "IP: " + IP,
                       "Port: " + GlobalVars.RobloxPort.ToString(),
                       "Map: " + GlobalVars.Map,
                       "Players: " + GlobalVars.PlayerLimit,
                       "Version: Novetus " + GlobalVars.Version,
                       "Online URI Link:",
                       URI,
                       "Local URI Link:",
                       URI2,
                       GlobalVars.IsWebServerOn == true ? "Web Server URL:" : "",
                       GlobalVars.IsWebServerOn == true ? "http://" + IP + ":" + GlobalVars.WebServer.Port.ToString() : "",
                       GlobalVars.IsWebServerOn == true ? "Local Web Server URL:" : "",
                       GlobalVars.IsWebServerOn == true ? GlobalVars.LocalWebServerURI : ""
                   );

                File.WriteAllText(GlobalVars.BasePath + "\\" + LocalVars.ServerInfoFileName, GlobalVars.RemoveEmptyLines(text));
                ConsolePrint("Server Information sent to file " + GlobalVars.BasePath + "\\" + LocalVars.ServerInfoFileName, 4);
            }
        }

        static void ConsolePrint(string text, int type)
        {
            ConsoleText("[" + DateTime.Now.ToShortTimeString() + "] - ", ConsoleColor.White);

            switch (type)
            {
                case 2:
                    ConsoleText(text, ConsoleColor.Red);
                    break;
                case 3:
                    ConsoleText(text, ConsoleColor.Green);
                    break;
                case 4:
                    ConsoleText(text, ConsoleColor.Cyan);
                    break;
                case 5:
                    ConsoleText(text, ConsoleColor.Yellow);
                    break;
                case 1:
                default:
                    ConsoleText(text, ConsoleColor.White);
                    break;
            }

            ConsoleText(Environment.NewLine, ConsoleColor.White);
        }

        static void ConsoleText(string text, ConsoleColor color)
		{
			Console.ForegroundColor = color; 
			Console.Write(text);
		}
	}
}