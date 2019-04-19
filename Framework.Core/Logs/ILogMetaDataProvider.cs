namespace Framework.Core.Logs
{
    public interface ILogMetaDataProvider
    {
        string GetMachineName();

        string GetApplicationName();

        string GetTraceId();

        string GetUserIdentity();
    }
}
