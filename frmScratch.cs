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

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for frmScratch.
	/// </summary>
	public class frmScratch : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// First Timer ------------------------------------------
		// To stop the tread if its active
		private bool timerDefined = false;

		// Following for timer event fire functions
		AutoResetEvent autoEvent = new AutoResetEvent(false);

		// Create the delegate that invokes methods for the timer.
		TimerCallback timerDelegate;

		// Create a timer that signals the delegate to invoke 
		System.Threading.Timer stateTimer;

		int tick = 10;

		// Second Timer -----------------------------------------
		// To stop the tread if its active
		private bool timerDefined2 = false;

		// Following for timer event fire functions
		AutoResetEvent autoEvent2 = new AutoResetEvent(false);

		// Create the delegate that invokes methods for the timer.
		TimerCallback timerDelegate2;

		// Create a timer that signals the delegate to invoke 
		System.Threading.Timer stateTimer2;

		int tick2 = 10;

		//-------------------------------------------------------

		//int baseTick = 100;
		int playTick = 100;

		int rotate1 = 0;
		int rotate2 = 0;

		//bool alreadyPainting = true;

		bool spinClockwise1 = true;
		bool spinClockwise2 = true;

		int sample1;
		int sample2;

		bool sample1Reverse = false;
		bool sample2Reverse = false;

		//int currentPosition = 0;

		private System.Windows.Forms.Button cmbPlay1;
		private System.Windows.Forms.Button cmbPause1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cmbSample1;
		private System.Windows.Forms.PictureBox record1;
		private System.Windows.Forms.PictureBox record2;
		private System.Windows.Forms.ComboBox cmbSample2;
		private System.Windows.Forms.Button cmbPlay2;
		private System.Windows.Forms.Button cmbPause2;
		private System.Windows.Forms.TrackBar crossFader;
		private System.Windows.Forms.Panel panel2;
	

		public frmScratch()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				// Dispose of the timer when disposing the form
				if (timerDefined == true)
				{
					stateTimer.Dispose();
					timerDefined = false;
				}

				// Dispose of the timer when disposing the form
				if (timerDefined2 == true)
				{
					stateTimer2.Dispose();
					timerDefined2 = false;
				}


                dsInterface.stop(sample1);
                dsInterface.stop(256);
                dsInterface.stop(257);
                dsInterface.stop(258);

                dsInterface.stop(sample2);
                dsInterface.stop(259);
                dsInterface.stop(260);
                dsInterface.stop(261);


                // If the MDI Child is full screen when disposing it causes an issue where the
                // MDI window flickers, as if it doesn't know when to go full screen or normal.
                // Any way, seting this window to the Normal window state before disposing
                // the form, allows it to disposes correctly and all other windows to continue 
                // on as normal.
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScratch));
            this.cmbPlay1 = new System.Windows.Forms.Button();
            this.record1 = new System.Windows.Forms.PictureBox();
            this.cmbPause1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbSample2 = new System.Windows.Forms.ComboBox();
            this.cmbPlay2 = new System.Windows.Forms.Button();
            this.cmbPause2 = new System.Windows.Forms.Button();
            this.cmbSample1 = new System.Windows.Forms.ComboBox();
            this.crossFader = new System.Windows.Forms.TrackBar();
            this.record2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.record1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossFader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.record2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPlay1
            // 
            this.cmbPlay1.BackColor = System.Drawing.Color.Black;
            this.cmbPlay1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbPlay1.BackgroundImage")));
            this.cmbPlay1.Enabled = false;
            this.cmbPlay1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlay1.Location = new System.Drawing.Point(8, 8);
            this.cmbPlay1.Name = "cmbPlay1";
            this.cmbPlay1.Size = new System.Drawing.Size(64, 32);
            this.cmbPlay1.TabIndex = 0;
            this.cmbPlay1.Text = "|>";
            this.cmbPlay1.UseVisualStyleBackColor = false;
            this.cmbPlay1.Click += new System.EventHandler(this.cmbPlay1_Click);
            // 
            // record1
            // 
            this.record1.Image = ((System.Drawing.Image)(resources.GetObject("record1.Image")));
            this.record1.Location = new System.Drawing.Point(0, 0);
            this.record1.Name = "record1";
            this.record1.Size = new System.Drawing.Size(256, 256);
            this.record1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.record1.TabIndex = 1;
            this.record1.TabStop = false;
            this.record1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.record1_MouseMove);
            this.record1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.record1_MouseUp);
            // 
            // cmbPause1
            // 
            this.cmbPause1.BackColor = System.Drawing.Color.Black;
            this.cmbPause1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbPause1.BackgroundImage")));
            this.cmbPause1.Location = new System.Drawing.Point(176, 8);
            this.cmbPause1.Name = "cmbPause1";
            this.cmbPause1.Size = new System.Drawing.Size(64, 32);
            this.cmbPause1.TabIndex = 2;
            this.cmbPause1.Text = "[]";
            this.cmbPause1.UseVisualStyleBackColor = false;
            this.cmbPause1.Click += new System.EventHandler(this.Pause1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.cmbSample2);
            this.panel1.Controls.Add(this.cmbPlay2);
            this.panel1.Controls.Add(this.cmbPause2);
            this.panel1.Controls.Add(this.cmbSample1);
            this.panel1.Controls.Add(this.cmbPlay1);
            this.panel1.Controls.Add(this.cmbPause1);
            this.panel1.Controls.Add(this.crossFader);
            this.panel1.Location = new System.Drawing.Point(0, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 48);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmbSample2
            // 
            this.cmbSample2.BackColor = System.Drawing.Color.Black;
            this.cmbSample2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSample2.ForeColor = System.Drawing.Color.White;
            this.cmbSample2.Location = new System.Drawing.Point(472, 8);
            this.cmbSample2.Name = "cmbSample2";
            this.cmbSample2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbSample2.Size = new System.Drawing.Size(88, 24);
            this.cmbSample2.TabIndex = 4;
            this.cmbSample2.Text = "Sample";
            this.cmbSample2.SelectedIndexChanged += new System.EventHandler(this.cmbSample2_SelectedIndexChanged);
            // 
            // cmbPlay2
            // 
            this.cmbPlay2.BackColor = System.Drawing.Color.Black;
            this.cmbPlay2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbPlay2.BackgroundImage")));
            this.cmbPlay2.Enabled = false;
            this.cmbPlay2.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlay2.Location = new System.Drawing.Point(400, 8);
            this.cmbPlay2.Name = "cmbPlay2";
            this.cmbPlay2.Size = new System.Drawing.Size(64, 32);
            this.cmbPlay2.TabIndex = 3;
            this.cmbPlay2.Text = "|>";
            this.cmbPlay2.UseVisualStyleBackColor = false;
            this.cmbPlay2.Click += new System.EventHandler(this.cmbPlay2_Click);
            // 
            // cmbPause2
            // 
            this.cmbPause2.BackColor = System.Drawing.Color.Black;
            this.cmbPause2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbPause2.BackgroundImage")));
            this.cmbPause2.Location = new System.Drawing.Point(568, 8);
            this.cmbPause2.Name = "cmbPause2";
            this.cmbPause2.Size = new System.Drawing.Size(64, 32);
            this.cmbPause2.TabIndex = 5;
            this.cmbPause2.Text = "[]";
            this.cmbPause2.UseVisualStyleBackColor = false;
            this.cmbPause2.Click += new System.EventHandler(this.cmbPause2_Click);
            // 
            // cmbSample1
            // 
            this.cmbSample1.BackColor = System.Drawing.Color.Black;
            this.cmbSample1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSample1.ForeColor = System.Drawing.Color.White;
            this.cmbSample1.Location = new System.Drawing.Point(80, 8);
            this.cmbSample1.Name = "cmbSample1";
            this.cmbSample1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbSample1.Size = new System.Drawing.Size(88, 24);
            this.cmbSample1.TabIndex = 0;
            this.cmbSample1.Text = "Sample";
            this.cmbSample1.SelectedIndexChanged += new System.EventHandler(this.cmbSample1_SelectedIndexChanged);
            // 
            // crossFader
            // 
            this.crossFader.Location = new System.Drawing.Point(240, 16);
            this.crossFader.Maximum = 12000;
            this.crossFader.Name = "crossFader";
            this.crossFader.Size = new System.Drawing.Size(160, 42);
            this.crossFader.TabIndex = 6;
            this.crossFader.Value = 6000;
            this.crossFader.Scroll += new System.EventHandler(this.crossFader_Scroll);
            // 
            // record2
            // 
            this.record2.Image = ((System.Drawing.Image)(resources.GetObject("record2.Image")));
            this.record2.Location = new System.Drawing.Point(384, 0);
            this.record2.Name = "record2";
            this.record2.Size = new System.Drawing.Size(256, 256);
            this.record2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.record2.TabIndex = 4;
            this.record2.TabStop = false;
            this.record2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.record2_MouseMove);
            this.record2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.record2_MouseUp);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(256, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(128, 256);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // frmScratch
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(12, 22);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(642, 298);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.record2);
            this.Controls.Add(this.record1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmScratch";
            this.Text = "Scratch";
            this.Deactivate += new System.EventHandler(this.frmScratch_Deactivate);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmScratch_Paint);
            this.Activated += new System.EventHandler(this.frmScratch_Activated);
            this.Load += new System.EventHandler(this.frmScratch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.record1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossFader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.record2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void frmScratch_Load(object sender, System.EventArgs e)
		{
            if (dsInterface.loadedSamples == -1)
            {
                System.Windows.Forms.MessageBox.Show("Please load a sample first.");
                this.Close();
            }
            else
            {

                for (int i = 1; i <= dsInterface.loadedSamples + 1; i++)
                {
                    cmbSample1.Items.Add(i);
                    cmbSample2.Items.Add(i);
                }

                cmbSample1.SelectedIndex = 0;
                cmbSample2.SelectedIndex = 0;
            }
		}

		
		private void record1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (dsInterface.aSound[256].Status.Playing == false)
				{
					// If the fast forward sample is not playing, the 
					// current position of the origional speed sample
					// is taken and extrapalated against the fast sample
					// to obtain an equivelent cursor position in the fast
					// sample. Play is stoped for the slow sample while the
					// fast sample takes over; bringing the ilusion of 
					// moving the turn table faster..
					float playOffset = (float)dsInterface.aSound[256].Caps.BufferBytes / (float)dsInterface.aSound[sample1].Caps.BufferBytes;
					float newCursor = playOffset * getPlayPosition_1();
					dsInterface.aSound[256].SetCurrentPosition((int)newCursor);
					dsInterface.playFromPosition(256);
					
					if (sample1Reverse == true)
					{
						sample1Reverse = false;
					}
				}
				reDrawSpin(true,true);
			}
			else if(e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (dsInterface.aSound[258].Status.Playing == false)
				{
					// If the fast forward sample is not playing, the 
					// current position of the origional speed sample
					// is taken and extrapalated against the fast sample
					// to obtain an equivelent cursor position in the fast
					// sample. Play is stoped for the slow sample while the
					// fast sample takes over; bringing the ilusion of 
					// moving the turn table faster..
					float playOffset = (float)dsInterface.aSound[258].Caps.BufferBytes / (float)dsInterface.aSound[257].Caps.BufferBytes;
					float newCursor = playOffset * getPlayPosition_1();
					dsInterface.aSound[258].SetCurrentPosition(dsInterface.aSound[258].Caps.BufferBytes - (int)newCursor);
					dsInterface.playFromPosition(258);
					
					if (sample1Reverse == false)
					{

					}	
				}
				reDrawSpin(false,true);
			}
		}

		
		public void reDrawSpin(bool clockwise, bool left)
		{
			if (left == true)
			{
				spinClockwise1 = clockwise;
			}
			else
			{
				spinClockwise2 = clockwise;
			}



			//Graphics g = this.CreateGraphics();
			Graphics g1 = record1.CreateGraphics();
			
			//System.Drawing.Pen aPen = new Pen(Color.Red);
			//g.DrawRectangle(aPen,100,100,100,100);
	
			Matrix myMatrix1 = new Matrix();
			Point aPoint1 = new Point(125,125);
			if (left == true)
			{
				//Point aPoint = new Point(250,250);
				if (clockwise == true)
				{
					rotate1 = rotate1 + 1;
					rotate1 = rotate1 + 1;	
				}
				else
				{
					rotate1 = rotate1 - 1;
					rotate1 = rotate1 - 1;
				}
			}
			myMatrix1.RotateAt(rotate1,aPoint1);
			g1.Transform = myMatrix1;
			
			//==============

			/*Graphics g2 = record2.CreateGraphics();
			Matrix myMatrix2 = new Matrix();
			Point aPoint2 = new Point(125,125);
			
			if (left == false)
			{
				if (clockwise == true)
				{
					rotate2 = rotate2 + 1;
					rotate2 = rotate2 + 1;
				}
				else
				{
					rotate2 = rotate2 - 1;
					rotate2 = rotate2 - 1;
				}
			}
			myMatrix2.RotateAt(rotate2,aPoint2);
			g2.Transform = myMatrix2;
			//g.DrawImage(this.BackgroundImage, 0,0,500,500);
			//g.DrawImage(this.BackgroundImage, 0,0,250,250);
			*/

			

			try
			{
				g1.DrawImage(record1.Image, 0,0,250,250);
				//g2.DrawImage(record2.Image, 0,0,250,250);
			}
			catch{}
			
		}
		
		public void reDrawSpinRight(bool clockwise)
		{
			spinClockwise2 = clockwise;

			Graphics g2 = record2.CreateGraphics();
			Matrix myMatrix2 = new Matrix();
			Point aPoint2 = new Point(125,125);
			
			if (clockwise == true)
			{
				rotate2 = rotate2 + 1;
				rotate2 = rotate2 + 1;
			}
			else
			{
				rotate2 = rotate2 - 1;
				rotate2 = rotate2 - 1;
			}
			
			myMatrix2.RotateAt(rotate2,aPoint2);
			g2.Transform = myMatrix2;
			//g.DrawImage(this.BackgroundImage, 0,0,500,500);
			//g.DrawImage(this.BackgroundImage, 0,0,250,250);
			
			try
			{
				g2.DrawImage(record2.Image, 0,0,250,250);
			}
			catch{}
		}

		private void acurateTime()
		{
			// Create the delegate that invokes methods for the timer.
			timerDelegate = new TimerCallback(goGo);
			
			// Create a timer that signals the delegate to invoke 
			// CheckStatus imidiatealy (0 milliseconds), and every 10 milliseconds
			// thereafter.
			stateTimer = new System.Threading.Timer(timerDelegate, autoEvent, 0, tick);
			
			// Used so that if the play button is clicked a second time
			// the timer can be disposed of saftley.
			timerDefined = true;
		}

		private void goGo(Object stateInfo)
		{
			//Need to try catch incase the form has started disposing when the call
			//back is fired
			try
			{				
				this.Invalidate();
				reDrawSpin(spinClockwise1, true);
				this.Update();
				this.Validate();

				playTick++;
			}
			catch{}
		}


		private void acurateTime2()
		{
			// Create the delegate that invokes methods for the timer.
			timerDelegate2 = new TimerCallback(goGo2);
			
			// Create a timer that signals the delegate to invoke 
			// CheckStatus imidiatealy (0 milliseconds), and every 10 milliseconds
			// thereafter.
			stateTimer2 = new System.Threading.Timer(timerDelegate2, autoEvent2, 0, tick2);
			
			// Used so that if the play button is clicked a second time
			// the timer can be disposed of saftley.
			timerDefined2 = true;
		}


		private void goGo2(Object stateInfo)
		{
			//Need to try catch incase the form has started disposing when the call
			//back is fired
			try
			{				
				this.Invalidate();
				reDrawSpinRight(spinClockwise2);
				this.Update();
				this.Validate();

				playTick++;
			}
			catch{}
		}



		private void cmbPlay1_Click(object sender, System.EventArgs e)
		{
			if (timerDefined == true)
			{
				stateTimer.Dispose();
				timerDefined = false;
			}

			timerDefined = true;
			
			acurateTime();
			playTick = 100;

			dsInterface.play(sample1);

            cmbSample1.Enabled = false;


		}

		
		private void frmScratch_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// Redraw with last spin direction.
			//reDrawSpin(spinClockwise1, true);

			//reDrawSpinRight(true);
			//reDrawSpin(spinClockwise2, false);
		}

		
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			// Needed only to override the normal painting of the forms;
			// the normal painting caused a ripple effect..
		}

		
		private void Pause1_Click(object sender, System.EventArgs e)
		{
			if (timerDefined == true)
			{
				stateTimer.Dispose();
				timerDefined = false;
			}
		
			dsInterface.stop(sample1);
			dsInterface.stop(256);
			dsInterface.stop(257);
			dsInterface.stop(258);

            cmbSample1.Enabled = true;
		}

		
		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		
		private void record1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				dsInterface.aSound[sample1].SetCurrentPosition(getPlayPosition_1());
				dsInterface.playFromPosition(sample1);
				spinClockwise1 = true;
			}
			else if(e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				dsInterface.aSound[257].SetCurrentPosition(dsInterface.aSound[257].Caps.BufferBytes - getPlayPosition_1());
				dsInterface.playFromPosition(257);
				spinClockwise1 = false;
			}
		}


		private void cmbSample1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			sample1 = cmbSample1.SelectedIndex;
			
			bufferCopy bCopy = new bufferCopy();
			bCopy.CopySample(sample1,257);

			bufferReverse bufRev = new bufferReverse();
			bufRev.reverseSample(257);
			
			bufferFast bFast = new bufferFast();
			bFast.FastForwardSample(sample1,256);
			bFast.FastForwardSample(257,258);

			// Goes better with looped samples;
			// allows the samples in reverse to play, even when they are at the 'begining'
			// of the sample i.e. at the end of the actual buffer.
			dsInterface.loopSample[sample1] = true;
			dsInterface.loopSample[256] = true;
			dsInterface.loopSample[257] = true;
			dsInterface.loopSample[258] = true;
			
			cmbPlay1.Enabled = true;	
		}


		private int getPlayPosition_1()
		{
			// This function gets the play position of the sample( and converts it back 
			// to the base play position; so that it can be converted back to the 
			// correct position where the sample will be played from )  and then
			// stops the sample - this ensures that only one sample is playing at a time

			if (dsInterface.aSound[sample1].Status.Playing == true)
			{
				// The basic play position - no calculation required

				float newCursor = dsInterface.aSound[sample1].PlayPosition;
				
				dsInterface.aSound[sample1].Stop();
				return (int)newCursor;
			}

			else if (dsInterface.aSound[256].Status.Playing == true)
			{
				// The fast forwarded position; the full sample length is divided
				// by the shortened length leaving the offset value. This is 
				// multiplied by the play position to obtain the 'normal' position

				float playOffset = (float)dsInterface.aSound[sample1].Caps.BufferBytes / (float)dsInterface.aSound[256].Caps.BufferBytes;
				float newCursor = playOffset * dsInterface.aSound[256].PlayPosition;
				
				dsInterface.aSound[256].Stop();
				return (int)newCursor;
			}

			else if (dsInterface.aSound[257].Status.Playing == true)
			{
				// The reversed play position; to calculate the normal play position
				// the current play position is subtracted from the maximum length
				// leaving the 'normal' position
				
				float newCursor = dsInterface.aSound[257].Caps.BufferBytes - dsInterface.aSound[257].PlayPosition;
				
				dsInterface.aSound[257].Stop();
				return (int)newCursor;
			}
			
			else if (dsInterface.aSound[258].Status.Playing == true)
			{
				// The reversed and fast forwarded position; the full sample length is divided
				// by the shortened length leaving the offset value. This is 
				// multiplied by the play position to obtain the reversed position. The 
				// reversed position is then subtracted from the 'origional' maximum length
				// to obtain the 'normal' position.

				float playOffset = (float)dsInterface.aSound[257].Caps.BufferBytes / (float)dsInterface.aSound[258].Caps.BufferBytes;
				float newCursor = playOffset * dsInterface.aSound[258].PlayPosition;
				newCursor = dsInterface.aSound[257].Caps.BufferBytes - newCursor;
				
				dsInterface.aSound[258].Stop();
				return (int)newCursor;
			}
			else 
			{
				// In case no sample is playing; this occurs especially in the instance
				// when the samples aren't looped
				return 5;
			}
		}

		
		private void cmbSample2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			sample2 = cmbSample2.SelectedIndex;
			
			bufferCopy bCopy = new bufferCopy();
			bCopy.CopySample(sample2,260);

			bufferReverse bufRev = new bufferReverse();
			bufRev.reverseSample(260);
			
			bufferFast bFast = new bufferFast();
			bFast.FastForwardSample(sample2,259);
			bFast.FastForwardSample(260,261);

			// Goes better with looped samples;
			// allows the samples in reverse to play, even when they are at the 'begining'
			// of the sample i.e. at the end of the actual buffer.
			dsInterface.loopSample[sample2] = true;
			dsInterface.loopSample[259] = true;
			dsInterface.loopSample[260] = true;
			dsInterface.loopSample[261] = true;
			
			cmbPlay2.Enabled = true;
		}

		
		private int getPlayPosition_2()
		{
			// This function gets the play position of the sample( and converts it back 
			// to the base play position; so that it can be converted back to the 
			// correct position where the sample will be played from )  and then
			// stops the sample - this ensures that only one sample is playing at a time

			if (dsInterface.aSound[sample2].Status.Playing == true)
			{
				// The basic play position - no calculation required

				float newCursor = dsInterface.aSound[sample2].PlayPosition;
				
				dsInterface.aSound[sample2].Stop();
				return (int)newCursor;
			}

			else if (dsInterface.aSound[259].Status.Playing == true)
			{
				// The fast forwarded position; the full sample length is divided
				// by the shortened length leaving the offset value. This is 
				// multiplied by the play position to obtain the 'normal' position

				float playOffset = (float)dsInterface.aSound[sample2].Caps.BufferBytes / (float)dsInterface.aSound[259].Caps.BufferBytes;
				float newCursor = playOffset * dsInterface.aSound[259].PlayPosition;
				
				dsInterface.aSound[259].Stop();
				return (int)newCursor;
			}

			else if (dsInterface.aSound[260].Status.Playing == true)
			{
				// The reversed play position; to calculate the normal play position
				// the current play position is subtracted from the maximum length
				// leaving the 'normal' position
				
				float newCursor = dsInterface.aSound[260].Caps.BufferBytes - dsInterface.aSound[260].PlayPosition;
				
				dsInterface.aSound[260].Stop();
				return (int)newCursor;
			}
			
			else if (dsInterface.aSound[261].Status.Playing == true)
			{
				// The reversed and fast forwarded position; the full sample length is divided
				// by the shortened length leaving the offset value. This is 
				// multiplied by the play position to obtain the reversed position. The 
				// reversed position is then subtracted from the 'origional' maximum length
				// to obtain the 'normal' position.

				float playOffset = (float)dsInterface.aSound[260].Caps.BufferBytes / (float)dsInterface.aSound[261].Caps.BufferBytes;
				float newCursor = playOffset * dsInterface.aSound[261].PlayPosition;
				newCursor = dsInterface.aSound[260].Caps.BufferBytes - newCursor;
				
				dsInterface.aSound[261].Stop();
				return (int)newCursor;
			}
			else 
			{
				// In case no sample is playing; this occurs especially in the instance
				// when the samples aren't looped
				return 5;
			}
		}

		
		private void record2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            try
            {

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    dsInterface.aSound[sample2].SetCurrentPosition(getPlayPosition_2());
                    dsInterface.playFromPosition(sample2);
                    spinClockwise2 = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    dsInterface.aSound[260].SetCurrentPosition(dsInterface.aSound[260].Caps.BufferBytes - getPlayPosition_2());
                    dsInterface.playFromPosition(260);
                    spinClockwise2 = false;
                }
            }
            catch 
            {
            
            }
		}

		
		private void cmbPlay2_Click(object sender, System.EventArgs e)
		{
			if (timerDefined2 == true)
			{
				stateTimer2.Dispose();
				timerDefined2 = false;
			}

			timerDefined2 = true;
			
			acurateTime2();
			playTick = 100;

			dsInterface.play(sample2);

            cmbSample2.Enabled = false;
		}

		private void record2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (dsInterface.aSound[259].Status.Playing == false)
				{
					// If the fast forward sample is not playing, the 
					// current position of the origional speed sample
					// is taken and extrapalated against the fast sample
					// to obtain an equivelent cursor position in the fast
					// sample. Play is stoped for the slow sample while the
					// fast sample takes over; bringing the ilusion of 
					// moving the turn table faster..
					float playOffset = (float)dsInterface.aSound[259].Caps.BufferBytes / (float)dsInterface.aSound[sample2].Caps.BufferBytes;
					float newCursor = playOffset * getPlayPosition_2();
					dsInterface.aSound[259].SetCurrentPosition((int)newCursor);
					dsInterface.playFromPosition(259);
					
					if (sample2Reverse == true)
					{
						sample2Reverse = false;
					}					
				}
				reDrawSpinRight(true);
			}

			else if(e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (dsInterface.aSound[261].Status.Playing == false)
				{
					// If the fast forward sample is not playing, the 
					// current position of the origional speed sample
					// is taken and extrapalated against the fast sample
					// to obtain an equivelent cursor position in the fast
					// sample. Play is stoped for the slow sample while the
					// fast sample takes over; bringing the ilusion of 
					// moving the turn table faster..
					float playOffset = (float)dsInterface.aSound[261].Caps.BufferBytes / (float)dsInterface.aSound[260].Caps.BufferBytes;
					float newCursor = playOffset * getPlayPosition_2();
					dsInterface.aSound[261].SetCurrentPosition(dsInterface.aSound[261].Caps.BufferBytes - (int)newCursor);
					dsInterface.playFromPosition(261);
					
					if (sample2Reverse == false)
					{

					}
				}
				reDrawSpinRight(false);
			}
		}

		private void cmbPause2_Click(object sender, System.EventArgs e)
		{
			if (timerDefined2 == true)
			{
				stateTimer2.Dispose();
				timerDefined2 = false;
			}
		

			dsInterface.stop(sample2);
			dsInterface.stop(259);
			dsInterface.stop(260);
			dsInterface.stop(261);


            cmbSample2.Enabled = true;
		}

		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void frmScratch_Deactivate(object sender, System.EventArgs e)
		{
			// Dispose of the timer when disposing the form
			if (timerDefined == true)
			{
				stateTimer.Change(-1,-1);//.Dispose();
				//timerDefined = false;
			}

			// Dispose of the timer when disposing the form
			if (timerDefined2 == true)
			{
				stateTimer2.Change(-1,-1);//.Dispose();
				//timerDefined2 = false;
			}
		}

		private void frmScratch_Activated(object sender, System.EventArgs e)
		{
			// Dispose of the timer when disposing the form
			if (timerDefined == true)
			{
				stateTimer.Change(tick,tick);//.Dispose();
				//timerDefined = false;
			}

			// Dispose of the timer when disposing the form
			if (timerDefined2 == true)
			{
				stateTimer2.Change(tick,tick);//.Dispose();
				//timerDefined2 = false;
			}
		}

        private void crossFader_Scroll(object sender, EventArgs e)
        {
            // Cross fading goodness.
            // Has to take care of the volume for the normal and fast tempo samples.
            if (crossFader.Value == 6000){
                //Center audio - both chanels full volume                
                dsInterface.aSound[256].Volume = 0;
                dsInterface.aSound[257].Volume = 0;
                dsInterface.aSound[258].Volume = 0;
                dsInterface.aSound[cmbSample1.SelectedIndex].Volume = 0;

                dsInterface.aSound[259].Volume = 0;
                dsInterface.aSound[260].Volume = 0;
                dsInterface.aSound[261].Volume = 0;
                dsInterface.aSound[cmbSample2.SelectedIndex].Volume = 0;
            }else{
                if (crossFader.Value > 6000)
                {
                    // Moving right, fade out the left sample
                    //dsInterface.aSound[260].Volume = (crossFader.Value - 6000) * -1;

                    dsInterface.aSound[256].Volume = (crossFader.Value - 6000) * -1;
                    dsInterface.aSound[257].Volume = (crossFader.Value - 6000) * -1;
                    dsInterface.aSound[258].Volume = (crossFader.Value - 6000) * -1;
                    dsInterface.aSound[cmbSample1.SelectedIndex].Volume = (crossFader.Value - 6000) * -1;

                    dsInterface.aSound[259].Volume = 0;
                    dsInterface.aSound[260].Volume = 0;
                    dsInterface.aSound[261].Volume = 0;
                    dsInterface.aSound[cmbSample2.SelectedIndex].Volume = 0;
                }
                else
                {
                    // Moving left, fade out the right sample
                    //dsInterface.aSound[260].Volume = 0;
                    dsInterface.aSound[256].Volume = 0;
                    dsInterface.aSound[257].Volume = 0;
                    dsInterface.aSound[258].Volume = 0;
                    dsInterface.aSound[cmbSample1.SelectedIndex].Volume = 0;

                    dsInterface.aSound[259].Volume = (crossFader.Value - 6000);
                    dsInterface.aSound[260].Volume = (crossFader.Value - 6000);
                    dsInterface.aSound[261].Volume = (crossFader.Value - 6000) ;
                    dsInterface.aSound[cmbSample2.SelectedIndex].Volume = (crossFader.Value - 6000) ;

                }


            }
        
        }




	}
}
