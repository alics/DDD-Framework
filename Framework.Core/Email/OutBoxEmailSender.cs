using System;
using System.Threading.Tasks;

namespace Framework.Core.Email
{
    public class OutBoxEmailSender : IEmailSender
    {
        private readonly IUnitOfWork _unitOfWork;

        public OutBoxEmailSender(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Send(EmailMessage email)
        {
            var emailData = Newtonsoft.Json.JsonConvert.SerializeObject(email);
            var outBoxMessage = new OutboxMessage(DateTime.UtcNow, typeof(EmailMessage).Name, emailData);

            await _unitOfWork.AddOutboxMessage(outBoxMessage);
        }
    }
}
