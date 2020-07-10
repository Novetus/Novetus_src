#region Usings
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region Diogenes Editor
    public partial class DiogenesEditor : Form
    {
        #region Constructor
        public DiogenesEditor()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            label2.Text = "Not Loaded";
            richTextBox1.Text = "";
        }

        void LoadToolStripMenuItemClick(object sender, EventArgs e)
        {
            SDKFuncs.LoadDiogenes(label2.Text, richTextBox1.Text);
        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            SDKFuncs.SaveDiogenes(label2.Text, richTextBox1.Lines);
        }

        void saveAsTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SDKFuncs.SaveDiogenes(label2.Text, richTextBox1.Lines, true);
        }
        #endregion
    }
    #endregion
}
