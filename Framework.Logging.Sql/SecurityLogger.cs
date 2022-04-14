using Framework.Core.Logging;
using Framework.Logging.Sql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging.Sql
{
    public class SecurityLogger : LoggerBase, ISecurityLogger
    {
        private SecurityLoggerConfig _config;
        private IExceptionLogger _exceptionLogger;

        public SecurityLogger(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            _config = new SecurityLoggerConfig
            {
                ApplicationName = configuration.GetSection("Logger:ApplicationName").Value,
                SecurityLogConnectionString = configuration.GetConnectionString("SecurityLogConnectionString"),
                IsActive = bool.Parse(configuration.GetSection("Logger:SecurityLogger:IsActive").Value)
            };

            _exceptionLogger = exceptionLogger;
        }

        public void Log(string activityName, object data)
        {
            Task.Run(() =>
            {
                SecurityLog securityLog = null;

                try
                {
                    if (!_config.IsActive) return;

                    securityLog = new SecurityLog
                    {
                        ApplicationName = _config.ApplicationName,
                        Message = activityName,
                        UserIdentity = GetUserIdentity(),
                        Type = activityName,
                        ServerIdentity = Environment.MachineName,
                        Data = JsonConvert.SerializeObject(data),
                        IP = GetClientIp()
                    };

                    var options = new DbContextOptionsBuilder<LogDbContext>()
                        .UseSqlServer(_config.SecurityLogConnectionString)
                        .Options;

                    using var context = new LogDbContext(options);
                    context.SecurityLogs.Add(securityLog);
                    context.SaveChanges();
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(activityName, securityLog, loggerException);
                    loggerException.Data.Add("FailoverMessage", failoverMessage);

                    _exceptionLogger.Log(loggerException);
                }
            });
        }
        private string FormatFailoverMessage(string activityName, SecurityLog securityLog, Exception loggerException)
        {
            var builder = new StringBuilder();

            builder.AppendLine();
            builder.AppendLine();
            builder.Append("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{DateTime.UtcNow} : {securityLog.Type} --> {activityName}");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("-----------------Data----------------");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{securityLog}");
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
