using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Novetus.Core
{
    #region Part Color Options
    [JsonObject(MemberSerialization.OptIn)]
    public class PartColor
    {
        [JsonProperty]
        [JsonRequired]
        public string ColorName;
        [JsonProperty]
        [JsonRequired]
        public int ColorID;
        [JsonProperty]
        [JsonRequired]
        public string ColorRGB;
        public Color ColorObject;
        public string ColorGroup;
        public string ColorRawName;
        public Bitmap ColorImage;

        public static PartColor[] GetPartColors()
        {
            if (File.Exists(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName))
            {
                List<PartColor> colors = JsonConvert.DeserializeObject<List<PartColor>>(File.ReadAllText(GlobalPaths.ConfigDir + "\\" + GlobalPaths.PartColorXMLName));

                foreach (var item in colors)
                {
                    string colorFixed = Regex.Replace(item.ColorRGB, @"[\[\]\{\}\(\)\<\> ]", "");
                    string[] rgbValues = colorFixed.Split(',');
                    item.ColorObject = Color.FromArgb(ConvertSafe.ToInt32Safe(rgbValues[0]), ConvertSafe.ToInt32Safe(rgbValues[1]), ConvertSafe.ToInt32Safe(rgbValues[2]));

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

                return colors.ToArray();
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
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
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
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
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
}
