#region Usings
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
#endregion

#region Script Functions
public class ScriptFuncs
{
	#region Script Generator/Signer
	public class Generator
	{
		public static void SignGeneratedScript(string scriptFileName, bool newSigFormat = false, bool encodeInBase64 = true)
		{
			string privateKeyPath = Path.GetDirectoryName(scriptFileName) + "//privatekey.txt";

			if (File.Exists(privateKeyPath))
            {
				//init vars
				string format = (newSigFormat ? "--rbxsig" : "") + "%{0}%{1}";
				byte[] blob = Encoding.Default.GetBytes(File.ReadAllText(privateKeyPath));

				if (encodeInBase64)
				{
					blob = Convert.FromBase64String(Encoding.Default.GetString(blob));
				}

				//create cryptography providers
				var shaCSP = new SHA1CryptoServiceProvider();
				var rsaCSP = new RSACryptoServiceProvider();
				rsaCSP.ImportCspBlob(blob);

				// sign script
				string script = "\r\n" + File.ReadAllText(scriptFileName);
				byte[] signature = rsaCSP.SignData(Encoding.Default.GetBytes(script), shaCSP);
				// override file.
				GlobalFuncs.FixedFileDelete(scriptFileName);
				File.WriteAllText(scriptFileName, string.Format(format, Convert.ToBase64String(signature), script));
			}
			else
            {
				//create the signature file if it doesn't exist
				var signingRSACSP = new RSACryptoServiceProvider(1024);
				byte[] privateKeyBlob = signingRSACSP.ExportCspBlob(true);
				signingRSACSP.Dispose();

				// save our text file in the script's directory
				File.WriteAllText(privateKeyPath, encodeInBase64 ? Convert.ToBase64String(privateKeyBlob) : Encoding.Default.GetString(privateKeyBlob));

				// try signing again.
				SignGeneratedScript(scriptFileName, encodeInBase64);
			}
		}

		public static string GetScriptFuncForType(ScriptType type)
		{
			return GetScriptFuncForType(GlobalVars.UserConfiguration.SelectedClient, type);
		}

		public static string GetScriptFuncForType(string ClientName, ScriptType type)
		{
			FileFormat.ClientInfo info = GlobalFuncs.GetClientInfoValues(ClientName);

			string rbxexe = "";
			if (info.LegacyMode)
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp.exe";
			}
			else if (info.SeperateFolders)
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\client\\RobloxApp_client.exe";
			}
			else if (info.UsesCustomClientEXEName)
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + info.CustomClientEXEName;
			}
			else
			{
				rbxexe = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\RobloxApp_client.exe";
			}

#if LAUNCHER
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
			string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
			string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
			string md5s = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";

			switch (type)
			{
				case ScriptType.Client:
					return "_G.CSConnect("
						+ (info.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
						+ GlobalVars.IP + "',"
						+ GlobalVars.JoinPort + ",'"
						+ (info.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
						+ GlobalVars.Loadout + ","
						+ md5s + ",'"
						+ GlobalVars.PlayerTripcode
						+ ((GlobalVars.ValidatedExtraFiles > 0) ? "'," + GlobalVars.ValidatedExtraFiles.ToString() + "," : "',0,")
						+ GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
				case ScriptType.Server:
					return "_G.CSServer("
						+ GlobalVars.UserConfiguration.RobloxPort + ","
						+ GlobalVars.UserConfiguration.PlayerLimit + ","
						+ md5s + ","
						+ GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower() 
						+ ((GlobalVars.ValidatedExtraFiles > 0) ? "," + GlobalVars.ValidatedExtraFiles.ToString() + "," : ",0,")
						+ GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
				case ScriptType.Solo:
				case ScriptType.EasterEgg:
					return "_G.CSSolo("
						+ (info.UsesID ? GlobalVars.UserConfiguration.UserID : 0) + ",'"
						+ (info.UsesPlayerName ? GlobalVars.UserConfiguration.PlayerName : "Player") + "',"
						+ GlobalVars.soloLoadout + ","
						+ GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
				case ScriptType.Studio:
					return "_G.CSStudio("
						+ GlobalVars.UserConfiguration.NewGUI.ToString().ToLower() + ");";
				default:
					return "";
			}
		}

		public static string GetNameForType(ScriptType type)
		{
			switch (type)
			{
				case ScriptType.Client:
					return "Client";
				case ScriptType.Server:
					return "Server";
				case ScriptType.Solo:
					return "Play Solo";
				case ScriptType.Studio:
					return "Studio";
				case ScriptType.EasterEgg:
					return "A message from Bitl";
				default:
					return "N/A";
			}
		}

		public static void GenerateScriptForClient(ScriptType type)
		{
			GenerateScriptForClient(GlobalVars.UserConfiguration.SelectedClient, type);
		}

		public static void GenerateScriptForClient(string ClientName, ScriptType type)
		{
			bool shouldUseLoadFile = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%useloadfile%");
			string execScriptMethod = shouldUseLoadFile ? "loadfile" : "dofile";

			string[] code = {
							   "--Load Script",
							   //scriptcontents,
							   (GlobalVars.SelectedClientInfo.SeperateFolders ? "" +
									execScriptMethod + "('rbxasset://../../content/scripts/" + GlobalPaths.ScriptName + ".lua')" + (shouldUseLoadFile ? "()" : "") :
									execScriptMethod + "('rbxasset://scripts/" + GlobalPaths.ScriptName + ".lua')" + (shouldUseLoadFile ? "()" : "")),
							   GetScriptFuncForType(type),
							   !string.IsNullOrWhiteSpace(GlobalPaths.AddonScriptPath) ? "dofile('" + GlobalPaths.AddonScriptPath + "')" : ""
							};

			if (GlobalVars.SelectedClientInfo.SeperateFolders)
            {
				string scriptsFolder = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + GlobalFuncs.GetClientSeperateFolderName(type) + @"\\content\\scripts";
				if (!Directory.Exists(scriptsFolder))
                {
					Directory.CreateDirectory(scriptsFolder);
                }
            }

			string outputPath = (GlobalVars.SelectedClientInfo.SeperateFolders ?
						GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + GlobalFuncs.GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua" :
						GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua");

			File.WriteAllLines(outputPath, code);

			bool shouldSign = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%signgeneratedjoinscript%");
			bool shouldUseNewSigFormat = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%usenewsignformat%");

			if (shouldSign)
			{
				SignGeneratedScript(outputPath, shouldUseNewSigFormat);
			}
		}
	}
#endregion

	#region ClientScript Parser
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
#if URI || LAUNCHER || CMD || BASICLAUNCHER
			catch (Exception ex)
			{
				GlobalFuncs.LogExceptions(ex);
#else
			catch (Exception)
			{
#endif
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

		public static string GetTagFromType(ScriptType type, bool endtag, bool no3d)
		{
			switch (type)
			{
				case ScriptType.Client:
					return endtag ? "</client>" : "<client>";
				case ScriptType.Server:
					return no3d ? (endtag ? "</no3d>" : "<no3d>") : (endtag ? "</server>" : "<server>");
				case ScriptType.Solo:
					return endtag ? "</solo>" : "<solo>";
				case ScriptType.Studio:
					return endtag ? "</studio>" : "<studio>";
				default:
					return "";
			}
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
			try
			{
				string result = source.Substring(0, source.IndexOf(seperator));

				if (File.Exists(GlobalPaths.MapsDir + @"\\" + result + @"\\" + source))
				{
					return result + @"\\" + source;
				}
				else
				{
					return source;
				}
			}
#if URI || LAUNCHER || CMD || BASICLAUNCHER
			catch (Exception ex)
			{
				GlobalFuncs.LogExceptions(ex);
#else
			catch (Exception)
			{
#endif
				return source;
			}
		}

		public static string GetFolderAndMapName(string source)
		{
			return GetFolderAndMapName(source, " -");
		}

		public static string GetRawArgsForType(ScriptType type, string ClientName, string luafile)
		{
			FileFormat.ClientInfo info = GlobalFuncs.GetClientInfoValues(ClientName);

			if (!info.Fix2007)
			{
				return Generator.GetScriptFuncForType(ClientName, type);
			}
			else
			{
				return luafile;
			}
		}

		public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
		{
			return CompileScript(GlobalVars.UserConfiguration.SelectedClient, code, tag, endtag, mapfile, luafile, rbxexe, usesharedtags);
		}

		public static string CompileScript(string ClientName, string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
		{
			string start = tag;
			string end = endtag;

			FileFormat.ClientInfo info = GlobalFuncs.GetClientInfoValues(ClientName);

			ScriptType type = GetTypeFromTag(start);

			//we must have the ending tag before we continue.
			if (string.IsNullOrWhiteSpace(end))
			{
				return "";
			}

			if (usesharedtags)
            {
				string sharedstart = "<shared>";
				string sharedend = "</shared>";

				if (code.Contains(sharedstart) && code.Contains(sharedend))
				{
					start = sharedstart;
					end = sharedend;
				}
            }

			if (info.Fix2007)
			{
				Generator.GenerateScriptForClient(type);
			}

			string extractedCode = GetArgsFromTag(code, start, end);

			if (extractedCode.Contains("%donothing%"))
			{
				return "";
			}

#if LAUNCHER
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
			string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
			string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
			string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
            string md5sd = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
			string md5s = "'" + info.ClientMD5 + "','" + md5dir + "','" + info.ScriptMD5 + "'";
			string compiled = extractedCode.Replace("%version%", GlobalVars.ProgramInformation.Version)
					.Replace("%mapfile%", mapfile)
					.Replace("%luafile%", luafile)
					.Replace("%charapp%", GlobalVars.UserCustomization.CharacterID)
					.Replace("%ip%", GlobalVars.IP)
					.Replace("%port%", GlobalVars.UserConfiguration.RobloxPort.ToString())
					.Replace("%joinport%", GlobalVars.JoinPort.ToString())
					.Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
					.Replace("%icone%", ConvertIconStringToInt().ToString())
					.Replace("%icon%", GlobalVars.UserCustomization.Icon)
					.Replace("%id%", GlobalVars.UserConfiguration.UserID.ToString())
					.Replace("%face%", GlobalVars.UserCustomization.Face)
					.Replace("%head%", GlobalVars.UserCustomization.Head)
					.Replace("%tshirt%", GlobalVars.UserCustomization.TShirt)
					.Replace("%shirt%", GlobalVars.UserCustomization.Shirt)
					.Replace("%pants%", GlobalVars.UserCustomization.Pants)
					.Replace("%hat1%", GlobalVars.UserCustomization.Hat1)
					.Replace("%hat2%", GlobalVars.UserCustomization.Hat2)
					.Replace("%hat3%", GlobalVars.UserCustomization.Hat3)
					.Replace("%faced%", GlobalVars.UserCustomization.Face.Contains("http://") ? GlobalVars.UserCustomization.Face : GlobalPaths.faceGameDir + GlobalVars.UserCustomization.Face)
					.Replace("%headd%", GlobalPaths.headGameDir + GlobalVars.UserCustomization.Head)
					.Replace("%tshirtd%", GlobalVars.UserCustomization.TShirt.Contains("http://") ? GlobalVars.UserCustomization.TShirt : GlobalPaths.tshirtGameDir + GlobalVars.UserCustomization.TShirt)
					.Replace("%shirtd%", GlobalVars.UserCustomization.Shirt.Contains("http://") ? GlobalVars.UserCustomization.Shirt : GlobalPaths.shirtGameDir + GlobalVars.UserCustomization.Shirt)
					.Replace("%pantsd%", GlobalVars.UserCustomization.Pants.Contains("http://") ? GlobalVars.UserCustomization.Pants : GlobalPaths.pantsGameDir + GlobalVars.UserCustomization.Pants)
					.Replace("%hat1d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat1)
					.Replace("%hat2d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat2)
					.Replace("%hat3d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Hat3)
					.Replace("%headcolor%", GlobalVars.UserCustomization.HeadColorID.ToString())
					.Replace("%torsocolor%", GlobalVars.UserCustomization.TorsoColorID.ToString())
					.Replace("%larmcolor%", GlobalVars.UserCustomization.LeftArmColorID.ToString())
					.Replace("%llegcolor%", GlobalVars.UserCustomization.LeftLegColorID.ToString())
					.Replace("%rarmcolor%", GlobalVars.UserCustomization.RightArmColorID.ToString())
					.Replace("%rlegcolor%", GlobalVars.UserCustomization.RightLegColorID.ToString())
					.Replace("%md5launcher%", md5dir)
					.Replace("%md5script%", info.ScriptMD5)
					.Replace("%md5exe%", info.ClientMD5)
					.Replace("%md5scriptd%", md5script)
					.Replace("%md5exed%", md5exe)
					.Replace("%md5s%", md5s)
					.Replace("%md5sd%", md5sd)
					.Replace("%limit%", GlobalVars.UserConfiguration.PlayerLimit.ToString())
					.Replace("%extra%", GlobalVars.UserCustomization.Extra)
					.Replace("%hat4%", GlobalVars.UserCustomization.Extra)
					.Replace("%extrad%", GlobalPaths.extraGameDir + GlobalVars.UserCustomization.Extra)
					.Replace("%hat4d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.Extra)
					.Replace("%mapfiled%", GlobalPaths.BaseGameDir + GlobalVars.UserConfiguration.MapPathSnip.Replace(@"\\", @"\").Replace(@"/", @"\"))
					.Replace("%mapfilec%", extractedCode.Contains("%mapfilec%") ? GlobalFuncs.CopyMapToRBXAsset() : "")
					.Replace("%tripcode%", GlobalVars.PlayerTripcode)
					.Replace("%scripttype%", Generator.GetNameForType(type))
					.Replace("%addonscriptpath%", GlobalPaths.AddonScriptPath)
					.Replace("%notifications%", GlobalVars.UserConfiguration.ShowServerNotifications.ToString().ToLower())
					.Replace("%loadout%", code.Contains("<solo>") ? GlobalVars.soloLoadout : GlobalVars.Loadout)
					.Replace("%doublequote%", "\"")
					.Replace("%validatedextrafiles%", GlobalVars.ValidatedExtraFiles.ToString())
					.Replace("%argstring%", GetRawArgsForType(type, ClientName, luafile))
					.Replace("%tshirttexid%", GlobalVars.TShirtTextureID)
					.Replace("%shirttexid%", GlobalVars.ShirtTextureID)
					.Replace("%pantstexid%", GlobalVars.PantsTextureID)
					.Replace("%facetexid%", GlobalVars.FaceTextureID)
					.Replace("%tshirttexidlocal%", GlobalVars.TShirtTextureLocal)
					.Replace("%shirttexidlocal%", GlobalVars.ShirtTextureLocal)
					.Replace("%pantstexidlocal%", GlobalVars.PantsTextureLocal)
					.Replace("%facetexlocal%", GlobalVars.FaceTextureLocal)
					.Replace("%newgui%", GlobalVars.UserConfiguration.NewGUI.ToString().ToLower());

			if (compiled.Contains("%disabled%"))
            {
				MessageBox.Show("This option has been disabled for this client.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "";
			}

			return compiled;
		}
	}
#endregion
}
#endregion