using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;
using System.Configuration;

namespace ConvertVideoServer
{
    public class ConverHanadler
    {
        public static ConverHanadler handler = null;

        static ConverHanadler()
        {
            handler = new ConverHanadler();
        }

        public void Start()
        {
            Thread th = new Thread(ProcessStart);
            th.IsBackground = true;
            th.Start();            
        }

        private void ProcessStart()
        {
            while (true)
            {
                //todo System.Data.DataTable dt= sqlHelper.Query("select top 1 * from video where Status=2");
                System.Data.DataTable dt = new System.Data.DataTable();
                if (dt.Rows.Count <= 0)
                {
                    Thread.Sleep(5000);
                    continue;
                }
                var id = Convert.ToInt32(dt.Rows[0]["Id"]);
                var fileExt = dt.Rows[0]["fileExt"].ToString();

                BeginConvert(id, fileExt);

            }
        }

        private void BeginConvert(int id, string fileExt)
        {
            var path = ConfigurationManager.AppSettings["path"];
            var srcFileName = path + id + fileExt;
            var destFile = path + id + ".flv";

            #region Myregion
            //Process p = new Process();//创建并启动一个新进程

            //p.StartInfo.FileName = HttpContext.Current.Server.MapPath("ffmpeg.exe");
            //p.StartInfo.UseShellExecute = false;

            //string srcFileName = HttpContext.Current.Server.MapPath("/Video/aa.avi");
            //if (isPic)
            //{
            //    string destFile = HttpContext.Current.Server.MapPath("/Video/1.jpg");
            //    p.StartInfo.Arguments = "-i " + srcFileName + " -y -f image2  -ss 53 -t 0.001 -s  600x500 " + destFile;   
            //}
            //else
            //{
            //    string destFileName = HttpContext.Current.Server.MapPath("/Video/a.flv");
            //    p.StartInfo.Arguments = "-i " + srcFileName + " -y -ab 56 -ar 22050 -b 800 -r 29.97 -s 420x340 " + destFileName;  
            //}
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            //p.Start();
            //p.BeginErrorReadLine();//开始异步读取
            //p.WaitForExit();//阻塞等待进程结束
            //p.Close();//关闭进程
            //p.Dispose();//释放资源
            #endregion
       
            File.Delete(srcFileName);
            File.Delete(destFile);
        }

    }
}