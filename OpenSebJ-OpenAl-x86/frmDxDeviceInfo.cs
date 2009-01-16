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

using Microsoft.DirectX.DirectSound;

namespace OpenSebJ
{
	/// <summary>
	/// Summary description for frmDxDeviceInfo.
	/// </summary>
	public class frmDxDeviceInfo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtInfo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDxDeviceInfo()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDxDeviceInfo));
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtInfo.Location = new System.Drawing.Point(0, 0);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(292, 273);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.TabStop = false;
            this.txtInfo.TextChanged += new System.EventHandler(this.txtInfo_TextChanged);
            // 
            // frmDxDeviceInfo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.txtInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDxDeviceInfo";
            this.Text = "Direct X Device Info ::";
            this.Load += new System.EventHandler(this.frmDxDeviceInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmDxDeviceInfo_Load(object sender, System.EventArgs e)
		{
			Caps deviceCaps = new Caps();
			deviceCaps = dsInterface.getDeviceCaps();

			txtInfo.Text = "Total Sound Card Memmory :: " + deviceCaps.TotalHardwareMemoryBytes.ToString() + System.Environment.NewLine;
			txtInfo.Text = txtInfo.Text + "Play CPU Overhead Software Buffers :: " + deviceCaps.PlayCpuOverheadSoftwareBuffers.ToString() + System.Environment.NewLine;
			txtInfo.Text = txtInfo.Text + "Certified DirectX Sound Card :: " + deviceCaps.Certified.ToString() + System.Environment.NewLine;
            txtInfo.Text = txtInfo.Text + "Secondary Stereo Buffers :: " + deviceCaps.SecondaryStereo.ToString() + System.Environment.NewLine;
            txtInfo.Text = txtInfo.Text + "Continuous Rate :: " + deviceCaps.ContinuousRate.ToString() + System.Environment.NewLine;
            txtInfo.Text = txtInfo.Text + "Emulate Driver :: " + deviceCaps.EmulateDriver.ToString() + System.Environment.NewLine;
            txtInfo.Text = txtInfo.Text + "Max Hardware Mixing Buffers :: " + deviceCaps.MaxHardwareMixingAllBuffers.ToString() + System.Environment.NewLine;
        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
