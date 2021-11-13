#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace NovetusURI
{
    #region URI Installation Form
    public partial class InstallForm : Form
    {
        #region Constructor
        public InstallForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void InstallForm_Load(object sender, EventArgs e)
        {
            GlobalFuncs.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName, true,
                    GlobalPaths.RootPathLauncher + "\\Novetus.exe");
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalFuncs.LogPrint("Attempting to install URI.");
            LocalFuncs.RegisterURI(this);
            Close();
        }
        #endregion
    }
    #endregion
}
