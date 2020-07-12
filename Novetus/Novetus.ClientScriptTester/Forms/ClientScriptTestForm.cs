#region Usings
using System;
using System.Windows.Forms;
#endregion

namespace Novetus.ClientScriptTester
{
    #region ClientScriptTestForm
    public partial class ClientScriptTestForm : Form
    {
        #region Constructor
        public ClientScriptTestForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void ClientScriptTestForm_Load(object sender, EventArgs e)
        {
            foreach (string str in LocalVars.SharedArgs)
            {
                OutputBox.AppendText(str + Environment.NewLine + Environment.NewLine);
            }
        }
        #endregion
    }
    #endregion
}
