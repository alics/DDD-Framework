using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Core.Email
{
    public static class EmailSenderExtensions
    {
        public static async Task Send(this IEmailSender emailSender, string recipient, string subject, string body)
        {
            var email = new EmailMessage
            {
                Recipient = recipient,
                Subject = subject,
                Body = body
            };

            await emailSender.Send(email);
        }
        public static async Task SendBatch(this IEmailSender emailSender, List<string> recipients, string subject, string body)
        {
            foreach (var recipient in recipients)
            {
                var email = new EmailMessage
                {
                    Recipient = recipient,
                    Body = body,
                    Subject = subject
                };

                await emailSender.Send(email);
            }
        }
    }
}
