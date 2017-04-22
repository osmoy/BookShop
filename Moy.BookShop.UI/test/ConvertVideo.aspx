<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertVideo.aspx.cs" Inherits="Moy.BookShop.UI.test.ConvertVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>模拟视频转码</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload" runat="server" /><br />
        标题<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnUpload" runat="server" Text="点击上传" OnClick="btnUpload_Click"/>
    </div>
    </form>
</body>
</html>
