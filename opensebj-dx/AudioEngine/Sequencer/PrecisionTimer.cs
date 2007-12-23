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
using System.Diagnostics;
using System.Threading;

namespace AudioEngine
{
    // The delegate to be used, to pass through time codes to be played
    public delegate void TimeCodesPlayHandler(object sender, TimeCodesPlayArgs e);

    public class PrecisionTimer
    {
        // Declare the timecodeplay event for the delegate
        public event TimeCodesPlayHandler TimeCodesToPlay;

        // Variable can be updated via different threads, as such volatile should be used in declaration 
        private volatile bool Playing = false;

        Stopwatch _stopWatch = new Stopwatch();

        // Used to calculate the inital offset, when the play event is triggered
        static System.DateTime _playTimeOffset;

        public void Start()
        {
            _stopWatch.Start();
            _playTimeOffset = DateTime.Now;

            Playing = true;

            Advance();
        }

        public Int64 GetElapsedMilliseconds()
        {
            return _stopWatch.ElapsedMilliseconds;
        }

        public TimeSpan GetElapsedTime()
        {
            return DateTime.Now.Subtract(_playTimeOffset);
        }

        //public void Reset()
        //{
        //    _stopWatch.Reset();
        //}

        public void Stop()
        {
            Playing = false;
            _stopWatch.Stop();
        }

        // Called from Start - This is the main loop
        public void Advance()
        {
            Int64 IncToleranceMilliseconds = 0;

            Int64 ElapsedMilliseconds =0;

            Int64 MissedMilliseconds = 0;

            Int64 LastPositionPlayedTo = 0;

            while (Playing)
            {
                // How many Milliseconds have past since the composition started playing
                ElapsedMilliseconds = GetElapsedMilliseconds();

                // Check that more milliseconds have elapsed than the timecode that was
                // played to last time (this ensures that at least 1 millisecond has actually passed
                // since this was called - only really relevant when the GlobalSettings.Tolerance set to 0)
                if (ElapsedMilliseconds > LastPositionPlayedTo)
                {
                   

                    //Console.Write(GetElapsedTime().ToString() + "    ");

                    //Console.WriteLine(GetElapsedMilliseconds().ToString());
                    
                    // at least 1 millisecond should have passed and we want to check it was only
                    // 1 - if not how many




                    // IncToleranceMilliseconds is used to check if qn overrun has occured. Rather than trying 
                    // to check every millisecond we check in blocks based on the tollerance and then
                    // play any samples from the last position up to the tollerance amount
                    IncToleranceMilliseconds = LastPositionPlayedTo + AudioEngineGlobalSettings.PrecisionTimerTolerance;

                    // Identifying if there has been an overrun. This occurs when more seconds have elapsed
                    // than the last play position + the bolck of tolernce time.
                    if (ElapsedMilliseconds < IncToleranceMilliseconds)
                    {
                        // No Over run - play required samples from the LastPositionPlayedTo up to ElapsedMilliseconds
                        OnTimeCodePlay(LastPositionPlayedTo, ElapsedMilliseconds);
                        
                        // As we have now played samples up to the elapsedmilliseconds we set the LastPositionPlayedTo
                        // to the latest amount played.
                        LastPositionPlayedTo = ElapsedMilliseconds;

                        // No overrun occured so the thread can sleep for half of the tolerance time
                        Thread.Sleep(AudioEngineGlobalSettings.PrecisionTimerHalfTolerance);
                    }
                    else
                    {
                        // An overrun has occured - play required samples from the LasPositionPlayedTo up to ElapsedMilliseconds
                        MissedMilliseconds = ElapsedMilliseconds - LastPositionPlayedTo - AudioEngineGlobalSettings.PrecisionTimerTolerance;
                        Console.WriteLine("overrun " + MissedMilliseconds.ToString());


                        OnTimeCodePlay(LastPositionPlayedTo, ElapsedMilliseconds);

                        LastPositionPlayedTo = ElapsedMilliseconds;

                        // Since an Overrun did occur the thread should not sleep as we need to catch up
                    }

                    
                }
                
            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }


        protected virtual void OnTimeCodePlay(Int64 FromTimeCode, Int64 ToTimeCode)
        {
            if (TimeCodesToPlay != null)
            {
                // Set the args for the delegate event
                TimeCodesPlayArgs tcpa = new TimeCodesPlayArgs();
                tcpa.FromTimeCode = FromTimeCode;
                tcpa.ToTimeCode = ToTimeCode;

                // Play with the relvant time codes
                TimeCodesToPlay(this, tcpa);
            }

        }


    }// PrecisionTimer

}//AudioEngine
