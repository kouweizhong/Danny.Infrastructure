using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danny.Infrastructure.Helper
{
    public class GuidHelper
    {
        /// <summary>
        /// 生成整数类型的唯一id
        /// </summary>
        /// <returns></returns>
        public static long GetLongUniqueId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 大写guid无横杠
        /// </summary>
        public static string GetUpperStringUniqueId()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }

        /// <summary>
        /// 小写写guid无横杠
        /// </summary>
        public static string GetLowerStringUniqueId()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }
    }
}
