<%@ Page Title="" Language="C#" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Moy.BookShop.UI.book.ShoppingCart" %>
<%@ Import Namespace="Moy.BookShop.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    购物车
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
    <link href="<%:ResolveUrl("~/libs/css/member.css") %>" rel="stylesheet" />
    <link href="<%:ResolveUrl("~/libs/thired/jQueryUI/jquery-ui-1.8.2.custom.css") %>" rel="stylesheet" />
    <script type="text/javascript" src="<%:ResolveUrl("~/libs/thired/jQueryUI/jquery-ui-1.8.2.custom.min.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div id="action_area">
        <h2 class="action_type backet">
            <p class="imp_link">
                全场运费一律2元  <a href="#"><strong>简易计算器</strong></a><br />
                <p class="mark">确认商品价格与交易条件</p>
        </h2>
        <table width="96%" border="0" cellspacing="3" cellpadding="0" align="center" class="data_table">
            <thead>
                <tr>
                    <th scope="col">图片</th>
                    <th scope="col">商品名称</th>
                    <th scope="col" width="10%">单价</th>
                    <th scope="col" width="15%">购买数量</th>
                    <th scope="col" width="10%">删除</th>
                </tr>
            </thead>
            <tbody id="basket_data">
                <% foreach (var c in cartList) {%>
                <tr class="align">
                    <td style='padding: 5px 0 5px 0;'>
                        <img src='<%:ResolveUrl(CommonHelper.GetDefault(string.Format("/libs/Images/bookcovers/{0}.jpg", c.Book.ISBN))) %>' width="50" height="50" border="0" /></td>
                    <td class="name"><%: c.Book.Title %></td>
                    <td>￥<span class="price" style="color: red;"><%:string.Format("{0:f2}", c.Book.UnitPrice) %></span></td>
                    <td><a href='javascript:;' onclick="changeCount('-',<%:c.Book.Id %>,<%:c.Id %>)" title='减一' style='margin-right: 2px;'>
                        <img alt="减" src="/libs/Images/bag_close.gif" width="9" height="9" style='display: inline; border: none' /></a>
                        <input type='text' id="txtCount<%:c.Book.Id%>" maxlength='3' style='width: 20px' onkeydown='if(event.keyCode == 13) event.returnValue = false' value="<%:c.Quantit %>" />
                        <a href='javascript:;' onclick="changeCount('+',<%:c.Book.Id %>,<%:c.Id %>)" title='加一' style='margin-left: 2px;'>
                            <img alt="加" src='/libs/Images/bag_open.gif' width="9" height="9" style='display: inline; border: none' /></a>
                    </td>
                    <%--<td><a href="javascript:;" onclick="removeItem(<%:c.Id %>,this)">删除</a></td>--%>
                    <td><a cid="<%:c.Id %>" href="javascript:;" class="delete">删除</a></td>
                </tr>
                <% } %>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="6">商品金额总计：<span id="money" style="color: red;">12</span></td>
                </tr>
            </tfoot>
        </table>
        <div style="text-align: center;">
            <a href="booklist.aspx">
                <img alt="" src="/libs/Images/gobuy.jpg" width="103" height="36" border="0" /></a>
            <a href="OrderConfirm.aspx">
                <img src="/libs/Images/balance.gif" border="0" /></a>
        </div>
                
        <div id="dialog-message" title="warnning">
            <p id="msg" style="color: red;"></p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        $(function () {

            showMoney();

            $('.align input').blur(function () {
                showMoney();
            })

            var del = document.getElementsByClassName('delete');
            for (var i = 0; i < del.length; i++) {
                del[i].onclick = function () {
                    var cid = $(this).attr('cid');
                    var link = $(this);
                    deleteEvent(cid, link);
                }
            }

        })        

        function deleteEvent(cid, link) {
            if (confirm('确定要删除此条记录？')) {
                $.post('/ashx/ProcessCart.ashx', { Action: 'delete', CartId: cid }, function (data) {
                    if (data == 'yes') {
                        link.parent().parent().remove();

                        showMoney();
                    }
                })
            }
        }

        function changeCount(op, bid, cid) {
            var count = parseInt($('#txtCount' + bid).val());
            if (op == '+') {
                count++;
            } else {
                if (count <= 1) {
                    $("#msg").text('确定要删除此条记录？');
                    showDialog(cid);//调用弹层
                    return;
                }
                count--;
            }
            $.post('/ashx/ProcessCart.ashx', { Action: 'modify', Count: count, CartId: cid }, function (resData) {
                if (resData == 'ok') {
                    $("#txtCount" + bid).val(count); 
                    showMoney();
                }
            })
        }

        function showDialog(cid) {
            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog('close');                        
                        /* $.post('/ashx/ProcessCart.ashx', { Action: 'delete', CartId: cid }, function (data) {
                            if (data == 'yes') {
                                alert('删除成功');
                            }
                        }); */
                    }
                }
            });
        }

        function showMoney() {
            var totalMoney = 0;
            $('.align').each(function () {
                var price = $(this).find('.price').text();
                var count = $(this).find('input').val();
                totalMoney = totalMoney + (parseInt(count) * parseFloat(price));
            })
            $('#money').text(totalMoney.toFixed(2));
        }

    </script>
</asp:Content>