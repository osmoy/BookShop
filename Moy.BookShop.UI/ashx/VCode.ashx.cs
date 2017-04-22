using Moy.BookShop.UI.utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Moy.BookShop.UI.ashx
{
    /// <summary>
    /// VCode 的摘要说明
    /// </summary>
    public class VCode : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/gif";

            string code = ValidataCode.CreateRandomCode(4);
            
            context.Session["Vcode"] = code;

            byte[] img = ValidataCode.DrawImage(code, 20, background: Color.White, border: Color.Black);

            context.Response.BinaryWrite(img);
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