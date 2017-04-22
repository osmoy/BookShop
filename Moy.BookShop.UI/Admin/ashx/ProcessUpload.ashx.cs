using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Moy.BookShop.UI.Admin.ashx
{
    /// <summary>
    /// ProcessUpload 的摘要说明
    /// </summary>
    public class ProcessUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile uploadFile = context.Request.Files["fileUpload"];
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(uploadFile.FileName);
                var fileEx = Path.GetExtension(uploadFile.FileName);
                if (fileEx == ".jpg" || fileEx == ".png" || fileEx == ".gif" || fileEx == ".bmp")
                {
                    var dir = "/upload/" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(context.Request.MapPath(dir)));
                    var path = dir + Guid.NewGuid().ToString() + fileEx;
                    uploadFile.SaveAs(context.Server.MapPath(path));
                    context.Response.Write(path + ":" + Path.GetFileNameWithoutExtension(fileName));
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            else
            {
                context.Response.Write("empty");
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