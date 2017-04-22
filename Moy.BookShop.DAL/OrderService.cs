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
    public class OrderService
    {
        public decimal CreateOrder(int userId, string orderNo, string address)
        {
            var paras = new SqlParameter[]{
                new SqlParameter("@userId", SqlDbType.Int,4),
                new SqlParameter("@OrderID",SqlDbType.NVarChar,50),
                new SqlParameter("@address",SqlDbType.NVarChar,100),
                new SqlParameter("@totalMoney",SqlDbType.Money,8),            
            };
            paras[0].Value = userId;
            paras[1].Value = orderNo;
            paras[2].Value = address;
            paras[3].Direction = ParameterDirection.Output;

            SqlHelper.RunProcedure("CreateOder", paras);

            return Convert.ToDecimal(paras[3].Value);
        }

        public IEnumerable<Order> GetById(string orderId)
        {
            IList<Order> list = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select * from Orders where OrderId=@OrderId",
                CommandType.Text, new SqlParameter("@OrderId", orderId));
            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list = new List<Order>();
                        Order order = new Order
                        {
                            Id = reader.GetString(0),
                            OrderDate = reader.GetDateTime(1),
                            UserId = reader.GetInt32(2),
                            TotalPrice = reader.GetDecimal(3),
                            ReceiverAddress = reader.GetString(4),
                            OrderState = reader.GetBoolean(5)
                        };
                        list.Add(order);
                    }
                }
            }
            return list;
        }

        public int Modify(string orderId, int userId)
        {
            return SqlHelper.ExecuteNonQuery("update Orders set OrderState=1 where OrderId=@OrderId and UserId=@UserId",
                CommandType.Text, new SqlParameter("@OrderId", orderId), new SqlParameter("@UserId", userId));
        }

    }
}