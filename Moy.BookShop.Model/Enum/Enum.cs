using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model.Enum
{
    public enum LoginResult
    {
        用户名不存在 = 0,
        密码错误 = 1,
        用户已被冻结 = 2,
        登录成功 = 3
    }

    public enum RegisterResult
    {
        用户名已存在 = 0,
        注册失败 = 1,
        注册成功 = 2,
    }

    public enum SortType
    {
        //SalesDesc = 0,
        PriceAsc = 1,
        PriceDesc = 2,
        //CommentDesc = 3,
        OnsaleDesc = 4
    }

    public enum VideoStatus
    {
        上传中 = 1,
        上传成功 = 2,
        转码成功 = 3,
        回传成功 = 4
    }
}