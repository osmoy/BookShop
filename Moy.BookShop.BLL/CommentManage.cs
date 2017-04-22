using System.Collections.Generic;
using Moy.BookShop.DAL;
using Moy.BookShop.Model;

namespace Moy.BookShop.BLL
{
    public class CommentManage
    {
        public static int Add(Comment comment)
        {
            return new CommentService().Add(comment);
        }

        public static IEnumerable<Comment> GetAll(int bookId)
        {
            return new CommentService().GetAll(bookId);
        }

        public static IEnumerable<Comment> PagingList(int bookId, int pageIndex, int pageSize)
        {
            return new CommentService().PagingList(bookId, pageIndex, pageSize);
        }

        public static int GetCount(int bookId)
        {
            return new CommentService().GetCount(bookId);
        }

    }
}
