#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region LauncherForm - Stylish - ServerInfo
    public partial class LauncherFormStylishServerInfo : Form
    {
        #region Constructor
        public LauncherFormStylishServerInfo()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private async void LauncherFormStylishServerInfo_Load(object sender, EventArgs e)
        {
            await LauncherFormShared.LoadServerInformation(ServerInfo);
        }
        #endregion
    }
    #endregion
}
