using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Moy.BookShop.DAL
{
    public class CaptchaService
    {
        /// <summary>
        /// 添加用户激活信息
        /// </summary>      
        public void Add(int userId, string token)
        {
            SqlHelper.ExecuteNonQuery(@"insert into Captcha(UserId, Token, Actived, Expired) 
                values(@UserId,@Token,0,getdate()+7)", CommandType.Text,
                new SqlParameter("@UserId", userId), 
                new SqlParameter("@Token", token));
        }
    }
}
