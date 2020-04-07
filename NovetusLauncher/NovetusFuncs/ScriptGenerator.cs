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

/*
	 * so, in order for us to generate a good script, we have to:
	 * - specify the script header that gives us our setting adjustments
	 * - add player customization into the script
	 * - call the main script
	 * - call the function
	 * 
	 * now, we have to call the funtion associated for the action, such as starting the main client or something
	 * we also need to make sure that when we add the option, we'll need to adapt map loading to work RBX2007 style for the clients using the script generator.
	 */
	
public class ScriptGenerator
{
	public enum ScriptType
	{
		Client = 0,
		Server = 1,
		Solo = 2,
		Studio = 3,
        EasterEgg = 4,
        None = 5
	}
		
	public static string GetScriptFuncForType(ScriptType type)
	{
		string rbxexe = "";
		if (GlobalVars.LegacyMode == true) {
			rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp.exe";
		} else {
			rbxexe = GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\RobloxApp_client.exe";
		}
			
		string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
		string md5script = SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua");
		string md5exe = SecurityFuncs.CalculateMD5(rbxexe);
		string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
        if (type == ScriptType.Client) {
            if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true) {
                return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ",'" + GlobalVars.PlayerTripcode + "')";
            } else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true) {
                return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ",'" + GlobalVars.PlayerTripcode + "')";
            } else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false) {
                return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ",'" + GlobalVars.PlayerTripcode + "')";
            } else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false) {
                return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ",'" + GlobalVars.PlayerTripcode + "')";
            } else {
                return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ",'" + GlobalVars.PlayerTripcode + "')";
            }
        } else if (type == ScriptType.Server) {
            return "_G.CSServer(" + GlobalVars.RobloxPort + "," + GlobalVars.PlayerLimit + "," + md5s + ")";
        } else if (type == ScriptType.Solo || type == ScriptType.EasterEgg) {
            if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true) {
                return "_G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
            } else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true) {
                return "_G.CSSolo(" + GlobalVars.UserID + ",'Player'," + GlobalVars.sololoadtext + ")";
            } else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false) {
                return "_G.CSSolo(0,'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
            } else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false) {
                return "_G.CSSolo(0,'Player'," + GlobalVars.sololoadtext + ")";
            } else {
                return "_G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
            }
        } else if (type == ScriptType.Studio) {
			return "_G.CSStudio()";
		} else {
			return "";
		}
	}
		
	public static string GetNameForType(ScriptType type)
	{
        if (type == ScriptType.Client) {
            return "Client";
        } else if (type == ScriptType.Server) {
            return "Server";
        } else if (type == ScriptType.Solo) {
            return "Play Solo";
        } else if (type == ScriptType.Studio) {
            return "Studio";
        } else if (type == ScriptType.EasterEgg) {
			return "A message from Bitl";
		} else {
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