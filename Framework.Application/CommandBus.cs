using System;
using System.Threading.Tasks;
using Framework.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }

    }
}