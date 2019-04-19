namespace Framework.Core.Settings
{
    public interface ISettingsProvider
    {
        bool TryGetSetting<T>(SettingEntryKey key, out T value);
        bool SettingExists(SettingEntryKey key);
        string ProviderName { get; }
    }
}
