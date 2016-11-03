using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danny.Infrastructure.Helper
{
    public class TimeHelper
    {
        #region 返回中文格式的时间差

        /// <summary>
        /// 获取当前时间到某个时间的中文格式的时间差
        /// 例如 :1小时前
        /// </summary>
        /// <param name="targetTime">目标时间</param>
        /// <returns>中文格式的时间差</returns>
        public static string DateDiff(DateTime targetTime)
        {
            string dateDiff = null;

            var timeSpan = DateTime.Now - targetTime;
            if (timeSpan.Days >= 1)
            {
                dateDiff = targetTime.Month + "月" + targetTime.Day + "日";
            }
            else
            {
                if (timeSpan.Hours > 1)
                {
                    dateDiff = timeSpan.Hours + "小时前";
                }
                else
                {
                    dateDiff = timeSpan.Minutes + "分钟前";
                }
            }

            return dateDiff;
        }

        #endregion

        #region 获取时间戳

        /// <summary>
        ///  获取毫秒级别时间戳 
        /// </summary>
        /// <returns></returns>
        public static string GetMilliTimeStamp(DateTime time)
        {
            var timeSpan = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalMilliseconds).ToString();
        }

        /// <summary>
        ///  获取秒级别时间戳 
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime time)
        {
            var timeSpan = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalSeconds).ToString();
        }


        #endregion
    }
}
