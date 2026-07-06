using Contracts.Interfaces.Application.Commands;

namespace Contracts.Interfaces.Application.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command);
    }
}
