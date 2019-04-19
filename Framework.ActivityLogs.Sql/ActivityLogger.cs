using Framework.ActivityLogs.Sql.Data;
using Framework.Core.Logs;
using Framework.Core.Times;
using System;
using System.Threading;

namespace Framework.ActivityLogs.Sql
{
    public class ActivityLogger : IActivityLogger
    {
        private ILogMetaDataProvider logMetaDataProvider;
        private ITimeProvider timeProvider;

        public ActivityLogger(ILogMetaDataProvider logMetaDataProvider, ITimeProvider timeProvider)
        {
            this.logMetaDataProvider = logMetaDataProvider;
            this.timeProvider = timeProvider;
        }

        public void Log(string activityName, string logType, string message)
        {
            CreateLogData(activityName, logType, message);
        }

        private ActivityLogData CreateLogData(string activityName, string logType, string message)
        {
            var logData = new ActivityLogData()
            {
                MachineName = logMetaDataProvider.GetMachineName(),
                ApplicationName = logMetaDataProvider.GetApplicationName(),
                TraceId = logMetaDataProvider.GetTraceId(),
                ThreadId = Thread.CurrentThread.Name,
                UserIdentity = logMetaDataProvider.GetUserIdentity(),
                LogDate = timeProvider.GetCurrentTime(),
                LogType = logType,
                ActivityName = activityName,
                Message = message
            };

            return logData;
        }

        private void SaveLogData(ActivityLogData logData)
        {
            using (var context = new ActivityLogContext())
            {
                context.ActivityLogDatas.Add(logData);
                context.SaveChanges();
            }
        }
    }
}
