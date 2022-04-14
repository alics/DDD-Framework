using Framework.Core;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}