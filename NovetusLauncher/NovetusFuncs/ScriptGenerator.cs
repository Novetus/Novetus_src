/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:02 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

public enum ScriptType
{
	Client = 0,
	Server = 1,
	Solo = 2,
	Studio = 3,
	EasterEgg = 4,
	None = 5
}

public class ScriptGenerator
{
	public static string GetScriptFuncForType(ScriptType type)
	{
		string rbxexe = "";
		if (GlobalVars.SelectedClientInfo.LegacyMode == true) 
		{
			rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
		} 
		else 
		{
			rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_client.exe";
		}
			
		string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
		string md5script = SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua");
		string md5exe = SecurityFuncs.CalculateMD5(rbxexe);
		string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";

		switch (type)
		{
			case ScriptType.Client:
				return "_G.CSConnect(" 
					+ (GlobalVars.SelectedClientInfo.UsesID == true ? GlobalVars.UserID : 0) + ",'" 
					+ GlobalVars.IP + "'," 
					+ GlobalVars.RobloxPort + ",'"
					+ (GlobalVars.SelectedClientInfo.UsesPlayerName == true ? GlobalVars.PlayerName : "Player") + "',"
					+ GlobalVars.loadtext + "," 
					+ md5s + ",'" 
					+ GlobalVars.PlayerTripcode + "')";
			case ScriptType.Server:
				return "_G.CSServer(" 
					+ GlobalVars.RobloxPort + "," 
					+ GlobalVars.PlayerLimit + "," 
					+ md5s + ")";
			case ScriptType.Solo:
			case ScriptType.EasterEgg:
				return "_G.CSSolo("
					+ (GlobalVars.SelectedClientInfo.UsesID == true ? GlobalVars.UserID : 0) + ",'"
					+ (GlobalVars.SelectedClientInfo.UsesPlayerName == true ? GlobalVars.PlayerName : "Player") + "',"
					+ GlobalVars.sololoadtext + ")";
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
		string code = GlobalVars.MultiLine(
			               "--Load Script",
							//scriptcontents,
			               LauncherFuncs.ChangeGameSettings(),
                           "dofile('rbxasset://scripts/" + GlobalVars.ScriptName + ".lua')",
			               GetScriptFuncForType(type),
                           !string.IsNullOrWhiteSpace(GlobalVars.AddonScriptPath) ? "dofile('" + GlobalVars.AddonScriptPath + "')" : ""
                       );
			
		List<string> list = new List<string>(Regex.Split(code, Environment.NewLine));
		string[] convertedList = list.ToArray();
		File.WriteAllLines(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua", convertedList);
	}
}