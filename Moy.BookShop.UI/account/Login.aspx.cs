using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.Common;
using Moy.BookShop.BLL;

namespace Moy.BookShop.UI.account
{
    public partial class Login : System.Web.UI.Page
    {
        protected string msg = string.Empty;
        protected string userName = string.Empty;
        protected string pwd = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string login = Request.Form["login"];
            if (string.IsNullOrEmpty(login))
            {
                HttpCookie cookies = Request.Cookies["userInfo"];
                if (cookies != null)
                {
                    userName = cookies["userName"];
                    pwd = cookies["password"];
                    var model = UserManage.SearchByLoginId(userName);
                    if (model != null)
                    {
                        if (model.LoginPwd.Equals(pwd, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Session["currentUser"] = model;
                            GoPage("");
                        }
                        else
                        {
                            cookies.Expires = DateTime.Now.AddDays(-1);                            
                        }
                    }
                    else
                    {
                        cookies.Expires = DateTime.Now.AddDays(-1);
                    }
                }

            }
            else
            {
                string userName = Request.Form["username"];
                string password = Request.Form["password"];
                string vCode = Request.Form["vCode"];
                string remember = Request.Form["rememberme"];
                string url = Request.Form["returnUrl"];
                Model.User user = null;

                ///校验非空
                CheckEmpty(userName, password, vCode);
                if (Session["Vcode"] != null)
                {
                    string code = Session["Vcode"].ToString();
                    if (code != vCode)
                    {
                        msg = "验证码不正确";
                        return;
                    }
                }
                password = CommonHelper.GetMD5(password + CommonHelper.GetValue("pwdSalt"));
                var result = UserManage.IsLogin(userName, password, out user);
                switch (result)
                {
                    case BookShop.Model.Enum.LoginResult.用户名不存在:
                        msg = "用户名不存在，请核对后重试";
                        break;
                    case BookShop.Model.Enum.LoginResult.密码错误:
                        msg = "密码错误，请核对后重试";
                        break;
                    case BookShop.Model.Enum.LoginResult.用户已被冻结:
                        msg = "用户已被冻结，请联系管理员";
                        break;
                    case BookShop.Model.Enum.LoginResult.登录成功:
                        msg = "登陆成功";
                        if (remember == "on")
                        {
                            HttpCookie cookie = new HttpCookie("userInfo");
                            cookie.Values.Add("userName", userName);
                            cookie.Values.Add("password", password);
                            cookie.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Add(cookie);
                        }
                        //记录Session
                        Session["currentUser"] = user;
                        GoPage(url);
                        break;
                }
            }
        }

        private void GoPage(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Response.Redirect(url);
            }
            //跳转
            Response.Redirect("/ShowMsg.aspx?msg=" + Server.UrlEncode(msg) + "&txt=" + Server.HtmlEncode("首页")
                + "&url=" + Server.HtmlEncode("/book/Index.aspx"));
        }

        private void CheckEmpty(string userName, string password, string vCode)
        {
            if (string.IsNullOrEmpty(userName))
            {
                Response.Write("用户名不能为空");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                Response.Write("密码不能为空");
                return;
            }
            if (string.IsNullOrEmpty(vCode))
            {
                Response.Write("验证码不能为空");
                return;
            }
        }

    }
}