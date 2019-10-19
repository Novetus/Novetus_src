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
	class Program
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
				catch (Exception ex)
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
				catch (Exception ex)
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
				catch (Exception ex)
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
			catch (Exception ex)
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
			catch (Exception ex)
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
     				GlobalVars.WebServer = new SimpleHTTPServer(GlobalVars.DataPath, GlobalVars.WebServer_Port);
        			ConsolePrint("WebServer: Server is running on port: " + GlobalVars.WebServer.Port.ToString(), 3);
      			}
      			catch (Exception ex)
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
      			catch (Exception ex)
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
			LauncherFuncs.WriteConfigValues(GlobalVars.ConfigDir + "\\config.ini");
			ConsolePrint("Config Saved.", 3);
		}
        
        static void ProgramClose(object sender, EventArgs e)
        {
			WriteConfigValues();
			if (GlobalVars.IsWebServerOn == true)
			{
				StopWebServer();
			}
        }
		
		static void ReadConfigValues()
		{
			LauncherFuncs.ReadConfigValues(GlobalVars.ConfigDir + "\\config.ini");
			
			if (GlobalVars.UserID == 0)
			{
				LauncherFuncs.GeneratePlayerID();
				WriteConfigValues();
			}
			
			if (GlobalVars.PlayerLimit == 0)
			{
				//We need at least a limit of 12 players.
				GlobalVars.PlayerLimit = 12;
			}
			
			ConsolePrint("Config loaded.", 3);
			ReadClientValues(GlobalVars.SelectedClient);
		}
		
		static void ReadClientValues(string ClientName)
		{
			string clientpath = GlobalVars.ClientDir + @"\\" + ClientName + @"\\clientinfo.nov";
			
			if (!File.Exists(clientpath))
			{
				ConsolePrint("ERROR 1 - No clientinfo.nov detected with the client you chose. The client either cannot be loaded, or it is not available.", 2);
				GlobalVars.SelectedClient = GlobalVars.DefaultClient;
			}
			
			LauncherFuncs.ReadClientValues(clientpath);
			ConsolePrint("Client '" + GlobalVars.SelectedClient + "' successfully loaded.", 3);
		}
		
		static string ProcessInput(string s)
    	{
       		return s;
    	}
		
		public static void Main(string[] args)
		{
			bool StartInNo3D = false;
            bool OverrideINI = false;

            if (args.Length == 0)
			{
                ConsolePrint("Help: Command Line Arguments", 3);
                ConsolePrint("---------", 1);
                ConsolePrint("-no3d | Launches server in NoGraphics mode", 3);
                ConsolePrint("-overrideconfig | Override the launcher settings.", 3);
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
					StartInNo3D = true;
                    ConsolePrint("NovetusCMD will now launch the server in No3D mode.\n", 4);
                }

                if (CommandLine["overrideconfig"] != null)
                {
                    OverrideINI = true;
                    ConsolePrint("NovetusCMD will no longer grab values from the INI file.\n", 4);

                    if (CommandLine["upnp"] != null)
                    {
                        GlobalVars.UPnP = true;
                        ConsolePrint("NovetusCMD will now use UPnP for port forwarding.\n", 4);
                    }

                    if (CommandLine["map"] != null)
                    {
                        GlobalVars.Map = CommandLine["map"];
                    }

                    if (CommandLine["client"] != null)
                    {
                        GlobalVars.SelectedClient = CommandLine["client"];
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
            }
			
			string[] lines = File.ReadAllLines(GlobalVars.ConfigDir + "\\info.txt"); //File is in System.IO
			string version = lines[0];
    		GlobalVars.DefaultClient = lines[1];
    		GlobalVars.DefaultMap = lines[2];
    		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
    		GlobalVars.Map = GlobalVars.DefaultMap;
    		Console.Title = "Novetus " + version + " CMD";
    		GlobalVars.Version = version;

            if (!OverrideINI)
            {
                ConsolePrint("NovetusCMD is now loading all server configurations from the INI file.", 5);
                ConsolePrint("NovetusCMD version " + version + " loaded. Initializing config.", 4);

                if (!File.Exists(GlobalVars.ConfigDir + "\\config.ini"))
                {
                    ConsolePrint("WARNING 2 - config.ini not found. Creating one with default values.", 5);
                    WriteConfigValues();
                }

                ReadConfigValues();
            }

    		InitUPnP();
    		StartWebServer();
    		
    		AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramClose);
    		
			StartServer(StartInNo3D);
			Console.ReadKey();
		}
		
		static void StartServer(bool no3d)
		{
			string luafile = "";
			if (!GlobalVars.FixScriptMapMode)
			{
				luafile = "rbxasset://scripts\\\\" + GlobalVars.ScriptName + ".lua";
			}
			else
			{
				luafile = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua";
			}
			string mapfile = GlobalVars.MapsDir + @"\\" + TreeNodeHelper.GetFolderNameFromPrefix(GlobalVars.Map) + GlobalVars.Map;
			string rbxexe = "";
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_server.exe";
			}
			string quote = "\"";
			string args = "";
			if (GlobalVars.CustomArgs.Equals("%args%"))
			{
				if (!GlobalVars.FixScriptMapMode)
				{
					args = quote + mapfile + "\" -script \"dofile('" + luafile + "'); " + ScriptGenerator.GetScriptFuncForType(ScriptGenerator.ScriptType.Server, GlobalVars.SelectedClient) + quote + (no3d ? " -no3d" : "");
				}
				else
				{
					ScriptGenerator.GenerateScriptForClient(ScriptGenerator.ScriptType.Server, GlobalVars.SelectedClient);
					args = "-script " + quote + luafile + quote + (no3d ? " -no3d" : "") + " " + quote + mapfile + quote;
				}
			}
			else
			{
				if (!no3d)
				{
					args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<server>", "</server>", mapfile, luafile, rbxexe);
				}
				else
				{
					args = ClientScript.CompileScript(GlobalVars.CustomArgs, "<no3d>", "</no3d>", mapfile, luafile, rbxexe);
				}
			}
			try
			{
				//when we add upnp, change this
				ConsolePrint("Server Loaded.", 4);
				Process client = new Process();
				client.StartInfo.FileName = rbxexe;
				client.StartInfo.Arguments = args;
				client.EnableRaisingEvents = true;
				ReadClientValues(GlobalVars.SelectedClient);
				client.Exited += new EventHandler(ServerExited);
				client.Start();
				SecurityFuncs.RenameWindow(client, ScriptGenerator.ScriptType.Server);
			}
			catch (Exception ex)
			{
				ConsolePrint("ERROR 2 - Failed to launch Novetus. (" + ex.Message + ")", 2);
			}
		}

        static void ServerExited(object sender, EventArgs e)
		{
            Environment.Exit(0);
		}
		
		static void ConsolePrint(string text, int type)
		{
			ConsoleText("[" + DateTime.Now.ToShortTimeString() + "] - ", ConsoleColor.White);
			if (type == 1)
			{
				ConsoleText(text, ConsoleColor.White);
			}
			else if (type == 2)
			{
				ConsoleText(text, ConsoleColor.Red);
			}
			else if (type == 3)
			{
				ConsoleText(text, ConsoleColor.Green);
			}
			else if (type == 4)
			{
				ConsoleText(text, ConsoleColor.Cyan);
			}
			else if (type == 5)
			{
				ConsoleText(text, ConsoleColor.Yellow);
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