#region Usings
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
#endregion

namespace Novetus.Core
{
	#region Security Functions
	public class SecurityFuncs
	{
		[DllImport("user32.dll")]
		static extern int SetWindowText(IntPtr hWnd, string text);
		public static bool IsElevated { get { return WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid); } }

        public enum OldEncodingMode_t
		{
			MODE_AES,
			MODE_BASE64,
			MODE_DES
		}

		public static string Decode(string EncodedData, OldEncodingMode_t iDecodingMode = OldEncodingMode_t.MODE_AES, bool ignoreEncodingMode = true)
		{
            if ((iDecodingMode != OldEncodingMode_t.MODE_AES) && !ignoreEncodingMode)
            {
                switch (iDecodingMode)
                {
                    case OldEncodingMode_t.MODE_BASE64:
                        return DecodeOld(EncodedData);
                    case OldEncodingMode_t.MODE_DES:
                        return EncodedData.DecryptDES();
                }

                return "";
            }

            try
			{
				string decode = EncodedData.DecryptAES();
				return decode;
			}
			catch (Exception)
			{
				if (!ignoreEncodingMode)
				{
					try
					{
						string decode2 = EncodedData.DecryptDES();
						return decode2;
					}
					catch (Exception)
					{
						return DecodeOld(EncodedData);
					}
				}

                return "";
            }
		}

		private static string DecodeOld(string EncodedData)
        {
			var EncodedBytes = Convert.FromBase64String(EncodedData);
			return System.Text.Encoding.UTF8.GetString(EncodedBytes);
		}

		public static string Encode(string plainText, OldEncodingMode_t iEncodingMode = OldEncodingMode_t.MODE_AES, bool ignoreEncodingMode = true)
		{
			if ((iEncodingMode != OldEncodingMode_t.MODE_AES) && !ignoreEncodingMode)
			{
				switch (iEncodingMode)
				{
					case OldEncodingMode_t.MODE_BASE64:
                        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                        return Convert.ToBase64String(plainTextBytes);
					case OldEncodingMode_t.MODE_DES:
                        return plainText.CryptDES();
                }

				return "";
			}
			else
			{
                return plainText.CryptAES();
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
			//add a smaller delay time so the client can load fully.
			//based of half the time of the initial ClientLaunchTime.
			//Ex. due to this, 2012M is 1.5 minutes rather than 1 minute.
			GlobalVars.ClientLoadDelay = DateTime.Now.AddMinutes(GlobalVars.SelectedClientInfo.ClientLaunchTime * 0.5);

			//int moduleCountOnAppLaunch = exe.Modules.Count;

            if (exe.IsRunning())
			{
				while (exe.IsRunning())
				{
                    if (!exe.IsRunning())
                    {
                        WorkerKill(exe, type, time, worker, clientname, mapname);
                        return;
                    }

					if (DateTime.Now > GlobalVars.ClientLoadDelay)
					{
						Client.ResetScripts();
						//add an additional amount of time each cycle so new client windows can create scripts
						//based of half the time of the initial ClientLaunchTime.
						//Ex. 2012M has cycle times of 0.5 seconds.
						GlobalVars.ClientLoadDelay = DateTime.Now.AddMinutes(GlobalVars.SelectedClientInfo.ClientLaunchTime * 0.5);
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

                    /*int moduleCount = exe.Modules.Count;

					if (moduleCount != moduleCountOnAppLaunch && (type != ScriptType.Solo && type != ScriptType.SoloServer))
					{
                        WorkerKill(exe, type, time, worker, clientname, mapname);
                        exe.Close();
                        return;
                    }*/

                    Thread.Sleep(time);
				}
			}
			else
			{
				Thread.Sleep(time);
				RenameWindow(exe, type, clientname, mapname);
			}
		}

		public static string GenArrays()
		{
            CryptoRandom random = new CryptoRandom();

            string aeskey = "public static byte[] aeskey = new byte[32] { ";

            for (int i = 1; i < 33; i++)
            {
                aeskey += random.Next(0, 255) + ((i == 32) ? "" : ", ");
            }

            aeskey += " };";

            string aesiv = "public static byte[] aesiv = new byte[16] { ";

            for (int i = 1; i < 17; i++)
            {
                aesiv += random.Next(0, 255) + ((i == 16) ? "" : ", ");
            }

            aesiv += " };";

			return string.Join("\n" , new string[] { aeskey, aesiv });
		}
    }
	#endregion
}