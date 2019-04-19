using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Logs
{
    public interface IFailoverLogger
    {
        void Log(LogEntryBase logData, Exception logException);
    }

    public class FailoverLogDecorator : ILogger
    {
        private readonly ILogger logger;
        private readonly IFailoverLogger failoverLogger;

        public LogSettings LogSettings { get; set; }

        public FailoverLogDecorator(ILogger logger, IFailoverLogger failoverLogger)
        {
            this.logger = logger;
            this.failoverLogger = failoverLogger;
        }

        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            try
            {
                logger.Log(logLevel, message, args);
            }
            catch (Exception ex)
            {

            }
        }

        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            try
            {
                logger.Log(logLevel, exception, message, args);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
