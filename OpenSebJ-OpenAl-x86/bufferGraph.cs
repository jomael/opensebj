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
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace OpenSebJ
{
    public class bufferGraph
    {
        int _channels;
        short[] _bufferData;
        int _count;
        int _frequency;
        float _zoomPercent;

        int maxSampleValue = 1000;

        Bitmap _Bitmap;

        // To replace the dimensions which was used for the PictureBox
        int _Width;
        int _Height;



        public void Setup(int Channels, Int16[] BufferData, int Count, int Frequency, float Zoom, int Width, int Height)
        {
            _channels = Channels;
            _bufferData = BufferData;
            _count = Count;
            _frequency = Frequency;
            _zoomPercent = Zoom;

            _Width = Width;
            _Height = Height;
        }



        public void ShowWave(bool ShowNumbers)
        {
            DrawWave(_bufferData, ShowNumbers);
        }


        /// <summary>Draw a left or right channel wave</summary>
        private void DrawWave(short[] channelSamples, bool ShowNumbers)
        {
            //Control topLevelControl = this.FindForm();
            //if (topLevelControl == null) { topLevelControl = this; };
            //topLevelControl.Cursor = Cursors.WaitCursor;

            try
            {

                float zoom = _zoomPercent / 100;
                //float zoom = 10/100;
                //System.Windows.Forms.MessageBox.Show(zoom.ToString());

                //resize picturebox
                //Commented out to be more usable in control - i.e. takes controls width
                ////int scaledWidth = (int)((float)channelSamples.Length * zoom);
                ////if (scaledWidth > 9999) { scaledWidth = 9999; } //prevent ArgumentException creating Bitmap
                ////picWave.Width = scaledWidth;
                //picWave.Width = _Width;

                //calculate scaling
                //Commented out to be more usable in control - i.e. takes controls height
                ////picWave.Height = (this.Height - SystemInformation.HorizontalScrollBarHeight) / 2;
                //picWave.Height = _Height;

                int samplesPerPixel;
                int spacePerSample;
                if (_Width > channelSamples.Length)
                {
                    //multiple pixels per second
                    samplesPerPixel = 1;
                    spacePerSample = _Width / channelSamples.Length;
                }
                else
                {
                    //multiple samples per pixel
                    spacePerSample = 1;
                    samplesPerPixel = channelSamples.Length / _Width;
                }

                //calculate width of one second
                int pixelsPerSecond = (int)(_channels * _frequency * spacePerSample / samplesPerPixel);

                Bitmap bitmap = new Bitmap(_Width, _Height, PixelFormat.Format24bppRgb);
                Graphics graphics = Graphics.FromImage(bitmap);

                Pen pen = new Pen(Color.Black);
                graphics.Clear(Color.White);


                int absValue;
                for (int channelSamplesIndex = 0; channelSamplesIndex < channelSamples.Length; channelSamplesIndex++)
                {
                    absValue = Math.Abs((int)channelSamples[channelSamplesIndex]);
                    if (absValue > maxSampleValue)
                    {
                        maxSampleValue = (absValue > short.MaxValue) ? (short)(absValue - 1) : (short)absValue;
                    }
                }

                float yOffset = bitmap.Height / 2;

                if (maxSampleValue != 0)
                { //not trying to display silence (all pixels == 0)

                    float yScale = yOffset / maxSampleValue;

                    float xPosition = 0;
                    int pixelMaximum = 0;
                    int pixelMinimum = 0;
                    short currentSample;

                    PointF previousPoint = new PointF(0, yOffset);
                    for (int n = 0; n < channelSamples.Length; n += samplesPerPixel)
                    {
                        currentSample = channelSamples[n];
                        pixelMaximum = 0;
                        pixelMinimum = 0;

                        for (int sampleIndex = n; sampleIndex < (n + samplesPerPixel); sampleIndex++)
                        {
                            if (currentSample > pixelMaximum) { pixelMaximum = currentSample; }
                            if (currentSample < pixelMinimum) { pixelMinimum = currentSample; }
                        }

                        pixelMaximum = (int)(pixelMaximum * yScale);
                        pixelMinimum = (int)(pixelMinimum * yScale);

                        graphics.DrawLine(pen, previousPoint.X, previousPoint.Y, xPosition, yOffset + pixelMinimum);
                        graphics.DrawLine(pen, xPosition, yOffset + pixelMaximum, xPosition, yOffset + pixelMinimum);
                        previousPoint.X = xPosition;
                        previousPoint.Y = yOffset + pixelMaximum;

                        xPosition += spacePerSample;
                    }
                }

                if (ShowNumbers == true)
                {
                    //show seconds
                    int second = 0;
                    Brush brush = new SolidBrush(Color.Blue);
                    Font boldFont = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                    for (int n = 0; n < _Width; n += pixelsPerSecond)
                    {
                        graphics.DrawString(second.ToString(), boldFont, brush, n, _Height - 15);
                        second++;
                    }
                
                    // Not the number but the red line isn't required on a small scale ;-)
                    pen.Color = Color.Red;
                    graphics.DrawLine(pen, 0, yOffset, bitmap.Width, yOffset);

                }

                graphics.Dispose();
                //picWave.Image = bitmap;
                //picWave.Refresh();

                // Set the class bitmap so it can be called for latter
                _Bitmap = new Bitmap(bitmap);
            }
            finally
            {
                //topLevelControl.Cursor = Cursors.Default;
            }

            //Sets the background image to the pic box.
            //this.BackgroundImage = picWave.Image;

        }


        public Bitmap getWaveBitmap()
        {
            return _Bitmap;
        }









        private System.IO.MemoryStream stream0;
        private byte[] streamBuffer0;
        private System.IO.MemoryStream stream1;
        private byte[] streamBuffer1;

        // Length of the buffer
        private int numOfBytes;
    

        public short[] getBuffer(int sampleToRead)
        {
            //numOfBytes = dsInterface.aSound[sampleToRead].Caps.BufferBytes;
            //int bytesPerSample = dsInterface.aSound[sampleToRead].Format.BitsPerSample / 8;

            numOfBytes = globalSettings.osj.sampleFormat_BufferBytes_Size[sampleToRead];
            int bytesPerSample = globalSettings.osj.sampleFormat_BitsPerSample[sampleToRead] / 8;
            

            CreateStreamBuffers();
            CreateStreams();

            // Create a 16bit Buffer for the total number of samples assuming the maximum 
            // bits per sample is 16
            int iNumOfSamples = numOfBytes / bytesPerSample;
            short[] Points = new Int16[iNumOfSamples];

            // Read the complete stream in to a memory stream
            //dsInterface.aSound[sampleToRead].Read(0, stream0, numOfBytes, Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);

            // Read the complete stream in to a memory stream
            Byte[] waveData = OpenAlInterface.getSampleBytes(sampleToRead);
            MemoryStream OrigionalWaveData = new MemoryStream(waveData);
            OrigionalWaveData.WriteTo(stream0);
            

            //System.Windows.Forms.MessageBox.Show(bytesPerSample.ToString());
           

            // Get the data from the sample
            for(int i=0; i < numOfBytes; i = i + bytesPerSample)
            {
                Int16 iSample = 0;

                switch (bytesPerSample)
                {
                    case 1:
                        //Int16Converter iC = new Int16Converter();
                        //iC.ConvertFrom(
                        iSample = streamBuffer0[i];
                        Points[i] = iSample;
                        break;
                    case 2:
                        //iSample = streamBuffer0[i + 1];
                        //// Shifts the data 8 bits to the left
                        //iSample <<= 8;
                        //// Performs a Bitwise logical OR
                        //iSample |= streamBuffer0[i];
                        //Points[i / 2] = iSample;

                        // Submitted by Alfeu Marcatto - alfmarc :: via sourceforge.com
                        iSample = streamBuffer0[i + 1];
                        if (iSample > 127)
                        {
                            iSample -= 255;
                        }
                        iSample <<= 8; 
                        Points[i / 2] = iSample;

                        break;
                    default:
                        break;
                }

            }
            return Points;
        }


        private void CreateStreamBuffers()
        {
            streamBuffer0 = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; i++)
                streamBuffer0[i] = 0;

            streamBuffer1 = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; i++)
                streamBuffer1[i] = 0;
        }


        private void CreateStreams()
        {
            stream0 = new MemoryStream(streamBuffer0);
            stream1 = new MemoryStream(streamBuffer1);
        }


        public Bitmap getGraph(int Sample)
        {
            int _Sample = Sample;

            short[] buff = getBuffer(_Sample);
            //Setup(dsInterface.aSound[_Sample].Format.Channels, buff, (int)(dsInterface.aSound[_Sample].Caps.BufferBytes / (dsInterface.aSound[_Sample].Format.BitsPerSample / 8.0f)), dsInterface.aSound[_Sample].Frequency, 100f, 800, 250);
            Setup(globalSettings.osj.sampleFormat_Channels[_Sample], buff, (int)(globalSettings.osj.sampleFormat_BufferBytes_Size[_Sample] / (globalSettings.osj.sampleFormat_BitsPerSample[_Sample] / 8.0f)), globalSettings.osj.sampleSettings_Frequency[_Sample], 100f, 800, 250);
            ShowWave(true);
            return getWaveBitmap();
        }

        public Bitmap getGraph(int Sample, int Width, int Height)
        {
            int _Sample = Sample;

            short[] buff = getBuffer(_Sample);
            //Setup(dsInterface.aSound[_Sample].Format.Channels, buff, (int)(dsInterface.aSound[_Sample].Caps.BufferBytes / (dsInterface.aSound[_Sample].Format.BitsPerSample / 8.0f)), dsInterface.aSound[_Sample].Frequency, 100f, Width, Height);
            Setup(globalSettings.osj.sampleFormat_Channels[_Sample], buff, (int)(globalSettings.osj.sampleFormat_BufferBytes_Size[_Sample] / (globalSettings.osj.sampleFormat_BitsPerSample[_Sample] / 8.0f)), globalSettings.osj.sampleSettings_Frequency[_Sample], 100f, Width, Height);
            ShowWave(false);
            return getWaveBitmap();
        }







    }
}
