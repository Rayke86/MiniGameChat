namespace Client
{
    partial class RPSLS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRock = new System.Windows.Forms.Button();
            this.buttonLizard = new System.Windows.Forms.Button();
            this.buttonSiccors = new System.Windows.Forms.Button();
            this.buttonSpock = new System.Windows.Forms.Button();
            this.buttonPaper = new System.Windows.Forms.Button();
            this.labelSmash = new System.Windows.Forms.Label();
            this.labelCut = new System.Windows.Forms.Label();
            this.labelpoisons = new System.Windows.Forms.Label();
            this.labelCovers = new System.Windows.Forms.Label();
            this.labelCrushes = new System.Windows.Forms.Label();
            this.labelCenter = new System.Windows.Forms.Label();
            this.buttonGiveUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRock
            // 
            this.buttonRock.Location = new System.Drawing.Point(414, 3);
            this.buttonRock.Name = "buttonRock";
            this.buttonRock.Size = new System.Drawing.Size(150, 96);
            this.buttonRock.TabIndex = 0;
            this.buttonRock.UseVisualStyleBackColor = true;
            this.buttonRock.Click += new System.EventHandler(this.buttonRock_Click);
            // 
            // buttonLizard
            // 
            this.buttonLizard.Location = new System.Drawing.Point(88, 170);
            this.buttonLizard.Name = "buttonLizard";
            this.buttonLizard.Size = new System.Drawing.Size(200, 50);
            this.buttonLizard.TabIndex = 1;
            this.buttonLizard.UseVisualStyleBackColor = true;
            this.buttonLizard.Click += new System.EventHandler(this.buttonLizard_Click);
            // 
            // buttonSiccors
            // 
            this.buttonSiccors.Location = new System.Drawing.Point(555, 432);
            this.buttonSiccors.Name = "buttonSiccors";
            this.buttonSiccors.Size = new System.Drawing.Size(200, 85);
            this.buttonSiccors.TabIndex = 2;
            this.buttonSiccors.UseVisualStyleBackColor = true;
            this.buttonSiccors.Click += new System.EventHandler(this.buttonSiccors_Click);
            // 
            // buttonSpock
            // 
            this.buttonSpock.Location = new System.Drawing.Point(193, 390);
            this.buttonSpock.Name = "buttonSpock";
            this.buttonSpock.Size = new System.Drawing.Size(200, 168);
            this.buttonSpock.TabIndex = 3;
            this.buttonSpock.UseVisualStyleBackColor = true;
            this.buttonSpock.Click += new System.EventHandler(this.buttonSpock_Click);
            // 
            // buttonPaper
            // 
            this.buttonPaper.Location = new System.Drawing.Point(711, 130);
            this.buttonPaper.Name = "buttonPaper";
            this.buttonPaper.Size = new System.Drawing.Size(100, 130);
            this.buttonPaper.TabIndex = 4;
            this.buttonPaper.UseVisualStyleBackColor = true;
            this.buttonPaper.Click += new System.EventHandler(this.buttonPaper_Click);
            // 
            // labelSmash
            // 
            this.labelSmash.Location = new System.Drawing.Point(399, 432);
            this.labelSmash.Name = "labelSmash";
            this.labelSmash.Size = new System.Drawing.Size(150, 50);
            this.labelSmash.TabIndex = 5;
            // 
            // labelCut
            // 
            this.labelCut.Location = new System.Drawing.Point(708, 263);
            this.labelCut.Name = "labelCut";
            this.labelCut.Size = new System.Drawing.Size(50, 150);
            this.labelCut.TabIndex = 6;
            // 
            // labelpoisons
            // 
            this.labelpoisons.Location = new System.Drawing.Point(238, 237);
            this.labelpoisons.Name = "labelpoisons";
            this.labelpoisons.Size = new System.Drawing.Size(50, 150);
            this.labelpoisons.TabIndex = 7;
            // 
            // labelCovers
            // 
            this.labelCovers.Location = new System.Drawing.Point(594, 17);
            this.labelCovers.Name = "labelCovers";
            this.labelCovers.Size = new System.Drawing.Size(150, 100);
            this.labelCovers.TabIndex = 8;
            // 
            // labelCrushes
            // 
            this.labelCrushes.Location = new System.Drawing.Point(238, 54);
            this.labelCrushes.Name = "labelCrushes";
            this.labelCrushes.Size = new System.Drawing.Size(150, 100);
            this.labelCrushes.TabIndex = 9;
            // 
            // labelCenter
            // 
            this.labelCenter.Location = new System.Drawing.Point(294, 102);
            this.labelCenter.Name = "labelCenter";
            this.labelCenter.Size = new System.Drawing.Size(410, 285);
            this.labelCenter.TabIndex = 10;
            // 
            // buttonGiveUp
            // 
            this.buttonGiveUp.Location = new System.Drawing.Point(870, 5);
            this.buttonGiveUp.Name = "buttonGiveUp";
            this.buttonGiveUp.Size = new System.Drawing.Size(75, 30);
            this.buttonGiveUp.TabIndex = 11;
            this.buttonGiveUp.Text = "Opgeven";
            this.buttonGiveUp.UseVisualStyleBackColor = true;
            this.buttonGiveUp.Click += new System.EventHandler(this.buttonGiveUp_Click);
            // 
            // RPSLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonGiveUp);
            this.Controls.Add(this.labelCrushes);
            this.Controls.Add(this.labelCovers);
            this.Controls.Add(this.labelpoisons);
            this.Controls.Add(this.labelCut);
            this.Controls.Add(this.labelSmash);
            this.Controls.Add(this.buttonPaper);
            this.Controls.Add(this.buttonSpock);
            this.Controls.Add(this.buttonSiccors);
            this.Controls.Add(this.buttonLizard);
            this.Controls.Add(this.buttonRock);
            this.Controls.Add(this.labelCenter);
            this.Name = "RPSLS";
            this.Size = new System.Drawing.Size(949, 600);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRock;
        private System.Windows.Forms.Button buttonLizard;
        private System.Windows.Forms.Button buttonSiccors;
        private System.Windows.Forms.Button buttonSpock;
        private System.Windows.Forms.Button buttonPaper;
        private System.Windows.Forms.Label labelSmash;
        private System.Windows.Forms.Label labelCut;
        private System.Windows.Forms.Label labelpoisons;
        private System.Windows.Forms.Label labelCovers;
        private System.Windows.Forms.Label labelCrushes;
        private System.Windows.Forms.Label labelCenter;
        private System.Windows.Forms.Button buttonGiveUp;
    }
}
