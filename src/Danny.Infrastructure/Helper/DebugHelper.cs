using System.Runtime.InteropServices;

namespace Danny.Infrastructure.Helper
{
    /// <summary>
    /// 调试相关帮助类
    /// </summdary>
   public class DebugHelper
    {
        /// <summary>
        /// 将消息写入DebugView中
        /// </summary>
        /// <param name="msg">写入的消息</param>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "OutputDebugString")]
        public static extern void DebugView(string msg);
    }
}
