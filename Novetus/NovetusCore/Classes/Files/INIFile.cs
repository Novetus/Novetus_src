#region Usings
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
#endregion

namespace Novetus.Core
{
    #region INI File Parser
    //modified from https://www.codeproject.com/articles/1966/an-ini-file-handling-class-using-c?fid=425860&df=90&mpp=25&prof=True&sort=Position&view=Normal&spc=Relaxed&fr=51

    public class INIFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public INIFile(string INIPath, bool createNewFile = true)
        {
            path = INIPath;

            if (createNewFile)
            {
                IOSafe.File.Delete(path);
                File.Create(path).Close();
            }
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Default Value. Optional for creating values in case they are invalid."></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string DefaultValue = "")
        {
            if (IniValueExists(Key))
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp,
                                      255, path);
                return temp.ToString();
            }
            else
            {
                IniWriteValue(Section, Key, DefaultValue);
                return DefaultValue;
            }
        }

        public bool IniValueExists(string SearchString)
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);

                    foreach (string line in lines)
                    {
                        if (line.Contains(SearchString))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return false;
            }
        }

        public string IniGetKey(string SearchString)
        {
            try
            {
                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);

                    foreach (string line in lines)
                    {
                        if (line.Contains(SearchString))
                        {
                            string Key = line.Replace(line.After("="), "").Replace("=", "");
                            return Key;
                        }
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
                return "";
            }
        }
    }
    #endregion
}
