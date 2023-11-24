#region Usings
using Novetus.Core;
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
            //FileManagement.ReadInfoFile(GlobalPaths.ConfigDir + "\\" + GlobalPaths.InfoName,
                    //GlobalPaths.ConfigDir + "\\" + GlobalPaths.TermListFileName,
                    //GlobalPaths.RootPathLauncher + "\\Novetus.exe");
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Util.LogPrint("Attempting to install URI.");
            LocalFuncs.RegisterURI(this);
            Close();
        }
        #endregion
    }
    #endregion
}
