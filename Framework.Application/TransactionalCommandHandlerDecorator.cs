using System;
using System.Threading.Tasks;
using Framework.Core;

namespace Framework.Application
{
    public class TransactionalCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandlerDecorator(IUnitOfWork unitOfWork, ICommandHandler<TCommand> commandHandler)
        {
            _commandHandler = commandHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(TCommand command)
        {
            try
            {
                await _commandHandler.HandleAsync(command);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
