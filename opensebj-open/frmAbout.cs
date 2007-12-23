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



//Not required when using standard configuration directives
//#define DEBUG

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

using Microsoft.DirectX.DirectSound;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label lblAbout;
		private System.Windows.Forms.PictureBox ghettoEditionLogo;
		private System.Timers.Timer AboutTimer;

		private static Microsoft.DirectX.DirectSound.Device testCard;
		private static Microsoft.DirectX.DirectSound.SecondaryBuffer testSound;

		//path used to locate and load fire.wav
		string path;
        private Label lblVersion;
        private Button cmbDone;
        private ComboBox cmbDevices;

        private Guid[] directSoundDevices;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAbout(bool useTimer)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			if (useTimer == true)
			{
				AboutTimer.Enabled = true;
			}
			else
			{
				cmbDone.Visible = true;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.ghettoEditionLogo = new System.Windows.Forms.PictureBox();
            this.lblAbout = new System.Windows.Forms.Label();
            this.AboutTimer = new System.Timers.Timer();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cmbDone = new System.Windows.Forms.Button();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ghettoEditionLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AboutTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // ghettoEditionLogo
            // 
            this.ghettoEditionLogo.BackColor = System.Drawing.Color.Black;
            this.ghettoEditionLogo.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ghettoEditionLogo.ErrorImage = null;
            this.ghettoEditionLogo.Image = ((System.Drawing.Image)(resources.GetObject("ghettoEditionLogo.Image")));
            this.ghettoEditionLogo.InitialImage = null;
            this.ghettoEditionLogo.Location = new System.Drawing.Point(15, 18);
            this.ghettoEditionLogo.Name = "ghettoEditionLogo";
            this.ghettoEditionLogo.Size = new System.Drawing.Size(402, 280);
            this.ghettoEditionLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ghettoEditionLogo.TabIndex = 0;
            this.ghettoEditionLogo.TabStop = false;
            this.ghettoEditionLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ghettoEditionLogo_MouseDown);
            // 
            // lblAbout
            // 
            this.lblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbout.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbout.ForeColor = System.Drawing.Color.Black;
            this.lblAbout.Location = new System.Drawing.Point(7, 316);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(419, 56);
            this.lblAbout.TabIndex = 2;
            this.lblAbout.Text = resources.GetString("lblAbout.Text");
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // AboutTimer
            // 
            this.AboutTimer.Interval = 3000;
            this.AboutTimer.SynchronizingObject = this;
            this.AboutTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.AboutTimer_Elapsed);
            // 
            // lblVersion
            // 
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVersion.ForeColor = System.Drawing.Color.Green;
            this.lblVersion.Location = new System.Drawing.Point(7, 372);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(419, 48);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Checking Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            // 
            // cmbDone
            // 
            this.cmbDone.BackColor = System.Drawing.SystemColors.Control;
            this.cmbDone.Location = new System.Drawing.Point(342, 434);
            this.cmbDone.Name = "cmbDone";
            this.cmbDone.Size = new System.Drawing.Size(75, 23);
            this.cmbDone.TabIndex = 6;
            this.cmbDone.Text = "Done";
            this.cmbDone.UseVisualStyleBackColor = false;
            this.cmbDone.Click += new System.EventHandler(this.cmbDone_Click);
            // 
            // cmbDevices
            // 
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(10, 436);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(326, 21);
            this.cmbDevices.TabIndex = 7;
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(433, 469);
            this.ControlBox = false;
            this.Controls.Add(this.cmbDevices);
            this.Controls.Add(this.cmbDone);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.ghettoEditionLogo);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAbout";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ghettoEditionLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AboutTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmAbout_Load(object sender, System.EventArgs e)
		{
			// All this to get the file name where the home directory of the app
			
			int fileName = Application.ExecutablePath.Length - Application.ExecutablePath.LastIndexOf("\\") - 1;
			path = Application.ExecutablePath.Remove(Application.ExecutablePath.Length - fileName, fileName);
			//MessageBox.Show(path);

            globalSettings.path = path;


			// I got sick of waiting while developing ;-)
			#if (DEBUG)
			cmbDone.Visible = true;
			#endif 

            
			try
			{
				testCard = new Microsoft.DirectX.DirectSound.Device();
				testCard.SetCooperativeLevel(this, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);

				//Microsoft.DirectX.DirectSound.SecondaryBuffer[] aSound = new SecondaryBuffer[256];
				testSound = new Microsoft.DirectX.DirectSound.SecondaryBuffer(path + "fire.wav", testCard);
			}
			catch(Microsoft.DirectX.DirectSound.SoundException Exc)
			{
				MessageBox.Show("There has been an error while seting up your sound card; please review your Direct X settings. :: " + Exc.ErrorString.ToString());
			}

            lblVersion.Text = "Sending Request";
            string _webCheck = globalSettings.versionCheckAddress + globalSettings.releaseVersion;
            try
            {
                // Don't need to check if I am running the latest version while
                // developing it ;-)
                #if (!DEBUG)
                lblVersion.Text = httpFunctions.getVersion(_webCheck);
                #endif
            }
            catch
            {

            }

            // Audio device enumeration
            AudioDeviceEnumeration();
		
		}


		private void cmbDone_Click(object sender, System.EventArgs e)
		{
            // Gets the GUID of the selected sound device
            globalSettings.audioDevice = directSoundDevices[cmbDevices.SelectedIndex];
            //globalSettings.selectedAudioDevice = cmbDevices.SelectedIndex;

            globalSettings.aDC.selectedAudioDevice = cmbDevices.SelectedIndex;
            audioDeviceSave();

            if (dsInterface.aSoundCard == null)
            {
                // Sets up the DX Audio Interface for the program
                dsInterface.setupAudio(this);
            }
            else
            {
                dsInterface.changeAudioDevice();
            }



            // Close it all down
            testSound = null;
			testCard = null;
			AboutTimer = null;
			this.Close();
		}

		private void lblLink_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.evolvingsoftware.com");
			this.TopMost = false;
		}

		

		private void ghettoEditionLogo_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				if (testSound.Status.Playing == false)
				{
					testSound.Play(0, Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);
				}
				else
				{
					testSound.SetCurrentPosition(0);
					testSound.Play(0, Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);
				}
			}
			catch (Microsoft.DirectX.DirectSound.SoundException Exc)
			{
				MessageBox.Show("There has been an error while attempting to play a test sample; please review your Direct X settings and confirm the test sample is located with this executable. Direct X error message follows :: " + Exc.Message.ToString() + " "  + Exc.ErrorCode.ToString() + " " + Exc.ErrorString.ToString());
			}


			Graphics g;
			// Sets g to a graphics object representing the drawing surface of the
			// control or form g is a member of.
			g = ghettoEditionLogo.CreateGraphics();
			
			System.Drawing.SolidBrush shadow = new System.Drawing.SolidBrush(System.Drawing.Color.DarkGray);
			g.FillEllipse(shadow, new Rectangle((e.X + 1) - 5, (e.Y + 1) - 5 ,10, 10));
			
			System.Drawing.SolidBrush hole = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
			g.FillEllipse(hole, new Rectangle(e.X - 5, e.Y - 5,10, 10));


		}

		private void AboutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			cmbDone.Visible = true;
		}

        

        private void lblAbout_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com");
            this.TopMost = false;
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.evolvingsoftware.com");
            this.TopMost = false;
        }



        public void AudioDeviceEnumeration()
        {
            Microsoft.DirectX.DirectSound.DevicesCollection devices = new Microsoft.DirectX.DirectSound.DevicesCollection();
            
            directSoundDevices = new Guid[devices.Count];
            
            // Gets the Guid and populates the associated descritption for each sound device
            for (int i = 0; i < devices.Count; i++)
            {
                DeviceInformation devInfo = devices[i];
                cmbDevices.Items.Add(devInfo.Description);
                directSoundDevices[i] = devInfo.DriverGuid;
            }

            // Setup the selected audio device, by enumerated value
            globalSettings.aDC.selectedAudioDevice = 0;

            try
            {
                // Load the audio device from the last session
                audioDeviceLoad();

            }catch
            {
                // Create the settings file if it doesn't exist and save with the default
                audioDeviceSave();
            }

            // Sets to the default value of 0 (Should be Primary) or the previously selected value if it was different
            cmbDevices.SelectedIndex = globalSettings.aDC.selectedAudioDevice;
            
        }

        private void audioDeviceSave()
        {
            // Binary Serializes
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(globalSettings.path + "audioDevice.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, globalSettings.aDC);
            stream.Close();
        }

        private void audioDeviceLoad()
        {
            // Reserializes
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(globalSettings.path + "audioDevice.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            globalSettings.aDC = (audioDeviceConfig)formatter.Deserialize(stream);
            stream.Close();
        }

	}

}