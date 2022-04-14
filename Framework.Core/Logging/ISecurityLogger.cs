namespace Framework.Core.Logging
{
    public interface ISecurityLogger
    {
        void Log(string activityName, object data);
    }

    public class SecurityLoggerConfig
    {
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }
        public string SecurityLogConnectionString { get; set; }
    }

    public static class SecurityLoggerExtensions
    {
        public static void LogSuccessLogin(this ISecurityLogger securityLogger,string userIdentity)
        {
            securityLogger.Log("Login", userIdentity);
        }

        public static void LogFailureLogin(this ISecurityLogger securityLogger, string userIdentity)
        {
            securityLogger.Log("FailureLogin", userIdentity);
        }

        public static void LogCreateRole(this ISecurityLogger securityLogger, object data)
        {
            securityLogger.Log("CreateRole", data);
        }

        public static void LogAssignTask(this ISecurityLogger securityLogger, string assignee, string taskId)
        {
            securityLogger.Log("AssignTask", $"{assignee}#{taskId}");
        }

        public static void LogAssignRoleToUser(this ISecurityLogger securityLogger, object data)
        {
            securityLogger.Log("AssignRoleToUser", data);
        }

        public static void LogUnassignRoleToUser(this ISecurityLogger securityLogger, object data)
        {
            securityLogger.Log("UnassignRoleToUser", data);
        }
    }
}
