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
//using System.Timers;
using System.Diagnostics;
using System.Threading;

namespace AudioEngine
{
    public class Sequencer
    {
        // Used to calculate the movement between positions
        private int _bpm = 120;
        #region Get/Set BPM 
        public int BPM
        {
            get //get accessor method
            {
                return _bpm;
            }
            set //set accessor method
            {
                if (value > 0)
                {
                    _bpm = value;
                }
            }
        }
        #endregion

        // Used for calculating the minimum level of position granularity when quantising
        private int _positionsPerBeat = 4;
        #region Get/Set Positions Per Beat
        public int positionsPerBeat
        {
            get //get accessor method
            {
                return _positionsPerBeat;
            }
            set //set accessor method
            {
                if (value > 0)
                {
                    _positionsPerBeat = value;
                }
            }
        }
        #endregion


        // Array of all Events for playback, used to identify when the next call back should be triggered.
        // TODO: Make sure at play back that this is repopulated 
        Events[] _orderedEvents = new Events[AudioEngineGlobalSettings.Tracks * AudioEngineGlobalSettings.TrackEvents];

        static Int64 firedTotal = 0;

        //static PrecisionTimer _pt = new PrecisionTimer();

        // The PrecisionTimer object. Declared here so that delegate events can be marsheled appropriatly
        //PrecisionTimer ptObject;

        // Tracks represent each instance of an instrument and contain the positioning of events
        private Track[] _tracks = new Track[AudioEngineGlobalSettings.Tracks];
        private int trackCount = -1;

        // The last event which was played - reduces the searching required when cycling through events
        // to play
        private int lastEventPlayed = 0;
        
        #region Tracks

        /// <summary>
        /// Add a new track to the sequencer
        /// </summary>
        /// <param name="name">The name of the track</param>
        /// <returns>The created track Number</returns>
        public int AddTrack(string name)
        {
            int trackNumber = trackCount++;

            // Check for an empty track and reuse if available
            for (int i = 0; i <= trackCount; i++)
            {
                if (TrackExists(i) == false)
                {
                    // Reuse an earlier spare track
                    trackNumber = i;
                    
                    // Exit Loop
                    i = trackCount; 
                }
            }

            // No spare tracks
            //trackNumber = trackCount++;

            // Setup the track
            _tracks[trackNumber] = new Track(name, trackNumber);

            return trackNumber;
        }

        /// <summary>
        /// Checks to see if the track has been created
        /// </summary>
        /// <param name="Track">The Track Number for checking</param>
        /// <returns>True: If the track has been created</returns>
        public bool TrackExists(int Track)
        {
            bool created = false;
            if (_tracks[Track] != null)
            {
                created = true;
            }
            return created;
        }

        /// <summary>
        /// Removes the track and all associated events
        /// </summary>
        /// <param name="trackNumber">The track number</param>
        /// <returns>Bool based on success</returns>
        public bool RemoveTrack(int trackNumber)
        {
            bool removed = false;

            try
            {
                if (_tracks[trackNumber] != null)
                {
                    removed = _tracks[trackNumber].RemoveTrackEvents();
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Can not remove a track out of range
            }

            if (removed == true)
            {
                _tracks[trackNumber] = null;
                trackCount--;
            }

            return removed;
        }

        /// <summary>
        /// Get the track name for the specified track number
        /// </summary>
        /// <param name="trackNumber">The track number for which the Track name is being sought</param>
        /// <returns>The track name</returns>
        public string GetTrackName(int trackNumber)
        {
            string trackName = "";

            try
            {
                if (_tracks[trackNumber] != null)
                {
                    trackName = _tracks[trackNumber].Name;
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Can not get a track name which does not exist
            }

            return trackName;
        }


        //public int MaximumTrackPosition()
        //{
        //    return _tracks.Length;
        //}

        #endregion

        #region Events

        /// <summary>
        /// Add an Event to the Track
        /// </summary>
        /// <param name="trackNumber">The Track Number</param>
        /// <param name="eventTimeCode">The Time Code for the Event</param>
        /// <returns>Bool based on success</returns>
        public bool AddEvent(int trackNumber, Int64 eventTimeCode)
        {
            bool eventAdded = false;
            int eventNumber = -1;
            
            // Check the track exists
            try
            {
                if (_tracks[trackNumber] != null)
                {
                    eventNumber = _tracks[trackNumber].AddEvent(eventTimeCode);

                    if (eventNumber >= 0)
                    {
                        eventAdded = true;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Can not get a track name which does not exist
            }

            return eventAdded;
        }

        /// <summary>
        /// Gets the Event Time Codes for a give Track
        /// </summary>
        /// <param name="Track">The Track where the Time Codes are located</param>
        /// <param name="Active">True: Active time codes only. False: All time codes</param>
        /// <returns>Int64[] with all of the relevant time code events</returns>
        public Int64[] GetTrackEventTimeCodes(int Track, bool Active)
        {
            Int64[] TrackEventTimeCodes = new Int64[AudioEngineGlobalSettings.TrackEvents];

            // Check that the track exists
            if (_tracks[Track] != null)
            {
                //Get all of the time codes
                TrackEventTimeCodes = _tracks[Track].GetEventTimeCodes(Active);
            }
            else
            {
                // Track doesn't exist, need to return null
                TrackEventTimeCodes = null;
            }

            return TrackEventTimeCodes;
            
        }

        #endregion


        /// <summary>
        /// Get the next Time Code of any Event for any Track, based on the Current Time Code
        /// </summary>
        /// <param name="CurrentTimeCode">The Current Time Code, from which the next Time Code</param>
        /// <returns>The next Time Code</returns>
        private Int64 GetNextTimeCode(Int64 CurrentTimeCode)
        {
            Int64 nextTimeCode = -1;
            
            //TODO: Finish

            return nextTimeCode;
        }

        /// <summary>
        /// Identifys and aranges all time codes in order for play back
        /// </summary>
        private Int64[] OrderTimeCodes()
        {
            Int64[] otc = new Int64[AudioEngineGlobalSettings.Tracks * AudioEngineGlobalSettings.TrackEvents];


            System.Collections.ArrayList timeCodeList = new System.Collections.ArrayList();

            for (int i = 0; i < _tracks.Length; i++)
            {
                if (_tracks[i] != null)
                {
                    Int64[] orderTime = new Int64[AudioEngineGlobalSettings.TrackEvents];
                    orderTime = _tracks[i].GetEventTimeCodes(true);

                    for (int q = 0; q < AudioEngineGlobalSettings.TrackEvents; q++)
                    {
                        // So that non negative (i.e. unasigned) time codes are not added to the list
                        if (orderTime[q] >= 0)
                        {
                            timeCodeList.Add(orderTime[q]);
                        }
                    }
                }
            }

            // Sort the time codes in to assending order
            timeCodeList.Sort();
            
            // Copy the time codes back to Int64[]
            timeCodeList.CopyTo(otc);

            return otc;

        }


        public Int64[] GetAllTimeCodesOrdered()
        {
            // All Time Codes, ordered for play back
            Int64[] _orderedTimeCodes = new Int64[AudioEngineGlobalSettings.Tracks * AudioEngineGlobalSettings.TrackEvents];

            _orderedTimeCodes = OrderTimeCodes();

            return _orderedTimeCodes;

        }


        /// <summary>
        /// Plays the current composition
        /// </summary>
        public void Play()
        {
            // Refresh the ordered time codes
            _orderedEvents = GetOrderedEvents();
            
            // Reset the lastEventPlayed so that searching through events to play can
            // start from the last event played
            lastEventPlayed = 0;


            StartPrecisionTimer(AudioEngineGlobalSettings.PrecisionTimerTolerance);

            PlayEvents(0);



            //int playTimeCode = 0;
            
            
            //Timer
            //System.Timers.Timer _timer = new System.Timers.Timer();

            //// Set in milliseconds
            //_timer.Interval = 1;

            //// Call back method when the timer hits the interval
            //_timer.Elapsed += new ElapsedEventHandler(TimerFired);
            //End Timer
            

            //_timer.Start();

            

            








            //for (int i = 0; i < _orderedEvents.Length; i++)
            //{
                    

            //}




            // // All Time Codes, ordered for play back
            // Int64[] _orderedTimeCodes = new Int64[GlobalSettings.Tracks * GlobalSettings.TrackEvents];
         
            //_orderedTimeCodes = OrderTimeCodes();

            
        }


        ///// <summary>
        ///// TimerFired - Bang
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="e"></param>
        //private static void TimerFired(object source, ElapsedEventArgs e)
        //{
            

           

        //    firedTotal = firedTotal + 1;

        //    //Console.Write(_pt.GetElapsedMilliseconds().ToString() + "   ");
        //    //Console.Write(firedTotal.ToString() + "     ");
        //    //Console.Write(_pt.GetElapsedTime().ToString() + "    ");  
        //    ////Console.Write(thedt.TotalMilliseconds.ToString() + "    ");
        //    //Console.WriteLine();
            

        //    //PlayEvents(_playTimeOffset - dt);
        //}

        private void PlayEvents(Int64 Tick)
        {
            Console.WriteLine("Play " + Tick.ToString());

            for (int i = lastEventPlayed; i < _orderedEvents.Length; i++)
            {
                if (_orderedEvents[i] != null)
                {
                    if (_orderedEvents[i].TimeCode == Tick)
                    {
                        Console.WriteLine("Play Event - Instrument " + _orderedEvents[i].Instrument + " Time Code " + Tick.ToString());

                        // TODO: Actually play the sample ;-)

                        lastEventPlayed = i;
                    }
                    else
                    {
                        // Check if we have iterated past the last event, with a time code the same as the 
                        // current play tick
                        if (_orderedEvents[i].TimeCode > Tick)
                        {
                            i = _orderedEvents.Length;
                        }
                    }
                }
                else
                {
                    i = _orderedEvents.Length;
                }
            }
        }

        /// <summary>
        /// Returns an array of all active events, ordered in time code, from all Tracks
        /// </summary>
        /// <returns>All active ordered Events</returns>
        private Events[] GetOrderedEvents()
        {

            Events[] OrderedEventCollection = new Events[AudioEngineGlobalSettings.Tracks * AudioEngineGlobalSettings.TrackEvents];
            

            System.Collections.ArrayList theEvents = new System.Collections.ArrayList();

            for (int i = 0; i < _tracks.Length; i++)
            {
                if (_tracks[i] != null)
                {
                    Events[] EventCollection = new Events[AudioEngineGlobalSettings.TrackEvents];
                    EventCollection = _tracks[i].GetEvents(true);

                    for (int q = 0; q < AudioEngineGlobalSettings.TrackEvents; q++)
                    {
                        // So that non negative (i.e. unasigned) time codes are not added to the list
                        if (EventCollection[q] != null)
                        {
                            theEvents.Add(EventCollection[q]);
                        }
                    }
                }
            }

            // Sort the time codes in to assending order
            theEvents.Sort();

            // Copy the time codes back to Int64[]
            theEvents.CopyTo(OrderedEventCollection);

            return OrderedEventCollection;

        }





        /// <summary>
        /// Starts the precision timer.
        /// This timer is used to increase the acuracy of when events are triggered and removes
        /// the reliance on the .net timer class
        /// </summary>
        /// <param name="MillisecondTolerance">The number of Milliseconds to use for the tolerance, to decide if an overrun has occured</param>
        public void StartPrecisionTimer(int MillisecondTolerance)
        {

            // Set the HalfMillisecondTollerance
            AudioEngineGlobalSettings.PrecisionTimerHalfTolerance = AudioEngineGlobalSettings.PrecisionTimerTolerance / 2;



            // Create the thread object. This does not start the thread.
            PrecisionTimer ptObject = new PrecisionTimer();

            // Add the event handler for the delegate to the method in this class
            ptObject.TimeCodesToPlay += new TimeCodesPlayHandler(ptObject_TimeCodesToPlay);

            // Create the thread
            Thread ptThread = new Thread(ptObject.Start);

            // Start the worker thread.
            ptThread.Start();
            //ptObject.DoWork();
            Console.WriteLine("main thread: Starting worker thread...");

            // Loop until worker thread activates.
            while (!ptThread.IsAlive) ;

            // Raise the thread above normal priority - doesn't corelate to direct 
            // increase in CPU but will schedule thread in front of normal priority threads
            ptThread.Priority = ThreadPriority.AboveNormal;

            // Put the main thread to sleep for 1 millisecond to
            // allow the worker thread to do some work:
            Thread.Sleep(150);

            // Request that the worker thread stop itself:
            ptObject.Stop();

            // Use the Join method to block the current thread 
            // until the object's thread terminates.
            ptThread.Join();
            Console.WriteLine("main thread: Worker thread has terminated.");
        }

        void ptObject_TimeCodesToPlay(object sender, TimeCodesPlayArgs e)
        {
            Console.WriteLine(e.FromTimeCode.ToString() + " " + e.ToTimeCode.ToString());

            for (Int64 i = e.FromTimeCode; i < e.ToTimeCode; i++)
            {
                PlayEvents(i);
            }
        }


    }
}
