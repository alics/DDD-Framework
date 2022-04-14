using Framework.Core.Logging;
using Framework.Logging.Sql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging.Sql
{
    public class ActivityLogger : LoggerBase, IActivityLogger
    {
        private ActivityLoggerConfig _config;
        private IExceptionLogger _exceptionLogger;

        public ActivityLogger(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            _config = new ActivityLoggerConfig
            {
                ApplicationName = configuration.GetSection("Logger:ApplicationName").Value,
                ActivityLogConnectionString = configuration.GetConnectionString("ActivityLogConnectionString"),
                IsActive = bool.Parse(configuration.GetSection("Logger:ActivityLogger:IsActive").Value),
                SaveInputInLog = bool.Parse(configuration.GetSection("Logger:ActivityLogger:SaveInputInLog").Value),
                SaveOutputInLog = bool.Parse(configuration.GetSection("Logger:ActivityLogger:SaveOutputInLog").Value)
            };

            _exceptionLogger = exceptionLogger;
        }

        public void StartActivityLog(string activityName, object input = null)
        {
            if (!_config.IsActive) return;
            Task.Run(() =>
            {
                ActivityLog activityLog = null;

                try
                {

                    activityLog = new ActivityLog
                    {
                        ApplicationName = _config.ApplicationName,
                        Message = activityName,
                        UserIdentity = GetUserIdentity(),
                        Type = "start",
                        ServerIdentity = Environment.MachineName,
                        Data = JsonConvert.SerializeObject(input),
                        IP = GetClientIp()
                    };

                    var options = new DbContextOptionsBuilder<LogDbContext>()
                        .UseSqlServer(_config.ActivityLogConnectionString)
                        .Options;

                    using var context = new LogDbContext(options);
                    context.ActivityLogs.Add(activityLog);
                    context.SaveChanges();
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(activityName, activityLog, loggerException);
                    loggerException.Data.Add("FormatFailoverMessage", failoverMessage);
                    _exceptionLogger.Log(loggerException);
                }
            });
        }

        public void EndActivityLog(string activityName, object output = null)
        {
            if (!_config.IsActive) return;
            Task.Run(() =>
            {
                ActivityLog activityLog = null;

                try
                {

                    activityLog = new ActivityLog
                    {
                        ApplicationName = _config.ApplicationName,
                        Message = activityName,
                        UserIdentity = GetUserIdentity(),
                        Type = "end",
                        ServerIdentity = Environment.MachineName,
                        Data = JsonConvert.SerializeObject(output),
                        IP = GetClientIp()
                    };

                    var options = new DbContextOptionsBuilder<LogDbContext>()
                        .UseSqlServer(_config.ActivityLogConnectionString)
                        .Options;

                    using var context = new LogDbContext(options);
                    context.ActivityLogs.Add(activityLog);
                    context.SaveChanges();
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(activityName, activityLog, loggerException);
                    loggerException.Data.Add("FormatFailoverMessage", failoverMessage);
                    _exceptionLogger.Log(loggerException);
                }
            });
        }

        private string FormatFailoverMessage(string activityName, ActivityLog activityLog, Exception loggerException)
        {
            var builder = new StringBuilder();

            builder.AppendLine();
            builder.AppendLine();
            builder.Append("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{DateTime.UtcNow} : {activityLog.Type} --> {activityName}");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("-----------------Data----------------");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{activityLog}");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("-----------logger exception----------");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{loggerException}");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            builder.AppendLine();
            builder.AppendLine();

            return builder.ToString();
        }
    }
}
