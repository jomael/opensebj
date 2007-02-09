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
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

using System.Threading;
using System.Diagnostics;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for dsInterface.
	/// </summary>
	public class dsInterface
	{
		public static Microsoft.DirectX.DirectSound.Device aSoundCard;
		
		// The first 256 (up to 255; start from 0 etc) positions have been 
		// allocated for user samples, all counts in the progam refer 
		// to this number.
		
		// Position 256 to 261 are utalised for the scratch sub program
		// as these need to be primarily controlled programatically

		// SLOT SUMMARY
		// 0 to 255 - User loaded samples
		// 256 - Sample1 Pre Fast Forwarded for Scratch
		// 257 - Sample1 Pre Reversed
		// 258 - Sample1 Pre Reversed and Fast Forwarded

		// 259 - Sample2 Pre Fast Forwarded for Scratch
		// 260 - Sample2 Pre Reversed
		// 261 - Sample2 Pre Reversed and Fast Forwarded

		public static Microsoft.DirectX.DirectSound.SecondaryBuffer[] aSound;
		//public static string[] sampleLocations = new string[262];
		public static bool[] loopSample = new bool[262];
		public static bool[] reverseSample = new bool[262];
		public static int[] freq = new int[262];
        public static int loadedSamples = -1;

        // For the pitch shifting roll
        public static sample[] aSample;


        //public static string[] imageLocations = new string[256];
        //public static string[] videoLocations = new string[256];



		
        public dsInterface()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void setupAudio(System.Windows.Forms.Control callingForm)
		{			
			
			aSoundCard = new Device();
			aSoundCard.SetCooperativeLevel(callingForm, CooperativeLevel.Priority);
			
			aSound = new SecondaryBuffer[262];
            
            // For the pitch shifting roll
            aSample = new sample[262];

			for(int i = 0; i < 261; i++)
			{
				//sampleLocations[i] = "";
				loopSample[i] = false;
			}
			


			//aSound[255].Play(0,Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);

			//Microsoft.DirectX.DirectSound.EffectDescription[] bufferEffects = new EffectDescription[256];

			
			

			//bufferEffects[0] = Microsoft.DirectX.DirectSound.DSoundHelper.StandardChorusGuid;
			//aSound[255].SetEffects(bufferEffects);
			//aSound[255].Play(0,Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);
			
		}
		
		public static void play(int sample)
		{

            if (globalSettings.osj.sampleLoaded[sample] == false)
            {
                System.Windows.Forms.MessageBox.Show("Sample not loaded");
                return;
            }

            //Removing check to see if sample is loaded before attempting to play
            //if (sample <= loadedSamples)
            {
                 // To support a the frequency roll
                shiftPitch(sample);

                // Continue to play the sample
                try
                {
                    if (aSound[sample].Status.Playing == false)
                    {

                        if (loopSample[sample] == false)
                        {
                            // Play One Shot
                            aSound[sample].Play(0, BufferPlayFlags.Default);
                        }
                        else
                        {
                            // Play Loop
                            aSound[sample].Play(0, BufferPlayFlags.Default | BufferPlayFlags.Looping);
                        }
                    }
                    else
                    {
                        //aSound[sample].Stop();

                        //Resets the play cursor to the begining
                        aSound[sample].SetCurrentPosition(0);

                        if (loopSample[sample] == false)
                        {
                            //Play One Shot
                            aSound[sample].Play(0, BufferPlayFlags.Default);
                        }
                        else
                        {
                            // Play Loop
                            aSound[sample].Play(0, BufferPlayFlags.Default | BufferPlayFlags.Looping);
                        }
                    }

                    // Play Video / Display Image
                    eRender.show(sample);

                }
                catch
                {
                    // This error handling tries to replay the sample before
                    // displaying an error; this error message was being show
                    // intermitently, seems DX would fail playing the same file
                    // for some reason; the first step is to try again; which
                    // seems to remove the intermintent error from poping up...

                    // TODO: Add logging code when the exception is thrown.
                    try
                    {
                        if (aSound[sample].Status.Playing == false)
                        {

                            if (loopSample[sample] == false)
                            {
                                // Play One Shot
                                aSound[sample].Play(0, BufferPlayFlags.Default);
                            }
                            else
                            {
                                // Play Loop
                                aSound[sample].Play(0, BufferPlayFlags.Default | BufferPlayFlags.Looping);
                            }
                        }
                        else
                        {
                            //aSound[sample].Stop();

                            //Resets the play cursor to the begining
                            aSound[sample].SetCurrentPosition(0);

                            if (loopSample[sample] == false)
                            {
                                //Play One Shot
                                aSound[sample].Play(0, BufferPlayFlags.Default);
                            }
                            else
                            {
                                // Play Loop
                                aSound[sample].Play(0, BufferPlayFlags.Default | BufferPlayFlags.Looping);
                            }
                        }

                        // Play Video / Display Image
                        eRender.show(sample);
                    
                    }
                    catch (Exception dsExc)
                    {
                        int sampleOffset = sample + 1;
                        System.Windows.Forms.MessageBox.Show("There seems to be a problem with sample number " + sampleOffset.ToString() + ". Please load a new sample in this bank and try again. Direct X error message follows :: " + dsExc.Message.ToString(), "Error with sample number " + sampleOffset.ToString());
                    }
                }

            }
            //else
            //{
            //    // The sample ain't loaded

            //}
		}



		public static void playFromPosition(int sample)
		{
            // To support a the frequency roll
            shiftPitch(sample);

            // Continue to play the sample
			try
			{
				if (aSound[sample].Status.Playing == false)
				{
					
					if (loopSample[sample] == false)
					{
						// Play One Shot
						aSound[sample].Play(0, BufferPlayFlags.Default);
					}
					else
					{
						// Play Loop
						aSound[sample].Play(0, BufferPlayFlags.Default|BufferPlayFlags.Looping);
					}
				}
				else
				{
					if (loopSample[sample] == false)
					{
						//Play One Shot
						aSound[sample].Play(0, BufferPlayFlags.Default);
					}
					else
					{
						// Play Loop
						aSound[sample].Play(0, BufferPlayFlags.Default|BufferPlayFlags.Looping);
					}
				}
			}
			catch
			{
				// This error handling tries to replay the sample before
				// displaying an error; this error message was being show
				// intermitently, seems DX would fail playing the same file
				// for some reason; the first step is to try again; which
				// seems to remove the intermintent error from poping up...

				// TODO: Add logging code when the exception is thrown.
				try
				{
					if (aSound[sample].Status.Playing == false)
					{
					
						if (loopSample[sample] == false)
						{
							// Play One Shot
							aSound[sample].Play(0, BufferPlayFlags.Default);
						}
						else
						{
							// Play Loop
							aSound[sample].Play(0, BufferPlayFlags.Default|BufferPlayFlags.Looping);
						}
					}
					else
					{
						if (loopSample[sample] == false)
						{
							//Play One Shot
							aSound[sample].Play(0, BufferPlayFlags.Default);
						}
						else
						{
							// Play Loop
							aSound[sample].Play(0, BufferPlayFlags.Default|BufferPlayFlags.Looping);
						}
					}

				}
				catch (Exception dsExc)
				{
					int sampleOffset = sample + 1;
					System.Windows.Forms.MessageBox.Show("There seems to be a problem with sample number " + sampleOffset.ToString() + ". Please load a new sample in this bank and try again. Direct X error message follows :: " + dsExc.Message.ToString(), "Error with sample number " + sampleOffset.ToString());
				}
			}
		}


		public static void playInThread(int sample)
		{

			// Supply the state information required by the task.
			ThreadWithState tws = new ThreadWithState(0);
			// Create a thread to execute the task, and then
			// start the thread.
			Thread t = new Thread(new ThreadStart(tws.ThreadProc));
			

			// Seems to work because the play cursor is being moved in the
			// thread rather than being stoped and started
			if (aSound[sample].Status.Playing == true)
			{
				aSound[sample].Stop();
				
			}
			else
			{
				t.Start();
			}
			
			// If the sample is already playing and t.Start is called
			// mulltiple instances will be played at the same time.. iteresting effect

			
			//t.Join();
			

		}

		public class ThreadWithState 
		{
			// State information used in the task.
			private int value;

			// The constructor obtains the state information.
			public ThreadWithState(int number) 
			{
				value = number;
			}
           
			// The thread procedure performs the task, such as formatting 
			// and printing a document.
			public void ThreadProc() 
			{
				//aSound[value].Volume = 50;

				int i = aSound[value].Caps.BufferBytes - 1;

				for(; i > 1; i--)
				{
					aSound[value].SetCurrentPosition(i);
					aSound[value].Play(0, BufferPlayFlags.Default);
				}
				aSound[value].Stop();

				
			}
		}



		public static int getSampleBytes(int sample)
		{
			return aSound[sample].Caps.BufferBytes;
		}





		public static string loadSample (string fileLocation, int position)
		{
			Microsoft.DirectX.DirectSound.BufferDescription bufferDes = new BufferDescription();
			bufferDes.CanGetCurrentPosition = true;
			bufferDes.ControlFrequency = true;
			bufferDes.ControlPan = true;
			bufferDes.ControlVolume = true;
			bufferDes.GlobalFocus = true;
			bufferDes.ControlEffects = true;
			
			try
			{
				aSound[position] = new SecondaryBuffer(fileLocation, bufferDes, aSoundCard);
				freq[position] = aSound[position].Frequency;
                
                //For Frequency Roll
                aSample[position] = new sample();
                
                loadedSamples++;
				return "";
			}
			catch (Exception e)
			{
				return e.Message.ToString();
			}
		}

		public static string setupBlankSample (int position, int size, int sampleToCopy)
		{
			// Used when speeding up samples so that the new buffer
			// matches the size of the actual sample, rather than leaving
			// an amount of space at the end, which sounds like shit
			// when it's played as a loop.

			// This lovely and seemingly useless block of code was a pain
			// to pinpoint. When I was trying to create a blank buffer
			// using a memory stream, I recived a System.ArgumentException
			// the painful peice being that I could't pin point which argument
			// was invalid. For those of you at home, you need to setup the 
			// waveFormatTag FIRST, then create the buffer. This peice I 
			// missed when reading the instructions as it is done automatically
			// when loading from a file but if your copying a memory stream
			// it obviously doesn't know what exactly is in the stream, so
			// you need to define it - even if you are copying an existing stream..

			// If any one from Micr0$0ft reads this - I would really think a few
			// more examples would be handy when your dealing with parts of the
			// api which aren't going to return freindly error messages..

			Microsoft.DirectX.DirectSound.WaveFormat waveFormatTag = new WaveFormat();
			waveFormatTag.AverageBytesPerSecond = aSound[sampleToCopy].Format.AverageBytesPerSecond;
			waveFormatTag.BitsPerSample = aSound[sampleToCopy].Format.BitsPerSample;
			waveFormatTag.BlockAlign = aSound[sampleToCopy].Format.BlockAlign;
			waveFormatTag.Channels = aSound[sampleToCopy].Format.Channels;
			waveFormatTag.FormatTag = aSound[sampleToCopy].Format.FormatTag;
			waveFormatTag.SamplesPerSecond = aSound[sampleToCopy].Format.SamplesPerSecond;

			// After the wave format header is defined, setting up the empty
			// buffer follows the same principles as loading from a file.

			Microsoft.DirectX.DirectSound.BufferDescription bufferDes = new BufferDescription(waveFormatTag);
			bufferDes.CanGetCurrentPosition = true;
			bufferDes.ControlFrequency = true;
			bufferDes.ControlPan = true;
			bufferDes.ControlVolume = true;
			bufferDes.GlobalFocus = true;
			bufferDes.ControlEffects = true;
			bufferDes.BufferBytes = size;

			try
			{
				aSound[position] = new SecondaryBuffer(bufferDes, aSoundCard);

                // Required for frequency roll mod - and yes it s not used for the scratch interface
                freq[position] = aSound[position].Frequency;
                aSample[position] = new sample();

				return "";
			}
			catch (Exception e)
			{
				return e.Message.ToString();
			}
		}

		public static Microsoft.DirectX.DirectSound.Caps getDeviceCaps()
		{
			return aSoundCard.Caps;
		}

		public static void stop(int sample)
		{
			try
			{	
				aSound[sample].Stop();
				aSound[sample].SetCurrentPosition(0);
                eRender.pauseVideo(sample);
			}
			catch //sample probably not loaded yet?
			{}
			
			
			//aSound[sample]
			//Microsoft.DirectX.DirectSound.BufferPlayFlags sampleFlags = new BufferPlayFlags();
			
			//BufferDescription desc = new BufferDescription();
			//desc.Flags = BufferCaps.ControlPan | BufferCaps.ControlVolume | BufferCaps.ControlFrequency;
		}


		public static void setVolume(int sample, int volume)
		{
			aSound[sample].Volume = volume;
		}

		public static void setPan(int sample, int pan)
		{
			aSound[sample].Pan = pan;
		}

		public static int getVolume(int sample)
		{
			return aSound[sample].Volume;
		}

		public static int getPan(int sample)
		{
			return aSound[sample].Pan;
		}

		public static float getLength(int sample)
		{
            // Removed the check for loaded sample here
            //if (sample <= loadedSamples)
            //{
                try
                {
                    int bytesPerSample = (aSound[sample].Format.BitsPerSample * aSound[sample].Format.Channels) / 8;
                    int numOfBytes = aSound[sample].Caps.BufferBytes;
                    int numberOfSamples = numOfBytes / bytesPerSample;
                    //Was returning the base time (not taking in to account any changes in frequency)
                    //float lengthOfSampleInSeconds = (float)numberOfSamples / (float)aSound[sample].Format.SamplesPerSecond;
                    float lengthOfSampleInSeconds = (float)numberOfSamples / (float)aSound[sample].Frequency;
                    return lengthOfSampleInSeconds;
                }
                catch
                {
                    return (float)-1;
                }
            //}
            //else
            //{
            //    // Again no sample loaded
            //    return (float)-1;
            //}
			
		}

		public static int getChannels(int sample)
		{
			return aSound[sample].Format.Channels;
		}

		public static int getBitsPerSample(int sample)
		{
			return aSound[sample].Format.BitsPerSample;
		}

		public static int getFrequency(int sample)
		{
			return aSound[sample].Format.SamplesPerSecond;
		}

		public static int getCurrentFrequency(int sample)
		{
			return aSound[sample].Frequency;
		}

		public static void setFrequency(int sample, int frequency)
		{
				aSound[sample].Frequency = frequency;
		}

		public static int getPlayPosition(int sample)
		{
			return aSound[sample].PlayPosition;
		}

		public static void playKey(int keyCode, bool ctrl)
		{
			for (int i = 0; i <= 255; i++)
			{
                //Old format and settings
				//if (layOut.setKey[i] == keyCode)
                if (globalSettings.osj.sampleSettings_KeyCode[i] == keyCode)
				{
                    // Check for ctrl key & stop if depressed.
                    if (ctrl == false)
                    {
                        dsInterface.play(i);
                    }
                    else
                    {
                        dsInterface.stop(i);
                    }
				}
			}
		}

        public static int getKeySample(int keyCode)
        {
            int sample = -1;

            for (int i = 0; i <= 255; i++)
            {
                if (globalSettings.osj.sampleSettings_KeyCode[i] == keyCode)
                {
                    sample = i;
                }
            }

            return sample;
        }



        public static void setFreqRoll(int sample, int freqPosition, int frequency)
        {
            aSample[sample].setFreq(freqPosition, frequency);
        }

        public static void setFreqRollEnabled(int sample, int rollPos, bool enabled)
        {
            aSample[sample].setRollPosEnabled(rollPos, enabled);
        }


        //public static void nextFreqPlay(int sample)
        //{
        //    aSound[sample].Frequency = aSample[sample].getNextFrequency();

        //    //System.Windows.Forms.MessageBox.Show(aSound[sample].Frequency.ToString());

        //    dsInterface.play(sample);
            
        //    //aSound[sample].SetCurrentPosition(0);
        //    //if (aSound[sample].Status.Playing == false)
        //   // {
        //        //aSound[sample].Play(0, BufferPlayFlags.Default);
        //    //}
        //}

        public static void shiftPitch(int sample)
        {
            int newFreq = 0;

            newFreq = aSample[sample].getNextFrequency();

            // If a value was returned set the sample to the new frequency
            if (newFreq != 0)
            {
                aSound[sample].Frequency = newFreq;
            }
            // Set the frequency back to the default
            else
            {
                aSound[sample].Frequency = dsInterface.getCurrentFrequency(sample);
            }

        }

    }// End Of Class
}
