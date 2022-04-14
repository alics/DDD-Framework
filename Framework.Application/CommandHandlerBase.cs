using Framework.Core;
using System.Threading.Tasks;

namespace Framework.Application
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
     where TCommand : ICommand
    {
        public IUnitOfWork _unitOfWork;

        protected CommandHandlerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public abstract Task HandleAsync(TCommand command);
    }
}