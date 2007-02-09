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

namespace OpenSebJ
{
    public class sample
    {
        private int[] _frequency = new int[8];
        private int _nextFreqPosition = 0;
        private bool[] _enabled = new bool[8];

        /// <summary>
        /// Set the frequency for the roll
        /// </summary>
        /// <param name="freqPosition">The position in the frequency roll</param>
        /// <param name="frequency">The actual frequency</param>
        public void setFreq(int freqPosition, int frequency)
        {
            _frequency[freqPosition] = frequency;
        }

        /// <summary>
        /// Get the next frequency set for the frequency roll
        /// </summary>
        /// <returns>Next frequency</returns>
        public int getNextFrequency()
        {
            int _theFreq = 0;

            for (int i = 0; i < 8; i++)
            {
                if (_enabled[_nextFreqPosition] == true)
                {
                    _theFreq = _frequency[_nextFreqPosition];
                    _nextFreqPosition++;
                    i = 8;
                }
                else
                {
                    _nextFreqPosition++;
                }
                
                if (_nextFreqPosition == 8)
                {
                    _nextFreqPosition = 0;
                }
            }

            // If no new freq was found, DS will automatically set back to
            // Default - seems like a similar issue to the upper bound? ;-)
            return _theFreq;
        }

        /// <summary>
        /// Set if the frequency roll position is enabled
        /// </summary>
        /// <param name="freqPosition">The frequency roll position</param>
        /// <param name="enabled">Set its enabled status</param>
        public void setRollPosEnabled(int freqPosition, bool enabled)
        {
            _enabled[freqPosition] = enabled;
        }

        public int getFreqByPos(int freqPosition)
        {
            int returnFreq = -1;
            
            if (_enabled[freqPosition] == true){
                returnFreq = _frequency[freqPosition];
            }

            return returnFreq;
        }
    }
}
