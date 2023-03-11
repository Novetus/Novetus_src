    partial class PlaceCompressor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaceCompressor));
            this.statusLabel = new System.Windows.Forms.Label();
            this.processStatus = new System.Windows.Forms.Label();
            this.selectButton = new System.Windows.Forms.Button();
            this.selectedText = new System.Windows.Forms.Label();
            this.currentFile = new System.Windows.Forms.Label();
            this.sourceLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 15);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 13);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Status:";
            // 
            // processStatus
            // 
            this.processStatus.AutoSize = true;
            this.processStatus.Location = new System.Drawing.Point(49, 15);
            this.processStatus.Name = "processStatus";
            this.processStatus.Size = new System.Drawing.Size(74, 13);
            this.processStatus.TabIndex = 1;
            this.processStatus.Text = "processStatus";
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(12, 31);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(340, 23);
            this.selectButton.TabIndex = 2;
            this.selectButton.Text = "Select Place";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // selectedText
            // 
            this.selectedText.AutoSize = true;
            this.selectedText.Location = new System.Drawing.Point(12, 57);
            this.selectedText.Name = "selectedText";
            this.selectedText.Size = new System.Drawing.Size(82, 13);
            this.selectedText.TabIndex = 3;
            this.selectedText.Text = "Selected Place:";
            // 
            // currentFile
            // 
            this.currentFile.AutoSize = true;
            this.currentFile.Location = new System.Drawing.Point(91, 57);
            this.currentFile.Name = "currentFile";
            this.currentFile.Size = new System.Drawing.Size(41, 13);
            this.currentFile.TabIndex = 4;
            this.currentFile.Text = "currFile";
            // 
            // sourceLink
            // 
            this.sourceLink.AutoSize = true;
            this.sourceLink.LinkColor = System.Drawing.Color.Black;
            this.sourceLink.Location = new System.Drawing.Point(12, 80);
            this.sourceLink.Name = "sourceLink";
            this.sourceLink.Size = new System.Drawing.Size(298, 13);
            this.sourceLink.TabIndex = 5;
            this.sourceLink.TabStop = true;
            this.sourceLink.Text = "Checkout the fully featured compressor and source on GitHub";
            this.sourceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sourceLink_LinkClicked);
            // 
            // PlaceCompressor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(364, 105);
            this.Controls.Add(this.sourceLink);
            this.Controls.Add(this.currentFile);
            this.Controls.Add(this.selectedText);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.processStatus);
            this.Controls.Add(this.statusLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlaceCompressor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RBLX Compressor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label processStatus;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Label selectedText;
        private System.Windows.Forms.Label currentFile;
        private System.Windows.Forms.LinkLabel sourceLink;
    }
