using NLog;

namespace ViewLayer.Models
{
    public static class LoggerFactory
    {
        public static Logger Logger { get; private set; }

        public static void Initialize() => Logger = NLog.LogManager.GetCurrentClassLogger();
    }
}