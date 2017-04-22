using Moy.BookShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.BLL
{
    public class SearchDetailManage
    {
        public static int Add(string keyWord)
        {
            return new SearchDetailService().Add(keyWord);
        }
    }
}
