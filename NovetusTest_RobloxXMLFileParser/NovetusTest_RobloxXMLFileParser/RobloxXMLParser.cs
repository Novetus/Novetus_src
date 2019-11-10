using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

public class RobloxXMLParser
{
    public static void SearchNodes(string path, string itemClassValue, string itemIdValue)
    {
        try
        {
            string oldfile = File.ReadAllText(path);
            string fixedfile = RemoveInvalidXmlChars(ReplaceHexadecimalSymbols(oldfile));
            Console.WriteLine("Valid: " + CheckValidXmlChars(fixedfile));
            XDocument doc = XDocument.Parse(fixedfile);

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
                        if (!item3.Value.Contains("rbxasset"))
                        {
                            //do whatever with your value
                            if (item3.Value.Contains('='))
                            {
                                string[] substrings = item3.Value.Split('=');

                                if (!string.IsNullOrWhiteSpace(substrings[1]))
                                {
                                    Console.WriteLine(item3.Value);
                                    Console.WriteLine("ID: " + substrings[1]);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static string RemoveInvalidXmlChars(string content)
    {
        return new string(content.Where(ch => System.Xml.XmlConvert.IsXmlChar(ch)).ToArray());
    }

    static string ReplaceHexadecimalSymbols(string txt)
    {
        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
        return Regex.Replace(txt, r, "", RegexOptions.Compiled);
    }

    public static bool CheckValidXmlChars(string content)
    {
        return content.All(ch => System.Xml.XmlConvert.IsXmlChar(ch));
    }
}
