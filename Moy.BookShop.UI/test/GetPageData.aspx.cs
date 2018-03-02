using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace Moy.BookShop.UI.test
{
    public partial class GetPageData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void GetData()
        {
            try
            {

                WebClient MyWebClient = new WebClient();

                MyWebClient.Credentials = CredentialCache.DefaultCredentials;

                string destUrl = "http://www.qq.com";

                Byte[] pageData = MyWebClient.DownloadData(destUrl);

                string pageHtml = Encoding.Default.GetString(pageData);    

                //string pageHtml = Encoding.UTF8.GetString(pageData);

                Response.Write(pageHtml);

                using (StreamWriter sw = new StreamWriter("G:\\ouput.html"))
                {
                    sw.Write(pageHtml);
                }

                Response.Write("写入成功");
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
            }           

        }

        private void WriteStream()
        {
            HttpWebRequest httpReq;
            HttpWebResponse httpResp;

            string strBuff = "";
            char[] cbuffer = new char[256];
            int byteRead = 0;

            //string filename = @"c:\log.txt";

            Uri httpURL = new Uri(txtURL.Text);
           
            httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
           
            httpResp = (HttpWebResponse)httpReq.GetResponse();
                       
            Stream respStream = httpResp.GetResponseStream();
            
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.Default);
            
            byteRead = respStreamReader.Read(cbuffer, 0, 256);

            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }

            respStream.Close();

            Response.Write(strBuff);
            
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            GetData();
        }

        protected void btnGo2_Click(object sender, EventArgs e)
        {
            WriteStream();
        }


    }
}