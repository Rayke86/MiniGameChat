namespace Client
{
    partial class Connect4
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
            this.buttonGiveUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGiveUp
            // 
            this.buttonGiveUp.Location = new System.Drawing.Point(870, 5);
            this.buttonGiveUp.Name = "buttonGiveUp";
            this.buttonGiveUp.Size = new System.Drawing.Size(75, 30);
            this.buttonGiveUp.TabIndex = 0;
            this.buttonGiveUp.Text = "Opgeven";
            this.buttonGiveUp.UseVisualStyleBackColor = true;
            this.buttonGiveUp.Click += new System.EventHandler(this.buttonGiveUp_Click);
            // 
            // Connect4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonGiveUp);
            this.Name = "Connect4";
            this.Size = new System.Drawing.Size(949, 640);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGiveUp;



    }
}
