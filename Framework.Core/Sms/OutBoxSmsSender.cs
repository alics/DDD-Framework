using System;
using System.Threading.Tasks;

namespace Framework.Core.Sms
{
    public class OutBoxSmsSender : ISmsSender
    {
        private readonly IUnitOfWork _unitOfWork;

        public OutBoxSmsSender(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Send(SmsMessage sms)
        {
            var smsData = Newtonsoft.Json.JsonConvert.SerializeObject(sms);
            var outBoxMessage = new OutboxMessage(DateTime.UtcNow, typeof(SmsMessage).Name, smsData);
            
            await _unitOfWork.AddOutboxMessage(outBoxMessage);
        }
    }
}
