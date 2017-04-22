using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Moy.BookShop.Model;

namespace Moy.BookShop.DAL
{
    public class UserStateService
    {
        /// <summary>
        /// 根据id查询用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserState GetByUserStateId(int id)
        {
            UserState state = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select * from UserStates where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
            using (reader)
            {
                if (reader.Read())
                {
                    state = new UserState()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = (string)reader["Name"]
                    };
                }
            }
            return state;
        }
    }
}