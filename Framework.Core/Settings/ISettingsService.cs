namespace Framework.Core.Settings
{
    public interface ISettingsService
    {
        T GetSetting<T>(SettingEntryKey key);
        bool TryGetSetting<T>(SettingEntryKey key, out T value);
        void SetSetting<T>(SettingEntryKey key, T value);
        bool TrySetSetting<T>(SettingEntryKey key, T value);
        bool SettingExists(SettingEntryKey key);
    }

    public static class SettingsServiceExtentions
    {
        public static T GetSetting<T>(this ISettingsService settingsService, string category, string name)
        {
            return settingsService.GetSetting<T>(category, name);
        }
    }
}
