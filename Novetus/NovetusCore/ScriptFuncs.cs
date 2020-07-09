#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
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
			string rbxexe = "";
			if (GlobalVars.SelectedClientInfo.LegacyMode)
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\RobloxApp_client.exe";
			}

			string md5dir = SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location);
			string md5script = SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua");
			string md5exe = SecurityFuncs.GenerateMD5(rbxexe);
			string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";

			switch (type)
			{
				case ScriptType.Client:
					return "_G.CSConnect("
						+ (GlobalVars.SelectedClientInfo.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
						+ GlobalVars.IP + "',"
						+ GlobalVars.UserConfiguration.RobloxPort + ",'"
						+ (GlobalVars.SelectedClientInfo.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
						+ GlobalVars.Loadout + ","
						+ md5s + ",'"
						+ GlobalVars.UserConfiguration.PlayerTripcode + "')";
				case ScriptType.Server:
					return "_G.CSServer("
						+ GlobalVars.UserConfiguration.RobloxPort + ","
						+ GlobalVars.UserConfiguration.PlayerLimit + ","
						+ md5s + ")";
				case ScriptType.Solo:
				case ScriptType.EasterEgg:
					return "_G.CSSolo("
						+ (GlobalVars.SelectedClientInfo.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
						+ (GlobalVars.SelectedClientInfo.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
						+ GlobalVars.soloLoadout + ")";
				case ScriptType.Studio:
					return "_G.CSStudio()";
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
					return "";
			}
		}
		public static void GenerateScriptForClient(ScriptType type)
		{
			string code = GlobalFuncs.MultiLine(
							   "--Load Script",
							   //scriptcontents,
							   GlobalFuncs.ChangeGameSettings(),
							   "dofile('rbxasset://scripts/" + GlobalPaths.ScriptName + ".lua')",
							   GetScriptFuncForType(type),
							   !string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? "dofile('" + GlobalPaths.AddonScriptPath + "')" : ""
						   );

			List<string> list = new List<string>(Regex.Split(code, Environment.NewLine));
			string[] convertedList = list.ToArray();
			File.WriteAllLines(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua", convertedList);
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

		public static string GetRawArgsForType(ScriptType type, string md5s, string luafile)
		{
			switch (type)
			{
				case ScriptType.Client:
					return GlobalFuncs.ChangeGameSettings() +
							" dofile('" + luafile + "'); _G.CSConnect("
							+ (GlobalVars.SelectedClientInfo.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
							+ GlobalVars.IP + "',"
							+ GlobalVars.UserConfiguration.RobloxPort + ",'"
							+ (GlobalVars.SelectedClientInfo.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
							+ GlobalVars.Loadout + ","
							+ md5s + ",'"
							+ GlobalVars.UserConfiguration.PlayerTripcode + "')";
				case ScriptType.Server:
					return GlobalFuncs.ChangeGameSettings() +
							" dofile('" + luafile + "'); _G.CSServer("
							+ GlobalVars.UserConfiguration.RobloxPort + ","
							+ GlobalVars.UserConfiguration.PlayerLimit + ","
							+ md5s + "); "
							+ (!string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? GlobalFuncs.ChangeGameSettings() +
							" dofile('" + GlobalPaths.AddonScriptPath + "');" : "");
				case ScriptType.Solo:
				case ScriptType.EasterEgg:
					return GlobalFuncs.ChangeGameSettings()
							+ " dofile('" + luafile + "'); _G.CSSolo("
							+ (GlobalVars.SelectedClientInfo.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
							+ (GlobalVars.SelectedClientInfo.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
							+ GlobalVars.soloLoadout + ")";
				case ScriptType.Studio:
					return GlobalFuncs.ChangeGameSettings()
							+ " dofile('" + luafile + "');";
				default:
					return "";
			}
		}

		public static string GetRawArgsFromTag(string tag, string md5s, string luafile)
		{
			return GetRawArgsForType(GetTypeFromTag(tag), md5s, luafile);
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

		public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe)
		{
			if (GlobalVars.SelectedClientInfo.Fix2007)
			{
				ScriptFuncs.Generator.GenerateScriptForClient(GetTypeFromTag(tag));
			}

			string extractedCode = GetArgsFromTag(code, tag, endtag);

			if (extractedCode.Contains("%donothing%"))
			{
				return "";
			}

			string md5dir = !GlobalVars.SelectedClientInfo.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
			string md5script = !GlobalVars.SelectedClientInfo.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
			string md5exe = !GlobalVars.SelectedClientInfo.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
			string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
			string compiled = extractedCode.Replace("%mapfile%", mapfile)
					.Replace("%luafile%", luafile)
					.Replace("%charapp%", GlobalVars.UserCustomization.CharacterID)
					.Replace("%ip%", GlobalVars.IP)
					.Replace("%port%", GlobalVars.UserConfiguration.RobloxPort.ToString())
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
					.Replace("%faced%", GlobalPaths.faceGameDir + GlobalVars.UserCustomization.Face)
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
					.Replace("%md5script%", GlobalVars.SelectedClientInfo.ClientMD5)
					.Replace("%md5exe%", GlobalVars.SelectedClientInfo.ScriptMD5)
					.Replace("%md5scriptd%", md5script)
					.Replace("%md5exed%", md5exe)
					.Replace("%limit%", GlobalVars.UserConfiguration.PlayerLimit.ToString())
					.Replace("%extra%", GlobalVars.UserCustomization.Extra)
					.Replace("%hat4%", GlobalVars.UserCustomization.Extra)
					.Replace("%extrad%", GlobalPaths.extraGameDir + GlobalVars.UserCustomization.Extra)
					.Replace("%hat4d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Extra)
					.Replace("%args%", GetRawArgsFromTag(tag, md5s, luafile))
					.Replace("%facews%", GlobalPaths.WebServer_FaceDir + GlobalVars.UserCustomization.Face)
					.Replace("%headws%", GlobalPaths.WebServer_HeadDir + GlobalVars.UserCustomization.Head)
					.Replace("%tshirtws%", GlobalVars.UserCustomization.TShirt.Contains("http://") ? GlobalVars.UserCustomization.TShirt : GlobalPaths.WebServer_TShirtDir + GlobalVars.UserCustomization.TShirt)
					.Replace("%shirtws%", GlobalVars.UserCustomization.Shirt.Contains("http://") ? GlobalVars.UserCustomization.Shirt : GlobalPaths.WebServer_ShirtDir + GlobalVars.UserCustomization.Shirt)
					.Replace("%pantsws%", GlobalVars.UserCustomization.Pants.Contains("http://") ? GlobalVars.UserCustomization.Pants : GlobalPaths.WebServer_PantsDir + GlobalVars.UserCustomization.Pants)
					.Replace("%hat1ws%", GlobalPaths.WebServer_HatDir + GlobalVars.UserCustomization.Hat1)
					.Replace("%hat2ws%", GlobalPaths.WebServer_HatDir + GlobalVars.UserCustomization.Hat2)
					.Replace("%hat3ws%", GlobalPaths.WebServer_HatDir + GlobalVars.UserCustomization.Hat3)
					.Replace("%extraws%", GlobalPaths.WebServer_ExtraDir + GlobalVars.UserCustomization.Extra)
					.Replace("%hat4ws%", GlobalPaths.WebServer_HatDir + GlobalVars.UserCustomization.Extra)
					.Replace("%mapfiled%", GlobalPaths.BaseGameDir + GlobalVars.UserConfiguration.MapPathSnip.Replace(@"\\", @"\"))
					.Replace("%tripcode%", GlobalVars.UserConfiguration.PlayerTripcode)
					.Replace("%addonscriptpath%", GlobalPaths.AddonScriptPath);
			return compiled;
		}
	}
	#endregion
}
#endregion