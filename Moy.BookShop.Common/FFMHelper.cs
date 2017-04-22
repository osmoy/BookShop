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
        /// <summary>
        ///  异步读取
        /// </summary>
        /// <param name="isPic"></param>
        public static bool ConvertVideo(bool isPic = false)
        {
            try
            {
                //创建并启动一个新进程
                Process p = new Process();
                //设置进程启动信息属性StartInfo，这是ProcessStartInfo类，包括了一些属性和方法：
                p.StartInfo.FileName = HttpContext.Current.Server.MapPath("ffmpeg.exe");
                p.StartInfo.UseShellExecute = false;
                //-y选项的意思是当输出文件存在的时候自动覆盖输出文件，不提示“y/n”这样才能自动化

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
                p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中
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

        /// <summary>
        /// 同步读取
        /// </summary>
        public static void ConvertVideoAsc()
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = @"c:/ffmpeg.exe";//要调用外部程序的绝对路径
            p.StartInfo.Arguments = "-i XXXXXXXXXXXXXX";//参数(这里就是FFMPEG的参数了)
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.Start();//启动线程
            p.WaitForExit();//等待完成
            p.StandardError.ReadToEnd();//开始同步读取
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }

    }
}
