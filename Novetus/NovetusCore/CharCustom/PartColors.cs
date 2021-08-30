#region Usings
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
#endregion

#region Part Color Options
public class PartColor
{
    public string ColorName;
    public int ColorID;
    public string ColorRGB;
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
