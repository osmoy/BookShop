using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Moy.BookShop.UI.Admin.ashx
{
    /// <summary>
    /// LogOut 的摘要说明
    /// </summary>
    public class LogOut : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Session["admin"] != null)
            {
                context.Session.Clear();
            }
            context.Response.Redirect("/Admin/Login.aspx");

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