using Framework.Core.Settings;

namespace Framework.Core.Logs
{
    public static class LogSettingsServiceExtentions
    {
        public static LogSettings GetLogSettings(this ISettingsService settingsService)
        {
            var isEnableDebug = settingsService.GetSetting<bool>(new SettingEntryKey { Category = "Logger", Name = "IsEnableDebug" });
            var isEnableWarn = settingsService.GetSetting<bool>(new SettingEntryKey { Category = "Logger", Name = "IsEnableWarn" });
            var isEnableError = settingsService.GetSetting<bool>(new SettingEntryKey { Category = "Logger", Name = "IsEnableError" });
            var isEnableInfo = settingsService.GetSetting<bool>(new SettingEntryKey { Category = "Logger", Name = "IsEnableInfo" });
            var isEnableFatal = settingsService.GetSetting<bool>(new SettingEntryKey { Category = "Logger", Name = "IsEnableFatal" });

            var logSettings = new LogSettings()
            {
                IsEnableDebug = isEnableDebug,
                IsEnableWarn = isEnableWarn,
                IsEnableError = isEnableError,
                IsEnableFatal = isEnableFatal,
                IsEnableInfo = isEnableInfo
            };

            return logSettings;
        }
    }
}
