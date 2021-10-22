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
    [XmlIgnore]
    public Bitmap ColorImage;
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
            PartColors colors;

            using (FileStream fs = new FileStream(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName, FileMode.Open))
            {
                colors = (PartColors)serializer.Deserialize(fs);
            }

            foreach (var item in colors.ColorList)
            {
                string colorFixed = Regex.Replace(item.ColorRGB, @"[\[\]\{\}\(\)\<\> ]", "");
                string[] rgbValues = colorFixed.Split(',');
                item.ColorObject = Color.FromArgb(Convert.ToInt32(rgbValues[0]), Convert.ToInt32(rgbValues[1]), Convert.ToInt32(rgbValues[2]));

                if (!(item.ColorName.Contains("[") && item.ColorName.Contains("]")))
                {
                    item.ColorRawName = item.ColorName;
                    item.ColorName = "[Uncategorized]" + item.ColorName;
                }
                else
                {
                    item.ColorRawName = item.ColorName;
                }

                int pFrom = item.ColorName.IndexOf("[");
                int pTo = item.ColorName.IndexOf("]");
                item.ColorGroup = item.ColorName.Substring(pFrom + 1, pTo - pFrom - 1);
                item.ColorName = item.ColorName.Replace(item.ColorGroup, "").Replace("[", "").Replace("]", "");
                item.ColorImage = GeneratePartColorIcon(item, 128);
            }

            return colors.ColorList;
        }
        else
        {
            return null;
        }
    }

    //make faster
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

                if (item.ColorImage != null)
                {
                    ColorImageList.Images.Add(item.ColorName, item.ColorImage);
                    lvi.ImageIndex = ColorImageList.Images.IndexOfKey(item.ColorName);
                }

                ColorView.Items.Add(lvi);
            }

            /*foreach (var group in ColorView.Groups.Cast<ListViewGroup>())
            {
                group.Header = group.Header + " (" + group.Items.Count + ")";
            }*/
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

    public static Bitmap GeneratePartColorIcon(PartColor color, int imgsize)
    {
        try
        {
            Bitmap Bmp = new Bitmap(imgsize, imgsize, PixelFormat.Format32bppArgb);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(color.ColorObject))
            {
                gfx.FillRectangle(brush, 0, 0, imgsize, imgsize);
            }

            return Bmp;
        }
#if URI || LAUNCHER || CMD
        catch (Exception ex)
        {
            GlobalFuncs.LogExceptions(ex);
#else
		catch (Exception)
		{
#endif
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
