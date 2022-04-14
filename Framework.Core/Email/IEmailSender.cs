using System.Threading.Tasks;

namespace Framework.Core.Email
{
    public interface IEmailSender
    {
        Task Send(EmailMessage email);
    }
}
