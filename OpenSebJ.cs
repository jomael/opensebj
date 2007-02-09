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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

//using Microsoft.DirectX.AudioVideoPlayback;
using System.Diagnostics;

using System.Threading;




namespace OpenSebJ
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmOpenSebJ : System.Windows.Forms.Form
	{

		//dsInterface ds;
        //System.Threading.Thread t;
		private System.Windows.Forms.Button cmb3;
		private System.Windows.Forms.MainMenu OpenSebJMainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItemOnTop;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.Button cmbBeatPad;
        private System.Windows.Forms.MenuItem menuItemDXSound;
        private System.Windows.Forms.Button cmbScratch;
        private Button cmbBeatBox;
        private Button cmbRecord;
        private IContainer components;
        private Button cmbGraphics;

        //private bool loaded = false;


		public frmOpenSebJ()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenSebJ));
            this.cmb3 = new System.Windows.Forms.Button();
            this.cmbBeatPad = new System.Windows.Forms.Button();
            this.OpenSebJMainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemOnTop = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItemDXSound = new System.Windows.Forms.MenuItem();
            this.cmbScratch = new System.Windows.Forms.Button();
            this.cmbBeatBox = new System.Windows.Forms.Button();
            this.cmbRecord = new System.Windows.Forms.Button();
            this.cmbGraphics = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmb3
            // 
            this.cmb3.ForeColor = System.Drawing.Color.White;
            this.cmb3.Location = new System.Drawing.Point(12, 162);
            this.cmb3.Name = "cmb3";
            this.cmb3.Size = new System.Drawing.Size(64, 32);
            this.cmb3.TabIndex = 2;
            this.cmb3.Text = "3";
            this.cmb3.Click += new System.EventHandler(this.cmb3_Click);
            // 
            // cmbBeatPad
            // 
            this.cmbBeatPad.BackColor = System.Drawing.Color.Black;
            this.cmbBeatPad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbBeatPad.BackgroundImage")));
            this.cmbBeatPad.ForeColor = System.Drawing.Color.White;
            this.cmbBeatPad.Location = new System.Drawing.Point(3, 12);
            this.cmbBeatPad.Name = "cmbBeatPad";
            this.cmbBeatPad.Size = new System.Drawing.Size(104, 24);
            this.cmbBeatPad.TabIndex = 1;
            this.cmbBeatPad.Text = "Beat Pad";
            this.cmbBeatPad.UseVisualStyleBackColor = false;
            this.cmbBeatPad.Visible = false;
            this.cmbBeatPad.Click += new System.EventHandler(this.cmbBeatPad_Click);
            // 
            // OpenSebJMainMenu
            // 
            this.OpenSebJMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSettings,
            this.menuItemExit});
            this.menuItem1.Text = "File";
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Index = 0;
            this.menuItemSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOnTop});
            this.menuItemSettings.Text = "Settings";
            // 
            // menuItemOnTop
            // 
            this.menuItemOnTop.Index = 0;
            this.menuItemOnTop.Text = "Always On Top";
            this.menuItemOnTop.Click += new System.EventHandler(this.menuItemOnTop_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 1;
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout,
            this.menuItemDXSound});
            this.menuItem2.Text = "Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Text = "About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // menuItemDXSound
            // 
            this.menuItemDXSound.Index = 1;
            this.menuItemDXSound.Text = "DX Sound";
            this.menuItemDXSound.Click += new System.EventHandler(this.menuItemDXSound_Click);
            // 
            // cmbScratch
            // 
            this.cmbScratch.BackColor = System.Drawing.Color.Black;
            this.cmbScratch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbScratch.BackgroundImage")));
            this.cmbScratch.ForeColor = System.Drawing.Color.White;
            this.cmbScratch.Location = new System.Drawing.Point(3, 42);
            this.cmbScratch.Name = "cmbScratch";
            this.cmbScratch.Size = new System.Drawing.Size(104, 24);
            this.cmbScratch.TabIndex = 3;
            this.cmbScratch.Text = "Scratch";
            this.cmbScratch.UseVisualStyleBackColor = false;
            this.cmbScratch.Click += new System.EventHandler(this.cmbScratch_Click);
            // 
            // cmbBeatBox
            // 
            this.cmbBeatBox.BackColor = System.Drawing.Color.Black;
            this.cmbBeatBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbBeatBox.BackgroundImage")));
            this.cmbBeatBox.ForeColor = System.Drawing.Color.White;
            this.cmbBeatBox.Location = new System.Drawing.Point(3, 72);
            this.cmbBeatBox.Name = "cmbBeatBox";
            this.cmbBeatBox.Size = new System.Drawing.Size(104, 24);
            this.cmbBeatBox.TabIndex = 4;
            this.cmbBeatBox.Text = "Beat Box";
            this.cmbBeatBox.UseVisualStyleBackColor = false;
            this.cmbBeatBox.Click += new System.EventHandler(this.cmbBeatBox_Click);
            // 
            // cmbRecord
            // 
            this.cmbRecord.BackColor = System.Drawing.Color.Black;
            this.cmbRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbRecord.BackgroundImage")));
            this.cmbRecord.ForeColor = System.Drawing.Color.White;
            this.cmbRecord.Location = new System.Drawing.Point(3, 102);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(104, 24);
            this.cmbRecord.TabIndex = 5;
            this.cmbRecord.Text = "Record";
            this.cmbRecord.UseVisualStyleBackColor = false;
            this.cmbRecord.Click += new System.EventHandler(this.cmbRecord_Click);
            // 
            // cmbGraphics
            // 
            this.cmbGraphics.BackColor = System.Drawing.Color.Black;
            this.cmbGraphics.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbGraphics.BackgroundImage")));
            this.cmbGraphics.ForeColor = System.Drawing.Color.White;
            this.cmbGraphics.Location = new System.Drawing.Point(3, 132);
            this.cmbGraphics.Name = "cmbGraphics";
            this.cmbGraphics.Size = new System.Drawing.Size(104, 24);
            this.cmbGraphics.TabIndex = 6;
            this.cmbGraphics.Text = "Video";
            this.cmbGraphics.UseVisualStyleBackColor = false;
            this.cmbGraphics.Click += new System.EventHandler(this.cmbGraphics_Click);
            // 
            // frmOpenSebJ
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(123, 227);
            this.Controls.Add(this.cmbGraphics);
            this.Controls.Add(this.cmbRecord);
            this.Controls.Add(this.cmbBeatBox);
            this.Controls.Add(this.cmbScratch);
            this.Controls.Add(this.cmb3);
            this.Controls.Add(this.cmbBeatPad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.OpenSebJMainMenu;
            this.Name = "frmOpenSebJ";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenSebJ";
            this.Activated += new System.EventHandler(this.OpenSebJ_Activated);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenSebJ_MouseDown);
            this.Load += new System.EventHandler(this.OpenSebJ_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
		    //Application.Run(new frmOpenSebJ());
            Application.Run(new frmMainEditor());
		}

		private void OpenSebJ_Load(object sender, System.EventArgs e)
		{
			
			//Microsoft.DirectX.AudioVideoPlayback.Video introVid = new Microsoft.DirectX.AudioVideoPlayback.Video("c:\\Picture 015.avi");
			//introVid.Owner = this;
			//introVid.Size = new Size(300,200);
			//introVid.Play();

			//ds = new dsInterface();
			

            // TO DO: Need check the warning about loading directx during a form load method



		}

		private void OpenSebJ_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			/*
			int aNumber = e.Clicks;
			if (aNumber == 2)
			{
				Debug.WriteLine(OpenSebJ.ActiveForm.WindowState.ToString());
				
				if (OpenSebJ.ActiveForm.WindowState == System.Windows.Forms.FormWindowState.Maximized)
				{
					
					OpenSebJ.ActiveForm.WindowState = System.Windows.Forms.FormWindowState.Normal;
				}
				else
				{
					OpenSebJ.ActiveForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
				}
							
			}
			
			
			Debug.WriteLine(aNumber.ToString());
			
			*/
		}


		private void cmb2_Click(object sender, System.EventArgs e)
		{
			frmScratch scratch = new frmScratch();
			scratch.Visible = true;
			scratch.Activate();
			
			//ds.playRewind(255);

			//ds.playInThread(255);
			//cmb2.Text = ds.getSampleBytes(255).ToString();
		}

		private void cmb3_Click(object sender, System.EventArgs e)
		{

            frmMainEditor mainEditor = new frmMainEditor();
            mainEditor.Show();










            //frmWaveView waveView = new frmWaveView();
            //waveView.Visible = true;
            //waveView.Activate();



            //frmWavePlot wavePlot = new frmWavePlot(0);
            //wavePlot.Visible = true;
            //wavePlot.Activate();











			//System.Windows.Forms.MessageBox.Show(dsInterface.getLength(0).ToString());
			
			
			//dsInterface.reverseBufferAndPlay(0);
			
			
			
			//dsInterface.playRewind(0);
			
			
			//frmSamples Loader = new frmSamples();
			//Loader.Visible = true;
			//Loader.Activate();
			

			// Commented out scratching via thread
			/* 
			//Thread t = new Thread(new ThreadStart(ThreadProc));
			t = new Thread(new ThreadStart(ThreadProc));
			// Start ThreadProc.  On a uniprocessor, the thread does not get 
			// any processor time until the main thread yields.  Uncomment 
			// the Thread.Sleep that follows t.Start() to see the difference.
			t.Start();
			*/


            //frmScratch Scratch = new frmScratch();
            //Scratch.Visible = true;
            //Scratch.Activate();

            //frmRecord rec = new frmRecord();
            //rec.Visible = true;
            //rec.Activate();

			//frmSamples newMDIChild = new frmSamples();
			// Set the Parent Form of the Child window.
			//newMDIChild.MdiParent = this;
			// Display the new form.
			//newMDIChild.Show();
			
			//frmGraph sampleGraph = new frmGraph();
			//sampleGraph.Visible = true;
			//sampleGraph.Activate();


			

		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}


		private void menuItemOnTop_Click(object sender, System.EventArgs e)
		{
			if (this.TopMost == true)
			{
				this.TopMost = false;
				menuItemOnTop.Checked = false;
			}
			else
			{
				this.TopMost = true;
				menuItemOnTop.Checked = true;
			}

		}

		private void cmbLoadSamples_Click(object sender, System.EventArgs e)
		{
            //frmSamples Loader = new frmSamples();
            //Loader.Visible = true;
            //Loader.Activate();
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			frmAbout AboutThis = new frmAbout(false);
			AboutThis.Visible = true;
			AboutThis.Activate();
		}

		private void cmbBeatPad_Click(object sender, System.EventArgs e)
		{
			frmBeatPad beatPad = new frmBeatPad();
			beatPad.Visible = true;
			beatPad.Activate();		
		}

		private void menuItemDXSound_Click(object sender, System.EventArgs e)
		{
			frmDxDeviceInfo deviceInfo = new frmDxDeviceInfo();
			deviceInfo.Visible = true;
			deviceInfo.Activate();	
		}

		private void cmbLayDown_Click(object sender, System.EventArgs e)
		{
            //frmLaydown laydown = new frmLaydown();
            //laydown.Visible = true;
            //laydown.Activate();	
		}



	
			//System.Windows.Forms.Application.DoEvents();
	
		public static void ThreadProc() 
		{
			Application.Run(new frmScratch());
			
			//frmScratch Scratch = new frmScratch();
			//Scratch.Visible = true;
			//Scratch.Activate();
			//Thread.Sleep(1000);

		}

		private void cmbScratch_Click(object sender, System.EventArgs e)
		{
			frmScratch Scratch = new frmScratch();
			Scratch.Visible = true;
			Scratch.Activate();
		}

        //private void cmbTape_Click(object sender, System.EventArgs e)
        //{
        //    frmTape Tape = new frmTape(0);
        //    Tape.Visible = true;
        //    Tape.Activate();
        //}

        private void menuItem3_Click(object sender, EventArgs e)
        {

        }

        private void cmbBeatBox_Click(object sender, EventArgs e)
        {
            frmBeatBox beatBox = new frmBeatBox();
            beatBox.Visible = true;
            beatBox.Activate();
        }

        private void cmbRecord_Click(object sender, EventArgs e)
        {
            frmRecord rec = new frmRecord();
            rec.Visible = true;
            rec.Activate();
        }

        private void OpenSebJ_Activated(object sender, EventArgs e)
        {
            //if (loaded == false)
            //{
            //    // Only needs to fire once - even if it doesn't work ;-)
            //    loaded = true;
                
            //    frmAbout AboutThis = new frmAbout(true);
            //    AboutThis.Visible = true;
            //    AboutThis.Activate();

            //    // Sets up the DX Audio Interface for the program
            //    dsInterface.setupAudio(this);
            //}
        }

        private void cmbGraphics_Click(object sender, EventArgs e)
        {
            cmbGraphics.Enabled = false;

            frmVideoRender videoRender = new frmVideoRender();
            videoRender.Visible = true;
            videoRender.Activate();
        }
		
	}
}
