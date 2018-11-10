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
			GlobalVars.CloseOnLaunch = false;
			GlobalVars.UserID = 0;
			GlobalVars.PlayerName = "Player";
			GlobalVars.SelectedClient = GlobalVars.DefaultClient;
			GlobalVars.Map = "Baseplate.rbxl";
			GlobalVars.RobloxPort = 53640;
			GlobalVars.PlayerLimit = 12;
			GlobalVars.DisableTeapotTurret = false;
			ResetCustomizationValues();
		}
		
		public static void ReadCustomizationValues(string cfgpath)
		{
			string line1;
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7, Decryptline8, Decryptline9, Decryptline10, Decryptline11, Decryptline12, Decryptline13, Decryptline14, Decryptline15, Decryptline16, Decryptline17, Decryptline18, Decryptline19, Decryptline20, Decryptline21;

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
				SecurityFuncs.Base64Encode(GlobalVars.Custom_Icon_Offline.ToString())
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
			string Decryptline1, Decryptline2, Decryptline3, Decryptline4, Decryptline5, Decryptline6, Decryptline7;

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
			
			bool bline1 = Convert.ToBoolean(Decryptline1);
			GlobalVars.UsesPlayerName = bline1;
			
			bool bline2 = Convert.ToBoolean(Decryptline2);
			GlobalVars.UsesID = bline2;
			
			bool bline3 = Convert.ToBoolean(Decryptline3);
			GlobalVars.LoadsAssetsOnline = bline3;
			
			bool bline4 = Convert.ToBoolean(Decryptline4);
			GlobalVars.LegacyMode = bline4;
			
			GlobalVars.SelectedClientMD5 = Decryptline5;
			
			GlobalVars.SelectedClientScriptMD5 = Decryptline6;
			
			GlobalVars.SelectedClientDesc = Decryptline7;
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
			if (GlobalVars.AdminMode != true)
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
			if (GlobalVars.AdminMode != true)
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
	public class ScriptGenerator
	{
		public ScriptGenerator()
		{
		}

		public static void GenerateScriptForClient()
		{
			//next, generate the header functions.

			SecurityFuncs.ReadConfigValues();
			
			int aasamples = GlobalVars.AASamples.Equals(0) ? 1 : GlobalVars.AASamples.Equals(1) ? 4 : GlobalVars.AASamples.Equals(2) ? 8 : 1;
			
			string header = MultiLine(
					"--Header",
					"function newWaitForChild(newParent,name)",
					"local returnable = nil",
					"if newParent:FindFirstChild(name) then",
					"returnable = newParent:FindFirstChild(name)",
					"else",
					"repeat wait() returnable = newParent:FindFirstChild(name)  until returnable ~= nil",
					"end",
					"return returnable",
					"end",
					"settings().Rendering.Shadows = " + GlobalVars.Shadows.ToString().ToLower(),
					"settings().Rendering.AASamples = " + aasamples,
					"AnimatedCharacter = " + GlobalVars.AnimatedCharacter.ToString().ToLower()
             		);

			string playersettings = MultiLine(
					"--Player Settings",
                	"UserID = " + GlobalVars.UserID,
                	"PlayerName = '" + GlobalVars.Name + "'"
            		);
			
			string customizationsettings = "";
			
			if (GlobalVars.AnimatedCharacter == false)
			{
				customizationsettings = MultiLine(
					"--Customization Settings",
                	"Hat1ID = 'NoHat.rbxm'"
            		);
			}
			else
			{
				customizationsettings = MultiLine(
					"--Customization Settings",
                	"Hat1ID = '" + GlobalVars.HatName + "'"
            		);
			}
			
			string colorsettings = "";
			
			if (GlobalVars.AnimatedCharacter == false)
			{
				if (GlobalVars.UseRandomColors)
				{
					colorsettings = GeneratePlayerColorString();
				}
				else
				{
					colorsettings = GeneratePlayerColorPresetString(GlobalVars.PlayerColorPreset);
				}
			}
			else
			{
				colorsettings = MultiLine(
					"--Color Settings",
                	"HeadColorID = " + GlobalVars.HeadColor,
                	"TorsoColorID = " + GlobalVars.TorsoColor,
                	"LeftArmColorID = " + GlobalVars.LeftArmColor,
                	"RightArmColorID = " + GlobalVars.RightArmColor,
                	"LeftLegColorID = " + GlobalVars.LeftLegColor,
                	"RightLegColorID = " + GlobalVars.RightLegColor
            		);
			}

			//add customization funcs
			string customizationgen = MultiLine(
					"--Customization Code",
				    "function InitalizeClientAppearance(Player,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,HatID)",
					"local newCharApp = Instance.new('IntValue',Player)",
					"newCharApp.Name = 'Appearance'",
					"for i=1,6,1 do",
					"local BodyColor = Instance.new('BrickColorValue',newCharApp)",
					"if (i == 1) then",
					"if (HeadColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(HeadColorID)",
					"BodyColor.Name = 'HeadColor (ID: '..HeadColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'HeadColor (ID: 1)'",
					"end",
					"elseif (i == 2) then",
					"if (TorsoColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(TorsoColorID)",
					"BodyColor.Name = 'TorsoColor (ID: '..TorsoColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'TorsoColor (ID: 1)'",
					"end",
					"elseif (i == 3) then",
					"if (LeftArmColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(LeftArmColorID)",
					"BodyColor.Name = 'LeftArmColor (ID: '..LeftArmColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'LeftArmColor (ID: 1)'",
					"end",
					"elseif (i == 4) then",
					"if (RightArmColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(RightArmColorID)",
					"BodyColor.Name = 'RightArmColor (ID: '..RightArmColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'RightArmColor (ID: 1)'",
					"end",
					"elseif (i == 5) then",
					"if (LeftLegColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(LeftLegColorID)",
					"BodyColor.Name = 'LeftLegColor (ID: '..LeftLegColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'LeftLegColor (ID: 1)'",
					"end",
					"elseif (i == 6) then",
					"if (RightLegColorID ~= nil) then",
					"BodyColor.Value = BrickColor.new(RightLegColorID)",
					"BodyColor.Name = 'RightLegColor (ID: '..RightLegColorID..')'",
					"else",
					"BodyColor.Value = BrickColor.new(1)",
					"BodyColor.Name = 'RightLegColor (ID: 1)'",
					"end",
					"end",
					"local typeValue = Instance.new('NumberValue')",
					"typeValue.Name = 'CustomizationType'",
					"typeValue.Parent = BodyColor",
					"typeValue.Value = 1",
					"local indexValue = Instance.new('NumberValue')",
					"indexValue.Name = 'ColorIndex'",
					"indexValue.Parent = BodyColor",
					"indexValue.Value = i",
					"end",
					"local newHat = Instance.new('StringValue',newCharApp)",
					"if (HatID ~= nil) then",
					"newHat.Value = HatID",
					"newHat.Name = HatID",
					"else",
					"newHat.Value = 'NoHat.rbxm'",
					"newHat.Name = 'NoHat.rbxm'",
					"end",
					"local typeValue = Instance.new('NumberValue')",
					"typeValue.Name = 'CustomizationType'",
					"typeValue.Parent = newHat",
					"typeValue.Value = 2",
					"end",
                    "function LoadCharacterNew(playerApp,newChar)",
					"local charparts = {[1] = newWaitForChild(newChar,'Head'),[2] = newWaitForChild(newChar,'Torso'),[3] = newWaitForChild(newChar,'Left Arm'),[4] = newWaitForChild(newChar,'Right Arm'),[5] = newWaitForChild(newChar,'Left Leg'),[6] = newWaitForChild(newChar,'Right Leg')}",
					"for _,newVal in pairs(playerApp:GetChildren()) do",
					"newWaitForChild(newVal,'CustomizationType')",
					"local customtype = newVal:FindFirstChild('CustomizationType')",
					"if (customtype.Value == 1) then ",
					"pcall(function()",
					"newWaitForChild(newVal,'ColorIndex')",
					"local colorindex = newVal:FindFirstChild('ColorIndex')",
					"charparts[colorindex.Value].BrickColor = newVal.Value ",
					"end)",
					"elseif (customtype.Value == 2)  then",
					"pcall(function()",
					"local newHat = game.Workspace:InsertContent('rbxasset://hats/'..newVal.Value)",
					"if newHat[1] then ",
					"if newHat[1].className == 'Hat' then",
					"newHat[1].Parent = newChar",
					"else",
					"newHat[1]:remove()",
					"end",
					"end",
					"end)",
					"end",
					"end",
					"end"
					);

			//finally, we generate the actual script code.

			string code = MultiLine(
					"--Game Code",
					"game:GetService('RunService'):run()",
					"local plr = game.Players:CreateLocalPlayer(UserID)",
					"plr.Name = PlayerName",
					"plr:LoadCharacter()",
					"pcall(function() plr:SetUnder13(false) end)",
					"pcall(function() plr:SetAccountAge(365) end)",
					"if (AnimatedCharacter == false) then",
					"if plr.Character:FindFirstChild('Animate') then",
					"plr.Character.Animate:Remove()",
					"end",
					"end",
					"InitalizeClientAppearance(plr,HeadColorID,TorsoColorID,LeftArmColorID,RightArmColorID,LeftLegColorID,RightLegColorID,Hat1ID)",
					"LoadCharacterNew(newWaitForChild(plr,'Appearance'),plr.Character)",
					"game:GetService('Visit')",
					"while true do",
					"wait(0.001)",
					"if (plr.Character ~= nil) then",
					"if (plr.Character.Humanoid.Health == 0) then",
					"wait(5)",
					"plr:LoadCharacter()",
					"LoadCharacterNew(newWaitForChild(plr,'Appearance'),plr.Character,plr.Backpack)",
					"if (AnimatedCharacter == false) then",
					"if plr.Character:FindFirstChild('Animate') then",
					"plr.Character.Animate:Remove()",
					"end",
					"end",
					"elseif (plr.Character.Parent == nil) then",
					"wait(5)",
					"plr:LoadCharacter()",
					"LoadCharacterNew(newWaitForChild(plr,'Appearance'),plr.Character,plr.Backpack)",
					"if (AnimatedCharacter == false) then",
					"if plr.Character:FindFirstChild('Animate') then",
					"plr.Character.Animate:Remove()",
					"end",
					"end",
					"end",
					"end",
					"end"
					);

			string scriptfile = MultiLine(
				header,
				playersettings,
				customizationsettings,
				colorsettings,
				customizationgen,
				code
				);
			
			List<string> list = new List<string>(Regex.Split(scriptfile, Environment.NewLine));
			string[] convertedList = list.ToArray();
			File.WriteAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + GlobalVars.ScriptLuaFile, convertedList);
		}
		
		public static string GeneratePlayerColorString()
		{
			CryptoRandom random = new CryptoRandom();
			int SkinPattern = random.Next(1,7);
			int LegsPattern = random.Next(1,5);
			int TorsoPattern = random.Next(1,8);
			
			int HeadColor = 0;
			int TorsoColor= 0;
			int LArmColor = 0;
			int RArmColor = 0;
			int LLegColor = 0;
			int RLegColor = 0;
			
			if (SkinPattern == 1)
			{
				HeadColor = 24;
				LArmColor = 24;
				RArmColor = 24;
			}
			else if (SkinPattern == 2)
			{
				HeadColor = 226;
				LArmColor = 226;
				RArmColor = 226;
			}
			else if (SkinPattern == 3)
			{
				HeadColor = 101;
				LArmColor = 101;
				RArmColor = 101;
			}
			else if (SkinPattern == 4)
			{
				HeadColor = 9;
				LArmColor = 9;
				RArmColor = 9;
			}
			else if (SkinPattern == 5)
			{
				HeadColor = 38;
				LArmColor = 38;
				RArmColor = 38;
			}
			else if (SkinPattern == 6)
			{
				HeadColor = 18;
				LArmColor = 18;
				RArmColor = 18;
			}
			else if (SkinPattern == 7)
			{
				HeadColor = 128;
				LArmColor = 128;
				RArmColor = 128;
			}
	
			if (LegsPattern == 1)
			{
				RLegColor = 119;
				LLegColor = 119;
			}
			else if (LegsPattern == 2)
			{
				LLegColor = 11;
				RLegColor = 11;
			}
			else if (LegsPattern == 3)
			{
				LLegColor = 23;
				RLegColor = 23;
			}
			else if (LegsPattern == 4)
			{
				LLegColor = 1;
				RLegColor = 1;
			}
			else if (LegsPattern == 5)
			{
				LLegColor = 45;
				RLegColor = 45;
			}
	
			if (TorsoPattern == 1)
			{
				TorsoColor = 194;
			}
			else if (TorsoPattern == 2)
			{
				TorsoColor = 199;
			}
			else if (TorsoPattern == 3)
			{
				TorsoColor = 1;
			}
			else if (TorsoPattern == 4)
			{
				TorsoColor = 21;
			}
			else if (TorsoPattern == 5)
			{
				TorsoColor = 37;
			}
			else if (TorsoPattern == 6)
			{
				TorsoColor = 23;
			}
			else if (TorsoPattern == 7)
			{
				TorsoColor = 45;
			}
			else if (TorsoPattern == 8)
			{
				TorsoColor = 11;
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
		
		public static string MultiLine(params string[] args)
		{
			return string.Join(Environment.NewLine, args);
		}
	}
	*/
	
	public static class GlobalVars
	{
		public static string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static string ClientDir = (BasePath + "\\clients").Replace(@"\",@"\\");
		public static string MapsDir = (BasePath + "\\maps").Replace(@"\",@"\\");
		public static string CustomPlayerDir = (BasePath + "\\charcustom").Replace(@"\",@"\\");
		public static string AddonDir = (BasePath + "\\addons").Replace(@"\",@"\\");
    	public static string IP = "localhost";
    	public static string Version = "";
   		public static string SharedArgs = "";
   		public static string ScriptName = "CSMPFunctions";
   		public static string ScriptGenName = "CSMPBoot";
    	//vars for loader
    	public static bool ReadyToLaunch = false;
		//server settings.
		public static string Map = "Baseplate.rbxl";
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
		public static bool UsesPlayerName = false;
		public static bool UsesID = true;
		public static string SelectedClientDesc = "";
		public static bool LoadsAssetsOnline = false;
		public static bool LegacyMode = false;
		public static string SelectedClientMD5 = "";
		public static string SelectedClientScriptMD5 = "";
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
