namespace MLPrognoza
{
    partial class formSlojeviMreze
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
            this.lblLayersEdit = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLayersEdit
            // 
            this.lblLayersEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayersEdit.Location = new System.Drawing.Point(0, 0);
            this.lblLayersEdit.Name = "lblLayersEdit";
            this.lblLayersEdit.Size = new System.Drawing.Size(260, 30);
            this.lblLayersEdit.TabIndex = 0;
            this.lblLayersEdit.Text = "Broj cvorova u slojevima";
            this.lblLayersEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(70, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Sacuvaj";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // formSlojeviMreze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 102);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblLayersEdit);
            this.Name = "formSlojeviMreze";
            this.Text = "Slojevi mreze";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formSlojeviMreze_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLayersEdit;
        private System.Windows.Forms.Button btnSave;
    }
}