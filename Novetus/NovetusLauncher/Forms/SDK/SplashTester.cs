#region Usings
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
        splashLabelNormal.Text = entryBox.Text;
        splashLabelStylish.Text = entryBox.Text;
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
        GlobalFuncs.DrawBorderSimple(e.Graphics, splashLabelStylish.DisplayRectangle, Color.White, ButtonBorderStyle.Solid, 1);
    }
    #endregion
}
#endregion
