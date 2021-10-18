#region Usings
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
#endregion

#region Part Color Options
public class PartColor
{
    public string ColorName;
    public int ColorID;
    public string ColorRGB;
    [XmlIgnore]
    public Color ColorObject;
}

[XmlRoot("PartColors")]
public class PartColors
{
    [XmlArray("ColorList")]
    public PartColor[] ColorList;
}

public class PartColorLoader
{
    public static PartColor[] GetPartColors()
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PartColors));

            FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName, FileMode.Open);
            PartColors colors;
            colors = (PartColors)serializer.Deserialize(fs);
            fs.Close();

            for (int i = 0; i < colors.ColorList.Length; i++)
            {
                string colorFixed = Regex.Replace(colors.ColorList[i].ColorRGB, @"[\[\]\{\}\(\)\<\> ]", "");
                string[] rgbValues = colorFixed.Split(',');
                colors.ColorList[i].ColorObject = Color.FromArgb(Convert.ToInt32(rgbValues[0]), Convert.ToInt32(rgbValues[1]), Convert.ToInt32(rgbValues[2]));
            }

            return colors.ColorList;
        }
        else
        {
            return null;
        }
    }

    public static PartColor FindPartColorByName(PartColor[] colors, string query)
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
        {
            return colors.SingleOrDefault(item => query.Contains(item.ColorName));
        }
        else
        {
            return null;
        }
    }

    public static PartColor FindPartColorByID(PartColor[] colors, string query)
    {
        if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
        {
            return colors.SingleOrDefault(item => query.Contains(item.ColorID.ToString()));
        }
        else
        {
            return null;
        }
    }
}
#endregion
