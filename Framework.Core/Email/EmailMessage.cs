namespace Framework.Core.Email
{
    public class EmailMessage : IEvent
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
