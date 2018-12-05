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
			string line1;
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8;

			using(StreamReader reader = new StreamReader(cfgpath))
			{
    			line1 = reader.ReadLine();
			}
			
			if (!SecurityFuncs.IsBase64String(line1))
				return;
			
			string ConvertedLine = SecurityFuncs.Base64Decode(line1);
			string[] result = ConvertedLine.Split('|');
			Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
    		Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
    		Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
    		Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
    		Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
    		Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
    		Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
    		Decryptline8 = SecurityFuncs.Base64Decode(result[7]);
    		
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
			
			ReadCustomizationValues(cfgpath.Replace(".txt","_customization.txt"));
		}
		
		public static void WriteConfigValues(string cfgpath)
		{
			string[] lines = { 
				SecurityFuncs.Base64Encode(GlobalVars.CloseOnLaunch.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.UserID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.PlayerName.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.SelectedClient.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Map.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.RobloxPort.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.PlayerLimit.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.DisableTeapotTurret.ToString())
			};
			File.WriteAllText(cfgpath, SecurityFuncs.Base64Encode(string.Join("|",lines)));
			WriteCustomizationValues(cfgpath.Replace(".txt","_customization.txt"));
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
			ResetCustomizationValues();
		}
		
		public static void ReadCustomizationValues(string cfgpath)
		{
			string line1;
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, Decryptline10, Decryptline11, Decryptline12, Decryptline13, Decryptline14, Decryptline15, Decryptline16, Decryptline17, Decryptline18, Decryptline19, Decryptline20, Decryptline21, Decryptline22;

			using(StreamReader reader = new StreamReader(cfgpath))
			{
    			line1 = reader.ReadLine();
			}
			
			if (!SecurityFuncs.IsBase64String(line1))
				return;
			
			string ConvertedLine = SecurityFuncs.Base64Decode(line1);
			string[] result = ConvertedLine.Split('|');
			Decryptline1 = SecurityFuncs.Base64Decode(result[0]);
    		Decryptline2 = SecurityFuncs.Base64Decode(result[1]);
    		Decryptline3 = SecurityFuncs.Base64Decode(result[2]);
    		Decryptline4 = SecurityFuncs.Base64Decode(result[3]);
    		Decryptline5 = SecurityFuncs.Base64Decode(result[4]);
    		Decryptline6 = SecurityFuncs.Base64Decode(result[5]);
    		Decryptline7 = SecurityFuncs.Base64Decode(result[6]);
    		Decryptline8 = SecurityFuncs.Base64Decode(result[7]);
    		Decryptline9 = SecurityFuncs.Base64Decode(result[8]);
    		Decryptline10 = SecurityFuncs.Base64Decode(result[9]);
    		Decryptline11 = SecurityFuncs.Base64Decode(result[10]);
    		Decryptline12 = SecurityFuncs.Base64Decode(result[11]);
    		Decryptline13 = SecurityFuncs.Base64Decode(result[12]);
    		Decryptline14 = SecurityFuncs.Base64Decode(result[13]);
    		Decryptline15 = SecurityFuncs.Base64Decode(result[14]);
    		Decryptline16 = SecurityFuncs.Base64Decode(result[15]);
    		Decryptline17 = SecurityFuncs.Base64Decode(result[16]);
    		Decryptline18 = SecurityFuncs.Base64Decode(result[17]);
    		Decryptline19 = SecurityFuncs.Base64Decode(result[18]);
    		Decryptline20 = SecurityFuncs.Base64Decode(result[19]);
    		Decryptline21 = SecurityFuncs.Base64Decode(result[20]);
    		Decryptline22 = SecurityFuncs.Base64Decode(result[21]);
			
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
			
			ReloadLoadtextValue();
		}
		
		public static void WriteCustomizationValues(string cfgpath)
		{
			string[] lines = { 
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Hat1ID_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Hat2ID_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Hat3ID_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.HeadColorID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.TorsoColorID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.LeftArmColorID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.RightArmColorID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.LeftLegColorID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.RightLegColorID.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.ColorMenu_HeadColor.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.ColorMenu_TorsoColor.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.ColorMenu_LeftArmColor.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.ColorMenu_RightArmColor.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.ColorMenu_LeftLegColor.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.ColorMenu_RightLegColor.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Face_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Head_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_T_Shirt_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Shirt_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Pants_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Icon_Offline.ToString()),
				SecurityFuncs.Base64Encode(GlobalVars.CharacterID.ToString())
			};
			File.WriteAllText(cfgpath, SecurityFuncs.Base64Encode(string.Join("|",lines)));
			
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
			ReloadLoadtextValue();
		}
		
		public static void ReloadLoadtextValue()
		{
			GlobalVars.loadtext = GlobalVars.Custom_Hat1ID_Offline + "','" + 
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
				GlobalVars.Custom_Icon_Offline + "'";
		}
		
		public static void ReadClientValues(string clientpath)
		{
			string line1;
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline9, Decryptline10, Decryptline11;

			using(StreamReader reader = new StreamReader(clientpath)) 
			{
    			line1 = reader.ReadLine();
			}
			
			if (!SecurityFuncs.IsBase64String(line1))
				return;
			
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
			if (GlobalVars.AdminMode != true || GlobalVars.AlreadyHasSecurity != true)
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
		
		public static bool checkScriptMD5(string client)
		{
			if (GlobalVars.AdminMode != true|| GlobalVars.AlreadyHasSecurity != true)
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
		
		public static string RandomString()
		{
			CryptoRandom random = new CryptoRandom();
			return new String(' ', random.Next(20));
		}
		
		public static void RenameWindow(Process exe, ScriptGenerator.ScriptType type)
		{
			int time = 500;
			BackgroundWorker worker = new BackgroundWorker();
			worker.DoWork += (obj, e) => WorkerDoWork(exe, type, time, worker);
			worker.RunWorkerAsync();
		}
		
		private static void WorkerDoWork(Process exe, ScriptGenerator.ScriptType type, int time, BackgroundWorker worker)
 		{
    		if (exe.IsRunning() == true)
			{
				while (exe.IsRunning() == true)
            	{
					if (exe.IsRunning() != true)
					{
						worker.DoWork -= (obj, e) => WorkerDoWork(exe, type, time, worker);
						worker.CancelAsync();
						worker.Dispose();
						break;
					}
					
					if (type == ScriptGenerator.ScriptType.Client)
					{
						SetWindowText(exe.MainWindowHandle, "Novetus - " + GlobalVars.SelectedClient + " " + ScriptGenerator.GetNameForType(type) + " [" + GlobalVars.IP + ":" + GlobalVars.RobloxPort + "]" + RandomString());
					}
					else if (type == ScriptGenerator.ScriptType.Server || type == ScriptGenerator.ScriptType.Solo || type == ScriptGenerator.ScriptType.Studio)
					{
						SetWindowText(exe.MainWindowHandle, "Novetus - " + GlobalVars.SelectedClient + " " + ScriptGenerator.GetNameForType(type) + " [" + GlobalVars.Map + "]" + RandomString());
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
					return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player','" + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + "," + md5s + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false)
				{
					return "_G.CSConnect(0,'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'Player','" + GlobalVars.loadtext + "," + md5s + ")";
				}
				else
				{
					return "_G.CSConnect(" + GlobalVars.UserID + ",'" + GlobalVars.IP + "'," + GlobalVars.RobloxPort + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + "," + md5s + ")";
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
					return "_G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == true)
				{
					return "_G.CSSolo(" + GlobalVars.UserID + ",'Player','" + GlobalVars.loadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == true && GlobalVars.UsesID == false)
				{
					return "_G.CSSolo(0,'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ")";
				}
				else if (GlobalVars.UsesPlayerName == false && GlobalVars.UsesID == false )
				{
					return "_G.CSSolo(0,'Player','" + GlobalVars.loadtext + ")";
				}
				else
				{
					return "_G.CSSolo(" + GlobalVars.UserID + ",'" + GlobalVars.PlayerName + "','" + GlobalVars.loadtext + ")";
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
			LauncherFuncs.ReadConfigValues(GlobalVars.BasePath + "\\config.txt");
		}

		public static void GenerateScriptForClient(ScriptType type, string client)
		{
			//next, generate the header functions.

			ReadConfigValues();
			
			//string scriptcontents = MultiLine(GetScriptContents(GlobalVars.ClientDir + @"\\" + GlobalVars.SelectedClient + @"\\content\\scripts\\" + GlobalVars.ScriptName + ".lua"));

			string code = MultiLine(
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
		
		public static string MultiLine(params string[] args)
		{
			return string.Join(Environment.NewLine, args);
		}
	}
	
	public class ClientScript
	{
		private static string hatdir = "rbxasset://../../../charcustom/hats/";
		private static string facedir = "rbxasset://../../../charcustom/faces/";
		private static string headdir = "rbxasset://../../../charcustom/heads/";
		private static string tshirtdir = "rbxasset://../../../charcustom/tshirts/";
		private static string shirtdir = "rbxasset://../../../charcustom/shirts/";
		private static string pantsdir = "rbxasset://../../../charcustom/pants/";
		
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
				.Replace("%limit%",GlobalVars.PlayerLimit.ToString());
			return compiled;
		}
	}
	
	public static class GlobalVars
	{
		public static string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static string ClientDir = (BasePath + "\\clients").Replace(@"\",@"\\");
		public static string MapsDir = (BasePath + "\\maps").Replace(@"\",@"\\");
		public static string CustomPlayerDir = (BasePath + "\\charcustom").Replace(@"\",@"\\");
    	public static string IP = "localhost";
    	public static string Version = "";
   		public static string SharedArgs = "";
   		public static string ScriptName = "CSMPFunctions";
   		public static string ScriptGenName = "CSMPBoot";
    	//vars for loader
    	public static bool ReadyToLaunch = false;
		//server settings.
		public static string Map = "";
		public static int RobloxPort = 53640;
		public static int DefaultRobloxPort = 53640;
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
		public static string CharacterID ="";
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
	}
}
