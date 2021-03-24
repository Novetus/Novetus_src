#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.PeerToPeer.Collaboration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
#endregion

#region Script Type
public enum ScriptType
{
	Client = 0,
	Server = 1,
	Solo = 2,
	Studio = 3,
	EasterEgg = 4,
	None = 5
}
#endregion

#region Script Functions
public class ScriptFuncs
{
	#region Script Generator
	public class Generator
	{
		public static string GetScriptFuncForType(ScriptType type)
		{
			return GetScriptFuncForType(GlobalVars.UserConfiguration.SelectedClient, type);
		}

		public static string GetScriptFuncForType(string ClientName, ScriptType type)
		{
			FileFormat.ClientInfo info = GlobalFuncs.GetClientInfoValues(ClientName);

			string rbxexe = "";
			if (info.LegacyMode)
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_client.exe";
			}

#if LAUNCHER
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
			string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
			string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
			string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";

			switch (type)
			{
				case ScriptType.Client:
					return "_G.CSConnect("
						+ (info.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
						+ GlobalVars.IP + "',"
						+ GlobalVars.JoinPort + ",'"
						+ (info.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
						+ GlobalVars.Loadout + ","
						+ md5s + ",'"
						+ GlobalVars.UserConfiguration.PlayerTripcode + "');";
				case ScriptType.Server:
					string IP = SecurityFuncs.GetExternalIPAddress();
					return "_G.CSServer("
						+ GlobalVars.UserConfiguration.RobloxPort + ","
						+ GlobalVars.UserConfiguration.PlayerLimit + ","
						+ md5s + ","
						+ GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower() + ",'"
						+ GlobalVars.UserConfiguration.ServerBrowserServerName + "','"
						+ GlobalVars.UserConfiguration.ServerBrowserServerAddress + "','"
						+ (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : IP) + "','"
						+ GlobalVars.UserConfiguration.SelectedClient + "');";
				case ScriptType.Solo:
				case ScriptType.EasterEgg:
					return "_G.CSSolo("
						+ (info.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
						+ (info.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
						+ GlobalVars.soloLoadout + ");";
				case ScriptType.Studio:
					return "_G.CSStudio();";
				default:
					return "";
			}
		}

		public static string GetNameForType(ScriptType type)
		{
			switch (type)
			{
				case ScriptType.Client:
					return "Client";
				case ScriptType.Server:
					return "Server";
				case ScriptType.Solo:
					return "Play Solo";
				case ScriptType.Studio:
					return "Studio";
				case ScriptType.EasterEgg:
					return "A message from Bitl";
				default:
					return "N/A";
			}
		}

		public static void GenerateScriptForClient(ScriptType type)
		{
			GenerateScriptForClient(GlobalVars.UserConfiguration.SelectedClient, type);
		}

		public static void GenerateScriptForClient(string ClientName, ScriptType type)
		{
			string code = GlobalFuncs.MultiLine(
							   "--Load Script",
							   //scriptcontents,
							   "dofile('rbxasset://scripts/" + GlobalPaths.ScriptName + ".lua')",
							   GetScriptFuncForType(type),
							   !string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? "dofile('" + GlobalPaths.AddonScriptPath + "')" : ""
						   );

			List<string> list = new List<string>(Regex.Split(code, Environment.NewLine));
			string[] convertedList = list.ToArray();
			File.WriteAllLines(GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua", convertedList);
		}
	}
#endregion

	#region ClientScript Parser
	public class ClientScript
	{
		public static string GetArgsFromTag(string code, string tag, string endtag)
		{
			try
			{
				int pFrom = code.IndexOf(tag) + tag.Length;
				int pTo = code.LastIndexOf(endtag);
				string result = code.Substring(pFrom, pTo - pFrom);
				return result;
			}
			catch (Exception)
			{
				return "%donothing%";
			}
		}

		public static ScriptType GetTypeFromTag(string tag)
		{
			switch (tag)
			{
				case string client when client.Contains("client"):
					return ScriptType.Client;
				case string server when server.Contains("server"):
				case string no3d when no3d.Contains("no3d"):
					return ScriptType.Server;
				case string solo when solo.Contains("solo"):
					return ScriptType.Solo;
				case string studio when studio.Contains("studio"):
					return ScriptType.Studio;
				default:
					return ScriptType.None;
			}
		}

		public static string GetTagFromType(ScriptType type, bool endtag, bool no3d)
		{
			switch (type)
			{
				case ScriptType.Client:
					return endtag ? "</client>" : "<client>";
				case ScriptType.Server:
					return no3d ? (endtag ? "</no3d>" : "<no3d>") : (endtag ? "</server>" : "<server>");
				case ScriptType.Solo:
					return endtag ? "</solo>" : "<solo>";
				case ScriptType.Studio:
					return endtag ? "</studio>" : "<studio>";
				default:
					return "";
			}
		}

		public static string GetRawArgsForType(ScriptType type, string md5s, string luafile)
		{
			switch (type)
			{
				case ScriptType.Client:
					return "dofile('" + luafile + "'); _G.CSConnect("
							+ (GlobalVars.SelectedClientInfo.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
							+ GlobalVars.IP + "',"
							+ GlobalVars.JoinPort + ",'"
							+ (GlobalVars.SelectedClientInfo.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
							+ GlobalVars.Loadout + ","
							+ md5s + ",'"
							+ GlobalVars.UserConfiguration.PlayerTripcode + "');";
				case ScriptType.Server:
					string IP = SecurityFuncs.GetExternalIPAddress();
					return "dofile('" + luafile + "'); _G.CSServer("
							+ GlobalVars.UserConfiguration.RobloxPort + ","
							+ GlobalVars.UserConfiguration.PlayerLimit + ","
							+ md5s + ","
							+ GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower() + ",'"
							+ GlobalVars.UserConfiguration.ServerBrowserServerName + "','"
							+ GlobalVars.UserConfiguration.ServerBrowserServerAddress + "','"
							+ GlobalVars.UserConfiguration.SelectedClient + "'); "
							+ (!string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? " dofile('" + GlobalPaths.AddonScriptPath + "');" : "");
				case ScriptType.Solo:
				case ScriptType.EasterEgg:
					return "dofile('" + luafile + "'); _G.CSSolo("
							+ (GlobalVars.SelectedClientInfo.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
							+ (GlobalVars.SelectedClientInfo.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
							+ GlobalVars.soloLoadout + ");";
				case ScriptType.Studio:
					return "dofile('" + luafile + "'); _G.CSStudio();";
				default:
					return "";
			}
		}

		public static int ConvertIconStringToInt()
		{
			switch (GlobalVars.UserCustomization.Icon)
			{
				case "BC":
					return 1;
				case "TBC":
					return 2;
				case "OBC":
					return 3;
				case "NBC":
				default:
					return 0;
			}
		}

		public static string GetFolderAndMapName(string source, string seperator)
		{
			try
			{
				string result = source.Substring(0, source.IndexOf(seperator));

				if (File.Exists(GlobalPaths.MapsDir + @"\\" + result + @"\\" + source))
				{
					return result + @"\\" + source;
				}
				else
				{
					return source;
				}
			}
			catch (Exception)
			{
				return source;
			}
		}

		public static string GetFolderAndMapName(string source)
		{
			return GetFolderAndMapName(source, " -");
		}

		public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
		{
			return CompileScript(GlobalVars.UserConfiguration.SelectedClient, code, tag, endtag, mapfile, luafile, rbxexe, usesharedtags);
		}

		public static string CompileScript(string ClientName, string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
		{
			string start = tag;
			string end = endtag;

			FileFormat.ClientInfo info = GlobalFuncs.GetClientInfoValues(ClientName);

			ScriptType type = GetTypeFromTag(start);

			//we must have the ending tag before we continue.
			if (string.IsNullOrWhiteSpace(end))
			{
				return "";
			}

			if (usesharedtags)
            {
				string sharedstart = "<shared>";
				string sharedend = "</shared>";

				if (code.Contains(sharedstart) && code.Contains(sharedend))
				{
					start = sharedstart;
					end = sharedend;
				}
            }

			if (info.Fix2007)
			{
				Generator.GenerateScriptForClient(type);
			}

			string extractedCode = GetArgsFromTag(code, start, end);

			if (extractedCode.Contains("%donothing%"))
			{
				return "";
			}

#if LAUNCHER
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
			string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
			string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
            string md5sd = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
			string md5s = "'" + info.ClientMD5 + "','" + md5dir + "','" + info.ScriptMD5 + "'";
			string compiled = extractedCode.Replace("%version%", GlobalVars.ProgramInformation.Version)
                    .Replace("%mapfile%", mapfile)
                    .Replace("%luafile%", luafile)
                    .Replace("%charapp%", GlobalVars.UserCustomization.CharacterID)
                    .Replace("%ip%", GlobalVars.IP)
                    .Replace("%port%", GlobalVars.UserConfiguration.RobloxPort.ToString())
					.Replace("%joinport%", GlobalVars.JoinPort.ToString())
                    .Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
                    .Replace("%icone%", ConvertIconStringToInt().ToString())
                    .Replace("%icon%", GlobalVars.UserCustomization.Icon)
                    .Replace("%id%", GlobalVars.UserConfiguration.UserID.ToString())
                    .Replace("%face%", GlobalVars.UserCustomization.Face)
                    .Replace("%head%", GlobalVars.UserCustomization.Head)
                    .Replace("%tshirt%", GlobalVars.UserCustomization.TShirt)
                    .Replace("%shirt%", GlobalVars.UserCustomization.Shirt)
                    .Replace("%pants%", GlobalVars.UserCustomization.Pants)
                    .Replace("%hat1%", GlobalVars.UserCustomization.Hat1)
                    .Replace("%hat2%", GlobalVars.UserCustomization.Hat2)
                    .Replace("%hat3%", GlobalVars.UserCustomization.Hat3)
                    .Replace("%faced%", GlobalVars.UserCustomization.Face.Contains("http://") ? GlobalVars.UserCustomization.Face : GlobalPaths.faceGameDir + GlobalVars.UserCustomization.Face)
                    .Replace("%headd%", GlobalPaths.headGameDir + GlobalVars.UserCustomization.Head)
                    .Replace("%tshirtd%", GlobalVars.UserCustomization.TShirt.Contains("http://") ? GlobalVars.UserCustomization.TShirt : GlobalPaths.tshirtGameDir + GlobalVars.UserCustomization.TShirt)
                    .Replace("%shirtd%", GlobalVars.UserCustomization.Shirt.Contains("http://") ? GlobalVars.UserCustomization.Shirt : GlobalPaths.shirtGameDir + GlobalVars.UserCustomization.Shirt)
                    .Replace("%pantsd%", GlobalVars.UserCustomization.Pants.Contains("http://") ? GlobalVars.UserCustomization.Pants : GlobalPaths.pantsGameDir + GlobalVars.UserCustomization.Pants)
                    .Replace("%hat1d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat1)
                    .Replace("%hat2d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat2)
                    .Replace("%hat3d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat3)
                    .Replace("%headcolor%", GlobalVars.UserCustomization.HeadColorID.ToString())
                    .Replace("%torsocolor%", GlobalVars.UserCustomization.TorsoColorID.ToString())
                    .Replace("%larmcolor%", GlobalVars.UserCustomization.LeftArmColorID.ToString())
                    .Replace("%llegcolor%", GlobalVars.UserCustomization.LeftLegColorID.ToString())
                    .Replace("%rarmcolor%", GlobalVars.UserCustomization.RightArmColorID.ToString())
                    .Replace("%rlegcolor%", GlobalVars.UserCustomization.RightLegColorID.ToString())
                    .Replace("%md5launcher%", md5dir)
                    .Replace("%md5script%", info.ScriptMD5)
                    .Replace("%md5exe%", info.ClientMD5)
                    .Replace("%md5scriptd%", md5script)
                    .Replace("%md5exed%", md5exe)
					.Replace("%md5s%", md5s)
					.Replace("%md5sd%", md5sd)
					.Replace("%limit%", GlobalVars.UserConfiguration.PlayerLimit.ToString())
					.Replace("%extra%", GlobalVars.UserCustomization.Extra)
					.Replace("%hat4%", GlobalVars.UserCustomization.Extra)
					.Replace("%extrad%", GlobalPaths.extraGameDir + GlobalVars.UserCustomization.Extra)
					.Replace("%hat4d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Extra)
					.Replace("%args%", GetRawArgsForType(type, md5sd, luafile))
					.Replace("%mapfiled%", GlobalPaths.BaseGameDir + GlobalVars.UserConfiguration.MapPathSnip.Replace(@"\\", @"\").Replace(@"/", @"\"))
					.Replace("%mapfilec%", extractedCode.Contains("%mapfilec%") ? GlobalFuncs.CopyMapToRBXAsset() : "")
					.Replace("%tripcode%", GlobalVars.UserConfiguration.PlayerTripcode)
					.Replace("%scripttype%", Generator.GetNameForType(type))
					.Replace("%addonscriptpath%", GlobalPaths.AddonScriptPath)
					.Replace("%notifications%", GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower())
					.Replace("%loadout%", code.Contains("<solo>") ? GlobalVars.soloLoadout : GlobalVars.Loadout)
					.Replace("%doublequote%", "\"");

			if (compiled.Contains("%disabled%"))
            {
				MessageBox.Show("This option has been disabled for this client.");
				return "";
			}

			return compiled;
		}
	}
#endregion
}
#endregion