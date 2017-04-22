using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Moy.BookShop.UI.ashx
{
    /// <summary>
    /// Logout 的摘要说明
    /// </summary>
    public class Logout : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["currentUser"] != null)
            {
                context.Session.Clear();
            }
            context.Response.Redirect("/book/Index.aspx");
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