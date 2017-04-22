<%@ Page Title="" Language="C#" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Moy.BookShop.UI.account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    登陆
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
    <link href="<%: ResolveUrl("~/libs/css/member.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="action_area">
        <h2 class="action_type">
            <img src="<%: ResolveUrl("~/libs/Images/login_in.gif") %>" alt="会员登录" /></h2>
        <p class="state">
            欢迎光临第三波书店网站，本站为淘宝网旗下专业在线书店！<br />
            您可以使用第三波书店的用户名，直接登录。
        </p>
        <form action="/account/Login.aspx" method="post" class="member_form">
            <input type="hidden" name="returnUrl" value="<%:Request.QueryString["returnUrl"] %>" />
            <p>
                <label>用户名</label><input name="username" type="text" class="opt_input" value="<%:this.userName %>" />
            </p>
            <p>
                <label>密&#160;&#160;&#160;&#160;码</label><input name="password" type="password" class="opt_input" value="<%:this.pwd %>" />
            </p>
            <p>
                <label>验证码</label><input name="vCode" type="text" class="opt_input" />
                <img id="img" src="/ashx/VCode.ashx" alt="看不清，换一张" title="看不清，换一张" style="cursor:pointer;" />
            </p>
            <p class="form_sub">
                <input type="checkbox" name="rememberme" />
                在此计算机上保留我的密码
                <span><%:this.msg %></span>
            </p>
            <p class="form_sub">
                <input type="submit" name="login" value="登陆" />
                <%--<input type="image" name="login" src="<%: ResolveUrl("~/libs/Images/az-login-gold-3d.gif") %>" />--%>
                <% if (string.IsNullOrEmpty(Request.QueryString["returnUrl"])) { %>
                    <a href="/account/Reg.aspx">立即注册</a>
                <% }else{ %>
                    <a href="/account/Reg.aspx?returnUrl=<%:Request.QueryString["returnUrl"] %>">立即注册</a>
                <% } %>
                <a href="#">忘记密码？</a>
                <input type="hidden" name="<%:Request.QueryString["returnUrl"] %>" />
            </p>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="<%:ResolveUrl("~/libs/js/jquery-1.8.2.min.js") %>"></script>
    <script type="text/javascript">
        $('#img').click(function () {
            var url = $('#img').attr('src');
            $('#img').attr('src', url + '?time=' + new Date());
        })
    </script>
</asp:Content>