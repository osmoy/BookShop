using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moy.BookShop.BLL;
using Moy.BookShop.Model;
using System.Web.Script.Serialization;

namespace Moy.BookShop.UI.ashx
{
    /// <summary>
    /// ProcessComment 的摘要说明
    /// </summary>
    public class ProcessComment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.Params["data[Action]"];
            string title = context.Request.Params["data[Title]"];
            string msg = context.Request.Params["data[Msg]"];
            string bookId = context.Request.Params["data[bookId]"];
            if (type == "add")
            {
                if (BannerManage.IsBannerdWord(msg))
                {
                    context.Response.Write("forbid");
                    return;
                }
                Comment comment = new Comment
                {
                    BookId = Convert.ToInt32(bookId),
                    Content = msg,
                    Title = title
                };
                int n = CommentManage.Add(comment);
                if (n > 0)
                {
                    context.Response.Write("ok");
                }
                else
                {
                    context.Response.Write("no");
                }
            }
            else if (type == "load")
            {
                var comments = CommentManage.GetAll(Convert.ToInt32(bookId));
                List<object> list = null;
                if (comments != null && comments.Count() > 0)
                {
                    list = new List<object>();
                    foreach (var c in comments)
                    {
                        var data = new
                        {
                            Title = c.Title,
                            Content = Utility.UBBHelper.Decode(c.Content, true),
                            Date = Common.CommonHelper.GetTimeSpan((DateTime.Now - c.Date))
                        };
                        list.Add(data);
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                context.Response.Write(js.Serialize(list));

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