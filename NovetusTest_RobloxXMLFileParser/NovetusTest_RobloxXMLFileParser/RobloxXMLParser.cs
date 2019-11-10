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
    /*
     * todo: make it so it:
     * 
     * - downloads files from ROBLOX's servers from the links in the xml
     * - puts them in their respective assetcache folders, naming them as [ID].[FILEEXT}
     * - modifies the value in the xml to contain the new link
     * - save the roblox xml.
     */
    public static void SearchNodes(string path, string itemClassValue, string itemIdValue)
    {
        try
        {
            string oldfile = File.ReadAllText(path);
            string fixedfile = RemoveInvalidXmlChars(ReplaceHexadecimalSymbols(oldfile));
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
