#region Usings
using Novetus.Core;
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
            ReadConfigValues();
            CenterToScreen();
        }

        private void NovetusSettings_Close(object sender, FormClosingEventArgs e)
        {
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

        private void NewGUI2011MBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.NewGUI = NewGUI2011MBox.Checked;
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

                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
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
                FileManagement.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, true);
                CustomGraphicsOptions opt = new CustomGraphicsOptions();
                opt.Show();
            }
            else
            {
                MessageBox.Show("You do not have the 'Custom' option selected. Please select it before continuing.", "Novetus - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.QualityLevel = Settings.Level.Automatic;
            GlobalVars.UserConfiguration.GraphicsMode = Settings.Mode.Automatic;
            ReadConfigValues();
            MessageBox.Show("Graphics options reset for the currently selected client!", "Novetus - Client Settings Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Functions
        void ReadConfigValues()
        {
            FileManagement.Config(GlobalPaths.ConfigDir + "\\" + GlobalPaths.ConfigName, false);
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
