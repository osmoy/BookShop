using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moy.BookShop.BLL;
using Moy.BookShop.Model;
using System.Web.Script.Serialization;

namespace Moy.BookShop.UI.Admin.ashx
{
    /// <summary>
    /// ProcessBook 的摘要说明
    /// </summary>
    public class ProcessBook : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["Action"];
            var bookId = context.Request["bid"];
            int bid;
            if (action == "delete")
            {
                if (int.TryParse(bookId, out bid))
                {
                    int n = BookManage.Delete(bid);
                    if (n > 0)
                    {                        
                        //global.CreateIndex.index.Enqueue(bid);
                        context.Response.Write("ok");
                    }
                    else
                    {
                        context.Response.Redirect("/ShowMsg.aspx?msg=" + HttpUtility.UrlEncode("删除失败") +
                            "&txt=" + HttpUtility.UrlEncode("列表页面") + "&url=/Admin/Index.aspx");
                    }
                }
            }
            else if (action == "edit")
            {
                if (int.TryParse(bookId, out bid))
                {
                    var model = BookManage.GetById(bid);
                    if (model != null)
                    {
                        context.Response.Write(new JavaScriptSerializer().Serialize(model));
                    }
                }                
            }
            else if (action == "modify")
            {
                if (int.TryParse(bookId, out bid))
                {                    
                    string title = context.Request["title"];
                    string author = context.Request["author"];
                    string categoryId = context.Request["category"];
                    string publishId = context.Request["publish"];
                    string pubDate = context.Request["pubDate"];
                    string price = context.Request["price"];
                    Book book = new Book
                    {
                        Id = bid,
                        Title = title,
                        Author = author,
                        Category = CategoryManage.GetCategoryById(Convert.ToInt32(categoryId)),
                        Publisher = PublishManage.GetPublishById(Convert.ToInt32(publishId)),
                        PublishDate = Convert.ToDateTime(pubDate),
                        UnitPrice = Convert.ToDecimal(price)
                    };
                    int n = BookManage.Modify(book);
                    if (n > 0)
                    {
                        //global.CreateIndex.index.Enqueue(bid, title, book.ContentDescription);
                        context.Response.Write("yes");
                    }
                    else
                    {
                        context.Response.Write("no");
                    }
                }  
            }
            else if (action == "add")
            {
                string title = context.Request["title"];
                string author = context.Request["author"];
                string categoryId = context.Request["category"];
                string publishId = context.Request["publish"];
                string pubDate = context.Request["publishDate"];
                string isbn = context.Request["imgPath"];//上传图片以isbn命名
                string price = context.Request["unitePrice"];
                string desc = context.Request["description"];
                string toc = "钢铁是怎样炼成的";
                int clicks = 0;
                Book book = new Book
                {
                    Title = title,
                    Author = author,
                    Category = CategoryManage.GetCategoryById(Convert.ToInt32(categoryId)),
                    Publisher = PublishManage.GetPublishById(Convert.ToInt32(publishId)),
                    ContentDescription = desc,
                    ISBN = isbn,
                    PublishDate = Convert.ToDateTime(pubDate),
                    TOC = toc,
                    UnitPrice = Convert.ToDecimal(price),
                    Clicks = clicks,
                };
                int id = BookManage.Add(book);
                if (id > 0)
                {
                    //global.CreateIndex.index.Enqueue(bid, title, desc);
                    context.Response.Write("ok");
                }
                else
                {
                    context.Response.Write("no");
                }
            }
            else
            {
                context.Response.Write("action参数有误");
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