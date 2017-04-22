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

                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。

                string destUrl = "http://www.qq.com";              //写到配置节点

                Byte[] pageData = MyWebClient.DownloadData(destUrl); //从指定网站下载数据

                string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句    

                //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句

                Response.Write(pageHtml);//输出获取的内容

                using (StreamWriter sw = new StreamWriter("G:\\ouput.html"))//将获取的内容写入文本
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

            string filename = @"c:\log.txt";

            Uri httpURL = new Uri(txtURL.Text);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换 
            httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换
            httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理 
            Stream respStream = httpResp.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8） 
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.Default);
            //StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            byteRead = respStreamReader.Read(cbuffer, 0, 256);

            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }

            respStream.Close();
            //txtHTML.Text = strBuff; 
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