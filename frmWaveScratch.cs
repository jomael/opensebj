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
using System.IO;

namespace OpenSebJ
{
    public partial class frmWaveScratch : Form
    {

        //private System.IO.MemoryStream stream0;
        //private byte[] streamBuffer0;
        //private System.IO.MemoryStream stream1;
        //private byte[] streamBuffer1;

        // Length of the buffer
        //private int numOfBytes;
    
        // Sample to load
        private int _Sample;

        // The last position of the needle
        int global_playPosition = 0;

        // The local variable for the wave BitMap
        Bitmap _WaveBitmap;

        //// Check to ensure that wave gets displayed after load
        //private bool loaded = false;

        public frmWaveScratch(int Sample)
        {
            InitializeComponent();
            _Sample = Sample;
        }


        //public short[] getBuffer(int sampleToRead)
        //{
        //    numOfBytes = dsInterface.aSound[sampleToRead].Caps.BufferBytes;
        //    int bytesPerSample = dsInterface.aSound[sampleToRead].Format.BitsPerSample / 8;

        //    CreateStreamBuffers();
        //    CreateStreams();

        //    // Create a 16bit Buffer for the total number of samples assuming the maximum 
        //    // bits per sample is 16
        //    int iNumOfSamples = numOfBytes / bytesPerSample;
        //    short[] Points = new Int16[iNumOfSamples];

        //    // Read the complete stream in to a memory stream
        //    dsInterface.aSound[sampleToRead].Read(0, stream0, numOfBytes, Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);


        //    //System.Windows.Forms.MessageBox.Show(bytesPerSample.ToString());
           

        //    // Get the data from the sample
        //    for(int i=0; i < numOfBytes; i = i + bytesPerSample)
        //    {
        //        Int16 iSample = 0;

        //        switch (bytesPerSample)
        //        {
        //            case 1:
        //                //Int16Converter iC = new Int16Converter();
        //                //iC.ConvertFrom(
        //                iSample = streamBuffer0[i];
        //                Points[i] = iSample;
        //                break;
        //            case 2:
        //                iSample = streamBuffer0[i + 1];
        //                // Shifts the data 8 bits to the left
        //                iSample <<= 8;
        //                // Performs a Bitwise logical OR
        //                iSample |= streamBuffer0[i];
        //                Points[i / 2] = iSample;
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    return Points;
        //}


        //private void CreateStreamBuffers()
        //{
        //    streamBuffer0 = new byte[numOfBytes];
        //    for (int i = 0; i < numOfBytes; i++)
        //        streamBuffer0[i] = 0;

        //    streamBuffer1 = new byte[numOfBytes];
        //    for (int i = 0; i < numOfBytes; i++)
        //        streamBuffer1[i] = 0;
        //}


        //private void CreateStreams()
        //{
        //    stream0 = new MemoryStream(streamBuffer0);
        //    stream1 = new MemoryStream(streamBuffer1);
        //}


        private void frmWavePlot_Load(object sender, EventArgs e)
        {
            //short[] buff = getBuffer(_Sample);

            // The code which uses the custom control
            //waveDisplay1.Setup(dsInterface.aSound[_Sample].Format.Channels, buff, (int)(dsInterface.aSound[_Sample].Caps.BufferBytes / (dsInterface.aSound[_Sample].Format.BitsPerSample / 8.0f)), dsInterface.aSound[_Sample].Frequency, 100f);
            //waveDisplay1.ShowWave();
            //_WaveBitmap = waveDisplay1.getWaveBitmap();
           
            //int TextualSample = _Sample + 1;
            //this.Text = "Sample :: " + TextualSample.ToString();

            this.Text = "Sample View :: " + globalSettings.osj.sampleDetails_sampleName[_Sample];


            // This code usese the bufferGraph class insead of the control
            bufferGraph bGraph = new bufferGraph();
            // Interface simplified, ask for a bitmap and get it (format predefined in bufferGraph class though ;-)
            _WaveBitmap = bGraph.getGraph(_Sample);


            //short[] buff = bGraph.getBuffer(_Sample);
            //bGraph.Setup(dsInterface.aSound[_Sample].Format.Channels, buff, (int)(dsInterface.aSound[_Sample].Caps.BufferBytes / (dsInterface.aSound[_Sample].Format.BitsPerSample / 8.0f)), dsInterface.aSound[_Sample].Frequency, 100f, 800, 250);
            //bGraph.ShowWave();
            //_WaveBitmap = bGraph.getWaveBitmap();

            
        }


        public void cleanBack()
        {
            // Used to clean p the backdrop in the first instance
            Graphics g = this.CreateGraphics();
            g.Clear(Color.Black);
        }

        public void redrawMarks(int playPosition)
        {

            // Clear form
            Graphics g = this.CreateGraphics();

            //Clean the background
            SolidBrush cleanBrush = new SolidBrush(Color.Black);

            //Wipe the background out from the last play marker
            Pen cleanPen = new Pen(cleanBrush, 2);
            System.Drawing.Point p3 = new Point(global_playPosition, 0);
            System.Drawing.Point p4 = new Point(global_playPosition, 287);
            g.DrawLine(cleanPen, p3, p4);

            // Draw the Bitmap
            g.DrawImage(_WaveBitmap, 7, 6);
            
            // Now draw the play marker
            SolidBrush myBrush = new SolidBrush(Color.Red);

            // Creates a pen with the same display properties as myBrush and a 
            // thickness of 2.
            Pen myPen = new Pen(myBrush, 2);
            System.Drawing.Point p1 = new Point(playPosition, 0);
            System.Drawing.Point p2 = new Point(playPosition, 287);
            g.DrawLine(myPen, p1, p2);

            global_playPosition = playPosition;

        }


        private void frmWaveScratch_MouseMove(object sender, MouseEventArgs e)
        {
            redrawMarks(e.X);

            int _temp = dsInterface.aSound[_Sample].Caps.BufferBytes / 800;

            try
            {
                dsInterface.aSound[_Sample].SetCurrentPosition((e.X - 7) * _temp);
            }
            catch
            {

            }
        }

        private void frmWaveScratch_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dsInterface.playFromPosition(_Sample);
            }
            else
            {

                dsInterface.stop(_Sample);
            }
        }

        //private void frmWaveScratch_Paint(object sender, PaintEventArgs e)
        //{
        //    // Occurs before the backdrop is pissed away
        //    //cleanBack();
        //    //redrawMarks(7);
        //}

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Occurs before the backdrop is pissed away
            cleanBack();
            redrawMarks(7);
        }

    }
}