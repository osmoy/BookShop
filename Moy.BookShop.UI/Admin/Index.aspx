<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master/AdminCommon.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Moy.BookShop.UI.Admin.Index" %>

<%@ Import Namespace="Moy.BookShop.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    管理后台
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%:ResolveUrl("~/libs/css/pageBar.css") %>" rel="stylesheet" />
    <link href="../libs/thired/jQuaryEasyUI/tableStyle.css" rel="stylesheet" />
    <link href="../libs/thired/jQuaryEasyUI/icon.css" rel="stylesheet" />
    <link href="../libs/thired/jQuaryEasyUI/easyui.css" rel="stylesheet" />
    <script src="../libs/thired/jQuaryEasyUI/jquery.easyui.min.js"></script>
    <script src="../libs/thired/datapattern2.js"></script>
    <script src="../libs/thired/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="breadcrumb" class="black">您现在的位置：  <a href="#">第三波</a>  >    <a href="#">管理员后台</a>  >    <a href="#">图书管理</a></div>

    <p style="text-align: center; font-size: 16px;"><a href="javascript:;" id="addInfo">添加图书</a></p>

    <table width="96%" border="0" cellspacing="0" cellpadding="0" align="center" class="data_table">
        <thead>
            <tr>
                <th width="10%">图书预览</th>
                <th width="25%">书名</th>
                <th width="15%">作者</th>
                <th width="10%">类别</th>
                <th width="15%">出版社</th>
                <th width="15%">出版时间</th>
                <th width="5%">编辑</th>
                <th width="5%">删除</th>
            </tr>
        </thead>
        <tbody id="data_body">
            <!-- 遍历. -->
            <% foreach (var book in books)
               { %>
            <tr>
                <td>
                    <img src="<%:ResolveUrl(CommonHelper.GetDefault(string.Format("/libs/Images/BookCovers/{0}.jpg", book.ISBN))) %>" width="80" height="80" /></td>
                <td class="name"><%: book.Title %></td>
                <td><%: book.Author %></td>
                <td><%: book.Category.Name %></td>
                <td><%: book.Publisher.Name %></td>
                <td><%: book.PublishDate.ToString("yyyy-MM-dd") %></td>
                <%--<td><a href="/Admin/ashx/ProcessBook.ashx?Action=edit&bid=<%:book.Id %>">编辑</a></td>--%>
                <td><a class="detail" href="javascript:;" bid="<%:book.Id %>">编辑</a></td>
                <%--<td><a class="del" href="/Admin/ashx/ProcessBook.ashx?Action=delete&bid=<%:book.Id %>">删除</a></td>--%>
                <td><a class="del" href="javascript:;" bid="<%:book.Id %>">删除</a></td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p class="page_nav"><%: new HtmlString(this.htmlLink) %></p>
    <!-- 编辑信息 -->
    <div id="div1">
        <form>
            <table class="caption" id="tb1">
                <tr>
                    <td>书名：</td>
                    <td>
                        <input type="text" name="title" id="title" value="" /></td>
                </tr>
                <tr>
                    <td>作者：</td>
                    <td>
                        <input type="text" name="author" id="author" value="" /></td>
                </tr>
                <tr>
                    <td>类别：</td>
                    <td>
                        <select name="category" id="category">
                            <% foreach (var c in categories)
                               { %>
                            <option value="<%:c.Id %>"><%:c.Name %></option>
                            <% } %>
                        </select></td>
                </tr>
                <tr>
                    <td>出版社：</td>
                    <td>
                        <select name="publish" id="publish">
                            <% foreach (var p in publishes)
                               { %>
                            <option value="<%:p.Id %>"><%:p.Name %></option>
                            <% } %>
                        </select></td>
                </tr>
                <tr>
                    <td>出版时间：</td>
                    <td>
                        <input type="text" name="pubDate" id="pubDate" /></td>
                </tr>
                <tr>
                    <td>价格：</td>
                    <td>
                        <input type="text" name="price" id="price" /></td>
                </tr>
            </table>
        </form>
    </div>

    <!-- 添加信息 iframe来做. -->
    <div id="divAdd" style="overflow: hidden;">
        <iframe id="ifram1" framborder="0" width="100%" height="100%"></iframe>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#div1').css('display', 'none');
            $('#divAdd').css('display', 'none');
            $('.del').click(function () {
                var bid = $(this).attr('bid');
                deleteEvent(bid, $(this));
            })
            $('.detail').click(function () {
                var id = $(this).attr('bid');
                showInfo(id);
            })
            $('#addInfo').click(function () {
                addBook();
            })
            $('#pubDate').click(function () {
                new WdatePicker({ 'skin': 'whyGreen' });    //日历控件..
            })

        })

        function deleteEvent(bid, control) {
            $.messager.confirm('提示', '确定要删除此图书？',function (r) {
                if (r) {
                    $.post('/Admin/ashx/ProcessBook.ashx', { Action: 'delete', Bid: bid }, function (data) {
                        if (data == 'ok') {
                            control.parent().parent().remove();
                            showMsg('信息', '删除成功', 5000, 'slide');
                        } else {
                            showMsg('信息', '删除失败', 5000, 'fade');
                        }
                    });
                }
            })
        }

        function showMsg(t, m, time, type) {
            $.messager.show({
                title: t,
                msg: m,
                timeout: time,
                showType: type
            });
        }

        function showInfo(bid) {
            $.post('/Admin/ashx/ProcessBook.ashx', { Action: 'edit', Bid: bid }, function (resData) {
                var data = JSON.parse(resData);

                $('#title').val(data.Title);
                $('#author').val(data.Author);
                $('#category').val(data.Category.Id);
                $('#publish').val(data.Publisher.Id);
                $('#price').val(data.UnitPrice);
                $('#pubDate').val((eval(data.PublishDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-MM-dd"));
            });
            $('#div1').css('display', 'block');
            $('#div1').dialog({
                modal: true,
                resizable: true,
                maximizable: true,
                collapsible: true,
                tilte: '图书详情',
                width: 400,
                height: 330,
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-ok',
                    handler: function () {
                        var formData = $('#div1 form').serialize();
                        //alert('ok');
                        $.post('/Admin/ashx/ProcessBook.ashx?Action=modify&bid=' + bid, formData, function (data) {
                            if (data == 'yes') {
                                showMsg('信息', '修改成功', 5000, 'slide');
                                $('#div1').dialog('close');
                            } else {
                                showMsg('信息', '修改失败', 5000, 'fade');
                            }
                        });
                    }
                }, {
                    text: '取消',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        //alert('cancel');;
                        $('#div1').dialog('close')
                    }
                }]
            })
        }

        function addBook() {
            $('#ifram1').attr('src', '/Admin/AddBook.aspx')
            $('#divAdd').css('display', 'block');   
            $('#divAdd').dialog({
                modal: true,
                resizable: true,
                maximizable: true,
                collapsible: true,
                tilte: '添加图书',
                width: 720,
                height: 520,
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-ok',
                    handler: function () {
                        
                        var childWindow = $('#ifram1')[0].contentWindow;
                        childWindow.subForm();
                        ////动态添加表格
                        //var tr = $('<tr>');
                        //tr.append('<td>' + new Date().getDay() + '</td>');
                        //tr.append('<td>' + childWindow.$('#title').val() + '</td>');
                        //tr.append('<td>' + childWindow.$('#author').val() + '</td>');
                        //tr.append('<td><a class="detail" href="javascript:;" bid="id">编辑</a></td>');
                        //tr.append('<td><a class="del" href="javascript:;" bid="id">删除</a></td>');
                        //tr.append('</tr>');
                        //if ($('#data_body').first()) {
                        //    tr.insertBefore($('#data_body').first());
                        //} else {
                        //    tr.appendTo($('#data_body'));
                        //}
                    }
                }, {
                    text: '取消',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        //alert('cancel');;
                        $('#divAdd').dialog('close')
                    }
                }]
            })
        }

        function onSuccess() {
            $('#divAdd').dialog('close');
        }

    </script>
</asp:Content>
