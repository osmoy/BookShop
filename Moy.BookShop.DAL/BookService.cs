using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.Model;
using System.Data;
using System.Data.SqlClient;
using Moy.BookShop.DAL;

namespace Moy.BookShop.DAL
{
    public class BookService
    {
        public IEnumerable<Book> GetAll()
        {
            string sql = "select * from Books order by PublishDate desc";
            return GetBooksBySql(sql, null);
        }

        public IEnumerable<Book> GetNewest()
        {
            string sql = "select top 16 * from Books order by PublishDate desc";
            return GetBooksBySql(sql, null);
        }

        public Book GetById(int id)
        {
            DataTable dt = SqlHelper.ExecuteTable("select * from Books where Id=@Id",
               CommandType.Text, new SqlParameter("@Id", id));
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("查出多条id=" + id + "的图书");
            }
            else
            {
                Book book = new Book();
                foreach (DataRow row in dt.Rows)
                {
                    book = ToBook(row);
                }
                return book;
            }
        }

        public int Delete(int bid)
        {
            return SqlHelper.ExecuteNonQuery("delete from Books where Id=@Id", CommandType.Text, new SqlParameter("@Id", bid));
        }

        public int Modify(Book book)
        {
            return SqlHelper.ExecuteNonQuery(@"update Books set Title=@Title,Author=@Author,PublisherId=@PublisherId,
                PublishDate=@PublishDate,UnitPrice=@UnitPrice,CategoryId=@CategoryId where Id=@Id",
                CommandType.Text, new SqlParameter("@Title", book.Title), new SqlParameter("@Author", book.Author),
                new SqlParameter("@PublisherId", book.Publisher.Id),
                new SqlParameter("@PublishDate", book.PublishDate),
                new SqlParameter("@UnitPrice", book.UnitPrice),
                new SqlParameter("@CategoryId", book.Category.Id),
                new SqlParameter("@Id", book.Id));
        }

        public int Add(Book book)
        {
            return (int)SqlHelper.ExecuteScalar(@"insert into Books(Title,Author,PublisherId,PublishDate,ISBN,UnitPrice,ContentDescription,TOC,CategoryId,Clicks) 
                output inserted.Id values(@Title,@Author,@PublisherId,@PublishDate,@ISBN,@UnitPrice,@ContentDescription,@TOC,@CategoryId,@Clicks)", CommandType.Text,
                new SqlParameter("@Title", book.Title),
                new SqlParameter("@Author", book.Author),
                new SqlParameter("@PublisherId", book.Publisher.Id),
                new SqlParameter("@PublishDate", book.PublishDate),
                new SqlParameter("@ISBN", book.ISBN),
                new SqlParameter("@UnitPrice", book.UnitPrice),
                new SqlParameter("@ContentDescription", book.ContentDescription),
                new SqlParameter("@TOC", book.TOC),
                new SqlParameter("@CategoryId", book.Category.Id),
                new SqlParameter("@Clicks", book.Clicks));
        }

        public int GetTotalCount(int categoryId)
        {
            string sql = "select count(0) from Books";
            if (categoryId != 0)
            {
                sql += " where CategoryId=@CategoryId";
            }
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@CategoryId", categoryId));
        }

        public IEnumerable<Book> GetByPage(int pageIndex, int pageSize, int categoryId)
        {
            string sql = @"select * from
                (
                select *,ROW_NUMBER() over(order by Id desc) as num from Books{0}
                )as s
                where s.num between @Start and @End";
            sql = string.Format(sql, categoryId != 0 ? " where CategoryId=@CategoryId" : "");
            SqlParameter[] paras = {
                new SqlParameter("@Start",(pageIndex-1)*pageSize+1),
                new SqlParameter("@End",pageSize*pageIndex),
                new SqlParameter("@CategoryId",categoryId)
            };
            return GetBooksBySql(sql, paras);
        }

        private IEnumerable<Book> GetBooksBySql(string sql, params SqlParameter[] cmdParas)
        {
            DataTable dt = SqlHelper.ExecuteTable(sql, CommandType.Text, cmdParas);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                IList<Book> bookList = new List<Book>();
                foreach (DataRow row in dt.Rows)
                {
                    Book book = ToBook(row);
                    bookList.Add(book);
                }
                return bookList;
            }
        }

        private Book ToBook(DataRow row)
        {
            return new Book()
            {
                Id = Convert.ToInt32(row["Id"]),
                Title = row["Title"].ToString(),
                Author = row["Author"].ToString(),
                PublishDate = (DateTime)row["PublishDate"],
                ISBN = row["ISBN"].ToString(),
                UnitPrice = (decimal)row["UnitPrice"],
                ContentDescription = (string)SqlHelper.FromDBNull(row["ContentDescription"]),
                TOC = (string)SqlHelper.FromDBNull(row["TOC"]),
                Clicks = Convert.ToInt32(row["Clicks"]),
                //对外键对象的处理
                Category = new CategoryService().GetCategoryById(Convert.ToInt32(row["CategoryId"])),
                Publisher = new PublishService().GetPublishById(Convert.ToInt32(row["PublisherId"]))
            };
        }

        public IEnumerable<Book> GetPage()
        {
            string sql = @"select top 10 * from Books order by Id desc";
          
            return GetBooksBySql(sql, null);
        }


    }
}