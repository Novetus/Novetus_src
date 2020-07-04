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

            switch (GlobalVars.GraphicsMode)
            {
                case GraphicsMode.DirectX:
                    comboBox1.SelectedIndex = 1;
                    break;
                case GraphicsMode.OpenGL:
                default:
                    comboBox1.SelectedIndex = 0;
                    break;
            }

            switch (GlobalVars.QualityLevel)
            {
                case QualityLevel.VeryLow:
                    comboBox2.SelectedIndex = 0;
                    break;
                case QualityLevel.Low:
                    comboBox2.SelectedIndex = 1;
                    break;
                case QualityLevel.Medium:
                    comboBox2.SelectedIndex = 2;
                    break;
                case QualityLevel.High:
                    comboBox2.SelectedIndex = 3;
                    break;
                case QualityLevel.Ultra:
                default:
                    comboBox2.SelectedIndex = 4;
                    break;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.ReShade = checkBox5.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.ReShadeFPSDisplay = checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.ReShadePerformanceMode = checkBox7.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    GlobalVars.GraphicsMode = GraphicsMode.DirectX;
                    break;
                default:
                    GlobalVars.GraphicsMode = GraphicsMode.OpenGL;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    GlobalVars.QualityLevel = QualityLevel.VeryLow;
                    break;
                case 1:
                    GlobalVars.QualityLevel = QualityLevel.Low;
                    break;
                case 2:
                    GlobalVars.QualityLevel = QualityLevel.Medium;
                    break;
                case 3:
                    GlobalVars.QualityLevel = QualityLevel.High;
                    break;
                case 4:
                default:
                    GlobalVars.QualityLevel = QualityLevel.Ultra;
                    break;
            }
        }

        private void NovetusSettings_Load(object sender, EventArgs e)
        {
            ReadConfigValues();
        }
    }
}
