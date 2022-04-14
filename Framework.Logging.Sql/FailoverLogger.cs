using Framework.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Framework.Logging.Sql
{
    public class FailoverLogger : IFailoverLogger
    {
        private readonly ILogger<FailoverLogger> _logger;

        public FailoverLogger(ILogger<FailoverLogger> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.Log(LogLevel.Error, message);
        }
    }
}