namespace BlockFitter
{
    partial class Form1
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
            this.pbxResult = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxResult
            // 
            this.pbxResult.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxResult.Location = new System.Drawing.Point(0, 0);
            this.pbxResult.Name = "pbxResult";
            this.pbxResult.Size = new System.Drawing.Size(1204, 685);
            this.pbxResult.TabIndex = 0;
            this.pbxResult.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 685);
            this.Controls.Add(this.pbxResult);
            this.Name = "Form1";
            this.Text = "Block Fitter";
            ((System.ComponentModel.ISupportInitialize)(this.pbxResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxResult;
    }
}

