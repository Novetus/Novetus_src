#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Splash Tester
    public partial class SplashTester : Form
    {
        #region Constructor
        public SplashTester()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label12.Text = textBox1.Text;
        }
        #endregion
    }
    #endregion
}
