using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace ConvertVideoServer
{
    /// <summary>
    /// UploadVideo 的摘要说明
    /// </summary>
    public class UploadVideo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var id = Convert.ToInt32(context.Request.QueryString["Id"]);
            var fileExt = context.Request.QueryString["fileExt"];
            var path = ConfigurationManager.AppSettings["path"];
            using (FileStream fs = File.OpenWrite(path + id + fileExt))
            {
                context.Request.InputStream.CopyTo(fs);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}