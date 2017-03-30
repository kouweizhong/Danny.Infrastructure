namespace Danny.Infrastructure.Logging
{
    public class LogFactory
    {
        public static ILogManager CreateLogManager()
        {
            return new LogManager();
        }
    }
}
