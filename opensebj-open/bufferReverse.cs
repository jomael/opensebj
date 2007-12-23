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
using Microsoft.DirectX.DirectSound;
using System.IO;
using System.Diagnostics;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for bufferReverse.
	/// </summary>
	public class bufferReverse
	{
		// The Temp Streams
		private System.IO.MemoryStream stream0;
		private byte[] streamBuffer0;
		private System.IO.MemoryStream stream1;
		private byte[] streamBuffer1;

		// Length of the buffer
		private int numOfBytes;
	
		public bufferReverse()
		{
			//
			// TODO: Add constructor logic here
			//
		
			
		}

		public bool reverseSample(int sampleToReverse)
		{
			numOfBytes = dsInterface.aSound[sampleToReverse].Caps.BufferBytes;
			int bytesPerSample = (dsInterface.aSound[sampleToReverse].Format.BitsPerSample * dsInterface.aSound[sampleToReverse].Format.Channels) / 8;

			CreateStreamBuffers();
			CreateStreams();

			// The alternatve location; starts at the end and works to
			// the begining
			int b = 0;
			
			// Read the complete stream in to a memory stream
			dsInterface.aSound[sampleToReverse].Read(0,stream0,numOfBytes,Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);	
			
			//Prime the loop by 'reducing' the numOfBytes by the first increment for the first sample 
			numOfBytes = numOfBytes - bytesPerSample;

			// Used for the imbeded loop to move the complete sample
			int q = 0;

			// Moves through the stream based on each sample
			for(int i=0; i < numOfBytes - bytesPerSample; i = i + bytesPerSample)
			{
				// Location of streamBuffer1 position, in the reversal process
				// Effectively a mirroing process; b will equal i (or be out by one if its an equal buffer)
				// when the middle of the buffer is reached.
				b = numOfBytes - bytesPerSample - i;
				
				// Copies the 'sample' in whole to the opposite end of streamBuffer1
				for (q = 0; q <= bytesPerSample; q ++)
				{
					streamBuffer1[b + q] = streamBuffer0[i + q];
				}				
			}

			// Writes back the reversed stream to the origional sample buffer
			dsInterface.aSound[sampleToReverse].Write(0,stream1,numOfBytes,Microsoft.DirectX.DirectSound.LockFlag.EntireBuffer);

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
	}
}
