using Framework.Core.DependencyInjection;

namespace Framework.Core.Commands
{
    public class CommandBus : ICommandBus
    {
        public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = ServiceLocator.Current.Resolve<ICommandHandler<TCommand>>();
            handler.Handle(command);
            handler.UnitOfWork.Commit();
        }
    }
}
