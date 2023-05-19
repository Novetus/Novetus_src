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
            GlobalVars.UserConfiguration.SaveSettingBool("NewGUI", NewGUI2011MBox.Checked);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.SaveSettingInt("GraphicsMode", comboBox1.SelectedIndex);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.SaveSettingInt("QualityLevel", comboBox2.SelectedIndex);

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
            ProcessPriorityClass setting = ProcessPriorityClass.RealTime;

            switch (comboBox3.SelectedIndex)
            {
                case 1:
                    setting = ProcessPriorityClass.BelowNormal;
                    break;
                case 2:
                    setting = ProcessPriorityClass.Normal;
                    break;
                case 3:
                    setting = ProcessPriorityClass.AboveNormal;
                    break;
                case 4:
                    setting = ProcessPriorityClass.High;
                    break;
                case 5:
                    setting = ProcessPriorityClass.RealTime;
                    break;
                default:
                    setting = ProcessPriorityClass.Idle;
                    break;
            }

            GlobalVars.UserConfiguration.SaveSettingInt("Priority", (int)setting);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GlobalVars.UserConfiguration.ReadSettingInt("QualityLevel") == (int)Settings.Level.Custom)
            {
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
            GlobalVars.UserConfiguration.SaveSettingInt("QualityLevel", (int)Settings.Level.Automatic);
            GlobalVars.UserConfiguration.SaveSettingInt("GraphicsMode", (int)Settings.Mode.Automatic);
            ReadConfigValues();
            MessageBox.Show("Graphics options reset for the currently selected client!", "Novetus - Client Settings Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Functions
        void ReadConfigValues()
        {
            NewGUI2011MBox.Checked = GlobalVars.UserConfiguration.ReadSettingBool("NewGUI");
            comboBox1.SelectedIndex = GlobalVars.UserConfiguration.ReadSettingInt("GraphicsMode");
            comboBox2.SelectedIndex = GlobalVars.UserConfiguration.ReadSettingInt("QualityLevel");

            switch ((ProcessPriorityClass)GlobalVars.UserConfiguration.ReadSettingInt("Priority"))
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
