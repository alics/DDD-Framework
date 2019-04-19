using Framework.Core.Logs;
using Framework.Core.Times;
using Framework.Logs.Sql.Data;
using System;
using System.Threading;

namespace Framework.Logs.Sql
{
    public class Logger : ILogger
    {
        private ILogMetaDataProvider logMetaDataProvider;
        private ITimeProvider timeProvider;

        public LogSettings LogSettings { get; set; }

        public Logger(ILogMetaDataProvider logMetaDataProvider, ITimeProvider timeProvider, LogSettings settings)
        {
            this.logMetaDataProvider = logMetaDataProvider;
            this.timeProvider = timeProvider;
            LogSettings = settings;
        }

        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            var logData = CreateLogData(logLevel, message, args);
            SaveLogData(logData);
        }

        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            var logData = CreateLogData(logLevel, message, args);
            SaveLogData(logData);
        }

        private LogData CreateLogData(LogLevel level, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            var logData = new LogData()
            {
                MachineName = logMetaDataProvider.GetMachineName(),
                ApplicationName = logMetaDataProvider.GetApplicationName(),
                TraceId = logMetaDataProvider.GetTraceId(),
                ThreadId = Thread.CurrentThread.Name,
                UserIdentity = logMetaDataProvider.GetUserIdentity(),
                LogDate = timeProvider.GetCurrentTime(),
                LogType = Enum.GetName(typeof(LogLevel), level),
                Message = msg
            };

            return logData;
        }

        private LogData CreateLogData(Exception exception, LogLevel level, string message, params object[] args)
        {
            var logdata = CreateLogData(level, message, args);
            logdata.Message += "Exception : " + exception.ToString();

            return logdata;
        }

        private void SaveLogData(LogData logData)
        {
            using (var context = new LogContext())
            {
                context.LogDatas.Add(logData);
                context.SaveChanges();
            }
        }
    }
}
