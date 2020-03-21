/*
 * Created by SharpDevelop.
 * User: BITL-Gaming
 * Date: 10/7/2016
 * Time: 3:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;
using Mono.Nat;
using System.Globalization;

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

        private void SplashTester_Load(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(GlobalVars.ConfigDir + "\\info.txt"); //File is in System.IO
            string version = lines[0].Replace("%build%", Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());
            GlobalVars.DefaultClient = lines[1];
            GlobalVars.DefaultMap = lines[2];
            GlobalVars.SelectedClient = GlobalVars.DefaultClient;
            GlobalVars.Map = GlobalVars.DefaultMap;
            label11.Text = version;
            GlobalVars.Version = version;

            ReadConfigValues();
        }

        void ReadConfigValues()
        {
            LauncherFuncs.ReadConfigValues(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName);
            textBox5.Text = GlobalVars.UserID.ToString();
            textBox2.Text = GlobalVars.PlayerName;
            label26.Text = GlobalVars.SelectedClient;
            label28.Text = GlobalVars.Map;
        }
    }
}
