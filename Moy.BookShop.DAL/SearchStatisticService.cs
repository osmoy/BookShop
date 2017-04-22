using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Moy.BookShop.DAL
{
    public class SearchStatisticService
    {
        public int Delete()
        {
            return SqlHelper.ExecuteNonQuery("delete from SearchStatistic", CommandType.Text);
        }

        public int Add()
        {
            return SqlHelper.ExecuteNonQuery(@"insert into SearchStatistic(KeyWord,TotalCount) select KeyWord,count(*) from
                SearchDetail where DateDiff(day,SearchTime,getdate())<=7 group by KeyWord", CommandType.Text);
        }

        public List<string> ShowRelative(string kw)
        {
            List<string> list = null;
            string sql = "select KeyWord from SearchStatistic where KeyWord like @KeyWord+'%'";
            DataTable dt = SqlHelper.ExecuteTable(sql, CommandType.Text, new SqlParameter("@KeyWord", kw));

            if (dt.Rows.Count > 0)
            {
                list = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["KeyWord"].ToString());
                }
            }

            return list;
        }


    }
}
