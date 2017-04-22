using System.Collections.Generic;
using System.Text;
using Moy.BookShop.Model;
using System.Web;
using Moy.BookShop.Common;
using Moy.BookShop.DAL;

namespace Moy.BookShop.BLL
{
    public class BookManage
    {
        public static IEnumerable<Book> GetAll()
        {
            return new BookService().GetAll();
        }

        public static IEnumerable<Book> GetNewest()
        {
            return new BookService().GetNewest();
        }

        public static Book GetById(int id)
        {
            return new BookService().GetById(id);
        }

        public static int Add(Book book)
        {
            return new BookService().Add(book);
        }

        public static int Delete(int bid)
        {
            return new BookService().Delete(bid);
        }

        public static int Modify(Book book)
        {
            return new BookService().Modify(book);
        }

        public static int GetTotalCount(int categoryId)
        {
            return new BookService().GetTotalCount(categoryId);
        }

        public static IEnumerable<Book> GetByPage(int pageIndex, int pageSize, int categoryId)
        {
            return new BookService().GetByPage(pageIndex, pageSize, categoryId);
        }

        public static IEnumerable<Book> GetPage()
        {
            return new BookService().GetPage();
        }

        /// <summary>
        /// 创建静态页面（根据商品id）
        /// </summary>
        public static void CreateHtmlPage(int id)
        {
            #region 第一个版本：使用replace替换
            //Book book = new BookService().GetById(id);
            //if (book != null)
            //{
            //    string path = HttpContext.Current.Server.MapPath("~/templates/BookDetatil.html");//找到模板路径..
            //    //读取模板中的内容..
            //    string content = System.IO.File.ReadAllText(path);
            //    content = content.Replace("$title", book.Title).Replace("$body", book.ContentDescription);//替换模板中的内容
            //    //把替换好的内容保存成一个文件.
            //    string dir = HttpContext.Current.Server.MapPath("~/staticPage/" + book.PublishDate.ToString("yyyy-MM-dd") + "/");
            //    ///创建文件夹
            //    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dir));
            //    System.IO.File.WriteAllText(dir + book.ISBN + ".html", content, Encoding.UTF8);
            //}
            #endregion
            Book book = new BookService().GetById(id);
            if (book != null)
            {
                string html = CommonHelper.RandomHtml("BookDetatil.html", book);
                string dir = HttpContext.Current.Server.MapPath("~/staticPage/" + book.PublishDate.ToString("yyyy-MM-dd") + "/");
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dir));
                System.IO.File.WriteAllText(dir + book.ISBN + ".html", html, Encoding.UTF8);
            }
        } 

    }
}