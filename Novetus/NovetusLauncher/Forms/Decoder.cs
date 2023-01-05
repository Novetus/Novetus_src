using Novetus.Core;
using System.Windows.Forms;

namespace NovetusLauncher
{
    public partial class Decoder : Form
    {
        public Decoder()
        {
            InitializeComponent();
        }

        private void Decoder_Load(object sender, System.EventArgs e)
        {
            CenterToScreen();
        }

        private void TextEntry_TextChanged(object sender, System.EventArgs e)
        {
            ResultBox.Text = SecurityFuncs.Decipher(TextEntry.Text, (int)Shift.Value);
        }

        private void Shift_ValueChanged(object sender, System.EventArgs e)
        {
            ResultBox.Text = SecurityFuncs.Decipher(TextEntry.Text, (int)Shift.Value);
        }
    }
}
