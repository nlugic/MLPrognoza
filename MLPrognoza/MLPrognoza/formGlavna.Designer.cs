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
            this.lblYearRange = new System.Windows.Forms.Label();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.nudYearEnd = new System.Windows.Forms.NumericUpDown();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.gbDataGathering = new System.Windows.Forms.GroupBox();
            this.nudYearStart = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearEnd)).BeginInit();
            this.gbDataGathering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearStart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Enabled = false;
            this.btnStartDownload.Location = new System.Drawing.Point(75, 80);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(120, 30);
            this.btnStartDownload.TabIndex = 0;
            this.btnStartDownload.Text = "Get weather data";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // lblYearRange
            // 
            this.lblYearRange.AutoSize = true;
            this.lblYearRange.Location = new System.Drawing.Point(19, 54);
            this.lblYearRange.Name = "lblYearRange";
            this.lblYearRange.Size = new System.Drawing.Size(67, 13);
            this.lblYearRange.TabIndex = 2;
            this.lblYearRange.Text = "Year Range:";
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(20, 115);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(230, 15);
            this.pbDownload.TabIndex = 3;
            // 
            // nudYearEnd
            // 
            this.nudYearEnd.Enabled = false;
            this.nudYearEnd.Location = new System.Drawing.Point(200, 52);
            this.nudYearEnd.Maximum = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.nudYearEnd.Minimum = new decimal(new int[] {
            1905,
            0,
            0,
            0});
            this.nudYearEnd.Name = "nudYearEnd";
            this.nudYearEnd.Size = new System.Drawing.Size(60, 20);
            this.nudYearEnd.TabIndex = 4;
            this.nudYearEnd.Value = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.nudYearEnd.ValueChanged += new System.EventHandler(this.nudYearEnd_ValueChanged);
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(100, 19);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(160, 21);
            this.cbLocation.TabIndex = 5;
            this.cbLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocation_SelectedIndexChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(35, 22);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(51, 13);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Location:";
            // 
            // gbDataGathering
            // 
            this.gbDataGathering.Controls.Add(this.nudYearStart);
            this.gbDataGathering.Controls.Add(this.cbLocation);
            this.gbDataGathering.Controls.Add(this.pbDownload);
            this.gbDataGathering.Controls.Add(this.nudYearEnd);
            this.gbDataGathering.Controls.Add(this.btnStartDownload);
            this.gbDataGathering.Controls.Add(this.lblYearRange);
            this.gbDataGathering.Controls.Add(this.lblLocation);
            this.gbDataGathering.Location = new System.Drawing.Point(10, 10);
            this.gbDataGathering.Name = "gbDataGathering";
            this.gbDataGathering.Size = new System.Drawing.Size(270, 140);
            this.gbDataGathering.TabIndex = 8;
            this.gbDataGathering.TabStop = false;
            this.gbDataGathering.Text = "Data gathering";
            // 
            // nudYearStart
            // 
            this.nudYearStart.Enabled = false;
            this.nudYearStart.Location = new System.Drawing.Point(100, 52);
            this.nudYearStart.Maximum = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.nudYearStart.Minimum = new decimal(new int[] {
            1905,
            0,
            0,
            0});
            this.nudYearStart.Name = "nudYearStart";
            this.nudYearStart.Size = new System.Drawing.Size(60, 20);
            this.nudYearStart.TabIndex = 8;
            this.nudYearStart.Value = new decimal(new int[] {
            1905,
            0,
            0,
            0});
            this.nudYearStart.ValueChanged += new System.EventHandler(this.nudYearStart_ValueChanged);
            // 
            // formGlavna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.gbDataGathering);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formGlavna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vremenska Prognoza";
            this.Load += new System.EventHandler(this.formGlavna_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudYearEnd)).EndInit();
            this.gbDataGathering.ResumeLayout(false);
            this.gbDataGathering.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Label lblYearRange;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.NumericUpDown nudYearEnd;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.GroupBox gbDataGathering;
        private System.Windows.Forms.NumericUpDown nudYearStart;
    }
}

