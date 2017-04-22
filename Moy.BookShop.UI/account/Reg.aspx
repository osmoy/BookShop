<%@ Page Title="" Language="C#" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="Moy.BookShop.UI.account.Reg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    注册页面
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
    <link href="<%:ResolveUrl("~/libs/css/member.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div id="action_area">
        <h2 class="action_type">
            <img src="<%: ResolveUrl("~/libs/Images/register.gif") %>" alt="会员注册" /></h2>
        <form action="/account/reg.aspx" method="post" class="member_form">
            <input type="hidden" name="returnUrl" value="<%:Request.QueryString["returnUrl"] %>" />
            <p>
                <label><span>*</span>用户名</label><input name="username" type="text" class="opt_input" />5-12个字符或数字组成，可用中文名
            </p>
            <p>
                <label><span>*</span>密&#160;&#160;&#160;&#160;码</label><input name="password" type="password" class="opt_input" />请输入密码
            </p>
            <p>
                <label><span>*</span>确认密码</label><input name="rePwd" type="password" class="opt_input" />请再次输入密码
            </p>
             <p>
                <label><span>*</span>真实姓名</label><input name="realname" type="text" class="opt_input" />
            </p>
            <p>
                <label><span>*</span>地址</label><input name="address" type="text" class="opt_input" />
            </p>
             <p>
                <label><span>*</span>电话</label><input name="tel" type="text" class="opt_input" />
            </p>
             <p>
                <label><span>*</span>邮箱</label><input name="email" type="text" class="opt_input" />请输入邮箱
            </p>
             <p>
                <label>生日</label><input id="birthday" name="birthday" type="text" class="opt_input Wdate" />
            </p>
            <p>
                <label><span>*</span>验证码</label><input name="vCode" type="text" class="opt_input" style="width:80px;" />
                <img id="img" src="/ashx/Vcode.ashx" title="看不清，换一张" alt="看不清，换一张" style="cursor:pointer;"/>
                请输入验证码
            </p>
            <%--<p class="form_sub">
                <input type="checkbox" name="remember" checked="checked" />
                在此计算机上保留我的密码
            </p>--%>
            <p class="form_sub">
                <input type="submit" name="save" value="确定了，马上提交" class="opt_sub" /> <span style="color:red;"><%: this.msg %></span>
            </p>
            <p class="form_sub">加<span>*</span>的为必填项目</p>
            <p class="form_sub">
                ><a href="login.html">已经有账号，马上登录</a><br />
                >如果你已经有“第三波书店”社区账号，请<a href="javascript:alert('书店社区暂未开通');">点这里</a>登录升级
            </p>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="<%:ResolveUrl("~/libs/thired/DatePicker/WdatePicker.js") %>"></script>
    <script type="text/javascript">
        $('#birthday').click(function () {
            new WdatePicker({ 'skin': 'whyGreen' });    //创建WdatePicker对象..
        })

        $('#img').click(function () {            
            $('#img').attr('src', this.src + "?time=" + new Date());
        })

    </script>
</asp:Content>
