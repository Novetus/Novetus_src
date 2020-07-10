#region Usings
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Customization Functions
    class CustomizationFuncs
    {
        //modified from the following:
        //https://stackoverflow.com/questions/28887314/performance-of-image-loading
        //https://stackoverflow.com/questions/2479771/c-why-am-i-getting-the-process-cannot-access-the-file-because-it-is-being-u
        public static Image LoadImage(string fileFullName, string fallbackFileFullName = "")
        {
            Image image = null;

            try
            {
                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(fileFullName)))
                {
                    image = Image.FromStream(ms);
                }

                // PropertyItems seem to get lost when fileStream is closed to quickly (?); perhaps
                // this is the reason Microsoft didn't want to close it in the first place.
                PropertyItem[] items = image.PropertyItems;

                foreach (PropertyItem item in items)
                {
                    image.SetPropertyItem(item);
                }
            }
            catch (Exception)
            {
                image = LoadImage(fallbackFileFullName);
            }

            return image;
        }

        public static void ChangeItem(string item, string itemdir, string defaultitem, PictureBox outputImage, TextBox outputString, ListBox box, bool initial, bool hatsinextra = false)
        {
            if (Directory.Exists(itemdir))
            {
                if (initial)
                {
                    box.Items.Clear();
                    DirectoryInfo dinfo = new DirectoryInfo(itemdir);
                    FileInfo[] Files = dinfo.GetFiles("*.rbxm");
                    foreach (FileInfo file in Files)
                    {
                        if (file.Name.Equals(string.Empty))
                        {
                            continue;
                        }

                        if (hatsinextra)
                        {
                            if (file.Name.Equals("NoHat.rbxm"))
                            {
                                continue;
                            }
                        }

                        box.Items.Add(file.Name);
                    }
                    //selecting items triggers the event.
                    try
                    {
                        box.SelectedItem = item;
                    }
                    catch (Exception)
                    {
                        box.SelectedItem = defaultitem + ".rbxm";
                    }

                    box.Enabled = true;
                }
            }

            if (File.Exists(itemdir + @"\\" + item.Replace(".rbxm", "") + "_desc.txt"))
            {
                outputString.Text = File.ReadAllText(itemdir + @"\\" + item.Replace(".rbxm", "") + "_desc.txt");
            }
            else
            {
                outputString.Text = item;
            }

            if (IsItemURL(item))
            {
                outputImage.Image = GetItemURLImage(item);
            }
            else
            {
                outputImage.Image = LoadImage(itemdir + @"\\" + item.Replace(".rbxm", "") + ".png", itemdir + @"\\" + defaultitem + ".png");
            }
        }

        public static bool IsItemURL(string item)
        {
            switch (item)
            {
                case string finobe when finobe.Contains("http://finobe.com/asset/?id="):
                    return true;
                case string roblox when roblox.Contains("http://www.roblox.com/asset/?id="):
                    return true;
                default:
                    return false;
            }
        }

        public static Image GetItemURLImage(string item)
        {
            switch (item)
            {
                case string finobe when finobe.Contains("http://finobe.com/asset/?id="):
                    return LoadImage(GlobalPaths.CustomPlayerDir + @"\\finobe.png", GlobalPaths.extradir + @"\\NoExtra.png");
                case string roblox when roblox.Contains("http://www.roblox.com/asset/?id="):
                    return LoadImage(GlobalPaths.CustomPlayerDir + @"\\roblox.png", GlobalPaths.extradir + @"\\NoExtra.png");
                default:
                    return LoadImage(GlobalPaths.extradir + @"\\NoExtra.png");
            }
        }
    }
    #endregion
}
