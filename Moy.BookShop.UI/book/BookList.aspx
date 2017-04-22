<%@ Page Title="" Language="C#" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="Moy.BookShop.UI.book.BookList" %>

<%@ Import Namespace="Moy.BookShop.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    图书列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
    <link href="<%:ResolveUrl("~/libs/css/channel.css") %>" rel="stylesheet" />
    <link href="<%: ResolveUrl("~/libs/css/pageBar.css") %>" rel="stylesheet" />    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main">
        <div class="list_asc">
            <!--choice order type-->
            <div class="type_choice f_left">
                排序方式
                <select name="list_type">
                    <option>按销量 排序</option>
                    <option>按价格 排序</option>
                    <option>按评论 排序</option>
                </select>
            </div>
        </div>

        <%
            if (bookList != null && bookList.Count() > 0)
            {
                foreach (var book in bookList)
                {%>
        <dl class="list_area">
            <dt><a href="/book/BookDetail.aspx?bid=<%:book.Id %>">
                <img src="<%:ResolveUrl(CommonHelper.GetDefault(string.Format("~/libs/Images/BookCovers/{0}.jpg", book.ISBN))) %>" width="100" height="100" alt="<%:book.Title %>" /></a></dt>
            <dd>
                <h2 class="b_title"><a href="/staticPage/<%:book.PublishDate.ToString("yyyy-MM-dd")+"/"+ book.ISBN %>.html"><%:book.Title %></a></h2>
                <!--将书籍的id 写入span-->
                <div class="b_score">顾客评分：<span id="book_id_15">0</span></div>
                <div class="b_property">
                    作者：<%:book.Author %><br />
                    出版社：<%:book.Publisher.Name %><br />
                    出版时间：<%:book.PublishDate %>
                </div>
                <h4 class="b_intro"><%:this.CutString(book.ContentDescription, 150) %></h4>
                <div class="b_buy">
                    <span class="red">价格：￥<%:string.Format("{0:f2}", book.UnitPrice) %></span>
                    <a href="/book/ShoppingCart.aspx?bid=<%:book.Id %>">
                        <img src="/libs/Images/btn_goumai.gif" onmouseover="this.src='/libs/Images/btn_goumai_click.gif'" onmouseout="this.src='/libs/Images/btn_goumai.gif'" /></a>
                    <img src="/libs/Images/btn_zancun.gif" onmouseover="this.src='/libs/Images/btn_zancun_click.gif'" onmouseout="this.src='/libs/Images/btn_zancun.gif'" />
                </div>
            </dd>
        </dl>

        <% }
            } %>

        <div class="page_nav">
            <%: new HtmlString(strHtml)%>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var book_list = new Array("15", "16", "17", "18");    //此处将该页需要显示的 书籍id 写入该数组；

        for (var i = 0; i < book_list.length; i++) {
            var book_score_str = "";
            var book_score = parseInt($("book_id_" + book_list[i]).innerHTML);
            for (var m = 0; m < book_score; m++) {
                book_score_str += "<img src='/libs/images/star_red.gif' />";
            }
            if (book_score == 0) book_score_str = "暂无评价";
            $("book_id_" + book_list[i]).innerHTML = book_score_str;
        }
    </script>
</asp:Content>
