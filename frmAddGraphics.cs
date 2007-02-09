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
    public partial class frmAddGraphics : Form
    {
        int _sample = 0;

        public frmAddGraphics(int sample)
        {
            _sample = sample;

            InitializeComponent();
        }

        private void frmAddGraphics_Load(object sender, EventArgs e)
        {
            
            //if (dsInterface.imageLocations[_sample] != null)
            if (globalSettings.osj.image_Locations[_sample] != null)
            {
                //lblImage.Text = "Image: " + dsInterface.imageLocations[_sample];
                lblImage.Text = "Image: " + globalSettings.osj.image_Locations[_sample];
            }
            //if (dsInterface.videoLocations[_sample] != null)
            if (globalSettings.osj.video_Locations[_sample] != null)
            {
                //lblVideo.Text = "Video: " + dsInterface.videoLocations[_sample];
                lblVideo.Text = "Video: " + globalSettings.osj.video_Locations[_sample];
            }
        }

        private void cmbBrowseImage_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openImage.ShowDialog())
            {
                // Image Location
                globalSettings.osj.image_Locations[_sample] = openImage.FileName.ToString();

                // Save the Image to the memory stream
                byte[] _bytes;
                _bytes = System.IO.File.ReadAllBytes(globalSettings.osj.image_Locations[_sample]);
                globalSettings.osj.image_MemoryStream[_sample] = new System.IO.MemoryStream(_bytes);
            }

            lblImage.Text = "Image: " + globalSettings.osj.image_Locations[_sample];

            
            // If the video portal is already opened; load the image to an available texture
            if (globalSettings.videoPortal == true)
            {
                eRender.LoadSingleTexture(_sample);
            }

        }

        private void cmbBrowseVideo_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openVideo.ShowDialog())
            {
                // Video location
                globalSettings.osj.video_Locations[_sample] = openVideo.FileName.ToString();

                // Save the Video to the memory stream
                byte[] _bytes;
                _bytes = System.IO.File.ReadAllBytes(globalSettings.osj.video_Locations[_sample]);
                globalSettings.osj.video_MemoryStream[_sample] = new System.IO.MemoryStream(_bytes);
            }

            lblVideo.Text = "Video: " + globalSettings.osj.video_Locations[_sample];

            
            // If the video portal is already loaded, then load the video as well
            if (globalSettings.videoPortal == true)
            {
                eRender.LoadSingleVideo(_sample);
            }
        }



        private void picDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}