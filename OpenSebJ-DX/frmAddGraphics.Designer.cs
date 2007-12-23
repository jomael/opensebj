namespace OpenSebJ
{
    partial class frmAddGraphics
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
            // If the MDI Child is full screen when disposing it causes an issue where the
            // MDI window flickers, as if it doesn't know when to go full screen or normal.
            // Any way, seting this window to the Normal window state before disposing
            // the form, allows it to disposes correctly and all other windows to continue 
            // on as normal.
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddGraphics));
            this.cmbBrowseVideo = new System.Windows.Forms.Button();
            this.lblVideo = new System.Windows.Forms.Label();
            this.cmbBrowseImage = new System.Windows.Forms.Button();
            this.lblImage = new System.Windows.Forms.Label();
            this.openImage = new System.Windows.Forms.OpenFileDialog();
            this.openVideo = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cmbBrowseVideo
            // 
            this.cmbBrowseVideo.BackColor = System.Drawing.SystemColors.Control;
            this.cmbBrowseVideo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmbBrowseVideo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbBrowseVideo.Location = new System.Drawing.Point(563, 49);
            this.cmbBrowseVideo.Name = "cmbBrowseVideo";
            this.cmbBrowseVideo.Size = new System.Drawing.Size(56, 24);
            this.cmbBrowseVideo.TabIndex = 41;
            this.cmbBrowseVideo.Text = "Browse";
            this.cmbBrowseVideo.UseVisualStyleBackColor = false;
            this.cmbBrowseVideo.Click += new System.EventHandler(this.cmbBrowseVideo_Click);
            // 
            // lblVideo
            // 
            this.lblVideo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVideo.Location = new System.Drawing.Point(12, 51);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(545, 20);
            this.lblVideo.TabIndex = 39;
            this.lblVideo.Text = "Video :";
            this.lblVideo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBrowseImage
            // 
            this.cmbBrowseImage.BackColor = System.Drawing.SystemColors.Control;
            this.cmbBrowseImage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmbBrowseImage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbBrowseImage.Location = new System.Drawing.Point(563, 10);
            this.cmbBrowseImage.Name = "cmbBrowseImage";
            this.cmbBrowseImage.Size = new System.Drawing.Size(56, 24);
            this.cmbBrowseImage.TabIndex = 45;
            this.cmbBrowseImage.Text = "Browse";
            this.cmbBrowseImage.UseVisualStyleBackColor = false;
            this.cmbBrowseImage.Click += new System.EventHandler(this.cmbBrowseImage_Click);
            // 
            // lblImage
            // 
            this.lblImage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblImage.Location = new System.Drawing.Point(12, 13);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(545, 18);
            this.lblImage.TabIndex = 43;
            this.lblImage.Text = "Image :";
            this.lblImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openImage
            // 
            this.openImage.FileName = "openImage";
            this.openImage.Filter = "JPG|*.jpg|PNG|*.png|All Files|*.*";
            // 
            // openVideo
            // 
            this.openVideo.FileName = "openVideo";
            this.openVideo.Filter = "AVI|*.avi|WMV|*.wmv|MOV|*.mov|ASF|*.asf|All Files|*.*";
            // 
            // frmAddGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(632, 83);
            this.Controls.Add(this.cmbBrowseImage);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.cmbBrowseVideo);
            this.Controls.Add(this.lblVideo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddGraphics";
            this.Text = "Add Video / Graphics";
            this.Load += new System.EventHandler(this.frmAddGraphics_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmbBrowseVideo;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Button cmbBrowseImage;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.OpenFileDialog openImage;
        private System.Windows.Forms.OpenFileDialog openVideo;
    }
}