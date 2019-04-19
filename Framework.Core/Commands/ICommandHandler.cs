namespace Framework.Core.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        IUnitOfWork UnitOfWork { get; set; }

        void Handle(TCommand command);
    }
}
