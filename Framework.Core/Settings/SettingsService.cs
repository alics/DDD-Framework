using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly IEnumerable<ISettingsProvider> _settingsProviders;


        public SettingsService(IEnumerable<ISettingsProvider> settingsProviders)
        {
            _settingsProviders = settingsProviders;
        }

        public virtual bool SettingExists(SettingEntryKey key)
        {
            return _settingsProviders.Any(p => p.SettingExists(key));
        }

        public virtual bool TryGetSetting<T>(SettingEntryKey key, out T value)
        {
            var provider = _settingsProviders.FirstOrDefault(p => p.SettingExists(key));
            value = default(T);
            return provider != null && provider.TryGetSetting(key, out value);
        }

        public virtual bool TrySetSetting<T>(SettingEntryKey key, T value)
        {
            throw new NotImplementedException();
        }

        public T GetSetting<T>(SettingEntryKey key)
        {
            T value;
            TryGetSetting(key, out value);

            return value;
        }

        public void SetSetting<T>(SettingEntryKey key, T value)
        {
            TrySetSetting(key, value);
        }
    }
}
