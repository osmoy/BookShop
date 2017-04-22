using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moy.BookShop.UI.Admin
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnPreLoad(EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("/Admin/Login.aspx?returnUrl=" + HttpUtility.UrlEncode(Request.Url.ToString()));
            }
            base.OnPreLoad(e);
        }

    }
}