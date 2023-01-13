#region Usings
using System;
using System.Windows.Forms;
using System.IO;
using Novetus.Core;
#endregion

#region ClientScriptDocumentation
public partial class ClientScriptDocumentation : Form
	{
        #region Constructor
        public ClientScriptDocumentation()
		{
			InitializeComponent();
		}
        #endregion

        #region Form Events
        void ClientScriptDocumentationLoad(object sender, EventArgs e)
		{
			richTextBox1.Text = File.ReadAllText(GlobalPaths.MiscDir + "\\" + GlobalPaths.ClientScriptDocumentationFileName);
		}
        #endregion
    }
    #endregion
