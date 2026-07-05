namespace Contracts.Interfaces.Application.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(Commands.ICommandHandler<TResult> command);
    }
}
