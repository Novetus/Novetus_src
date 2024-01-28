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
		public static bool IsElevated { get { return WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid); } }

		public static string Decode(string EncodedData, bool useOldDecoding = false)
		{
			if (useOldDecoding)
            {
				return DecodeOld(EncodedData);
			}

			try
			{
				string decode = EncodedData.Decrypt();
				return decode;
			}
			catch (Exception)
			{
				return DecodeOld(EncodedData);
			}
		}

		private static string DecodeOld(string EncodedData)
        {
			var EncodedBytes = Convert.FromBase64String(EncodedData);
			return System.Text.Encoding.UTF8.GetString(EncodedBytes);
		}

		public static string Encode(string plainText, bool useOldEncoding = false)
		{
			if (useOldEncoding)
			{
				var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
				return System.Convert.ToBase64String(plainTextBytes);
			}
			else
			{
				return plainText.Crypt();
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

		private static string RandomStringTitle()
		{
			CryptoRandom random = new CryptoRandom();
			return NovetusFuncs.RandomString(random.Next(20), " ");
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
								+ Script.Generator.GetNameForType(type)
								+ " [" + GlobalVars.CurrentServer.ToString() + "]"
								+ RandomStringTitle());
							break;
						case ScriptType.Server:
						case ScriptType.Solo:
                        case ScriptType.SoloServer:
                            SetWindowText(exe.MainWindowHandle, "Novetus "
								+ GlobalVars.ProgramInformation.Version + " - "
								+ clientname + " "
								+ Script.Generator.GetNameForType(type)
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
                        case ScriptType.OutfitView:
                            SetWindowText(exe.MainWindowHandle, "Novetus Avatar 3D Preview "
                                + GlobalVars.ProgramInformation.Version + " - "
                                + clientname + " "
                                + RandomStringTitle());
                            break;
                        default:
							SetWindowText(exe.MainWindowHandle, Script.Generator.GetNameForType(type)
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

		//https://www.c-sharpcorner.com/article/caesar-cipher-in-c-sharp/
		private static char cipher(char ch, int key)
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