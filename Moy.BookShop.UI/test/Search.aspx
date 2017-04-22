<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="Search.aspx.cs" Inherits="Moy.BookShop.UI.test.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>实现站内搜索Demo</title>
    <link href="/libs/thired/jQueryUI/jquery-ui-1.8.2.custom.css" rel="stylesheet" />
    <script src="/libs/js/jquery-1.8.2.min.js"></script>
    <script src="/libs/thired/jQueryUI/jquery-ui-1.8.2.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#SearchContent").autocomplete({
                source: '/ashx/AutoComplete.ashx'
            });
        });
	</script>

</head>
<body>
    <form method="get" action="">
        请输入搜索内容：<input type="text" name="content" id="SearchContent" />
        <input type="submit" value="搜索" name="btnSearch" />
        <input type="submit" value="创建索引" name="btnCreate" />

    </form>

    <ul>
        <% if (list != null && list.Count > 0)
           {
               foreach (var item in list)
               { %>
        <li><a href="<%: item.Url %>"><%: item.Title %></a></li>
        <li><%: new HtmlString(item.Msg) %></li>
        <% }
           } %>
    </ul>

</body>
</html>
