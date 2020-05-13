using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class NovetusSettings : Form
    {
        public NovetusSettings()
        {
            InitializeComponent();
        }

        void ReadConfigValues()
        {
            LauncherFuncs.Config(GlobalVars.ConfigDir + "\\" + GlobalVars.ConfigName, false);
            checkBox5.Checked = GlobalVars.ReShade;
            checkBox6.Checked = GlobalVars.ReShadeFPSDisplay;
            checkBox7.Checked = GlobalVars.ReShadePerformanceMode;
            if (GlobalVars.GraphicsMode == 1)
            {
                comboBox1.SelectedIndex = 0;
            }
            else if (GlobalVars.GraphicsMode == 2)
            {
                comboBox1.SelectedIndex = 1;
            }

            if (GlobalVars.QualityLevel == 1)
            {
                comboBox2.SelectedIndex = 0;
            }
            else if (GlobalVars.QualityLevel == 2)
            {
                comboBox2.SelectedIndex = 1;
            }
            else if (GlobalVars.QualityLevel == 3)
            {
                comboBox2.SelectedIndex = 2;
            }
            else if (GlobalVars.QualityLevel == 4)
            {
                comboBox2.SelectedIndex = 3;
            }
            else if (GlobalVars.QualityLevel == 5)
            {
                comboBox2.SelectedIndex = 4;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                GlobalVars.ReShade = true;
            }
            else if (checkBox5.Checked == false)
            {
                GlobalVars.ReShade = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                GlobalVars.ReShadeFPSDisplay = true;
            }
            else if (checkBox6.Checked == false)
            {
                GlobalVars.ReShadeFPSDisplay = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                GlobalVars.ReShadePerformanceMode = true;
            }
            else if (checkBox7.Checked == false)
            {
                GlobalVars.ReShadePerformanceMode = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                GlobalVars.GraphicsMode = 1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                GlobalVars.GraphicsMode = 2;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                GlobalVars.QualityLevel = 1;
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                GlobalVars.QualityLevel = 2;
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                GlobalVars.QualityLevel = 3;
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                GlobalVars.QualityLevel = 4;
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                GlobalVars.QualityLevel = 5;
            }
        }

        private void NovetusSettings_Load(object sender, EventArgs e)
        {
            ReadConfigValues();
        }
    }
}
