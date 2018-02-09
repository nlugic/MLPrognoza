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
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.pbLearning = new System.Windows.Forms.ProgressBar();
            this.btnEditLayers = new System.Windows.Forms.Button();
            this.btnStartLearning = new System.Windows.Forms.Button();
            this.nudHiddenLayers = new System.Windows.Forms.NumericUpDown();
            this.lblHiddenLayers = new System.Windows.Forms.Label();
            this.nudSigmoidValue = new System.Windows.Forms.NumericUpDown();
            this.lblIterations = new System.Windows.Forms.Label();
            this.nudIterations = new System.Windows.Forms.NumericUpDown();
            this.lblSigmoidValue = new System.Windows.Forms.Label();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rbBernoulli = new System.Windows.Forms.RadioButton();
            this.rbGaussian = new System.Windows.Forms.RadioButton();
            this.gbDataChart = new System.Windows.Forms.GroupBox();
            this.gbLearningData = new System.Windows.Forms.GroupBox();
            this.lblLearninngData = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearEnd)).BeginInit();
            this.gbDataGathering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYearStart)).BeginInit();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHiddenLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSigmoidValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).BeginInit();
            this.gbFunction.SuspendLayout();
            this.gbLearningData.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Enabled = false;
            this.btnStartDownload.Location = new System.Drawing.Point(75, 80);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(120, 30);
            this.btnStartDownload.TabIndex = 0;
            this.btnStartDownload.Text = "Preuzmi podatke";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // lblYearRange
            // 
            this.lblYearRange.AutoSize = true;
            this.lblYearRange.Location = new System.Drawing.Point(45, 54);
            this.lblYearRange.Name = "lblYearRange";
            this.lblYearRange.Size = new System.Drawing.Size(40, 13);
            this.lblYearRange.TabIndex = 2;
            this.lblYearRange.Text = "Period:";
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
            this.lblLocation.Size = new System.Drawing.Size(50, 13);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "Lokacija:";
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
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.pbLearning);
            this.gbSettings.Controls.Add(this.btnEditLayers);
            this.gbSettings.Controls.Add(this.btnStartLearning);
            this.gbSettings.Controls.Add(this.nudHiddenLayers);
            this.gbSettings.Controls.Add(this.lblHiddenLayers);
            this.gbSettings.Controls.Add(this.nudSigmoidValue);
            this.gbSettings.Controls.Add(this.lblIterations);
            this.gbSettings.Controls.Add(this.nudIterations);
            this.gbSettings.Controls.Add(this.lblSigmoidValue);
            this.gbSettings.Controls.Add(this.gbFunction);
            this.gbSettings.Enabled = false;
            this.gbSettings.Location = new System.Drawing.Point(10, 155);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(270, 235);
            this.gbSettings.TabIndex = 9;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Podesavanja";
            // 
            // pbLearning
            // 
            this.pbLearning.Location = new System.Drawing.Point(20, 210);
            this.pbLearning.Name = "pbLearning";
            this.pbLearning.Size = new System.Drawing.Size(230, 15);
            this.pbLearning.TabIndex = 12;
            // 
            // btnEditLayers
            // 
            this.btnEditLayers.Location = new System.Drawing.Point(20, 170);
            this.btnEditLayers.Name = "btnEditLayers";
            this.btnEditLayers.Size = new System.Drawing.Size(110, 30);
            this.btnEditLayers.TabIndex = 11;
            this.btnEditLayers.Text = "Slojevi mreze...";
            this.btnEditLayers.UseVisualStyleBackColor = true;
            this.btnEditLayers.Click += new System.EventHandler(this.btnEditLayers_Click);
            // 
            // btnStartLearning
            // 
            this.btnStartLearning.Location = new System.Drawing.Point(140, 170);
            this.btnStartLearning.Name = "btnStartLearning";
            this.btnStartLearning.Size = new System.Drawing.Size(110, 30);
            this.btnStartLearning.TabIndex = 10;
            this.btnStartLearning.Text = "Zapocni ucenje";
            this.btnStartLearning.UseVisualStyleBackColor = true;
            this.btnStartLearning.Click += new System.EventHandler(this.btnStartLearning_Click);
            // 
            // nudHiddenLayers
            // 
            this.nudHiddenLayers.Location = new System.Drawing.Point(175, 140);
            this.nudHiddenLayers.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudHiddenLayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHiddenLayers.Name = "nudHiddenLayers";
            this.nudHiddenLayers.Size = new System.Drawing.Size(75, 20);
            this.nudHiddenLayers.TabIndex = 9;
            this.nudHiddenLayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudHiddenLayers.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudHiddenLayers.ValueChanged += new System.EventHandler(this.nudHiddenLayers_ValueChanged);
            // 
            // lblHiddenLayers
            // 
            this.lblHiddenLayers.AutoSize = true;
            this.lblHiddenLayers.Location = new System.Drawing.Point(22, 142);
            this.lblHiddenLayers.Name = "lblHiddenLayers";
            this.lblHiddenLayers.Size = new System.Drawing.Size(140, 13);
            this.lblHiddenLayers.TabIndex = 8;
            this.lblHiddenLayers.Text = "Broj skrivenih slojeva mreze:";
            // 
            // nudSigmoidValue
            // 
            this.nudSigmoidValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSigmoidValue.Location = new System.Drawing.Point(175, 110);
            this.nudSigmoidValue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSigmoidValue.Name = "nudSigmoidValue";
            this.nudSigmoidValue.Size = new System.Drawing.Size(75, 20);
            this.nudSigmoidValue.TabIndex = 7;
            this.nudSigmoidValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSigmoidValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblIterations
            // 
            this.lblIterations.AutoSize = true;
            this.lblIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIterations.Location = new System.Drawing.Point(150, 40);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(93, 18);
            this.lblIterations.TabIndex = 6;
            this.lblIterations.Text = "Broj iteracija:";
            // 
            // nudIterations
            // 
            this.nudIterations.Location = new System.Drawing.Point(155, 70);
            this.nudIterations.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudIterations.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudIterations.Name = "nudIterations";
            this.nudIterations.Size = new System.Drawing.Size(75, 20);
            this.nudIterations.TabIndex = 5;
            this.nudIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudIterations.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // lblSigmoidValue
            // 
            this.lblSigmoidValue.AutoSize = true;
            this.lblSigmoidValue.Location = new System.Drawing.Point(20, 112);
            this.lblSigmoidValue.Name = "lblSigmoidValue";
            this.lblSigmoidValue.Size = new System.Drawing.Size(142, 13);
            this.lblSigmoidValue.TabIndex = 4;
            this.lblSigmoidValue.Text = "Vrednost sigmoidne funkcije:";
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.rbBernoulli);
            this.gbFunction.Controls.Add(this.rbGaussian);
            this.gbFunction.Location = new System.Drawing.Point(10, 30);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(115, 70);
            this.gbFunction.TabIndex = 2;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Funkcija raspodele";
            // 
            // rbBernoulli
            // 
            this.rbBernoulli.AutoSize = true;
            this.rbBernoulli.Checked = true;
            this.rbBernoulli.Location = new System.Drawing.Point(10, 20);
            this.rbBernoulli.Name = "rbBernoulli";
            this.rbBernoulli.Size = new System.Drawing.Size(77, 17);
            this.rbBernoulli.TabIndex = 0;
            this.rbBernoulli.TabStop = true;
            this.rbBernoulli.Text = "Bernulijeva";
            this.rbBernoulli.UseVisualStyleBackColor = true;
            // 
            // rbGaussian
            // 
            this.rbGaussian.AutoSize = true;
            this.rbGaussian.Location = new System.Drawing.Point(10, 43);
            this.rbGaussian.Name = "rbGaussian";
            this.rbGaussian.Size = new System.Drawing.Size(68, 17);
            this.rbGaussian.TabIndex = 1;
            this.rbGaussian.Text = "Gausova";
            this.rbGaussian.UseVisualStyleBackColor = true;
            // 
            // gbDataChart
            // 
            this.gbDataChart.Location = new System.Drawing.Point(290, 10);
            this.gbDataChart.Name = "gbDataChart";
            this.gbDataChart.Size = new System.Drawing.Size(480, 300);
            this.gbDataChart.TabIndex = 10;
            this.gbDataChart.TabStop = false;
            this.gbDataChart.Text = "Data visualization";
            this.gbDataChart.Enter += new System.EventHandler(this.gbDataChart_Enter);
            // 
            // gbLearningData
            // 
            this.gbLearningData.Controls.Add(this.lblLearninngData);
            this.gbLearningData.Location = new System.Drawing.Point(290, 315);
            this.gbLearningData.Name = "gbLearningData";
            this.gbLearningData.Size = new System.Drawing.Size(480, 75);
            this.gbLearningData.TabIndex = 11;
            this.gbLearningData.TabStop = false;
            this.gbLearningData.Text = "Podaci o ucenju";
            // 
            // lblLearninngData
            // 
            this.lblLearninngData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLearninngData.Location = new System.Drawing.Point(30, 20);
            this.lblLearninngData.Name = "lblLearninngData";
            this.lblLearninngData.Size = new System.Drawing.Size(420, 45);
            this.lblLearninngData.TabIndex = 0;
            this.lblLearninngData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formGlavna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 402);
            this.Controls.Add(this.gbLearningData);
            this.Controls.Add(this.gbDataChart);
            this.Controls.Add(this.gbSettings);
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
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHiddenLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSigmoidValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).EndInit();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            this.gbLearningData.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rbBernoulli;
        private System.Windows.Forms.RadioButton rbGaussian;
        private System.Windows.Forms.NumericUpDown nudHiddenLayers;
        private System.Windows.Forms.Label lblHiddenLayers;
        private System.Windows.Forms.NumericUpDown nudSigmoidValue;
        private System.Windows.Forms.Label lblIterations;
        private System.Windows.Forms.NumericUpDown nudIterations;
        private System.Windows.Forms.Label lblSigmoidValue;
        private System.Windows.Forms.Button btnEditLayers;
        private System.Windows.Forms.Button btnStartLearning;
        private System.Windows.Forms.GroupBox gbDataChart;
        private System.Windows.Forms.ProgressBar pbLearning;
        private System.Windows.Forms.GroupBox gbLearningData;
        private System.Windows.Forms.Label lblLearninngData;
    }
}

