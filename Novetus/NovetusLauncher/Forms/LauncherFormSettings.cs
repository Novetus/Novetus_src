#region Usings
using System;
using System.Diagnostics;
using System.Windows.Forms;
#endregion

namespace NovetusLauncher
{
    #region LauncherFormSettings
    public partial class LauncherFormSettings : Form
    {
        #region Constructor
        public LauncherFormSettings()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void NovetusSettings_Load(object sender, EventArgs e)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
            ReadConfigValues();
            CenterToScreen();
        }

        private void NovetusSettings_Close(object sender, FormClosingEventArgs e)
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);

            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "CustomGraphicsOptions")
                {
                    frm.Close();
                    break;
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ReShade = checkBox5.Checked;
        }

        private void NewGUI2011MBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.NewGUI = NewGUI2011MBox.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ReShadeFPSDisplay = checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.ReShadePerformanceMode = checkBox7.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.GraphicsMode = (Settings.Mode)comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.QualityLevel = (Settings.Level)comboBox2.SelectedIndex;

            if (comboBox2.SelectedIndex != 6)
            {
                //https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp

                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "CustomGraphicsOptions")
                    {
                        frm.Close();
                        break;
                    }
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 1:
                    GlobalVars.UserConfiguration.Priority = ProcessPriorityClass.BelowNormal;
                    break;
                case 2:
                    GlobalVars.UserConfiguration.Priority = ProcessPriorityClass.Normal;
                    break;
                case 3:
                    GlobalVars.UserConfiguration.Priority = ProcessPriorityClass.AboveNormal;
                    break;
                case 4:
                    GlobalVars.UserConfiguration.Priority = ProcessPriorityClass.High;
                    break;
                case 5:
                    GlobalVars.UserConfiguration.Priority = ProcessPriorityClass.RealTime;
                    break;
                default:
                    GlobalVars.UserConfiguration.Priority = ProcessPriorityClass.Idle;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GlobalVars.UserConfiguration.QualityLevel == Settings.Level.Custom)
            {
                GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
                CustomGraphicsOptions opt = new CustomGraphicsOptions();
                opt.Show();
            }
            else
            {
                MessageBox.Show("You do not have the 'Custom' option selected. Please select it before continuing.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Functions
        void ReadConfigValues()
        {
            GlobalFuncs.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
            checkBox5.Checked = GlobalVars.UserConfiguration.ReShade;
            checkBox6.Checked = GlobalVars.UserConfiguration.ReShadeFPSDisplay;
            checkBox7.Checked = GlobalVars.UserConfiguration.ReShadePerformanceMode;
            NewGUI2011MBox.Checked = GlobalVars.UserConfiguration.NewGUI;
            comboBox1.SelectedIndex = (int)GlobalVars.UserConfiguration.GraphicsMode;
            comboBox2.SelectedIndex = (int)GlobalVars.UserConfiguration.QualityLevel;

            switch (GlobalVars.UserConfiguration.Priority)
            {
                case ProcessPriorityClass.BelowNormal:
                    comboBox3.SelectedIndex = 1;
                    break;
                case ProcessPriorityClass.Normal:
                    comboBox3.SelectedIndex = 2;
                    break;
                case ProcessPriorityClass.AboveNormal:
                    comboBox3.SelectedIndex = 3;
                    break;
                case ProcessPriorityClass.High:
                    comboBox3.SelectedIndex = 4;
                    break;
                case ProcessPriorityClass.RealTime:
                    comboBox3.SelectedIndex = 5;
                    break;
                case ProcessPriorityClass.Idle:
                default:
                    comboBox3.SelectedIndex = 0;
                    break;
            }
        }
        #endregion
    }
    #endregion
}
