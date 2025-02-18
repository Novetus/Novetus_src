namespace NovetusLauncher.Forms.SDK.ClientInfoCreator
{
    partial class ClientInfoCreatorScriptEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientInfoCreatorScriptEditor));
            this.editor = new ICSharpCode.TextEditor.TextEditorControl();
            this.SuspendLayout();
            // 
            // editor
            // 
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Highlighting = "SQL";
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Name = "editor";
            this.editor.ShowEOLMarkers = true;
            this.editor.Size = new System.Drawing.Size(787, 528);
            this.editor.TabIndex = 0;
            // 
            // ClientInfoCreatorScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 528);
            this.Controls.Add(this.editor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(373, 157);
            this.Name = "ClientInfoCreatorScriptEditor";
            this.Text = "LaunchScript Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientInfoCreatorScriptEditor_FormClosing);
            this.Load += new System.EventHandler(this.ClientInfoCreatorScriptEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl editor;
    }
}