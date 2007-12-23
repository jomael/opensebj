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

using System.Threading;
using System.Diagnostics;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for frmBeatBox.
	/// </summary>
	public class frmBeatBox : System.Windows.Forms.Form
    {

        private System.Windows.Forms.CheckBox[] bBox1;
        private System.Windows.Forms.CheckBox[] bBox2;
        private System.Windows.Forms.CheckBox[] bBox3;
        private System.Windows.Forms.CheckBox[] bBox4;
        private System.Windows.Forms.CheckBox[] bBox5;

        int playPos = 0;
        bool playing = false;
        bool looping = true;

        //---------------Timer----------------------//

        // To stop the tread if its active
        private bool timerDefined = false;

        // Following for timer event fire functions
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        // Create the delegate that invokes methods for the timer.
        TimerCallback timerDelegate;

        // Create a timer that signals the delegate to invoke 
        // CheckStatus 
        System.Threading.Timer stateTimer;

        //---------------END Timer------------------//

        // To keep track of the stop button being pressed twice
        // The second time it is pressed all of the mid playing samples will be stopped
        private bool doubleStop = false;

        // The location of the right hand scroll bar
        private int scrollPosition = 0;
        private Button cmbPlay;
        private Button cmbStop;
        private TextBox txtBPM;
        private Label lblBPM;
        private Label lblBBox1;
        private Label lblBBox2;
        private VScrollBar sampleScroll;
        private Label lblBBox3;
        private Label lblBBox4;
        private Label lblBBox5;
        private CheckBox bBoxTemplate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmBeatBox()
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
            if (timerDefined == true)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBeatBox));
            this.cmbPlay = new System.Windows.Forms.Button();
            this.cmbStop = new System.Windows.Forms.Button();
            this.txtBPM = new System.Windows.Forms.TextBox();
            this.lblBPM = new System.Windows.Forms.Label();
            this.lblBBox1 = new System.Windows.Forms.Label();
            this.lblBBox2 = new System.Windows.Forms.Label();
            this.sampleScroll = new System.Windows.Forms.VScrollBar();
            this.lblBBox3 = new System.Windows.Forms.Label();
            this.lblBBox4 = new System.Windows.Forms.Label();
            this.lblBBox5 = new System.Windows.Forms.Label();
            this.bBoxTemplate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbPlay
            // 
            this.cmbPlay.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlay.Location = new System.Drawing.Point(80, 12);
            this.cmbPlay.Name = "cmbPlay";
            this.cmbPlay.Size = new System.Drawing.Size(80, 24);
            this.cmbPlay.TabIndex = 4;
            this.cmbPlay.Text = "Play";
            this.cmbPlay.UseVisualStyleBackColor = false;
            this.cmbPlay.Click += new System.EventHandler(this.cmbPlay_Click);
            // 
            // cmbStop
            // 
            this.cmbStop.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStop.Location = new System.Drawing.Point(166, 12);
            this.cmbStop.Name = "cmbStop";
            this.cmbStop.Size = new System.Drawing.Size(80, 24);
            this.cmbStop.TabIndex = 32;
            this.cmbStop.Text = "Stop";
            this.cmbStop.UseVisualStyleBackColor = false;
            this.cmbStop.Click += new System.EventHandler(this.cmbStop_Click);
            // 
            // txtBPM
            // 
            this.txtBPM.Location = new System.Drawing.Point(544, 15);
            this.txtBPM.Name = "txtBPM";
            this.txtBPM.Size = new System.Drawing.Size(72, 20);
            this.txtBPM.TabIndex = 17;
            this.txtBPM.Text = "120";
            // 
            // lblBPM
            // 
            this.lblBPM.AutoSize = true;
            this.lblBPM.Location = new System.Drawing.Point(508, 18);
            this.lblBPM.Name = "lblBPM";
            this.lblBPM.Size = new System.Drawing.Size(30, 13);
            this.lblBPM.TabIndex = 18;
            this.lblBPM.Text = "BPM";
            // 
            // lblBBox1
            // 
            this.lblBBox1.AutoSize = true;
            this.lblBBox1.Location = new System.Drawing.Point(55, 56);
            this.lblBBox1.Name = "lblBBox1";
            this.lblBBox1.Size = new System.Drawing.Size(13, 13);
            this.lblBBox1.TabIndex = 19;
            this.lblBBox1.Text = "1";
            // 
            // lblBBox2
            // 
            this.lblBBox2.AutoSize = true;
            this.lblBBox2.Location = new System.Drawing.Point(55, 89);
            this.lblBBox2.Name = "lblBBox2";
            this.lblBBox2.Size = new System.Drawing.Size(13, 13);
            this.lblBBox2.TabIndex = 20;
            this.lblBBox2.Text = "2";
            // 
            // sampleScroll
            // 
            this.sampleScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.sampleScroll.LargeChange = 5;
            this.sampleScroll.Location = new System.Drawing.Point(632, 0);
            this.sampleScroll.Maximum = 255;
            this.sampleScroll.Name = "sampleScroll";
            this.sampleScroll.Size = new System.Drawing.Size(16, 275);
            this.sampleScroll.TabIndex = 21;
            this.sampleScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sampleScroll_Scroll);
            // 
            // lblBBox3
            // 
            this.lblBBox3.AutoSize = true;
            this.lblBBox3.Location = new System.Drawing.Point(55, 125);
            this.lblBBox3.Name = "lblBBox3";
            this.lblBBox3.Size = new System.Drawing.Size(13, 13);
            this.lblBBox3.TabIndex = 22;
            this.lblBBox3.Text = "3";
            // 
            // lblBBox4
            // 
            this.lblBBox4.AutoSize = true;
            this.lblBBox4.Location = new System.Drawing.Point(55, 160);
            this.lblBBox4.Name = "lblBBox4";
            this.lblBBox4.Size = new System.Drawing.Size(13, 13);
            this.lblBBox4.TabIndex = 23;
            this.lblBBox4.Text = "4";
            // 
            // lblBBox5
            // 
            this.lblBBox5.AutoSize = true;
            this.lblBBox5.Location = new System.Drawing.Point(55, 196);
            this.lblBBox5.Name = "lblBBox5";
            this.lblBBox5.Size = new System.Drawing.Size(13, 13);
            this.lblBBox5.TabIndex = 24;
            this.lblBBox5.Text = "5";
            // 
            // bBoxTemplate
            // 
            this.bBoxTemplate.Appearance = System.Windows.Forms.Appearance.Button;
            this.bBoxTemplate.Location = new System.Drawing.Point(12, 10);
            this.bBoxTemplate.Name = "bBoxTemplate";
            this.bBoxTemplate.Size = new System.Drawing.Size(28, 29);
            this.bBoxTemplate.TabIndex = 3;
            this.bBoxTemplate.Text = "Off";
            this.bBoxTemplate.UseVisualStyleBackColor = true;
            this.bBoxTemplate.Visible = false;
            this.bBoxTemplate.CheckedChanged += new System.EventHandler(this.bBoxTemplate_CheckedChanged);
            // 
            // frmBeatBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(648, 275);
            this.Controls.Add(this.lblBBox5);
            this.Controls.Add(this.lblBBox4);
            this.Controls.Add(this.lblBBox3);
            this.Controls.Add(this.sampleScroll);
            this.Controls.Add(this.lblBBox2);
            this.Controls.Add(this.lblBBox1);
            this.Controls.Add(this.lblBPM);
            this.Controls.Add(this.txtBPM);
            this.Controls.Add(this.cmbStop);
            this.Controls.Add(this.cmbPlay);
            this.Controls.Add(this.bBoxTemplate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmBeatBox";
            this.Text = "Beat Box";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmBeatBox_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBeatBox_KeyDown);
            this.Load += new System.EventHandler(this.frmBeatBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmBeatBox_Load(object sender, System.EventArgs e)
		{

            bBox1 = new System.Windows.Forms.CheckBox[32];
            bBox2 = new System.Windows.Forms.CheckBox[32];
            bBox3 = new System.Windows.Forms.CheckBox[32];
            bBox4 = new System.Windows.Forms.CheckBox[32];
            bBox5 = new System.Windows.Forms.CheckBox[32];

            int i = 0;

            for (i = 0; i < 32; i++)
            {

                //----1------//

                this.bBox1[i] = new System.Windows.Forms.CheckBox();

                this.bBox1[i].Appearance = System.Windows.Forms.Appearance.Button;
                //this.bBox1[i].BackColor = System.Drawing.Color.Gray;
                this.bBox1[i].Location = new System.Drawing.Point(80 + (i * (6 + 28)), 48);
                this.bBox1[i].Name = "bBoxTemplate";
                this.bBox1[i].Size = new System.Drawing.Size(28, 29);
                this.bBox1[i].TabIndex = 3;
                this.bBox1[i].Text = "Off";
                //this.bBox1[i].UseVisualStyleBackColor = false;
                this.bBox1[i].CheckedChanged += new System.EventHandler(this.bBox1_CheckedChanged);

                this.Controls.Add(this.bBox1[i]);

                //----2------//

                this.bBox2[i] = new System.Windows.Forms.CheckBox();

                this.bBox2[i].Appearance = System.Windows.Forms.Appearance.Button;
                //this.bBox2[i].BackColor = System.Drawing.Color.Gray;
                this.bBox2[i].Location = new System.Drawing.Point(80 + (i * (6 + 28)), 48 + 29 + 6);
                this.bBox2[i].Name = "bBoxTemplate";
                this.bBox2[i].Size = new System.Drawing.Size(28, 29);
                this.bBox2[i].TabIndex = 3;
                this.bBox2[i].Text = "Off";
                //this.bBox2[i].UseVisualStyleBackColor = false;
                this.bBox2[i].CheckedChanged += new System.EventHandler(this.bBox2_CheckedChanged);

                this.Controls.Add(this.bBox2[i]);


                //----3------//

                this.bBox3[i] = new System.Windows.Forms.CheckBox();

                this.bBox3[i].Appearance = System.Windows.Forms.Appearance.Button;
                //this.bBox3[i].BackColor = System.Drawing.Color.Gray;
                this.bBox3[i].Location = new System.Drawing.Point(80 + (i * (6 + 28)), 48 + ((29 + 6) * 2));
                this.bBox3[i].Name = "bBoxTemplate";
                this.bBox3[i].Size = new System.Drawing.Size(28, 29);
                this.bBox3[i].TabIndex = 3;
                this.bBox3[i].Text = "Off";
                //this.bBox3[i].UseVisualStyleBackColor = false;
                this.bBox3[i].CheckedChanged += new System.EventHandler(this.bBox3_CheckedChanged);

                this.Controls.Add(this.bBox3[i]);

                //----4------//

                this.bBox4[i] = new System.Windows.Forms.CheckBox();

                this.bBox4[i].Appearance = System.Windows.Forms.Appearance.Button;
                //this.bBox4[i].BackColor = System.Drawing.Color.Gray;
                this.bBox4[i].Location = new System.Drawing.Point(80 + (i * (6 + 28)), 48 + ((29 + 6) * 3));
                this.bBox4[i].Name = "bBoxTemplate";
                this.bBox4[i].Size = new System.Drawing.Size(28, 29);
                this.bBox4[i].TabIndex = 3;
                this.bBox4[i].Text = "Off";
                //this.bBox4[i].UseVisualStyleBackColor = false;
                this.bBox4[i].CheckedChanged += new System.EventHandler(this.bBox4_CheckedChanged);

                this.Controls.Add(this.bBox4[i]);

                //----5------//

                this.bBox5[i] = new System.Windows.Forms.CheckBox();

                this.bBox5[i].Appearance = System.Windows.Forms.Appearance.Button;
                //this.bBox5[i].BackColor = System.Drawing.Color.Gray;
                this.bBox5[i].Location = new System.Drawing.Point(80 + (i * (6 + 28)), 48 + ((29 + 6) * 4));
                this.bBox5[i].Name = "bBoxTemplate";
                this.bBox5[i].Size = new System.Drawing.Size(28, 29);
                this.bBox5[i].TabIndex = 3;
                this.bBox5[i].Text = "Off";
                //this.bBox5[i].UseVisualStyleBackColor = false;
                this.bBox5[i].CheckedChanged += new System.EventHandler(this.bBox5_CheckedChanged);

                this.Controls.Add(this.bBox5[i]); 
            }


            // Set-em Up
            ReCheckBeats();
        }




        private void bBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                if (sender.Equals(bBox1[i]))


                    if (bBox1[i].Checked == true)
                    {
                        bBox1[i].BackColor = Color.Red;
                        bBox1[i].Text = "On";

                        beatBox.setBeatOn(scrollPosition, i);
                    }
                    else
                    {
                        bBox1[i].BackColor = this.BackColor;
                        bBox1[i].Text = "Off";

                        beatBox.setBeatOff(scrollPosition, i);
                    }
            }
        }

        private void ReCheckBeats()
        {

            for (int i = 0; i < 32; i++)
            {
                // Resets beats for top level of beats
                if (beatBox.getBeatStat(scrollPosition, i) == true)
                {
                    bBox1[i].BackColor = Color.Red;
                    bBox1[i].Text = "On";
                    bBox1[i].Checked = true;
                }
                else
                {
                    bBox1[i].BackColor = this.BackColor;
                    bBox1[i].Text = "Off";
                    bBox1[i].Checked = false;
                }


                // Resets beats for second level of beats
                if (beatBox.getBeatStat(scrollPosition + 1, i) == true)
                {
                    bBox2[i].BackColor = Color.Red;
                    bBox2[i].Text = "On";
                    bBox2[i].Checked = true;
                }
                else
                {
                    bBox2[i].BackColor = this.BackColor;
                    bBox2[i].Text = "Off";
                    bBox2[i].Checked = false;
                }


                // Resets beats for third level of beats
                if (beatBox.getBeatStat(scrollPosition + 2, i) == true)
                {
                    bBox3[i].BackColor = Color.Red;
                    bBox3[i].Text = "On";
                    bBox3[i].Checked = true;
                }
                else
                {
                    bBox3[i].BackColor = this.BackColor;
                    bBox3[i].Text = "Off";
                    bBox3[i].Checked = false;
                }


                // Resets beats for forth level of beats
                if (beatBox.getBeatStat(scrollPosition + 3, i) == true)
                {
                    bBox4[i].BackColor = Color.Red;
                    bBox4[i].Text = "On";
                    bBox4[i].Checked = true;
                }
                else
                {
                    bBox4[i].BackColor = this.BackColor;
                    bBox4[i].Text = "Off";
                    bBox4[i].Checked = false;
                }


                // Resets beats for fifth level of beats
                if (beatBox.getBeatStat(scrollPosition + 4, i) == true)
                {
                    bBox5[i].BackColor = Color.Red;
                    bBox5[i].Text = "On";
                    bBox5[i].Checked = true;
                }
                else
                {
                    bBox5[i].BackColor = this.BackColor;
                    bBox5[i].Text = "Off";
                    bBox5[i].Checked = false;
                }
            }
        }


        private void bBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                if (sender.Equals(bBox2[i]))


                    if (bBox2[i].Checked == true)
                    {
                        bBox2[i].BackColor = Color.Red;
                        bBox2[i].Text = "On";

                        beatBox.setBeatOn(scrollPosition + 1, i);

                    }
                    else
                    {
                        bBox2[i].BackColor = this.BackColor;
                        bBox2[i].Text = "Off";
                        
                        beatBox.setBeatOff(scrollPosition + 1, i);
                    }
            }
        }

        private void bBox3_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                if (sender.Equals(bBox3[i]))


                    if (bBox3[i].Checked == true)
                    {
                        bBox3[i].BackColor = Color.Red;
                        bBox3[i].Text = "On";
                        
                        beatBox.setBeatOn(scrollPosition + 2, i);
                    }
                    else
                    {
                        bBox3[i].BackColor = this.BackColor;
                        bBox3[i].Text = "Off";
                        
                        beatBox.setBeatOff(scrollPosition + 2, i);
                    }
            }
        }

        private void bBox4_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                if (sender.Equals(bBox4[i]))


                    if (bBox4[i].Checked == true)
                    {
                        bBox4[i].BackColor = Color.Red;
                        bBox4[i].Text = "On";

                        beatBox.setBeatOn(scrollPosition + 3, i);
                    }
                    else
                    {
                        bBox4[i].BackColor = this.BackColor;
                        bBox4[i].Text = "Off";

                        beatBox.setBeatOff(scrollPosition + 3, i);
                    }
            }
        }


        private void bBox5_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                if (sender.Equals(bBox5[i]))


                    if (bBox5[i].Checked == true)
                    {
                        bBox5[i].BackColor = Color.Red;
                        bBox5[i].Text = "On";

                        beatBox.setBeatOn(scrollPosition + 4, i);
                    }
                    else
                    {
                        bBox5[i].BackColor = this.BackColor;
                        bBox5[i].Text = "Off";

                        beatBox.setBeatOff(scrollPosition + 4, i);
                    }
            }
        }



        public void redrawMarks()
        {
            //Clear form
            Graphics g = this.CreateGraphics();

            // Draw baseline back marks

            SolidBrush whiteBrush = new SolidBrush(Color.White);
            
            Pen timeMarkers = new Pen(whiteBrush, 26);

            System.Drawing.Point p5;
            System.Drawing.Point p6;

            for (int i = 0; i < 32; i++)
            {
                p5 = new Point(93 + (i * (6 + 28)), 230);
                p6 = new Point(93 + (i * (6 + 28)), 240);
                g.DrawLine(timeMarkers, p5, p6);
            }

            // Now draw play marker
            if (playing == true)
            {
                SolidBrush myBrush = new SolidBrush(Color.Green);
                // Creates a pen with the same display properties as myBrush and a 
                // thickness of 26. Surprisingly enough the same as the back marks ;-)
                Pen myPen = new Pen(myBrush, 26);
                System.Drawing.Point p1 = new Point(93 + (playPos * (6 + 28)), 230);
                System.Drawing.Point p2 = new Point(93 + (playPos * (6 + 28)), 240);

                g.DrawLine(myPen, p1, p2);
            }

        }


        private void frmBeatBox_Paint(object sender, PaintEventArgs e)
        {
            redrawMarks();
        }

        private void cmbPlay_Click(object sender, EventArgs e)
        {
            if (timerDefined == true)
            {
                stateTimer.Dispose();
                timerDefined = false;
            }

            playing = true;

            // Reset the doubleStop (so that if stop is pressed twice it stop playing the
            // samples mid play)
            doubleStop = false;

            acurateTime();
        }


        private void cmbStop_Click(object sender, System.EventArgs e)
        {
            if (timerDefined == true)
            {
                stateTimer.Dispose();
                timerDefined = false;
            }


            // Code to for the doubleStop (so that if stop is pressed twice it stop 
            // playing the samples mid play)
            if (doubleStop == false)
            {
                doubleStop = true;
            }
            else
            {
                doubleStop = false;

                for (int i = 0; i <= dsInterface.loadedSamples; i++)
                {

                    dsInterface.aSound[i].Stop();

                }
            }

        }

        private void acurateTime()
        {
            // Create the delegate that invokes methods for the timer.
            timerDelegate = new TimerCallback(goGo);

            // Otherwise first beat is missed
            playPos = -1; 

            // Create a timer that signals the delegate to invoke 
            // CheckStatus imidiatealy (0 milliseconds), and every 10 milliseconds
            // thereafter.
            try
            {
                float BPM = (((60 / float.Parse(this.txtBPM.Text)) * 1000) / 4);

                stateTimer = new System.Threading.Timer(timerDelegate, autoEvent, 0, (int)BPM);

                // Used so that if the play button is clicked a second time
                // the timer can be disposed of saftley.
                timerDefined = true;

            }catch
            {
                System.Windows.Forms.MessageBox.Show("Please make sure the BPM you have entered is a numeric" + this.txtBPM.Text.ToString());
            }  
        }

        private void goGo(Object stateInfo)
        {
            try
            {
                playPos++;

                // If it hits the loop, back to the begining please
                if (playPos == 32 && looping == true)
                {
                    playPos = 0;
                    redrawMarks();
                }

                // For all of the loaded samples, check if any sample needs to be played
                // This is basically the bit which makes the beat box work. If you were hard core
                // you could just adjust the array manually - but where is the fun in that ;-)
                for (int i = 0; i <= dsInterface.loadedSamples; i++)
                {
                    if (beatBox.getBeatStat(i, playPos) == true)
                    {
                        dsInterface.play(i);
                    }
                }









                ////// This will need to happen regardless of if the loop has
                ////// been hit or not.
                ////for (int i = 0; i < 32; i++)
                ////{
                //    if (bBox1[playPos].Checked == true)
                //    {
                //        // If the sample hasn't been loaded; an error will be thrown
                //        // and no other samples will be played at the same position
                //        dsInterface.play(0);
                //    }
                //    if (bBox2[playPos].Checked == true)
                //    {
                //        // If the sample hasn't been loaded; an error will be thrown
                //        // and no other samples will be played at the same position
                //        dsInterface.play(1);
                //    }
                //    if (bBox3[playPos].Checked == true)
                //    {
                //        // If the sample hasn't been loaded; an error will be thrown
                //        // and no other samples will be played at the same position
                //        dsInterface.play(2);
                //    }
                //    if (bBox4[playPos].Checked == true)
                //    {
                //        // If the sample hasn't been loaded; an error will be thrown
                //        // and no other samples will be played at the same position
                //        dsInterface.play(3);
                //    }
                //    if (bBox5[playPos].Checked == true)
                //    {
                //        // If the sample hasn't been loaded; an error will be thrown
                //        // and no other samples will be played at the same position
                //        dsInterface.play(4);
                //    }










                //}

                // Was causing the performance issue when previously refreshing 
                // the whole screen. Now only the new pen mark is drawn which greatly
                // imrpoves the situation and means that the samples firing exactly 
                // on cue.
                redrawMarks();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Exception within firing event - " + e.ToString());
            }
        }




        private void sampleScroll_Scroll(object sender, ScrollEventArgs e)
        {
            scrollSamples();
        }


        private void scrollSamples()
        {
            if (scrollPosition != sampleScroll.Value)
            {
                scrollPosition = sampleScroll.Value;

                int temp = 0;
                temp = sampleScroll.Value + 1;
                lblBBox1.Text = temp.ToString();

                temp = sampleScroll.Value + 2;
                lblBBox2.Text = temp.ToString();

                temp = sampleScroll.Value + 3;
                lblBBox3.Text = temp.ToString();

                temp = sampleScroll.Value + 4;
                lblBBox4.Text = temp.ToString();

                temp = sampleScroll.Value + 5;
                lblBBox5.Text = temp.ToString();


                ReCheckBeats();







                ////Gets rid of the inActionPlays so that they can be readded
                //for (int i = 0; i < globalSettings.inActionPlays; i++)
                //{
                //    try
                //    {
                //        this.labels[i].Dispose();
                //    }
                //    catch
                //    {
                //        // After re-opening can't disposes sample labels which don't yet exist? 
                //    }

                //}

                //redrawPlaysAfterScroll();
            }

        }








        // Template methods not used

        private void bBoxTemplate_CheckedChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < 32; i++)
            //{
            //    if (sender.Equals(bBox1[i]))


            if (bBoxTemplate.Checked == true)
            {
                bBoxTemplate.BackColor = Color.Red;
                bBoxTemplate.Text = "On";
            }
            else
            {
                bBoxTemplate.BackColor = Color.Gray;
                bBoxTemplate.Text = "Off";
            }
            //}
        }

        private void frmBeatBox_KeyDown(object sender, KeyEventArgs e)
        {
            dsInterface.playKey(e.KeyValue, e.Control);
        }


        //private void cmbSample_Click(object sender, System.EventArgs e)
        //{
        //    for (int i = 0; i < 32; i++)
        //    {
        //        if (sender.Equals(cmbSample[i]))
        //        {
        //            dsInterface.play(i + beatPadScroll.Value * 4);
        //        }

        //    }
        //}



	}
}
