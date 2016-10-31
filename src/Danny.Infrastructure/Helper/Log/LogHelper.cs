using log4net;

namespace Danny.Infrastructure.Helper.Log4Net
{
    public class LogHelper
    {
        public static ILog Current { get; set; }

        static LogHelper()
        {
            Current = LogManager.GetLogger("RollingLogFileAppender");
        }
    }
}
