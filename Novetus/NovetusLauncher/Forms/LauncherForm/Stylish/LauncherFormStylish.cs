using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace NovetusLauncher
{
    public partial class LauncherFormStylish : Form
    {
        public LauncherFormStylish()
        {
            InitializeComponent();
        }

        private void LauncherFormStylish_Load(object sender, EventArgs e)
        {
            CryptoRandom rand = new CryptoRandom();
            int randColor = rand.Next(0, 2);

            switch (randColor)
            {
                case 0:
                    //orange message
                    splashLabel.BackColor = Color.FromArgb(245, 135, 13);
                    break;
                case 1:
                    //red message
                    splashLabel.BackColor = Color.FromArgb(255, 3, 2);
                    break;
                default:
                    break;
            }
        }

        void splashLabel_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderSimple(e.Graphics, splashLabel.DisplayRectangle, Color.White, ButtonBorderStyle.Solid, 1);
        }

        void DrawBorderSimple(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style, int width)
        {
            //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            ControlPaint.DrawBorder(graphics, bounds, 
                color, width, style, 
                color, width, style, 
                color, width, style, 
                color, width, style);
        }
    }
}
