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
    public partial class frmVideoRender : Form
    {
        public frmVideoRender()
        {
            InitializeComponent();
        }

        private void frmVideoRender_Load(object sender, EventArgs e)
        {
            // Setup the window size before any content is rendered
            setWindow();

            // Setup the D3D Device
            eRender.SetupD3D(this);

            // Load a video file to setup DirectShow
            eRender.StartGraph();

            // Load the inital textures
            eRender.LoadTextures();

            // Prepare the DirectShow video array
            eRender.initDSArrays();

            // Load the videos
            eRender.LoadVideos();

            // Render the inital background
            eRender.renderBackground(eRender.images[1]);

            // Keeping track that the window has been opened
            globalSettings.videoPortal = true;

            globalSettings.videoPortal_eRenderReady = true;

        }

        private void setWindow()
        {
            if (globalSettings.videoPortal_FullScreen == true)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            }

        }

        private void frmVideoRender_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.SendToBack();
            }
        }
    }
}