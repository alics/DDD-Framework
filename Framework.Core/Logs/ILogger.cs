using System;

namespace Framework.Core.Logs
{
    public interface ILogger
    {
        LogSettings LogSettings { get; set; }

        void Log(LogLevel logLevel, string message, params object[] args);
        void Log(LogLevel logLevel, Exception exception, string message, params object[] args);

    }

    public static class LoggerExtentions
    {
        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        public static void Info(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Info, message, args);
        }

        public static void Warn(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warn, message, args);
        }

        public static void Warn(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warn, exception, message, args);
        }

        public static void Error(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }

        public static void Error(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, exception, message, args);
        }

        public static void Fatal(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Fatal, message, args);
        }

        public static void Fatal(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Fatal, exception, message, args);
        }
    }
}
