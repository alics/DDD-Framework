namespace Framework.Core.Sms
{
    public class SmsMessage: IEvent
    {
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
