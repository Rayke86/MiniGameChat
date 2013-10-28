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
            this.panelGame = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.labelSituation2 = new System.Windows.Forms.Label();
            this.labelSituation = new System.Windows.Forms.Label();
            this.buttonConnect4 = new System.Windows.Forms.Button();
            this.buttonRpsls = new System.Windows.Forms.Button();
            this.panelGame1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitcontainer)).BeginInit();
            this.splitcontainer.Panel1.SuspendLayout();
            this.splitcontainer.Panel2.SuspendLayout();
            this.splitcontainer.SuspendLayout();
            this.panelGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            // 
            // splitcontainer.Panel2
            // 
            this.splitcontainer.Panel2.Controls.Add(this.panelGame);
            this.splitcontainer.Size = new System.Drawing.Size(1284, 762);
            this.splitcontainer.SplitterDistance = 330;
            this.splitcontainer.TabIndex = 0;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(233, 502);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(98, 32);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Verzend";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textChat
            // 
            this.textChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textChat.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textChat.Location = new System.Drawing.Point(2, 502);
            this.textChat.Name = "textChat";
            this.textChat.Size = new System.Drawing.Size(229, 23);
            this.textChat.TabIndex = 0;
            this.textChat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textChat_KeyPress);
            // 
            // tabController
            // 
            this.tabController.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabController.Location = new System.Drawing.Point(2, 2);
            this.tabController.Name = "tabController";
            this.tabController.SelectedIndex = 0;
            this.tabController.Size = new System.Drawing.Size(329, 500);
            this.tabController.TabIndex = 0;
            this.tabController.SelectedIndexChanged += new System.EventHandler(this.tabController_SelectedIndexChanged);
            // 
            // panelGame
            // 
            this.panelGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGame.Controls.Add(this.splitContainer2);
            this.panelGame.Location = new System.Drawing.Point(1, 0);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(949, 762);
            this.panelGame.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.labelSituation2);
            this.splitContainer2.Panel1.Controls.Add(this.labelSituation);
            this.splitContainer2.Panel1.Controls.Add(this.buttonConnect4);
            this.splitContainer2.Panel1.Controls.Add(this.buttonRpsls);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelGame1);
            this.splitContainer2.Size = new System.Drawing.Size(949, 762);
            this.splitContainer2.SplitterDistance = 105;
            this.splitContainer2.TabIndex = 0;
            // 
            // labelSituation2
            // 
            this.labelSituation2.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSituation2.ForeColor = System.Drawing.Color.Red;
            this.labelSituation2.Location = new System.Drawing.Point(506, 63);
            this.labelSituation2.Name = "labelSituation2";
            this.labelSituation2.Size = new System.Drawing.Size(307, 29);
            this.labelSituation2.TabIndex = 3;
            // 
            // labelSituation
            // 
            this.labelSituation.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSituation.ForeColor = System.Drawing.Color.Red;
            this.labelSituation.Location = new System.Drawing.Point(506, 21);
            this.labelSituation.Name = "labelSituation";
            this.labelSituation.Size = new System.Drawing.Size(307, 29);
            this.labelSituation.TabIndex = 2;
            // 
            // buttonConnect4
            // 
            this.buttonConnect4.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect4.Location = new System.Drawing.Point(246, 12);
            this.buttonConnect4.Name = "buttonConnect4";
            this.buttonConnect4.Size = new System.Drawing.Size(200, 80);
            this.buttonConnect4.TabIndex = 1;
            this.buttonConnect4.Text = "Connect 4";
            this.buttonConnect4.UseVisualStyleBackColor = true;
            this.buttonConnect4.Click += new System.EventHandler(this.buttonConnect4_Click);
            // 
            // buttonRpsls
            // 
            this.buttonRpsls.Font = new System.Drawing.Font("Consolas", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRpsls.Location = new System.Drawing.Point(20, 12);
            this.buttonRpsls.Name = "buttonRpsls";
            this.buttonRpsls.Size = new System.Drawing.Size(200, 80);
            this.buttonRpsls.TabIndex = 0;
            this.buttonRpsls.UseVisualStyleBackColor = true;
            this.buttonRpsls.Click += new System.EventHandler(this.buttonRpsls_Click);
            // 
            // panelGame1
            // 
            this.panelGame1.Location = new System.Drawing.Point(1, 1);
            this.panelGame1.Name = "panelGame1";
            this.panelGame1.Size = new System.Drawing.Size(945, 649);
            this.panelGame1.TabIndex = 0;
            // 
            // ClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 762);
            this.Controls.Add(this.splitcontainer);
            this.Name = "ClientGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientGUI_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientGUI_FormClosed);
            this.splitcontainer.Panel1.ResumeLayout(false);
            this.splitcontainer.Panel1.PerformLayout();
            this.splitcontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitcontainer)).EndInit();
            this.splitcontainer.ResumeLayout(false);
            this.panelGame.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitcontainer;
        private System.Windows.Forms.TabControl tabController;
        private System.Windows.Forms.TextBox textChat;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelGame1;
        private System.Windows.Forms.Button buttonConnect4;
        private System.Windows.Forms.Button buttonRpsls;
        private System.Windows.Forms.Label labelSituation;
        private System.Windows.Forms.Label labelSituation2;
    }
}

