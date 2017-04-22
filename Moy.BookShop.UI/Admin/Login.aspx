<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="Login.aspx.cs" Inherits="Moy.BookShop.UI.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>第三波书店"-网上书店</title>
    <link href="/Admin/css/admin.css" rel="stylesheet" />
</head>
<body>
    <div class="login_t">
    </div>
    <div class="login_m">
    </div>
    <div class="login_b">
        <form id="form1" name="form1" method="post" action="/Admin/Login.aspx">
            <input type="hidden" name="url" value="<%: Request.QueryString["returnUrl"] %>" />
            <p>
                <label>
                    用户名</label><input type="text" name="LoginId" class="login_input" />
            </p>
            <p>
                <label>
                    密&#160;&#160;码</label><input type="password" name="LoginPwd" class="login_input" />
            </p>
            <p>
                <input type="submit" name="btnLogin" value="登陆" class="login_sub" />
                <input type="reset" name="btnReset" value="重置" class="login_sub login_reset" /><p>
        </form>
        <span style="color: red; font-size: 16px;"><%: this.errMsg %></span>
    </div>
</body>
</html>
