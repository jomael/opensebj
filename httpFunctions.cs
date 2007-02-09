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
using System.Data;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace OpenSebJ
{
    public class httpFunctions
    {

        public static string getVersion(string httpReq)
        {

            string _httpRequest = httpReq;

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(_httpRequest);

            // Off the connection keep alive
            HttpWReq.KeepAlive = false;

            try
            {

                HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

                System.IO.Stream call = HttpWResp.GetResponseStream();

                Byte[] data = new Byte[1024];
                string strTemp = "";
                StringBuilder strTotal = new StringBuilder();
                ASCIIEncoding textEncode = new ASCIIEncoding();
                int contInt = 0;

                bool cont = true;
                while (cont == true)
                {
                    contInt = call.Read(data, 0, 1);
                    if (contInt <= 0)
                    {
                        cont = false;
                    }
                    else
                    {
                        strTemp = textEncode.GetString(data, 0, 1);
                        strTotal.Append(strTemp);
                    }
                }

                
                // Once the httpRequest is closed the strTotal wasn't acessiable?
                string _tempResult = strTotal.ToString();

                HttpWResp.Close();

                return _tempResult;

            }
            catch //(Exception excp)
            {
                return "Unable to check version, please check your internect connection and firewall settings";
            }
        }

    }
}
