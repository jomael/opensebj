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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for frmBeatPad.
	/// </summary>
	/// 
	
	public class frmBeatPad : System.Windows.Forms.Form
	{

		int sampleMenu = 0;

		private System.Windows.Forms.Button[] cmbSample;

		private int[] cmbSampleX = new int[16] {9,73,139,201,9,73,139,201,9,73,139,201,9,73,139,201};
		private int[] cmbSampleY = new int[16] {7,7,7,7,74,74,74,74,141,141,141,141,208,208,208,208};
		

		private System.Windows.Forms.VScrollBar beatPadScroll;
		private System.Windows.Forms.Panel panelPopUp;
		private System.Windows.Forms.Button cmbDone;
		private System.Windows.Forms.Button cmbSettings;
		private System.Windows.Forms.Button cmbSetKey;
        private Button cmbSplice;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmBeatPad()
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
            // If the MDI Child is full screen when disposing it causes an issue where the
            // MDI window flickers, as if it doesn't know when to go full screen or normal.
            // Any way, seting this window to the Normal window state before disposing
            // the form, allows it to disposes correctly and all other windows to continue 
            // on as normal.
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBeatPad));
            this.beatPadScroll = new System.Windows.Forms.VScrollBar();
            this.panelPopUp = new System.Windows.Forms.Panel();
            this.cmbSplice = new System.Windows.Forms.Button();
            this.cmbSetKey = new System.Windows.Forms.Button();
            this.cmbDone = new System.Windows.Forms.Button();
            this.cmbSettings = new System.Windows.Forms.Button();
            this.panelPopUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // beatPadScroll
            // 
            this.beatPadScroll.Location = new System.Drawing.Point(264, 0);
            this.beatPadScroll.Maximum = 69;
            this.beatPadScroll.Name = "beatPadScroll";
            this.beatPadScroll.Size = new System.Drawing.Size(16, 256);
            this.beatPadScroll.TabIndex = 16;
            this.beatPadScroll.ValueChanged += new System.EventHandler(this.beatPadScroll_ValueChanged);
            // 
            // panelPopUp
            // 
            this.panelPopUp.Controls.Add(this.cmbSplice);
            this.panelPopUp.Controls.Add(this.cmbSetKey);
            this.panelPopUp.Controls.Add(this.cmbDone);
            this.panelPopUp.Controls.Add(this.cmbSettings);
            this.panelPopUp.Location = new System.Drawing.Point(97, 37);
            this.panelPopUp.Name = "panelPopUp";
            this.panelPopUp.Size = new System.Drawing.Size(56, 95);
            this.panelPopUp.TabIndex = 133;
            this.panelPopUp.Visible = false;
            // 
            // cmbSplice
            // 
            this.cmbSplice.Location = new System.Drawing.Point(0, 48);
            this.cmbSplice.Name = "cmbSplice";
            this.cmbSplice.Size = new System.Drawing.Size(56, 24);
            this.cmbSplice.TabIndex = 38;
            this.cmbSplice.Text = "Splice";
            this.cmbSplice.UseVisualStyleBackColor = false;
            this.cmbSplice.Click += new System.EventHandler(this.cmbSplice_Click);
            // 
            // cmbSetKey
            // 
            this.cmbSetKey.Location = new System.Drawing.Point(0, 24);
            this.cmbSetKey.Name = "cmbSetKey";
            this.cmbSetKey.Size = new System.Drawing.Size(56, 24);
            this.cmbSetKey.TabIndex = 37;
            this.cmbSetKey.Text = "Set Key";
            this.cmbSetKey.UseVisualStyleBackColor = false;
            this.cmbSetKey.Click += new System.EventHandler(this.cmbSetKey_Click);
            // 
            // cmbDone
            // 
            this.cmbDone.Location = new System.Drawing.Point(0, 72);
            this.cmbDone.Name = "cmbDone";
            this.cmbDone.Size = new System.Drawing.Size(56, 24);
            this.cmbDone.TabIndex = 36;
            this.cmbDone.Text = "Done";
            this.cmbDone.UseVisualStyleBackColor = false;
            this.cmbDone.Click += new System.EventHandler(this.cmbDone_Click);
            // 
            // cmbSettings
            // 
            this.cmbSettings.Location = new System.Drawing.Point(0, 0);
            this.cmbSettings.Name = "cmbSettings";
            this.cmbSettings.Size = new System.Drawing.Size(56, 24);
            this.cmbSettings.TabIndex = 35;
            this.cmbSettings.Text = "Settings";
            this.cmbSettings.UseVisualStyleBackColor = false;
            this.cmbSettings.Click += new System.EventHandler(this.cmbSettings_Click);
            // 
            // frmBeatPad
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(280, 256);
            this.Controls.Add(this.panelPopUp);
            this.Controls.Add(this.beatPadScroll);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmBeatPad";
            this.Text = "Beat Pad";
            this.Click += new System.EventHandler(this.frmBeatPad_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBeatPad_KeyDown);
            this.Load += new System.EventHandler(this.frmBeatPad_Load);
            this.panelPopUp.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void cmbSample_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < 16; i++)
			{
				if (sender.Equals(cmbSample[i]))
				{
					dsInterface.play(i + beatPadScroll.Value * 4);
				}

			}
		}

		private void beatPadScroll_ValueChanged(object sender, System.EventArgs e)
		{
			clearPopUp();

			int tempValue = 0 ;
			for (int i = 0; i < 16; i++)
			{
				tempValue = i + beatPadScroll.Value * 4 + 1;
				cmbSample[i].Text = tempValue.ToString();
			}
		}
		

		private void cmbSample_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			for (int i = 0; i < 16; i++)
			{
			
				if (sender.Equals(cmbSample[i]))
				{
					sampleMenu = i;
					
					if (e.Button == System.Windows.Forms.MouseButtons.Right)
					{
						panelPopUp.Visible = false;
						panelPopUp.Left = e.X + cmbSample[i].Left - 2;
						panelPopUp.Top = e.Y + cmbSample[i].Top - 2;
						panelPopUp.BringToFront();
						panelPopUp.Visible = true;
						sampleMenu = i + beatPadScroll.Value * 4;
					}
					else
					{
						clearPopUp();
					}
                    
				}
			}
		}

		private void clearPopUp()
		{
			if(panelPopUp.Visible == true)
			{
				panelPopUp.Visible = false;
			}
		}

		private void frmBeatPad_Load(object sender, System.EventArgs e)
		{
		
			cmbSample = new System.Windows.Forms.Button[16]; 

			int tempInt = 0;
			//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmBeatPad));
			for (int i = 0; i < 16; i++)
			{
				this.cmbSample[i] = new System.Windows.Forms.Button();
				//this.cmbSample[i].BackColor = System.Drawing.Color.Black;
				//this.cmbSample[i].BackgroundImage = System.Drawing.Image.FromFile("wood100x100.jpg");
				//this.cmbSample[i].BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbSetKey.BackgroundImage")));
				this.cmbSample[i].Location = new System.Drawing.Point(cmbSampleX[i], cmbSampleY[i]);
				this.cmbSample[i].Name = "cmbSample" + i.ToString();
				this.cmbSample[i].Size = new System.Drawing.Size(44, 39);
				this.cmbSample[i].TabIndex = i;
				tempInt = i + 1;
				this.cmbSample[i].Text = tempInt.ToString();
				this.cmbSample[i].Click += new System.EventHandler(this.cmbSample_Click);
				this.cmbSample[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbSample_MouseDown);
				// Not needed with key preview switched on for the form
				// this.cmbSample[i].KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBeatPad_KeyDown);
			}


			for (int i = 0; i < 16; i++)
			{
				this.Controls.Add(this.cmbSample[i]);
			}

		}

		private void frmBeatPad_Click(object sender, System.EventArgs e)
		{
			clearPopUp();
		}

		private void cmbSettings_Click(object sender, System.EventArgs e)
		{
            int _sample = sampleMenu;

            if (globalSettings.osj.sampleLoaded[_sample] == true)
            {
                // Open the properties window - if the sample has been loaded.
                frmProperties _properties = new frmProperties(_sample);
                _properties.MdiParent = globalSettings.MDIForm;
                _properties.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Sample not loaded");
            }

            // Clear the popup for the sub menu
            clearPopUp();    
		}

		private void cmbDone_Click(object sender, System.EventArgs e)
		{
			clearPopUp();
		}

		private void cmbSetKey_Click(object sender, System.EventArgs e)
		{
			frmKey keySet = new frmKey(sampleMenu);
			keySet.Visible = true;
			keySet.Activate();
	
			clearPopUp();
		}

		private void frmBeatPad_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//for (int i = 0; i < 255; i++)
			//{
			//	if (layOut.setKey[i] == e.KeyValue)
			//	{
			//		dsInterface.play(i);
			//	}
			//}
            //if (e.Modifiers == Keys.Control)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.Modifiers.ToString());
            //}
			dsInterface.playKey(e.KeyValue, e.Control);

		}

        private void cmbSplice_Click(object sender, EventArgs e)
        {
            //frmTape tape = new frmTape(sampleMenu);
            //tape.Visible = true;
            //tape.Activate();

            frmWaveScratch wavePlot = new frmWaveScratch(sampleMenu);
            wavePlot.Visible = true;
            wavePlot.Activate();

            clearPopUp();
        }
	}
}
