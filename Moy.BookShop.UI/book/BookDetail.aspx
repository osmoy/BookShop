<%@ Page Title="" Language="C#" MasterPageFile="~/master/Common.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="Moy.BookShop.UI.book.BookDetail" %>
<%@ Import Namespace="Moy.BookShop.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
        图书详情页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeHolder1" runat="server">
     <link href="<%: ResolveUrl("~/libs/css/channel.css") %>" rel="stylesheet" />
    <link href="<%: ResolveUrl("~/libs/css/answer.css") %>" rel="stylesheet" />    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="book_view">
        <h1 class="b_title"><%: book.Title %></h1>
        <div class="b_exa">
            <span class="book_group"><%: book.Title %></span>
            <span class="book_status">已经阅读（<%: book.Clicks %> 人） <span><a href="#">放入书架</a></span> <a href="#">什么是书架？</a></span>
        </div>

        <!--book basic start-->
        <dl class="put_book">
            <dt>
                <img src="<%:ResolveUrl(CommonHelper.GetDefault(string.Format("/libs/Images/BookCovers/{0}.jpg", book.ISBN))) %>" alt="<%: book.Title %>" />
                <div class="chakan">
                    <img src="<%:ResolveUrl("~/libs/Images/zoom.gif") %>" />
                    <a class="gray878787a" href="#">点击查看大图</a>
                </div>
            </dt>
            <dd>
                <div id="book_editor">
                    作　　者： <%: book.Author %><br />
                    出 版 社： <%: book.Publisher.Name %>
                </div>
                <ul id="book_attribute">
                    <li>出版时间： <%: book.PublishDate.ToString("yyyy-MM-dd") %></li>
                    <li>字　　数： </li>
                    <li>版　　次： 1</li>

                    <li>页　　数： 540</li>
                    <li>印刷时间： 2009-5-1</li>
                    <li>开　　本： 16开</li>
                    <li>印　　次： 1</li>
                    <li>纸　　张： 胶版纸</li>
                    <li>I S B N    ： <%: book.ISBN %></li>

                    <li>包　　装： 平装</li>
                </ul>

                <div id="book_categroy">所属分类： <a href="#" target="_blank" class="blue12a"><%: book.Category.Name %></a></div>

                <div id="book_price">
                    <span class="red">定价：￥<b><%: string.Format("{0:f2}", book.UnitPrice) %></b></span>
                </div>

                <div id="book_point">
                    <br />
                    <a href="#">
                        <img src="/libs/Images/btn_goumai.gif" onmouseover="this.src='/libs/Images/btn_goumai_click.gif'" onmouseout="this.src='/libs/Images/btn_goumai.gif'" /></a>
                    <a href="#">
                        <img src="/libs/Images/btn_zancun.gif" onmouseover="this.src='/libs/Images/btn_zancun_click.gif'" onmouseout="this.src='/libs/Images/btn_zancun.gif'" /></a>
                </div>

                <div id="book_count">
                    顾客评分：<span id="book_id_15">0</span>
                    共有商品评论0条  <a href="#">查看评论摘要</a>
                </div>
            </dd>
        </dl>
        <!--book basic end-->
        <!--book intro start-->
        <dl class="book_intro">
            <dt>编辑推荐</dt>
            <dd><%: book.ContentDescription %></dd>
        </dl>
        <div id="showRemain">
            <dl class="book_intro">
                <dt>目录</dt>
                <dd style="display:block"><%: new HtmlString(book.TOC.Substring(0, 150)) %></dd>
                <span style="display: none;"><%: new HtmlString(book.TOC) %></span>
                <a href="javascript:;">查看完整目录</a>
            </dl>
        </div>
        <!--book intro end-->

        <!--recommed start-->
        <div class="comm_answer">
            <!--review head start-->
            <div id="div_product_reviews">
                <div class="total_comm">
                    <div class="comm_title">
                        <h2>商品评论 共<em>814</em>条
                        <span class="look_comm">(<a href="#" name='reviewList' target='_blank'>查看所有评论</a>)</span></h2>
                    </div>

                    <div class="total_body">
                        <div class="people_average">
                            <div class="average_left">
                                <p>购买过的顾客平均评分</p>
                                <span class="a_red28b pd">4</span><span class="red_bold">星半</span><img src='/libs/Images/star_red.gif' /><img src='/libs/Images/star_red.gif' /><img src='/libs/Images/star_red.gif' /><img src='/libs/Images/star_red.gif' /><img src='/libs/Images/star_redgray_big.gif' />
                            </div>
                            <span class="span_jt" id="div_window_star">
                                <input class="button_down1" value="" type="button" /></span>
                        </div>

                        <div id="div_product_summary">

                            <div class="people_heart">
                                心情指数:<em>249</em>人 开心
                                <span id="div_emotion_hover">
                                    <input class="button_down1" type="button" />
                                </span>
                            </div>
                            <div id="Div1" class="people_read">
                                阅读场所:<em>180</em>人 床上
                                <span id="div_location_hover">
                                    <input class="button_down1" type="button" />
                                </span>
                            </div>
                        </div>

                        <div class="write_comm">
                            <a id="reviewTipa" href="#">
                                <img src="/libs/Images/button_write_comm.gif" border="0" title="写评论" /></a>
                        </div>
                    </div>
                </div>
            </div>
            <!--review head end-->

            <!--the one reviews start-->
            <div class="comm_list">
                <h3>
                    <img src="/libs/Images/label_1.gif" title="精彩评论" /><a href="#" target="_blank" name="reviewDetail">非常不错的一本书</a>
                    <span>发表于 2009-04-29 22:46</span>
                </h3>

                哦耶，终于拿到书了。&nbsp;<br />
                实物比照片显示的要漂亮，颜色是看上去很舒服的红色。&nbsp;<br />
                书很厚，是塑封的，里面有两张挂图，一张标准穴位图，一张足部反射区图，还有一张配套的光盘，用DVD机试了一下，是中里老师讲的。嗯，
            </div>
            <!--the one reviews end-->
        </div>
        <!--recommed end-->
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var book_score = parseInt($("book_id_15").innerHTML);
        var book_score_str = "";

        for (var m = 0; m < book_score; m++) {
            book_score_str += "<img src='images/star_red.gif' />";
        }
        $("book_id_15").innerHTML = book_score_str;

        showContent();

        function showContent() {
            var oDiv = document.getElementById('showRemain');
            var oA = oDiv.getElementsByTagName('a')[0];
            var oSpan = oDiv.getElementsByTagName('span')[0];
            var odd = oDiv.getElementsByTagName('dd')[0];
            var oBtn = true;
            oA.onclick = function () {
                if (oBtn) {
                    odd.style.display = 'none';
                    oSpan.style.display = 'block';
                    oA.innerHTML = '查看部分目录';
                    oBtn = false;
                } else {
                    oBtn = true;
                    odd.style.display = 'block';
                    oSpan.style.display = 'none';
                    oA.innerHTML = '查看完整目录';
                }
            }
        }

    </script>
   
</asp:Content>
