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

namespace OpenSebJ
{
    public partial class frmRecord : Form
    {
        private bufferRecord _rec;
        
        public frmRecord()
        {
            InitializeComponent();
        }

        private void cmbRecord_Click(object sender, EventArgs e)
        {
            if (cmbRecord.Text == "Record")
            {
                _rec.startRecording();
                cmbRecord.Text = "Stop";
            }
            else
            {
                _rec.StopRecording();
                cmbRecord.Text = "Record";
                
                // Fix the buttons so there aren't any concurency issue
                cmbRecord.Enabled = false;
                cmbSelectFile.Enabled = true;
            }


        }

        private void cmbSelectFile_Click(object sender, EventArgs e)
        {
            //string fileName = "";
            
            try
            {
                DialogResult save = new DialogResult();
                save = saveDialog.ShowDialog();
                if (save == DialogResult.OK)
                {
                    
                        lblFile.Text = saveDialog.FileName;

                        _rec = new bufferRecord();
                        _rec.createCaptureDevice();
                        _rec.setRecordFormat();
                        _rec.createRecordBuffer();
                        _rec.preRecord(saveDialog.FileName);

                        // Fix the buttons so there aren't any concurency issue
                        cmbRecord.Enabled = true;
                        cmbSelectFile.Enabled = false;
                }
                
            }
            catch (Exception excpt)
            {
                System.Windows.Forms.MessageBox.Show(excpt.ToString());
            }
        }

        private void frmRecord_Load(object sender, EventArgs e)
        {

        }
    
    } // End of class
}