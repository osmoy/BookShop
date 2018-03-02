using System;
using System.Collections.Generic;
using Moy.BookShop.Model;
using System.Data;
using System.Data.SqlClient;

namespace Moy.BookShop.DAL
{
    public class CommentService
    {
        public int Add(Comment comment)
        {
            return SqlHelper.ExecuteNonQuery(@"insert into ReaderComments(BookId,Title,Comment,[Date]) 
                values(@BookId,@Title,@Comment,getdate())", CommandType.Text, new SqlParameter("@BookId", comment.BookId),
             new SqlParameter("@Title", comment.Title), new SqlParameter("@Comment", comment.Content));
        }

        public IEnumerable<Comment> GetAll(int bookId)
        {
            DataTable dt = SqlHelper.ExecuteTable("select * from ReaderComments where BookId=@BookId order by Date desc",
                CommandType.Text, new SqlParameter("@BookId", bookId));
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                IList<Comment> list = new List<Comment>();
                foreach (DataRow row in dt.Rows)
                {
                    Comment comment = new Comment
                    {
                        Title = row["Title"].ToString(),
                        Content = row["Comment"].ToString(),
                        Date = Convert.ToDateTime(row["Date"])
                    };
                    list.Add(comment);
                }
                return list;
            }
        }

        public IEnumerable<Comment> PagingList(int bookId, int pageIndex, int pageSize)
        {
            IList<Comment> list = null;
            string sql = @"select Title,Comment,[DATE] from 
                (select *,row_number() over(order by Id desc) as num from ReaderComments where BookId=@BookId) as r 
                where r.num between @Start and @End";
            SqlParameter[] paras ={
                                  new SqlParameter("@BookId",bookId),
                                  new SqlParameter("@Start",(pageIndex-1)*pageSize+1),
                                  new SqlParameter("@End",pageIndex*pageSize)
                                  };
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, paras);
            using (reader)
            {
                if (reader.HasRows)
                {
                    list = new List<Comment>();
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Title = reader.GetString(0),
                            Content = reader.GetString(1),
                            Date = reader.GetDateTime(2)
                        };
                        list.Add(comment);
                    }
                }
            }
            return list;
        }

        public int GetCount(int bookId)
        {
            return (int)SqlHelper.ExecuteScalar("select count(1) from ReaderComments where BookId=@BookId",
                CommandType.Text, new SqlParameter("@BookId", bookId));
        }

    }
}