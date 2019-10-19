/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 10:24 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class LauncherFuncs
{
	public LauncherFuncs()
	{
	}
		
	public static void ReadConfigValues(string cfgpath)
	{
		string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline9, Decryptline10, Decryptline11;
			
		IniFile ini = new IniFile(cfgpath);
			
		string section = "Config";
			
		Decryptline1 = ini.IniReadValue(section, "CloseOnLaunch");
			
		if (string.IsNullOrWhiteSpace(Decryptline1)) {
			ini.IniWriteValue(section, "CloseOnLaunch", GlobalVars.CloseOnLaunch.ToString());
		}
			
		Decryptline2 = ini.IniReadValue(section, "UserID");
			
		if (string.IsNullOrWhiteSpace(Decryptline2)) {
			ini.IniWriteValue(section, "UserID", GlobalVars.UserID.ToString());
		}
			
		Decryptline3 = ini.IniReadValue(section, "PlayerName");
    		
		if (string.IsNullOrWhiteSpace(Decryptline3)) {
			ini.IniWriteValue(section, "PlayerName", GlobalVars.PlayerName.ToString());
		}
    		
		Decryptline4 = ini.IniReadValue(section, "SelectedClient");
			
		if (string.IsNullOrWhiteSpace(Decryptline4)) {
			ini.IniWriteValue(section, "SelectedClient", GlobalVars.SelectedClient.ToString());
		}
    		
		Decryptline5 = ini.IniReadValue(section, "Map");
    		
		if (string.IsNullOrWhiteSpace(Decryptline5)) {
			ini.IniWriteValue(section, "Map", GlobalVars.Map.ToString());
		}
    		
		Decryptline6 = ini.IniReadValue(section, "RobloxPort");
    		
		if (string.IsNullOrWhiteSpace(Decryptline6)) {
			ini.IniWriteValue(section, "RobloxPort", GlobalVars.RobloxPort.ToString());
		}
    		
		Decryptline7 = ini.IniReadValue(section, "PlayerLimit");
    		
		if (string.IsNullOrWhiteSpace(Decryptline7)) {
			ini.IniWriteValue(section, "PlayerLimit", GlobalVars.PlayerLimit.ToString());
		}
    		
		Decryptline9 = ini.IniReadValue(section, "ShowHatsOnExtra");
    		
		if (string.IsNullOrWhiteSpace(Decryptline9)) {
			ini.IniWriteValue(section, "ShowHatsOnExtra", GlobalVars.Custom_Extra_ShowHats.ToString());
		}
    		
		Decryptline10 = ini.IniReadValue(section, "UPnP");
    		
		if (string.IsNullOrWhiteSpace(Decryptline10)) {
			ini.IniWriteValue(section, "UPnP", GlobalVars.UPnP.ToString());
		}
    		
		Decryptline11 = ini.IniReadValue(section, "ItemMakerDisableHelpMessage");
    		
		if (string.IsNullOrWhiteSpace(Decryptline11)) {
			ini.IniWriteValue(section, "ItemMakerDisableHelpMessage", GlobalVars.DisabledHelp.ToString());
		}
    		
		bool bline1 = Convert.ToBoolean(Decryptline1);
		GlobalVars.CloseOnLaunch = bline1;
			
		int iline2 = Convert.ToInt32(Decryptline2);
		GlobalVars.UserID = iline2;
			
		GlobalVars.PlayerName = Decryptline3;
			
		GlobalVars.SelectedClient = Decryptline4;
			
		GlobalVars.Map = Decryptline5;
			
		int iline6 = Convert.ToInt32(Decryptline6);
		GlobalVars.RobloxPort = iline6;
			
		int iline7 = Convert.ToInt32(Decryptline7);
		GlobalVars.PlayerLimit = iline7;
			
		bool bline9 = Convert.ToBoolean(Decryptline9);
		GlobalVars.Custom_Extra_ShowHats = bline9;
			
		bool bline10 = Convert.ToBoolean(Decryptline10);
		GlobalVars.UPnP = bline10;
			
		bool bline11 = Convert.ToBoolean(Decryptline11);
		GlobalVars.DisabledHelp = bline11;
			
		ReadCustomizationValues(cfgpath.Replace(".ini", "_customization.ini"));
	}
		
	public static void WriteConfigValues(string cfgpath)
	{
		IniFile ini = new IniFile(cfgpath);
			
		string section = "Config";
			
		ini.IniWriteValue(section, "CloseOnLaunch", GlobalVars.CloseOnLaunch.ToString());
		ini.IniWriteValue(section, "UserID", GlobalVars.UserID.ToString());
		ini.IniWriteValue(section, "PlayerName", GlobalVars.PlayerName.ToString());
		ini.IniWriteValue(section, "SelectedClient", GlobalVars.SelectedClient.ToString());
		ini.IniWriteValue(section, "Map", GlobalVars.Map.ToString());
		ini.IniWriteValue(section, "RobloxPort", GlobalVars.RobloxPort.ToString());
		ini.IniWriteValue(section, "PlayerLimit", GlobalVars.PlayerLimit.ToString());
		ini.IniWriteValue(section, "ShowHatsOnExtra", GlobalVars.Custom_Extra_ShowHats.ToString());
		ini.IniWriteValue(section, "UPnP", GlobalVars.UPnP.ToString());
		ini.IniWriteValue(section, "ItemMakerDisableHelpMessage", GlobalVars.DisabledHelp.ToString());
		WriteCustomizationValues(cfgpath.Replace(".ini", "_customization.ini"));
	}
		
	public static void ResetConfigValues()
	{
		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
		GlobalVars.Map = GlobalVars.DefaultMap;
		GlobalVars.CloseOnLaunch = false;
		GlobalVars.UserID = 0;
		GlobalVars.PlayerName = "Player";
		GlobalVars.SelectedClient = GlobalVars.DefaultClient;
		GlobalVars.Map = GlobalVars.DefaultMap;
		GlobalVars.RobloxPort = 53640;
		GlobalVars.PlayerLimit = 12;
		GlobalVars.Custom_Extra_ShowHats = false;
		GlobalVars.UPnP = false;
		ResetCustomizationValues();
	}
		
	public static void ReadCustomizationValues(string cfgpath)
	{
		string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, Decryptline10, Decryptline11, Decryptline12, Decryptline13, Decryptline14, Decryptline15, Decryptline16, Decryptline17, Decryptline18, Decryptline19, Decryptline20, Decryptline21, Decryptline22, Decryptline23, Decryptline24;
			
		IniFile ini = new IniFile(cfgpath);
			
		string section = "Items";
			
		Decryptline1 = ini.IniReadValue(section, "Hat1");
			
		if (string.IsNullOrWhiteSpace(Decryptline1)) {
			ini.IniWriteValue(section, "Hat1", GlobalVars.Custom_Hat1ID_Offline.ToString());
		}
			
		Decryptline2 = ini.IniReadValue(section, "Hat2");
			
		if (string.IsNullOrWhiteSpace(Decryptline2)) {
			ini.IniWriteValue(section, "Hat2", GlobalVars.Custom_Hat2ID_Offline.ToString());
		}
			
		Decryptline3 = ini.IniReadValue(section, "Hat3");
			
		if (string.IsNullOrWhiteSpace(Decryptline3)) {
			ini.IniWriteValue(section, "Hat3", GlobalVars.Custom_Hat3ID_Offline.ToString());
		}
			
		Decryptline16 = ini.IniReadValue(section, "Face");
			
		if (string.IsNullOrWhiteSpace(Decryptline16)) {
			ini.IniWriteValue(section, "Face", GlobalVars.Custom_Face_Offline.ToString());
		}
			
		Decryptline17 = ini.IniReadValue(section, "Head");
			
		if (string.IsNullOrWhiteSpace(Decryptline17)) {
			ini.IniWriteValue(section, "Head", GlobalVars.Custom_Head_Offline.ToString());
		}
			
		Decryptline18 = ini.IniReadValue(section, "TShirt");
			
		if (string.IsNullOrWhiteSpace(Decryptline18)) {
			ini.IniWriteValue(section, "TShirt", GlobalVars.Custom_T_Shirt_Offline.ToString());
		}
			
		Decryptline19 = ini.IniReadValue(section, "Shirt");
			
		if (string.IsNullOrWhiteSpace(Decryptline19)) {
			ini.IniWriteValue(section, "Shirt", GlobalVars.Custom_Shirt_Offline.ToString());
		}
			
		Decryptline20 = ini.IniReadValue(section, "Pants");
			
		if (string.IsNullOrWhiteSpace(Decryptline20)) {
			ini.IniWriteValue(section, "Pants", GlobalVars.Custom_Pants_Offline.ToString());
		}
			
		Decryptline21 = ini.IniReadValue(section, "Icon");
			
		if (string.IsNullOrWhiteSpace(Decryptline21)) {
			ini.IniWriteValue(section, "Icon", GlobalVars.Custom_Icon_Offline.ToString());
		}
			
		Decryptline23 = ini.IniReadValue(section, "Extra");
			
		if (string.IsNullOrWhiteSpace(Decryptline23)) {
			ini.IniWriteValue(section, "Extra", GlobalVars.Custom_Extra.ToString());
		}
			
		string section2 = "Colors";
			
		Decryptline4 = ini.IniReadValue(section2, "HeadColorID");	
			
		if (string.IsNullOrWhiteSpace(Decryptline4)) {
			ini.IniWriteValue(section2, "HeadColorID", GlobalVars.HeadColorID.ToString());
		}
			
		Decryptline10 = ini.IniReadValue(section2, "HeadColorString");
			
		if (string.IsNullOrWhiteSpace(Decryptline10)) {
			ini.IniWriteValue(section2, "HeadColorString", GlobalVars.ColorMenu_HeadColor.ToString());
		}
			
		Decryptline5 = ini.IniReadValue(section2, "TorsoColorID");
			
		if (string.IsNullOrWhiteSpace(Decryptline5)) {
			ini.IniWriteValue(section2, "TorsoColorID", GlobalVars.TorsoColorID.ToString());
		}
			
		Decryptline11 = ini.IniReadValue(section2, "TorsoColorString");
			
		if (string.IsNullOrWhiteSpace(Decryptline11)) {
			ini.IniWriteValue(section2, "TorsoColorString", GlobalVars.ColorMenu_TorsoColor.ToString());
		}
			
		Decryptline6 = ini.IniReadValue(section2, "LeftArmColorID");
			
		if (string.IsNullOrWhiteSpace(Decryptline6)) {
			ini.IniWriteValue(section2, "LeftArmColorID", GlobalVars.LeftArmColorID.ToString());
		}
			
		Decryptline12 = ini.IniReadValue(section2, "LeftArmColorString");
			
		if (string.IsNullOrWhiteSpace(Decryptline12)) {
			ini.IniWriteValue(section2, "LeftArmColorString", GlobalVars.ColorMenu_LeftArmColor.ToString());
		}
			
		Decryptline7 = ini.IniReadValue(section2, "RightArmColorID");
			
		if (string.IsNullOrWhiteSpace(Decryptline7)) {
			ini.IniWriteValue(section2, "RightArmColorID", GlobalVars.RightArmColorID.ToString());
		}
			
		Decryptline13 = ini.IniReadValue(section2, "RightArmColorString");
			
		if (string.IsNullOrWhiteSpace(Decryptline13)) {
			ini.IniWriteValue(section2, "RightArmColorString", GlobalVars.ColorMenu_RightArmColor.ToString());
		}
			
		Decryptline8 = ini.IniReadValue(section2, "LeftLegColorID");
			
		if (string.IsNullOrWhiteSpace(Decryptline8)) {
			ini.IniWriteValue(section2, "LeftLegColorID", GlobalVars.LeftLegColorID.ToString());
		}
			
		Decryptline14 = ini.IniReadValue(section2, "LeftLegColorString");
			
		if (string.IsNullOrWhiteSpace(Decryptline14)) {
			ini.IniWriteValue(section2, "LeftLegColorString", GlobalVars.ColorMenu_LeftLegColor.ToString());
		}
			
		Decryptline9 = ini.IniReadValue(section2, "RightLegColorID");
			
		if (string.IsNullOrWhiteSpace(Decryptline9)) {
			ini.IniWriteValue(section2, "RightLegColorID", GlobalVars.RightLegColorID.ToString());
		}
			
		Decryptline15 = ini.IniReadValue(section2, "RightLegColorString");
			
		if (string.IsNullOrWhiteSpace(Decryptline15)) {
			ini.IniWriteValue(section2, "RightLegColorString", GlobalVars.ColorMenu_RightLegColor.ToString());
		}
			
		string section3 = "Other";
			
		Decryptline22 = ini.IniReadValue(section3, "CharacterID");
			
		if (string.IsNullOrWhiteSpace(Decryptline22)) {
			ini.IniWriteValue(section3, "CharacterID", GlobalVars.CharacterID.ToString());
		}
			
		Decryptline24 = ini.IniReadValue(section3, "ExtraSelectionIsHat");
			
		if (string.IsNullOrWhiteSpace(Decryptline24)) {
			ini.IniWriteValue(section3, "ExtraSelectionIsHat", GlobalVars.Custom_Extra_SelectionIsHat.ToString());
		}
    		
		GlobalVars.Custom_Hat1ID_Offline = Decryptline1;
		GlobalVars.Custom_Hat2ID_Offline = Decryptline2;
		GlobalVars.Custom_Hat3ID_Offline = Decryptline3;
			
		int iline4 = Convert.ToInt32(Decryptline4);
		GlobalVars.HeadColorID = iline4;
			
		int iline5 = Convert.ToInt32(Decryptline5);
		GlobalVars.TorsoColorID = iline5;
			
		int iline6 = Convert.ToInt32(Decryptline6);
		GlobalVars.LeftArmColorID = iline6;
			
		int iline7 = Convert.ToInt32(Decryptline7);
		GlobalVars.RightArmColorID = iline7;
			
		int iline8 = Convert.ToInt32(Decryptline8);
		GlobalVars.LeftLegColorID = iline8;
			
		int iline9 = Convert.ToInt32(Decryptline9);
		GlobalVars.RightLegColorID = iline9;
			
		GlobalVars.ColorMenu_HeadColor = Decryptline10;
		GlobalVars.ColorMenu_TorsoColor = Decryptline11;
		GlobalVars.ColorMenu_LeftArmColor = Decryptline12;
		GlobalVars.ColorMenu_RightArmColor = Decryptline13;
		GlobalVars.ColorMenu_LeftLegColor = Decryptline14;
		GlobalVars.ColorMenu_RightLegColor = Decryptline15;
			
		GlobalVars.Custom_Face_Offline = Decryptline16;
		GlobalVars.Custom_Head_Offline = Decryptline17;
		GlobalVars.Custom_T_Shirt_Offline = Decryptline18;
		GlobalVars.Custom_Shirt_Offline = Decryptline19;
		GlobalVars.Custom_Pants_Offline = Decryptline20;
		GlobalVars.Custom_Icon_Offline = Decryptline21;
			
		GlobalVars.CharacterID = Decryptline22;
			
		GlobalVars.Custom_Extra = Decryptline23;
			
		bool bline24 = Convert.ToBoolean(Decryptline24);
		GlobalVars.Custom_Extra_SelectionIsHat = bline24;
			
		ReloadLoadtextValue();
	}
		
	public static void WriteCustomizationValues(string cfgpath)
	{
		IniFile ini = new IniFile(cfgpath);
			
		string section = "Items";
			
		ini.IniWriteValue(section, "Hat1", GlobalVars.Custom_Hat1ID_Offline.ToString());
		ini.IniWriteValue(section, "Hat2", GlobalVars.Custom_Hat2ID_Offline.ToString());
		ini.IniWriteValue(section, "Hat3", GlobalVars.Custom_Hat3ID_Offline.ToString());
		ini.IniWriteValue(section, "Face", GlobalVars.Custom_Face_Offline.ToString());
		ini.IniWriteValue(section, "Head", GlobalVars.Custom_Head_Offline.ToString());
		ini.IniWriteValue(section, "TShirt", GlobalVars.Custom_T_Shirt_Offline.ToString());
		ini.IniWriteValue(section, "Shirt", GlobalVars.Custom_Shirt_Offline.ToString());
		ini.IniWriteValue(section, "Pants", GlobalVars.Custom_Pants_Offline.ToString());
		ini.IniWriteValue(section, "Icon", GlobalVars.Custom_Icon_Offline.ToString());
		ini.IniWriteValue(section, "Extra", GlobalVars.Custom_Extra.ToString());
			
		string section2 = "Colors";
			
		ini.IniWriteValue(section2, "HeadColorID", GlobalVars.HeadColorID.ToString());
		ini.IniWriteValue(section2, "HeadColorString", GlobalVars.ColorMenu_HeadColor.ToString());
		ini.IniWriteValue(section2, "TorsoColorID", GlobalVars.TorsoColorID.ToString());
		ini.IniWriteValue(section2, "TorsoColorString", GlobalVars.ColorMenu_TorsoColor.ToString());
		ini.IniWriteValue(section2, "LeftArmColorID", GlobalVars.LeftArmColorID.ToString());
		ini.IniWriteValue(section2, "LeftArmColorString", GlobalVars.ColorMenu_LeftArmColor.ToString());
		ini.IniWriteValue(section2, "RightArmColorID", GlobalVars.RightArmColorID.ToString());
		ini.IniWriteValue(section2, "RightArmColorString", GlobalVars.ColorMenu_RightArmColor.ToString());
		ini.IniWriteValue(section2, "LeftLegColorID", GlobalVars.LeftLegColorID.ToString());
		ini.IniWriteValue(section2, "LeftLegColorString", GlobalVars.ColorMenu_LeftLegColor.ToString());
		ini.IniWriteValue(section2, "RightLegColorID", GlobalVars.RightLegColorID.ToString());
		ini.IniWriteValue(section2, "RightLegColorString", GlobalVars.ColorMenu_RightLegColor.ToString());
			
		string section3 = "Other";
				
		ini.IniWriteValue(section3, "CharacterID", GlobalVars.CharacterID.ToString());
		ini.IniWriteValue(section3, "ExtraSelectionIsHat", GlobalVars.Custom_Extra_SelectionIsHat.ToString());
			
		ReloadLoadtextValue();
	}
		
	public static void ResetCustomizationValues()
	{
		GlobalVars.Custom_Hat1ID_Offline = "NoHat.rbxm";
		GlobalVars.Custom_Hat2ID_Offline = "NoHat.rbxm";
		GlobalVars.Custom_Hat3ID_Offline = "NoHat.rbxm";
		GlobalVars.Custom_Face_Offline = "DefaultFace.rbxm";
		GlobalVars.Custom_Head_Offline = "DefaultHead.rbxm";
		GlobalVars.Custom_T_Shirt_Offline = "NoTShirt.rbxm";
		GlobalVars.Custom_Shirt_Offline = "NoShirt.rbxm";
		GlobalVars.Custom_Pants_Offline = "NoPants.rbxm";
		GlobalVars.Custom_Icon_Offline = "NBC";
		GlobalVars.Custom_Extra = "NoExtra.rbxm";
		GlobalVars.HeadColorID = 24;
		GlobalVars.TorsoColorID = 23;
		GlobalVars.LeftArmColorID = 24;
		GlobalVars.RightArmColorID = 24;
		GlobalVars.LeftLegColorID = 119;
		GlobalVars.RightLegColorID = 119;
		GlobalVars.CharacterID = "";
		GlobalVars.ColorMenu_HeadColor = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.ColorMenu_TorsoColor = "Color [A=255, R=13, G=105, B=172]";
		GlobalVars.ColorMenu_LeftArmColor = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.ColorMenu_RightArmColor = "Color [A=255, R=245, G=205, B=47]";
		GlobalVars.ColorMenu_LeftLegColor = "Color [A=255, R=164, G=189, B=71]";
		GlobalVars.ColorMenu_RightLegColor = "Color [A=255, R=164, G=189, B=71]";
		GlobalVars.Custom_Extra_SelectionIsHat = false;
		ReloadLoadtextValue();
	}
		
	public static void ReloadLoadtextValue()
	{
		//Temporarily removed until i can figure out a way to better integrate this.
			
		/*
			if (GlobalVars.IsWebServerOn == true)
			{
				string extra = GlobalVars.Custom_Extra_SelectionIsHat == true ? GlobalVars.WebServer_HatDir + GlobalVars.Custom_Extra : GlobalVars.WebServer_ExtraDir + GlobalVars.Custom_Extra;
			
				GlobalVars.loadtext = "'" + GlobalVars.WebServer_BodyColors + "','" +
					GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat1ID_Offline + "','" +
					GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat2ID_Offline + "','" +  
					GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat3ID_Offline + "'," + 
					GlobalVars.WebServer_TShirtDir + GlobalVars.Custom_T_Shirt_Offline + "','" +
					GlobalVars.WebServer_ShirtDir + GlobalVars.Custom_Shirt_Offline + "','" +
					GlobalVars.WebServer_PantsDir + GlobalVars.Custom_Pants_Offline + "','" +
					GlobalVars.WebServer_FaceDir + GlobalVars.Custom_Face_Offline + "','" +
					GlobalVars.WebServer_HeadDir + GlobalVars.Custom_Head_Offline + "','" +
					GlobalVars.Custom_Icon_Offline + "','" +
					extra + "', true";
			
				GlobalVars.sololoadtext = GlobalVars.loadtext.Replace(GlobalVars.WebServerURI,GlobalVars.LocalWebServerURI);
			}
			else
			{
			
				GlobalVars.loadtext = "'" + GlobalVars.Custom_Hat1ID_Offline + "','" + 
					GlobalVars.Custom_Hat2ID_Offline + "','" +  
					GlobalVars.Custom_Hat3ID_Offline + "'," + 
					GlobalVars.HeadColorID + "," + 
					GlobalVars.TorsoColorID + "," + 
					GlobalVars.LeftArmColorID + "," + 
					GlobalVars.RightArmColorID + "," + 
					GlobalVars.LeftLegColorID + "," + 
					GlobalVars.RightLegColorID + ",'" +
					GlobalVars.Custom_T_Shirt_Offline + "','" +
					GlobalVars.Custom_Shirt_Offline + "','" +
					GlobalVars.Custom_Pants_Offline + "','" +
					GlobalVars.Custom_Face_Offline + "','" +
					GlobalVars.Custom_Head_Offline + "','" +
					GlobalVars.Custom_Icon_Offline + "','" +
					//GlobalVars.Custom_Extra  + "', false";
					GlobalVars.Custom_Extra + "'";
			
				GlobalVars.sololoadtext = GlobalVars.loadtext;
			}
			*/
			
		string hat1 = (GlobalVars.Custom_Hat1ID_Offline != "TeapotTurret.rbxm") ? GlobalVars.Custom_Hat1ID_Offline : "NoHat.rbxm";
		string hat2 = (GlobalVars.Custom_Hat2ID_Offline != "TeapotTurret.rbxm") ? GlobalVars.Custom_Hat2ID_Offline : "NoHat.rbxm";
		string hat3 = (GlobalVars.Custom_Hat3ID_Offline != "TeapotTurret.rbxm") ? GlobalVars.Custom_Hat3ID_Offline : "NoHat.rbxm";
		string extra = (GlobalVars.Custom_Extra != "TeapotTurret.rbxm") ? GlobalVars.Custom_Extra : "NoExtra.rbxm";
			
		GlobalVars.loadtext = "'" + hat1 + "','" +
		hat2 + "','" +
		hat3 + "'," +
		GlobalVars.HeadColorID + "," +
		GlobalVars.TorsoColorID + "," +
		GlobalVars.LeftArmColorID + "," +
		GlobalVars.RightArmColorID + "," +
		GlobalVars.LeftLegColorID + "," +
		GlobalVars.RightLegColorID + ",'" +
		GlobalVars.Custom_T_Shirt_Offline + "','" +
		GlobalVars.Custom_Shirt_Offline + "','" +
		GlobalVars.Custom_Pants_Offline + "','" +
		GlobalVars.Custom_Face_Offline + "','" +
		GlobalVars.Custom_Head_Offline + "','" +
		GlobalVars.Custom_Icon_Offline + "','" +
		extra + "'";
			
		GlobalVars.sololoadtext = "'" + GlobalVars.Custom_Hat1ID_Offline + "','" +
		GlobalVars.Custom_Hat2ID_Offline + "','" +
		GlobalVars.Custom_Hat3ID_Offline + "'," +
		GlobalVars.HeadColorID + "," +
		GlobalVars.TorsoColorID + "," +
		GlobalVars.LeftArmColorID + "," +
		GlobalVars.RightArmColorID + "," +
		GlobalVars.LeftLegColorID + "," +
		GlobalVars.RightLegColorID + ",'" +
		GlobalVars.Custom_T_Shirt_Offline + "','" +
		GlobalVars.Custom_Shirt_Offline + "','" +
		GlobalVars.Custom_Pants_Offline + "','" +
		GlobalVars.Custom_Face_Offline + "','" +
		GlobalVars.Custom_Head_Offline + "','" +
		GlobalVars.Custom_Icon_Offline + "','" +
		GlobalVars.Custom_Extra + "'";
	}
		
	public static void ReadClientValues(string clientpath)
	{
		string line1;
		string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline9, Decryptline10, Decryptline11;

		using (StreamReader reader = new StreamReader(clientpath)) {
			line1 = reader.ReadLine();
		}
			
		string ConvertedLine = SecurityFuncs.Base64Decode(line1);
		string[] result = ConvertedLine.Split('|');
		Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
		Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
		Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
		Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
		Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
		Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
		Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
		Decryptline9 = SecurityFuncs.Base64Decode(result[8]);
		Decryptline10 = SecurityFuncs.Base64Decode(result[9]);
		Decryptline11 = SecurityFuncs.Base64Decode(result[10]);
			
		bool bline1 = Convert.ToBoolean(Decryptline1);
		GlobalVars.UsesPlayerName = bline1;
			
		bool bline2 = Convert.ToBoolean(Decryptline2);
		GlobalVars.UsesID = bline2;
			
		GlobalVars.Warning = Decryptline3;
			
		bool bline4 = Convert.ToBoolean(Decryptline4);
		GlobalVars.LegacyMode = bline4;
			
		GlobalVars.SelectedClientMD5 = Decryptline5;
			
		GlobalVars.SelectedClientScriptMD5 = Decryptline6;
			
		GlobalVars.SelectedClientDesc = Decryptline7;
			
		bool bline9 = Convert.ToBoolean(Decryptline9);
		GlobalVars.FixScriptMapMode = bline9;
			
		bool bline10 = Convert.ToBoolean(Decryptline10);
		GlobalVars.AlreadyHasSecurity = bline10;
			
		GlobalVars.CustomArgs = Decryptline11;
	}
		
	public static void GeneratePlayerID()
	{
		CryptoRandom random = new CryptoRandom();
		int randomID = 0;
		int randIDmode = random.Next(0, 7);
		if (randIDmode == 0) {
			randomID = random.Next(0, 99);
		} else if (randIDmode == 1) {
			randomID = random.Next(0, 999);
		} else if (randIDmode == 2) {
			randomID = random.Next(0, 9999);
		} else if (randIDmode == 3) {
			randomID = random.Next(0, 99999);
		} else if (randIDmode == 4) {
			randomID = random.Next(0, 999999);
		} else if (randIDmode == 5) {
			randomID = random.Next(0, 9999999);
		} else if (randIDmode == 6) {
			randomID = random.Next(0, 99999999);
		} else if (randIDmode == 7) {
			randomID = random.Next();
		}
		//2147483647 is max id.
		GlobalVars.UserID = randomID;
	}

    public static Image LoadImage(string fileFullName)
    {
        Stream fileStream = File.OpenRead(fileFullName);
        Image image = Image.FromStream(fileStream);

        // PropertyItems seem to get lost when fileStream is closed to quickly (?); perhaps
        // this is the reason Microsoft didn't want to close it in the first place.
        PropertyItem[] items = image.PropertyItems;

        fileStream.Close();

        foreach (PropertyItem item in items)
        {
            image.SetPropertyItem(item);
        }

        return image;
    }
}
