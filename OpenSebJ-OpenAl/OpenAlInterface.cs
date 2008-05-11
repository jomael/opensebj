/* 
 * OpenSebJ
 * Copyright (C) 2007 Sebastian Gray - sebastiangray@gmail.com 
 * Website: http://www.evolvingsoftware.com/opensebj-vScaleNotes.html
 * 
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 * 
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 * 
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
*/

//TODO : Implement OpenAL ;-)
//TODO : OpenAL to be implemented for Audio recording and low level control; utalising SDL.Net for Audio Playback for the time being..

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

//OpenAL References
using OpenALDotNet;
using OpenALDotNet.Streams;
using AdvanceMath;








namespace OpenSebJ
{
    public static class OpenAlInterface
    {

        // Has OpenAL been initalised
        public static bool OpenAlInitalised = false;

        



        /// <summary>
        /// Setup the audio subsystem
        /// </summary>
        /// <param name="callingForm">The Calling Form</param>
        public static void setupOpenAL(System.Windows.Forms.Control callingForm)
        {
            OpenAudioLibrary.AlutInit();
            OpenAlInitalised = true;
        }


        public static short getSampleChannels(string fileName)
        {
            WaveFileReader wfr = new WaveFileReader();
            wfr.OpenFile(fileName);
            return (short)wfr.Channels();
        }

        public static void getSampleSetting(int _sample)
        {
            WaveFileReader wfr = new WaveFileReader();
            wfr.OpenFile(globalSettings.osj.sampleLocations[_sample]);

            globalSettings.osj.sampleFormat_Channels[_sample] = (short)wfr.Channels();
            globalSettings.osj.sampleFormat_BitsPerSample[_sample] = (short)wfr.Bits();
            globalSettings.osj.sampleFormat_BufferBytes_Size[_sample] = wfr.Size();
            globalSettings.osj.sampleFormat_SamplesPerSecond[_sample] = wfr.Samples();
            globalSettings.osj.sampleSettings_Frequency[_sample] = wfr.Frequency();
            globalSettings.osj.sampleFormat_LengthInSeconds[_sample] = wfr.Seconds();
            
                
               
        }



        ///// <summary>
        ///// Creates a seperate thread to stream all samples to disk & awaits keyboard input to stop
        ///// </summary>
        //static void RecordToDisk()
        //{
        //    recordingStreamThread = new Thread(StreamAudio);
        //    OpenALRecoding = true;
        //    recordingStreamThread.Start();
        //    Console.ReadLine();
        //    recording = false;
        //    recordingStreamThread.Join();

        //}

       















        /// <summary>
        /// Load a sample
        /// </summary>
        /// <param name="fileName">The filename of the sample to load</param>
        /// <param name="position">The position to load the sample</param>
        public static string loadSample(string fileName, int position)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Play the audio sample associated with the position
        /// </summary>
        /// <param name="position">
        /// The position to be played
        /// </param>
        public static void playSample(int position)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Stop Playing Sample
        /// </summary>
        /// <param name="position"></param>
        public static void stopSample(int position)
        {
            throw new NotImplementedException();
        }

        public static void playFromPosition(int position)
        {
            throw new NotImplementedException();
        }

        public static int getVolume(int position)
        {
            throw new NotImplementedException();
        }

        public static void setVolume(int position, int volume)
        {
            throw new NotImplementedException();
        }
    }
}
