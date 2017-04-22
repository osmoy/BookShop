using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using NVelocity.App;
using NVelocity.Runtime;
using NVelocity;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;

namespace Moy.BookShop.Common
{
    public class CommonHelper
    {
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] hashBuffer = md5.ComputeHash(buffer);
            md5.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBuffer.Length; i++)
            {
                sb.Append(hashBuffer[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GetSalt()
        {
            return ConfigurationManager.AppSettings["pwdSalt"];
        }

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetMD5(Stream stream)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] arrbytHashValue = md5.ComputeHash(stream);
            md5.Clear();
            string strHashData = BitConverter.ToString(arrbytHashValue);
            strHashData = strHashData.Replace("-", "");
            return strHashData;
        }

        public static string GetDefault(string url)
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath(url)))
            {
                return "~/libs/Images/default.jpg";
            }
            return url;
        }

        public static string RandomHtml(string templateName, object data)
        {
            VelocityEngine vltEngine = new VelocityEngine();
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH,
                System.Web.Hosting.HostingEnvironment.MapPath("~/templates"));
            vltEngine.Init();

            VelocityContext vltContext = new VelocityContext();
            vltContext.Put("Model", data);

            Template vltTemplate = vltEngine.GetTemplate(templateName);
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);

            string html = vltWriter.GetStringBuilder().ToString();
            return html;
        }

        public static string GetTimeSpan(TimeSpan ts)
        {
            if (ts.TotalDays >= 365)
            {
                return Math.Floor(ts.TotalDays / 365) + "年前";
            }
            else if (ts.TotalDays >= 30)
            {
                return Math.Floor(ts.TotalDays / 30) + "月前";
            }
            else if (ts.TotalDays >= 1)
            {
                return Math.Floor(ts.TotalDays) + "天前";
            }
            else if (ts.TotalHours >= 1)
            {
                return Math.Floor(ts.TotalHours) + "小时前";
            }
            else if (ts.TotalMinutes >= 1)
            {
                return Math.Floor(ts.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }

        public static string[] SplitWord(string str)
        {
            List<string> list = new List<string>();
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list.ToArray();
        }

        public static string HighLight(string keyword, string content)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
                   new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\"><b>", "</b></font>");
            PanGu.HighLight.Highlighter highlighter =
                new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            //设置每个摘要段的字符数 
            highlighter.FragmentSize = 100;
            //获取最匹配的摘要段 
            return highlighter.GetBestFragment(keyword, content);
        }

    }
}