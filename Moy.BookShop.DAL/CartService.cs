using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Moy.BookShop.Model;

namespace Moy.BookShop.DAL
{
    public class CartService
    {

        public int Add(Cart cart)
        {
            return SqlHelper.ExecuteNonQuery("insert into Cart(UserId,BookId,Quantity) values(@UserId,@BookId,@Quantity)",
                CommandType.Text, new SqlParameter("@UserId", cart.User.Id),
                new SqlParameter("@BookId", cart.Book.Id), new SqlParameter("@Quantity", cart.Quantit));
        }


        public int Delete(int id)
        {
            return SqlHelper.ExecuteNonQuery("delete from Cart where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
        }

        public int Modify(Cart cart)
        {
            return SqlHelper.ExecuteNonQuery("update Cart set Quantity=@Quantity where Id=@Id", CommandType.Text,
                 new SqlParameter("@Quantity", cart.Quantit), new SqlParameter("@Id", cart.Id));
        }

        public Cart GetById(int bookId, int userId)
        {
            Cart cart = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select * from Cart where BookId=@BookId and UserId=@UserId",
                CommandType.Text, new SqlParameter("BookId", bookId), new SqlParameter("@UserId", userId));
            using (reader)
            {
                if (reader.Read())
                {
                    cart = ToModel(reader);
                }
            }
            return cart;
        }

        private Cart ToModel(SqlDataReader reader)
        {
            return new Cart
            {
                Id = reader.GetInt32(0),
                User = new UserService().GetById(reader.GetInt32(1)),
                Book = new BookService().GetById(reader.GetInt32(2)),
                Quantit = reader.GetInt32(3)
            };
        }

        public IEnumerable<Cart> GetAll(int userId)
        {
            IList<Cart> list = new List<Cart>();
            SqlDataReader reader = SqlHelper.ExecuteReader("select * from Cart where UserId=@UserId",
                CommandType.Text, new SqlParameter("@UserId", userId));
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cart cart = ToModel(reader);
                        list.Add(cart);
                    }
                }
            }
            return list;
        }

        public Cart GetById(int id)
        {
            Cart cart = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select * from Cart where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
            using (reader)
            {
                if (reader.Read())
                {
                    cart = ToModel(reader);
                }
            }
            return cart;
        }

    }
}