using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.DAL;
using Moy.BookShop.Model;
using Moy.BookShop.Model.Enum;

namespace Moy.BookShop.BLL
{
    public class UserManage
    {
        /// <summary>
        /// 是否登陆成功
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginPwd"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static LoginResult IsLogin(string loginId, string loginPwd, out User user)
        {
            User u = new UserService().GetByLoginId(loginId);
            if (u == null)
            {
                user = null;
                return LoginResult.用户名不存在;
            }
            else
            {
                if (u.LoginPwd != loginPwd)
                {
                    user = null;
                    return LoginResult.密码错误;
                }
                else if (u.UserState.Id != 1)
                {
                    user = null;
                    return LoginResult.用户已被冻结;
                }
                else
                {
                    user = u;
                    return LoginResult.登录成功;
                }
            }
        }

        /// <summary>
        /// 判断是否注册成功
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static RegisterResult IsRegiser(User user)
        {
            User u = new UserService().GetByLoginId(user.LoginId);
            if (u != null)
            {
                return RegisterResult.用户名已存在;
            }
            else//没有注册
            {
                user.UserRole = new UserRole() { Id = 1, Name = "普通用户" };
                user.UserState = new UserState() { Id = 1, Name = "正常" };
                int n = new UserService().Add(user);    //拿到刚注册的用户id.
                if (n > 0)
                {
                    user.Id = n;
                    return RegisterResult.注册成功;
                }
                else
                {
                    return RegisterResult.注册失败;
                }
            }
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
            return new UserService().GetAllUsers();
        }

        /// <summary>
        /// 通过用户名查询用户信息，非管理员用户不允许登陆
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginPwd"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SearchByLoginId(string loginId, string loginPwd, out string msg)
        {
            User user = new UserService().GetByLoginId(loginId);
            if (user == null)
            {
                msg = "此用户名不存在，请核对后重试";
                return false;
            }
            else
            {
                if (user.UserRole.Id != 3)
                {
                    msg = "您没有权限登陆，请联系管理员";
                    return false;
                }
                else
                {
                    if (user.LoginPwd != loginPwd)
                    {
                        msg = "密码输入错误，请核对后重试";
                        return false;
                    }
                    msg = "";
                    return true;
                }
            }
        }

        public static User SearchByLoginId(string loginId)
        {            
            return new UserService().GetByLoginId(loginId);           
        }

    }
}
