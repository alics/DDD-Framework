using Framework.Core.Settings;
using System;

namespace Framework.Core.Contexts
{
    public interface IWorkContextProvider
    {
        void AddParameter(string key, object value);
        TValue GetParameter<TValue>(string key)
             where TValue : class;

        DateTime GetCurrentTime();
    }
}
