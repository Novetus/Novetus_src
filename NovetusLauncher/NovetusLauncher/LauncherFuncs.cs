/*
 * Created by SharpDevelop.
 * User: BITL
 * Date: 6/13/2017
 * Time: 10:24 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Linq;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using Mono.Nat;

namespace NovetusLauncher
{
	/// <summary>
	/// Description of LauncherFuncs.
	/// </summary>
	public class LauncherFuncs
	{
		public LauncherFuncs()
		{
		}
		
		public static void ReadConfigValues(string cfgpath)
		{
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, Decryptline10;
			
			IniFile ini = new IniFile(cfgpath);
			
			string section = "Config";
			
			Decryptline1 = ini.IniReadValue(section, "CloseOnLaunch");
    		Decryptline2 = ini.IniReadValue(section, "UserID");
    		Decryptline3 = ini.IniReadValue(section, "PlayerName");
    		Decryptline4 = ini.IniReadValue(section, "SelectedClient");
    		Decryptline5 = ini.IniReadValue(section, "Map");
    		Decryptline6 = ini.IniReadValue(section, "RobloxPort");
    		Decryptline7 = ini.IniReadValue(section, "PlayerLimit");
    		Decryptline8 = ini.IniReadValue(section, "DisableTeapotTurret");
    		Decryptline9 = ini.IniReadValue(section, "ShowHatsOnExtra");
    		Decryptline10 = ini.IniReadValue(section, "UPnP");
    		
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
			
			bool bline8 = Convert.ToBoolean(Decryptline8);
			GlobalVars.DisableTeapotTurret = bline8;
			
			bool bline9 = Convert.ToBoolean(Decryptline9);
			GlobalVars.Custom_Extra_ShowHats = bline9;
			
			bool bline10 = Convert.ToBoolean(Decryptline10);
			GlobalVars.UPnP = bline10;
			
			ReadCustomizationValues(cfgpath.Replace(".ini","_customization.ini"));
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
			ini.IniWriteValue(section, "DisableTeapotTurret", GlobalVars.DisableTeapotTurret.ToString());
			ini.IniWriteValue(section, "ShowHatsOnExtra", GlobalVars.Custom_Extra_ShowHats.ToString());
			ini.IniWriteValue(section, "UPnP", GlobalVars.UPnP.ToString());
			WriteCustomizationValues(cfgpath.Replace(".ini","_customization.ini"));
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
			GlobalVars.DisableTeapotTurret = false;
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
			Decryptline2 = ini.IniReadValue(section, "Hat2");
			Decryptline3 = ini.IniReadValue(section, "Hat3");
			Decryptline16 = ini.IniReadValue(section, "Face");
			Decryptline17 = ini.IniReadValue(section, "Head");
			Decryptline18 = ini.IniReadValue(section, "TShirt");
			Decryptline19 = ini.IniReadValue(section, "Shirt");
			Decryptline20 = ini.IniReadValue(section, "Pants");
			Decryptline21 = ini.IniReadValue(section, "Icon");
			Decryptline23 = ini.IniReadValue(section, "Extra");
			
			string section2 = "Colors";
			
			Decryptline4 = ini.IniReadValue(section2, "HeadColorID");
			Decryptline10 = ini.IniReadValue(section2, "HeadColorString");
			Decryptline5 = ini.IniReadValue(section2, "TorsoColorID");
			Decryptline11 = ini.IniReadValue(section2, "TorsoColorString");
			Decryptline6 = ini.IniReadValue(section2, "LeftArmColorID");
			Decryptline12 = ini.IniReadValue(section2, "LeftArmColorString");
			Decryptline7 = ini.IniReadValue(section2, "RightArmColorID");
			Decryptline13 = ini.IniReadValue(section2, "RightArmColorString");
			Decryptline8 = ini.IniReadValue(section2, "LeftLegColorID");
			Decryptline14 = ini.IniReadValue(section2, "LeftLegColorString");
			Decryptline9 = ini.IniReadValue(section2, "RightLegColorID");
			Decryptline15 = ini.IniReadValue(section2, "RightLegColorString");
			
			string section3 = "Other";
				
			Decryptline22 = ini.IniReadValue(section3, "CharacterID");
			Decryptline24 = ini.IniReadValue(section3, "ExtraSelectionIsHat");
    		
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
			*/
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
					GlobalVars.Custom_Extra  + "', false";
			
				GlobalVars.sololoadtext = GlobalVars.loadtext;
			//}
		}
		
		public static void ReadClientValues(string clientpath)
		{
			string line1;
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline9, Decryptline10, Decryptline11;

			using(StreamReader reader = new StreamReader(clientpath)) 
			{
    			line1 = reader.ReadLine();
			}
			
			string ConvertedLine = SecurityFuncs.DecryptText(line1,"");
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
			int randIDmode = random.Next(0,7);
			if (randIDmode == 0)
			{
				randomID = random.Next(0, 99);
			}
			else if (randIDmode == 1)
			{
				randomID = random.Next(0, 999);
			}
			else if (randIDmode == 2)
			{
				randomID = random.Next(0, 9999);
			}
			else if (randIDmode == 3)
			{
				randomID = random.Next(0, 99999);
			}
			else if (randIDmode == 4)
			{
				randomID = random.Next(0, 999999);
			}
			else if (randIDmode == 5)
			{
				randomID = random.Next(0, 9999999);
			}
			else if (randIDmode == 6)
			{
				randomID = random.Next(0, 99999999);
			}
			else if (randIDmode == 7)
			{
				randomID = random.Next();
			}
			//2147483647 is max id.
			GlobalVars.UserID = randomID;
		}
	}
	
		//Discord Rich Presence Integration :D
	public class DiscordRpc
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ReadyCallback();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void DisconnectedCallback(int errorCode, string message);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ErrorCallback(int errorCode, string message);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void JoinCallback(string secret);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SpectateCallback(string secret);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void RequestCallback(JoinRequest request);

		public struct EventHandlers
		{
			public ReadyCallback readyCallback;
			public DisconnectedCallback disconnectedCallback;
			public ErrorCallback errorCallback;
			public JoinCallback joinCallback;
			public SpectateCallback spectateCallback;
			public RequestCallback requestCallback;
		}

		[System.Serializable]
		public struct RichPresence
		{
			public string state; /* max 128 bytes */
			public string details; /* max 128 bytes */
			public long startTimestamp;
			public long endTimestamp;
			public string largeImageKey; /* max 32 bytes */
			public string largeImageText; /* max 128 bytes */
			public string smallImageKey; /* max 32 bytes */
			public string smallImageText; /* max 128 bytes */
			public string partyId; /* max 128 bytes */
			public int partySize;
			public int partyMax;
			public string matchSecret; /* max 128 bytes */
			public string joinSecret; /* max 128 bytes */
			public string spectateSecret; /* max 128 bytes */
			public bool instance;
		}

		[System.Serializable]
		public struct JoinRequest
		{
			public string userId;
			public string username;
			public string avatar;
		}

		public enum Reply
		{
			No = 0,
			Yes = 1,
			Ignore = 2
		}

		[DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

		[DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Shutdown();

		[DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
		public static extern void RunCallbacks();

		[DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
		public static extern void UpdatePresence(ref RichPresence presence);

		[DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Respond(string userId, Reply reply);
	}
	
	public static class TextLineRemover
	{
	    public static void RemoveTextLines(IList<string> linesToRemove, string filename, string tempFilename)
	    {
	        // Initial values
	        int lineNumber = 0;
	        int linesRemoved = 0;
	        DateTime startTime = DateTime.Now;
	
	        // Read file
	        using (var sr = new StreamReader(filename))
	        {
	            // Write new file
	            using (var sw = new StreamWriter(tempFilename))
	            {
	                // Read lines
	                string line;
	                while ((line = sr.ReadLine()) != null)
	                {
	                    lineNumber++;
	                    // Look for text to remove
	                    if (!ContainsString(line, linesToRemove))
	                    {
	                        // Keep lines that does not match
	                        sw.WriteLine(line);
	                    }
	                    else
	                    {
	                        // Ignore lines that DO match
	                        linesRemoved++;
	                        InvokeOnRemovedLine(new RemovedLineArgs { RemovedLine = line, RemovedLineNumber = lineNumber});
	                    }
	                }
	            }
	        }
	        // Delete original file
	        File.Delete(filename);
	
	        // ... and put the temp file in its place.
	        File.Move(tempFilename, filename);
	
	        // Final calculations
	        DateTime endTime = DateTime.Now;
	        InvokeOnFinished(new FinishedArgs {LinesRemoved = linesRemoved, TotalLines = lineNumber, TotalTime = endTime.Subtract(startTime)});
	    }
	
	    private static bool ContainsString(string line, IEnumerable<string> linesToRemove)
	    {
	        foreach (var lineToRemove in linesToRemove)
	        {
	            if(line.Contains(lineToRemove))
	                return true;
	        }
	        return false;
	    }
	
	    public static event RemovedLine OnRemovedLine;
	    public static event Finished OnFinished;
	
	    public static void InvokeOnFinished(FinishedArgs args)
	    {
	        Finished handler = OnFinished;
	        if (handler != null) handler(null, args);
	    }
	
	    public static void InvokeOnRemovedLine(RemovedLineArgs args)
	    {
	        RemovedLine handler = OnRemovedLine;
	        if (handler != null) handler(null, args);
	    }
	}
	
	public delegate void Finished(object sender, FinishedArgs args);
	
	public class FinishedArgs
	{
	    public int TotalLines { get; set; }
	    public int LinesRemoved { get; set; }
	    public TimeSpan TotalTime { get; set; }
	}
	
	public delegate void RemovedLine(object sender, RemovedLineArgs args);
	
	public class RemovedLineArgs
	{
	    public string RemovedLine { get; set; }
	    public int RemovedLineNumber { get; set; }
	}
	
	
	
	/// <summary>
	/// Description of SecurityFuncs.
	/// </summary>
	public class SecurityFuncs
	{
		[DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);
		
		public SecurityFuncs()
		{
		}
		
		public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
    		byte[] encryptedBytes = null;
    		byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    		using (MemoryStream ms = new MemoryStream())
    		{
        		using (RijndaelManaged AES = new RijndaelManaged())
       			{ 
            		AES.KeySize = 256;
           			AES.BlockSize = 128;
            		var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            		AES.Key = key.GetBytes(AES.KeySize / 8);
            		AES.IV = key.GetBytes(AES.BlockSize / 8);
            		AES.Mode = CipherMode.CBC;
            		using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
            		{
                		cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                		cs.Close();
            		}
            		encryptedBytes = ms.ToArray();
        		}
    		}
    		return encryptedBytes;
		}

		public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
    		byte[] decryptedBytes = null;
    		byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    		using (MemoryStream ms = new MemoryStream())
    		{
        		using (RijndaelManaged AES = new RijndaelManaged())
        		{
            		AES.KeySize = 256;
            		AES.BlockSize = 128;
            		var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
            		AES.Key = key.GetBytes(AES.KeySize / 8);
            		AES.IV = key.GetBytes(AES.BlockSize / 8);
            		AES.Mode = CipherMode.CBC;
            		using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
            		{
                		cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                		cs.Close();
           			}
            		decryptedBytes = ms.ToArray();
        		}
    		}
    		return decryptedBytes;
		}
		
		public static string EncryptText(string input, string password)
		{
    		// Get the bytes of the string
    		byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
   			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

    		// Hash the password with SHA256
    		passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

    		byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

    		string result = Convert.ToBase64String(bytesEncrypted);

    		return result;
		}
		
		public static string DecryptText(string input, string password)
		{
    		// Get the bytes of the string
    		try
    		{
    			byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
    			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
    			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

    			byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

    			string result = Encoding.UTF8.GetString(bytesDecrypted);

    			return result;
    		}
    		catch (Exception)
    		{
    			return Base64Decode(input);
    		}
		}
		
		public static string RandomString(int length)
		{
			CryptoRandom random = new CryptoRandom();
    		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
    		return new string(Enumerable.Repeat(chars, length)
      			.Select(s => s[random.Next(s.Length)]).ToArray());
		}
		
		public static string RandomString()
		{
			CryptoRandom random = new CryptoRandom();
			return RandomString(random.Next(5, 20));
		}
		
		public static string Base64Decode(string base64EncodedData) 
		{
  			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
  			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
		
		public static string Base64Encode(string plainText) 
		{
  			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
  			return System.Convert.ToBase64String(plainTextBytes);
		}
		
		public static bool IsBase64String(string s)
		{
    		s = s.Trim();
    		return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
		}
		
		public static void RegisterURLProtocol(string protocolName, string applicationPath, string description)
    	{
      		RegistryKey subKey = Registry.ClassesRoot.CreateSubKey(protocolName);
      		subKey.SetValue((string) null, (object) description);
      		subKey.SetValue("URL Protocol", (object) string.Empty);
      		Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell");
      		Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell\\open");
      		Registry.ClassesRoot.CreateSubKey(protocolName + "\\Shell\\open\\command").SetValue((string) null, (object) ("\"" + applicationPath + "\" \"%1\""));
    	}
		
		public static long UnixTimeNow()
		{
    		var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
    		return (long)timeSpan.TotalSeconds;
		}
		
		public static bool checkClientMD5(string client)
		{
			if (GlobalVars.AdminMode != true)
			{
				if (GlobalVars.AlreadyHasSecurity != true)
				{
					string rbxexe = "";
					if (GlobalVars.LegacyMode == true)
					{
						rbxexe = GlobalVars.BasePath + "\\clients\\" + client + "\\RobloxApp.exe";
					}
					else
					{
						rbxexe = GlobalVars.BasePath + "\\clients\\" + client + "\\RobloxApp_client.exe";
					}
    				using (var md5 = MD5.Create())
    				{
    					using (var stream = File.OpenRead(rbxexe))
        				{
    						byte[] hash = md5.ComputeHash(stream);
    						string clientMD5 = BitConverter.ToString(hash).Replace("-", "");
            				if (clientMD5.Equals(GlobalVars.SelectedClientMD5))
            				{
            					return true;
            				}
            				else
            				{
            					return false;
            				}
        				}
    				}
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}
		
		public static bool checkScriptMD5(string client)
		{
			if (GlobalVars.AdminMode != true)
			{
				if (GlobalVars.AlreadyHasSecurity != true)
				{
					string rbxscript = GlobalVars.BasePath + "\\clients\\" + client + "\\content\\scripts\\" + GlobalVars.ScriptName + ".lua";
    				using (var md5 = MD5.Create())
    				{
    					using (var stream = File.OpenRead(rbxscript))
        				{
    						byte[] hash = md5.ComputeHash(stream);
    						string clientMD5 = BitConverter.ToString(hash).Replace("-", "");
            				if (clientMD5.Equals(GlobalVars.SelectedClientScriptMD5))
            				{
            					return true;
            				}
            				else
            				{
            					return false;
            				}
        				}
    				}
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}
		
		public static string CalculateMD5(string filename)
		{
    		using (var md5 = MD5.Create())
    		{
        		using (var stream = File.OpenRead(filename))
        		{
            		return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-","");
        		}
    		}
		}
		
		public static bool IsElevated
    	{
      		get
      		{
        		return WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
      		}
    	}
		
		public static string RandomStringTitle()
		{
			CryptoRandom random = new CryptoRandom();
			return new String(' ', random.Next(20));
		}
		
		public static void RenameWindow(Process exe, ScriptGenerator.ScriptType type)
		{
			int time = 500;
			BackgroundWorker worker = new BackgroundWorker();
			worker.DoWork += (obj, e) => WorkerDoWork(exe, type, time, worker, GlobalVars.SelectedClient);
			worker.RunWorkerAsync();
		}
		
		private static void WorkerDoWork(Process exe, ScriptGenerator.ScriptType type, int time, BackgroundWorker worker, string clientname)
 		{
    		if (exe.IsRunning() == true)
			{
				while (exe.IsRunning() == true)
            	{
					if (exe.IsRunning() != true)
					{
						worker.DoWork -= (obj, e) => WorkerDoWork(exe, type, time, worker, clientname);
						worker.CancelAsync();
						worker.Dispose();
						break;
					}
					
					if (type == ScriptGenerator.ScriptType.Client)
					{
						SetWindowText(exe.MainWindowHandle, "Novetus - " + clientname + " " + ScriptGenerator.GetNameForType(type) + " [" + GlobalVars.IP + ":" + GlobalVars.RobloxPort + "]" + RandomStringTitle());
					}
					else if (type == ScriptGenerator.ScriptType.Server || type == ScriptGenerator.ScriptType.Solo || type == ScriptGenerator.ScriptType.Studio)
					{
						SetWindowText(exe.MainWindowHandle, "Novetus - " + clientname + " " + ScriptGenerator.GetNameForType(type) + " [" + GlobalVars.Map + "]" + RandomStringTitle());
					}
               		Thread.Sleep(time);
            	}
			}
			else
			{
				Thread.Sleep(time);
				RenameWindow(exe, type);
			}
 		}
	}

	public static class RichTextBoxExtensions
	{
    	public static void AppendText(this RichTextBox box, string text, Color color)
    	{
        	box.SelectionStart = box.TextLength;
        	box.SelectionLength = 0;

        	box.SelectionColor = color;
        	box.AppendText(text);
        	box.SelectionColor = box.ForeColor;
    	}
	}
	
	public static class ProcessExtensions
	{
		public static bool IsRunning(this Process process)
		{
			try  {Process.GetProcessById(process.Id);}
			catch (InvalidOperationException) { return false; }
			catch (ArgumentException){return false;}
			return true;
		}
	}
	
	public class CryptoRandom : RandomNumberGenerator
	{
		private static RandomNumberGenerator r;
	
		public CryptoRandom()
	 	{ 
	  		r = RandomNumberGenerator.Create();
	 	}
	
	 	///<param name=”buffer”>An array of bytes to contain random numbers.</param>
	 	public override void GetBytes(byte[] buffer)
	 	{
	  		r.GetBytes(buffer);
	 	}
	 	
		public override void GetNonZeroBytes(byte[] data)
		{
			r.GetNonZeroBytes(data);
		}
	 	public double NextDouble()
	 	{
	  		byte[] b = new byte[4];
	  		r.GetBytes(b);
	  		return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
	 	}
	
	 	///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
	 	///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
	 	public int Next(int minValue, int maxValue)
	 	{
	  		return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
	 	}
	 	public int Next()
	 	{
	  		return Next(0, Int32.MaxValue);
	 	}
	
	 	///<param name=”maxValue”>The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
	 	public int Next(int maxValue)
	 	{
	  		return Next(0, maxValue);
	 	}
	}
	
	
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
			if (GlobalVars.LegacyMode == true)
			{
				rbxexe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +  "\\clients\\" + client + @"\\RobloxApp.exe";
			}
			else
			{
				rbxexe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +  "\\clients\\" + client + @"\\RobloxApp_client.exe";
			}
			
			string md5dir = SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location);
			string md5script = SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua");
			string md5exe = SecurityFuncs.CalculateMD5(rbxexe);
			string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
			if (type == ScriptType.Client)
			{
				if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
				{
					return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false)
				{
					return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else
				{
					return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
				}
			}
			else if (type == ScriptType.Server)
			{
				return "_G.CSServer(" + GlobalVars.RobloxPort + "," + GlobalVars.PlayerLimit + "," + md5s + "," + GlobalVars.DisableTeapotTurret.ToString().ToLower() + ")";
			}
			else if (type == ScriptType.Solo)
			{
				if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
				{
					return "_G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					return "_G.CSSolo(" + GlobalVars.UserID + ",'Player'," + GlobalVars.sololoadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					return "_G.CSSolo(0,'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false )
				{
					return "_G.CSSolo(0,'Player'," + GlobalVars.sololoadtext + ")";
				}
				else
				{
					return "_G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
				}
			}
			else if (type == ScriptType.Studio)
			{
				return "";
			}
			else
			{
				return "";
			}
		}
		
		public static string GetNameForType(ScriptType type)
		{
			if (type == ScriptType.Client)
			{
				return "Client";
			}
			else if (type == ScriptType.Server)
			{
				return "Server";
			}
			else if (type == ScriptType.Solo)
			{
				return "Play Solo";
			}
			else if (type == ScriptType.Studio)
			{
				return "Studio";
			}
			else
			{
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
	
	public class ClientScript
	{
		private static string basedir = "rbxasset://../../../shareddata/charcustom/";
		private static string basemapdir = "rbxasset://../../../maps/";
		private static string hatdir = basedir + "hats/";
		private static string facedir = basedir + "faces/";
		private static string headdir = basedir + "heads/";
		private static string tshirtdir = basedir + "tshirts/";
		private static string shirtdir = basedir + "shirts/";
		private static string pantsdir = basedir + "pants/";
		private static string extradir = basedir + "custom/";
		
		public static string GetArgsFromTag(string code, string tag, string endtag)
		{
			int pFrom = code.IndexOf(tag) + tag.Length;
			int pTo = code.LastIndexOf(endtag);

			string result = code.Substring(pFrom, pTo - pFrom);
			
			return result;
		}
		
		public static ScriptGenerator.ScriptType GetTypeFromTag(string tag, string endtag)
		{
			if (tag.Contains("client") && endtag.Contains("client"))
			{
				return ScriptGenerator.ScriptType.Client;
			}
			else if (tag.Contains("server") && endtag.Contains("server") || tag.Contains("no3d") && endtag.Contains("no3d"))
			{
				return ScriptGenerator.ScriptType.Server;
			}
			else if (tag.Contains("solo") && endtag.Contains("solo"))
			{
				return ScriptGenerator.ScriptType.Solo;
			}
			else if (tag.Contains("studio") && endtag.Contains("studio"))
			{
				return ScriptGenerator.ScriptType.Studio;
			}
			else
			{
				return ScriptGenerator.ScriptType.None;
			}
		}
		
		public static string GetRawArgsForType(ScriptGenerator.ScriptType type, string md5s, string luafile)
		{
			if (type == ScriptGenerator.ScriptType.Client)
			{
				if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
				{
					return "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					return "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					return "dofile('" + luafile + "'); _G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false)
				{
					return "dofile('" + luafile + "'); _G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player'," + GlobalVars.loadtext + "," + md5s + ")";
				}
				else
				{
					return "dofile('" + luafile + "'); _G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.loadtext + "," + md5s + ")";
				}
			}
			else if (type == ScriptGenerator.ScriptType.Server)
			{
				return "dofile('" + luafile + "'); _G.CSServer(" + GlobalVars.RobloxPort + "," + GlobalVars.PlayerLimit + "," + md5s + "," + GlobalVars.DisableTeapotTurret.ToString().ToLower() + ")";
			}
			else if (type == ScriptGenerator.ScriptType.Solo)
			{
				if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == true)
				{
					return "dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					return "dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'Player'," + GlobalVars.sololoadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					return "dofile('" + luafile + "'); _G.CSSolo(0,'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false )
				{
					return "dofile('" + luafile + "'); _G.CSSolo(0,'Player'," + GlobalVars.sololoadtext + ")";
				}
				else
				{
					return "dofile('" + luafile + "'); _G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "'," + GlobalVars.sololoadtext + ")";
				}
			}
			else if (type == ScriptGenerator.ScriptType.Studio)
			{
				return "dofile('" + luafile + "');";
			}
			else
			{
				return "";
			}
		}
		
		public static string GetRawArgsFromTag(string tag, string endtag, string md5s, string luafile)
		{	
			return GetRawArgsForType(GetTypeFromTag(tag, endtag), md5s, luafile);
		}
		
		public static int ConvertIconStringToInt()
		{
			if (GlobalVars.Custom_Icon_Offline == "BC")
			{
				return 1;
			}
			else if (GlobalVars.Custom_Icon_Offline == "TBC")
			{
				return 2;
			}
			else if (GlobalVars.Custom_Icon_Offline == "OBC")
			{
				return 3;
			}
			else if (GlobalVars.Custom_Icon_Offline == "NBC")
			{
				return 0;				
			}
			
			return 0;
		}
		
		public static string GetFolderAndMapName(string source, string seperator = " -")
		{
			try
			{
				string result = source.Substring(0, source.IndexOf(seperator));
				
				if (File.Exists(GlobalVars.MapsDir + @"\\" + result + @"\\" + source))
				{
					return result + @"\\" + source;
				}
				else
				{
					return "";
				}
			}
			catch (Exception)
			{
				return "";
			}
		}
		
		public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe)
		{
			if (GlobalVars.FixScriptMapMode)
			{
				ScriptGenerator.GenerateScriptForClient(GetTypeFromTag(tag, endtag), GlobalVars.SelectedClient);
			}
			
			string extractedCode = GetArgsFromTag(code, tag, endtag);
			
			string md5dir = GlobalVars.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(Assembly.GetExecutingAssembly().Location) : "";
			string md5script = GlobalVars.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua") : "";
			string md5exe = GlobalVars.AlreadyHasSecurity != true ? SecurityFuncs.CalculateMD5(rbxexe) : "";
			string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
			string compiled = extractedCode.Replace("%mapfile%",mapfile)
				.Replace("%luafile%",luafile)
				.Replace("%charapp%",GlobalVars.CharacterID)
				.Replace("%ip%",GlobalVars.IP)
				.Replace("%port%",GlobalVars.RobloxPort.ToString())
				.Replace("%name%",GlobalVars.PlayerName)
				.Replace("%icone%",ConvertIconStringToInt().ToString())
				.Replace("%icon%",GlobalVars.Custom_Icon_Offline)
				.Replace("%id%",GlobalVars.UserID.ToString())
				.Replace("%face%",GlobalVars.Custom_Face_Offline)
				.Replace("%head%",GlobalVars.Custom_Head_Offline)
				.Replace("%tshirt%",GlobalVars.Custom_T_Shirt_Offline)
				.Replace("%shirt%",GlobalVars.Custom_Shirt_Offline)
				.Replace("%pants%",GlobalVars.Custom_Pants_Offline)
				.Replace("%hat1%",GlobalVars.Custom_Hat1ID_Offline)
				.Replace("%hat2%",GlobalVars.Custom_Hat2ID_Offline)
				.Replace("%hat3%",GlobalVars.Custom_Hat3ID_Offline)
				.Replace("%faced%",facedir + GlobalVars.Custom_Face_Offline)
				.Replace("%headd%",headdir + GlobalVars.Custom_Head_Offline)
				.Replace("%tshirtd%",tshirtdir + GlobalVars.Custom_T_Shirt_Offline)
				.Replace("%shirtd%",shirtdir + GlobalVars.Custom_Shirt_Offline)
				.Replace("%pantsd%",pantsdir + GlobalVars.Custom_Pants_Offline)
				.Replace("%hat1d%",hatdir + GlobalVars.Custom_Hat1ID_Offline)
				.Replace("%hat2d%",hatdir + GlobalVars.Custom_Hat2ID_Offline)
				.Replace("%hat3d%",hatdir + GlobalVars.Custom_Hat3ID_Offline)
				.Replace("%headcolor%",GlobalVars.HeadColorID.ToString())
				.Replace("%torsocolor%",GlobalVars.TorsoColorID.ToString())
				.Replace("%larmcolor%",GlobalVars.LeftArmColorID.ToString())
				.Replace("%llegcolor%",GlobalVars.LeftLegColorID.ToString())
				.Replace("%rarmcolor%",GlobalVars.RightArmColorID.ToString())
				.Replace("%rlegcolor%",GlobalVars.RightLegColorID.ToString())
				.Replace("%rlegcolor%",GlobalVars.SelectedClientMD5)
				.Replace("%md5launcher%",md5dir)
				.Replace("%md5script%",GlobalVars.SelectedClientMD5)
				.Replace("%md5exe%",GlobalVars.SelectedClientScriptMD5)
				.Replace("%md5scriptd%",md5script)
				.Replace("%md5exed%",md5exe)
				.Replace("%limit%",GlobalVars.PlayerLimit.ToString())
				.Replace("%extra%",GlobalVars.Custom_Extra)
				.Replace("%extrad%",extradir + GlobalVars.Custom_Extra)
				.Replace("%hat4d%",hatdir + GlobalVars.Custom_Extra)
				.Replace("%args%",GetRawArgsFromTag(tag, endtag, md5s, luafile))
				.Replace("%facews%",GlobalVars.WebServer_FaceDir + GlobalVars.Custom_Face_Offline)
				.Replace("%headws%",GlobalVars.WebServer_HeadDir + GlobalVars.Custom_Head_Offline)
				.Replace("%tshirtws%",GlobalVars.WebServer_TShirtDir + GlobalVars.Custom_T_Shirt_Offline)
				.Replace("%shirtws%",GlobalVars.WebServer_ShirtDir + GlobalVars.Custom_Shirt_Offline)
				.Replace("%pantsws%",GlobalVars.WebServer_PantsDir + GlobalVars.Custom_Pants_Offline)
				.Replace("%hat1ws%",GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat1ID_Offline)
				.Replace("%hat2ws%",GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat2ID_Offline)
				.Replace("%hat3ws%",GlobalVars.WebServer_HatDir + GlobalVars.Custom_Hat3ID_Offline)
				.Replace("%extraws%",GlobalVars.WebServer_ExtraDir + GlobalVars.Custom_Extra)
				.Replace("%hat4ws%",GlobalVars.WebServer_HatDir + GlobalVars.Custom_Extra)
				.Replace("%bodycolors%",GlobalVars.WebServer_BodyColors)
				.Replace("%mapfiled%",basemapdir + GetFolderAndMapName(GlobalVars.Map));
			return compiled;
		}
	}
	
	public static class TreeNodeHelper
	{
		public static void ListDirectory(TreeView treeView, string path)
		{
    		treeView.Nodes.Clear();
    		var rootDirectoryInfo = new DirectoryInfo(path);
    		treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
		}

		public static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
    		var directoryNode = new TreeNode(directoryInfo.Name);
    		foreach (var directory in directoryInfo.GetDirectories())
        		directoryNode.Nodes.Add(CreateDirectoryNode(directory));
    		foreach (var file in directoryInfo.GetFiles())
        		directoryNode.Nodes.Add(new TreeNode(file.Name));
    		return directoryNode;
		}
		
		public static TreeNode SearchTreeView(string p_sSearchTerm, TreeNodeCollection p_Nodes)
		{
    		foreach (TreeNode node in p_Nodes)
    		{
        		if (node.Text == p_sSearchTerm)
            		return node;

        		if (node.Nodes.Count > 0)
        		{
            		TreeNode child = SearchTreeView(p_sSearchTerm, node.Nodes);
            		if (child != null) return child;
        		}
    		}

    		return null;
		}
		
		public static string GetFolderNameFromPrefix(string source, string seperator = " -")
		{
			try
			{
				string result = source.Substring(0, source.IndexOf(seperator));
				
				if (Directory.Exists(GlobalVars.MapsDir + @"\\" + result))
				{
					return result + @"\\";
				}
				else
				{
					return "";
				}
			}
			catch (Exception)
			{
				return "";
			}
		}
		
		public static void CopyNodes(TreeNodeCollection oldcollection, TreeNodeCollection newcollection)
		{
			foreach(TreeNode node in oldcollection)
			{
				newcollection.Add((TreeNode)node.Clone());
			}
		}
		
		public static List<TreeNode> GetAllNodes(this TreeView _self)
		{
    		List<TreeNode> result = new List<TreeNode>();
    		foreach (TreeNode child in _self.Nodes)
    		{
        		result.AddRange(child.GetAllNodes());
    		}
    		return result;
		}

		public static List<TreeNode> GetAllNodes(this TreeNode _self)
		{
    		List<TreeNode> result = new List<TreeNode>();
    		result.Add(_self);
    		foreach (TreeNode child in _self.Nodes)
    		{
        		result.AddRange(child.GetAllNodes());
    		}
    		return result;
		}
	}
	
	public static class UPnP
	{
		public static void InitUPnP(EventHandler<DeviceEventArgs> DeviceFound, EventHandler<DeviceEventArgs> DeviceLost)
		{
			if (GlobalVars.UPnP == true)
			{
				NatUtility.DeviceFound += DeviceFound;
				NatUtility.DeviceLost += DeviceLost;
				NatUtility.StartDiscovery();
			}
		}
		
		public static void StartUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UPnP == true)
			{
				int map = device.GetSpecificMapping(protocol, port).PublicPort;
			
				if (map == -1)
				{
					device.CreatePortMap(new Mapping(protocol, port, port));
				}
			}
		}
		
		public static void StopUPnP(INatDevice device, Protocol protocol, int port)
		{
			if (GlobalVars.UPnP == true)
			{
				int map = device.GetSpecificMapping(protocol, port).PublicPort;
			
				if (map != -1)
				{
					device.DeletePortMap(new Mapping(protocol, port, port));
				}
			}
		}
	}
	
	public static class StringExtensions
	{
    	public static bool Contains(this string source, string toCheck, StringComparison comp)
    	{
        	if (source == null) return false;
			return source.IndexOf(toCheck, comp) >= 0;
    	}
	}
	
	//credit to BLaZiNiX
	public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,string val,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key,string def, StringBuilder retVal,
            int size,string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section,string Key,string Value)
        {
            WritePrivateProfileString(Section,Key,Value,this.path);
        }
        
        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section,string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section,Key,"",temp, 
                                            255, this.path);
            return temp.ToString();

        }
    }
	
	public static class SplashReader
	{
		private static string RandomSplash()
		{
			string[] splashes = File.ReadAllLines(GlobalVars.ConfigDir + "\\splashes.txt");
			string splash = "";
			
    		try
    		{
    			splash = splashes[new CryptoRandom().Next(0,splashes.Length-1)];
    		}
    		catch (Exception)
    		{
    			try
    			{
    				splash = splashes[0];
    			}
    			catch (Exception)
    			{
    				splash = "missingno";
    				return splash;
    			}
    		}
    		
    		string formattedsplash = splash.Replace("%name%",GlobalVars.PlayerName);
    		
    		return formattedsplash;
		}
		
		private static bool IsTheSameDay(DateTime date1, DateTime date2)
		{
    		return (date1.Month == date2.Month && date1.Day == date2.Day);
		}
		
		public static string GetSplash()
		{
			DateTime today = DateTime.Now;
			string splash = "";
			
			if (IsTheSameDay(today, new DateTime(today.Year,12,24)) || IsTheSameDay(today, new DateTime(today.Year,12,25)))
			{
				splash = "Merry Christmas!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,12,31)) || IsTheSameDay(today, new DateTime(today.Year,1,1)))
			{
				splash = "Happy New Year!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,10,31)))
			{
				splash = "Happy Halloween!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,6,10)))
			{
				splash = "Happy Birthday, Bitl!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,8,27)))
			{
				splash = "Happy Birthday, ROBLOX!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,10,27)))
			{
				splash = "Happy Birthday, Novetus!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,1,28)))
			{
				splash = "Happy Birthday, Tytygigas!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,2,15)))
			{
				splash = "Happy Birthday, Carrot!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,6,14)))
			{
				splash = "Happy Birthday, MAO!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,9,15)))
			{
				splash = "Happy Birthday, Coke!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,5,17)))
			{
				splash = "Happy Birthday, TheLivingBee!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,10,9)))
			{
				splash = "Happy Leif Erikson Day! HINGA DINGA DURGEN!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,10,10)))
			{
				splash = "I used to wonder what friendship could be!";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,4,20)))
			{
				splash = "4/20 lol";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,4,27)))
			{
				splash = "fluttershy is best pone";
			}
			else if (IsTheSameDay(today, new DateTime(today.Year,2,11)))
			{
				splash = "RIP Erik Cassel";
			}
			else
			{
				splash = RandomSplash();
			}
			
			return splash;
		}
	}
	
	//made by aksakalli
	public class SimpleHTTPServer
	{
		
    private readonly string[] _indexFiles = { 
        "index.html", 
        "index.htm",
        "index.php",
        "default.html", 
        "default.htm", 
        "default.php"       
    };
    
    private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        {".asf", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".avi", "video/x-msvideo"},
        {".bin", "application/octet-stream"},
        {".cco", "application/x-cocoa"},
        {".crt", "application/x-x509-ca-cert"},
        {".css", "text/css"},
        {".deb", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dll", "application/octet-stream"},
        {".dmg", "application/octet-stream"},
        {".ear", "application/java-archive"},
        {".eot", "application/octet-stream"},
        {".exe", "application/octet-stream"},
        {".flv", "video/x-flv"},
        {".gif", "image/gif"},
        {".hqx", "application/mac-binhex40"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".ico", "image/x-icon"},
        {".img", "application/octet-stream"},
        {".iso", "application/octet-stream"},
        {".jar", "application/java-archive"},
        {".jardiff", "application/x-java-archive-diff"},
        {".jng", "image/x-jng"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".mml", "text/mathml"},
        {".mng", "video/x-mng"},
        {".mov", "video/quicktime"},
        {".mp3", "audio/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpg", "video/mpeg"},
        {".msi", "application/octet-stream"},
        {".msm", "application/octet-stream"},
        {".msp", "application/octet-stream"},
        {".pdb", "application/x-pilot"},
        {".pdf", "application/pdf"},
        {".pem", "application/x-x509-ca-cert"},
        {".php", "text/html"},
        {".pl", "application/x-perl"},
        {".pm", "application/x-perl"},
        {".png", "image/png"},
        {".prc", "application/x-pilot"},
        {".ra", "audio/x-realaudio"},
        {".rar", "application/x-rar-compressed"},
        {".rpm", "application/x-redhat-package-manager"},
        {".rss", "text/xml"},
        {".run", "application/x-makeself"},
        {".sea", "application/x-sea"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".swf", "application/x-shockwave-flash"},
        {".tcl", "application/x-tcl"},
        {".tk", "application/x-tcl"},
        {".txt", "text/plain"},
        {".war", "application/java-archive"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wmv", "video/x-ms-wmv"},
        {".xml", "text/xml"},
        {".xpi", "application/x-xpinstall"},
        {".zip", "application/zip"},
    };
    private Thread _serverThread;
    private string _rootDirectory;
    private HttpListener _listener;
    private int _port;
 
    public int Port
    {
        get { return _port; }
        private set { }
    }
 
    /// <summary>
    /// Construct server with given port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    /// <param name="port">Port of the server.</param>
    public SimpleHTTPServer(string path, int port)
    {
        this.Initialize(path, port);
    }
 
    /// <summary>
    /// Construct server with suitable port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    public SimpleHTTPServer(string path)
    {
        //get an empty port
        TcpListener l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        this.Initialize(path, port);
    }
 
    /// <summary>
    /// Stop server and dispose all functions.
    /// </summary>
    public void Stop()
    {
        _serverThread.Abort();
        _listener.Stop();
        GlobalVars.IsWebServerOn = false;
    }
 
    private void Listen()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
        _listener.Start();
        while (true)
        {
            try
            {
                HttpListenerContext context = _listener.GetContext();
                Process(context);
            }
            catch (Exception)
            {
 
            }
        }
    }
    
    private string ProcessPhpPage(string phpCompilerPath, string pageFileName)
    {
		Process proc = new Process();
		proc.StartInfo.FileName = phpCompilerPath;
		proc.StartInfo.Arguments = "-d \"display_errors=1\" -d \"error_reporting=E_PARSE\" \"" + pageFileName + "\"";
		proc.StartInfo.CreateNoWindow = true;
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.Start();
		string res = proc.StandardOutput.ReadToEnd();
		proc.StandardOutput.Close();
		proc.Close();
		return res;
    }
 
    private void Process(HttpListenerContext context)
    {
        string filename = context.Request.Url.AbsolutePath;
        filename = filename.Substring(1);
 
        if (string.IsNullOrEmpty(filename))
        {
            foreach (string indexFile in _indexFiles)
            {
                if (File.Exists(Path.Combine(_rootDirectory, indexFile)))
                {
                    filename = indexFile;
                    break;
                }
            }
        }
 
        filename = Path.Combine(_rootDirectory, filename);
 
        if (File.Exists(filename))
        {
            try
            {
            	var ext = new FileInfo(filename);
            	
            	if (ext.Extension == ".php")
            	{
            		string output = ProcessPhpPage(GlobalVars.ConfigDir + "\\php\\php.exe", filename);
            		byte[] input = ASCIIEncoding.UTF8.GetBytes(output);
            		//Adding permanent http response headers
               		string mime;
                	context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                	context.Response.ContentLength64 = input.Length;
                	context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                	context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));
                    context.Response.OutputStream.Write(input, 0, input.Length);
                	context.Response.StatusCode = (int)HttpStatusCode.OK;
                	context.Response.OutputStream.Flush();
            	}
            	else
            	{
                	Stream input = new FileStream(filename, FileMode.Open);
                	//Adding permanent http response headers
               		string mime;
                	context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                	context.Response.ContentLength64 = input.Length;
                	context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                	context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));
 
                	byte[] buffer = new byte[1024 * 16];
                	int nbytes;
                	while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    	context.Response.OutputStream.Write(buffer, 0, nbytes);
                	input.Close();
                
                	context.Response.StatusCode = (int)HttpStatusCode.OK;
                	context.Response.OutputStream.Flush();
            	}
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
 
        }
        else
        {
        	if (context.Request.HttpMethod.Equals("GET") && filename.Contains("bodycolors.rbxm", StringComparison.OrdinalIgnoreCase))
        	{
        		string output = WebServerGenerator.GenerateBodyColorsXML();
            	byte[] input = ASCIIEncoding.UTF8.GetBytes(output);
                context.Response.ContentType = "text/xml";
            	context.Response.ContentLength64 = input.Length;
                context.Response.OutputStream.Write(input, 0, input.Length);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.OutputStream.Flush();
        	}
        	else
        	{
            	context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        	}
        }
        
        context.Response.OutputStream.Close();
    }
 
    private void Initialize(string path, int port)
    {
        this._rootDirectory = path;
        this._port = port;
        _serverThread = new Thread(this.Listen);
        _serverThread.Start();
        GlobalVars.IsWebServerOn = true;
    }
	}
	
	public static class WebServerGenerator
	{
		public static string GenerateBodyColorsXML()
		{
			string xmltemplate = GlobalVars.MultiLine(File.ReadAllLines(GlobalVars.CustomPlayerDir + "\\BodyColors.xml"));
			string xml = String.Format(xmltemplate, GlobalVars.HeadColorID, GlobalVars.LeftArmColorID, GlobalVars.LeftLegColorID, GlobalVars.RightArmColorID, GlobalVars.RightLegColorID, GlobalVars.TorsoColorID);
			return xml;
		}
	}
	
	public static class GlobalVars
	{
		public static string RootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static string BasePath = RootPath.Replace(@"\",@"\\");
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
		public static int WebServer_Port = (RobloxPort+1);
		public static int PlayerLimit = 12;
		public static bool DisableTeapotTurret = false;
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
		public static string CharacterID ="";
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
		
        public static string MultiLine(params string[] args)
		{
			return string.Join(Environment.NewLine, args);
		}
	}
}
