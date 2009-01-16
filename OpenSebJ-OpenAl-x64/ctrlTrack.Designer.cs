namespace OpenSebJ
{
    partial class ctrlTrack
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
            this.cmbAdd = new System.Windows.Forms.Button();
            this.lblTrack = new System.Windows.Forms.Label();
            this.cmbSettings = new System.Windows.Forms.Button();
            this.loadSamples = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cmbAdd
            // 
            this.cmbAdd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmbAdd.Location = new System.Drawing.Point(0, 25);
            this.cmbAdd.Name = "cmbAdd";
            this.cmbAdd.Size = new System.Drawing.Size(100, 25);
            this.cmbAdd.TabIndex = 0;
            this.cmbAdd.Text = "Add";
            this.cmbAdd.UseVisualStyleBackColor = false;
            this.cmbAdd.Click += new System.EventHandler(this.cmbAdd_Click);
            // 
            // lblTrack
            // 
            this.lblTrack.BackColor = System.Drawing.SystemColors.Window;
            this.lblTrack.Location = new System.Drawing.Point(0, 0);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(100, 25);
            this.lblTrack.TabIndex = 1;
            this.lblTrack.Text = "Sample";
            this.lblTrack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSettings
            // 
            this.cmbSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmbSettings.Location = new System.Drawing.Point(0, 50);
            this.cmbSettings.Name = "cmbSettings";
            this.cmbSettings.Size = new System.Drawing.Size(100, 25);
            this.cmbSettings.TabIndex = 2;
            this.cmbSettings.Text = "Settings";
            this.cmbSettings.UseVisualStyleBackColor = false;
            this.cmbSettings.Click += new System.EventHandler(this.cmbSettings_Click);
            // 
            // loadSamples
            // 
            this.loadSamples.Filter = "Wave Files|*.wav|All Files|*.*";
            // 
            // ctrlTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cmbSettings);
            this.Controls.Add(this.lblTrack);
            this.Controls.Add(this.cmbAdd);
            this.Name = "ctrlTrack";
            this.Size = new System.Drawing.Size(876, 100);
            this.Load += new System.EventHandler(this.ctrlTrack_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmbAdd;
        public System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.Button cmbSettings;
        private System.Windows.Forms.OpenFileDialog loadSamples;
    }
}
