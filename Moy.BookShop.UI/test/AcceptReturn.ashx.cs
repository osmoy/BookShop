using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Moy.BookShop.UI.test
{
    /// <summary>
    /// AcceptReturn 的摘要说明
    /// </summary>
    public class AcceptReturn : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var id = Convert.ToInt32(context.Request.QueryString["Id"]);

            using (FileStream fs = File.OpenWrite(context.Request.MapPath("/Video/" + id + ".flv")))
            {
                context.Request.InputStream.CopyTo(fs);
            }

            //通过回传的id从数据库找到，最终更改状态 回传服务器完成.
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