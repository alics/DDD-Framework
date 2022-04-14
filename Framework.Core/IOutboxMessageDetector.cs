using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Core
{
    public interface IOutboxMessageDetector
    {
        List<OutboxMessage> GetOutboxMessages();
    }
}