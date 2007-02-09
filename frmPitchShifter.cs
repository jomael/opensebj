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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenSebJ
{
    public partial class frmPitchShifter : Form
    {
        int _sample;
        bool[] _enabled = new bool[8];

        public frmPitchShifter(int sample)
        {
            InitializeComponent();

            _sample = sample;
        }

        private void frmPitchShifter_Load(object sender, EventArgs e)
        {
            //int i = 0;

            int _TabIndex = 0;
            

            this.trkFreq = new System.Windows.Forms.TrackBar[8];
            //this.cmbEnable = new System.Windows.Forms.Button[8];
            this.cmbEnable = new System.Windows.Forms.CheckBox[8];


            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmBeatPad));

            for (int i = 0; i < 8; i++)
            {
                // The track bar
                this.trkFreq[i] = new TrackBar();
                ((System.ComponentModel.ISupportInitialize)(this.trkFreq[i])).BeginInit();
                
                //this.trkFreq[i].BackColor = System.Drawing.Color.Black;
                this.trkFreq[i].Location = new System.Drawing.Point(70, 48 * i + 10);
                //this.trkFreq[i].Maximum = 44000;
                this.trkFreq[i].Name = "trkFreq" + i.ToString();
                this.trkFreq[i].Size = new System.Drawing.Size(376, 42);
                this.trkFreq[i].TabIndex = _TabIndex++;
                this.trkFreq[i].TabStop = false;
                this.trkFreq[i].TickStyle = System.Windows.Forms.TickStyle.None;
                //this.trkFreq[i].Value = 22000;


                this.trkFreq[i].Minimum = 4000;
                this.trkFreq[i].Maximum = dsInterface.getFrequency(_sample) + 3000;
                this.trkFreq[i].Value = dsInterface.getCurrentFrequency(_sample);

                // set default
                //dsInterface.setFreqRoll(_sample, i, trkFreq[i].Value);

                //this.trkFreq[i].Click += new System.EventHandler(this.trkFreq_Click);
                this.trkFreq[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.trkFreq_MouseDown);
                this.trkFreq[i].MouseEnter += new System.EventHandler(this.trkFreq_MouseEnter);
                this.trkFreq[i].ValueChanged += new System.EventHandler(this.trkFreq_ValueChanged);

                this.trkFreq[i].Visible = false;


                // The Enable Button
                //this.cmbEnable[i] = new Button();
                this.cmbEnable[i] = new CheckBox();
                //((System.ComponentModel.ISupportInitialize)(this.cmbEnable[i])).BeginInit();

                this.cmbEnable[i].Appearance = Appearance.Button;
                //this.cmbEnable[i].BackColor = System.Drawing.Color.Black;
                //this.cmbEnable[i].BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbSetKey.BackgroundImage")));
                this.cmbEnable[i].Cursor = System.Windows.Forms.Cursors.Default;
                this.cmbEnable[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //this.cmbEnable[i].ForeColor = System.Drawing.Color.White;
                this.cmbEnable[i].Location = new System.Drawing.Point(8, 48 * i + 17);
                this.cmbEnable[i].Name = "cmbEnable";
                this.cmbEnable[i].Size = new System.Drawing.Size(56, 24);
                this.cmbEnable[i].TabIndex = 50 + i;
                this.cmbEnable[i].Text = "Enable";
                this.cmbEnable[i].UseVisualStyleBackColor = false;
                this.cmbEnable[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
                this.cmbEnable[i].Click += new System.EventHandler(this.cmbEnable_Click);


                //Set them all to not enable
                _enabled[i] = false;
                
            }

            for (int i = 0; i < 8; i++)
            {
                this.Controls.Add(this.trkFreq[i]);
                ((System.ComponentModel.ISupportInitialize)(this.trkFreq[i])).EndInit();
            
                this.Controls.Add(this.cmbEnable[i]);
                //((System.ComponentModel.ISupportInitialize)(this.cmbEnable[i])).EndInit();    
            
            }

            //int readSample = _sample + 1;
            //this.Text = this.Text + " - " + readSample.ToString();
            this.Text = "Sample Pitch Roller :: " + globalSettings.osj.sampleDetails_sampleName[_sample];

            // Setup to pre-loaded / pre-configured values - previously this was being stored but not visible when reopened
            for (int i = 0; i < 8; i++)
            {

                if (dsInterface.aSample[_sample].getFreqByPos(i) > -1)
                {
                    _enabled[i] = true;
                    cmbEnable[i].Checked = true;
                    cmbEnable[i].Text = "Enabled";
                    trkFreq[i].Visible = true;
                    trkFreq[i].Value = dsInterface.aSample[_sample].getFreqByPos(i);

                    //System.Windows.Forms.MessageBox.Show(dsInterface.aSample[_sample].getFreqByPos(i).ToString());
                }
                else
                {
                    _enabled[i] = false;
                    cmbEnable[i].Checked = false;
                    cmbEnable[i].Text = "Enable";
                    trkFreq[i].Visible = false;
                }
            }
            


        }


        private void trkFreq_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (sender.Equals(trkFreq[i]))
                {
                    dsInterface.setFreqRoll(_sample, i, trkFreq[i].Value);
                }
            }
        }

        private void trkFreq_ValueChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (sender.Equals(trkFreq[i]))
                {
                    dsInterface.setFreqRoll(_sample, i, trkFreq[i].Value);
                }
            }
        }

        private void trkFreq_MouseEnter(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (sender.Equals(trkFreq[i]))
                {
                    // Otherwise the control choses to select itself once a
                    // mouse down event is fired - it then does not re-handle the
                    // mose down event. I.e. first down to select, next one allows
                    // movement to be tracked. Sort of anoying. This forces control
                    // select on enter. Also this seems to only be an issue for
                    // track bars in an array. If any one from MS reads this,
                    // can you fix this in the next drame work release? Please ;-)
                    trkFreq[i].Select();
                    
                }
            }
        }

        private void cmbEnable_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (sender.Equals(cmbEnable[i]))
                {
                    if (_enabled[i] == false){

                        _enabled[i] = true;
                        cmbEnable[i].Text = "Enabled";
                        trkFreq[i].Visible = true;
                        dsInterface.setFreqRollEnabled(_sample,i,true);
                        dsInterface.setFreqRoll(_sample, i, trkFreq[i].Value);
                    }
                    else
                    {
                        _enabled[i] = false;
                        cmbEnable[i].Text = "Enable";
                        trkFreq[i].Visible = false;
                        dsInterface.setFreqRollEnabled(_sample, i,false);
                    }
                }
            }
        }


    }// End of class
}