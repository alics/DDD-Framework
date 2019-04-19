using Framework.Core.Helpers;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Script.Serialization;

namespace Framework.Core.Settings.Providers
{
    // Do not modify settings in web.config at runtime because any changes to it make application restart.
    // For a file-based writable provider create a standalone configuration file instead.
    public class ConfigFileSettingsProvider : ISettingsProvider
    {
        public bool TryGetSetting<T>(SettingEntryKey key, out T value)
        {
            value = default(T);

            if (!SettingExists(key))
            {
                return false;
            }

            var appSettingKey = key.Category + ":" + key.Name;
            var appSettingValue = ConfigurationManager.AppSettings.Get(appSettingKey);

            //Throw.If(appSettingValue == null && !typeof(T).AllowsNullValue()).A<InvalidOperationException>("The setting entry is null but output type is not allowed null.");

            if (appSettingValue != null)
            {
                var serializer = new JavaScriptSerializer();
                value = serializer.Deserialize<T>(appSettingValue);
            }

            return true;
        }

        public bool SettingExists(SettingEntryKey key)
        {
            var appSettingKey = key.Category + ":" + key.Name;
            return ConfigurationManager.AppSettings.AllKeys.Any(k => k.EqualsIgnoreCase(appSettingKey));
        }

        public string ProviderName
        {
            get { return "ConfigFileSettingsProvider"; }
        }
    }
}
