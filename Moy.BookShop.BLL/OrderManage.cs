using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.DAL;

namespace Moy.BookShop.BLL
{
    public class OrderManage
    {
        public static decimal CreateOrder(int userId, string orderNo, string address)
        {
            return new OrderService().CreateOrder(userId, orderNo, address);
        }

        public static IEnumerable<Model.Order> GetById(string orderId)
        {
            return new OrderService().GetById(orderId);
        }

        public static int Modify(string orderId, int userId)
        {
            return new OrderService().Modify(orderId, userId);
        }


    }
}
