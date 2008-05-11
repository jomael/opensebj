/* 
 * OpenSebJ
 * Copyright (C) 2007 Sebastian Gray - sebastiangray@gmail.com 
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml.Serialization;

namespace OpenSebJ
{
    public partial class frmMainEditor : Form
    {
        //private int childFormNumber = 0;
        private frmTrackEditor trackEditor;

        // Used to check if the form has previously loaded
        // Checked in the activate event to ensure that no direct sound locking issues occur
        private bool loaded = false;

        // Only want one video portal open at a time
        //frmVideoRender videoRender;


        public frmMainEditor()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            createNewComposition();
            
            
            //// Create a new instance of the child form.
            //Form childForm = new Form();
            //// Make it a child of this MDI form before showing it.
            //childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            //childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "OpenSebJ Files (*.osj)|*.osj|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                globalSettings.osjFileName = openFileDialog.FileName;

                // Uses the path preset in the global settings
                loadComposition();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDialogPop();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void beatPadToolStripButton_Click(object sender, EventArgs e)
        {
            normWindows();

            frmBeatPad beatPad = new frmBeatPad();
            beatPad.MdiParent = this;
            beatPad.Show();
        }

        private void trackToolStripLabel_Click(object sender, EventArgs e)
        {
            
        }

        private void frmMainEditor_Load(object sender, EventArgs e)
        {
            // Set the MDI form so it is accesiable from all locations
            globalSettings.MDIForm = this;

            // Open a blank track editor
            openTrackEditor();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            // If the filename is blank; it wasn't loaded and it hasn't been saved
            if ((globalSettings.osjFileName == "") || (globalSettings.osjFileName == globalSettings.path + "NewComposition.osj"))
            {
                saveDialogPop();
            }
            else
            {
                osjSave(globalSettings.osjFileName);
            }
        }

        private void saveDialogPop()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "OpenSebJ Files (*.osj)|*.osj|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                globalSettings.osjFileName = saveFileDialog.FileName;
                osjSave(globalSettings.osjFileName);
            }

        }

        private void osjSave(string fileName)
        {
            // Binary Serializes
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, globalSettings.osj);
            stream.Close();
        }


        private void openTrackEditor()
        {
            normWindows();

            if (globalSettings.trackEditorDefined == true)
            {
                trackEditor.Dispose();
            }
            trackEditor = new frmTrackEditor();
            trackEditor.MdiParent = this;
            trackEditor.Show();
            trackEditor.WindowState = FormWindowState.Maximized;
            globalSettings.trackEditorDefined = true;
        }

        private void trackToolStripButton_Click(object sender, EventArgs e)
        {
            openTrackEditor();
        }

        private void trackStripMenuItem_Click(object sender, EventArgs e)
        {
            openTrackEditor();
        }

        private void beatPadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normWindows();

            frmBeatPad beatPad = new frmBeatPad();
            beatPad.MdiParent = this;
            beatPad.Show();
        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normWindows();

            frmRecord record = new frmRecord();
            record.MdiParent = this;
            record.Show();
        }

        private void frmMainEditor_Activated(object sender, EventArgs e)
        {
            if (loaded == false)
            {
                // Only needs to fire once - even if it doesn't work ;-)
                loaded = true;

                frmAbout AboutThis = new frmAbout(true);
                AboutThis.Visible = true;
                AboutThis.Activate();

                // Sets up the DX Audio Interface for the program
                //dsInterface.setupAudio(this);
            }
        }

        //private void dXSoundToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    normWindows();

        //    frmDxDeviceInfo deviceInfo = new frmDxDeviceInfo();
        //    deviceInfo.MdiParent = this;
        //    deviceInfo.Show();
        //}

        public void createNewComposition()
        {
            // Ensure yes was clicked
            if(System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Do you want to create a new composition? Any unsaved changes will be lost.", "New Composition", System.Windows.Forms.MessageBoxButtons.YesNo))
            {
                // Load the new composition which is packaged OpenSebJ
                globalSettings.osjFileName = globalSettings.path + "NewComposition.osj";
                loadComposition();
            }
        }


        /// <summary>
        /// Load an existing composition; used for loading an empty composition when creating a new instance
        /// </summary>
        /// <param name="fileName"></param>
        public void loadComposition()
        {
            // Reserializes
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(globalSettings.osjFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            globalSettings.osj = (OpenSebJSettings)formatter.Deserialize(stream);
            stream.Close();


            // Reload the samples (from their origional file system location)
            //for (int i = 0; i < 262; i++)
            //{
            //    if (globalSettings.osj.sampleLoaded[i] == true)
            //    {
            //        dsInterface.loadSample(globalSettings.osj.sampleLocations[i], i);
            //    }
            //}


            // This loop extracts the sample from the memory streams where they were saved and loads them 1 by 1 through the
            // direct sound interface, in to memory.
            for (int i = 0; i < 262; i++)
            {
                if (globalSettings.osj.sampleLoaded[i] == true)
                {
                    // Setup the blank byte array
                    byte[] _bytes;

                    // Use the application data directory defined as the temporary location to write the wave sample to
                    string sampleName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sample.wav";

                    //System.Windows.Forms.MessageBox.Show(sampleName);

                    // Copy the memory stream to the temporary byte array
                    _bytes = globalSettings.osj.sample_MemoryStream[i].ToArray();

                    // Writes out the byte array to the predefined file location
                    System.IO.File.WriteAllBytes(sampleName, _bytes);

                    // Load the file using the standard dx interface
                    //dsInterface.loadSample(sampleName, i);
                    sdlInterface.loadSample(sampleName, i);
                }
            }

            //// Make sure that if the video window has already been opened the videos will still be loaded
            //if (globalSettings.videoPortal == true)
            //{
            //    // Pause any attempt to render
            //    globalSettings.videoPortal_eRenderReady = false;

            //    // Load the textures
            //    eRender.LoadTextures();

            //    // Load the videos
            //    eRender.LoadVideos();

            //    // Rendering OK now
            //    globalSettings.videoPortal_eRenderReady = true;

            //}

            // Need to close any existing, open track editor and open another one
            openTrackEditor();
        }

        //private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    toggleVideoPortal();
        //}

        private void recordToolStripButton_Click(object sender, EventArgs e)
        {
            normWindows();

            //frmRecord record = new frmRecord();
            //record.MdiParent = this;
            //record.Show();
        
        }

        //private void toggleVideoPortal()
        //{
        //    if (globalSettings.videoPortal == false)
        //    {

        //        DialogResult fullScreenDialog = new DialogResult();
        //        fullScreenDialog = System.Windows.Forms.MessageBox.Show("Do you want to launch the video portal in full screen?", "Full Screen?", MessageBoxButtons.YesNoCancel);

        //        switch (fullScreenDialog)
        //        {
        //            case DialogResult.Yes:
        //                globalSettings.videoPortal_FullScreen = true;
        //                break;

        //            case DialogResult.No:
        //                globalSettings.videoPortal_FullScreen = false;
        //                break;

        //            case DialogResult.Cancel:
        //                return;
        //                //break;
        //        }
                
                
        //        // Can't handle two video portals
        //        //globalSettings.videoPortal = true;
        //        videoToolStripMenuItem.Checked = true;

        //        // Mark that the video portal still shouldn't start rendering yet
        //        globalSettings.videoPortal_eRenderReady = false;

        //        videoRender = new frmVideoRender();
        //        videoRender.Visible = true;
        //        videoRender.Activate();

        //    }
        //    else
        //    {

        //        System.Windows.Forms.MessageBox.Show("Currently closing the Video Portal after it has been opened is not supported", "Closing the video portal not supported");

        //        //// Close video portals
        //        //globalSettings.videoPortal = false;
        //        //videoToolStripMenuItem.Checked = false;
        //        //globalSettings.videoPortal_eRenderReady = false;

        //        //videoRender.Close();
        //    }
        //}

        private void drumMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normWindows();

            frmBeatBox beatBox = new frmBeatBox();
            beatBox.MdiParent = this;
            beatBox.Show();
        }


        /// <summary>
        /// To help the MDI interface handle the windows opening and some redraw issue
        /// similar to the issue when closing an MDI child.
        /// </summary>
        private void normWindows()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                this.MdiChildren[i].WindowState = FormWindowState.Normal;
            }


        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com/helpContents.html");
            this.TopMost = false;
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com/helpIndex.html");
            this.TopMost = false;
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com/helpSearch.html");
            this.TopMost = false;
        }

        private void findAudioSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com/sample_search.html");
            this.TopMost = false;
        }

        private void findAudioSamplesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com/sample_search.html");
            this.TopMost = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout AboutThis = new frmAbout(true);
            AboutThis.Visible = true;
            AboutThis.Activate();
        }

        private void oSJSaveExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaveExplorer saveExplorer = new frmSaveExplorer();
            saveExplorer.Visible = true;
            saveExplorer.Activate();
        }

        private void frmMainEditor_KeyDown(object sender, KeyEventArgs e)
        {
            dsInterface.playKey(e.KeyValue, e.Control);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the filename is blank; it wasn't loaded and it hasn't been saved
            if ((globalSettings.osjFileName == "") || (globalSettings.osjFileName == globalSettings.path + "NewComposition.osj"))
            {
                saveDialogPop();
            }
            else
            {
                osjSave(globalSettings.osjFileName);
            }
        }

    }
}
