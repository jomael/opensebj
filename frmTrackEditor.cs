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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace OpenSebJ
{
    public partial class frmTrackEditor : Form
    {
        // The user control tracks
        private ctrlTrack[] _ctrlTrack = new ctrlTrack[256];

        // The cuurent location of the play cursor
        //private int _playTick = globalSettings.osj.TrackEditor_BaseTick;

        // To stop the tread if its active
        private bool timerDefined = false;

        // Following for timer event fire functions
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        // Create the delegate that invokes methods for the timer.
        TimerCallback timerDelegate;

        // Create a timer that signals the delegate to invoke CheckStatus 
        System.Threading.Timer stateTimer;

        // Used to stop all playing samples.
        private bool doubleStop = false;

        // Used for the position of the loop before it was attempted to be moved
        private int loopPrePosition = 0;

        public frmTrackEditor()
        {
            InitializeComponent();
        }

        private void frmTrackEditor_Load(object sender, EventArgs e)
        {
            // Ensure that the form gets the key press events before the control with focus
            this.KeyPreview = true;
            
            // Need to do this before adding the controls, otherwise would trigger multiple screen redraws
            this.SuspendLayout();

            // Draws all of the possiable tracks initally
            for (int i = 0; i < 256; i++)
            {
                this._ctrlTrack[i] = new OpenSebJ.ctrlTrack(i);

                this._ctrlTrack[i].BackColor = System.Drawing.Color.White;
                this._ctrlTrack[i].Location = new System.Drawing.Point(0, globalSettings.osj.TrackEditor_GlobalHeightOffset + (i * (globalSettings.osj.TrackEditor_Offset + globalSettings.osj.TrackEditor_TrackHeight)));
                this._ctrlTrack[i].Name = "ctrlTrack" + i.ToString();
                this._ctrlTrack[i].Size = new System.Drawing.Size(10000, globalSettings.osj.TrackEditor_TrackHeight);
                this._ctrlTrack[i].TabIndex = i + 2;


                // CR 1661564
                // Display the track name after it has been loaded
                if (globalSettings.osj.sampleLoaded[i] == true)
                {
                    this._ctrlTrack[i].lblTrack.Text = globalSettings.osj.sampleDetails_sampleName[i];
                }
                else
                {
                    // No sample loaded yet
                    this._ctrlTrack[i].lblTrack.Text = i.ToString();
                }


                //this._ctrlTrack[i].

                this.Controls.Add(this._ctrlTrack[i]);

            }

            // Set the loop height to the same height as the form
            lblLoop.Height = 256 * (globalSettings.osj.TrackEditor_Offset + globalSettings.osj.TrackEditor_TrackHeight);

            // Reload the loop settings
            lblLoop.Left = globalSettings.osj.TrackEditor_LoopLocation;
            lblLoop.Visible = globalSettings.osj.TrackEditor_LoopVisible;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        //private void frmTrackEditor_Resize(object sender, EventArgs e)
        //{
        //    //this._ctrlTrack[0].Size = new System.Drawing.Size(10000, 100);
        //}

        private void playToolStripButton_Click(object sender, EventArgs e)
        {
            if (timerDefined == true)
            {
                stateTimer.Dispose();
                timerDefined = false;
            }

            // Check if their are any samples at the begining position to play
            for (int i = 0; i < globalSettings.osj.TrackEditor_SampleInstance_InAction; i++)
            {
                if (globalSettings.osj.TrackEditor_SampleInstance_Location[i] == globalSettings.osj.TrackEditor_PlayTick)
                {
                    if (globalSettings.osj.TrackEditor_SampleInstance_Enabled[i] == true)
                    {
                        dsInterface.play(globalSettings.osj.TrackEditor_SampleInstance_Sample[i]);
                    }
                }
            }
            this.Refresh();

            //txtTick.Text = (60 / int.Parse(txtBPM.Text)) * 1000

            // Reset the doubleStop (so that if stop is pressed twice it stop playing the
            // samples mid play)
            doubleStop = false;

            acurateTime();
        }


        private void acurateTime()
        {
            // Create the delegate that invokes methods for the timer.
            timerDelegate = new TimerCallback(goGo);

            // Create a timer that signals the delegate to invoke 
            // CheckStatus imidiatealy (0 milliseconds), and every 10 milliseconds
            // thereafter.
            stateTimer = new System.Threading.Timer(timerDelegate, autoEvent, 0, globalSettings.osj.TrackEditor_Tick);

            // Used so that if the play button is clicked a second time
            // the timer can be disposed of saftley.
            timerDefined = true;
        }

        private void goGo(Object stateInfo)
        {
            try
            {
                globalSettings.osj.TrackEditor_PlayTick++;

                // If it hits the loop, back to the begining please
                if (globalSettings.osj.TrackEditor_PlayTick == lblLoop.Left && lblLoop.Visible == true)
                {
                    // Offset reduced by one to ensure that the the next trigger of GoGo is at the BaseTick
                    globalSettings.osj.TrackEditor_PlayTick = globalSettings.osj.TrackEditor_BaseTick -1;
                    
                    // Clean the form where the play cursor has been drawn
                    cleanBack();
                }

                // This will need to happen regardless of if the loop has
                // been hit or not.
                for (int i = 0; i < globalSettings.osj.TrackEditor_SampleInstance_InAction; i++)
                {
                    if (globalSettings.osj.TrackEditor_SampleInstance_Location[i] == globalSettings.osj.TrackEditor_PlayTick)
                    {
                        if (globalSettings.osj.TrackEditor_SampleInstance_Enabled[i] == true)
                        {
                            // If the sample hasn't been loaded; an error will be thrown
                            // and no other samples will be played at the same position
                            dsInterface.play(globalSettings.osj.TrackEditor_SampleInstance_Sample[i]);
                        }
                    }
                }

                // Was causing the performance issue when previously refreshing 
                // the whole screen. Now only the new pen mark is drawn which greatly
                // imrpoves the situation and means that the samples firing exactly 
                // on cue.
                
                //TODO : Show the play only
                drawPlayOnly();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Exception within firing event - " + e.ToString());
            }
        }

        private void resetToolStripButton_Click(object sender, EventArgs e)
        {
            int tickOffset = 0;

            // If playing, add an offset to make sure the next fire of GoGo starts at the very begining
            if (timerDefined == true)
            {
                tickOffset = 1;
            }
            else
            {
                // CR 1661562 
                // If the timer is not defined then reset the wave positions 
                // when the reset button is pressed
                for (int i = 0; i < 262; i++)
                {
                    if (globalSettings.osj.sampleLoaded[i])
                    {
                        dsInterface.aSound[i].SetCurrentPosition(0);
                    }
                }
            }
            
            globalSettings.osj.TrackEditor_PlayTick = globalSettings.osj.TrackEditor_BaseTick - tickOffset;

            cleanBack();
        }


        private void drawPlayOnly()
        {

            Graphics g = this.CreateGraphics();

            SolidBrush myBrush = new SolidBrush(Color.Red);
            // Creates a pen with the same display properties as myBrush and a 
            // thickness of 5.
            Pen myPen = new Pen(myBrush, 2);
            System.Drawing.Point p1 = new Point(globalSettings.osj.TrackEditor_PlayTick - this.HorizontalScroll.Value, globalSettings.osj.TrackEditor_GlobalHeightOffset);
            System.Drawing.Point p2 = new Point(globalSettings.osj.TrackEditor_PlayTick - this.HorizontalScroll.Value, 26000);

            g.DrawLine(myPen, p1, p2);

        }

        private void cleanBack()
        {
            // Clean away the play cursor

            Graphics g = this.CreateGraphics();

            SolidBrush myBrush = new SolidBrush(this.BackColor);

            g.FillRectangle(myBrush, 0, 0, this.Width, this.Height);

        }


        private void stopToolStripButton_Click(object sender, EventArgs e)
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

                for (int i = 0; i < 262; i++)
                {
                    if (globalSettings.osj.sampleLoaded[i])
                    {
                        dsInterface.aSound[i].Stop();
                    }
                }
            }
        }

        private void ControlsToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void keyRecordStripButton_Click(object sender, EventArgs e)
        {

            // Set the global settings for recording key strokes
            if (keyRecordStripButton.Text == "Key Record")
            {
                keyRecordStripButton.Text = "Stop Record";
                globalSettings.keyRecord = true;
            }
            else
            {
                keyRecordStripButton.Text = "Key Record";
                globalSettings.keyRecord = false;
            }

        }

        private void frmTrackEditor_KeyDown(object sender, KeyEventArgs e)
        {
            dsInterface.playKey(e.KeyValue, e.Control);

            if (globalSettings.keyRecord == true)
            {
                int theSample = dsInterface.getKeySample(e.KeyValue);
                if (theSample != -1)
                {
                    int theSampleIncOffset = theSample + 1;

                    // Adds the play to the current correct sample position by using 
                    // the current tick location
                    _ctrlTrack[theSample].keyRecordNewInstance(globalSettings.osj.TrackEditor_PlayTick);

                }
            }
        }

        private void lblLoop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //clearPopUp();
                if (globalSettings.osj.TrackEditor_BaseTick <= lblLoop.Left + e.X - loopPrePosition)
                {
                    lblLoop.Left = lblLoop.Left + e.X - loopPrePosition;
                    globalSettings.osj.TrackEditor_LoopLocation = lblLoop.Left;
                }
                else
                {
                    lblLoop.Left = globalSettings.osj.TrackEditor_BaseTick;
                    globalSettings.osj.TrackEditor_LoopLocation = lblLoop.Left;
                }
            }
        }

        private void lblLoop_MouseDown(object sender, MouseEventArgs e)
        {
            loopPrePosition = e.X;
            lblLoop.BringToFront();
        }

        private void loopToolStripButton_Click(object sender, EventArgs e)
        {
            if (loopToolStripButton.Text == "Set Loop")
            {
                // Set the loop to the height of the form
                //lblLoop.Height = this.Height - globalSettings.osj.TrackEditor_GlobalHeightOffset - globalSettings.osj.TrackEditor_Offset;
                
                loopToolStripButton.Text = "Hide Loop";
                lblLoop.Visible = true;
                globalSettings.osj.TrackEditor_LoopVisible = true;
            }
            else
            {
                loopToolStripButton.Text = "Set Loop";
                lblLoop.Visible = false;
                globalSettings.osj.TrackEditor_LoopVisible = false;
            }  
        }


    }// End Class
}// End Namespace