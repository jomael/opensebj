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
using System.Data;

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
        private IContainer components;

        //private bool loaded = false;


		public frmOpenSebJ()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenSebJ));
            this.SuspendLayout();
            // 
            // frmOpenSebJ
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(91, 74);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmOpenSebJ";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenSebJ";
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
            Application.Run(new frmMainEditor());
		}

		private void OpenSebJ_Load(object sender, System.EventArgs e)
		{
				
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
				//menuItemOnTop.Checked = false;
			}
			else
			{
				this.TopMost = true;
				//menuItemOnTop.Checked = true;
			}

		}

		
		
	}
}
