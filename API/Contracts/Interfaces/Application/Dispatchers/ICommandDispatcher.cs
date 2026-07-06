using Contracts.Interfaces.Application.Commands;

namespace Contracts.Interfaces.Application.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command);
    }
}
