using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace Danny.Infrastructure.Helper
{
    public partial class CommonHelper
    {
        #region 提取html中所有的文本

        public static string ExtractTextFromHtml(string htmlstring)
        {
            if (string.IsNullOrEmpty(htmlstring)) return "";
            //删除脚本  
            htmlstring = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|emsp);", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlstring.Replace("<", "");
            htmlstring.Replace(">", "");
            htmlstring.Replace("\r\n", "");

            return htmlstring;
        }
        #endregion

        #region 相对路径映射到磁盘的物理路径

        /// <summary>
        /// 映射一个路径到磁盘的物理路径
        /// </summary>
        /// <param name="path">相对路径 例如："~/bin"</param>
        /// <returns>物理路径.  例如："c:\inetpub\wwwroot\bin"</returns>
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            //不在宿主环境中运行，运行在单元测试中
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        #endregion

       

        #region 生成字符串类型的随机数字

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">数字的长度</param>
        /// <returns>随机数字字符串</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (int i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        #endregion

        #region 是否是ajax请求

        public static bool IsAjaxRequest()
        {
            var request = HttpContext.Current.Request;
            if (request == null)
                throw new ArgumentNullException("request");

            if (request["X-Requested-With"] == "XMLHttpRequest")
                return true;

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        #endregion

        #region 流转换为字节数组

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        #endregion

        #region [获取应用程序根路径]

        private static string GetApplicationRootPath()
        {
            var appPath = "";
            var httpCurrent = HttpContext.Current;
            if (httpCurrent != null)
            {
                appPath = httpCurrent.Server.MapPath("~");
            }
            else
            {
                appPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(appPath, @"\\$", RegexOptions.Compiled).Success)
                    appPath = appPath.Substring(0, appPath.Length - 1);
            }
            return appPath;
        }

        #endregion


    }
}
