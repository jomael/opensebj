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
	/// Summary description for frmKey.
	/// </summary>
	public class frmKey : System.Windows.Forms.Form
	{
		private int _sample;
		//private char _keyChar;

		private System.Windows.Forms.Label lblSample;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.Button cmbSetKey;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmKey(int sample)
		{
			_sample = sample;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKey));
            this.lblSample = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.cmbSetKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSample
            // 
            this.lblSample.Location = new System.Drawing.Point(0, 10);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(72, 16);
            this.lblSample.TabIndex = 0;
            this.lblSample.Text = "Sample :";
            this.lblSample.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtKey
            // 
            this.txtKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey.Location = new System.Drawing.Point(89, 7);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(39, 20);
            this.txtKey.TabIndex = 1;
            // 
            // cmbSetKey
            // 
            this.cmbSetKey.BackColor = System.Drawing.SystemColors.Control;
            this.cmbSetKey.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmbSetKey.Location = new System.Drawing.Point(134, 4);
            this.cmbSetKey.Name = "cmbSetKey";
            this.cmbSetKey.Size = new System.Drawing.Size(56, 24);
            this.cmbSetKey.TabIndex = 38;
            this.cmbSetKey.Text = "Set Key";
            this.cmbSetKey.UseVisualStyleBackColor = false;
            this.cmbSetKey.Click += new System.EventHandler(this.cmbSetKey_Click);
            // 
            // frmKey
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(202, 34);
            this.Controls.Add(this.cmbSetKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lblSample);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmKey";
            this.Text = "Assign Key";
            this.Load += new System.EventHandler(this.frmKey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmKey_Load(object sender, System.EventArgs e)
		{
            if (!char.IsWhiteSpace((char)globalSettings.osj.sampleSettings_KeyCode[_sample]))
			{
                //TODO: Cleanup - Old form and settings type
				//txtKey.Text = layOut.setKey[_sample].ToString();
                txtKey.Text = globalSettings.osj.sampleSettings_KeyCode[_sample].ToString();
			}
			
			int offSetSample = _sample + 1;
			lblSample.Text = "Sample " + offSetSample.ToString() + " : ";
		}

		private void cmbSetKey_Click(object sender, System.EventArgs e)
		{
            try
			{
                //TODO: Cleanup -  Old form and settings type
				//layOut.setKey[_sample] = txtKey.Text[0];

                globalSettings.osj.sampleSettings_KeyCode[_sample] = txtKey.Text[0];
                this.Close();
			}
			catch{}
		}
	}
}
