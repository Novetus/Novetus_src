using NLog;
using System;
using System.Drawing;
using System.Drawing.Text;
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
            FileManagement.ReadInfoFile(LocalPaths.InfoPath, true, LocalPaths.LauncherPath);
            ReadConfigValues(LocalPaths.ConfigPath);

            if (GlobalVars.UserConfiguration.BootstrapperShowUI)
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

                VersionLabel.Text = GlobalVars.ProgramInformation.Version.ToUpper();
                CenterToScreen();
            }
            else
            {
                LaunchNovetus();
            }
        }

        void ReadConfigValues(string cfgpath)
        {
            FileManagement.Config(cfgpath, false);
            LauncherBox.Checked = !GlobalVars.UserConfiguration.BootstrapperShowUI;
        }

        private void LaunchNovetusButton_Click(object sender, EventArgs e)
        {
            LaunchNovetus();
        }

        private void LaunchNovetus()
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
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-cmd " + ArgBox.Text);
            Close();
        }

        private void CMDHelpButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.LauncherName, "-cmd -help");
        }

        private void DependencyInstallerButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplicationExt(GlobalPaths.BasePathLauncher, LocalPaths.DependencyLauncherName);
            Close();
        }

        private void URIButton_Click(object sender, EventArgs e)
        {
            LocalFuncs.LaunchApplication(LocalPaths.URIName);
            Close();
        }

        private void LauncherBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVars.UserConfiguration.BootstrapperShowUI = !LauncherBox.Checked;
        }
    }
}
