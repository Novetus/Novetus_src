using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Novetus.Core
{
    #region Script Functions
    public class Script
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
                    IOSafe.File.Delete(scriptFileName);
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
                return GetScriptFuncForType(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), type);
            }

            public static string GetScriptFuncForType(string ClientName, ScriptType type)
            {
                FileFormat.ClientInfoLegacy info = Client.GetClientInfoValues(ClientName);

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

                string serverIP = (type == ScriptType.SoloServer ? "localhost" : GlobalVars.CurrentServer.ServerIP);
                int serverjoinport = (type == ScriptType.Solo ? GlobalVars.PlaySoloPort : GlobalVars.CurrentServer.ServerPort);
                int serverhostport = (type == ScriptType.SoloServer ? GlobalVars.PlaySoloPort : GlobalVars.UserConfiguration.ReadSettingInt("RobloxPort"));
                string playerLimit = (type == ScriptType.SoloServer ? "1" : GlobalVars.UserConfiguration.ReadSetting("PlayerLimit"));
                string joinNotifs = (type == ScriptType.SoloServer ? "false" : GlobalVars.UserConfiguration.ReadSetting("ShowServerNotifications").ToLower());

                switch (type)
                {
                    case ScriptType.Client:
                    case ScriptType.Solo:
                        return "_G.CSConnect("
                            + (info.UsesID ? GlobalVars.UserConfiguration.ReadSettingInt("UserID") : 0) + ",'"
                            + serverIP + "',"
                            + serverjoinport + ",'"
                            + (info.UsesPlayerName ? GlobalVars.UserConfiguration.ReadSetting("PlayerName") : "Player") + "',"
                            + GlobalVars.Loadout + ","
                            + md5s + ",'"
                            + GlobalVars.PlayerTripcode
                            + ((GlobalVars.ValidatedExtraFiles > 0) ? "'," + GlobalVars.ValidatedExtraFiles.ToString() + "," : "',0,")
                            + GlobalVars.UserConfiguration.ReadSetting("NewGUI").ToLower() + ");";
                    case ScriptType.Server:
                    case ScriptType.SoloServer:
                        return "_G.CSServer("
                            + serverhostport + ","
                            + playerLimit + ","
                            + md5s + ","
                            + joinNotifs
                            + ((GlobalVars.ValidatedExtraFiles > 0) ? "," + GlobalVars.ValidatedExtraFiles.ToString() + "," : ",0,")
                            + GlobalVars.UserConfiguration.ReadSetting("NewGUI").ToLower() + ");";
                    case ScriptType.Studio:
                        return "_G.CSStudio("
                            + GlobalVars.UserConfiguration.ReadSetting("NewGUI").ToLower() + ");";
                    case ScriptType.OutfitView:
                        return "_G.CS3DView(0,'"
                            + GlobalVars.UserConfiguration.ReadSetting("PlayerName") + "',"
                            + GlobalVars.Loadout + ");";
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
                    case ScriptType.SoloServer:
                        return "Play Solo";
                    case ScriptType.Studio:
                        return "Studio";
                    case ScriptType.OutfitView:
                        return "Avatar 3D Preview";
                    default:
                        return "N/A";
                }
            }

            public static void GenerateScriptForClient(ScriptType type)
            {
                GenerateScriptForClient(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), type);
            }

            public static void GenerateScriptForClient(string ClientName, ScriptType type)
            {
                string outputPath = (GlobalVars.SelectedClientInfo.SeperateFolders ?
                            GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + Client.GetClientSeperateFolderName(type) + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua" :
                            GlobalPaths.ClientDir + @"\\" + ClientName + @"\\content\\scripts\\" + GlobalPaths.ScriptGenName + ".lua");

                IOSafe.File.Delete(outputPath);

                bool shouldUseLoadFile = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%useloadfile%");
                string execScriptMethod = shouldUseLoadFile ? "loadfile" : "dofile";

                string[] code = {
                               "--Load Script",
							   //scriptcontents,
							   (GlobalVars.SelectedClientInfo.SeperateFolders ? "" +
                                    execScriptMethod + "('rbxasset://../../content/scripts/" + GlobalPaths.ScriptName + ".lua')" + (shouldUseLoadFile ? "()" : "") :
                                    execScriptMethod + "('rbxasset://scripts/" + GlobalPaths.ScriptName + ".lua')" + (shouldUseLoadFile ? "()" : "")),
                               GetScriptFuncForType(type),
                            };

                if (GlobalVars.SelectedClientInfo.SeperateFolders)
                {
                    string scriptsFolder = GlobalPaths.ClientDir + @"\\" + ClientName + @"\\" + Client.GetClientSeperateFolderName(type) + @"\\content\\scripts";
                    if (!Directory.Exists(scriptsFolder))
                    {
                        Directory.CreateDirectory(scriptsFolder);
                    }
                }

                File.WriteAllLines(outputPath, code);

                bool shouldSign = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%signgeneratedjoinscript%");
                bool shouldUseNewSigFormat = GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%usenewsignformat%");

                if (shouldSign)
                {
                    SignGeneratedScript(outputPath, shouldUseNewSigFormat);
                }
            }

            public static string GetGeneratedScriptName(string ClientName, ScriptType type)
            {
                GenerateScriptForClient(ClientName, type);
                return Client.GetGenLuaFileName(ClientName, type);
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
                    if (string.IsNullOrEmpty(endtag))
                    {
                        //VERSION 2!!
                        string result = code.Substring(code.IndexOf(tag) + tag.Length);
                        return result;
                    }
                    else
                    {
                        int pFrom = code.IndexOf(tag) + tag.Length;
                        int pTo = code.LastIndexOf(endtag);
                        string result = code.Substring(pFrom, pTo - pFrom);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    return "";
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

            public static string GetTagFromType(ScriptType type, bool endtag, bool no3d, bool v1)
            {
                if (v1)
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
                else
                {
                    if (endtag == true)
                    {
                        //NO END TAGS.
                        return "";
                    }

                    switch (type)
                    {
                        case ScriptType.Client:
                            return "client=";
                        case ScriptType.Server:
                            return no3d ? "no3d=" : "server=";
                        case ScriptType.Solo:
                            return "solo=";
                        case ScriptType.Studio:
                            return "studio=";
                        default:
                            return "";
                    }
                }
            }

            public static int ConvertIconStringToInt()
            {
                switch (GlobalVars.UserCustomization.ReadSetting("Icon"))
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
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                    return source;
                }
            }

            public static string GetFolderAndMapName(string source)
            {
                return GetFolderAndMapName(source, " -");
            }

            public static string GetRawArgsForType(ScriptType type, string ClientName, string luafile)
            {
                FileFormat.ClientInfoLegacy info = Client.GetClientInfoValues(ClientName);

                if (!info.Fix2007)
                {
                    return Generator.GetScriptFuncForType(ClientName, type);
                }
                else
                {
                    return luafile;
                }
            }

            public static string CopyMapToRBXAsset()
            {
                string clientcontentpath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.ReadSetting("SelectedClient") + @"\\content\\temp.rbxl";
                IOSafe.File.Copy(GlobalVars.UserConfiguration.ReadSetting("MapPath"), clientcontentpath, true);
                return GlobalPaths.AltBaseGameDir + "temp.rbxl";
            }

            public static string CompileScript(string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
            {
                return CompileScript(GlobalVars.UserConfiguration.ReadSetting("SelectedClient"), code, tag, endtag, mapfile, luafile, rbxexe, usesharedtags);
            }

            //TODO I'll deal with this later.....

            public static string CompileScript(string ClientName, string code, string tag, string endtag, string mapfile, string luafile, string rbxexe, bool usesharedtags = true)
            {
                string start = tag;
                string end = endtag;

                bool v1 = false;

                if (code.Contains("<") &&
                    code.Contains("</") &&
                    code.Contains(">"))
                {
                    v1 = true;
                }
                else
                {
                    //make sure we have no end tags.
                    if (!string.IsNullOrWhiteSpace(end))
                    {
                        end = "";
                    }
                }

                FileFormat.ClientInfoLegacy info = Client.GetClientInfoValues(ClientName);

                ScriptType type = GetTypeFromTag(start);

                //we must have the ending tag before we continue.
                if (v1 && string.IsNullOrWhiteSpace(end))
                {
                    return "";
                }

                if (usesharedtags)
                {
                    if (v1)
                    {
                        string sharedstart = "<shared>";
                        string sharedend = "</shared>";

                        if (code.Contains(sharedstart) && code.Contains(sharedend))
                        {
                            start = sharedstart;
                            end = sharedend;
                        }
                    }
                    else
                    {
                        string sharedstart = "shared=";

                        if (code.Contains(sharedstart))
                        {
                            start = sharedstart;
                        }
                    }
                }

                if (info.Fix2007)
                {
                    Generator.GenerateScriptForClient(type);
                }

                string extractedCode = GetArgsFromTag(code, start, end);
#if LAUNCHER
			    string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(Assembly.GetExecutingAssembly().Location) : "";
#else
                string md5dir = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.RootPathLauncher + "\\Novetus.exe") : "";
#endif
                string md5script = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.ReadSetting("SelectedClient") + @"\\content\\scripts\\" + GlobalPaths.ScriptName + ".lua") : "";
                string md5exe = !info.AlreadyHasSecurity ? SecurityFuncs.GenerateMD5(rbxexe) : "";
                string md5sd = "'" + md5exe + "','" + md5dir + "','" + md5script + "'";
                string md5s = "'" + info.ClientMD5 + "','" + md5dir + "','" + info.ScriptMD5 + "'";
                string compiled = extractedCode.Replace("%version%", GlobalVars.ProgramInformation.Version)
                        .Replace("%mapfile%", mapfile)
                        .Replace("%luafile%", luafile)
                        .Replace("%charapp%", GlobalVars.UserCustomization.ReadSetting("CharacterID"))
                        .Replace("%server%", GlobalVars.CurrentServer.ToString())
                        .Replace("%ip%", GlobalVars.CurrentServer.ServerIP)
                        .Replace("%port%", GlobalVars.UserConfiguration.ReadSetting("RobloxPort"))
                        .Replace("%joinport%", GlobalVars.CurrentServer.ServerPort.ToString())
                        .Replace("%name%", GlobalVars.UserConfiguration.ReadSetting("PlayerName"))
                        .Replace("%icone%", ConvertIconStringToInt().ToString())
                        .Replace("%icon%", GlobalVars.UserCustomization.ReadSetting("Icon"))
                        .Replace("%id%", GlobalVars.UserConfiguration.ReadSetting("UserID"))
                        .Replace("%face%", GlobalVars.UserCustomization.ReadSetting("Face"))
                        .Replace("%head%", GlobalVars.UserCustomization.ReadSetting("Head"))
                        .Replace("%tshirt%", GlobalVars.UserCustomization.ReadSetting("TShirt"))
                        .Replace("%shirt%", GlobalVars.UserCustomization.ReadSetting("Shirt"))
                        .Replace("%pants%", GlobalVars.UserCustomization.ReadSetting("Pants"))
                        .Replace("%hat1%", GlobalVars.UserCustomization.ReadSetting("Hat1"))
                        .Replace("%hat2%", GlobalVars.UserCustomization.ReadSetting("Hat2"))
                        .Replace("%hat3%", GlobalVars.UserCustomization.ReadSetting("Hat3"))
                        .Replace("%faced%", GlobalVars.UserCustomization.ReadSetting("Face").Contains("http://") ? GlobalVars.UserCustomization.ReadSetting("Face") : GlobalPaths.faceGameDir + GlobalVars.UserCustomization.ReadSetting("Face"))
                        .Replace("%headd%", GlobalPaths.headGameDir + GlobalVars.UserCustomization.ReadSetting("Head"))
                        .Replace("%tshirtd%", GlobalVars.UserCustomization.ReadSetting("TShirt").Contains("http://") ? GlobalVars.UserCustomization.ReadSetting("TShirt") : GlobalPaths.tshirtGameDir + GlobalVars.UserCustomization.ReadSetting("TShirt"))
                        .Replace("%shirtd%", GlobalVars.UserCustomization.ReadSetting("Shirt").Contains("http://") ? GlobalVars.UserCustomization.ReadSetting("Shirt") : GlobalPaths.shirtGameDir + GlobalVars.UserCustomization.ReadSetting("Shirt"))
                        .Replace("%pantsd%", GlobalVars.UserCustomization.ReadSetting("Pants").Contains("http://") ? GlobalVars.UserCustomization.ReadSetting("Pants") : GlobalPaths.pantsGameDir + GlobalVars.UserCustomization.ReadSetting("Pants"))
                        .Replace("%hat1d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.ReadSetting("Hat1"))
                        .Replace("%hat2d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.ReadSetting("Hat1"))
                        .Replace("%hat3d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.ReadSetting("Hat1"))
                        .Replace("%headcolor%", GlobalVars.UserCustomization.ReadSetting("HeadColorID"))
                        .Replace("%torsocolor%", GlobalVars.UserCustomization.ReadSetting("TorsoColorID"))
                        .Replace("%larmcolor%", GlobalVars.UserCustomization.ReadSetting("LeftArmColorID"))
                        .Replace("%llegcolor%", GlobalVars.UserCustomization.ReadSetting("LeftLegColorID"))
                        .Replace("%rarmcolor%", GlobalVars.UserCustomization.ReadSetting("RightArmColorID"))
                        .Replace("%rlegcolor%", GlobalVars.UserCustomization.ReadSetting("RightLegColorID"))
                        .Replace("%md5launcher%", md5dir)
                        .Replace("%md5script%", info.ScriptMD5)
                        .Replace("%md5exe%", info.ClientMD5)
                        .Replace("%md5scriptd%", md5script)
                        .Replace("%md5exed%", md5exe)
                        .Replace("%md5s%", md5s)
                        .Replace("%md5sd%", md5sd)
                        .Replace("%limit%", GlobalVars.UserConfiguration.ReadSetting("PlayerLimit"))
                        .Replace("%extra%", GlobalVars.UserCustomization.ReadSetting("Extra"))
                        .Replace("%hat4%", GlobalVars.UserCustomization.ReadSetting("Extra"))
                        .Replace("%extrad%", GlobalPaths.extraGameDir + GlobalVars.UserCustomization.ReadSetting("Extra"))
                        .Replace("%hat4d%", GlobalPaths.hatGameDir + GlobalVars.UserCustomization.ReadSetting("Extra"))
                        .Replace("%mapfiled%", GlobalPaths.BaseGameDir + GlobalVars.UserConfiguration.ReadSetting("MapPathSnip").Replace(@"\\", @"\").Replace(@"/", @"\"))
                        .Replace("%mapfilec%", extractedCode.Contains("%mapfilec%") ? CopyMapToRBXAsset() : "")
                        .Replace("%tripcode%", GlobalVars.PlayerTripcode)
                        .Replace("%scripttype%", Generator.GetNameForType(type))
                        .Replace("%notifications%", GlobalVars.UserConfiguration.ReadSetting("ShowServerNotifications").ToLower())
                        .Replace("%loadout%", GlobalVars.Loadout)
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
                        .Replace("%newgui%", GlobalVars.UserConfiguration.ReadSetting("NewGUI").ToLower());

                return compiled;
            }
        }
        #endregion
    }
    #endregion
}
