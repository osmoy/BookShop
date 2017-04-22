using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moy.BookShop.UI
{
    public partial class ShowMsg : System.Web.UI.Page
    {
        protected string msg = string.Empty;
        protected string text = string.Empty;
        protected string url = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["msg"]))
            {
                msg = Request.QueryString["msg"];
            }
            else
            {
                msg = "暂无消息";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["txt"]))
            {
                text = Request.QueryString["txt"];
            }
            else
            {
                text = "首页";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["url"]))
            {
                url = Request.QueryString["url"];
            }
            else
            {
                url = "/book/Index.aspx";
            }
        }

    }
}