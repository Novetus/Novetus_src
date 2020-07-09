/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 10/7/2016
 * Time: 3:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace NovetusLauncher
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class SplashTester : Form
    {
        public SplashTester()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label12.Text = textBox1.Text;
        }
    }
}
