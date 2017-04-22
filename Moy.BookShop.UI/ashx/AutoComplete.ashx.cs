using Moy.BookShop.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Moy.BookShop.UI.ashx
{
    /// <summary>
    /// AutoComplete 的摘要说明
    /// </summary>
    public class AutoComplete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var kw = context.Request["term"];
            List<string> relative = SearchStatisticManage.ShowRelative(kw);

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(relative));
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