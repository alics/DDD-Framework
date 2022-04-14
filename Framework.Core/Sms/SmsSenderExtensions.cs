using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Core.Sms
{
    public static class SmsSenderExtensions
    {
        public static async Task Send(this ISmsSender smsSender, string receiverNumber, string message)
        {
            var sms = new SmsMessage()
            {
                Receiver = receiverNumber,
                Message = message
            };

            await smsSender.Send(sms);
        }
        public static async Task SendBatch(this ISmsSender smsSender, List<string> receiverNumbers, string message)
        {
            foreach (var number in receiverNumbers)
            {
                var sms = new SmsMessage()
                {
                    Receiver = number,
                    Message = message
                };

                await smsSender.Send(sms);
            }
        }
    }
}
