using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.Common;

namespace Moy.BookShop.UI.test
{
    public partial class CreateStaticPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var list = BLL.BookManage.GetPage();
            foreach (var book in list)
            {
                BLL.BookManage.CreateHtmlPage(book.Id);
            }
            Context.Response.Write("ok");
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            if (Common.FFMHelper.ConvertVideo())
                Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>alert('转换成功!')</script>");
            else
                Response.Write("转换失败");
        }

    }
}