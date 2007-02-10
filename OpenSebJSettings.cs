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
using System.Text;
using System.IO;

namespace OpenSebJ
{
    [Serializable]
    public class OpenSebJSettings
    {
        // This class is designed to contain all variables for OpenSebJ which are either of a static nature
        // (the C# definition, need to be accessiable in more than one class) or which can be used to save a 
        // composition for latter reloading; as this class wil be serialized by the standard methods for saving
        // and loading (rather than writing the XML serialization by hand and having to fetch many variables
        // from different locations.


        // Track Editor =====================================================================

        // Track Editor Appearance Variables
        public int TrackEditor_GlobalHeightOffset = 27;
        public int TrackEditor_TrackHeight = 100;
        public int TrackEditor_Offset = 2;
        
        // Where the new samples get positioned by default
        public int TrackEditor_SampleStartingLeftOffset = 200;
        public int TrackEditor_SampleStartingTopOffset = 0;

        // The number of Ticks per ????
        public int TrackEditor_Tick = 31;

        // Base tick, is the number of pixels from the left where the first sample can start
        public int TrackEditor_BaseTick = 100;
        public int TrackEditor_PlayTick = 100;

        // Track Editor Sample Instance's
        public int TrackEditor_SampleInstance_InAction = 0;
        public int TrackEditor_MaxInstances = 50000;
        public bool[] TrackEditor_SampleInstance_Enabled = new bool[50000];
        public int[] TrackEditor_SampleInstance_Sample = new int[50000];
        public int[] TrackEditor_SampleInstance_Location = new int[50000];

        // Track Editor Loop Related
        public int TrackEditor_LoopLocation = 300;
        public bool TrackEditor_LoopVisible = false;

        // End Track Editor =================================================================


        // Samples ==========================================================================

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

        public bool[] sampleLoaded = new bool[262];
        public string[] sampleLocations = new string[262];

        // TODO: Use sample details for serializing buffers

        // Sample Format
        public int[] sampleFormat_AverageBytesPerSecond = new int[262];
        public int[] sampleFormat_SamplesPerSecond = new int[262];
        public int[] sampleFormat_BufferBytes_Size = new int[262];
        public short[] sampleFormat_BitsPerSample = new short[262];
        public short[] sampleFormat_BlockAlign = new short[262];
        public short[] sampleFormat_Channels = new short[262];
        
        // Using a hashcode to identify the format tag - DirectX components are not normally serializable
        public int[] sampleFormat_FormatTag_HashCode = new int[262];	

        // Sample Settings - TODO : save from user positioning
        public int[] sampleSettings_Volume = new int[262];
        public int[] sampleSettings_Pan = new int[262];
        public int[] sampleSettings_Frequency = new int[262];

        // The Keycode assigned to trigger the sample
        public char[] sampleSettings_KeyCode = new char[262];

        // The array of bytes for the sample
        public MemoryStream[] sample_MemoryStream = new MemoryStream[262];

        // User details for the sample
        public string[] sampleDetails_sampleName = new string[262];
        public string[] sampleDetails_sampleSource = new string[262];
        public string[] sampleDetails_sampleCopyright = new string[262];
        

        // End Sample =======================================================================


        // Graphics =========================================================================


        // User details for the video
        public bool[] video_Loaded = new bool[262];
        public string[] video_Locations = new string[262];
        
        public string[] videoDetails_videoName = new string[262];
        public string[] videoDetails_videoSource = new string[262];
        public string[] videoDetails_videoCopyright = new string[262];

        // Video Extenstion
        public string[] videoDetails_Extension = new string[262];

        // The array of bytes for the video
        public MemoryStream[] video_MemoryStream = new MemoryStream[262];


        // User details for the image
        public bool[] image_Loaded = new bool[262];
        public string[] image_Locations = new string[262];

        public string[] imageDetails_imageName = new string[262];
        public string[] imageDetails_imageSource = new string[262];
        public string[] imageDetails_imageCopyright = new string[262];

        // Image Extenstion
        public string[] imageDetails_Extension = new string[262];

        // The array of bytes for the image
        public MemoryStream[] image_MemoryStream = new MemoryStream[262];

        // End Graphics =====================================================================

        // Beat Box =========================================================================

        // Was 32 - currently all thats displayed. But 2400 should be enough for a 5 minute
        // composition
        public bool[,] beats = new bool[256, 2400];




        // End Beat Box =====================================================================

    }
}
