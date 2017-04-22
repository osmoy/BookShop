using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.Model;
using Moy.BookShop.BLL;
using Moy.BookShop.Common;
using Moy.BookShop.UI.utility;

namespace Moy.BookShop.UI.book
{
    public partial class BookList : System.Web.UI.Page
    {
        protected IEnumerable<Model.Book> bookList = null;
        protected int pageSize = Convert.ToInt32(CommonHelper.GetValue("pageSize"));
        protected int pageIndex;
        protected int categoryId;
        protected string strHtml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBookList();
        }

        private void LoadBookList()
        {
            if (string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
            {
                pageIndex = 1;
            }
            else
            {
                int pid;
                if (int.TryParse(Request.QueryString["pageIndex"], out pid))
                {
                    pageIndex = pid;
                }
            }
            if (string.IsNullOrEmpty(Request.QueryString["categoryId"]))
            {
                categoryId = 0;
            }
            else
            {
                int result;
                if (int.TryParse(Request.QueryString["categoryId"], out result))
                {
                    categoryId = result;
                }
            }
            int totalCount = BookManage.GetTotalCount(categoryId);
            int totalPage = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            bookList = BLL.BookManage.GetByPage(pageIndex, pageSize, categoryId);
            strHtml = Paging.PageList(pageIndex, totalPage);
        }

        protected string CutString(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + "......";
            }
            return str;
        }

    }
}