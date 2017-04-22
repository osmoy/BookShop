<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPageData.aspx.cs" Inherits="Moy.BookShop.UI.test.GetPageData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>获取网页内容数据</title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Button ID="btnGo" runat="server" Text="WebClient抓取" OnClick="btnGo_Click" />


        <asp:Button ID="btnGo2" runat="server" Text="HttpWebRequest抓取" OnClick="btnGo2_Click" />
             
        <asp:TextBox ID="txtURL" runat="server"></asp:TextBox>

    </form>        
</body>
</html>
