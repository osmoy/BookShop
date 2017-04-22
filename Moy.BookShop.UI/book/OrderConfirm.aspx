<%@ Page Title="" Language="C#" EnableViewState="false" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="OrderConfirm.aspx.cs" Inherits="Moy.BookShop.UI.book.OrderConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    确认订单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; left: 30px;">
        <form action="/book/OrderConfirm.aspx" method="post">
            <div style="-background: #AACDED; text-align: left">
                <h2>填写核对订单信息</h2>
            </div>
            <div align="left" style="margin-top: 10px;">
                <h2>收货人信息:</h2>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <td style="WIDTH: 100px" align="right">姓名：</td>
                                <td style="text-align: left">
                                    <input type="text" name="txtName" size="50" value="<%: user.Name %>" /><img src="/libs/Images/cha.ico" style="display: none" width="15" height="15" /></td>
                            </tr>
                            <tr>
                                <td align="right">地址：</td>
                                <td style="text-align: left">
                                    <input type="text" name="txtAddress" size="50" value="<%:user.Address%>" /><img src="/libs/Images/cha.ico" style="display: none" width="15" height="15" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">电话：</td>
                                <td style="text-align: left">
                                    <input type="text" name="txtPhone" size="50" value="<%: user.Phone %>" /><img src="/libs/Images/cha.ico" style="display: none" width="15" height="15" /></td>
                            </tr>
                            <tr>
                                <td align="right">邮编：</td>
                                <td style="text-align: left">
                                    <input type="text" name="txtPostCode" size="50" value="<%: user.Mail %>" /><img src="/libs/Images/cha.ico" style="display: none" width="15" height="15" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <hr style="border-style: dashed; border-color: #ccc" />
                <div style="margin-top: 10px;">
                    <h2>请选择支付方式:</h2>
                    <div>
                        <!--支付方式-->
                        <table width="70%">
                            <tbody>
                                <tr valign="middle">
                                    <td style="TEXT-ALIGN: right; WIDTH: 80px">支付方式：</td>
                                    <td style="vertical-align: middle; text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt="" src="/libs/Images/y_zfb.gif" /><input name="zfPay" type="radio" value="zfb" checked="checked" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                                            alt="" src="/libs/Images/unionpay.gif" /><input name="zfPay"
                                                type="radio" value="wyzx" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!--支付方式-->
                </div>
                <hr style="border-style: dashed; border-color: #ccc" />
                <div align="left" style="margin-top: 10px;">
                    <!--订单确定-->
                    <h2>商品清单:</h2>
                    <div>
                        <table cellspacing="0" cellpadding="1" border="1">
                            <tbody>
                                <tr class="align_Center Thead">
                                    <td width="10%">图书编号</td>
                                    <td>商品名称</td>
                                    <td width="10%">单价</td>
                                    <td width="8%">数量</td>
                                </tr>
                                <!-- 再次展示商品 -->
                                <% foreach (var c in carts)
                                   { %>
                                <tr class="align_Center">
                                    <td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 5px"><%:c.Book.ISBN %></td>
                                    <td style="width: 28%;" class="align_Left"><a href='/book/BookDetail.aspx?bid=<%:c.Book.Id %>' target="_blank"><%: c.Book.Title %></a>
                                    </td>
                                    <td>￥<span style="color: red;"><%:string.Format("{0:f2}", c.Book.UnitPrice) %></span></td>
                                    <td><%: c.Quantit %></td>
                                </tr>
                                <% } %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <!--订单确定-->
                <div align="right" style="margin: 10px 20px 0 10px;">
                    <!--总价格显示-->
                    <h2>你需要支付的总价格为:<span class="price">￥<%: this.money.ToString("0.000")%></span></h2>
                    <br />
                    <input id="btnGoPay" type="submit" name="settlement" value="" style="background: url('/libs/Images/basker_ok.gif') no-repeat center; width: 133px; height: 43px; border: none; outline: none; cursor:pointer;" />
                    <span id="spanMsg" style="color:Red"></span>
                </div>
            </div>
        </form>
    </div>
    <input name="hdType" type="hidden" value="0" id="hdType" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".m_r").css("width", "710px");
            $(".m_r").css("float", "none");
        });
        function OnSubmit() {
            Validate();
            if (isValid == false) {
                $("#spanMsg").text("请检查收货信息是否填写完整!");
                return false;
            }
            $("#hdType").val("1");
        }

        var isValid = true;
        function Validate() {
            isValid = true;
            $("#userinfo input[type=text]").each(
               function () {
                   if ($(this).val().length <= 0) {
                       isValid = false;
                       $($(this).next()).attr("style", "display:inline");
                   }
               }
            );
        }
    </script>
</asp:Content>
