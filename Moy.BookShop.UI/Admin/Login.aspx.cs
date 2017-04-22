using Moy.BookShop.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moy.BookShop.UI.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected string errMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            var login = Request.Form["btnLogin"];
            if (!string.IsNullOrEmpty(login))
            {
                var loginId = Request.Form["LoginId"];
                var loginPwd = Request.Form["LoginPwd"];
                var returnUrl = Request.Form["url"];
                if (UserManage.SearchByLoginId(loginId, loginPwd, out errMsg))
                {
                    Session["admin"] = loginId;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        Response.Redirect(returnUrl);
                    }
                    Response.Redirect("/Admin/Index.aspx");
                }
            }
        }

    }
}