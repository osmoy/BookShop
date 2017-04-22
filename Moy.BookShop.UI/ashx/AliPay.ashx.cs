using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moy.BookShop.Model;
using Moy.BookShop.BLL;
using Moy.BookShop.Common;

namespace Moy.BookShop.UI.ashx
{
    /// <summary>
    /// AliPay 的摘要说明
    /// </summary>
    public class AliPay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string orderId = context.Request.QueryString["out_trade_no"];
            string code = context.Request.QueryString["returncode"];
            string totalMoney = context.Request.QueryString["total_fee"];
            string sign = context.Request.QueryString["sign"];
            var user = context.Session["currentUser"] as User;

            var list = OrderManage.GetById(orderId);
            if (list != null && list.Count() > 0)
            {
                string mySign = CommonHelper.GetMD5(orderId + code + totalMoney + CommonHelper.GetValue("key"));
                if (sign.Equals(mySign, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(code) && code == "ok")
                    {
                        if (OrderManage.Modify(orderId, user.Id) > 0)
                        {
                            context.Response.Write("支付成功");
                        }
                    }
                    else
                    {
                        context.Response.Write("支付失败");
                    }
                }
            }
            else
            {
                context.Response.Write("没有找到该订单号");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}