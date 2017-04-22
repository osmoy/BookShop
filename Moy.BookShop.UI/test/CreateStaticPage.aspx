<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateStaticPage.aspx.cs" Inherits="Moy.BookShop.UI.test.CreateStaticPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Button id="btnCreate" runat="server" Text="生成静态页" OnClick="btnCreate_Click"></asp:Button>

        <asp:Button ID="btnConvert" runat="server" Text="开始转换视频" OnClick="btnConvert_Click" />

    </form>
</body>
</html>