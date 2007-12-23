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

namespace OpenSebJ
{
    public partial class frmSaveExplorer : Form
    {
        public frmSaveExplorer()
        {
            InitializeComponent();
        }

        private void frmSaveExplorer_Load(object sender, EventArgs e)
        {

        }

        private void cmbOutput_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();

            if (folderBrowser.SelectedPath != null)
            {

                //Extracts Videos

                string videoName = folderBrowser.SelectedPath + "\\Videos";

                System.Windows.Forms.MessageBox.Show(videoName);

                System.IO.Directory.CreateDirectory(videoName);

                
                for (int i = 0; i < 255; i++)
                {
                    //if (dsInterface.videoLocations[i] != null)
                    if (globalSettings.osj.video_Locations[i] != null)
                    {
                        try
                        {
                            // This loop extracts the video from the memory stream where they were saved and loads them 1 by 1 through the
                            // DirectShow interface, in to memory.
                            if (globalSettings.osj.video_Loaded[i] == true)
                            {
                                // Setup the blank byte array
                                byte[] _bytes;

                                // Use the application data directory defined as the temporary location to write the wave sample to
                                // TODO: Setup the extension to match the image type - however it seems to work even without an extension?
                                //string videoName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\video";

                                // Copy the memory stream to the temporary byte array
                                _bytes = globalSettings.osj.video_MemoryStream[i].ToArray();

                                // Writes out the byte array to the predefined file location
                                System.IO.File.WriteAllBytes(videoName + "\\" + i + ".wmv", _bytes);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.ToString());
                        }
                    }
                }


                //Extract Audio Samples

                string audioName = folderBrowser.SelectedPath + "\\AudioSamples";

                System.Windows.Forms.MessageBox.Show(audioName);

                System.IO.Directory.CreateDirectory(audioName);

                for (int i = 0; i < 262; i++)
                {
                    if (globalSettings.osj.sampleLoaded[i] == true)
                    {
                        // Setup the blank byte array
                        byte[] _bytes;

                        // Copy the memory stream to the temporary byte array
                        _bytes = globalSettings.osj.sample_MemoryStream[i].ToArray();

                        // Writes out the byte array to the predefined file location
                        System.IO.File.WriteAllBytes(audioName + "\\" + i + ".wav", _bytes);
                    }
                }





            }
        }
    }
}