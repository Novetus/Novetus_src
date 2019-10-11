/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using System.IO;
using System.Reflection;

public static class GlobalVars
{
	public static string RootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
	public static string BasePath = RootPath.Replace(@"\", @"\\");
	public static string DataPath = BasePath + "\\shareddata";
	public static string ConfigDir = BasePath + "\\config";
	public static string ClientDir = BasePath + "\\clients";
	public static string MapsDir = BasePath + "\\maps";
	public static string CustomPlayerDir = DataPath + "\\charcustom";
	public static string IP = "localhost";
	public static string Version = "";
	public static string SharedArgs = "";
	public static string ScriptName = "CSMPFunctions";
	public static string ScriptGenName = "CSMPBoot";
	public static SimpleHTTPServer WebServer = null;
	public static bool IsWebServerOn = false;
	//vars for loader
	public static bool ReadyToLaunch = false;
	//server settings.
	public static bool UPnP = false;
	public static string Map = "";
	public static string FullMapPath = "";
	public static int RobloxPort = 53640;
	public static int DefaultRobloxPort = 53640;
	public static int WebServer_Port = (RobloxPort + 1);
	public static int PlayerLimit = 12;
	//player settings
	public static int UserID = 0;
	public static string PlayerName = "Player";
	//launcher settings.
	public static bool CloseOnLaunch = false;
	public static bool LocalPlayMode = false;
	//client shit
	public static string SelectedClient = "";
	public static string DefaultClient = "";
	public static string DefaultMap = "";
	public static bool UsesPlayerName = false;
	public static bool UsesID = true;
	public static string SelectedClientDesc = "";
	public static string Warning = "";
	public static bool LegacyMode = false;
	public static string SelectedClientMD5 = "";
	public static string SelectedClientScriptMD5 = "";
	public static bool FixScriptMapMode = false;
	public static bool AlreadyHasSecurity = false;
	public static string CustomArgs = "";
	//charcustom
	public static string Custom_Hat1ID_Offline = "NoHat.rbxm";
	public static string Custom_Hat2ID_Offline = "NoHat.rbxm";
	public static string Custom_Hat3ID_Offline = "NoHat.rbxm";
	public static string Custom_Face_Offline = "DefaultFace.rbxm";
	public static string Custom_Head_Offline = "DefaultHead.rbxm";
	public static string Custom_T_Shirt_Offline = "NoTShirt.rbxm";
	public static string Custom_Shirt_Offline = "NoShirt.rbxm";
	public static string Custom_Pants_Offline = "NoPants.rbxm";
	public static string Custom_Icon_Offline = "NBC";
	public static int HeadColorID = 24;
	public static int TorsoColorID = 23;
	public static int LeftArmColorID = 24;
	public static int RightArmColorID = 24;
	public static int LeftLegColorID = 119;
	public static int RightLegColorID = 119;
	public static string loadtext = "";
	public static string sololoadtext = "";
	public static string CharacterID = "";
	public static string Custom_Extra = "NoExtra.rbxm";
	public static bool Custom_Extra_ShowHats = false;
	public static bool Custom_Extra_SelectionIsHat = false;
	//color menu.
	public static string ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
	public static string ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
	public static string ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
	public static string ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
	public static string ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
	public static string ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
	public static bool AdminMode = false;
	public static string important = "";
	//discord
	public static DiscordRpc.RichPresence presence;
	public static string appid = "505955125727330324";
	public static string imagekey_large = "novetus_large";
	//webserver
	public static string WebServerURI = "http://" + IP + ":" + (WebServer_Port).ToString();
	public static string LocalWebServerURI = "http://localhost:" + (WebServer_Port).ToString();
	public static string WebServer_CustomPlayerDir = WebServerURI + "/charcustom/";
	public static string WebServer_HatDir = WebServer_CustomPlayerDir + "hats/";
	public static string WebServer_FaceDir = WebServer_CustomPlayerDir + "faces/";
	public static string WebServer_HeadDir = WebServer_CustomPlayerDir + "heads/";
	public static string WebServer_TShirtDir = WebServer_CustomPlayerDir + "tshirts/";
	public static string WebServer_ShirtDir = WebServer_CustomPlayerDir + "shirts/";
	public static string WebServer_PantsDir = WebServer_CustomPlayerDir + "pants/";
	public static string WebServer_ExtraDir = WebServer_CustomPlayerDir + "custom/";
	public static string WebServer_BodyColors = WebServer_CustomPlayerDir + "bodycolors.rbxm";
	//itemmaker
	public static bool DisabledHelp = false;
		
	public static string MultiLine(params string[] args)
	{
		return string.Join(Environment.NewLine, args);
	}
}