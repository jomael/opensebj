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


using System;
using System.Collections.Generic;
using System.Text;

using SdlDotNet;
using SdlDotNet.Audio;

using System.Threading;

namespace OpenSebJ
{
    public static class OpenAlInterface
    {

        // Has SDL been initalised
        public static bool OpenAlInitalised = false;

        // The array of channels; used to ensure each sample is played on a seperate channel
        // and then using the sub channels to cycle the incremental notes played
        public static Channel[][] ch;

        // Main channels used to play the master audio samples
        public static int channels = 255;

        // Channel used to cycle of each sample instance played
        public static int subChannels = 8;




        // The first 256 (up to 255; start from 0 etc) positions have been 
        // allocated for user samples, all counts in the progam refer 
        // to this number.

        // The Sound array used to store the samples
        public static Sound[] aSound;

        public static bool[] loopSample = new bool[262];
        public static bool[] reverseSample = new bool[262];
        public static int[] freq = new int[262];
        
        // Used for the beat box
        public static int loadedSamples = -1;

        // For the pitch shifting roll
        public static sample[] aSample;



        /// <summary>
        /// Setup the audio subsystem
        /// </summary>
        /// <param name="callingForm">The Calling Form</param>
        public static void setupAudio(System.Windows.Forms.Control callingForm)
        {
            // Open the mixer with, high quality
            Mixer.Open(44100, AudioFormat.Default, (int)SoundChannel.Stereo, 1024);

            // Make sure enough channels are allocated to cover the complete sample range
            Mixer.ChannelsAllocated = channels * subChannels; // Set the maximum number of channels to 1000

            //ch = new Channel[channels,subChannels];
            ch = new Channel[channels][];

            // Setup the subChannels which will be cycled through to ensure that the quality is maintained
            for (int i = 0; i < channels; i++)
            {
                ch[i] = new Channel[subChannels];
            }

            // Need a unique count for channels; otherwise if the same channel declaration is assigned to
            // more than one position; all of those positions would be effected by any directive.
            // I.e. Calling play on one array position which has the same channel as another array position
            // would then be accessing the same properties
            int channelAllocated = 0;

            // Create all of the channels; so that the audio can be assigned to and played from the channel
            for (int i = 0; i < channels; i++)
            {
                for (int p = 0; p < subChannels; p++)
                {
                    // Create the channels that the audio can be played in to
                    ch[i][p] = Mixer.CreateChannel(channelAllocated++);
                }
            }

            aSound = new Sound[255];
            
            //// Setup the array to store the individual samples for each level of power
            //for (int i = 0; i < 3; i++)
            //{
            //    aSound[i] = new Sound();
            //}


            // For the pitch shifting roll
            aSample = new sample[262];

            for (int i = 0; i < 261; i++)
            {
                //sampleLocations[i] = "";
                loopSample[i] = false;
            }



            //SDL Interface Initalised
            SDLInitalised = true;
        }


        /// <summary>
        /// Load a sample
        /// </summary>
        /// <param name="fileName">The filename of the sample to load</param>
        /// <param name="position">The position to load the sample</param>
        public static string loadSample(string fileName, int position)
        {
            try
            {
                // Load the sample in to the appropriate key position; samples of the same 
                // key are split by the power level
                aSound[position] = new Sound(fileName);
                loadedSamples++;
                return "";
            }
            catch (Exception excp)
            {
                //System.Windows.Forms.MessageBox.Show(excp.ToString());

                //return excp.ToString();

                return fileName + " could not be loaded - please check the file exists before trying again";
            }
        }


        /// <summary>
        /// Play the audio sample associated with the position
        /// </summary>
        /// <param name="position">
        /// The position to be played
        /// </param>
        public static void play(int position)
        {
            for(int p = 0; p < subChannels; p++)
            {
                // If the subchannel is not playing then it can be used to play the audio
                if (ch[position][p].IsPlaying() == false)
                {
                    // Play the required sample
                    ch[position][p].Play(aSound[position], 0);

                    //Thread.Sleep(50);

                    // Ensure no more sample slots are called to be played
                    p = subChannels + 1;
                }
                // Else the last channel had already been used to play audio
                else
                {
                    // Fade out the last sample regardless of what has been assigned to the channel
                    // This helps simulate the hammer additive action of the instrument
                    // rather than a replacive nature which was previously being used
                    ch[position][p].Fadeout(100);

                    // If all of the sub channels have been used all ready then go to the first position
                    // this should also hopefully buy enough time to allow the next channel to free up
                    if (p == (subChannels - 1))
                    {
                        ch[position][0].Play(aSound[position], 0);
                    }
                }
            }

        }


        public static void stop(int position)
        {
            aSound[position].Stop();
        }

        public static void playFromPosition(int position)
        {
            throw new NotImplementedException();
        }

        public static int getVolume(int position)
        {
            return aSound[position].Volume;
        }

        public static void setVolume(int position, int volume)
        {
            aSound[position].Volume = volume;
        }
    }
}
