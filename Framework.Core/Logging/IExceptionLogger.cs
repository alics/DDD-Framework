using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Logging
{
    public interface IExceptionLogger
    {
        void Log(Exception exception);
    }

    public class ExceptionLoggerConfig
    {
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }
        public string ExceptionLogConnectionString { get; set; }
    }
}
