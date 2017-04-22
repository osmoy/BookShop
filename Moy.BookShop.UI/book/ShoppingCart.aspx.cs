using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.BLL;
using Moy.BookShop.Model;

namespace Moy.BookShop.UI.book
{
    public partial class ShoppingCart : BasePage
    {
        protected IEnumerable<Cart> cartList = null;
        protected User user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["currentUser"] as User;            
            AddCart();
            cartList = CartManage.GetAll(user.Id);
        }

        private void AddCart()
        {
            int bookId;
            if (int.TryParse(Request.QueryString["bid"], out bookId))
            {
                var book = BookManage.GetById(bookId);
                if (book != null)
                {
                    var model = CartManage.GetById(bookId, user.Id);
                    if (model == null)
                    {
                        var cart = new Cart()
                        {
                            Book = book,
                            Quantit = 1,
                            User = user
                        };
                        CartManage.Add(cart);
                    }
                    else
                    {
                        model.Quantit++;
                        CartManage.Modify(model);
                    }
                }
                else
                {
                    Response.Redirect("/ShowMsg.aspx?msg" + Server.UrlEncode("没有该商品") +
                        "&txt=" + Server.UrlEncode("商品页面") + "&url=/book/BookList.aspx");
                }
            }
        }


    }
}