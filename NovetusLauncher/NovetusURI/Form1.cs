using System;
using System.Windows.Forms;

namespace NovetusURI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SecurityFuncs.IsElevated)
            {
                try
                {
                    URIReg novURI = new URIReg("novetus","url.novetus");
                    novURI.Register();

                    MessageBox.Show("URI successfully installed and registered!", "Novetus - Install URI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to register. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to register. (Error: Did not run as Administrator)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
