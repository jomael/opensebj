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
using Microsoft.DirectX.DirectSound;
using System.IO;
using System.Diagnostics;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for bufferFast.
	/// </summary>
	public class bufferFast
	{
		// The Temp Streams
		private System.IO.MemoryStream stream0;
		private byte[] streamBuffer0;
		private System.IO.MemoryStream stream1;
		private byte[] streamBuffer1;
		private System.IO.MemoryStream fastStream;
		private byte[] streamBufferFast;

		// Length of the buffer
		private int numOfBytes;
	
		public bufferFast()
		{
			//
			// TODO: Add constructor logic here
			//
		
			
		}

		public bool FastForwardSample(int sampleToFastForward, int slotDesignation)
		{
			numOfBytes = dsInterface.aSound[sampleToFastForward].Caps.BufferBytes;
			int bytesPerSample = (dsInterface.aSound[sampleToFastForward].Format.BitsPerSample * dsInterface.aSound[sampleToFastForward].Format.Channels) / 8;

			CreateStreamBuffers();
			CreateStreams();

			// The alternatve location; starts at the end and works to
			// the begining
			int b = 0;
			
			// Read the complete stream in to a memory stream
			dsInterface.aSound[sampleToFastForward].Read(0,stream0,numOfBytes,Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);	
			
			//Prime the loop by 'reducing' the numOfBytes by the first increment for the first sample 
			numOfBytes = numOfBytes - bytesPerSample;

			// Used for the imbeded loop to move the complete sample
			int q = 0;

			// The counter to skip samples
			int skip = 0;

			// Moves through the stream based on each sample
			for(int i=0; i < numOfBytes - bytesPerSample; i = i + bytesPerSample)
			{
			
				// The number after skip defines how fast, the fast forwarded sample
				// will be; in this case every 5th sample is dropped.
				// (Based on less than 4 etc etc)
				if (skip < 4)
				{
					skip++;
					
					// Increments the conter to the next position
					b = b + bytesPerSample;

					// Copies the 'sample' in whole to the next available position
					// effectively bunching the appropriate samples together
					for (q = 0; q <= bytesPerSample; q ++)
					{
						streamBuffer1[b + q] = streamBuffer0[i + q];
					}			
				}
				else
				{
					skip = 0;
					// Not incrementing b - while allowing i to increment, effectively skips a sample
				}
			}


			// Writes back the fast forwarded stream to the origional sample buffer
			//dsInterface.aSound[sampleToFastForward].Write(0,stream1,numOfBytes,Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);

			// Create a new stream buffer; which is the now correct size of
			// the shortened sample
			createFastStreamBuffer(b + bytesPerSample);
			createFastStream();

			// Copy sample by sample to the fastStream
			for(int i=0; i < b - bytesPerSample; i = i + bytesPerSample)
			{
				for (q = 0; q <= bytesPerSample; q ++)
				{
					streamBufferFast[i + q] = streamBuffer1[i + q];
				}
			}

			// Setup the new blank sample, passing the position number,
			// length and previous sample so the correct format can be
			// detremined
			string result = dsInterface.setupBlankSample(slotDesignation,b,sampleToFastForward);
			if (result != "")
			{
				// Show any error which occured.
				System.Windows.Forms.MessageBox.Show(result);
			}
			else
			
			// Write the shortened stream to the newly created buffer.
			dsInterface.aSound[slotDesignation].Write(0,fastStream,b,Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);

			return true;
		}
		
	
		private void CreateStreamBuffers()
		{
			streamBuffer0 = new byte[numOfBytes];
			for(int i=0; i< numOfBytes; i++)
				streamBuffer0[i] = 0;
		


			streamBuffer1 = new byte[numOfBytes];
			for(int i=0; i< numOfBytes; i++)
				streamBuffer1[i] = 0;
		
		} 
		
		private void CreateStreams()  
		{
			stream0 = new MemoryStream(streamBuffer0);
			stream1 = new MemoryStream(streamBuffer1);
		}

		private void createFastStreamBuffer(int size)
		{
			streamBufferFast = new byte[size];
			for(int i=0; i< size; i++)
				streamBufferFast[i] = 0;
		}

		public void createFastStream()
		{
			fastStream = new MemoryStream(streamBufferFast);
		}
	}
}
