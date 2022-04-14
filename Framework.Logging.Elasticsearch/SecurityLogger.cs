using Framework.Core.Logging;
using Framework.Logging.Elasticsearch.Data;
using Framework.Logging.Elasticsearch.ESClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging.Elasticsearch
{
    public class SecurityLogger : LoggerBase, ISecurityLogger
    {
        private SecurityLoggerConfig _config;
        private IFailoverLogger _failoverLogger;
        private readonly LoggingElasticClient<SecurityLog> _elasticClient;

        public SecurityLogger(IConfiguration configuration, IFailoverLogger failoverLogger)
        {
            _config = new SecurityLoggerConfig
            {
                ApplicationName = configuration.GetSection("Logger:ApplicationName").Value,
                SecurityLogConnectionString = configuration.GetConnectionString("SecurityLogConnectionString"),
                IsActive = bool.Parse(configuration.GetSection("Logger:SecurityLogger:IsActive").Value)
            };

            _failoverLogger = failoverLogger;
        }

        public void Log(string activityName, object data)
        {
            Task.Run(() =>
            {
                var securityLog = new SecurityLog
                {
                    applicationName = _config.ApplicationName,
                    message = activityName,
                    userIdentity = GetUserIdentity(),
                    type = activityName,
                    serverIdentity = Environment.MachineName,
                    data = JsonConvert.SerializeObject(data),
                    IP = GetClientIp()
                };

                try
                {
                    if (!_config.IsActive) return;

                    var fluentIndexResponse = _elasticClient.Index(securityLog, "security_log");
                    // TODO Check the result
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(activityName, securityLog, loggerException);
                    _failoverLogger.Log(failoverMessage);
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
            builder.Append($"{DateTime.UtcNow} : {securityLog.type} --> {activityName}");
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
