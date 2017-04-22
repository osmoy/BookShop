<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMsg.aspx.cs" Inherits="Moy.BookShop.UI.ShowMsg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .style1 {
            text-align: center;
        }
    </style>
    <script>
        window.onload = function () {
            setTimeout(change, 1000);
        }
        function change() {
            var oA = document.getElementById('url');
            var oSpan = document.getElementById('count');

            time = parseInt(oSpan.innerHTML);
            time--;
            if (time < 1) {
                window.location = oA.href;
            } else {
                oSpan.innerHTML = time;
                setTimeout(change, 1000);   //递归..
            }
        }
    </script>
</head>
<body>
    <div>
        <table width="490" height="325" border="0" align="center" cellpadding="0" cellspacing="0" background="libs/Images/showinfo.png">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="50">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td width="40">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="50">&nbsp;</td>
                            <td style="text-align: center; font-size: 20px;"><%:this.msg %></td>
                            <td width="40">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="50">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td width="40">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="50" class="style1">&nbsp;</td>
                            <td style="text-align: center"><span id="count" style="color: red;">5</span>秒后自动返回<a id="url" href="<%: this.url %>"><%:this.text %></a></td>
                            <td width="40">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>