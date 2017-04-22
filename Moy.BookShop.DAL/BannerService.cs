using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Moy.BookShop.DAL
{
    public class BannerService
    {     
        public IEnumerable<string> GetAllBanner()
        {
            IList<string> list = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select WordPattern from Banner_Words where IsForbid=1",
                CommandType.Text);
            using (reader)
            {
                if (reader.HasRows)
                {
                    list = new List<string>();
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
            }
            return list;
        }


    }
}
