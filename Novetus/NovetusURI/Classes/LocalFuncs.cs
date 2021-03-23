#region Usings
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

                    MessageBox.Show("URI successfully installed and registered!", "Novetus - Install URI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to register. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to register. (Error: Did not run as Administrator)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                form.Close();
            }
        }

        public static void SetupURIValues()
        {
            string ExtractedArg = LocalVars.SharedArgs.Replace("novetus://", "").Replace("novetus", "").Replace(":", "").Replace("/", "").Replace("?", "");
            string ConvertedArg = SecurityFuncs.Base64DecodeOld(ExtractedArg);
            string[] SplitArg = ConvertedArg.Split('|');
            string ip = SecurityFuncs.Base64Decode(SplitArg[0]);
            string port = SecurityFuncs.Base64Decode(SplitArg[1]);
            string client = SecurityFuncs.Base64Decode(SplitArg[2]);
            GlobalVars.UserConfiguration.SelectedClient = client;
            GlobalVars.IP = ip;
            GlobalVars.JoinPort = Convert.ToInt32(port);
            GlobalFuncs.ReadClientValues();
        }
    }
    #endregion
}
