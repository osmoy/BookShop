using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.Model;
using Moy.BookShop.BLL;

namespace Moy.BookShop.UI.Admin
{
    public partial class AddBook : PageBase
    {
        protected IEnumerable<Publisher> publish = null;
        protected IEnumerable<Category> categories = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            categories = CategoryManage.GetAll();
            publish = PublishManage.GetAll();
        }
    }
}