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
    class Track
    {
        // The starting time code for the track - can not be less than 0; as such is set to 0 as the default.
        Int64 _timeCode = 0;
        public Int64 TimeCode
        {
            get { return _timeCode; }
            set { _timeCode = value; }
        }

        // The instrument that the track represents (in a generic sence, as each event can represent a different instrument)
        int _instrument = -1;

        // Default volume for the track
        int _volume = 0;

        // Default volume for the track
        int _pan = 0;

        // Track Active status
        bool _active = false;


        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private int _trackNumber;

        /// <summary>
        /// Creates a new Track
        /// </summary>
        /// <param name="Name">The name of the Track</param>
        /// <param name="TrackNumber">The Track Number</param>
        public Track(string Name, int TrackNumber)
        {
            _name = Name;
            _trackNumber = TrackNumber;

        }

        /// <summary>
        /// Removes all active events for a track
        /// </summary>
        /// <returns>True after events have been removed</returns>
        public bool RemoveTrackEvents()
        {
            _events = null;
            return true;
        }

        //public string GetName()
        //{
        //    return _name;
        //}


        private Events[] _events = new Events[AudioEngineGlobalSettings.TrackEvents];
        private int eventCount = -1;

        #region Events


        /// <summary>
        /// Add an event to the Track
        /// </summary>
        /// <param name="timeCode">The TimeCode for the position of the Event</param>
        /// <returns></returns>
        public int AddEvent(Int64 timeCode)
        {
            int eventNumber = eventCount++;

            // Check for an empty event and reuse if available
            for (int i = 0; i <= eventCount; i++)
            {
                if (EventAvailable(i))
                {
                    // Reuse an earlier spare event
                    eventNumber = i;
                    
                    // Exit Loop
                    i = eventCount; 
                }
            }

            // Setup the event
            _events[eventNumber] = new Events(timeCode);

            return eventNumber;
        }

        private bool EventAvailable(int eventNumber)
        {
            bool available = false;
            if (_events[eventNumber] == null)
            {
                available = true;
            }
            return available;
        }

        /// <summary>
        /// Remove a specific event from the track
        /// </summary>
        /// <param name="eventNumber">The event number to remove</param>
        /// <returns>True on sucessful removal</returns>
        public bool RemoveEvent(int eventNumber)
        {
            bool removed = false;

            try
            {
                if (_events[eventNumber] != null)
                {
                    _events[eventNumber] = null;
                    eventCount--;
                    removed = true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Can not remove an Event out of range
            }

            return removed;
        }


        /// <summary>
        /// Get all of the Time Codes for events assigned to the track
        /// </summary>
        /// <param name="Active">True: Active time codes only. False: All time codes</param>
        /// <param name="IncludeTrackOffset">True: Add the current Track Offset when reporting positions. i.e. the events current global time code independant of the track's starting time code.</param>
        /// <returns>Int64[] with all of the relevant time code events</returns>
        internal Int64[] GetEventTimeCodes(bool Active, bool IncludeTrackOffset)
        {
            Int64[] EventTimeCodes = new Int64[AudioEngineGlobalSettings.TrackEvents];

            for (int i = 0; i < AudioEngineGlobalSettings.TrackEvents; i++)
            {
                if (_events[i] != null)
                {
                    //Only want active events
                    if (Active && _events[i].Active)
                    {
                        if (IncludeTrackOffset)
                        {
                            EventTimeCodes[i] = _events[i].TimeCode + _timeCode;
                        }
                        else
                        {
                            EventTimeCodes[i] = _events[i].TimeCode;
                        }
                    }
                    else
                    {
                        // non active events to be included
                        if (Active == false)
                        {
                            if (IncludeTrackOffset)
                            {
                                EventTimeCodes[i] = _events[i].TimeCode + _timeCode;
                            }
                            else
                            {
                                EventTimeCodes[i] = _events[i].TimeCode;
                            }
                        }
                    }
                }
                else
                {
                    // If the event has not been set a value, then a negative value is returned in the time code array
                    EventTimeCodes[i] = -1;

                }
            }

            return EventTimeCodes;
        }

        


        internal Events[] GetEvents(bool Active)
        {
            Events[] EventCollection = new Events[AudioEngineGlobalSettings.TrackEvents];

            for (int i = 0; i < AudioEngineGlobalSettings.TrackEvents; i++)
            {
                if (_events[i] != null)
                {
                    if (Active && _events[i].Active)
                    {
                        EventCollection[i] = _events[i];
                    }
                    else
                    {
                        if (Active == false)
                        {
                            EventCollection[i] = _events[i];
                        }
                    }
                }
                else
                {
                    // If the event has not been set a value, then a negative value is returned in the time code array
                    EventCollection[i] = null;

                }
            }

            return EventCollection;
        }


        #endregion
    }
}
