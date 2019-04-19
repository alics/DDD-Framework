using Framework.Core.DependencyInjection;
using Framework.Core.Times;
using System;
using System.Collections.Generic;
using Framework.Core.Settings;

namespace Framework.Core.Contexts
{
    public class DefaultWorkContextProvider : IWorkContextProvider
    {
        private Dictionary<string, object> _parameters;
        

        public DefaultWorkContextProvider()
        {
            _parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string key, object value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
            }
            else
            {
                _parameters.Add(key, value);
            }
        }

        public TValue GetParameter<TValue>(string key)
            where TValue : class
        {
            if (!_parameters.ContainsKey(key))
            {
                return null;
            }
            else
            {
                return (_parameters[key] as TValue);
            }
        }

        public DateTime GetCurrentTime()
        {
            return ServiceLocator.Current.Resolve<ITimeProvider>().GetCurrentTime();
        }
    }
}
