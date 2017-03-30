using System;
using Danny.Infrastructure.Helper.Log;

namespace Danny.Infrastructure.Logging
{
    public class LogManager:ILogManager
    {
        public void Error(Exception ex)
        {
           LogHelper.Current.Error(ex);
        }

        public void Error(string messsage)
        {
            LogHelper.Current.Error(messsage);
        }

        public void Info(string message)
        {
            LogHelper.Current.Info(message);
        }

        public void Debug(string message)
        {
            LogHelper.Current.Debug(message);
        }
    }
}

