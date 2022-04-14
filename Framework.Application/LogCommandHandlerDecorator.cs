using System.Threading.Tasks;
using Framework.Core;
using Framework.Core.Logging;
using Microsoft.Extensions.Configuration;

namespace Framework.Application
{
    public class LogCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;
        private readonly IActivityLogger _activityLogger;
        private readonly IConfiguration _configuration;

        public LogCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IActivityLogger activityLogger, IConfiguration configuration)
        {
            _commandHandler = commandHandler;
            _activityLogger = activityLogger;
            _configuration = configuration;
        }

        public async Task HandleAsync(TCommand command)
        {
            bool isActiveCommandLog = bool.Parse(_configuration.GetSection("Logger:ActivityLogger:IsActiveCommandLog").Value);

            if(isActiveCommandLog)
                _activityLogger.StartActivityLog(command.GetType().FullName, command);

            await _commandHandler.HandleAsync(command);
            
            if (isActiveCommandLog)
                _activityLogger.EndActivityLog(command.GetType().FullName, command);
        }
    }
}
