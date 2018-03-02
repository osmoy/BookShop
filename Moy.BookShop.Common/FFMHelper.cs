using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Moy.BookShop.Common
{
    public class FFMHelper
    {

        public static bool ConvertVideo(bool isPic = false)
        {
            try
            {
                //创建并启动一个新进程
                Process p = new Process();

                p.StartInfo.FileName = HttpContext.Current.Server.MapPath("ffmpeg.exe");
                p.StartInfo.UseShellExecute = false;
               

                string srcFileName = HttpContext.Current.Server.MapPath("/Video/aa.avi");
                if (isPic)
                {
                    string destFile = HttpContext.Current.Server.MapPath("/Video/1.jpg");
                    p.StartInfo.Arguments = "-i " + srcFileName + " -y -f image2  -ss 53 -t 0.001 -s  600x500 " + destFile;    //执行参数
                }
                else
                {
                    string destFileName = HttpContext.Current.Server.MapPath("/Video/a.flv");
                    p.StartInfo.Arguments = "-i " + srcFileName + " -y -ab 56 -ar 22050 -b 800 -r 29.97 -s 420x340 " + destFileName;    //执行参数
                }
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                p.Start();
                p.BeginErrorReadLine();//开始异步读取
                p.WaitForExit();//阻塞等待进程结束
                p.Close();//关闭进程
                p.Dispose();//释放资源
                return true;
            }
            catch (Exception ex)
            {
                //记录错误信息
                return false;                
            }
        }

        private static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {

        }

        private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

        }


        public static void ConvertVideoAsc()
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = @"c:/ffmpeg.exe";
            p.StartInfo.Arguments = "-i XXXXXXXXXXXXXX";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.WaitForExit();//等待完成
            p.StandardError.ReadToEnd();//开始同步读取
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }

    }
}
