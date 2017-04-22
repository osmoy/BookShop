using System.Collections.Generic;
using Moy.BookShop.Model;
using Moy.BookShop.DAL;

namespace Moy.BookShop.BLL
{
    public class CartManage
    {
        public static int Add(Cart cart)
        {
            return new CartService().Add(cart);
        }

        public static int Delete(int id)
        {
            return new CartService().Delete(id);
        }

        public static int Modify(Cart cart)
        {
            return new CartService().Modify(cart);
        }
        
        public static Cart GetById(int bookId, int userId)
        {
            return new CartService().GetById(bookId, userId);
        }
        
        public static IEnumerable<Cart> GetAll(int userId)
        {
            return new CartService().GetAll(userId);
        }
        
        public static Cart GetById(int id)
        {
            return new CartService().GetById(id);
        }

    }
}
