using Framework.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandBus
    {
        Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
