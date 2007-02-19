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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenSebJ
{
    public partial class ctrlTrack : UserControl
    {
        // Save a local reference to what sample the track represents
        int _sample;

        // Number of sample positions stored for this track
        int _storedInstances;

        // The array of labels to represent the samples and their respective play positions
        System.Windows.Forms.Label[] _sampleInstance = new Label[50000];

        // Array used to keep relation for the local sample label and it instance in the global settings; so that the right 
        // sample location is used when it is moved.
        int[] _globalSampleInstanceRelation = new int[50000];

        // Where the label was initally clicked - used to make the movement of the sample set from an offset position
        int[] initalClickX = new int[50000];



        public ctrlTrack(int thisSample)
        {
            InitializeComponent();

            _sample = thisSample;
        }

        private void ctrlTrack_Load(object sender, EventArgs e)
        {

            //System.Windows.Forms.MessageBox.Show("globalSettings.osj.TrackEditor_MaxInstances" + globalSettings.osj.TrackEditor_MaxInstances.ToString());


            for (int i = 0; i < globalSettings.osj.TrackEditor_MaxInstances; i++)
            {
                if (globalSettings.osj.TrackEditor_SampleInstance_Enabled[i] == true)
                {
                    if (globalSettings.osj.TrackEditor_SampleInstance_Sample[i] == _sample)
                    {
                        // Add any existing sample instances to the track editor instance
                        addExistingSampleInstance(i);
                    }
                }
            }
        }

        /// <summary>
        /// Add sample to track; both the sample image on the track and save the location to global settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_Click(object sender, EventArgs e)
        {
            // First check if the sample is loaded - if not then prompt to load a sample
            if (globalSettings.osj.sampleLoaded[_sample] == false)
            {
                if (DialogResult.OK == loadSamples.ShowDialog())
                {
                    string sampleLocation = loadSamples.FileName.ToString();

                    // Attempt to load the sample
                    string error = dsInterface.loadSample(sampleLocation, _sample);

                    if (error != "")
                    {
                        // Display the error if the file didn't load correctly
                        System.Windows.Forms.MessageBox.Show("There was an error loading sample number " + _sample.ToString() + ", this sample has not been loaded and can not be played. There are a few reasons this can occur: The samples format may be invalid, it may be to small, etc. Please refer to the website for more information. http://www.evolvingsoftware.com :: Direct sound error message follows - " + error, "Sample " + _sample.ToString() + " was not loaded ::");
                    }
                    else
                    {
                        // Save the sample to the memory stream
                        byte[] _bytes;
                        _bytes = System.IO.File.ReadAllBytes(sampleLocation);
                        globalSettings.osj.sample_MemoryStream[_sample] = new System.IO.MemoryStream(_bytes);
                        
                        // Everything OK - sample loaded sucessfully
                        globalSettings.osj.sampleLocations[_sample] = sampleLocation;
                        globalSettings.osj.sampleLoaded[_sample] = true;

                        globalSettings.osj.sampleDetails_sampleName[_sample] = getSampleName(sampleLocation);

                        // CR 1661564
                        // Display the track name after it has been loaded
                        lblTrack.Text = globalSettings.osj.sampleDetails_sampleName[_sample];
                    }

                }
            }

            // Still want to add the sample to the screen once it has been loaded
            if (globalSettings.osj.sampleLoaded[_sample] == true)
            {

                globalSettings.osj.TrackEditor_SampleInstance_Sample[globalSettings.osj.TrackEditor_SampleInstance_InAction] = _sample;
                globalSettings.osj.TrackEditor_SampleInstance_Location[globalSettings.osj.TrackEditor_SampleInstance_InAction] = 200;
                globalSettings.osj.TrackEditor_SampleInstance_Enabled[globalSettings.osj.TrackEditor_SampleInstance_InAction] = true;

                // Adds a new _sampleInstance with all of the relevant details
                addNewInstance(globalSettings.osj.TrackEditor_SampleInstance_Location[globalSettings.osj.TrackEditor_SampleInstance_InAction]);

                //Needed to make sure when moving a _sampleInstance that the global location is also updated correctly
                _globalSampleInstanceRelation[_storedInstances] = globalSettings.osj.TrackEditor_SampleInstance_InAction;

                // Increment the local variable of the number of labels - this is kept seperate from the one below as each sample
                // is treated seperatly by virtue of the user control
                _storedInstances++;

                // Increments the global variable keeping track of all of the _sampleInstance and their related details
                globalSettings.osj.TrackEditor_SampleInstance_InAction++;
            }
        }

        /// <summary>
        /// Used when adding instances back to the screen, after reloading from a file or when the track editor has been
        /// closed and reopened
        /// </summary>
        /// <param name="Location"></param>
        private void addNewInstance(int Location)
        {
            // Need to do this before adding the controls, otherwise would trigger multiple screen redraws
            this.SuspendLayout();

            this._sampleInstance[_storedInstances] = new System.Windows.Forms.Label();

            this._sampleInstance[_storedInstances].Location = new System.Drawing.Point(Location, globalSettings.osj.TrackEditor_SampleStartingTopOffset);
            this._sampleInstance[_storedInstances].TabIndex = _storedInstances;
            this._sampleInstance[_storedInstances].Cursor = System.Windows.Forms.Cursors.Default;

            this._sampleInstance[_storedInstances].BackColor = System.Drawing.Color.White;
            this._sampleInstance[_storedInstances].ForeColor = System.Drawing.Color.Black;
            this._sampleInstance[_storedInstances].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._sampleInstance[_storedInstances].FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this._sampleInstance[_storedInstances].Height = 100;
            this._sampleInstance[_storedInstances].Width = (int)(dsInterface.getLength(_sample) * (float)(1000 / globalSettings.osj.TrackEditor_Tick));

            // TODO: Add movement controls for samples here
            this._sampleInstance[_storedInstances].MouseMove += new System.Windows.Forms.MouseEventHandler(this._sampleInstance_MouseMove);
            this._sampleInstance[_storedInstances].MouseDown += new System.Windows.Forms.MouseEventHandler(this._sampleInstance_MouseDown);

            // New instance of bufferGraph (used to add an image of the sample to the track)
            bufferGraph bGraph = new bufferGraph();
            // Interface simplified, ask for a bitmap and get it (format predefined in bufferGraph class though ;-)
            // Uses height twice the size of the label so that it is bigger and better
            this._sampleInstance[_storedInstances].Image = bGraph.getGraph(_sample, this._sampleInstance[_storedInstances].Width, 200);


            this.Controls.Add(this._sampleInstance[_storedInstances]);
            this._sampleInstance[_storedInstances].BringToFront();

            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void _sampleInstance_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            for (int i = 0; i < _storedInstances; i++)
            {
                // If the click was initated by the control in position labels[i]
                // Then set the initalClickX for that play to the current mouse location
                if (sender.Equals(_sampleInstance[i]))
                {
                    initalClickX[i] = e.X;

                    // Need to suspend the layout during the move otherwise the screen returns to the left when performing
                    // the layout
                    this.SuspendLayout();

                    _sampleInstance[i].BringToFront();

                    this.ResumeLayout(false);

                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {

                        this.SuspendLayout();
                        
                        _sampleInstance[i].Dispose();

                        // TODO: Need to recycle sampleinstances which are disabled
                        // Disables the global reference to this sample; to ensure that is no longer played when the position is reached
                        globalSettings.osj.TrackEditor_SampleInstance_Enabled[_globalSampleInstanceRelation[i]] = false;
                        
                        this.ResumeLayout();
                        
                        // TODO : Add Popup for track editor
                        //panelPopUp.Left = e.X + labels[i].Left - 2;
                        //panelPopUp.Top = e.Y + labels[i].Top - 2;
                        //panelPopUp.BringToFront();
                        //panelPopUp.Visible = true;
                        //sampleMenu = i;
                    }
                }
            }

        }

        private void _sampleInstance_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            for (int i = 0; i < _storedInstances; i++)
            {
                if (sender.Equals(_sampleInstance[i]))
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        // TODO : Add Popup for track editor
                        //clearPopUp();

                        // Ensure that we don't move the sample to before the start of the track.
                        if (globalSettings.osj.TrackEditor_BaseTick <= _sampleInstance[i].Left + e.X - initalClickX[i])
                        {
                            // Need to suspend the layout during the move otherwise the screen returns to the left when performing
                            // the layout
                            this.SuspendLayout();

                            _sampleInstance[i].Left = _sampleInstance[i].Left + e.X - initalClickX[i];
                            
                            this.ResumeLayout(false);
                        }
                        else
                        {
                            this.SuspendLayout();

                            _sampleInstance[i].Left = globalSettings.osj.TrackEditor_BaseTick;

                            this.ResumeLayout(false);
                        }

                        // Set the global setting location for the sample instance
                        globalSettings.osj.TrackEditor_SampleInstance_Location[_globalSampleInstanceRelation[i]] = _sampleInstance[i].Left;
                    }
                    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        // TODO : Add Popup for track editor
                        //panelPopUp.Left = e.X + labels[i].Left - 2;
                        //panelPopUp.Top = e.Y + labels[i].Top - 2;
                        //panelPopUp.BringToFront();
                        //panelPopUp.Visible = true;
                        //sampleMenu = i;
                    }

                }

            }
        }

        private void cmbSettings_Click(object sender, EventArgs e)
        {
            // Check if the sample is loaded first;
            if (globalSettings.osj.sampleLoaded[_sample] == true)
            {
                // Open the properties window - if the sample has been loaded.
                frmProperties _properties = new frmProperties(_sample);
                _properties.MdiParent = globalSettings.MDIForm;
                _properties.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Sample not loaded");
            }
        }


        private void addExistingSampleInstance(int sampleInstance)
        {
            // Adds a new _sampleInstance with all of the relevant details
            addNewInstance(globalSettings.osj.TrackEditor_SampleInstance_Location[sampleInstance]);

            //Needed to make sure when moving a _sampleInstance that the global location is also updated correctly
            _globalSampleInstanceRelation[_storedInstances] = sampleInstance;

            // Increment the local variable of the number of labels - this is kept seperate from the one below as each sample
            // is treated seperatly by virtue of the user control
            _storedInstances++;
        }


        /// <summary>
        /// The keyRecordNewInstance adds the sample to the screen and ensures that the instance location is saved to the global settings
        /// </summary>
        /// <param name="Location"></param>
        public void keyRecordNewInstance(int Location)
        {
            // Adds the sample to the screen
            addNewInstance(Location);

            // Save the sample instance details to the global settings
            globalSettings.osj.TrackEditor_SampleInstance_Sample[globalSettings.osj.TrackEditor_SampleInstance_InAction] = _sample;
            globalSettings.osj.TrackEditor_SampleInstance_Location[globalSettings.osj.TrackEditor_SampleInstance_InAction] = Location;
            globalSettings.osj.TrackEditor_SampleInstance_Enabled[globalSettings.osj.TrackEditor_SampleInstance_InAction] = true;

            // Adds a new _sampleInstance with all of the relevant details
            addNewInstance(globalSettings.osj.TrackEditor_SampleInstance_Location[globalSettings.osj.TrackEditor_SampleInstance_InAction]);

            //Needed to make sure when moving a _sampleInstance that the global location is also updated correctly
            _globalSampleInstanceRelation[_storedInstances] = globalSettings.osj.TrackEditor_SampleInstance_InAction;

            // Increment the local variable of the number of labels - this is kept seperate from the one below as each sample
            // is treated seperatly by virtue of the user control
            _storedInstances++;

            // Increments the global variable keeping track of all of the _sampleInstance and their related details
            globalSettings.osj.TrackEditor_SampleInstance_InAction++;
        

        }


        private string getSampleName(string sampleLocation)
        {
            //string location = globalSettings.osj.sampleLocations[sample];

            //System.Windows.Forms.MessageBox.Show(location);

            int lastfolder = sampleLocation.LastIndexOf("\\") + 1;

            sampleLocation = sampleLocation.Remove(0, lastfolder);

            sampleLocation = sampleLocation.Remove(sampleLocation.Length - 4, 4);

            return sampleLocation;
        }

    }// End Class
}// End NameSpace
