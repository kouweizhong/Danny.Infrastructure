using System;

namespace Danny.Infrastructure.Logging
{
    public interface ILogManager
    {
        void Error(Exception ex);

        void Error(string messsage);

        void Info(string message);

        void Debug(string message);

    }
}
