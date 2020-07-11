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
        private void button1_Click(object sender, EventArgs e)
        {
            LocalFuncs.RegisterURI(this);
        }
        #endregion
    }
    #endregion
}
