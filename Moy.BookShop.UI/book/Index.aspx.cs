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
    public partial class Index : System.Web.UI.Page
    {
        protected IEnumerable<Book> books;
        protected void Page_Load(object sender, EventArgs e)
        {
            books = BookManage.GetNewest();
            //var layout = this.Master as master.Common;    //可以拿到母版页对象
            //layout.IsIndex = true;
        }
    }
}