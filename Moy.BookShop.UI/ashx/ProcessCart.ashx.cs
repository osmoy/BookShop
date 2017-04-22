using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moy.BookShop.BLL;

namespace Moy.BookShop.UI.ashx
{
    /// <summary>
    /// ProcessCart 的摘要说明
    /// </summary>
    public class ProcessCart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["Action"];
            var cartId = int.Parse(context.Request.Params["CartId"]);
            if (!string.IsNullOrEmpty(action) && action == "delete")
            {
                var count = CartManage.Delete(cartId);
                if (count > 0)
                {
                    context.Response.Write("yes");
                }
                else
                {
                    context.Response.Write("no");
                }
            }
            else if (!string.IsNullOrEmpty(action) && action == "modify")
            {
                var count = context.Request.Params["Count"];
                var model = CartManage.GetById(cartId);
                if (model != null)
                {
                    model.Quantit = Convert.ToInt32(count);
                    int r = CartManage.Modify(model);
                    if (r > 0)
                    {
                        context.Response.Write("ok");
                    }
                    else
                    {
                        context.Response.Write("no");
                    }
                }
            }
            else
            {
                context.Response.Write("参数有误");
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