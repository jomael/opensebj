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
    partial class frmTrackEditor
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
            globalSettings.trackEditorDefined = false;

            // Dispose of the timer when disposing the form
            if (disposing && timerDefined)
            {
                stateTimer.Dispose();
                timerDefined = false;
            }

            // If the MDI Child is full screen when disposing it causes an issue where the
            // MDI window flickers, as if it doesn't know when to go full screen or normal.
            // Any way, seting this window to the Normal window state before disposing
            // the form, allows it to disposes correctly and all other windows to continue 
            // on as normal.
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

            // Dispose of all components on the form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrackEditor));
            this.ControlsToolStrip = new System.Windows.Forms.ToolStrip();
            this.playToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.keyRecordStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lblLoop = new System.Windows.Forms.Label();
            this.ControlsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsToolStrip
            // 
            this.ControlsToolStrip.AllowItemReorder = true;
            this.ControlsToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ControlsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripButton,
            this.toolStripSeparator1,
            this.resetToolStripButton,
            this.toolStripSeparator2,
            this.stopToolStripButton,
            this.toolStripSeparator3,
            this.keyRecordStripButton,
            this.toolStripSeparator4,
            this.loopToolStripButton});
            this.ControlsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ControlsToolStrip.Name = "ControlsToolStrip";
            this.ControlsToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ControlsToolStrip.Size = new System.Drawing.Size(256, 25);
            this.ControlsToolStrip.TabIndex = 3;
            this.ControlsToolStrip.Text = "Controls";
            this.ControlsToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ControlsToolStrip_ItemClicked);
            // 
            // playToolStripButton
            // 
            this.playToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playToolStripButton.Name = "playToolStripButton";
            this.playToolStripButton.Size = new System.Drawing.Size(31, 22);
            this.playToolStripButton.Text = "Play";
            this.playToolStripButton.Click += new System.EventHandler(this.playToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // resetToolStripButton
            // 
            this.resetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("resetToolStripButton.Image")));
            this.resetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetToolStripButton.Name = "resetToolStripButton";
            this.resetToolStripButton.Size = new System.Drawing.Size(39, 22);
            this.resetToolStripButton.Text = "Reset";
            this.resetToolStripButton.Click += new System.EventHandler(this.resetToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripButton.Image")));
            this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Size = new System.Drawing.Size(33, 22);
            this.stopToolStripButton.Text = "Stop";
            this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // keyRecordStripButton
            // 
            this.keyRecordStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.keyRecordStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.keyRecordStripButton.Name = "keyRecordStripButton";
            this.keyRecordStripButton.Size = new System.Drawing.Size(66, 22);
            this.keyRecordStripButton.Text = "Key Record";
            this.keyRecordStripButton.Click += new System.EventHandler(this.keyRecordStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // loopToolStripButton
            // 
            this.loopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loopToolStripButton.Name = "loopToolStripButton";
            this.loopToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.loopToolStripButton.Text = "Set Loop";
            this.loopToolStripButton.Click += new System.EventHandler(this.loopToolStripButton_Click);
            // 
            // lblLoop
            // 
            this.lblLoop.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblLoop.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.lblLoop.ForeColor = System.Drawing.Color.Black;
            this.lblLoop.Location = new System.Drawing.Point(300, 26);
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size(5, 250);
            this.lblLoop.TabIndex = 137;
            this.lblLoop.Visible = false;
            this.lblLoop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblLoop_MouseDown);
            this.lblLoop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblLoop_MouseMove);
            // 
            // frmTrackEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(920, 308);
            this.Controls.Add(this.lblLoop);
            this.Controls.Add(this.ControlsToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTrackEditor";
            this.Text = "Track Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTrackEditor_KeyDown);
            this.Load += new System.EventHandler(this.frmTrackEditor_Load);
            this.ControlsToolStrip.ResumeLayout(false);
            this.ControlsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.ToolStrip ControlsToolStrip;
        private System.Windows.Forms.ToolStripButton playToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton resetToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripButton keyRecordStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label lblLoop;
        private System.Windows.Forms.ToolStripButton loopToolStripButton;






    }
}