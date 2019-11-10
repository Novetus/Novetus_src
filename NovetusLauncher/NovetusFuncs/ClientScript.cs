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
using System.Linq;
using System.Globalization;

public class ClientScript
{	
	public static string GetArgsFromTag(string code, string tag, string endtag)
	{
		int pFrom = code.IndexOf(tag) + tag.Length;
		int pTo = code.LastIndexOf(endtag);

		string result = code.Substring(pFrom, pTo - pFrom);
			
		return result;
	}
		
	public static ScriptGenerator.ScriptType GetTypeFromTag(string tag, string endtag)
	{
		if (tag.Contains("client") && endtag.Contains("client")) {
			return ScriptGenerator.ScriptType.Client;
		} else if (tag.Contains("server") && endtag.Contains("server") || tag.Contains("no3d") && endtag.Contains("no3d")) {
			return ScriptGenerator.ScriptType.Server;
		} else if (tag.Contains("solo") && endtag.Contains("solo")) {
			return ScriptGenerator.ScriptType.Solo;
		} else if (tag.Contains("studio") && endtag.Contains("studio")) {
			return ScriptGenerator.ScriptType.Studio;
		} else {
			return ScriptGenerator.ScriptType.None;
		}
	}
		
	public static string GetRawArgsForType(ScriptGenerator.ScriptType type, string md5s, string luafile)
	{
		if (type == ScriptGenerator.ScriptType.Client) {
			if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true) {
				return "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
			} else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true) {
				return "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
			} else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false) {
				return "dofile('" + luafile + "'); _G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
			} else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false) {
				return "dofile('" + luafile + "'); _G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
			} else {
				return "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
			}
		} else if (type == ScriptGenerator.ScriptType.Server) {
			return "dofile('" + luafile + "'); _G.CSServer(" + GlobalVars.RobloxPort + "," + GlobalVars.PlayerLimit + "," + md5s + ")";
		} else if (type == ScriptGenerator.ScriptType.Solo) {
			if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true) {
				return "dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
			} else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true) {
				return "dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'Player'," + GlobalVars.sololoadtext + ")";
			} else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false) {
				return "dofile('" + luafile + "'); _G.CSSolo(0,'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
			} else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false) {
				return "dofile('" + luafile + "'); _G.CSSolo(0,'Player'," + GlobalVars.sololoadtext + ")";
			} else {
				return "dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
			}
		} else if (type == ScriptGenerator.ScriptType.Studio) {
			return "dofile('" + luafile + "');";
		} else {
			return "";
		}
	}
		
	public static string GetRawArgsFromTag(string tag, string endtag, string md5s, string luafile)
	{	
		return GetRawArgsForType(GetTypeFromTag(tag, endtag), md5s, luafile);
	}
		
	public static int ConvertIconStringToInt()
	{
		if (GlobalVars.Custom_Icon_Offline == "BC") {
			return 1;
		} else if (GlobalVars.Custom_Icon_Offline == "TBC") {
			return 2;
		} else if (GlobalVars.Custom_Icon_Offline == "OBC") {
			return 3;
		} else if (GlobalVars.Custom_Icon_Offline == "NBC") {
			return 0;				
		}
			
		return 0;
	}
		
	public static string GetFolderAndMapName(string source, string seperator)
	{
		try {
			string result = source.Substring(0, source.IndexOf(seperator));
				
			if (File.Exists(GlobalVars.MapsDir + @"\\" + result + @"\\" + source)) {
				return result + @"\\" + source;
			} else {
				return "";
			}
		} catch (Exception) {
			return "";
		}
	}

    public static string GetFolderAndMapName(string source)
    {
        return GetFolderAndMapName(source, " -");
    }


    public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe)
	{
		if (GlobalVars.FixScriptMapMode) {
			ScriptGenerator.GenerateScriptForClient(GetTypeFromTag(tag, endtag), GlobalVars.SelectedClient);
		}
			
		string extractedCode = GetArgsFromTag(code, tag, endtag);
			
		string md5dir = GlobalVars.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location) : "";
		string md5script = GlobalVars.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") : "";
		string md5exe = GlobalVars.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(rbxexe) : "";
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
				.Replace("%tshirtd%", GlobalVars.tshirtGameDir + GlobalVars.Custom_T_Shirt_Offline)
				.Replace("%shirtd%", GlobalVars.shirtGameDir + GlobalVars.Custom_Shirt_Offline)
				.Replace("%pantsd%", GlobalVars.pantsGameDir + GlobalVars.Custom_Pants_Offline)
				.Replace("%hat1d%", GlobalVars.hatGameDir + GlobalVars.Custom_Hat1ID_Offline)
				.Replace("%hat2d%", GlobalVars.hatGameDir + GlobalVars.Custom_Hat2ID_Offline)
				.Replace("%hat3d%", GlobalVars.hatGameDir + GlobalVars.Custom_Hat3ID_Offline)
				.Replace("%headcolor%", GlobalVars.HeadColorID.ToString())
				.Replace("%torsocolor%", GlobalVars.TorsoColorID.ToString())
				.Replace("%larmcolor%", GlobalVars.LeftArmColorID.ToString())
				.Replace("%llegcolor%", GlobalVars.LeftLegColorID.ToString())
				.Replace("%rarmcolor%", GlobalVars.RightArmColorID.ToString())
				.Replace("%rlegcolor%", GlobalVars.RightLegColorID.ToString())
				.Replace("%rlegcolor%", GlobalVars.SelectedClientMD5)
				.Replace("%md5launcher%", md5dir)
				.Replace("%md5script%", GlobalVars.SelectedClientMD5)
				.Replace("%md5exe%", GlobalVars.SelectedClientScriptMD5)
				.Replace("%md5scriptd%", md5script)
				.Replace("%md5exed%", md5exe)
				.Replace("%limit%", GlobalVars.PlayerLimit.ToString())
				.Replace("%extra%", GlobalVars.Custom_Extra)
				.Replace("%extrad%", GlobalVars.extraGameDir + GlobalVars.Custom_Extra)
				.Replace("%hat4d%", GlobalVars.hatGameDir + GlobalVars.Custom_Extra)
				.Replace("%args%", GetRawArgsFromTag(tag, endtag, md5s, luafile))
				.Replace("%facews%", GlobalVars.WebServer_FaceDir + GlobalVars.Custom_Face_Offline)
				.Replace("%headws%", GlobalVars.WebServer_HeadDir + GlobalVars.Custom_Head_Offline)
				.Replace("%tshirtws%", GlobalVars.WebServer_TShirtDir + GlobalVars.Custom_T_Shirt_Offline)
				.Replace("%shirtws%", GlobalVars.WebServer_ShirtDir + GlobalVars.Custom_Shirt_Offline)
				.Replace("%pantsws%", GlobalVars.WebServer_PantsDir + GlobalVars.Custom_Pants_Offline)
				.Replace("%hat1ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat1ID_Offline)
				.Replace("%hat2ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat2ID_Offline)
				.Replace("%hat3ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat3ID_Offline)
				.Replace("%extraws%", GlobalVars.WebServer_ExtraDir + GlobalVars.Custom_Extra)
				.Replace("%hat4ws%", GlobalVars.WebServer_HatDir + GlobalVars.Custom_Extra)
				.Replace("%bodycolors%", GlobalVars.WebServer_BodyColors)
				.Replace("%mapfiled%", GlobalVars.MapGameDir + GetFolderAndMapName(GlobalVars.Map));
		return compiled;
	}
}