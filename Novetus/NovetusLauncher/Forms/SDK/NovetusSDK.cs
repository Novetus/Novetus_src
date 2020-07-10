#region Usings
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Novetus SDK Launcher
    public partial class NovetusSDK : Form
    {
        #region Constructor
        public NovetusSDK()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void NovetusSDK_Load(object sender, EventArgs e)
        {
            Text = "Novetus SDK " + GlobalVars.ProgramInformation.Version;
            label1.Text = GlobalVars.ProgramInformation.Version;
        }

        private void NovetusSDK_Close(object sender, CancelEventArgs e)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SDKFuncs.LaunchSDKAppByIndex(listBox1.SelectedIndex);
        }
        #endregion
    }
    #endregion
}
