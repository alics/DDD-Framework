namespace Framework.Core.Logs
{
    public interface IActivityLogger
    {
        void Log(string activityName, string logType, string message);
    }
}
