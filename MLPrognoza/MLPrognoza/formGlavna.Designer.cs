namespace MLPrognoza
{
    partial class formGlavna
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
            this.btnStartDownload = new System.Windows.Forms.Button();
            this.lblYear = new System.Windows.Forms.Label();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Location = new System.Drawing.Point(50, 73);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(119, 23);
            this.btnStartDownload.TabIndex = 0;
            this.btnStartDownload.Text = "Download files";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(30, 49);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(65, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Select Year:";
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(59, 102);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(100, 23);
            this.pbDownload.TabIndex = 3;
            // 
            // nudYear
            // 
            this.nudYear.Location = new System.Drawing.Point(115, 47);
            this.nudYear.Maximum = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            1905,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(63, 20);
            this.nudYear.TabIndex = 4;
            this.nudYear.Value = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            // 
            // formGlavna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.nudYear);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.btnStartDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formGlavna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vremenska Prognoza";
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.NumericUpDown nudYear;
    }
}

