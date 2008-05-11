using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


//OpenAL References
using OpenALDotNet;
using OpenALDotNet.Streams;
using AdvanceMath;


namespace OpenSebJ
{
    class OpenALRecord
    {
        // Has OpenAL started recording
        public bool OpenALRecoding = false;

        //OpenAL Static Recording Thread
        public Thread recordingStreamThread;

        //FileName for saving the file
        private string _FileName = "";

        /// <summary>
        /// Create capture buffer, output wave file and stream recorded samples to disk every 50 milliseconds
        /// </summary>
        public void StreamAudio()
        {
            AudioListener.Position = new Vector3D(0, 0, 0);
            AudioListener.Velocity = new Vector3D(0, 0, 0);
            AudioListener.Orientation = new Orientation(new Vector3D(1, 1, 0), new Vector3D(0, 1, 0));
            Byte[] recordedData = null;

            AudioFormatEnum HQcaptureFormat = AudioFormatEnum.Stereo16;
            int HQcaptureFrequency = 44100;
            int HQcaptureBufferSize = 1028000;

            //Console.WriteLine("Creating File {0}", Environment.CurrentDirectory + "\\test.wav");

            if (_FileName == "")
            {
                _FileName = "undefined.wav";
            }
            
            WaveFileWriter wave = new WaveFileWriter();
            wave.CreateFile(_FileName, HQcaptureFormat);

            using (AudioCaptureDevice g = new AudioCaptureDevice(null, HQcaptureFormat, HQcaptureFrequency, HQcaptureBufferSize))
            {
                //Console.WriteLine("Started Recording (press Enter To Stop)");
                g.Start();

                while (OpenALRecoding)
                {
                    Thread.Sleep(50);
                    int samplecount = g.AvaliabeSampleCount;
                    recordedData = g.CaptureSamples();
                    wave.WriteCaptured(recordedData);
                }

                g.Stop();
                //Console.WriteLine("Stopped Recording");
            }
            wave.CloseFile();
            //Console.WriteLine("File Saved");
        }


        /// <summary>
        /// Creates a seperate thread to stream all samples to disk & awaits keyboard input to stop
        /// </summary>
        public void startRecording()
        {
            recordingStreamThread = new Thread(StreamAudio);
            OpenALRecoding = true;
            recordingStreamThread.Start();

        }

        /// <summary>
        /// Stop OpenAL from recording
        /// </summary>
        public void stopRecording()
        {
            OpenALRecoding = false;
            recordingStreamThread.Join();
        }



        public void preRecord(string fileName)
        {
            _FileName = fileName;
        }
    }
}
