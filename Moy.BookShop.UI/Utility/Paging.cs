﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Moy.BookShop.UI.utility
{
    public class Paging
    {
        public static string PageList(int pageIndex, int pageCount)
        {
            if (pageCount == 1)
            {
                return null;
            }
            int start = pageIndex - 5;
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;
            if (end > pageCount)
            {
                end = pageCount;
                start = end - 9 > 0 ? end - 9 : 1;
            }
            StringBuilder sb = new StringBuilder();
            #region MyRegion
            if (pageCount > 1)
            {
                sb.Append("<a href='?pageIndex=1'>首页</a>");
            }
            if (pageIndex > 1)
            {
                sb.AppendFormat("<a href='?pageIndex={0}'>上一页</a>", pageIndex - 1);
            }
            for (int i = start; i < end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>", i));
                }
            }

            if (pageIndex < pageCount)
            {
                sb.AppendFormat("<a href='?pageIndex={0}'>下一页</a>", pageIndex + 1);
            }
            if (pageIndex != pageCount && pageCount > 0)
            {
                sb.AppendFormat("<a href='?pageIndex={0}'>尾页</a>", pageCount);
            }
            sb.Append("<span>当前第" + pageIndex + "页/共" + pageCount + " 页</span>");
            #endregion
            #region
            //if (pageCount > 1)
            //{
            //    sb.Append("<a href='/book/BookList_1.aspx'>首页</a>");
            //}
            //if (pageIndex > 1)
            //{
            //    sb.AppendFormat("<a href='/book/BookList_{0}.aspx'>上一页</a>", pageIndex - 1);
            //}
            //for (int i = start; i < end; i++)
            //{
            //    if (i == pageIndex)
            //    {
            //        sb.Append(i);
            //    }
            //    else
            //    {
            //        //sb.AppendFormat("<a href='/book/BookList_{0}_{1}.aspx'>{0}</a>", i, categoryId);
            //        sb.Append(string.Format("<a href='/book/BookList_{0}.aspx'>{0}</a>", i));
            //    }
            //}

            //if (pageIndex < pageCount)
            //{
            //    sb.AppendFormat("<a href='/book/BookList_{0}.aspx'>下一页</a>", pageIndex + 1);
            //}
            //if (pageIndex != pageCount)
            //{
            //    sb.AppendFormat("<a href='/book/BookList_{0}.aspx'>尾页</a>", pageCount);
            //} 
            #endregion
            return sb.ToString();
        }
    }
}