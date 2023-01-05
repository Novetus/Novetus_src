#region Usings
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Linq;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
#endregion

namespace Novetus.Core
{
	#region Security Functions
	public class SecurityFuncs
	{
		[DllImport("user32.dll")]
		static extern int SetWindowText(IntPtr hWnd, string text);

		public static string RandomString(int length)
		{
			CryptoRandom random = new CryptoRandom();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
			return new string(Enumerable.Repeat(chars, length)
					  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static int GenerateRandomNumber()
		{
			CryptoRandom random = new CryptoRandom();
			int randomID = 0;
			int randIDmode = random.Next(0, 8);
			int idlimit = 0;

			switch (randIDmode)
			{
				case 0:
					idlimit = 9;
					break;
				case 1:
					idlimit = 99;
					break;
				case 2:
					idlimit = 999;
					break;
				case 3:
					idlimit = 9999;
					break;
				case 4:
					idlimit = 99999;
					break;
				case 5:
					idlimit = 999999;
					break;
				case 6:
					idlimit = 9999999;
					break;
				case 7:
					idlimit = 99999999;
					break;
				case 8:
				default:
					break;
			}

			if (idlimit > 0)
			{
				randomID = random.Next(0, idlimit);
			}
			else
			{
				randomID = random.Next();
			}

			//2147483647 is max id.
			return randomID;
		}

		//these 2 methods are for the clientinfo creator.
		public static string Base64DecodeNew(string base64EncodedData)
		{
			return base64EncodedData.Decrypt();
		}

		public static string Base64DecodeOld(string base64EncodedData)
		{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}

		//this is for everything else
		public static string Base64Decode(string base64EncodedData)
		{
			try
			{
				string decode = base64EncodedData.Decrypt();
				return decode;
			}
			catch (Exception)
			{
				var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
				return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
			}
		}

		public static string Base64Encode(string plainText, bool oldVer = false)
		{
			if (oldVer)
			{
				var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
				return System.Convert.ToBase64String(plainTextBytes);
			}
			else
			{
				return plainText.Crypt();
			}
		}

		public static bool IsBase64String(string s)
		{
			s = s.Trim();
			return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
		}

		public static long UnixTimeNow()
		{
			var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
			return (long)timeSpan.TotalSeconds;
		}

		public static bool checkClientMD5(string client)
		{
			if (!GlobalVars.AdminMode)
			{
				if (!GlobalVars.SelectedClientInfo.AlreadyHasSecurity)
				{
					string rbxexe = "";
					string BasePath = GlobalPaths.BasePath + "\\clients\\" + client;
					if (GlobalVars.SelectedClientInfo.LegacyMode)
					{
						rbxexe = BasePath + "\\RobloxApp.exe";
					}
					else if (GlobalVars.SelectedClientInfo.SeperateFolders)
					{
						rbxexe = BasePath + "\\client\\RobloxApp_client.exe";
					}
					else if (GlobalVars.SelectedClientInfo.UsesCustomClientEXEName)
					{
						rbxexe = BasePath + @"\\" + GlobalVars.SelectedClientInfo.CustomClientEXEName;
					}
					else
					{
						rbxexe = BasePath + "\\RobloxApp_client.exe";
					}
					return CheckMD5(GlobalVars.SelectedClientInfo.ClientMD5, rbxexe);
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
			if (!GlobalVars.AdminMode)
			{
				if (!GlobalVars.SelectedClientInfo.AlreadyHasSecurity)
				{
					string rbxscript = GlobalPaths.BasePath + "\\clients\\" + client + "\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua";
					return CheckMD5(GlobalVars.SelectedClientInfo.ScriptMD5, rbxscript);
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

		public static bool CheckMD5(string MD5Hash, string path)
		{
			if (!File.Exists(path))
				return false;

			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(path))
				{
					byte[] hash = md5.ComputeHash(stream);
					string clientMD5 = BitConverter.ToString(hash).Replace("-", "");
					if (clientMD5.Equals(MD5Hash))
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

		public static string GenerateMD5(string filename)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = new BufferedStream(File.OpenRead(filename), 1200000))
				{
					return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
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

		public static void RenameWindow(Process exe, ScriptType type, string clientname, string mapname)
		{
			if (!GlobalVars.SelectedClientInfo.AlreadyHasSecurity)
			{
				int time = 500;
				BackgroundWorker worker = new BackgroundWorker();
				worker.WorkerSupportsCancellation = true;
				worker.DoWork += (obj, e) => WorkerDoWork(exe, type, time, worker, clientname, mapname);
				worker.RunWorkerAsync();
			}
		}

		private static void WorkerKill(Process exe, ScriptType type, int time, BackgroundWorker worker, string clientname, string mapname)
		{
			worker.DoWork -= (obj, e) => WorkerDoWork(exe, type, time, worker, clientname, mapname);
			worker.CancelAsync();
			worker.Dispose();
		}

		private static void WorkerDoWork(Process exe, ScriptType type, int time, BackgroundWorker worker, string clientname, string mapname)
		{
			DateTime StartTimeAfterMinute = exe.StartTime.AddMinutes(1);

			if (exe.IsRunning())
			{
				while (exe.IsRunning())
				{
					if (exe.MainWindowHandle == null && DateTime.Now > StartTimeAfterMinute)
					{
						exe.Kill();
						WorkerKill(exe, type, time, worker, clientname, mapname);
						break;
					}

					switch (type)
					{
						case ScriptType.Client:
							SetWindowText(exe.MainWindowHandle, "Novetus "
								+ GlobalVars.ProgramInformation.Version + " - "
								+ clientname + " "
								+ ScriptFuncs.Generator.GetNameForType(type)
								+ " [" + GlobalVars.CurrentServer.ToString() + "]"
								+ RandomStringTitle());
							break;
						case ScriptType.Server:
						case ScriptType.Solo:
							SetWindowText(exe.MainWindowHandle, "Novetus "
								+ GlobalVars.ProgramInformation.Version + " - "
								+ clientname + " "
								+ ScriptFuncs.Generator.GetNameForType(type)
								+ (string.IsNullOrWhiteSpace(mapname) ? " [Place1]" : " [" + mapname + "]")
								+ RandomStringTitle());
							break;
						case ScriptType.Studio:
							SetWindowText(exe.MainWindowHandle, "Novetus Studio "
								+ GlobalVars.ProgramInformation.Version + " - "
								+ clientname
								+ (string.IsNullOrWhiteSpace(mapname) ? " [Place1]" : " [" + mapname + "]")
								+ RandomStringTitle());
							break;
						case ScriptType.EasterEgg:
						default:
							SetWindowText(exe.MainWindowHandle, ScriptFuncs.Generator.GetNameForType(type)
								+ RandomStringTitle());
							break;
					}

					Thread.Sleep(time);
				}
			}
			else
			{
				Thread.Sleep(time);
				RenameWindow(exe, type, clientname, mapname);
			}
		}

		public static string GetExternalIPAddress()
		{
			string ipAddress;

			try
			{
				ipAddress = new WebClient().DownloadString("https://ipv4.icanhazip.com/").TrimEnd();
			}
#if URI || LAUNCHER || BASICLAUNCHER
			catch (Exception ex)
			{
				Util.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
				ipAddress = "localhost";
			}

			return ipAddress;
		}

		//modified from https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
		public static string GenerateName(int len)
		{
			CryptoRandom r = new CryptoRandom();
			string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
			string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
			string Name = "";
			Name += consonants[r.Next(consonants.Length)].ToUpper();
			Name += vowels[r.Next(vowels.Length)];
			int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
			while (b < len)
			{
				Name += consonants[r.Next(consonants.Length)];
				b++;
				Name += vowels[r.Next(vowels.Length)];
				b++;
			}

			return Name;
		}

		//https://www.c-sharpcorner.com/article/caesar-cipher-in-c-sharp/
		public static char cipher(char ch, int key)
		{
			if (!char.IsLetter(ch))
			{
				return ch;
			}

			char d = char.IsUpper(ch) ? 'A' : 'a';
			return (char)((((ch + key) - d) % 26) + d);
		}

		public static string Encipher(string input, int key)
		{
			string output = string.Empty;

			foreach (char ch in input)
				output += cipher(ch, key);

			return output;
		}

		public static string Decipher(string input, int key)
		{
			return Encipher(input, 26 - key);
		}
	}
	#endregion
}