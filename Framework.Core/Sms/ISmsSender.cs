using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Sms
{
    public interface ISmsSender
    {
        Task Send(SmsMessage sms);
    }
}
