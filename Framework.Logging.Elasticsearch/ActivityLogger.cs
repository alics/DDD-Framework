using Framework.Core.Logging;
using Framework.Logging.Elasticsearch.Data;
using Framework.Logging.Elasticsearch.ESClient;
using Microsoft.Extensions.Configuration;
using Nest;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging.Elasticsearch
{
    public class ActivityLogger : LoggerBase, IActivityLogger
    {
        private ActivityLoggerConfig _config;
        private IFailoverLogger _failoverLogger;
        private readonly LoggingElasticClient<ActivityLog> _elasticClient;

        public ActivityLogger(IConfiguration configuration, IFailoverLogger failoverLogger)
        {
            _config = new ActivityLoggerConfig
            {
                ApplicationName = configuration.GetSection("Logger:ApplicationName").Value,
                ActivityLogConnectionString = configuration.GetConnectionString("ActivityLogConnectionString"),
                IsActive = bool.Parse(configuration.GetSection("Logger:ActivityLogger:IsActive").Value),
                SaveInputInLog = bool.Parse(configuration.GetSection("Logger:ActivityLogger:SaveInputInLog").Value),
                SaveOutputInLog = bool.Parse(configuration.GetSection("Logger:ActivityLogger:SaveOutputInLog").Value)
            };

            _failoverLogger = failoverLogger;
            _elasticClient = new LoggingElasticClient<ActivityLog>(configuration);
            _failoverLogger = failoverLogger;
        }

        public void StartActivityLog(string activityName, object input = null)
        {
            Task.Run(() =>
            {
                var activityLog = new ActivityLog
                {
                    applicationName = "CCDevelopment"/*_config.ApplicationName*/,
                    message = activityName,
                    userIdentity = GetUserIdentity(),
                    type = "start",
                    serverIdentity = Environment.MachineName,
                    data = JsonConvert.SerializeObject(input),
                    IP = "123"//GetClientIp()
                };

                try
                {
                    if (!_config.IsActive) return;

                    var fluentIndexResponse = _elasticClient.Index(activityLog,"activity_log");

                    // TODO Check the result
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(activityName, activityLog, loggerException);
                    _failoverLogger.Log(failoverMessage);
                }
            });
        }

        public void EndActivityLog(string activityName, object output = null)
        {
            Task.Run(() =>
            {
                var activityLog = new ActivityLog
                {
                    applicationName = _config.ApplicationName,
                    message = activityName,
                    userIdentity = GetUserIdentity(),
                    type = "end",
                    serverIdentity = Environment.MachineName,
                    data = JsonConvert.SerializeObject(output),
                    IP = GetClientIp()
                };

                try
                {
                    if (!_config.IsActive) return;

                    var fluentIndexResponse = _elasticClient.Index(activityLog, "activity_log");
                    // TODO Check the result
                }
                catch (Exception loggerException)
                {
                    var failoverMessage = FormatFailoverMessage(activityName, activityLog, loggerException);
                    _failoverLogger.Log(failoverMessage);
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
            builder.Append($"{DateTime.UtcNow} : {activityLog.type} --> {activityName}");
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
