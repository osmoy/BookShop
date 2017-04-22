using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moy.BookShop.UI
{
    public partial class BasePage : System.Web.UI.Page
    {      
        protected override void OnLoad(EventArgs e)
        {
            if (Session["currentUser"] == null)
            {
                Response.Redirect("/account/Login.aspx?returnUrl=" + HttpUtility.UrlEncode(Request.Url.ToString()));                
            }

            base.OnLoad(e);
        }
    }
}