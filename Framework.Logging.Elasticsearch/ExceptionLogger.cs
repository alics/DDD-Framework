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
    public class ExceptionLogger : LoggerBase, IExceptionLogger
    {
        private ExceptionLoggerConfig _config;
        private IFailoverLogger _failoverLogger;
        private readonly LoggingElasticClient<ErrorLog> _elasticClient;

        public ExceptionLogger(IConfiguration configuration, IFailoverLogger failoverLogger)
        {
            _config = new ExceptionLoggerConfig()
            {
                ApplicationName = configuration.GetSection("Logger:ApplicationName").Value,
                ExceptionLogConnectionString = configuration.GetConnectionString("ExceptionLogConnectionString"),
                IsActive = bool.Parse(configuration.GetSection("Logger:ExceptionLogger:IsActive").Value)
            };
            _failoverLogger = failoverLogger;
            _elasticClient = new LoggingElasticClient<ErrorLog>(configuration);
        }

        public void Log(Exception exception)
        {
            Task.Run(() =>
            {
                var errorLog = new ErrorLog
                {
                    applicationName = _config.ApplicationName,
                    message = exception.ToString(),
                    userIdentity = GetUserIdentity(),
                    type = exception.GetType().FullName,
                    serverIdentity = Environment.MachineName,
                    data = JsonConvert.SerializeObject(exception.Data),
                    ip = GetClientIp()
                };

                try
                {
                    if (!_config.IsActive) return;

                    var fluentIndexResponse = _elasticClient.Index(errorLog, "error_log");

                    // TODO Check the result
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