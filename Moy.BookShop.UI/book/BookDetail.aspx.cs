using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.Model;
using Moy.BookShop.BLL;

namespace Moy.BookShop.UI.book
{
    public partial class BookDetail : System.Web.UI.Page
    {
        protected Book book = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["bid"];
            int bid;
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out bid))
            {
                book = BookManage.GetById(bid);
            }
        }

    }
}