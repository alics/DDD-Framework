namespace Framework.Core.Logging
{
    public interface IActivityLogger
    {
        void StartActivityLog(string activityName, object data = null);

        void EndActivityLog(string activityName, object data = null);
    }

    public class ActivityLoggerConfig
    {
        public  bool IsActive { get; set; }
        public  bool SaveInputInLog { get; set; }
        public  bool SaveOutputInLog { get; set; }
        public  string ApplicationName { get; set; }
        public string ActivityLogConnectionString { get; set; }
    }
}
