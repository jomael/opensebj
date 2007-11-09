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
    class Events: IComparable {
   
        /// <summary>
        /// Implementation of IComparable.CompareTo interface, to allow ordering of events in an ArrayList.
        /// </summary>
        public int CompareTo(object obj) {
            if (obj is Events)
            {
                Events anEvent = (Events)obj;

                return TimeCode.CompareTo(anEvent.TimeCode);
            }
            
            throw new ArgumentException("Object is not an Event");    
        }
        
        // The time code for the position instance
        Int64 _timeCode = -1;
        public Int64 TimeCode
        {
            get { return _timeCode; }
            set { _timeCode = value; }
        }

        // The instrument that the position represents
        int _instrument = -1;
        public int Instrument
        {
            get { return _instrument; }
            set { _instrument = value; }
        }

        // Volume for the position
        int _volume = 0;

        // Pan for the position
        int _pan = 0;

        // Active status
        bool _active = false;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        // Length of time for the instance to play for
        int _duration = 0;
        
        /// <summary>
        /// Creation of the Event
        /// </summary>
        /// <param name="TimeCode">The TimeCode for the Event position</param>
        public Events(Int64 TimeCode)
        {
            _timeCode = TimeCode;
            _active = true;
        }

        /// <summary>
        /// Creation of the Event
        /// </summary>
        /// <param name="TimeCode">The TimeCode for the Event position</param>
        /// <param name="Instrument">The Instrument the Event represents</param>
        public Events(Int64 TimeCode, int Instrument)
        {
            _timeCode = TimeCode;
            _active = true;
            _instrument = Instrument;
        }

    }
}
