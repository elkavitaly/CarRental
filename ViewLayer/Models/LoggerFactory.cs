using NLog;

namespace ViewLayer.Models
{
    /// <summary>
    /// Provide logging events in the system
    /// </summary>
    public static class LoggerFactory
    {
        public static Logger Logger { get; private set; }

        public static void Initialize() => Logger = NLog.LogManager.GetCurrentClassLogger();
    }
}