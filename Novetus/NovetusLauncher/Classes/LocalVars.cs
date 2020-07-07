#region Usings
using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
#endregion

namespace NovetusLauncher
{
    #region LocalVars
    class LocalVars
    {
        public static int Clicks = 0;
        public static string prevsplash = "";
        public static int DefaultRobloxPort = 53640;
        public static bool LocalPlayMode = false;
    }
    #endregion

    #region Downloader
    class Downloader
    {
        private readonly string fileURL;
        private readonly string fileName;
        private readonly string fileFilter;
        private string downloadOutcome;
        private static string downloadOutcomeException;

        public Downloader(string url, string name, string filter)
        {
            fileName = name;
            fileURL = url;
            fileFilter = filter;
        }

        public Downloader(string url, string name)
        {
            fileName = name;
            fileURL = url;
            fileFilter = "";
        }

        public void setDownloadOutcome(string text)
        {
            downloadOutcome = text;
        }

        public string getDownloadOutcome()
        {
            return downloadOutcome;
        }

        public void InitDownload(string path, string fileext, string additionalText = "")
        {
            string downloadOutcomeAddText = additionalText;

            string outputfilename = fileName + fileext;
            string fullpath = path + "\\" + outputfilename;

            try
            {
                int read = DownloadFile(fileURL, fullpath);
                downloadOutcome = "File " + outputfilename + " downloaded! " + read + " bytes written! " + downloadOutcomeAddText + downloadOutcomeException;
            }
            catch (Exception ex)
            {
                downloadOutcome = "Error when downloading file: " + ex.Message;
            }
        }

        public void InitDownload(string additionalText = "")
        {
            string downloadOutcomeAddText = additionalText;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                FileName = fileName,
                //"Compressed zip files (*.zip)|*.zip|All files (*.*)|*.*"
                Filter = fileFilter,
                Title = "Save " + fileName
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int read = DownloadFile(fileURL, saveFileDialog1.FileName);
                    downloadOutcome = "File " + Path.GetFileName(saveFileDialog1.FileName) + " downloaded! " + read + " bytes written! " + downloadOutcomeAddText + downloadOutcomeException;
                }
                catch (Exception ex)
                {
                    downloadOutcome = "Error when downloading file: " + ex.Message;
                }
            }
        }

        private static int DownloadFile(string remoteFilename, string localFilename)
        {
            //credit to Tom Archer (https://www.codeguru.com/columns/dotnettips/article.php/c7005/Downloading-Files-with-the-WebRequest-and-WebResponse-Classes.htm)
            //and Brokenglass (https://stackoverflow.com/questions/4567313/uncompressing-gzip-response-from-webclient/4567408#4567408)

            // Function will return the number of bytes processed
            // to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

            // Assign values to these objects here so that they can
            // be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            //thanks to https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0 for the net 4.0 compatible TLS 1.1/1.2 code!
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | (SecurityProtocolType)3072
                    | (SecurityProtocolType)768
                    | SecurityProtocolType.Ssl3;
                // Create a request for the specified remote file name
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(remoteFilename);
                request.UserAgent = "Roblox/WinINet";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object 
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        // Create the local file
                        localStream = File.Create(localFilename);

                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                downloadOutcomeException = " Exception detected: " + e.Message;
            }
            finally
            {
                // Close the response and streams objects here 
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            // Return total bytes processed to caller.
            return bytesProcessed;
        }
    }
    #endregion

    #region Addon Loader
    public class AddonLoader
    {
        private readonly OpenFileDialog openFileDialog1;
        private string installOutcome = "";
        private int fileListDisplay = 0;

        public AddonLoader()
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select an addon .zip file",
                Filter = "Compressed zip files (*.zip)|*.zip",
                Title = "Open addon .zip"
            };
        }

        public void setInstallOutcome(string text)
        {
            installOutcome = text;
        }

        public string getInstallOutcome()
        {
            return installOutcome;
        }

        public void setFileListDisplay(int number)
        {
            fileListDisplay = number;
        }

        public void LoadAddon()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int filecount = 0;
                    StringBuilder filelistbuilder = new StringBuilder();

                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        using (var zipFile = ZipFile.Read(str))
                        {
                            ZipEntry[] entries = zipFile.Entries.ToArray();

                            foreach (ZipEntry entry in entries)
                            {
                                filelistbuilder.Append(entry.FileName + " (" + entry.UncompressedSize + ")");
                                filelistbuilder.Append(Environment.NewLine);
                            }

                            zipFile.ExtractAll(Directories.BasePath, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }

                    string filelist = filelistbuilder.ToString();

                    if (filecount > fileListDisplay)
                    {
                        installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist + Environment.NewLine + "and " + (filecount - fileListDisplay) + " more files!";
                    }
                    else
                    {
                        installOutcome = "Addon " + openFileDialog1.SafeFileName + " installed! " + filecount + " files copied!" + Environment.NewLine + "Files added/modified:" + Environment.NewLine + Environment.NewLine + filelist;
                    }
                }
                catch (Exception ex)
                {
                    installOutcome = "Error when installing addon: " + ex.Message;
                }
            }
        }
    }
    #endregion

    #region Icon Loader
    public class IconLoader
    {
        private OpenFileDialog openFileDialog1;
        private string installOutcome = "";

        public IconLoader()
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select an icon .png file",
                Filter = "Portable Network Graphics image (*.png)|*.png",
                Title = "Open icon .png"
            };
        }

        public void setInstallOutcome(string text)
        {
            installOutcome = text;
        }

        public string getInstallOutcome()
        {
            return installOutcome;
        }

        public void LoadImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        using (Stream output = new FileStream(Directories.extradir + "\\icons\\" + GlobalVars.UserConfiguration.PlayerName + ".png", FileMode.Create))
                        {
                            byte[] buffer = new byte[32 * 1024];
                            int read;

                            while ((read = str.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, read);
                            }
                        }

                        str.Close();
                    }

                    installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
                }
                catch (Exception ex)
                {
                    installOutcome = "Error when installing icon: " + ex.Message;
                }
            }
        }
    }
    #endregion

    #region Roblox Type Definitions
    public class RobloxDefs
    {
        public static AssetCacheDef Fonts 
        { 
            get 
            { 
                return new AssetCacheDef("SpecialMesh", 
                new string[] { "MeshId", "TextureId" }, 
                new string[] { ".mesh", ".png" }, 
                new string[] { Directories.AssetCacheDirFonts, Directories.AssetCacheDirTextures }, 
                new string[] { Directories.AssetCacheFontsGameDir, Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Sky 
        { 
            get 
            { 
                return new AssetCacheDef("Sky", 
                    new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirSky }, 
                    new string[] { Directories.AssetCacheSkyGameDir }); 
            } 
        }

        public static AssetCacheDef Decal 
        { 
            get 
            { 
                return new AssetCacheDef("Decal", 
                    new string[] { "Texture" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures }, 
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Texture 
        { 
            get 
            { 
                return new AssetCacheDef("Texture", 
                    new string[] { "Texture" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures },
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef HopperBin 
        { 
            get 
            { return new AssetCacheDef("HopperBin", 
                new string[] { "TextureId" }, 
                new string[] { ".png" }, 
                new string[] { Directories.AssetCacheDirTextures }, 
                new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Tool 
        { 
            get 
            { 
                return new AssetCacheDef("Tool", 
                    new string[] { "TextureId" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures }, 
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Sound 
        { 
            get 
            { 
                return new AssetCacheDef("Sound", 
                    new string[] { "SoundId" }, 
                    new string[] { ".wav" }, 
                    new string[] { Directories.AssetCacheDirSounds }, 
                    new string[] { Directories.AssetCacheSoundsGameDir }); 
            } 
        }

        public static AssetCacheDef ImageLabel 
        { 
            get 
            { 
                return new AssetCacheDef("ImageLabel", 
                    new string[] { "Image" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures }, 
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Shirt 
        { 
            get 
            { 
                return new AssetCacheDef("Shirt", 
                    new string[] { "ShirtTemplate" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures }, 
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef ShirtGraphic 
        { 
            get 
            { 
                return new AssetCacheDef("ShirtGraphic", 
                    new string[] { "Graphic" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures }, 
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Pants 
        { 
            get 
            { 
                return new AssetCacheDef("Pants", 
                    new string[] { "PantsTemplate" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.AssetCacheDirTextures }, 
                    new string[] { Directories.AssetCacheTexturesGameDir }); 
            } 
        }

        public static AssetCacheDef Script 
        { 
            get 
            { 
                return new AssetCacheDef("Script", 
                    new string[] { "LinkedSource" }, 
                    new string[] { ".lua" }, 
                    new string[] { Directories.AssetCacheDirScripts }, 
                    new string[] { Directories.AssetCacheScriptsGameDir }); 
            } 
        }

        public static AssetCacheDef LocalScript 
        { 
            get 
            { 
                return new AssetCacheDef("LocalScript", 
                    new string[] { "LinkedSource" }, 
                    new string[] { ".lua" }, 
                    new string[] { Directories.AssetCacheDirScripts }, 
                    new string[] { Directories.AssetCacheScriptsGameDir }); 
            } 
        }

        //item defs below
        public static AssetCacheDef ItemHatFonts 
        { 
            get 
            { 
                return new AssetCacheDef("SpecialMesh", 
                    new string[] { "MeshId", "TextureId" }, 
                    new string[] { ".mesh", ".png" }, 
                    new string[] { Directories.hatdirFonts, Directories.hatdirTextures }, 
                    new string[] { Directories.hatGameDirFonts, Directories.hatGameDirTextures }); 
            } 
        }

        public static AssetCacheDef ItemHatSound 
        { 
            get 
            { 
                return new AssetCacheDef("Sound", 
                    new string[] { "SoundId" }, 
                    new string[] { ".wav" }, 
                    new string[] { Directories.hatdirSounds }, 
                    new string[] { Directories.hatGameDirSounds }); 
            } 
        }

        public static AssetCacheDef ItemHatScript 
        { 
            get 
            { 
                return new AssetCacheDef("Script", 
                    new string[] { "LinkedSource" }, 
                    new string[] { ".lua" }, 
                    new string[] { Directories.hatdirScripts }, 
                    new string[] { Directories.hatGameDirScripts }); 
            } 
        }

        public static AssetCacheDef ItemHatLocalScript 
        { 
            get 
            { 
                return new AssetCacheDef("LocalScript", 
                    new string[] { "LinkedSource" }, 
                    new string[] { ".lua" }, 
                    new string[] { Directories.hatdirScripts }, 
                    new string[] { Directories.hatGameDirScripts }); 
            } 
        }

        public static AssetCacheDef ItemHeadFonts 
        { 
            get 
            { 
                return new AssetCacheDef("SpecialMesh", 
                    new string[] { "MeshId", "TextureId" }, 
                    new string[] { ".mesh", ".png" }, 
                    new string[] { Directories.headdirFonts, Directories.headdirTextures }, 
                    new string[] { Directories.headGameDirFonts, Directories.headGameDirTextures }); 
            } 
        }

        public static AssetCacheDef ItemFaceTexture 
        { 
            get 
            { 
                return new AssetCacheDef("Decal", 
                    new string[] { "Texture" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.facedirTextures }, 
                    new string[] { Directories.faceGameDirTextures }); 
            } 
        }

        public static AssetCacheDef ItemShirtTexture 
        { 
            get 
            { 
                return new AssetCacheDef("Shirt", 
                    new string[] { "ShirtTemplate" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.shirtdirTextures }, 
                    new string[] { Directories.shirtGameDirTextures }); 
            } 
        }

        public static AssetCacheDef ItemTShirtTexture 
        { 
            get 
            { 
                return new AssetCacheDef("ShirtGraphic", 
                    new string[] { "Graphic" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.tshirtdirTextures }, 
                    new string[] { Directories.tshirtGameDirTextures }); 
            } 
        }

        public static AssetCacheDef ItemPantsTexture 
        { 
            get 
            { 
                return new AssetCacheDef("Pants", 
                    new string[] { "PantsTemplate" }, 
                    new string[] { ".png" }, 
                    new string[] { Directories.pantsdirTextures }, 
                    new string[] { Directories.pantsGameDirTextures }); 
            } 
        }
    }
    #endregion

    #region Roblox XML Localizer
    public static class RobloxXMLLocalizer
    {
        public static void DownloadFromNodes(string filepath, AssetCacheDef assetdef, string name = "", string meshname = "")
        {
            DownloadFromNodes(filepath, assetdef.Class, assetdef.Id[0], assetdef.Ext[0], assetdef.Dir[0], assetdef.GameDir[0], name, meshname);
        }

        public static void DownloadFromNodes(string filepath, AssetCacheDef assetdef, int idIndex, int extIndex, int outputPathIndex, int inGameDirIndex, string name = "", string meshname = "")
        {
            DownloadFromNodes(filepath, assetdef.Class, assetdef.Id[idIndex], assetdef.Ext[extIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex], name, meshname);
        }

        public static void DownloadFromNodes(string filepath, string itemClassValue, string itemIdValue, string fileext, string outputPath, string inGameDir, string name = "", string meshname = "")
        {
            string oldfile = File.ReadAllText(filepath);
            string fixedfile = RemoveInvalidXmlChars(ReplaceHexadecimalSymbols(oldfile));
            XDocument doc = XDocument.Parse(fixedfile);

            try
            {
                var v = from nodes in doc.Descendants("Item")
                        where nodes.Attribute("class").Value == itemClassValue
                        select nodes;

                foreach (var item in v)
                {
                    var v2 = from nodes in item.Descendants("Content")
                             where nodes.Attribute("name").Value == itemIdValue
                             select nodes;

                    foreach (var item2 in v2)
                    {
                        var v3 = from nodes in item2.Descendants("url")
                                 select nodes;

                        foreach (var item3 in v3)
                        {
                            if (!item3.Value.Contains("rbxassetid"))
                            {
                                if (!item3.Value.Contains("rbxasset"))
                                {
                                    if (string.IsNullOrWhiteSpace(meshname))
                                    {
                                        string url = item3.Value;
                                        string urlFixed = url.Replace("&amp;", "&").Replace("amp;", "&");
                                        string peram = "id=";

                                        if (string.IsNullOrWhiteSpace(name))
                                        {
                                            if (urlFixed.Contains(peram))
                                            {
                                                string IDVal = urlFixed.After(peram);
                                                DownloadFilesFromNode(urlFixed, outputPath, fileext, IDVal);
                                                item3.Value = inGameDir + IDVal + fileext;
                                            }
                                        }
                                        else
                                        {
                                            DownloadFilesFromNode(urlFixed, outputPath, fileext, name);
                                            item3.Value = inGameDir + name + fileext;
                                        }
                                    }
                                    else
                                    {
                                        item3.Value = inGameDir + meshname;
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(meshname))
                                {
                                    string url = item3.Value;
                                    string rbxassetid = "rbxassetid://";
                                    string urlFixed = "https://www.roblox.com/asset/?id=" + url.After(rbxassetid);
                                    string peram = "id=";

                                    if (string.IsNullOrWhiteSpace(name))
                                    {
                                        if (urlFixed.Contains(peram))
                                        {
                                            string IDVal = urlFixed.After(peram);
                                            DownloadFilesFromNode(urlFixed, outputPath, fileext, IDVal);
                                            item3.Value = inGameDir + IDVal + fileext;
                                        }
                                    }
                                    else
                                    {
                                        DownloadFilesFromNode(urlFixed, outputPath, fileext, name);
                                        item3.Value = inGameDir + name + fileext;
                                    }
                                }
                                else
                                {
                                    item3.Value = inGameDir + meshname;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                doc.Save(filepath);
            }
        }

        private static void DownloadFilesFromNode(string url, string path, string fileext, string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                Downloader download = new Downloader(url, id);

                try
                {
                    download.InitDownload(path, fileext);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset Localizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static string RemoveInvalidXmlChars(string content)
        {
            return new string(content.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());
        }

        private static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }
    }
    #endregion

    #region Splash Reader
    public static class SplashReader
    {
        private static string RandomSplash()
        {
            string[] splashes = File.ReadAllLines(Directories.ConfigDir + "\\splashes.txt");
            string splash = "";

            try
            {
                splash = splashes[new CryptoRandom().Next(0, splashes.Length - 1)];
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

            CryptoRandom random = new CryptoRandom();

            string formattedsplash = splash
                .Replace("%name%", GlobalVars.UserConfiguration.PlayerName)
                .Replace("%nextversion%", (Convert.ToDouble(GlobalVars.ProgramInformation.Branch) + 0.1).ToString())
                .Replace("%randomtext%", SecurityFuncs.RandomString(random.Next(2, 32)));

            return formattedsplash;
        }

        public static string GetSplash()
        {
            DateTime today = DateTime.Now;
            string splash = "";

            switch (today)
            {
                case DateTime christmaseve when christmaseve.Month.Equals(12) && christmaseve.Day.Equals(24):
                case DateTime christmasday when christmasday.Month.Equals(12) && christmasday.Day.Equals(25):
                    splash = "Merry Christmas!";
                    break;
                case DateTime newyearseve when newyearseve.Month.Equals(12) && newyearseve.Day.Equals(31):
                case DateTime newyearsday when newyearsday.Month.Equals(1) && newyearsday.Day.Equals(1):
                    splash = "Happy New Year!";
                    break;
                case DateTime halloween when halloween.Month.Equals(10) && halloween.Day.Equals(31):
                    splash = "Happy Halloween!";
                    break;
                case DateTime bitlbirthday when bitlbirthday.Month.Equals(6) && bitlbirthday.Day.Equals(10):
                    splash = "Happy Birthday, Bitl!";
                    break;
                case DateTime robloxbirthday when robloxbirthday.Month.Equals(8) && robloxbirthday.Day.Equals(27):
                    splash = "Happy Birthday, ROBLOX!";
                    break;
                case DateTime novetusbirthday when novetusbirthday.Month.Equals(10) && novetusbirthday.Day.Equals(27):
                    splash = "Happy Birthday, Novetus!";
                    break;
                case DateTime leiferikson when leiferikson.Month.Equals(10) && leiferikson.Day.Equals(9):
                    splash = "Happy Leif Erikson Day! HINGA DINGA DURGEN!";
                    break;
                case DateTime smokeweedeveryday when smokeweedeveryday.Month.Equals(4) && smokeweedeveryday.Day.Equals(20):
                    CryptoRandom random = new CryptoRandom();
                    if (random.Next(0, 1) == 1)
                    {
                        splash = "smoke weed every day";
                    }
                    else
                    {
                        splash = "4/20 lol";
                    }
                    break;
                case DateTime erikismyhero when erikismyhero.Month.Equals(2) && erikismyhero.Day.Equals(11):
                    splash = "RIP Erik Cassel";
                    break;
                default:
                    splash = RandomSplash();
                    break;
            }

            return splash;
        }
    }
    #endregion
}
