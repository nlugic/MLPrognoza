namespace MLPrognoza
{
    partial class EditorSloja
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
            this.lblLayer = new System.Windows.Forms.Label();
            this.nudLayerNodes = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayerNodes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLayer
            // 
            this.lblLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayer.Location = new System.Drawing.Point(0, 0);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(60, 25);
            this.lblLayer.TabIndex = 0;
            this.lblLayer.Text = "Sloj ";
            this.lblLayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudLayerNodes
            // 
            this.nudLayerNodes.Location = new System.Drawing.Point(75, 5);
            this.nudLayerNodes.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLayerNodes.Name = "nudLayerNodes";
            this.nudLayerNodes.Size = new System.Drawing.Size(60, 20);
            this.nudLayerNodes.TabIndex = 1;
            this.nudLayerNodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLayerNodes.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // EditorSloja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudLayerNodes);
            this.Controls.Add(this.lblLayer);
            this.Name = "EditorSloja";
            this.Size = new System.Drawing.Size(140, 30);
            ((System.ComponentModel.ISupportInitialize)(this.nudLayerNodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.NumericUpDown nudLayerNodes;
    }
}
