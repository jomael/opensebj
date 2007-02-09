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
    public static class globalSettings
    {
        // Storing the Global Variables here for convenience.
        public static string releaseVersion = "0.2";
        public static string versionCheckAddress = "http://www.evolvingsoftware.com/version.php?version=";

        // Used for the lay down window. Needs to be available
        // once the screen is closed..
        public static int inActionPlays = 0;

        // Reload Plays
        public static int reloadPlays = 0;

        // The position of the loop marker
        public static int loopMarker = 400;

        // Flag for key position recording on the lay down window
        public static bool keyRecord = false;

        // Save the application path for use with default or packaged media
        public static string path = "";

        // Keeps track of the video view
        public static bool videoPortal = false;

        // Used to determine if the window should be made full screen
        public static bool videoPortal_FullScreen = false;

        // Used to ensure that the setup of the video portal has completed
        public static bool videoPortal_eRenderReady = false;

        // To allow only one track editor window
        public static bool trackEditorDefined = false;

        // Static variable to define if the current project already has a save location somewhere.
        // i.e. if it's been loaded from a file or if it has already been saved.
        public static string osjFileName = "";

        // All variables that are Serializable, stored in the osj instance of OpenSebJSettings
        public static OpenSebJSettings osj = new OpenSebJSettings();

        // Reference to the MDIForm - reference used to allow forms created out side of the main editor
        // form to still be docked in the MDI container
        public static System.Windows.Forms.Form MDIForm = null;

    }
}
