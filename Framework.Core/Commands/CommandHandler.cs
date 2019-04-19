namespace Framework.Core.Commands
{
    public abstract class CommandHandler<TCommand> :ICommandHandler<TCommand>
        where TCommand:ICommand
    {
        protected CommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public abstract void Handle(TCommand command);
    }
}
