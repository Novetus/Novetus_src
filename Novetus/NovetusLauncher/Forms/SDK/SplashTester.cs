#region Usings
using Novetus.Core;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
#endregion

#region Splash Tester
public partial class SplashTester : Form
{
    #region Constructor
    public SplashTester()
    {
        InitializeComponent();
    }
    #endregion

    #region Form Events
    private void entryBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            contextToolTipStylish.ToolTipIcon = ToolTipIcon.None;
            contextToolTipStylish.ToolTipTitle = "";
            contextToolTipStylish.SetToolTip(splashLabelStylish, null);

            contextToolTipNormal.ToolTipIcon = ToolTipIcon.None;
            contextToolTipNormal.ToolTipTitle = "";
            contextToolTipNormal.SetToolTip(splashLabelNormal, null);

            fromDate.Text = "";
            toDate.Text = "";
            persistantFromDate.Text = "";
            persistantToDate.Text = "";

            Splash splash = new Splash(entryBox.Text, specialSplashTesting.Checked);

            bool stylishLabel = true;
            bool normalLabel = true;

            if (splash.Compatibility == SplashCompatibility.Stylish)
            {
                normalLabel = false;
                splashLabelNormal.Text = "";
            }
            else if (splash.Compatibility == SplashCompatibility.Normal)
            {
                stylishLabel = false;
                splashLabelStylish.Text = "";
            }

            if (stylishLabel)
            {
                splashLabelStylish.Text = splash.SplashText;

                if (!string.IsNullOrWhiteSpace(splash.SplashContext))
                {
                    contextToolTipStylish.ToolTipIcon = ToolTipIcon.Info;
                    contextToolTipStylish.ToolTipTitle = "Context (Stylish)";
                    contextToolTipStylish.SetToolTip(splashLabelStylish, splash.SplashContext);
                }
            }

            if (normalLabel)
            {
                splashLabelNormal.Text = splash.SplashText;

                if (!string.IsNullOrWhiteSpace(splash.SplashContext))
                {
                    contextToolTipNormal.ToolTipIcon = ToolTipIcon.Info;
                    contextToolTipNormal.ToolTipTitle = "Context (Normal)";
                    contextToolTipNormal.SetToolTip(splashLabelNormal, splash.SplashContext);
                }
            }

        
            if (splash.SplashFirstAppearanceDate != null)
            {
                if (splash.SplashEndAppearanceDate != null)
                {
                    fromDate.Text = splash.SplashFirstAppearanceDate.Value.Month + "/" + splash.SplashFirstAppearanceDate.Value.Day;
                    toDate.Text = splash.SplashEndAppearanceDate.Value.Month + "/" + splash.SplashEndAppearanceDate.Value.Day;

                    if (splash.SplashDateStopAppearingAllTheTime != null && splash.SplashDateStartToAppearLess != null)
                    {
                        persistantFromDate.Text = splash.SplashDateStopAppearingAllTheTime.Value.Month + "/" + splash.SplashDateStopAppearingAllTheTime.Value.Day;
                        persistantToDate.Text = splash.SplashDateStartToAppearLess.Value.Month + "/" + splash.SplashDateStartToAppearLess.Value.Day;
                    }
                }
                else
                {
                    fromDate.Text = splash.SplashFirstAppearanceDate.Value.Month + "/" + splash.SplashFirstAppearanceDate.Value.Day;
                    toDate.Text = splash.SplashFirstAppearanceDate.Value.Month + "/" + (splash.SplashFirstAppearanceDate.Value.Day + 1);
                }
            }
            else if (splash.SplashWeekday != null)
            {
                fromDate.Text = splash.SplashWeekday.ToString();
                toDate.Text = splash.SplashWeekday.ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogExceptions(ex);
        }
    }

    private void SplashTester_Load(object sender, EventArgs e)
    {
        //splash loader
        CryptoRandom rand = new CryptoRandom();
        int randColor = rand.Next(0, 3);

        if (randColor == 0)
        {
            splashLabelStylish.BackColor = Color.FromArgb(245, 135, 13);
        }
        else if (randColor == 1)
        {
            splashLabelStylish.BackColor = Color.FromArgb(255, 3, 2);
        }
        else if (randColor == 2)
        {
            splashLabelStylish.BackColor = Color.FromArgb(238, 154, 181);
        }

        entryBox.Text = "Novetus!|This is placeholder text!";

        CenterToScreen();
    }

    private void changeStylishColor_Click(object sender, EventArgs e)
    {
        Color orange = Color.FromArgb(245, 135, 13);
        Color red = Color.FromArgb(255, 3, 2);
        Color pink = Color.FromArgb(238, 154, 181);

        if (splashLabelStylish.BackColor == pink)
        {
            splashLabelStylish.BackColor = orange;
        }
        else if (splashLabelStylish.BackColor == orange)
        {
            splashLabelStylish.BackColor = red;
        }
        else if (splashLabelStylish.BackColor == red)
        {
            splashLabelStylish.BackColor = pink;
        }
    }

    void splashLabelStylish_Paint(object sender, PaintEventArgs e)
    {
        Util.DrawBorderSimple(e.Graphics, splashLabelStylish.DisplayRectangle, Color.White, ButtonBorderStyle.Solid, 1);
    }

    private void variableToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ToolStripMenuItem senderitem = (ToolStripMenuItem)sender;
        entryBox.Paste(senderitem.Text);
    }
    #endregion
}
#endregion
