using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.DAL;
using Moy.BookShop.Model;

namespace Moy.BookShop.BLL
{
    public class CategoryManage
    {
        public static List<Category> GetAll()
        {
            return new CategoryService().GetAll();
        }

        public static Category GetCategoryById(int id)
        {
            return new CategoryService().GetCategoryById(id);
        }
    }
}
