using Moy.BookShop.BLL;
using Moy.BookShop.Common;
using Moy.BookShop.Model;
using Moy.BookShop.UI.utility;
using System;

namespace Moy.BookShop.UI.account
{
    public partial class Reg : System.Web.UI.Page
    {
        protected string msg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string save = Request.Form["save"];
            if (!string.IsNullOrEmpty(save))
            {
                string userName = Request.Form["username"];
                string pwd = Request.Form["password"];
                string rePwd = Request.Form["rePwd"];
                string realName = Request.Form["realname"];
                string address = Request.Form["address"];
                string tel = Request.Form["tel"];
                string email = Request.Form["email"];
                string birthday = Request.Form["birthday"];
                string vCode = Request.Form["vCode"];
                //string url = Request.Form["returnUrl"];   //url跳转，看需求..

                CheckEmpty(userName, pwd, realName);
                ////todo...校验非空，合法性、异步检查信息..
                if (pwd != rePwd)
                {
                    msg = "两次输入的密码不一致";
                    return;
                }
                if (vCode != null && Session["Vcode"] != null)
                {
                    string code = Session["Vcode"].ToString();
                    if (code != vCode)
                    {
                        msg = "验证码不正确";
                        return;
                    }
                }
                var user = new User
                {
                    LoginId = userName,
                    LoginPwd = CommonHelper.GetMD5(pwd + CommonHelper.GetSalt()),
                    Name = realName,
                    Mail = email,
                    RegisterTime = DateTime.Now,
                    RegisterIp = Request.UserHostAddress,
                    Phone = tel,
                    Birthday = Convert.ToDateTime(birthday),
                    Address = address
                };
                var result = UserManage.IsRegiser(user);
                switch (result)
                {
                    case Moy.BookShop.Model.Enum.RegisterResult.用户名已存在:
                        msg = "此用户已被注册，请更换其他用户";
                        break;
                    case Moy.BookShop.Model.Enum.RegisterResult.注册失败:
                        msg = "注册失败，请重试";
                        break;
                    case Moy.BookShop.Model.Enum.RegisterResult.注册成功:
                        msg = "注册成功";
                        
                        string token = Guid.NewGuid().ToString();
                        
                        CaptchaManager.Add(user.Id, token);
                        
                        MailHelper.Send("419373118@qq.com", "激活认证邮件", "<a href='163.com?token=" + token + "'>点击激活</a>");
                        
                        Response.Redirect("/ShowMsg.aspx?msg=" + Server.UrlEncode(msg) +
                            "&txt=" + Server.UrlEncode("首页") + "&url=" + Server.UrlEncode("/account/Login.aspx"));

                        break;
                }
            }
            else
            {

            }

        }

        private void CheckEmpty(string userName, string pwd, string realName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                Response.Write("用户名不能为空");
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                Response.Write("密码不能为空");
                return;
            }
            if (string.IsNullOrEmpty(realName))
            {
                Response.Write("请填写真实姓名");
                return;
            }            
        }

    }
}