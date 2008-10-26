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

using AudioEngine;

namespace SequencerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            AudioEngine.Sequencer sequencer = new AudioEngine.Sequencer();
            
            bool quit = false;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();


            while (quit == false){
                
                Console.WriteLine("Press 1 to Add a new Track");
                Console.WriteLine("Press 2 to Remove a Track");
                Console.WriteLine("Press 3 to Add an Event to a Track");
                Console.WriteLine("Press 4 to List all Events for a Track");
                Console.WriteLine("Press 5 randomly add events to a Track");
                Console.WriteLine("Press 6 to get a list of the first 100 events in order");
                Console.WriteLine("Press 7 to test sequencer speed");
                Console.WriteLine("Press Q to Exit test");

                
                cki = Console.ReadKey();
                Console.WriteLine("");
                if (cki.Key == ConsoleKey.Q)
                {
                        quit = true;
                }
                if (cki.Key == ConsoleKey.D1)
                {
                    Console.WriteLine("Enter track name to add");
                    Console.WriteLine("Track number " + sequencer.AddTrack(Console.ReadLine()).ToString() + " was added");
                }
                if (cki.Key == ConsoleKey.D2)
                {
                    Console.WriteLine("Enter a track number to remove");
                    int track = int.Parse(Console.ReadLine());
                    Console.WriteLine("Track " + sequencer.GetTrackName(track) + " was removed: " + sequencer.RemoveTrack(track).ToString());
                }
                if (cki.Key == ConsoleKey.D3)
                {
                    Console.WriteLine("Enter a track number to add an event to");
                    int trackNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter a time code for the event");
                    Int64 eventTimeCode = Int64.Parse(Console.ReadLine());
                    bool eventAdded = sequencer.AddEvent(trackNumber, eventTimeCode);
                    Console.WriteLine("Event added:" + eventAdded.ToString() + " to " + sequencer.GetTrackName(trackNumber) + " at time code " + eventTimeCode.ToString());
                }
                if (cki.Key == ConsoleKey.D4)
                {
                    Console.WriteLine("Enter a track number to list all events for");
                    int trackNumber = int.Parse(Console.ReadLine());
                    
                    // Get the time codes
                    Int64[] timeCodes = new Int64[AudioEngineGlobalSettings.TrackEvents];
                    timeCodes = sequencer.GetTrackEventTimeCodes(trackNumber,true);

                    // Print the time codes
                    if (timeCodes != null)
                    {
                        for (int i = 0; i < timeCodes.GetLength(0); i++)
                        {
                            if (timeCodes[i] >= 0)
                            {
                                Console.WriteLine(timeCodes[i]);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please pick a valid track");
                    }
                }
                if (cki.Key == ConsoleKey.D5)
                {
                    Console.WriteLine("Enter a track number to add random events to");
                    int trackNumber = int.Parse(Console.ReadLine());
                    
                    if (sequencer.TrackExists(trackNumber))
                    {

                        Random ran = new Random();

                        for (int i = 0; i < 5000; i++)
                        {
                            sequencer.AddEvent(trackNumber, ran.Next());

                        }

                        Console.WriteLine("Events added");
                    }
                }
                if (cki.Key == ConsoleKey.D6)
                {
                    Int64[] timeCodes = new Int64[AudioEngineGlobalSettings.TrackEvents];
                    timeCodes = sequencer.GetAllTimeCodesOrdered();

                    // Print the time codes
                    if (timeCodes != null)
                    {
                        //for (int i = 0; i < timeCodes.GetLength(0); i++)
                        for (int i = 0; i < 100; i++)
                        {
                            if (timeCodes[i] >= 0)
                            {
                                Console.WriteLine(timeCodes[i]);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please pick a valid track");
                    }
                }
                if (cki.Key == ConsoleKey.D7)
                {
                    sequencer.Play();

                    //Int64 timeCode = sequencer.GetOrderedEvents();
                    //Console.WriteLine(timeCode);
                }


                Console.WriteLine("");
            }
            
        }
            
            
        
    }
}

