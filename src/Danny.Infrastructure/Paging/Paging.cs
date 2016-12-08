using System;
using System.Text;
using System.Web;

namespace Lawyer.Common.Tool
{
    
    public class Paging
    {
        public string HeadText = "首页";

        public string TailText = "尾页";
       
        private int _maxPage;

        /// <summary>
        /// 最大页码
        /// </summary>
         public int MaxPage
        {
            get
            {
                if (_maxPage == 0)
                {
                    _maxPage = (int)Math.Ceiling((decimal)TotalCounts / PageSize);
                }
                return _maxPage;
            }
        }

         /// <summary>
         /// url生成规则
         /// </summary>
         public Func<int, string> UrlGenerate { set; get; }

         /// <summary>
         /// 总数据数量
         /// </summary>
         public int TotalCounts { set; get; }
         /// <summary>
         /// 单页数据量
         /// </summary>
         public int PageSize { set; get; }

         private string OnUrlGenerate(int index)
         {
             if (UrlGenerate != null)
             {
                 return UrlGenerate(index);
             }
             else
             {
                 return string.Empty;
             }
         }


        public Paging(int totalCounts, int pageSize, Func<int, string> func = null)
        {
            this.TotalCounts = totalCounts;
            this.PageSize = pageSize;
            this.UrlGenerate = func;
        }

        public HtmlString GetString(int currentIndex)
        {
            //边界条件判断和处理
            if (MaxPage == 1) return new HtmlString(string.Empty);
            if (currentIndex > MaxPage) currentIndex = MaxPage;
            if (currentIndex < 1) currentIndex = 1;
            //分页结果显示数量
            int showCount = 9;

            int offSetLeft = 0;
            int offSetRight = 0;
            //右边显示最大页码
            int rightMax = currentIndex + showCount / 2;
            //左边显示最大页码
            int leftMin = currentIndex - showCount / 2;
            //如果临近左边，显示不足一半，需要右边补充的数量
            offSetLeft = 1 - leftMin;
            offSetLeft = offSetLeft < 0 ? 0 : offSetLeft;
            //同样右边
            offSetRight = rightMax - MaxPage;
            offSetRight = offSetRight < 0 ? 0 : offSetRight;
            //判定处理offset
            //分页末尾页码区域
            if (offSetLeft > 0 && offSetRight == 0)
            {
                rightMax += offSetLeft;
                if (rightMax > MaxPage)
                {
                    rightMax = MaxPage;
                }
            }
            //分页起始页码区域
            else if (offSetRight > 0 && offSetLeft == 0)
            {
                leftMin -= offSetRight;
                if (leftMin < 1) leftMin = 1;
            }
            //统一的页码范围判定
            if (leftMin <= 0) leftMin = 1;
            if (rightMax > MaxPage) rightMax = MaxPage;
            //判定是否需要首尾“...”
            bool isAddFirst = false;
            bool isAddMax = false;
            if (showCount >= 7)
            {
                if (leftMin >= 2)
                {
                    leftMin = leftMin + 2;
                    isAddFirst = true;
                }
                if (rightMax <= MaxPage - 1)
                {
                    rightMax = rightMax - 2;
                    isAddMax = true;
                }
            }

            var str = new StringBuilder();
            str.Append("<div class=\"m-page tc mt50\">");
            str.Append(string.Format("<a  class=\"m-page-prev\" href=\"{0}\">{1}</a>", OnUrlGenerate(1), string.IsNullOrEmpty(HeadText) ? "&lt;" : HeadText));
            if (isAddFirst)
            {
                str.Append(string.Format("<a   href=\"{0}\">{1}</a>", OnUrlGenerate(1), 1));
                str.Append("...");
            }
            for (int item = leftMin; item <= rightMax; item++)
            {
                if (item == currentIndex)
                {
                    str.Append(string.Format("<a class=\"m-page-ct\">{0}</a>", item));
                    continue;
                }
                str.Append(string.Format("<a href=\"{0}\">{1}</a>", OnUrlGenerate(item), item));
            }
            if (isAddMax)
            {
                str.Append("...");
                str.Append(string.Format("<a href=\"{0}\">{1}</a>", OnUrlGenerate(MaxPage), MaxPage));
            }

            str.Append(string.Format("<a class=\"next_on\"  href=\"{0}\">{1}</a>", OnUrlGenerate(MaxPage), string.IsNullOrEmpty(TailText) ? "&gt;" : TailText));
            str.Append("</div>");
            return new HtmlString(str.ToString());
        }
    }

    public class PagingHelper
    {
        /// <summary>
        /// 渲染分页代码
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="totalCounts">总页数</param>
        /// <param name="pageSize">每页多少数量</param>
        /// <param name="func">url</param>
        /// <returns></returns>
        public static HtmlString RenderPaging(int currentPage, int totalCounts, int pageSize, Func<int, string> func = null)
        {
            return new Paging(1000, 10, func).GetString(currentPage);
        }
    }
}
