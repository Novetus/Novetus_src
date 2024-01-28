#region Usings
using Microsoft.Win32;
using Novetus.Core;
using System;
using System.IO;
using System.Windows.Forms;
#endregion

namespace NovetusURI
{
    #region LocalFuncs
    class LocalFuncs
    {
        public static void RegisterURI(Form form)
        {
            if (SecurityFuncs.IsElevated)
            {
                try
                {
                    URIReg novURI = new URIReg("novetus", "url.novetus");
                    novURI.Register();

                    InstallRegServer();

                    MessageBox.Show("URI and UserAgent successfully installed and registered!", "Novetus - Install URI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to register. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.Close();
                }
            }
            else
            {
                Util.LogPrint("Failed to register. (Error: Did not run as Administrator)", 2);
                MessageBox.Show("Failed to register. (Error: Did not run as Administrator)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form.Close();
            }
        }

        public static void InstallRegServer()
        {
            if (SecurityFuncs.IsElevated)
            {
                try
                {
                    using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("TypeLib", true))
                    {
                        if (key != null)
                        {
                            RegistryKey UARootKey = key.CreateSubKey("{03E1C8ED-C1C6-47BF-B9B9-A37B677318DD}");

                            RegistryKey UAKey12 = UARootKey.CreateSubKey("1.2");
                            UAKey12.SetValue("", "Roblox 1.2 Type Library");
                            RegistryKey UAKey12Win32 = UAKey12.CreateSubKey("0").CreateSubKey("win32");
                            string client1Path = GlobalPaths.ClientDir + @"\\" + GlobalVars.ProgramInformation.RegisterClient1;
                            string fixedpath1 = client1Path.Replace(@"\\", @"\");
                            UAKey12Win32.SetValue("", fixedpath1 + @"\RobloxApp_studio.exe");
                            UAKey12.CreateSubKey("FLAGS").SetValue("", "0");
                            UAKey12.CreateSubKey("HELPDIR").SetValue("", fixedpath1 + @"\");

                            RegistryKey UAKey13 = UARootKey.CreateSubKey("1.3");
                            UAKey13.SetValue("", "Roblox 1.3 Type Library");
                            RegistryKey UAKey13Win32 = UAKey13.CreateSubKey("0").CreateSubKey("win32");
                            string client2Path = GlobalPaths.ClientDir + @"\\" + GlobalVars.ProgramInformation.RegisterClient2;
                            string fixedpath2 = client2Path.Replace(@"\\", @"\");
                            UAKey13Win32.SetValue("", fixedpath2 + @"\RobloxApp_studio.exe");
                            UAKey13.CreateSubKey("FLAGS").SetValue("", "0");
                            UAKey13.CreateSubKey("HELPDIR").SetValue("", fixedpath2 + @"\");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.LogExceptions(ex);
                }
            }
        }

        public static void SetupURIValues()
        {
            try
            {
                string ExtractedArg = LocalVars.SharedArgs.Replace("novetus://", "").Replace("novetus", "").Replace(":", "").Replace("/", "").Replace("?", "");
                string ConvertedArg = SecurityFuncs.Decode(ExtractedArg, true);
                string[] SplitArg = ConvertedArg.Split('|');
                string ip = SecurityFuncs.Decode(SplitArg[0]);
                string port = SecurityFuncs.Decode(SplitArg[1]);
                string client = SecurityFuncs.Decode(SplitArg[2]);
                GlobalVars.UserConfiguration.SaveSetting("SelectedClient", client);
                GlobalVars.CurrentServer.ServerIP = ip;
                GlobalVars.CurrentServer.ServerPort = ConvertSafe.ToInt32Safe(port);
                Client.ReadClientValues();
            }
            catch (Exception ex)
            {
                Util.LogExceptions(ex);
            }
        }
    }
    #endregion
}
