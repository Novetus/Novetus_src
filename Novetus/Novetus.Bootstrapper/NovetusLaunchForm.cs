using Novetus.Core;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Novetus.Bootstrapper
{
    public partial class NovetusLaunchForm : Form
    {
        public NovetusLaunchForm()
        {
            InitializeComponent();
        }

        private void NovetusLaunchForm_Load(object sender, EventArgs e)
        {
            FileManagement.ReadInfoFile(LocalPaths.InfoPath,
                    LocalPaths.VersionTermList,
                    LocalPaths.LauncherPath);

            if (File.Exists(LocalPaths.ConfigPath))
            {
                ReadConfigValues(LocalPaths.ConfigPath);
            }

            if (GlobalVars.UserConfiguration.ReadSettingBool("BootstrapperShowUI"))
            {
                //use novetus font for label!!

                //dammit windows 11...
                /*GlobalFuncs.LogPrint("Loading Font...");
                try
                {
                    PrivateFontCollection pfc = new PrivateFontCollection();
                    string fontPath = LocalPaths.FixedDataDir + "\\BootstrapperFont.ttf";
                    pfc.AddFontFile(fontPath);

                    foreach (var fam in pfc.Families)
                    {
                        VersionLabel.Font = new Font(fam, VersionLabel.Font.Size);
                        LaunchNovetusButton.Font = new Font(fam, VersionLabel.Font.Size);
                    }
                    GlobalFuncs.LogPrint("Font Loaded");
                }
                catch (Exception ex)
                {
                    GlobalFuncs.LogExceptions(ex);
                }*/

                if (File.Exists(LocalPaths.ConfigPath))
                {
                    VersionLabel.Text = GlobalVars.ProgramInformation.Version.ToUpper();
                }
                CenterToScreen();
            }
            else
            {
                LaunchNovetus();
            }
        }

        void ReadConfigValues(string cfgpath)
        {
            LauncherBox.Checked = !GlobalVars.UserConfiguration.ReadSettingBool("BootstrapperShowUI");
        }

        private void LaunchNovetusButton_Click(object sender, EventArgs e)
        {
            LaunchNovetus();
        }

        private void LaunchNovetus()
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-nocmd");
            Close();
        }

        private void LaunchNovetusWithConsoleButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName);
            Close();
        }

        private void LaunchSDKButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-sdk");
            Close();
        }

        private void CMDButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-cmdonly " + ArgBox.Text);
            Close();
        }

        private void CMDHelpButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-cmdonly -help");
        }

        private void DependencyInstallerButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplicationExt(GlobalPaths.BasePath, LocalPaths.DependencyLauncherName);
            Close();
        }

        private void URIButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.URIName);
            Close();
        }

        private void LauncherBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.SaveSettingBool("BootstrapperShowUI", !LauncherBox.Checked);
        }

        private void CMDBarebonesButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-cmdonly " + ArgBox.Text);
        }
    }
}
