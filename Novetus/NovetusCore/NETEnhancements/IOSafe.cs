using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Novetus.Core
{
    public static class IOSafe
    {
        public static class File
        {
            public static void Copy(string src, string dest, bool overwrite, bool overwritewarning = false)
            {
                if (System.IO.File.Exists(dest))
                {
                    if (overwrite && overwritewarning)
                    {
                        if (ShowOverrideWarning(dest) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    System.IO.File.SetAttributes(dest, FileAttributes.Normal);
                }

                System.IO.File.Copy(src, dest, overwrite);
                System.IO.File.SetAttributes(dest, FileAttributes.Normal);
            }

            public static void Delete(string src)
            {
                if (System.IO.File.Exists(src))
                {
                    System.IO.File.SetAttributes(src, FileAttributes.Normal);
                    System.IO.File.Delete(src);
                }
            }

            public static void Move(string src, string dest, bool overwrite, bool overwritewarning = false)
            {
                if (src.Equals(dest))
                    return;

                if (!System.IO.File.Exists(dest))
                {
                    System.IO.File.SetAttributes(src, FileAttributes.Normal);
                    System.IO.File.Move(src, dest);
                }
                else
                {
                    if (overwrite)
                    {
                        if (overwritewarning)
                        {
                            if (ShowOverrideWarning(dest) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        Delete(dest);
                        System.IO.File.SetAttributes(src, FileAttributes.Normal);
                        System.IO.File.Move(src, dest);
                    }
                    else
                    {
                        throw new IOException("Cannot create a file when that file already exists. FixedFileMove cannot override files with overwrite disabled.");
                    }
                }
            }
        }

        private static DialogResult ShowOverrideWarning(string dest)
        {
            DialogResult box = MessageBox.Show("A file with a similar name was detected in the directory as '" + dest +
                            "'.\n\nWould you like to override it?", "Novetus - Override Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            return box;
        }
    }
}
