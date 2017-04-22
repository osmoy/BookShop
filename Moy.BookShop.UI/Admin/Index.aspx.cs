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
    public partial class Index : PageBase
    {
        protected IEnumerable<Book> books = null;
        protected string htmlLink = string.Empty;
        protected IEnumerable<Category> categories = null;
        protected IEnumerable<Publisher> publishes = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var pageIndex = string.IsNullOrEmpty(Request.QueryString["pageIndex"]) ? 1 : int.Parse(Request.QueryString["pageIndex"]);
            var pageSize = Convert.ToInt32(Common.CommonHelper.GetValue("pageSize"));
            var categoryId = string.IsNullOrEmpty(Request["categoryId"]) ? 0 : Convert.ToInt32(Request["categoryId"]);
            int totalCount = BookManage.GetTotalCount(categoryId);
            int totalPage = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            categories = CategoryManage.GetAll();
            publishes = PublishManage.GetAll();

            books = BookManage.GetByPage(pageIndex, pageSize, categoryId);           
            htmlLink = utility.Paging.PageList(pageIndex, totalCount);
        }


    }
}