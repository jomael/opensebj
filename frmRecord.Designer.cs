/* 
 * OpenSebJ
 * Copyright (C) 2006  Sebastian Gray - sebastiangray@gmail.com 
 * Website: http://www.evolvingsoftware.com/opensebj.html
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
*/


namespace OpenSebJ
{
    partial class frmRecord
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
            try
            {
                if (_rec != null)
                {
                    _rec.StopRecording();
                }
            }
            catch
            {
                // Incase the recording has't been initiated
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecord));
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFile = new System.Windows.Forms.Label();
            this.cmbSelectFile = new System.Windows.Forms.Button();
            this.cmbRecord = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveDialog
            // 
            this.saveDialog.DefaultExt = "*.wav";
            this.saveDialog.Filter = "Wave Files (*.wav)|*.wav|All Files (*.*)|*.*";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.lblFile);
            this.flowLayoutPanel1.Controls.Add(this.cmbSelectFile);
            this.flowLayoutPanel1.Controls.Add(this.cmbRecord);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(665, 86);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFile.Location = new System.Drawing.Point(3, 0);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(0, 13);
            this.lblFile.TabIndex = 7;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSelectFile
            // 
            this.cmbSelectFile.Location = new System.Drawing.Point(9, 3);
            this.cmbSelectFile.Name = "cmbSelectFile";
            this.cmbSelectFile.Size = new System.Drawing.Size(77, 23);
            this.cmbSelectFile.TabIndex = 4;
            this.cmbSelectFile.Text = "Select File";
            this.cmbSelectFile.UseVisualStyleBackColor = true;
            this.cmbSelectFile.Click += new System.EventHandler(this.cmbSelectFile_Click);
            // 
            // cmbRecord
            // 
            this.cmbRecord.Enabled = false;
            this.cmbRecord.Location = new System.Drawing.Point(92, 3);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(77, 23);
            this.cmbRecord.TabIndex = 3;
            this.cmbRecord.Text = "Record";
            this.cmbRecord.UseVisualStyleBackColor = true;
            this.cmbRecord.Click += new System.EventHandler(this.cmbRecord_Click);
            // 
            // frmRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(665, 86);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecord";
            this.Text = "Record";
            this.Load += new System.EventHandler(this.frmRecord_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cmbSelectFile;
        private System.Windows.Forms.Button cmbRecord;
        private System.Windows.Forms.Label lblFile;
    }
}