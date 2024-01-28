using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Novetus.Core
{
    #region Roblox XML Parser
    public static class RobloxXML
    {
        public enum XMLTypes
        {
            Token,
            Bool,
            Float,
            String,
            Vector2Int16,
            Int
        }

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
}
