#region Usings
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    [XmlIgnore]
    public string ColorGroup;
    [XmlIgnore]
    public string ColorRawName;
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

                if (!(colors.ColorList[i].ColorName.Contains("[") && colors.ColorList[i].ColorName.Contains("]")))
                {
                    colors.ColorList[i].ColorRawName = colors.ColorList[i].ColorName;
                    colors.ColorList[i].ColorName = "[Uncategorized]" + colors.ColorList[i].ColorName;
                }
                else
                {
                    colors.ColorList[i].ColorRawName = colors.ColorList[i].ColorName;
                }

                int pFrom = colors.ColorList[i].ColorName.IndexOf("[");
                int pTo = colors.ColorList[i].ColorName.IndexOf("]");
                colors.ColorList[i].ColorGroup = colors.ColorList[i].ColorName.Substring(pFrom + 1, pTo - pFrom - 1);
                colors.ColorList[i].ColorName = colors.ColorList[i].ColorName.Replace(colors.ColorList[i].ColorGroup, "").Replace("[", "").Replace("]", "");
            }

            return colors.ColorList;
        }
        else
        {
            return null;
        }
    }

    public static void AddPartColorsToListView(PartColor[] PartColorList, ListView ColorView, int imgsize, bool showIDs = false)
    {
        try
        {
            ImageList ColorImageList = new ImageList();
            ColorImageList.ImageSize = new Size(imgsize, imgsize);
            ColorImageList.ColorDepth = ColorDepth.Depth32Bit;
            ColorView.LargeImageList = ColorImageList;
            ColorView.SmallImageList = ColorImageList;

            foreach (var item in PartColorList)
            {
                var lvi = new ListViewItem(item.ColorName);
                lvi.Tag = item.ColorID;

                if (showIDs)
                {
                    lvi.Text = lvi.Text + " (" + item.ColorID + ")";
                }

                var group = ColorView.Groups.Cast<ListViewGroup>().FirstOrDefault(g => g.Header == item.ColorGroup);

                if (group == null)
                {
                    group = new ListViewGroup(item.ColorGroup);
                    ColorView.Groups.Add(group);
                }

                lvi.Group = group;

                Bitmap Bmp = new Bitmap(imgsize, imgsize, PixelFormat.Format32bppArgb);
                using (Graphics gfx = Graphics.FromImage(Bmp))
                using (SolidBrush brush = new SolidBrush(item.ColorObject))
                {
                    gfx.FillRectangle(brush, 0, 0, imgsize, imgsize);
                }

                ColorImageList.Images.Add(item.ColorName, Bmp);
                lvi.ImageIndex = ColorImageList.Images.IndexOfKey(item.ColorName);
                ColorView.Items.Add(lvi);
            }

            foreach (var group in ColorView.Groups.Cast<ListViewGroup>())
            {
                group.Header = group.Header + " (" + group.Items.Count + ")";
            }
        }
#if URI || LAUNCHER || CMD
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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
