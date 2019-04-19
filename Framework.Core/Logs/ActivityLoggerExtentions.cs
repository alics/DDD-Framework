namespace Framework.Core.Logs
{
    public static class ActivityLoggerExtentions
    {
        public static void StartActivityLog(this IActivityLogger activityLogger, string activityName, string message)
        {
            activityLogger.Log(activityName, "StartActivity", message);
        }

        public static void EndActivityWithSuccessLog(this IActivityLogger activityLogger, string activityName, string message)
        {
            activityLogger.Log(activityName, "EndActivityWithSuccess", message);
        }

        public static void EndActivityWithFailLog(this IActivityLogger activityLogger, string activityName, string message)
        {
            activityLogger.Log(activityName, "EndActivityWithFail", message);
        }
    }
}
