using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.Model;
using System.Data;
using System.Data.SqlClient;

namespace Moy.BookShop.DAL
{
    public class PublishService
    {
        /// 根据id查询出版社
        public Publisher GetPublishById(int id)
        {
            Publisher publish = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select Id,Name from Publishers where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
            using (reader)
            {
                if (reader.Read())
                {
                    publish = new Publisher
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            return publish;
        }

        /// 获取所有出版社
        public List<Publisher> GetAll()
        {
            DataTable dt = SqlHelper.ExecuteTable("select Id,Name from Publishers", CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<Publisher> list = new List<Publisher>();
                foreach (DataRow row in dt.Rows)
                {
                    Publisher publishName = new Publisher()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString()
                    };
                    list.Add(publishName);
                }
                return list;
            }
        }

        /// 添加出版社
        public int Add(Publisher publish)
        {
            return SqlHelper.ExecuteNonQuery("insert into Publishers(Name) values(@name)",
                 CommandType.Text, new SqlParameter("@name", publish.Name));
        }

       ///根据id删除出版社
        public int Delete(int id)
        {
            return SqlHelper.ExecuteNonQuery("delete from Publishers where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
        }

    }
}