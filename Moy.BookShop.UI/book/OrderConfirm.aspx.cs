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
    public partial class OrderConfirm : BasePage
    {
        protected User user = null;
        protected IEnumerable<Cart> carts = null;
        protected decimal money = 0M;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["currentUser"] as User;
            string settlement = Request["settlement"];
            if (string.IsNullOrEmpty(settlement))
            {
                ShowCartList();
            }
            else
            {
                GoPay();
            }
        }

        private void ShowCartList()
        {
            var list = CartManage.GetAll(user.Id);

            if (list != null && list.Count() > 0)
            {
                carts = list;
                foreach (var c in carts)
                {
                    money = money + (c.Quantit * c.Book.UnitPrice);
                }
            }
            else
            {
                Response.Redirect("/ShowMsg.aspx?msg=" + Server.UrlEncode("购物车中没有商品") +
                 "&txt=" + Server.UrlEncode("商品列表页面") + "&url=/book/BookList.aspx");
            }
        }

        private void GoPay()
        {
            var orderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + user.Id;

            var address = string.Format("收货人：{0},地址：{1},电话：{2},邮编：{3}", Request["txtName"],
                Request["txtAddress"], Request["txtPhone"], Request["txtPostCode"]);

            decimal totalFee = OrderManage.CreateOrder(user.Id, orderNo, address);

            var pay = new aliPay.Pay("图书", "图书商城", orderNo, totalFee);

            //跳转
            Response.Redirect(pay.PayUrl());
        }

    }
}