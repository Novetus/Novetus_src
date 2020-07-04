/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:02 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Reflection;

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
        catch (Exception) when (!Env.Debugging)
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
				return LauncherFuncs.ChangeGameSettings() +
						" dofile('" + luafile + "'); _G.CSConnect("
						+ (GlobalVars.SelectedClientInfo.UsesID == true ? GlobalVars.UserID : 0) + ",'"
						+ GlobalVars.IP + "',"
						+ GlobalVars.RobloxPort + ",'"
						+ (GlobalVars.SelectedClientInfo.UsesPlayerName == true ? GlobalVars.PlayerName : "Player") + "',"
						+ GlobalVars.loadtext + ","
						+ md5s + ",'"
						+ GlobalVars.PlayerTripcode + "')";
			case ScriptType.Server:
				return LauncherFuncs.ChangeGameSettings() + 
						" dofile('" + luafile + "'); _G.CSServer(" 
						+ GlobalVars.RobloxPort + "," 
						+ GlobalVars.PlayerLimit + "," 
						+ md5s + "); " 
						+ (!string.IsNullOrWhiteSpace(GlobalVars.AddonScriptPath) ? LauncherFuncs.ChangeGameSettings() + 
						" dofile('" + GlobalVars.AddonScriptPath + "');" : "");
			case ScriptType.Solo:
			case ScriptType.EasterEgg:
				return LauncherFuncs.ChangeGameSettings() 
						+ " dofile('" + luafile + "'); _G.CSSolo(" 
						+ (GlobalVars.SelectedClientInfo.UsesID == true ? GlobalVars.UserID : 0) + ",'" 
						+ (GlobalVars.SelectedClientInfo.UsesPlayerName == true ? GlobalVars.PlayerName : "Player") + "'," 
						+ GlobalVars.sololoadtext + ")";
			case ScriptType.Studio:
				return LauncherFuncs.ChangeGameSettings() 
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
		switch (GlobalVars.Custom_Icon_Offline)
		{
			case "BC:":
				return 1;
			case "TBC:":
				return 2;
			case "OBC:":
				return 3;
			case "NBC:":
			default:
				return 0;
		}
	}
		
	public static string GetFolderAndMapName(string source, string seperator)
	{
		try {
			string result = source.Substring(0, source.IndexOf(seperator));
				
			if (File.Exists(GlobalVars.MapsDir + @"\\" + result + @"\\" + source)) {
				return result + @"\\" + source;
			} else {
				return source;
			}
		} catch (Exception) when (!Env.Debugging) {
			return source;
		}
	}

    public static string GetFolderAndMapName(string source)
    {
        return GetFolderAndMapName(source, " -");
    }

    public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe)
	{
		if (GlobalVars.SelectedClientInfo.Fix2007) {
			ScriptGenerator.GenerateScriptForClient(GetTypeFromTag(tag));
		}
			
		string extractedCode = GetArgsFromTag(code, tag, endtag);

        if (extractedCode.Contains("%donothing%"))
        {
            return "";
        }

        string md5dir = GlobalVars.SelectedClientInfo.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location) : "";
		string md5script = GlobalVars.SelectedClientInfo.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") : "";
		string md5exe = GlobalVars.SelectedClientInfo.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(rbxexe) : "";
		string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
        string compiled = extractedCode.Replace("%mapfile%", mapfile)
                .Replace("%luafile%", luafile)
                .Replace("%charapp%", GlobalVars.CharacterID)
                .Replace("%ip%", GlobalVars.IP)
                .Replace("%port%", GlobalVars.RobloxPort.ToString())
                .Replace("%name%", GlobalVars.PlayerName)
                .Replace("%icone%", ConvertIconStringToInt().ToString())
                .Replace("%icon%", GlobalVars.Custom_Icon_Offline)
                .Replace("%id%", GlobalVars.UserID.ToString())
                .Replace("%face%", GlobalVars.Custom_Face_Offline)
                .Replace("%head%", GlobalVars.Custom_Head_Offline)
                .Replace("%tshirt%", GlobalVars.Custom_T_Shirt_Offline)
                .Replace("%shirt%", GlobalVars.Custom_Shirt_Offline)
                .Replace("%pants%", GlobalVars.Custom_Pants_Offline)
                .Replace("%hat1%", GlobalVars.Custom_Hat1ID_Offline)
                .Replace("%hat2%", GlobalVars.Custom_Hat2ID_Offline)
                .Replace("%hat3%", GlobalVars.Custom_Hat3ID_Offline)
                .Replace("%faced%", GlobalVars.faceGameDir + GlobalVars.Custom_Face_Offline)
                .Replace("%headd%", GlobalVars.headGameDir + GlobalVars.Custom_Head_Offline)
                .Replace("%tshirtd%", GlobalVars.Custom_T_Shirt_Offline.Contains("http://") ? GlobalVars.Custom_T_Shirt_Offline : GlobalVars.tshirtGameDir + GlobalVars.Custom_T_Shirt_Offline)
                .Replace("%shirtd%", GlobalVars.Custom_Shirt_Offline.Contains("http://") ? GlobalVars.Custom_Shirt_Offline : GlobalVars.shirtGameDir + GlobalVars.Custom_Shirt_Offline)
                .Replace("%pantsd%", GlobalVars.Custom_Pants_Offline.Contains("http://") ? GlobalVars.Custom_Pants_Offline : GlobalVars.pantsGameDir + GlobalVars.Custom_Pants_Offline)
                .Replace("%hat1d%", GlobalVars.hatGameDir + GlobalVars.Custom_Hat1ID_Offline)
                .Replace("%hat2d%", GlobalVars.hatGameDir + GlobalVars.Custom_Hat2ID_Offline)
                .Replace("%hat3d%", GlobalVars.hatGameDir + GlobalVars.Custom_Hat3ID_Offline)
                .Replace("%headcolor%", GlobalVars.HeadColorID.ToString())
                .Replace("%torsocolor%", GlobalVars.TorsoColorID.ToString())
                .Replace("%larmcolor%", GlobalVars.LeftArmColorID.ToString())
                .Replace("%llegcolor%", GlobalVars.LeftLegColorID.ToString())
                .Replace("%rarmcolor%", GlobalVars.RightArmColorID.ToString())
                .Replace("%rlegcolor%", GlobalVars.RightLegColorID.ToString())
                .Replace("%md5launcher%", md5dir)
                .Replace("%md5script%", GlobalVars.SelectedClientInfo.ClientMD5)
                .Replace("%md5exe%", GlobalVars.SelectedClientInfo.ScriptMD5)
                .Replace("%md5scriptd%", md5script)
                .Replace("%md5exed%", md5exe)
                .Replace("%limit%", GlobalVars.PlayerLimit.ToString())
                .Replace("%extra%", GlobalVars.Custom_Extra)
                .Replace("%extrad%", GlobalVars.extraGameDir + GlobalVars.Custom_Extra)
                .Replace("%hat4d%", GlobalVars.hatGameDir + GlobalVars.Custom_Extra)
                .Replace("%args%", GetRawArgsFromTag(tag, md5s, luafile))
                .Replace("%facews%", GlobalVars.WebServer_FaceDir + GlobalVars.Custom_Face_Offline)
                .Replace("%headws%", GlobalVars.WebServer_HeadDir + GlobalVars.Custom_Head_Offline)
                .Replace("%tshirtws%", GlobalVars.Custom_T_Shirt_Offline.Contains("http://") ? GlobalVars.Custom_T_Shirt_Offline : GlobalVars.WebServer_TShirtDir + GlobalVars.Custom_T_Shirt_Offline)
                .Replace("%shirtws%", GlobalVars.Custom_Shirt_Offline.Contains("http://") ? GlobalVars.Custom_Shirt_Offline : GlobalVars.WebServer_ShirtDir + GlobalVars.Custom_Shirt_Offline)
                .Replace("%pantsws%", GlobalVars.Custom_Pants_Offline.Contains("http://") ? GlobalVars.Custom_Pants_Offline : GlobalVars.WebServer_PantsDir + GlobalVars.Custom_Pants_Offline)
                .Replace("%hat1ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat1ID_Offline)
                .Replace("%hat2ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat2ID_Offline)
                .Replace("%hat3ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat3ID_Offline)
                .Replace("%extraws%", GlobalVars.WebServer_ExtraDir + GlobalVars.Custom_Extra)
                .Replace("%hat4ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Extra)
                .Replace("%mapfiled%", GlobalVars.BaseGameDir + GlobalVars.MapPathSnip.Replace(@"\\", @"\"))
                .Replace("%tripcode%", GlobalVars.PlayerTripcode)
                .Replace("%addonscriptpath%", GlobalVars.AddonScriptPath);
        return compiled;
	}
}