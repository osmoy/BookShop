using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Moy.BookShop.DAL
{
    public class SearchDetailService
    {
        public int Add(string keyWord)
        {
            return SqlHelper.ExecuteNonQuery("insert into SearchDetail(KeyWord) values(@KeyWords)",
               CommandType.Text, new SqlParameter("@KeyWords", keyWord));
        }
    }
}
