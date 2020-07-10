#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region SDKApps
    enum SDKApps
    {
        ItemSDK = 0,
        ClientSDK = 1,
        ClientScriptDoc = 2,
        AssetLocalizer = 3,
        SplashTester = 4,
        Obj2MeshV1GUI = 5,
        ScriptGenerator = 6,
        LegacyPlaceConverter = 7,
        DiogenesEditor = 8
    }
    #endregion

    #region SDK Functions
    class SDKFuncs
    {
        #region Asset Localizer
        public static OpenFileDialog LoadROBLOXFileDialog(RobloxFileType type)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = (type == RobloxFileType.RBXL) ? "ROBLOX Level (*.rbxl)|*.rbxl" : "ROBLOX Model (*.rbxm)|*.rbxm",
                Title = "Open ROBLOX level or model"
            };

            return openFileDialog1;
        }

        public static RobloxFileType SelectROBLOXFileType(int index)
        {
            RobloxFileType type;

            switch (index)
            {
                case 1:
                    type = RobloxFileType.RBXM;
                    break;
                case 2:
                    type = RobloxFileType.Hat;
                    break;
                case 3:
                    type = RobloxFileType.Head;
                    break;
                case 4:
                    type = RobloxFileType.Face;
                    break;
                case 5:
                    type = RobloxFileType.Shirt;
                    break;
                case 6:
                    type = RobloxFileType.TShirt;
                    break;
                case 7:
                    type = RobloxFileType.Pants;
                    break;
                default:
                    type = RobloxFileType.RBXL;
                    break;
            }

            return type;
        }

        public static string GetProgressString(RobloxFileType type, int percent)
        {
            string progressString = "";

            switch (type)
            {
                case RobloxFileType.RBXL:
                    switch (percent)
                    {
                        case 0:
                            progressString = "Backing up RBXL...";
                            break;
                        case 5:
                            progressString = "Downloading RBXL Meshes and Textures...";
                            break;
                        case 10:
                            progressString = "Downloading RBXL Skybox Textures...";
                            break;
                        case 15:
                            progressString = "Downloading RBXL Decal Textures...";
                            break;
                        case 20:
                            progressString = "Downloading RBXL Textures...";
                            break;
                        case 25:
                            progressString = "Downloading RBXL Tool Textures...";
                            break;
                        case 30:
                            progressString = "Downloading RBXL HopperBin Textures...";
                            break;
                        case 40:
                            progressString = "Downloading RBXL Sounds...";
                            break;
                        case 50:
                            progressString = "Downloading RBXL GUI Textures...";
                            break;
                        case 60:
                            progressString = "Downloading RBXL Shirt Textures...";
                            break;
                        case 65:
                            progressString = "Downloading RBXL T-Shirt Textures...";
                            break;
                        case 70:
                            progressString = "Downloading RBXL Pants Textures...";
                            break;
                        case 80:
                            progressString = "Downloading RBXL Linked Scripts...";
                            break;
                        case 90:
                            progressString = "Downloading RBXL Linked LocalScripts...";
                            break;
                    }
                    break;
                case RobloxFileType.RBXM:
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading RBXL Meshes and Textures...";
                            break;
                        case 10:
                            progressString = "Downloading RBXL Skybox Textures...";
                            break;
                        case 15:
                            progressString = "Downloading RBXL Decal Textures...";
                            break;
                        case 20:
                            progressString = "Downloading RBXL Textures...";
                            break;
                        case 25:
                            progressString = "Downloading RBXL Tool Textures...";
                            break;
                        case 30:
                            progressString = "Downloading RBXL HopperBin Textures...";
                            break;
                        case 40:
                            progressString = "Downloading RBXL Sounds...";
                            break;
                        case 50:
                            progressString = "Downloading RBXL GUI Textures...";
                            break;
                        case 60:
                            progressString = "Downloading RBXL Shirt Textures...";
                            break;
                        case 65:
                            progressString = "Downloading RBXL T-Shirt Textures...";
                            break;
                        case 70:
                            progressString = "Downloading RBXL Pants Textures...";
                            break;
                        case 80:
                            progressString = "Downloading RBXL Linked Scripts...";
                            break;
                        case 90:
                            progressString = "Downloading RBXL Linked LocalScripts...";
                            break;
                    }
                    break;
                case RobloxFileType.Hat:
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Hat Meshes and Textures...";
                            break;
                        case 25:
                            progressString = "Downloading Hat Sounds...";
                            break;
                        case 50:
                            progressString = "Downloading Hat Linked Scripts...";
                            break;
                        case 75:
                            progressString = "Downloading Hat Linked LocalScripts...";
                            break;
                    }
                    break;
                case RobloxFileType.Head:
                    //meshes
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Head Meshes and Textures...";
                            break;
                    }
                    break;
                case RobloxFileType.Face:
                    //decal
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Face Textures...";
                            break;
                    }
                    break;
                case RobloxFileType.TShirt:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading T-Shirt Textures...";
                            break;
                    }
                    break;
                case RobloxFileType.Shirt:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Shirt Textures...";
                            break;
                    }
                    break;
                case RobloxFileType.Pants:
                    //texture
                    switch (percent)
                    {
                        case 0:
                            progressString = "Downloading Pants Textures...";
                            break;
                    }
                    break;
                default:
                    progressString = "Idle";
                    break;
            }

            return progressString + " " + percent.ToString() + "%";
        }

        public static void LocalizeAsset(RobloxFileType type, BackgroundWorker worker, string path, string itemname, string meshname)
        {
            try
            {
                switch (type)
                {
                    case RobloxFileType.RBXL:
                        //backup the original copy
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxl", " BAK.rbxl"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //meshes
                        worker.ReportProgress(5);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                        //skybox
                        worker.ReportProgress(10);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                        //decal
                        worker.ReportProgress(15);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Decal);
                        //texture
                        worker.ReportProgress(20);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Texture);
                        //tools and hopperbin
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Tool);
                        worker.ReportProgress(30);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.HopperBin);
                        //sound
                        worker.ReportProgress(40);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sound);
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ImageLabel);
                        //clothing
                        worker.ReportProgress(60);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Shirt);
                        worker.ReportProgress(65);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                        worker.ReportProgress(70);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Pants);
                        //scripts
                        worker.ReportProgress(80);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Script);
                        worker.ReportProgress(90);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.LocalScript);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.RBXM:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //meshes
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Fonts, 1, 1, 1, 1);
                        //skybox
                        worker.ReportProgress(10);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 1, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 2, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 3, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 4, 0, 0, 0);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sky, 5, 0, 0, 0);
                        //decal
                        worker.ReportProgress(15);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Decal);
                        //texture
                        worker.ReportProgress(20);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Texture);
                        //tools and hopperbin
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Tool);
                        worker.ReportProgress(30);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.HopperBin);
                        //sound
                        worker.ReportProgress(40);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Sound);
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ImageLabel);
                        //clothing
                        worker.ReportProgress(60);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Shirt);
                        worker.ReportProgress(65);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ShirtGraphic);
                        worker.ReportProgress(70);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Pants);
                        //scripts
                        worker.ReportProgress(80);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.Script);
                        worker.ReportProgress(90);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.LocalScript);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.Hat:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //meshes
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatFonts, itemname, meshname);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatFonts, 1, 1, 1, 1, itemname);
                        worker.ReportProgress(25);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatSound);
                        //scripts
                        worker.ReportProgress(50);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatScript);
                        worker.ReportProgress(75);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHatLocalScript);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.Head:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //meshes
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, itemname);
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemHeadFonts, 1, 1, 1, 1, itemname);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.Face:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //decal
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemFaceTexture, itemname);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.TShirt:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //texture
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemTShirtTexture, itemname);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.Shirt:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //texture
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemShirtTexture, itemname);
                        worker.ReportProgress(100);
                        break;
                    case RobloxFileType.Pants:
                        if (GlobalVars.UserConfiguration.AssetLocalizerSaveBackups)
                        {
                            try
                            {
                                worker.ReportProgress(0);
                                File.Copy(path, path.Replace(".rbxm", " BAK.rbxm"));
                            }
                            catch (Exception)
                            {
                                worker.ReportProgress(100);
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0);
                        }
                        //texture
                        RobloxXMLLocalizer.DownloadFromNodes(path, RobloxDefs.ItemPantsTexture, itemname);
                        worker.ReportProgress(100);
                        break;
                    default:
                        worker.ReportProgress(100);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Unable to localize the asset. " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Client SDK
        public static string SaveClientinfoAndGetPath(FileFormat.ClientInfo info, bool islocked, bool textonly = false)
        {
            string path = "";

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = textonly ? "Text file (*.txt)|*.txt" : "Novetus Clientinfo files (*.nov)|*.nov";
                sfd.FilterIndex = 1;
                string filename = textonly ? "clientinfo.txt" : "clientinfo.nov";
                sfd.FileName = filename;
                sfd.Title = "Save " + filename;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = {
                        textonly ? info.UsesPlayerName.ToString() : SecurityFuncs.Base64Encode(info.UsesPlayerName.ToString()),
                        textonly ? info.UsesID.ToString() : SecurityFuncs.Base64Encode(info.UsesID.ToString()),
                        textonly ? info.Warning.ToString() : SecurityFuncs.Base64Encode(info.Warning.ToString()),
                        textonly ? info.LegacyMode.ToString() : SecurityFuncs.Base64Encode(info.LegacyMode.ToString()),
                        textonly ? info.ClientMD5.ToString() : SecurityFuncs.Base64Encode(info.ClientMD5.ToString()),
                        textonly ? info.ScriptMD5.ToString() : SecurityFuncs.Base64Encode(info.ScriptMD5.ToString()),
                        textonly ? info.Description.ToString()  : SecurityFuncs.Base64Encode(info.Description.ToString()),
                        textonly ? islocked.ToString() : SecurityFuncs.Base64Encode(islocked.ToString()),
                        textonly ? info.Fix2007.ToString() : SecurityFuncs.Base64Encode(info.Fix2007.ToString()),
                        textonly ? info.AlreadyHasSecurity.ToString() : SecurityFuncs.Base64Encode(info.AlreadyHasSecurity.ToString()),
                        textonly ? info.NoGraphicsOptions.ToString() : SecurityFuncs.Base64Encode(info.NoGraphicsOptions.ToString()),
                        textonly ? info.CommandLineArgs.ToString() : SecurityFuncs.Base64Encode(info.CommandLineArgs.ToString())
                    };
                    File.WriteAllText(sfd.FileName, SecurityFuncs.Base64Encode(string.Join("|", lines)));
                    path = Path.GetDirectoryName(sfd.FileName);
                }
            }

            return path;
        }

        public static string LoadClientinfoAndGetPath(FileFormat.ClientInfo info, bool islocked, string veroutput, bool islockedoutput)
        {
            string path = "";
            bool IsVersion2 = false;

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Novetus Clientinfo files (*.nov)|*.nov";
                ofd.FilterIndex = 1;
                ofd.FileName = "clientinfo.nov";
                ofd.Title = "Load clientinfo.nov";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string file, usesplayername, usesid, warning, legacymode, clientmd5,
                        scriptmd5, desc, locked, fix2007, alreadyhassecurity,
                        cmdargsornogfxoptions, commandargsver2;

                    using (StreamReader reader = new StreamReader(ofd.FileName))
                    {
                        file = reader.ReadLine();
                    }

                    string ConvertedLine = "";

                    try
                    {
                        IsVersion2 = true;
                        veroutput = "v2";
                        ConvertedLine = SecurityFuncs.Base64DecodeNew(file);
                    }
                    catch (Exception)
                    {
                        veroutput = "v1";
                        ConvertedLine = SecurityFuncs.Base64DecodeOld(file);
                    }

                    string[] result = ConvertedLine.Split('|');
                    usesplayername = SecurityFuncs.Base64Decode(result[0]);
                    usesid = SecurityFuncs.Base64Decode(result[1]);
                    warning = SecurityFuncs.Base64Decode(result[2]);
                    legacymode = SecurityFuncs.Base64Decode(result[3]);
                    clientmd5 = SecurityFuncs.Base64Decode(result[4]);
                    scriptmd5 = SecurityFuncs.Base64Decode(result[5]);
                    desc = SecurityFuncs.Base64Decode(result[6]);
                    locked = SecurityFuncs.Base64Decode(result[7]);
                    fix2007 = SecurityFuncs.Base64Decode(result[8]);
                    alreadyhassecurity = SecurityFuncs.Base64Decode(result[9]);
                    cmdargsornogfxoptions = SecurityFuncs.Base64Decode(result[10]);
                    commandargsver2 = "";
                    try
                    {
                        if (IsVersion2)
                        {
                            commandargsver2 = SecurityFuncs.Base64Decode(result[11]);
                        }
                    }
                    catch (Exception)
                    {
                        veroutput = "v2 (DEV)";
                        IsVersion2 = false;
                    }

                    if (!GlobalVars.AdminMode)
                    {
                        bool lockcheck = Convert.ToBoolean(locked);
                        if (lockcheck)
                        {
                            MessageBox.Show("This client is locked and therefore it cannot be loaded.", "Novetus Launcher - Error when loading client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return "";
                        }
                        else
                        {
                            islocked = lockcheck;
                            islockedoutput = islocked;
                        }
                    }
                    else
                    {
                        islocked = Convert.ToBoolean(locked);
                        islockedoutput = islocked;
                    }

                    info.UsesPlayerName = Convert.ToBoolean(usesplayername);
                    info.UsesID = Convert.ToBoolean(usesid);
                    info.Warning = warning;
                    info.LegacyMode = Convert.ToBoolean(legacymode);
                    info.ClientMD5 = clientmd5;
                    info.ScriptMD5 = scriptmd5;
                    info.Description = desc;
                    info.Fix2007 = Convert.ToBoolean(fix2007);
                    info.AlreadyHasSecurity = Convert.ToBoolean(alreadyhassecurity);

                    if (IsVersion2)
                    {
                        info.NoGraphicsOptions = Convert.ToBoolean(cmdargsornogfxoptions);
                        info.CommandLineArgs = commandargsver2;
                    }
                    else
                    {
                        //Again, fake it.
                        info.NoGraphicsOptions = false;
                        info.CommandLineArgs = cmdargsornogfxoptions;
                    }
                }
            }

            return path;
        }

        public static void NewClientinfo(FileFormat.ClientInfo info, bool islocked)
        {
            info.UsesPlayerName = false;
            info.UsesID = false;
            info.Warning = "";
            info.LegacyMode = false;
            info.Fix2007 = false;
            info.AlreadyHasSecurity = false;
            info.Description = "";
            info.ClientMD5 = "";
            info.ScriptMD5 = "";
            info.CommandLineArgs = "";
            islocked = false;
        }
        #endregion

        #region Diogenes Editor
        // credit to Carrot for this :D

        public static string DiogenesCrypt(string word)
        {
            StringBuilder result = new StringBuilder("");
            byte[] bytes = Encoding.ASCII.GetBytes(word);

            foreach (byte singular in bytes)
            {
                result.Append(Convert.ToChar(0x55 ^ singular));
            }

            return result.ToString();
        }

        public static void LoadDiogenes(string veroutput, string textoutput)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "ROBLOX Diogenes filter (diogenes.fnt)|diogenes.fnt";
                ofd.FilterIndex = 1;
                ofd.FileName = "diogenes.fnt";
                ofd.Title = "Load diogenes.fnt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder builder = new StringBuilder();

                    using (StreamReader reader = new StreamReader(ofd.FileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();

                            try
                            {
                                line = DiogenesCrypt(line);
                                veroutput = "v2";
                            }
                            catch (Exception)
                            {
                                veroutput = "v1";
                                continue;
                            }

                            builder.Append(line + Environment.NewLine);
                        }
                    }

                    textoutput = builder.ToString();
                }
            }
        }

        public static void SaveDiogenes(string veroutput, string[] lineinput, bool textonly = false)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "ROBLOX Diogenes filter v2 (diogenes.fnt)|diogenes.fnt|ROBLOX Diogenes filter v1 (diogenes.fnt)|diogenes.fnt";
                sfd.FilterIndex = 1;
                sfd.FileName = "diogenes.fnt";
                sfd.Title = "Save diogenes.fnt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!textonly)
                    {
                        StringBuilder builder = new StringBuilder();

                        foreach (string s in lineinput)
                        {
                            if (sfd.FilterIndex == 1)
                            {
                                builder.Append(DiogenesCrypt(s) + Environment.NewLine);
                                veroutput = "v2";
                            }
                            else
                            {
                                builder.Append(s + Environment.NewLine);
                                veroutput = "v1";
                            }
                        }

                        using (StreamWriter sw = File.CreateText(sfd.FileName))
                        {
                            sw.Write(builder.ToString());
                        }
                    }
                    else
                    {
                        File.WriteAllLines(sfd.FileName, lineinput);
                    }
                }
            }
        }
        #endregion

        #region Item SDK

        public static void StartItemDownload(string name, string url, string id, int ver, bool iswebsite)
        {
            try
            {
                string version = ((ver != 0) && (!iswebsite)) ? "&version=" + ver : "";
                string fullURL = url + id + version;

                if (!iswebsite)
                {
                    if (!GlobalVars.UserConfiguration.DisabledItemMakerHelp)
                    {
                        string helptext = "If you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                        MessageBox.Show(helptext, "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Downloader download = new Downloader(fullURL, name, "Roblox Model (*.rbxm)|*.rbxm|Roblox Mesh (*.mesh)|*.mesh|PNG Image (*.png)|*.png|WAV Sound (*.wav)|*.wav");

                    try
                    {
                        string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.";
                        download.InitDownload((!GlobalVars.UserConfiguration.DisabledItemMakerHelp) ? helptext : "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Unable to download the file. " + ex.Message, "Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (!string.IsNullOrWhiteSpace(download.getDownloadOutcome()))
                    {
                        MessageBox.Show(download.getDownloadOutcome(), "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    System.Diagnostics.Process.Start(fullURL);

                    if (!GlobalVars.UserConfiguration.DisabledItemMakerHelp)
                    {
                        string helptext = "In order for the item to work in Novetus, you'll need to find an icon for your item (it must be a .png file), then name it the same name as your item.\n\nIf you want to create a local (offline) item, you'll have to download the meshes/textures from the links in the rbxm file, then replace the links in the file pointing to where they are using rbxasset://. Look at the directory in the 'shareddata/charcustom' folder that best suits your item type, then look at the rbxm for any one of the items. If you get a corrupted file, change the URL using the drop down box.\n\nIf you're trying to create a offline item, please use these file extension names when saving your files:\n.rbxm - ROBLOX Model/Item\n.mesh - ROBLOX Mesh\n.png - Texture/Icon\n.wav - Sound";
                        MessageBox.Show(helptext, "Novetus Item SDK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Unable to download the file. Try using a different file name or ID.", "Novetus Item SDK | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SelectItemDownloadURL(int index, string url, bool iswebsite)
        {
            switch (index)
			{
				case 1:
					url = "http://assetgame.roblox.com/asset/?id=";
                    iswebsite = false;
					break;
				case 2:
					url = "https://www.roblox.com/catalog/";
                    iswebsite = true;
					break;
				case 3:
					url = "https://www.roblox.com/library/";
                    iswebsite = true;
					break;
				default:
					url = "http://www.roblox.com/asset?id=";
                    iswebsite = false;
					break;
			}
        }
        #endregion

        #region SDK Launcher
        public static void LaunchSDKAppByIndex(int index)
        {
            SDKApps selectedApp = GetSDKAppForIndex(index);

            switch (selectedApp)
            {
                case SDKApps.ClientSDK:
                    ClientinfoEditor cie = new ClientinfoEditor();
                    cie.Show();
                    break;
                case SDKApps.ClientScriptDoc:
                    ClientScriptDocumentation csd = new ClientScriptDocumentation();
                    csd.Show();
                    break;
                case SDKApps.AssetLocalizer:
                    AssetLocalizer al = new AssetLocalizer();
                    al.Show();
                    break;
                case SDKApps.SplashTester:
                    SplashTester st = new SplashTester();
                    st.Show();
                    break;
                case SDKApps.Obj2MeshV1GUI:
                    Obj2MeshV1GUI obj = new Obj2MeshV1GUI();
                    obj.Show();
                    break;
                case SDKApps.ScriptGenerator:
                    Process proc = new Process();
                    proc.StartInfo.FileName = GlobalPaths.ConfigDirData + "\\RSG.exe";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    break;
                case SDKApps.LegacyPlaceConverter:
                    Process proc2 = new Process();
                    proc2.StartInfo.FileName = GlobalPaths.ConfigDirData + "\\Roblox_Legacy_Place_Converter.exe";
                    proc2.StartInfo.CreateNoWindow = false;
                    proc2.StartInfo.UseShellExecute = false;
                    proc2.Start();
                    break;
                case SDKApps.DiogenesEditor:
                    DiogenesEditor dio = new DiogenesEditor();
                    dio.Show();
                    break;
                default:
                    ItemMaker im = new ItemMaker();
                    im.Show();
                    break;
            }
        }

        public static SDKApps GetSDKAppForIndex(int index)
        {
            switch (index)
            {
                case 1:
                    return SDKApps.ClientSDK;
                case 2:
                    return SDKApps.ClientScriptDoc;
                case 3:
                    return SDKApps.AssetLocalizer;
                case 4:
                    return SDKApps.SplashTester;
                case 5:
                    return SDKApps.Obj2MeshV1GUI;
                case 6:
                    return SDKApps.ScriptGenerator;
                case 7:
                    return SDKApps.LegacyPlaceConverter;
                case 8:
                    return SDKApps.DiogenesEditor;
                default:
                    return SDKApps.ItemSDK;
            }
        }
        #endregion
    }
    #endregion
}
