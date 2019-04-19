using Framework.Core.Contexts;
using Framework.Core.Settings;
using System;

namespace Framework.Core.Logs
{
    public class DefaultLogMetaDataProvider : ILogMetaDataProvider
    {
        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        public string GetApplicationName()
        {
            return WorkContext.Settings.GetSetting<string>("Application", "ApplicationName");
        }

        public string  GetTraceId()
        {
            return WorkContext.Current.GetParameter<string>("TraceId");
        }

        public string GetUserIdentity()
        {
            return WorkContext.Current.GetParameter<string>("UserIdentity");
        }
    }
}
