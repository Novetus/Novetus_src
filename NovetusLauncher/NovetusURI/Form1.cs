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
                    string loadstring = GlobalVars.BasePath + "/" + AppDomain.CurrentDomain.FriendlyName;
                    SecurityFuncs.RegisterURLProtocol("Novetus", loadstring, "Novetus URI");

                    MessageBox.Show("URI and Library Successfully Installed and Registered!", "Novetus - Install URI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) when (!Env.Debugging)
                {
                    MessageBox.Show("Failed to register. (Error: " + ex.Message + ")", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to register. (Error: Did not run as Administrator)", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
