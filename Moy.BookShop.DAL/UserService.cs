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
    public class UserService
    {
        public User GetByLoginId(string loginId)
        {
            User user = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserRoleId,UserStateId from Users where LoginId=@LoginId",
                CommandType.Text, new SqlParameter("@LoginId", loginId));
            using (reader)
            {
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        LoginId = reader["LoginId"].ToString(),
                        LoginPwd = reader["LoginPwd"].ToString(),
                        Name = (string)reader["Name"],
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Mail = reader["Mail"].ToString(),
                        UserRole = new UserRoleService().GetByUserRoleId(Convert.ToInt32(reader["UserRoleId"])),
                        UserState = new UserStateService().GetByUserStateId(Convert.ToInt32(reader["UserStateId"]))
                    };
                }
            }
            return user;
        }

        public int Add(User user)
        {
            return (int)SqlHelper.ExecuteScalar(@"insert into Users(LoginId,LoginPwd,Name,[Address],Phone,Mail,Birthday,UserRoleId,UserStateId,RegisterIp,RegisterTime) 
                output inserted.Id values(@LoginId,@LoginPwd,@Name,@Address,@Phone,@Mail,@Birthday,@UserRoleId,@UserStateId,@RegisterIp,getdate())", CommandType.Text,
                new SqlParameter("@LoginId", user.LoginId),
                new SqlParameter("@LoginPwd", user.LoginPwd),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@Phone", user.Phone),
                new SqlParameter("@Mail", user.Mail),
                new SqlParameter("@Birthday", SqlHelper.ToDBNull(user.Birthday)),
                new SqlParameter("@UserRoleId", user.UserRole.Id),
                new SqlParameter("@UserStateId", user.UserState.Id),
                new SqlParameter("@RegisterIp", user.RegisterIp));
        }

        public List<User> GetAllUsers()
        {
            DataTable dt = SqlHelper.ExecuteTable("select Id,LoginId,Name,[Address],Phone,Mail,Birthday,UserRoleId,UserStateId from Users", CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                List<User> list = new List<User>();
                foreach (DataRow row in dt.Rows)
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        LoginId = row["LoginId"].ToString(),
                        Name = row["Name"].ToString(),
                        Address = row["Address"].ToString(),
                        Phone = row["Phone"].ToString(),
                        Mail = row["Mail"].ToString(),
                        Birthday = (DateTime?)SqlHelper.FromDBNull(row["Birthday"]),
                        UserRole = new UserRoleService().GetByUserRoleId(Convert.ToInt32(row["UserRoleId"])),
                        UserState = new UserStateService().GetByUserStateId(Convert.ToInt32(row["UserStateId"]))
                    };
                    list.Add(user);
                }
                return list;
            }
        }

        public User GetById(int id)
        {
            User user = null;
            SqlDataReader reader = SqlHelper.ExecuteReader("select Id,LoginId,LoginPwd,Name,UserRoleId,UserStateId from Users where Id=@Id",
                CommandType.Text, new SqlParameter("@Id", id));
            using (reader)
            {
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        LoginId = reader["LoginId"].ToString(),
                        LoginPwd = reader["LoginPwd"].ToString(),
                        Name = (string)reader["Name"],
                        UserRole = new UserRoleService().GetByUserRoleId(Convert.ToInt32(reader["UserRoleId"])),
                        UserState = new UserStateService().GetByUserStateId(Convert.ToInt32(reader["UserStateId"]))
                    };
                }
            }
            return user;
        }

    }
}