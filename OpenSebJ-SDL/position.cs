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
    public class position
    {
        // Used to track the location of each of the 'plays'
        private int _tickPosition;

        // Used to track which sample belongs to the position
        private int _sample;

        public void newPosition(int sample, int tickPosition)
        {
            _sample = sample;
            _tickPosition = tickPosition;
        }

        public int getSample()
        {
            return _sample;
        }

        public int getTick()
        {
            return _tickPosition;
        }

        public void setTick(int tickPosition)
        {
            _tickPosition = tickPosition;      
        }
    }
}
