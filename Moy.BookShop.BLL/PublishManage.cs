using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.Model;
using Moy.BookShop.DAL;

namespace Moy.BookShop.BLL
{
    public class PublishManage
    {       
        public static List<Publisher> GetAll()
        {
            return new PublishService().GetAll();
        }

        public static Publisher GetPublishById(int id)
        {
            return new PublishService().GetPublishById(id);
        }
    }
}
