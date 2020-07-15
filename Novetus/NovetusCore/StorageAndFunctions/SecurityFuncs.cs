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
		
	public static string RandomString()
	{
		return RandomString(20);
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
        catch(Exception)
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
				if (GlobalVars.SelectedClientInfo.LegacyMode) 
				{
					rbxexe = GlobalPaths.BasePath + "\\clients\\" + client + "\\RobloxApp.exe";
				} 
				else 
				{
					rbxexe = GlobalPaths.BasePath + "\\clients\\" + client + "\\RobloxApp_client.exe";
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
			} else {
				return true;
			}
		} else {
			return true;
		}
	}

	public static bool CheckMD5(string MD5Hash, string path)
    {
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
			worker.DoWork += (obj, e) => WorkerDoWork(exe, type, time, worker, clientname, mapname);
			worker.RunWorkerAsync();
		}
	}
		
	private static void WorkerDoWork(Process exe, ScriptType type, int time, BackgroundWorker worker, string clientname, string mapname)
	{
		if (exe.IsRunning()) 
		{
			while (exe.IsRunning()) 
			{
				if (!exe.IsRunning()) 
				{
					worker.DoWork -= (obj, e) => WorkerDoWork(exe, type, time, worker, clientname, mapname);
					worker.CancelAsync();
					worker.Dispose();
					break;
				}

				switch (type)
				{
					case ScriptType.Client:
						SetWindowText(exe.MainWindowHandle, "Novetus " 
							+ GlobalVars.ProgramInformation.Version + " - " 
							+ clientname + " " 
							+ ScriptFuncs.Generator.GetNameForType(type) 
							+ " [" + GlobalVars.IP + ":" + GlobalVars.UserConfiguration.RobloxPort + "]" 
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
            string url = "http://checkip.dyndns.org";
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            ipAddress = a3[0];
        }
        catch (Exception)
		{
            ipAddress = "localhost";
        }

        return ipAddress;
    }

    public static async Task<string> GetExternalIPAddressAsync()
    {
        var task = Task.Factory.StartNew(() => GetExternalIPAddress());
        return await task;
    }
}
#endregion