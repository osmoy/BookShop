<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="AddBook.aspx.cs" Inherits="Moy.BookShop.UI.Admin.AddBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加图书</title>
    <link href="../libs/thired/jQuaryEasyUI/tableStyle.css" rel="stylesheet" />
    <link href="../libs/thired/jQuaryEasyUI/easyui.css" rel="stylesheet" />
    <script src="../libs/js/jquery-1.8.2.min.js"></script>
    <script src="../libs/thired/ckeditor/ckeditor.js"></script>
    <script src="../libs/thired/jQuaryEasyUI/jquery.easyui.min.js"></script>
    <script src="/Admin/js/MyAjaxForm.js"></script>
    <script src="../libs/thired/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            CKEDITOR.replace('desc');
            $('#publishDate').click(function () {
                new WdatePicker({ 'skin': 'whyGreen' });
            })
            $('#upload').click(function () {
                uploadFile();
            })
        })

        function uploadFile() {
            if ($('#file').val() == '') {
                $.messager.alert('提示', '请选择上传图片', 'info');
            } else {
                $('#form1').ajaxSubmit({
                    type: 'post',
                    url: '/Admin/ashx/ProcessUpload.ashx',
                    success: function (data) {
                        if (data == 'empty') {
                            $.messager.alert('提示', '请选择上传图片', 'info');
                        } else if (data == 'error') {
                            $.messager.alert('提示', '上传图片格式有误', 'warning');
                        } else {
                            //$('#imgPath').val(data)
                            var d = data.split(':');
                            $('#imgPath').val(d[1]);
                            var img = $('<img src="' + d[0] + '" width="50px" height="50px"/>');
                            $('#showImg').append(img);
                        }
                    }
                });
            }
        }

        function subForm() {
            var stemTxt = CKEDITOR.instances.desc.document.getBody().getText();
            $('#content').val(stemTxt); //

            $('#form1').ajaxSubmit({
                type: 'post',
                url: '/Admin/ashx/ProcessBook.ashx?Action=add',
                success: function (data) {
                    if (data == 'ok') {
                        $.messager.alert('提示', '添加成功', 'info');

                        setTimeout(closeWindw, 3000);
                    } else {
                        alert("添加失败");
                    }
                }
            });
        }

        function closeWindw() {
            window.parent.onSuccess();
        }
    </script>
</head>
<body>
    <form id="form1">
        <table class="caption">
            <tr>
                <td>标题</td>
                <td>
                    <input type="text" name="title" id="title" /></td>
            </tr>
            <tr>
                <td>作者</td>
                <td>
                    <input type="text" name="author" id="author" /></td>
            </tr>
            <%--禁止访问..--%>
            <tr>
                <td>出版社</td>
                <td>
                    <select name="publish">
                        <%foreach (var p in publish)
                          {%>
                        <option value="<%: p.Id %>"><%: p.Name %></option>
                        <% } %>
                    </select></td>
            </tr>
            <tr>
                <td>分类</td>
                <td>
                    <select name="category">
                        <%foreach (var c in categories)
                          {%>
                        <option value="<%: c.Id %>"><%: c.Name %></option>
                        <% } %>
                    </select></td>
            </tr>
            <tr>
                <td>出版日期</td>
                <td>
                    <input id="publishDate" type="text" name="publishDate" /></td>
            </tr>
            <tr>
                <td>上传图片</td>
                <td>
                    <input type="file" name="fileUpload" id="file" />
                    <input type="button" id="upload" name="uploadImg" value="上传图片" />
                    <div id="showImg"></div>
                </td>
            </tr>
            <tr>
                <td>单价</td>
                <td>
                    <input type="text" name="unitePrice" /></td>
            </tr>
            <tr>
                <td>内容描述</td>
                <td>
                    <textarea id="desc"></textarea></td>
            </tr>
        </table>
        <input type="hidden" name="description" id="content" />
        <input type="hidden" name="imgPath" id="imgPath" />
    </form>
</body>
</html>
