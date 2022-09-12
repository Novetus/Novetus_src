#region Usings
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Windows.Forms;
#endregion

#region Novetus Functions
public class NovetusFuncs
{
    public static string CopyMapToRBXAsset()
    {
        string clientcontentpath = GlobalPaths.ClientDir + @"\\" + GlobalVars.UserConfiguration.SelectedClient + @"\\content\\temp.rbxl";
        Util.FixedFileCopy(GlobalVars.UserConfiguration.MapPath, clientcontentpath, true);
        return GlobalPaths.AltBaseGameDir + "temp.rbxl";
    }

    public static string GetItemTextureLocalPath(string item, string nameprefix)
    {
        //don't bother, we're offline.
        if (GlobalVars.ExternalIP.Equals("localhost"))
            return "";

        if (!GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%localizeonlineclothing%"))
            return "";

        if (item.Contains("http://") || item.Contains("https://"))
        {
            string peram = "id=";
            string fullname = nameprefix + "Temp.png";

            if (item.Contains(peram))
            {
                string id = item.After(peram);
                fullname = id + ".png";
            }
            else
            {
                return item;
            }

            Downloader download = new Downloader(item, fullname, "", GlobalPaths.AssetCacheDirTextures);

            try
            {
                string path = download.GetFullDLPath();
                download.InitDownloadNoDialog(path);
                return GlobalPaths.AssetCacheTexturesGameDir + download.fileName;
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
            }
        }

        return "";
    }

    public static string GetItemTextureID(string item, string name, AssetCacheDefBasic assetCacheDef)
    {
        //don't bother, we're offline.
        if (GlobalVars.ExternalIP.Equals("localhost"))
            return "";

        if (!GlobalVars.SelectedClientInfo.CommandLineArgs.Contains("%localizeonlineclothing%"))
            return "";

        if (item.Contains("http://") || item.Contains("https://"))
        {
            string peram = "id=";
            if (!item.Contains(peram))
            {
                return item;
            }

            Downloader download = new Downloader(item, name + "Temp.rbxm", "", GlobalPaths.AssetCacheDirFonts);

            try
            {
                string path = download.GetFullDLPath();
                download.InitDownloadNoDialog(path);
                string oldfile = File.ReadAllText(path);
                string fixedfile = RobloxXML.RemoveInvalidXmlChars(RobloxXML.ReplaceHexadecimalSymbols(oldfile)).Replace("&#9;", "\t").Replace("#9;", "\t");
                XDocument doc = null;
                XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };
                Stream filestream = Util.GenerateStreamFromString(fixedfile);
                using (XmlReader xmlReader = XmlReader.Create(filestream, xmlReaderSettings))
                {
                    xmlReader.MoveToContent();
                    doc = XDocument.Load(xmlReader);
                }

                return RobloxXML.GetURLInNodes(doc, assetCacheDef.Class, assetCacheDef.Id[0], item);
            }
#if URI || LAUNCHER || CMD || BASICLAUNCHER
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
#else
		    catch (Exception)
		    {
#endif
            }
        }

        return "";
    }

    public static void GeneratePlayerID()
	{
		int randomID = SecurityFuncs.GenerateRandomNumber();
        //2147483647 is max id.
        GlobalVars.UserConfiguration.UserID = randomID;
	}

    public static string GenerateAndReturnTripcode()
    {
        //Powered by https://github.com/davcs86/csharp-uhwid
        return UHWID.UHWIDEngine.AdvancedUid;
    }

#if LAUNCHER
    public static void PingMasterServer(bool online, string reason, RichTextBox box)
#else
    public static void PingMasterServer(bool online, string reason)
#endif
    {
        if (online)
        {
            GlobalVars.ServerID = SecurityFuncs.RandomString(30) + SecurityFuncs.GenerateRandomNumber();
            GlobalVars.PingURL = "http://" + GlobalVars.UserConfiguration.ServerBrowserServerAddress +
            "/list.php?name=" + GlobalVars.UserConfiguration.ServerBrowserServerName +
            "&ip=" + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP) +
            "&port=" + GlobalVars.UserConfiguration.RobloxPort +
            "&client=" + GlobalVars.UserConfiguration.SelectedClient +
            "&version=" + GlobalVars.ProgramInformation.Version +
            "&id=" + GlobalVars.ServerID;
        }
        else
        {
            GlobalVars.PingURL = "http://" + GlobalVars.UserConfiguration.ServerBrowserServerAddress +
            "/delist.php?id=" + GlobalVars.ServerID;
            GlobalVars.ServerID = "N/A";
        }

#if LAUNCHER
        Util.ConsolePrint("Pinging master server. " + reason, 4, box);
#elif CMD
        Util.ConsolePrint("Pinging master server. " + reason, 4);
#endif

#if LAUNCHER
        Task.Factory.StartNew(() => TryPing(box));
#else
        Task.Factory.StartNew(() => TryPing());
#endif
    }

#if LAUNCHER
    public static void TryPing(RichTextBox box)
#else
    private static void TryPing()
#endif
    {
        string response = Util.HttpGet(GlobalVars.PingURL);

        if (!string.IsNullOrWhiteSpace(response))
        {
#if LAUNCHER
            Util.ConsolePrint(response, response.Contains("ERROR:") ? 2 : 4, box);
#elif CMD
            Util.ConsolePrint(response, response.Contains("ERROR:") ? 2 : 4);
#endif

            if (response.Contains("ERROR:"))
            {
                GlobalVars.ServerID = "N/A";
            }
        }

        if (!GlobalVars.ServerID.Equals("N/A"))
        {
#if LAUNCHER
            Util.ConsolePrint("Your server's ID is " + GlobalVars.ServerID, 4, box);
#elif CMD
            Util.ConsolePrint("Your server's ID is " + GlobalVars.ServerID, 4);
#endif
        }

        GlobalVars.PingURL = "";
    }

#if CMD
    public static void CreateTXT()
    {
        if (GlobalVars.RequestToOutputInfo)
        {
            string[] lines1 = {
                        SecurityFuncs.Base64Encode(!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
            string URI = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines1), true);
            string[] lines2 = {
                        SecurityFuncs.Base64Encode("localhost"),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.RobloxPort.ToString()),
                        SecurityFuncs.Base64Encode(GlobalVars.UserConfiguration.SelectedClient)
                    };
            string URI2 = "novetus://" + SecurityFuncs.Base64Encode(string.Join("|", lines2), true);

            string[] text = {
                   "Process ID: " + (GlobalVars.ProcessID == 0 ? "N/A" : GlobalVars.ProcessID.ToString()),
                   "Don't copy the Process ID when sharing the server.",
                   "--------------------",
                   "Server Info:",
                   "Client: " + GlobalVars.UserConfiguration.SelectedClient,
                   "IP: " + (!string.IsNullOrWhiteSpace(GlobalVars.UserConfiguration.AlternateServerIP) ? GlobalVars.UserConfiguration.AlternateServerIP : GlobalVars.ExternalIP),
                   "Port: " + GlobalVars.UserConfiguration.RobloxPort.ToString(),
                   "Map: " + GlobalVars.UserConfiguration.Map,
                   "Players: " + GlobalVars.UserConfiguration.PlayerLimit,
                   "Version: Novetus " + GlobalVars.ProgramInformation.Version,
                   "Online URI Link:",
                   URI,
                   "Local URI Link:",
                   URI2
                   };

            string txt = GlobalPaths.BasePath + "\\" + GlobalVars.ServerInfoFileName;
            File.WriteAllLines(txt, text);
            Util.ConsolePrint("Server Information sent to file " + txt, 4);
        }
    }
#endif

#if LAUNCHER || URI
    public static void LaunchCharacterCustomization()
    {
        //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp
        FormCollection fc = Application.OpenForms;

        foreach (Form frm in fc)
        {
            //iterate through
            if (frm.Name == "CharacterCustomizationExtended" ||
                frm.Name == "CharacterCustomizationCompact")
            {
                frm.Close();
                break;
            }
        }

        switch (GlobalVars.UserConfiguration.LauncherStyle)
        {
            case Settings.Style.Extended:
                CharacterCustomizationExtended ccustom = new CharacterCustomizationExtended();
                ccustom.Show();
                break;
            case Settings.Style.Compact:
                CharacterCustomizationCompact ccustom2 = new CharacterCustomizationCompact();
                ccustom2.Show();
                break;
            case Settings.Style.Stylish:
            default:
                CharacterCustomizationExtended ccustom3 = new CharacterCustomizationExtended();
                ccustom3.Show();
                break;
        }
    }
#endif

    public static string FixURLString(string str, string str2)
    {
        string fixedStr = str.ToLower().Replace("?version=1&amp;id=", "?id=")
                    .Replace("?version=1&id=", "?id=")
                    .Replace("&amp;", "&")
                    .Replace("amp;", "&");

        string baseurl = fixedStr.Before("/asset/?id=");

        if (baseurl == "")
        {
            baseurl = fixedStr.Before("/asset?id=");
            if (baseurl == "")
            {
                baseurl = fixedStr.Before("/item.aspx?id=");
            }
        }

        string fixedUrl = fixedStr.Replace(baseurl + "/asset/?id=", str2)
                    .Replace(baseurl + "/asset?id=", str2)
                    .Replace(baseurl + "/item.aspx?id=", str2);

        //...because scripts mess it up.
        string id = fixedUrl.After("id=");
        string fixedID = Regex.Replace(id, "[^0-9]", "");

        //really fucking hacky.
        string finalUrl = fixedUrl.Before("id=") + "id=" + fixedID;

        return finalUrl;
    }
}
#endregion

#region Roblox Helpers
#region Vector3
public class Vector3
{
    public double X;
    public double Y;
    public double Z;

    public Vector3(double aX, double aY, double aZ)
    {
        X = aX;
        Y = aY;
        Z = aZ;
    }
}
#endregion

#region Roblox File Types
public enum RobloxFileType
{
    //RBXL and RBXM
    RBXL,
    RBXM,
    //Items
    Hat,
    Head,
    Face,
    TShirt,
    Shirt,
    Pants,
    Script,
    HeadNoCustomMesh
}
#endregion

#region Asset Cache Definition
public class AssetCacheDefBasic
{
    public AssetCacheDefBasic(string clas, string[] id)
    {
        Class = clas;
        Id = id;
    }

    public string Class { get; set; }
    public string[] Id { get; set; }
}

public class AssetCacheDef : AssetCacheDefBasic
{
    public AssetCacheDef(string clas, string[] id, string[] ext,
        string[] dir, string[] gamedir) : base(clas, id)
    {
        Ext = ext;
        Dir = dir;
        GameDir = gamedir;
    }

    public string[] Ext { get; set; }
    public string[] Dir { get; set; }
    public string[] GameDir { get; set; }
}
#endregion

#region Roblox Type Definitions
public struct RobloxDefs
{
    public static AssetCacheDef Fonts
    {
        get
        {
            return new AssetCacheDef("SpecialMesh",
            new string[] { "MeshId", "TextureId" },
            new string[] { ".mesh", ".png" },
            new string[] { GlobalPaths.AssetCacheDirFonts, GlobalPaths.AssetCacheDirTextures },
            new string[] { GlobalPaths.AssetCacheFontsGameDir, GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Sky
    {
        get
        {
            return new AssetCacheDef("Sky",
                new string[] { "SkyboxBk", "SkyboxDn", "SkyboxFt", "SkyboxLf", "SkyboxRt", "SkyboxUp" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirSky },
                new string[] { GlobalPaths.AssetCacheSkyGameDir });
        }
    }

    public static AssetCacheDef Decal
    {
        get
        {
            return new AssetCacheDef("Decal",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Texture
    {
        get
        {
            return new AssetCacheDef("Texture",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef HopperBin
    {
        get
        {
            return new AssetCacheDef("HopperBin",
              new string[] { "TextureId" },
              new string[] { ".png" },
              new string[] { GlobalPaths.AssetCacheDirTextures },
              new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Tool
    {
        get
        {
            return new AssetCacheDef("Tool",
                new string[] { "TextureId" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Sound
    {
        get
        {
            return new AssetCacheDef("Sound",
                new string[] { "SoundId" },
                new string[] { ".wav" },
                new string[] { GlobalPaths.AssetCacheDirSounds },
                new string[] { GlobalPaths.AssetCacheSoundsGameDir });
        }
    }

    public static AssetCacheDef ImageLabel
    {
        get
        {
            return new AssetCacheDef("ImageLabel",
                new string[] { "Image" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Shirt
    {
        get
        {
            return new AssetCacheDef("Shirt",
                new string[] { "ShirtTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef ShirtGraphic
    {
        get
        {
            return new AssetCacheDef("ShirtGraphic",
                new string[] { "Graphic" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Pants
    {
        get
        {
            return new AssetCacheDef("Pants",
                new string[] { "PantsTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.AssetCacheDirTextures },
                new string[] { GlobalPaths.AssetCacheTexturesGameDir });
        }
    }

    public static AssetCacheDef Script
    {
        get
        {
            return new AssetCacheDef("Script",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.AssetCacheDirScripts },
                new string[] { GlobalPaths.AssetCacheScriptsGameDir });
        }
    }

    public static AssetCacheDef LocalScript
    {
        get
        {
            return new AssetCacheDef("LocalScript",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.AssetCacheDirScripts },
                new string[] { GlobalPaths.AssetCacheScriptsGameDir });
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
                new string[] { GlobalPaths.hatdirFonts, GlobalPaths.hatdirTextures },
                new string[] { GlobalPaths.hatGameDirFonts, GlobalPaths.hatGameDirTextures });
        }
    }

    public static AssetCacheDef ItemHatSound
    {
        get
        {
            return new AssetCacheDef("Sound",
                new string[] { "SoundId" },
                new string[] { ".wav" },
                new string[] { GlobalPaths.hatdirSounds },
                new string[] { GlobalPaths.hatGameDirSounds });
        }
    }

    public static AssetCacheDef ItemHatScript
    {
        get
        {
            return new AssetCacheDef("Script",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.hatdirScripts },
                new string[] { GlobalPaths.hatGameDirScripts });
        }
    }

    public static AssetCacheDef ItemHatLocalScript
    {
        get
        {
            return new AssetCacheDef("LocalScript",
                new string[] { "LinkedSource" },
                new string[] { ".lua" },
                new string[] { GlobalPaths.hatdirScripts },
                new string[] { GlobalPaths.hatGameDirScripts });
        }
    }

    public static AssetCacheDef ItemHeadFonts
    {
        get
        {
            return new AssetCacheDef("SpecialMesh",
                new string[] { "MeshId", "TextureId" },
                new string[] { ".mesh", ".png" },
                new string[] { GlobalPaths.headdirFonts, GlobalPaths.headdirTextures },
                new string[] { GlobalPaths.headGameDirFonts, GlobalPaths.headGameDirTextures });
        }
    }

    public static AssetCacheDef ItemFaceTexture
    {
        get
        {
            return new AssetCacheDef("Decal",
                new string[] { "Texture" },
                new string[] { ".png" },
                new string[] { GlobalPaths.facedirTextures },
                new string[] { GlobalPaths.faceGameDirTextures });
        }
    }

    public static AssetCacheDef ItemShirtTexture
    {
        get
        {
            return new AssetCacheDef("Shirt",
                new string[] { "ShirtTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.shirtdirTextures },
                new string[] { GlobalPaths.shirtGameDirTextures });
        }
    }

    public static AssetCacheDef ItemTShirtTexture
    {
        get
        {
            return new AssetCacheDef("ShirtGraphic",
                new string[] { "Graphic" },
                new string[] { ".png" },
                new string[] { GlobalPaths.tshirtdirTextures },
                new string[] { GlobalPaths.tshirtGameDirTextures });
        }
    }

    public static AssetCacheDef ItemPantsTexture
    {
        get
        {
            return new AssetCacheDef("Pants",
                new string[] { "PantsTemplate" },
                new string[] { ".png" },
                new string[] { GlobalPaths.pantsdirTextures },
                new string[] { GlobalPaths.pantsGameDirTextures });
        }
    }
}
#endregion

#region XML Types
public enum XMLTypes
{
    Token,
    Bool,
    Float,
    String,
    Vector2Int16,
    Int
}
#endregion

#region Roblox XML Parser
public static class RobloxXML
{
    public static void EditRenderSettings(XDocument doc, string setting, string value, XMLTypes type)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "RenderSettings"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants((type != XMLTypes.Vector2Int16 ? type.ToString().ToLower() : "Vector2int16"))
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                if (type != XMLTypes.Vector2Int16)
                {
                    item2.Value = value;
                }
                else
                {
                    string[] vals = value.Split('x');

                    var v3 = from nodes in item2.Descendants("X")
                             select nodes;

                    foreach (var item3 in v3)
                    {
                        item3.Value = vals[0];
                    }

                    var v4 = from nodes in item2.Descendants("Y")
                             select nodes;

                    foreach (var item4 in v4)
                    {
                        item4.Value = vals[1];
                    }
                }
            }
        }
    }

    public static bool IsRenderSettingStringValid(XDocument doc, string setting, XMLTypes type)
    {
        if (type != XMLTypes.String)
            return false;

        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "RenderSettings"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(type.ToString().ToLower())
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                return true;
            }
        }

        return false;
    }

    public static string GetRenderSettings(XDocument doc, string setting, XMLTypes type)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "RenderSettings"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants((type != XMLTypes.Vector2Int16 ? type.ToString().ToLower() : "Vector2int16"))
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                if (type != XMLTypes.Vector2Int16)
                {
                    return item2.Value;
                }
                else
                {
                    string ValX = "";
                    string ValY = "";

                    var v3 = from nodes in item2.Descendants("X")
                             select nodes;

                    foreach (var item3 in v3)
                    {
                        ValX = item3.Value;
                    }

                    var v4 = from nodes in item2.Descendants("Y")
                             select nodes;

                    foreach (var item4 in v4)
                    {
                        ValY = item4.Value;
                    }

                    return ValX + "x" + ValY;
                }
            }
        }

        return "";
    }

    public static void DownloadFilesFromNode(string url, string path, string fileext, string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            Downloader download = new Downloader(url, id);
            download.InitDownload(path, fileext, "", true, false);
            if (download.getDownloadOutcome().Contains("Error"))
            {
                throw new IOException(download.getDownloadOutcome());
            }
        }
    }

    public static string GetURLInNodes(XDocument doc, string itemClassValue, string itemIdValue, string url)
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
                            string oldurl = item3.Value;
                            string urlFixed = NovetusFuncs.FixURLString(oldurl, url);
                            string peram = "id=";

                            if (urlFixed.Contains(peram))
                            {
                                return urlFixed;
                            }
                        }
                    }
                    else
                    {
                        string oldurl = item3.Value;
                        string rbxassetid = "rbxassetid://";
                        string urlFixed = url + oldurl.After(rbxassetid);
                        string peram = "id=";

                        if (urlFixed.Contains(peram))
                        {
                            return urlFixed;
                        }
                    }
                }
            }
        }

        return "";
    }

    public static string RemoveInvalidXmlChars(string content)
    {
        return new string(content.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());
    }

    public static string ReplaceHexadecimalSymbols(string txt)
    {
        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F]";
        return Regex.Replace(txt, r, "", RegexOptions.Compiled);
    }
}
#endregion
#endregion
