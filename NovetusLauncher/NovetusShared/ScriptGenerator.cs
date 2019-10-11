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
using System.Linq;

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
	public ScriptGenerator()
	{
	}
		
	public enum ScriptType
	{
		Client = 0,
		Server = 1,
		Solo = 2,
		Studio = 3,
		None = 4
	}
		
	public static string GetScriptFuncForType(ScriptType type, string client)
	{
		string rbxexe = "";
		if (GlobalVars.LegacyMode == true) {
			rbxexe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\clients\\" + client + @"\\RobloxApp.exe";
		} else {
			rbxexe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\clients\\" + client + @"\\RobloxApp_client.exe";
		}
			
		string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
		string md5script = SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua");
		string md5exe = SecurityFuncs.CalculateMD5(rbxexe);
		string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
		if (type == ScriptType.Client) {
			if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true) {
				return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
			} else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true) {
				return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
			} else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false) {
				return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
			} else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false) {
				return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
			} else {
				return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
			}
		} else if (type == ScriptType.Server) {
			return "_G.CSServer(" + GlobalVars.RobloxPort + "," + GlobalVars.PlayerLimit + "," + md5s + ")";
		} else if (type == ScriptType.Solo) {
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
			return "";
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
		} else {
			return "";
		}
	}
		
	/*
		public static string[] GetScriptContents(string scriptPath)
		{
			List<string> array = new List<string>();
			string line = "";
         	using (StreamReader sr = new StreamReader(scriptPath)) 
         	{
            	while ((line = sr.ReadLine()) != null) 
            	{
            		array.Add(line);
            	}
         	}
         	
         	return array.ToArray();
		}
		*/
		
	private static void ReadConfigValues()
	{
		LauncherFuncs.ReadConfigValues(GlobalVars.ConfigDir + "\\config.ini");
	}

	public static void GenerateScriptForClient(ScriptType type, string client)
	{
		//next, generate the header functions.

		ReadConfigValues();
			
		//string scriptcontents = MultiLine(GetScriptContents(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua"));

		string code = GlobalVars.MultiLine(
			               "--Load Script",
					//scriptcontents,
			               "dofile('rbxasset://scripts/" + GlobalVars.ScriptName + ".lua')",
			               GetScriptFuncForType(type, client)
		               );
			
		List<string> list = new List<string>(Regex.Split(code, Environment.NewLine));
		string[] convertedList = list.ToArray();
		File.WriteAllLines(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptGenName + ".lua", convertedList);
	}
		
	// using this for a possible 2006 preset feature??
		
	/*
		public static string GeneratePlayerColorPresetString(int preset)
		{
			int HeadColor = 0;
			int TorsoColor = 0;
			int LArmColor = 0;
			int RArmColor = 0;
			int LLegColor = 0;
			int RLegColor = 0;
			
			if (preset == 1)
			{
				HeadColor = 24;
				TorsoColor = 194;
				LArmColor = 24;
				RArmColor = 24;
				LLegColor = 119;
				RLegColor = 119;
			}
			else if (preset == 2)
			{
				HeadColor = 24;
				TorsoColor = 22;
				LArmColor = 24;
				RArmColor = 24;
				LLegColor = 9;
				RLegColor = 9;
			}
			else if (preset == 3)
			{
				HeadColor = 24;
				TorsoColor = 23;
				LArmColor = 24;
				RArmColor = 24;
				LLegColor = 119;
				RLegColor = 119;
			}
			else if (preset == 4)
			{
				HeadColor = 24;
				TorsoColor = 22;
				LArmColor = 24;
				RArmColor = 24;
				LLegColor = 119;
				RLegColor = 119;
			}
			else if (preset == 5)
			{
				HeadColor = 24;
				TorsoColor = 11;
				LArmColor = 24;
				RArmColor = 24;
				LLegColor = 119;
				RLegColor = 119;
			}
			else if (preset == 6)
			{
				HeadColor = 38;
				TorsoColor = 194;
				LArmColor = 38;
				RArmColor = 38;
				LLegColor = 119;
				RLegColor = 119;
			}
			else if (preset == 7)
			{
				HeadColor = 128;
				TorsoColor = 119;
				LArmColor = 128;
				RArmColor = 128;
				LLegColor = 119;
				RLegColor = 119;
			}
			else if (preset == 8)
			{
				HeadColor = 9;
				TorsoColor = 194;
				LArmColor = 9;
				RArmColor = 9;
				LLegColor = 119;
				RLegColor = 119;
			}
			
			string output = MultiLine(
					"--Color Settings",
                	"HeadColorID = " + HeadColor,
                	"TorsoColorID = " + TorsoColor,
                	"LeftArmColorID = " + LArmColor,
                	"RightArmColorID = " + RArmColor,
                	"LeftLegColorID = " + LLegColor,
                	"RightLegColorID = " + RLegColor
            		);
			
			return output;
		}
		*/
}