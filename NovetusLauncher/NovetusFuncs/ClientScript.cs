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
		try {
			string result = source.Substring(0, source.IndexOf(seperator));
				
			if (File.Exists(GlobalVars.MapsDir + @"\\" + result + @"\\" + source)) {
				return result + @"\\" + source;
			} else {
				return source;
			}
		} catch (Exception) {
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
                .Replace("%charapp%", GlobalVars.UserCustomization.CharacterID)
                .Replace("%ip%", GlobalVars.IP)
                .Replace("%port%", GlobalVars.RobloxPort.ToString())
                .Replace("%name%", GlobalVars.PlayerName)
                .Replace("%icone%", ConvertIconStringToInt().ToString())
                .Replace("%icon%", GlobalVars.UserCustomization.Icon)
                .Replace("%id%", GlobalVars.UserID.ToString())
                .Replace("%face%", GlobalVars.UserCustomization.Face)
                .Replace("%head%", GlobalVars.UserCustomization.Head)
                .Replace("%tshirt%", GlobalVars.UserCustomization.TShirt)
                .Replace("%shirt%", GlobalVars.UserCustomization.Shirt)
                .Replace("%pants%", GlobalVars.UserCustomization.Pants)
                .Replace("%hat1%", GlobalVars.UserCustomization.Hat1)
                .Replace("%hat2%", GlobalVars.UserCustomization.Hat2)
                .Replace("%hat3%", GlobalVars.UserCustomization.Hat3)
                .Replace("%faced%", GlobalVars.faceGameDir + GlobalVars.UserCustomization.Face)
                .Replace("%headd%", GlobalVars.headGameDir + GlobalVars.UserCustomization.Head)
                .Replace("%tshirtd%", GlobalVars.UserCustomization.TShirt.Contains("http://") ? GlobalVars.UserCustomization.TShirt : GlobalVars.tshirtGameDir + GlobalVars.UserCustomization.TShirt)
                .Replace("%shirtd%", GlobalVars.UserCustomization.Shirt.Contains("http://") ? GlobalVars.UserCustomization.Shirt : GlobalVars.shirtGameDir + GlobalVars.UserCustomization.Shirt)
                .Replace("%pantsd%", GlobalVars.UserCustomization.Pants.Contains("http://") ? GlobalVars.UserCustomization.Pants : GlobalVars.pantsGameDir + GlobalVars.UserCustomization.Pants)
                .Replace("%hat1d%", GlobalVars.hatGameDir + GlobalVars.UserCustomization.Hat1)
                .Replace("%hat2d%", GlobalVars.hatGameDir + GlobalVars.UserCustomization.Hat2)
                .Replace("%hat3d%", GlobalVars.hatGameDir + GlobalVars.UserCustomization.Hat3)
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
                .Replace("%limit%", GlobalVars.PlayerLimit.ToString())
                .Replace("%extra%", GlobalVars.UserCustomization.Extra)
				.Replace("%hat4%", GlobalVars.UserCustomization.Extra)
				.Replace("%extrad%", GlobalVars.extraGameDir + GlobalVars.UserCustomization.Extra)
                .Replace("%hat4d%", GlobalVars.hatGameDir + GlobalVars.UserCustomization.Extra)
                .Replace("%args%", GetRawArgsFromTag(tag, md5s, luafile))
                .Replace("%facews%", GlobalVars.WebServer_FaceDir + GlobalVars.UserCustomization.Face)
                .Replace("%headws%", GlobalVars.WebServer_HeadDir + GlobalVars.UserCustomization.Head)
                .Replace("%tshirtws%", GlobalVars.UserCustomization.TShirt.Contains("http://") ? GlobalVars.UserCustomization.TShirt : GlobalVars.WebServer_TShirtDir + GlobalVars.UserCustomization.TShirt)
                .Replace("%shirtws%", GlobalVars.UserCustomization.Shirt.Contains("http://") ? GlobalVars.UserCustomization.Shirt : GlobalVars.WebServer_ShirtDir + GlobalVars.UserCustomization.Shirt)
                .Replace("%pantsws%", GlobalVars.UserCustomization.Pants.Contains("http://") ? GlobalVars.UserCustomization.Pants : GlobalVars.WebServer_PantsDir + GlobalVars.UserCustomization.Pants)
                .Replace("%hat1ws%", GlobalVars.WebServer_HatDir + GlobalVars.UserCustomization.Hat1)
                .Replace("%hat2ws%", GlobalVars.WebServer_HatDir + GlobalVars.UserCustomization.Hat2)
                .Replace("%hat3ws%", GlobalVars.WebServer_HatDir + GlobalVars.UserCustomization.Hat3)
                .Replace("%extraws%", GlobalVars.WebServer_ExtraDir + GlobalVars.UserCustomization.Extra)
                .Replace("%hat4ws%", GlobalVars.WebServer_HatDir + GlobalVars.UserCustomization.Extra)
                .Replace("%mapfiled%", GlobalVars.BaseGameDir + GlobalVars.MapPathSnip.Replace(@"\\", @"\"))
                .Replace("%tripcode%", GlobalVars.PlayerTripcode)
                .Replace("%addonscriptpath%", GlobalVars.AddonScriptPath);
        return compiled;
	}
}