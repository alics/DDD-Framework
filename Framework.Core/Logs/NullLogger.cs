using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Logs
{
    public class NullLogger : ILogger
    {
        public LogSettings LogSettings { get; set; }

        public void Log(LogLevel logLevel, string message, params object[] args)
        {

        }

        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {

        }
    }
}
