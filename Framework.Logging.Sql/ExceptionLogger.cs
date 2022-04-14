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
    public class ExceptionLogger : LoggerBase, IExceptionLogger
    {
        private ExceptionLoggerConfig _config;
        private IFailoverLogger _failoverLogger;

        public ExceptionLogger(IConfiguration configuration, IFailoverLogger failoverLogger)
        {
            _config = new ExceptionLoggerConfig()
            {
                ApplicationName = configuration.GetSection("Logger:ApplicationName").Value,
                ExceptionLogConnectionString = configuration.GetConnectionString("ExceptionLogConnectionString"),
                IsActive = bool.Parse(configuration.GetSection("Logger:ExceptionLogger:IsActive").Value)
            };
            _failoverLogger = failoverLogger;
        }

        public void Log(Exception exception)
        {
            Task.Run(() =>
            {
                ErrorLog errorLog = null;

                try
                {
                    if (!_config.IsActive) return;

                    errorLog = new ErrorLog
                    {
                        ApplicationName = _config.ApplicationName,
                        Message = exception.ToString(),
                        UserIdentity = GetUserIdentity(),
                        Type = exception.GetType().FullName,
                        ServerIdentity = Environment.MachineName,
                        Data = JsonConvert.SerializeObject(exception.Data),
                        IP = GetClientIp()
                    };

                    var options = new DbContextOptionsBuilder<LogDbContext>()
                        .UseSqlServer(_config.ExceptionLogConnectionString)
                        .Options;

                    using (var context = new LogDbContext(options))
                    {
                        context.ErrorLogs.Add(errorLog);
                        context.SaveChanges();
                    }
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(exception, errorLog, loggerException);
                    _failoverLogger.Log(failoverMessage);
                }
            });
        }

        private string FormatFailoverMessage(Exception mainException, ErrorLog errorLog, Exception loggerException)
        {
            var builder = new StringBuilder();

            builder.AppendLine();
            builder.AppendLine();
            builder.Append("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{DateTime.UtcNow}");
            builder.AppendLine();
            builder.Append($"{mainException}");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("-----------------Data----------------");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append($"{errorLog}");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append("-----------------Logger Exception----------------");
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