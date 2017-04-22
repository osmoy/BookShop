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
    public class CategoryService
    {               
        public Category GetCategoryById(int id)
        {
            Category category = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select Id,Name from Categories where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
            using (reader)
            {
                if (reader.Read())
                {
                    category = new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            return category;
        }
        
        public List<Category> GetAll()
        {
            DataTable dt = SqlHelper.ExecuteTable("select Id,Name from Categories", CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<Category> list = new List<Category>();
                foreach (DataRow row in dt.Rows)
                {
                    Category category = new Category()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString()
                    };
                    list.Add(category);
                }
                return list;
            }
        }

        public int Add(Category category)
        {
            return SqlHelper.ExecuteNonQuery("insert into Categories(Name) values(@name)",
               CommandType.Text, new SqlParameter("@name", category.Name));
        }

        public int Delete(int id)
        {
            return SqlHelper.ExecuteNonQuery("delete from Categories where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
        }

    }
}
