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
    public class TimeCodesPlayArgs : System.EventArgs
    {
        private Int64 _FromTimeCode;

        public Int64 FromTimeCode
        {
            get { return _FromTimeCode; }
            set { _FromTimeCode = value; }
        }


        private Int64 _ToTimeCode;

        public Int64 ToTimeCode
        {
            get { return _ToTimeCode; }
            set { _ToTimeCode = value; }
        }

	
    }
}
