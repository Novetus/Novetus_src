
namespace NovetusLauncher
{
    partial class ServerBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerBrowser));
            this.JoinGameButton = new System.Windows.Forms.Button();
            this.MasterServerBox = new System.Windows.Forms.TextBox();
            this.MasterServerLabel = new System.Windows.Forms.Label();
            this.MasterServerRefreshButton = new System.Windows.Forms.Button();
            this.ServerListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // JoinGameButton
            // 
            this.JoinGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JoinGameButton.Location = new System.Drawing.Point(495, 8);
            this.JoinGameButton.Name = "JoinGameButton";
            this.JoinGameButton.Size = new System.Drawing.Size(75, 23);
            this.JoinGameButton.TabIndex = 0;
            this.JoinGameButton.Text = "JOIN GAME";
            this.JoinGameButton.UseVisualStyleBackColor = true;
            this.JoinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
            // 
            // MasterServerBox
            // 
            this.MasterServerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MasterServerBox.Location = new System.Drawing.Point(135, 10);
            this.MasterServerBox.Name = "MasterServerBox";
            this.MasterServerBox.Size = new System.Drawing.Size(323, 20);
            this.MasterServerBox.TabIndex = 1;
            this.MasterServerBox.TextChanged += new System.EventHandler(this.MasterServerBox_TextChanged);
            // 
            // MasterServerLabel
            // 
            this.MasterServerLabel.AutoSize = true;
            this.MasterServerLabel.Location = new System.Drawing.Point(12, 13);
            this.MasterServerLabel.Name = "MasterServerLabel";
            this.MasterServerLabel.Size = new System.Drawing.Size(117, 13);
            this.MasterServerLabel.TabIndex = 2;
            this.MasterServerLabel.Text = "Master Server Address:";
            // 
            // MasterServerRefreshButton
            // 
            this.MasterServerRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MasterServerRefreshButton.AutoEllipsis = true;
            this.MasterServerRefreshButton.Image = global::NovetusLauncher.Properties.Resources.refresh;
            this.MasterServerRefreshButton.Location = new System.Drawing.Point(464, 8);
            this.MasterServerRefreshButton.Name = "MasterServerRefreshButton";
            this.MasterServerRefreshButton.Size = new System.Drawing.Size(25, 23);
            this.MasterServerRefreshButton.TabIndex = 3;
            this.MasterServerRefreshButton.UseVisualStyleBackColor = true;
            this.MasterServerRefreshButton.Click += new System.EventHandler(this.MasterServerRefreshButton_Click);
            // 
            // ServerListView
            // 
            this.ServerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerListView.HideSelection = false;
            this.ServerListView.Location = new System.Drawing.Point(10, 37);
            this.ServerListView.Name = "ServerListView";
            this.ServerListView.Size = new System.Drawing.Size(555, 408);
            this.ServerListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ServerListView.TabIndex = 4;
            this.ServerListView.UseCompatibleStateImageBehavior = false;
            this.ServerListView.View = System.Windows.Forms.View.Details;
            this.ServerListView.SelectedIndexChanged += new System.EventHandler(this.ServerListView_SelectedIndexChanged);
            // 
            // ServerBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(577, 456);
            this.Controls.Add(this.ServerListView);
            this.Controls.Add(this.MasterServerRefreshButton);
            this.Controls.Add(this.MasterServerLabel);
            this.Controls.Add(this.MasterServerBox);
            this.Controls.Add(this.JoinGameButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(593, 495);
            this.Name = "ServerBrowser";
            this.Text = "Server Browser";
            this.Load += new System.EventHandler(this.ServerBrowser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button JoinGameButton;
        private System.Windows.Forms.TextBox MasterServerBox;
        private System.Windows.Forms.Label MasterServerLabel;
        private System.Windows.Forms.Button MasterServerRefreshButton;
        private System.Windows.Forms.ListView ServerListView;
    }
}