using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace NovetusLauncher.Forms.SDK.ClientInfoCreator
{
    public partial class ClientInfoCreatorScriptEditor : Form
    {
        public string scriptText = "";

        public ClientInfoCreatorScriptEditor(string text)
        {
            InitializeComponent();
            CenterToScreen();
            scriptText = text;
        }

        private void ClientInfoCreatorScriptEditor_Load(object sender, EventArgs e)
        {
            editor.Text = scriptText;
        }

        private void ClientInfoCreatorScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //scriptText already changed in KeyPress but uh ok.
            scriptText = editor.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
