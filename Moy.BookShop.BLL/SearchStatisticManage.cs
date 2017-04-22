using Moy.BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.BLL
{
    public class SearchStatisticManage
    {
        public static int Delete()
        {
            return new SearchStatisticService().Delete();
        }

        public static int Add()
        {
            return new SearchStatisticService().Add();
        }

        public static List<string> ShowRelative(string kw)
        {
            return new SearchStatisticService().ShowRelative(kw);
        }

    }
}
