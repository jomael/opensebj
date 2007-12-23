/* 
 * AudioEngine
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


using System;
using System.Collections.Generic;
using System.Text;

namespace AudioEngine
{
    public static class AudioEngineGlobalSettings
    {
        public static int TrackEvents = 5000;
        public static int Tracks = 5000;

        // Tolerance defined here and passed through to the PrecisionTimer when starting
        public static int PrecisionTimerTolerance = 12;


        // Re-Calculate when tolerance set; saves the division in the loop
        public static int PrecisionTimerHalfTolerance = 6;
    }
}
