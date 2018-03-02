using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Moy.BookShop.Model;

namespace Moy.BookShop.DAL
{
    public class UserRoleService
    {
        public UserRole GetByUserRoleId(int id)
        {
            UserRole role = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select * from UserRoles where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
            using (reader)
            {
                if (reader.Read())
                {
                    role = new UserRole()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = (string)reader["Name"]
                    };
                }
            }
            return role;
        }
    }
}