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
using System.Text;
using Microsoft.DirectX.DirectSound;
using System.Threading;
using System.IO;

namespace OpenSebJ
{
    public class bufferRecord
    {
        // Used eventually for the format of the capture buffer
        private WaveFormat _recordFormat;

        // Need to setup the capture device
        private Capture captureDevice = null;

        // Setup in the setRecordFormat
        // Needs to be short as its going to be written to the wave file
        private short _bytesPerSample = 0;

        // When the notification event will be raised
        private int _notificationAt = 0;
        // Buffer size
        private int _recordBufferSize = 0;
        // The CaptureBuffer
        private CaptureBuffer _recordBuffer = null;
        // The number of notifications for the record buffer
        private int _bufferNotifications = 0;
        // Setup where exactly the notifications will be raised from
        private BufferPositionNotify[] _positionNotification;
        // Set the reset notification
        private AutoResetEvent _resetNotification = null;
        // The thread to handle notifications 	
        private Thread _callMeThread = null;
        // Set the notification which will be attached to the buffer
        private Notify _iSaidCallMe = null;
        // Just to know if we are recording
        private bool _recording = false;
        // The used to find the recording position
        private int _nextRecordingPosition = 0;
        // The bytes of samples in the sound recording
        private int _sampleBytes = 0;

        // File writing objects
        private FileStream _recordFile = null;
        private BinaryWriter _binaryWriter = null;


        //private Guid _emptyGuid = Guid.Empty;


        // Get the collection of devices
        CaptureDevicesCollection _device;


        /// <summary>
        /// Create the capture device to use in the recording.
        /// </summary>
        public void createCaptureDevice()
        {
            // Just getting the first devoce for the moment - can change 
            // latter if required.
            _device = new CaptureDevicesCollection();
            captureDevice = new Capture(_device[0].DriverGuid);
        }

        /// <summary>
        /// Setup the recording format. 
        /// Needs to be called after the capture device is created.
        /// </summary>
        public void setRecordFormat()
        {
            // Setup the record format to Stero - 16Bit - 44800 Hz
            // Could add other recording formats but this works for me, 
            // and it's the best my sound card can do ;-)
            
            // Stero
            _recordFormat.Channels = 2;

            // 48000 Samples Per Second
            _recordFormat.SamplesPerSecond = 48000;

            // 16 Bit
            _recordFormat.BitsPerSample = 16;

            // Set to PCM format
            _recordFormat.FormatTag = WaveFormatTag.Pcm;

            // Block Align
            _recordFormat.BlockAlign = 4;

            // AverageBytesPerSecond
            _recordFormat.AverageBytesPerSecond = 192000;


            // Therfore the hard coded Bytes Per Sample is...
            _bytesPerSample = 4;

            // Setup the notificationAt based on the quality
            // Would normally be calculated by ::
            // _recordFormat.AverageBytesPerSecond / 8
            // But we are using a fixed number because we are using a set quality ;-)
            _notificationAt = 24000;

            // The number of notifications for the record buffer
            // Only set here to following the Hard Coding standard of babaloyn
            _bufferNotifications = 16;

            // Set the buffer size
            // Calculated by the notification size * number of notiifcations setup for the buffer (using 16 here)
            _recordBufferSize = 384000;

            // Setup that there will be 17 positions which will fire the notification event
            _positionNotification = new BufferPositionNotify[17];  

        }

        /// <summary>
        /// Setup the buffer and notifications. Needs to be called after the
        /// format has been setup.
        /// </summary>
        public void createRecordBuffer()
        {
            // Create the recordBufferDesciption to be used
            CaptureBufferDescription recordBufferDescription = new CaptureBufferDescription();

            // Setup the pre-calculated buffer size
            recordBufferDescription.BufferBytes = _recordBufferSize;

            // Set the record buffer to the format which has been pre-defined
            recordBufferDescription.Format = _recordFormat;

            // Setup an instance of the capture buffer using the previously
            // formed description and the default capture device (as created in the
            // createCaptureDevice method)
            _recordBuffer = new CaptureBuffer(recordBufferDescription, captureDevice);

            // Now the buffer is setup, its time to config setup the notifications
            setupNotifications();
        
        }

        private void setupNotifications()
        {
            try
            {
                if (null == _callMeThread)
                {
                    // Create the thread to recive the notification events
                    // (which trigger the copying of the data to the wave 
                    // file being streamed to the disk)
                    _callMeThread = new Thread(new ThreadStart(HoldThread));
                    

                    // Start the recording
                    _recording = true;
                    _callMeThread.Start();

                    // Create a notification event
                    _resetNotification = new AutoResetEvent(false);

                }
            }
            catch(Exception anException)
            {
                System.Windows.Forms.MessageBox.Show(anException.ToString());
            }

            try
            {
                for (int i = 0; i < _bufferNotifications; i++)
                {
                    _positionNotification[i].Offset = (_notificationAt * i) + _notificationAt - 1;

                    // TO DO: this needs to be updated to the 'safe' one
                    _positionNotification[i].EventNotifyHandle = _resetNotification.Handle;
                }
            }
            catch(Exception anException)
            {
                System.Windows.Forms.MessageBox.Show(anException.ToString());
            }

            try
            {
            // Assign the notification to the buffer.
            _iSaidCallMe = new Notify(_recordBuffer);

            // Setup DirectSound with the notifications. The notifications will be handled in 
            // the _callMeThread thread.
            _iSaidCallMe.SetNotificationPositions(_positionNotification, _bufferNotifications);
            }
            catch (Exception anException)
            {
                System.Windows.Forms.MessageBox.Show(anException.ToString());
            }
        }

        /// <summary>
        /// Start Recording
        /// </summary>
        public void startRecording()
        {            
            // Use a looping buffer because the notification methods fire,
            // so that we can copy out data and not loose anything.
            _recordBuffer.Start(true);
        }

        /// <summary>
        /// Stop Recording
        /// </summary>
        public void StopRecording()
        {
            // Stop the buffer recording
            _recordBuffer.Stop();

            // Copy out any remaining data
            CopyRecordedData();

            // Check if the recording, is still recording - use this
            // same method when the form is disposing.
            if (_recording == true)
            {
                // Stop the recording and kill the thread
                _recording = false;
                _callMeThread.Abort();

                // Write the final configuration infromation for the RIFF description
                _binaryWriter.Seek(4, SeekOrigin.Begin); 
                _binaryWriter.Write((int)(_sampleBytes + 36));	
                
                // Write the data length of the descriptor in bytes
                _binaryWriter.Seek(40, SeekOrigin.Begin);
                _binaryWriter.Write(_sampleBytes);

                // Close and dispose of the writer components
                _binaryWriter.Close();	
                _binaryWriter = null;	
                _recordFile = null; 
            }
        }


        
        void CreateTheRIFF(string FileName)
        {
            // Create the binary stream writer for the wave file
            _recordFile = new FileStream(FileName, FileMode.Create);
            _binaryWriter = new BinaryWriter(_recordFile);

            // Set the wave header info up, this is standard for 
            // the wave format. The first part is that it is a 
            // RIFF format.
            char[] Riff = { 'R', 'I', 'F', 'F' };
            _binaryWriter.Write(Riff);

            // The length - this will be re-written after recording is
            // finalised and the file length is known.
            _binaryWriter.Write((int)0);

            // The header now needs to have WAVE
            char[] Type = { 'W', 'A', 'V', 'E' };
            _binaryWriter.Write(Type);

            // Next is the fmt marker, note this requires one empty
            // filler char.
            char[] Format = { 'f', 'm', 't', ' ' };
            _binaryWriter.Write(Format);

            // Now set the length of the chunk
            _binaryWriter.Write((int)0x10);
                        
            // Another filler for the format
            _binaryWriter.Write((short)1); 

            // Now the actual format header is written out.
            // When loading audio DX abstracts this information out but 
            // apparently not here ;-)
            _binaryWriter.Write(_recordFormat.Channels);
            _binaryWriter.Write(_recordFormat.SamplesPerSecond);
            _binaryWriter.Write(_recordFormat.AverageBytesPerSecond);
            _binaryWriter.Write(_bytesPerSample);
            _binaryWriter.Write(_recordFormat.BitsPerSample);

            // Now we can start loading the actual data chunks
            char[] Data = { 'd', 'a', 't', 'a' };
            _binaryWriter.Write(Data);

            // This is where will fill in the sample length when 
            // finalising the wave recording (this is 40 Bytes in)
            _binaryWriter.Write((int)0);
        }


        
        private void CopyRecordedData()
        {
            // This copys the recorded data out of the capture buffer 
            // and in to the wave file verbatam. The data is stored in
            // the capture buffer in the correct format and the wave file
            // header was previously written in the CreateTheRiff method
            int readCursor;
            int captureCursor;
            
            // Loads the captureCursor and readCursor with the buffer positions
            _recordBuffer.GetCurrentPosition(out captureCursor, out readCursor);


            // boundry used to check that we have located a valid boundry to 
            // write from 
            int boundry;

            boundry = readCursor - _nextRecordingPosition;
            if (boundry < 0)
            {
                boundry = boundry + _recordBufferSize;
            }

            // Align the block so that we will write on a boundry
            boundry = boundry - (boundry % _notificationAt);

            if (0 == boundry)
                return;


            // Set a temporary byte buffer to store the _recordBuffer data
            byte[] captured = null;
            
            // Copy the data out of the capture buffer 
            captured = (byte[])_recordBuffer.Read(_nextRecordingPosition, typeof(byte), LockFlag.None, boundry);

            // Write out the data to the file previously setup
            _binaryWriter.Write(captured, 0, captured.Length);

            // Increase the running total of sample bytes (used when
            // finalising the file to update the length )
            _sampleBytes = _sampleBytes + captured.Length;

            // Increase the capture buffer offset to the next position
            _nextRecordingPosition = _nextRecordingPosition + captured.Length;
            
            // Take in to account our circular buffer
            _nextRecordingPosition = _nextRecordingPosition % _recordBufferSize;
        }


        private void HoldThread()
        {
            // Pause in holding pattern waiting for a notification to arive
            while (_recording)
            {
                try
                {
                    _resetNotification.WaitOne(Timeout.Infinite, true);
                    CopyRecordedData();
                }
                catch //(Exception anException)
                {
                    // TO DO: When not running through the debuger this
                    // try catch block throws a System.NullReferenceException
                    // It has something to do with the thread not being available
                    // but only occurs at the first time this command is called.
                    // Does deserve more attention but throwing this exception doesn't
                    // seem to cause any adverse effects, any way its a pain in the
                    // arse to debug something that doesn't occur in the debuger ;-)
                    //System.Windows.Forms.MessageBox.Show(anException.ToString());
                }
            }
        }


        /// <summary>
        /// Last PreRecord Setup
        /// </summary>
        public void preRecord(string fileName)
        {
            // using the fileName, creates the Riff.
            CreateTheRIFF(fileName);
        }

    } // End Of The Class ;-)
}
