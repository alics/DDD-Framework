using System;

namespace Framework.Core.Logs
{
    public abstract class LogEntryBase
    {
        public string TraceId { get; set; }

        public string MachineName
        {
            get; set;
        }

        public string ApplicationName
        {
            get; set;
        }

        public string Message { get; set; }

        public string ThreadId
        {
            get; set;
        }

        public string UserIdentity { get; set; }

        public string LogType { get; set; }

        public DateTime LogDate { get; set; }

        public override string ToString()
        {
            return $"TraceId : {TraceId}, MachineName : {MachineName}," +
                $" ApplicationName: {ApplicationName}, Message: {Message}," +
                $" ThreadId: {ThreadId}, UserIdentity: {UserIdentity}, " +
                $"LogType: {LogType}, LogDate: {LogDate}";
        }
    }
}
