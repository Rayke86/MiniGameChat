namespace Client
{
    partial class ClientGUI
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
            this.splitcontainer = new System.Windows.Forms.SplitContainer();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textChat = new System.Windows.Forms.TextBox();
            this.tabController = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitcontainer)).BeginInit();
            this.splitcontainer.Panel1.SuspendLayout();
            this.splitcontainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitcontainer
            // 
            this.splitcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitcontainer.Location = new System.Drawing.Point(0, 0);
            this.splitcontainer.Name = "splitcontainer";
            // 
            // splitcontainer.Panel1
            // 
            this.splitcontainer.Panel1.Controls.Add(this.buttonSend);
            this.splitcontainer.Panel1.Controls.Add(this.textChat);
            this.splitcontainer.Panel1.Controls.Add(this.tabController);
            this.splitcontainer.Size = new System.Drawing.Size(1284, 762);
            this.splitcontainer.SplitterDistance = 331;
            this.splitcontainer.TabIndex = 0;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(234, 502);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(98, 40);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Verzend";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textChat
            // 
            this.textChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textChat.Location = new System.Drawing.Point(2, 502);
            this.textChat.Multiline = true;
            this.textChat.Name = "textChat";
            this.textChat.Size = new System.Drawing.Size(230, 40);
            this.textChat.TabIndex = 0;
            // 
            // tabController
            // 
            this.tabController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabController.Location = new System.Drawing.Point(2, 2);
            this.tabController.Name = "tabController";
            this.tabController.SelectedIndex = 0;
            this.tabController.Size = new System.Drawing.Size(330, 500);
            this.tabController.TabIndex = 0;
            // 
            // ClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 762);
            this.Controls.Add(this.splitcontainer);
            this.Name = "ClientGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientGUI_FormClosed);
            this.splitcontainer.Panel1.ResumeLayout(false);
            this.splitcontainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitcontainer)).EndInit();
            this.splitcontainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitcontainer;
        private System.Windows.Forms.TabControl tabController;
        private System.Windows.Forms.TextBox textChat;
        private System.Windows.Forms.Button buttonSend;
    }
}

