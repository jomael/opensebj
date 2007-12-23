/* 
 * OpenSebJ
 * Copyright (C) 2007 Sebastian Gray - sebastiangray@gmail.com 
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
    partial class frmSaveExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveExplorer));
            this.cmbOutput = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSaveExplorer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbOutput
            // 
            this.cmbOutput.Location = new System.Drawing.Point(161, 82);
            this.cmbOutput.Name = "cmbOutput";
            this.cmbOutput.Size = new System.Drawing.Size(102, 23);
            this.cmbOutput.TabIndex = 2;
            this.cmbOutput.Text = "Select Location";
            this.cmbOutput.UseVisualStyleBackColor = true;
            this.cmbOutput.Click += new System.EventHandler(this.cmbOutput_Click);
            // 
            // lblSaveExplorer
            // 
            this.lblSaveExplorer.Location = new System.Drawing.Point(12, 16);
            this.lblSaveExplorer.Name = "lblSaveExplorer";
            this.lblSaveExplorer.Size = new System.Drawing.Size(400, 48);
            this.lblSaveExplorer.TabIndex = 3;
            this.lblSaveExplorer.Text = resources.GetString("lblSaveExplorer.Text");
            // 
            // frmSaveExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 120);
            this.Controls.Add(this.lblSaveExplorer);
            this.Controls.Add(this.cmbOutput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSaveExplorer";
            this.Text = "Save Explorer";
            this.Load += new System.EventHandler(this.frmSaveExplorer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmbOutput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Label lblSaveExplorer;
    }
}