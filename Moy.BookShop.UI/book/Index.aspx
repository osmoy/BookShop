<%@ Page Title="" Language="C#" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Moy.BookShop.UI.book.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    "第三波书店"-网上书店
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
    <link href="<%: ResolveUrl("~/libs/css/Index.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div id="magic">
            <%--<img src="<%=Url.Content("~/images/a_b_02.jpg") %>" alt="幻灯图片" width="480" height="200" />--%>
            <ul>
                <li><a href="#">
                    <img src="/libs/Images/Common/1.jpg" alt="大国游戏" title="大国游戏" /></a></li>
                <li><a href="#">
                    <img src="/libs/Images/Common/2.jpg" alt="开学季" title="开学季" /></a></li>
                <li><a href="#">
                    <img src="/libs/Images/Common/3.jpg" alt="推荐好书" title="推荐好书" /></a></li>
                <li><a href="#">
                    <img src="/libs/Images/Common/4.jpg" alt="逻辑思维" title="逻辑思维" /></a></li>
            </ul>
            <ol>
                <li><span class="active"></span></li>
                <li><span></span></li>
                <li><span></span></li>
                <li><span></span></li>
            </ol>
            <a href="javasctipt:;"></a>
            <a href="javasctipt:;"></a>
        </div>
        <div id="a_b_02">
            <a href="#">电子词典专柜上线</a>
            <a href="#">Lucy陪你说真人口语英语对译软件</a>
            <a href="#" class="red">哇~这东西真便宜，大家快来抢啊！</a>
        </div>
        <!--comment books start-->
        <div id="comment_book">
            <ul>
                <% foreach (var book in books)
                   {%>
                <li><a href="/book/BookDetail.aspx?bid=<%:book.Id %>">
                    <img src="<%: string.Format("/libs/Images/BookCovers/{0}.jpg", book.ISBN) %>" alt="<%:book.Title %>" title="<%:book.Title %>" /></a>
                    <a href="/book/BookDetail.aspx?bid=<%:book.Id %>"><%:book.Author.Substring(0, 5) %></a></li>
                <% } %>
            </ul>
        </div>
        <!--comment books end-->
    </div>

    <!--sidebar content-->
    <div id="sidebar">
        <ul id="notice">
            <li><a href="#">国庆期间货物延期配送公告</a></li>
            <li><a href="#">英语高级口语资格考试</a></li>
            <li><a href="#">英语高级口语资格考试</a></li>
            <li><a href="#">英语高级口语资格考试</a></li>
        </ul>
        <div id="order_find">
            <form action="" method="post" target="_blank">
                <label>订单号：</label><input type="text" id="Text1" class="order_key" />
                <input type="submit" id="Submit1" class="order_sub" value="查询状态" />
            </form>
        </div>
        <div class="service">
            <p>
                <a href="#">
                    <img src="<%: ResolveUrl("~/libs/Images/QQ_01.gif") %>" /></a>
                <a href="#">
                    <img src="<%: ResolveUrl("~/libs/Images/QQ_02.gif") %>" /></a>
                <a href="#">
                    <img src="<%: ResolveUrl("~/libs/Images/QQ_02.gif") %>" /></a>
            </p>
            <p>
                <a href="#">
                    <img src="<%: ResolveUrl("~/libs/Images/taobao_01.gif") %>" /></a>
                <a href="#">
                    <img src="<%: ResolveUrl("~/libs/Images/taobao_02.gif") %>" /></a>
            </p>
        </div>
        <!--hot books start-->
        <div class="sidedt hots">
            <h1>热销排行</h1>
            <ul>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
            </ul>
        </div>
        <!--hot books end-->
        <!--laster books start-->
        <div class="sidedt laster">
            <h1>热销排行</h1>
            <ul>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
                <li><a href="#">英语高级口语资格考试</a></li>
            </ul>
        </div>
        <!--laster books end-->
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var oDiv = document.getElementById('magic');
        var oUl = oDiv.getElementsByTagName('ul')[0];
        var aLi = oUl.getElementsByTagName('li');
        var oOl = oDiv.getElementsByTagName('ol')[0];
        var aSpan = oOl.getElementsByTagName('span');
        var oneWidth = aLi[0].offsetWidth;
        var iNow = 0;
        var timer = null;

        oUl.style.width = oneWidth * aLi.length + 'px';
        for (var i = 0; i < aSpan.length; i++) {
            aSpan[i].index = i;
            aSpan[i].onmouseover = function () {
                iNow = this.index;
                change();
            }
        }

        oDiv.onmousemove = function () {
            clearInterval(timer);
        }
        oDiv.onmouseout = function () {
            timer = setInterval(autoRun, 3000);
        }

        timer = setInterval(autoRun, 3000);

        //auto run
        function autoRun() {
            iNow++;
            if (iNow == aLi.length) {
                iNow = 0;
            }
            change();
        }

        function change() {
            for (var i = 0; i < aSpan.length; i++) {
                aSpan[i].className = "";
                aLi[i].style.filter = 'alpha(opacity=0)';
                aLi[i].style.opacity = 0;
            }
            aSpan[iNow].className = 'active';
            aLi[iNow].style.opacity = 100;
            oUl.style.left = -iNow * oneWidth + 'px';
        }
    </script>
</asp:Content>
