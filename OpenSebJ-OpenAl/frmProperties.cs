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

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for frmProperties.
	/// </summary>
	public class frmProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox chkLoop;

		private int sample;

        private System.Windows.Forms.TrackBar trkPan;
		private System.Windows.Forms.Label lblL;
		private System.Windows.Forms.Label lblRight;
		private System.Windows.Forms.Label lblCenter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TrackBar trkVolume;
		private System.Windows.Forms.CheckBox chkReverse;
		private System.Windows.Forms.Label lblChannels;
		private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblBitDepth;
		private System.Windows.Forms.Label lblFrequency;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblFrequencyHigh;
		private System.Windows.Forms.Label lblFrequencyLow;
        private System.Windows.Forms.TrackBar trkFreq;
        private TextBox txtLocation;
        private ToolStrip toolStrip;
        private ToolStripButton playToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton stopToolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton viewToolStripButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton pitchRollerToolStripButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton setKeyToolStripButton;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton setVideoToolStripButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmProperties(int sampleNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			sample = sampleNum;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            // If the MDI Child is full screen when disposing it causes an issue where the
            // MDI window flickers, as if it doesn't know when to go full screen or normal.
            // Any way, seting this window to the Normal window state before disposing
            // the form, allows it to disposes correctly and all other windows to continue 
            // on as normal.
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProperties));
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.trkPan = new System.Windows.Forms.TrackBar();
            this.trkVolume = new System.Windows.Forms.TrackBar();
            this.lblL = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblCenter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.lblChannels = new System.Windows.Forms.Label();
            this.lblBitDepth = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFrequencyHigh = new System.Windows.Forms.Label();
            this.lblFrequencyLow = new System.Windows.Forms.Label();
            this.trkFreq = new System.Windows.Forms.TrackBar();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.playToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pitchRollerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.setKeyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.setVideoToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.trkPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFreq)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkLoop
            // 
            this.chkLoop.Location = new System.Drawing.Point(269, 115);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(56, 24);
            this.chkLoop.TabIndex = 2;
            this.chkLoop.Text = "Loop";
            this.chkLoop.CheckedChanged += new System.EventHandler(this.chkLoop_CheckedChanged);
            // 
            // trkPan
            // 
            this.trkPan.Location = new System.Drawing.Point(0, 59);
            this.trkPan.Maximum = 6000;
            this.trkPan.Minimum = -6000;
            this.trkPan.Name = "trkPan";
            this.trkPan.Size = new System.Drawing.Size(376, 45);
            this.trkPan.TabIndex = 4;
            this.trkPan.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkPan.ValueChanged += new System.EventHandler(this.trkPan_ValueChanged);
            // 
            // trkVolume
            // 
            this.trkVolume.Location = new System.Drawing.Point(179, 98);
            this.trkVolume.Maximum = 128;
            this.trkVolume.Name = "trkVolume";
            this.trkVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkVolume.Size = new System.Drawing.Size(45, 216);
            this.trkVolume.TabIndex = 5;
            this.trkVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkVolume.Value = 128;
            this.trkVolume.ValueChanged += new System.EventHandler(this.trkVolume_ValueChanged);
            // 
            // lblL
            // 
            this.lblL.Location = new System.Drawing.Point(8, 82);
            this.lblL.Name = "lblL";
            this.lblL.Size = new System.Drawing.Size(32, 16);
            this.lblL.TabIndex = 7;
            this.lblL.Text = "Left";
            this.lblL.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblRight
            // 
            this.lblRight.Location = new System.Drawing.Point(336, 82);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(32, 16);
            this.lblRight.TabIndex = 8;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblCenter
            // 
            this.lblCenter.Location = new System.Drawing.Point(184, 82);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(8, 16);
            this.lblCenter.TabIndex = 9;
            this.lblCenter.Text = "|";
            this.lblCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(203, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(8, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(165, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMax
            // 
            this.lblMax.Location = new System.Drawing.Point(206, 106);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(32, 16);
            this.lblMax.TabIndex = 13;
            this.lblMax.Text = "Max";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(206, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Min";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // chkReverse
            // 
            this.chkReverse.Location = new System.Drawing.Point(269, 145);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(82, 24);
            this.chkReverse.TabIndex = 46;
            this.chkReverse.Text = "Reverse";
            this.chkReverse.Click += new System.EventHandler(this.chkReverse_Click);
            this.chkReverse.CheckedChanged += new System.EventHandler(this.chkReverse_CheckedChanged);
            // 
            // lblChannels
            // 
            this.lblChannels.Location = new System.Drawing.Point(5, 115);
            this.lblChannels.Name = "lblChannels";
            this.lblChannels.Size = new System.Drawing.Size(152, 16);
            this.lblChannels.TabIndex = 47;
            this.lblChannels.Text = "Channels : ";
            this.lblChannels.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBitDepth
            // 
            this.lblBitDepth.Location = new System.Drawing.Point(5, 145);
            this.lblBitDepth.Name = "lblBitDepth";
            this.lblBitDepth.Size = new System.Drawing.Size(152, 16);
            this.lblBitDepth.TabIndex = 48;
            this.lblBitDepth.Text = "Bits Depth: ";
            this.lblBitDepth.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblBitDepth.Click += new System.EventHandler(this.lblFrequency_Click);
            // 
            // lblLength
            // 
            this.lblLength.Location = new System.Drawing.Point(5, 205);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(152, 16);
            this.lblLength.TabIndex = 49;
            this.lblLength.Text = "Seconds : ";
            this.lblLength.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFrequency
            // 
            this.lblFrequency.Location = new System.Drawing.Point(5, 175);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(152, 16);
            this.lblFrequency.TabIndex = 52;
            this.lblFrequency.Text = "Frequency : ";
            this.lblFrequency.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(184, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 16);
            this.label4.TabIndex = 56;
            this.label4.Text = "|";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFrequencyHigh
            // 
            this.lblFrequencyHigh.Location = new System.Drawing.Point(336, 354);
            this.lblFrequencyHigh.Name = "lblFrequencyHigh";
            this.lblFrequencyHigh.Size = new System.Drawing.Size(32, 16);
            this.lblFrequencyHigh.TabIndex = 55;
            this.lblFrequencyHigh.Text = "High";
            this.lblFrequencyHigh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblFrequencyLow
            // 
            this.lblFrequencyLow.Location = new System.Drawing.Point(5, 354);
            this.lblFrequencyLow.Name = "lblFrequencyLow";
            this.lblFrequencyLow.Size = new System.Drawing.Size(32, 16);
            this.lblFrequencyLow.TabIndex = 54;
            this.lblFrequencyLow.Text = "Low";
            this.lblFrequencyLow.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // trkFreq
            // 
            this.trkFreq.Location = new System.Drawing.Point(0, 331);
            this.trkFreq.Maximum = 44000;
            this.trkFreq.Name = "trkFreq";
            this.trkFreq.Size = new System.Drawing.Size(377, 45);
            this.trkFreq.TabIndex = 53;
            this.trkFreq.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkFreq.Value = 22000;
            this.trkFreq.ValueChanged += new System.EventHandler(this.trkFreq_ValueChanged);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(8, 31);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(360, 20);
            this.txtLocation.TabIndex = 62;
            this.txtLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocation_KeyPress);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripButton,
            this.toolStripSeparator1,
            this.stopToolStripButton,
            this.toolStripSeparator2,
            this.viewToolStripButton,
            this.toolStripSeparator3,
            this.pitchRollerToolStripButton,
            this.toolStripSeparator4,
            this.setKeyToolStripButton,
            this.toolStripSeparator5,
            this.setVideoToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(375, 25);
            this.toolStrip.TabIndex = 63;
            this.toolStrip.TabStop = true;
            this.toolStrip.Text = "toolStrip1";
            // 
            // playToolStripButton
            // 
            this.playToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("playToolStripButton.Image")));
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // viewToolStripButton
            // 
            this.viewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("viewToolStripButton.Image")));
            this.viewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewToolStripButton.Name = "viewToolStripButton";
            this.viewToolStripButton.Size = new System.Drawing.Size(33, 22);
            this.viewToolStripButton.Text = "View";
            this.viewToolStripButton.Click += new System.EventHandler(this.viewToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // pitchRollerToolStripButton
            // 
            this.pitchRollerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pitchRollerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pitchRollerToolStripButton.Image")));
            this.pitchRollerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pitchRollerToolStripButton.Name = "pitchRollerToolStripButton";
            this.pitchRollerToolStripButton.Size = new System.Drawing.Size(64, 22);
            this.pitchRollerToolStripButton.Text = "Pitch Roller";
            this.pitchRollerToolStripButton.Click += new System.EventHandler(this.pitchRollerToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // setKeyToolStripButton
            // 
            this.setKeyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.setKeyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("setKeyToolStripButton.Image")));
            this.setKeyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setKeyToolStripButton.Name = "setKeyToolStripButton";
            this.setKeyToolStripButton.Size = new System.Drawing.Size(48, 22);
            this.setKeyToolStripButton.Text = "Set Key";
            this.setKeyToolStripButton.Click += new System.EventHandler(this.setKeyToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // setVideoToolStripButton
            // 
            this.setVideoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.setVideoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("setVideoToolStripButton.Image")));
            this.setVideoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setVideoToolStripButton.Name = "setVideoToolStripButton";
            this.setVideoToolStripButton.Size = new System.Drawing.Size(56, 22);
            this.setVideoToolStripButton.Text = "Set Video";
            // 
            // frmProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(375, 381);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFrequencyHigh);
            this.Controls.Add(this.lblFrequencyLow);
            this.Controls.Add(this.trkFreq);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.lblBitDepth);
            this.Controls.Add(this.lblChannels);
            this.Controls.Add(this.chkReverse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trkVolume);
            this.Controls.Add(this.lblCenter);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblL);
            this.Controls.Add(this.trkPan);
            this.Controls.Add(this.chkLoop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmProperties";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.frmProperties_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmProperties_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trkPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFreq)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmProperties_Load(object sender, System.EventArgs e)
		{
            //// Check if the sample is loaded first;=
            //if (globalSettings.osj.sampleLoaded[sample] == false)
            //{
            //    System.Windows.Forms.MessageBox.Show("Sample not loaded");
            //    //this.Close();
            //    this.Dispose();
            //    return;
            //}
            //else
            //{
                //int tempSample = sample + 1;


                //this.Text = "Sample " + sample.ToString() + " Properties";

                this.Text = "Sample Properties :: " + globalSettings.osj.sampleDetails_sampleName[sample];

                txtLocation.Text = globalSettings.osj.sampleDetails_sampleName[sample];

                //if (dsInterface.loopSample[sample] == true)
                if (sdlInterface.loopSample[sample] == true)
                {
                    chkLoop.Checked = true;
                }
                else
                {
                    chkLoop.Checked = false;
                }

                




                //if (dsInterface.reverseSample[sample] == true)
                //{
                //    chkReverse.Checked = true;
                //}
                //else
                //{
                //    chkReverse.Checked = false;
                //}

                //trkPan.Value = dsInterface.getPan(sample);
                //trkVolume.Value = dsInterface.getVolume(sample);
                trkVolume.Value = sdlInterface.getVolume(sample);

                // Loads the sample settings to the globalSettings.osj instance
                OpenAlInterface.getSampleSetting(sample);
                // Retrives the details and populates the form.
                lblChannels.Text = lblChannels.Text + globalSettings.osj.sampleFormat_Channels[sample];
                lblBitDepth.Text = lblBitDepth.Text + globalSettings.osj.sampleFormat_BitsPerSample[sample];
                lblLength.Text = lblLength.Text + globalSettings.osj.sampleFormat_LengthInSeconds[sample];
                lblFrequency.Text = lblFrequency.Text + globalSettings.osj.sampleSettings_Frequency[sample];

                // Setup the trkFreq
                try
                {
                    //This needs to be set to the pratical minimum so that when
                    // the sample is set to the minimum the display of the sample length
                    // in the lay down is correct; otherwise it is too long, longer than
                    // the real play length if the value is set lower than what direct x will play.
                    trkFreq.Minimum = 4000;

                    // v0.05 changed to make sure the highest value is still playable.
                    // Seems to only actually handle a frequency value of 3000 more.
                    
                    //TODO: SG - Add freq stuff
                    //trkFreq.Maximum = dsInterface.getFrequency(sample) + 3000;
                    //trkFreq.Value = dsInterface.getCurrentFrequency(sample);
                }
                catch { }

            //}
		}

		private void chkLoop_CheckedChanged(object sender, System.EventArgs e)
		{
            sdlInterface.loopSample[sample] = true;
            


            //TODO: SG - add check for playing and add stop/start logic in


            //if (chkLoop.Checked == true)
            //{
            //    sdlInterface.loopSample[sample] = true;

            //    int pos = 0;
            //    int writePos = 0;

            //    if (sdlInterface.aSound[sample].Status.Playing == true)
            //    {
            //        sdlInterface.aSound[sample].GetCurrentPosition(out pos, out writePos);
            //        sdlInterface.aSound[sample].SetCurrentPosition(pos);
            //        sdlInterface.playFromPosition(sample);
            //    }



            //}
            //else
            //{
            //    dsInterface.loopSample[sample] = false;
				
            //    int pos = 0;
            //    int writePos = 0;

            //    if (dsInterface.aSound[sample].Status.Playing == true)
            //    {
            //        dsInterface.aSound[sample].GetCurrentPosition(out pos, out writePos);
            //        dsInterface.aSound[sample].SetCurrentPosition(pos);
            //        dsInterface.playFromPosition(sample);
            //    }
            //}
		}

		private void picDone_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void trkVolume_ValueChanged(object sender, System.EventArgs e)
		{
            sdlInterface.setVolume(sample, trkVolume.Value);

            //dsInterface.setVolume(sample,trkVolume.Value);
		}

		private void trkPan_ValueChanged(object sender, System.EventArgs e)
		{
            //TODO: SG - Work out how to set pan
            
            //dsInterface.setPan(sample,trkPan.Value);
		}

		private void chkReverse_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		private void lblFrequency_Click(object sender, System.EventArgs e)
		{
		
		}

        //private void cmbPlay_Click(object sender, System.EventArgs e)
        //{
        //    dsInterface.play(sample);
        //}

        //private void cmbStop_Click(object sender, System.EventArgs e)
        //{
        //    dsInterface.stop(sample);
        //}

		private void chkReverse_Click(object sender, System.EventArgs e)
		{
			if (chkReverse.Checked == true)
			{
				//dsInterface.reverseSample[sample] = true;
                sdlInterface.reverseSample[sample] = true;
			}
			else
			{
				//dsInterface.reverseSample[sample] = false;
                sdlInterface.reverseSample[sample] = false;
            }

            //TODO: sg - add buffer reverse
            //bufferReverse bR = new bufferReverse();
            //bR.reverseSample(sample);
		}

		private void trkFreq_ValueChanged(object sender, System.EventArgs e)
		{
            //TODO: Setup frequency stuff

            //dsInterface.setFrequency(sample,trkFreq.Value);
            //lblLength.Text = "Seconds : " + dsInterface.getLength(sample);
            //lblFrequency.Text = "Frequency : " + dsInterface.getCurrentFrequency(sample);
		}

		private void frmProperties_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			dsInterface.playKey(e.KeyValue, e.Control);
		}

        //private void cmbSetKey_Click(object sender, EventArgs e)
        //{
        //    frmKey keySet = new frmKey(sample);
        //    keySet.Visible = true;
        //    keySet.Activate();    
        //}

        private void cmbSplice_Click(object sender, EventArgs e)
        {
            //frmTape tape = new frmTape(sample);
            //tape.Visible = true;
            //tape.Activate();
        }

        //private void cmbPitchRoller_Click(object sender, EventArgs e)
        //{
        //    int _sample = sample;

        //    if (globalSettings.osj.sampleLoaded[_sample] == true)
        //    {
        //        // Open the pitchShift window - if the sample has been loaded.
        //        frmPitchShifter _pitchShift = new frmPitchShifter(_sample);
        //        _pitchShift.MdiParent = globalSettings.MDIForm;
        //        _pitchShift.Show();
        //    }
        //    else
        //    {
        //        System.Windows.Forms.MessageBox.Show("Sample not loaded");
        //    }
            
        //}

        //private void cmbView_Click(object sender, EventArgs e)
        //{
        //    int _sample = sample;

        //    if (globalSettings.osj.sampleLoaded[_sample] == true)
        //    {
        //        // Seems to have an issue when closing another MDI window - if the scratch window is launched 
        //        // as an MDI child
        //        //frmWaveScratch _wavePlot = new frmWaveScratch(_sample);
        //        //_wavePlot.MdiParent = globalSettings.MDIForm;
        //        //_wavePlot.Show();

        //        frmWaveScratch wavePlot = new frmWaveScratch(sample);
        //        wavePlot.Visible = true;
        //        wavePlot.Activate();

        //    }
        //    else
        //    {
        //        System.Windows.Forms.MessageBox.Show("Sample not loaded");
        //    }    
        //}

        //private void cmbVideo_Click(object sender, EventArgs e)
        //{
        //    frmAddGraphics AddGraphics = new frmAddGraphics(sample);
        //    AddGraphics.Visible = true;
        //    AddGraphics.Activate();
        //}

        private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Need to save the last key thats been pressed as well - the text in the text box 
            // is not updated in the variable until after the Keypress event has been completed
            globalSettings.osj.sampleDetails_sampleName[sample] = txtLocation.Text + e.KeyChar.ToString();
        }

        private void playToolStripButton_Click(object sender, EventArgs e)
        {
            //dsInterface.play(sample);

            sdlInterface.play(sample);
            
        }

        private void stopToolStripButton_Click(object sender, EventArgs e)
        {
            //dsInterface.stop(sample);

            sdlInterface.stop(sample);

        }

        private void viewToolStripButton_Click(object sender, EventArgs e)
        {
            int _sample = sample;

            if (globalSettings.osj.sampleLoaded[_sample] == true)
            {
                // Seems to have an issue when closing another MDI window - if the scratch window is launched 
                // as an MDI child
                
                //frmWaveScratch _wavePlot = new frmWaveScratch(_sample);
                //_wavePlot.MdiParent = globalSettings.MDIForm;
                //_wavePlot.Show();

                ////frmWaveScratch wavePlot = new frmWaveScratch(sample);
                ////wavePlot.Visible = true;
                ////wavePlot.Activate();

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Sample not loaded");
            }   
        }

        private void pitchRollerToolStripButton_Click(object sender, EventArgs e)
        {
            int _sample = sample;

            if (globalSettings.osj.sampleLoaded[_sample] == true)
            {
                // Open the pitchShift window - if the sample has been loaded.
                frmPitchShifter _pitchShift = new frmPitchShifter(_sample);
                _pitchShift.MdiParent = globalSettings.MDIForm;
                _pitchShift.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Sample not loaded");
            }
        }

        private void setKeyToolStripButton_Click(object sender, EventArgs e)
        {
            frmKey keySet = new frmKey(sample);
            keySet.Visible = true;
            keySet.Activate();    
        }

        //private void setVideoToolStripButton_Click(object sender, EventArgs e)
        //{
        //    frmAddGraphics AddGraphics = new frmAddGraphics(sample);
        //    AddGraphics.Visible = true;
        //    AddGraphics.Activate();
        //}


	}
}
