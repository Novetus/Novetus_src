#region Usings
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion

#region XML Types
public enum XMLTypes
{
    Token,
    Bool,
    Float,
    String
}
#endregion

#region Roblox XML Parser
public static class RobloxXML
{
    public static string GetStringForXMLType(XMLTypes type)
    {
        switch (type)
        {
            case XMLTypes.Bool:
                return "bool";
            case XMLTypes.Float:
                return "float";
            case XMLTypes.Token:
                return "token";
            case XMLTypes.String:
            default:
                return "string";
        }
    }

    public static void EditRenderSettings(XDocument doc, string setting, string value, XMLTypes type)
    {
        var v = from nodes in doc.Descendants("Item")
                where nodes.Attribute("class").Value == "RenderSettings"
                select nodes;

        foreach (var item in v)
        {
            var v2 = from nodes in item.Descendants(GetStringForXMLType(type))
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                item2.Value = value;
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
            var v2 = from nodes in item.Descendants(GetStringForXMLType(type))
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
            var v2 = from nodes in item.Descendants(GetStringForXMLType(type))
                     where nodes.Attribute("name").Value == setting
                     select nodes;

            foreach (var item2 in v2)
            {
                return item2.Value;
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
