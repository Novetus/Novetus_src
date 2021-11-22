#region Usings
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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
                        ValX =  item3.Value;
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

            try
            {
                download.InitDownload(path, fileext, "", true);
                if (download.getDownloadOutcome().Contains("Error"))
                {
                    throw new IOException(download.getDownloadOutcome());
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
                MessageBox.Show("The download has experienced an error: " + ex.Message, "Novetus Asset SDK - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static void DownloadOrFixURLS(XDocument doc, bool remote, string url, AssetCacheDef assetdef, string name = "", string meshname = "")
    {
        DownloadOrFixURLS(doc, remote, url, assetdef, 0, 0, 0, 0, name, meshname);
    }

    public static void DownloadOrFixURLS(XDocument doc, bool remote, string url, AssetCacheDef assetdef, int idIndex, int extIndex, int outputPathIndex, int inGameDirIndex, string name = "", string meshname = "")
    {
        DownloadOrFixURLS(doc, remote, url, assetdef.Class, assetdef.Id[idIndex], assetdef.Ext[extIndex], assetdef.Dir[outputPathIndex], assetdef.GameDir[inGameDirIndex], name, meshname);
    }

    public static void DownloadOrFixURLS(XDocument doc, bool remote, string url, string itemClassValue, string itemIdValue, string fileext, string outputPath, string inGameDir, string name = "", string meshname = "")
    {
        if (remote)
        {
            FixURLInNodes(doc, itemClassValue, itemIdValue, url);
        }
        else
        {
            DownloadFromNodes(doc, itemClassValue, itemIdValue, fileext, outputPath, inGameDir, name, meshname);
        }
    }

    public static void DownloadFromNodes(XDocument doc, string itemClassValue, string itemIdValue, string fileext, string outputPath, string inGameDir, string name = "", string meshname = "")
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
                                string newurl = "assetdelivery.roblox.com/v1/asset/?id=";
                                string urlFixed = url.Replace("http://", "https://")
                                    .Replace("?version=1&amp;id=", "?id=")
                                    .Replace("www.roblox.com/asset/?id=", newurl)
                                    .Replace("www.roblox.com/asset?id=", newurl)
                                    .Replace("assetgame.roblox.com/asset/?id=", newurl)
                                    .Replace("assetgame.roblox.com/asset?id=", newurl)
                                    .Replace("roblox.com/asset/?id=", newurl)
                                    .Replace("roblox.com/asset?id=", newurl)
                                    .Replace("&amp;", "&")
                                    .Replace("amp;", "&");
                                string peram = "id=";

                                if (string.IsNullOrWhiteSpace(name))
                                {
                                    if (urlFixed.Contains(peram))
                                    {
                                        string IDVal = urlFixed.After(peram);
                                        DownloadFilesFromNode(urlFixed, outputPath, fileext, IDVal);
                                        item3.Value = (inGameDir + IDVal + fileext).Replace(" ", "");
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
                            string urlFixed = "https://assetdelivery.roblox.com/v1/asset/?id=" + url.After(rbxassetid);
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

    public static void FixURLInNodes(XDocument doc, string itemClassValue, string itemIdValue, string url)
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
                            string urlFixed = oldurl.Replace("http://", "")
                                .Replace("https://", "")
                                .Replace("?version=1&amp;id=", "?id=")
                                .Replace("www.roblox.com/asset/?id=", url)
                                .Replace("www.roblox.com/asset?id=", url)
                                .Replace("assetgame.roblox.com/asset/?id=", url)
                                .Replace("assetgame.roblox.com/asset?id=", url)
                                .Replace("roblox.com/asset/?id=", url)
                                .Replace("roblox.com/asset?id=", url)
                                .Replace("&amp;", "&")
                                .Replace("amp;", "&");
                            string peram = "id=";

                            if (urlFixed.Contains(peram))
                            {
                                item3.Value = urlFixed;
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
                            item3.Value = urlFixed;
                        }
                    }
                }
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
                            string urlFixed = oldurl.Replace("http://", "")
                                .Replace("https://", "")
                                .Replace("?version=1&amp;id=", "?id=")
                                .Replace("www.roblox.com/asset/?id=", url)
                                .Replace("www.roblox.com/asset?id=", url)
                                .Replace("assetgame.roblox.com/asset/?id=", url)
                                .Replace("assetgame.roblox.com/asset?id=", url)
                                .Replace("roblox.com/asset/?id=", url)
                                .Replace("roblox.com/asset?id=", url)
                                .Replace("&amp;", "&")
                                .Replace("amp;", "&");
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
