using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        public async Task<TResult> DispatchAsync<TResult>(ICommandHandler<TResult> command)
        {
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResult));

            dynamic handler = serviceProvider.GetRequiredService(handlerType);

            return await handler.HandleAsync((dynamic)command);
        }
    }
}
