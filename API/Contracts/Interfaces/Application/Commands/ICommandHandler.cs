namespace Contracts.Interfaces.Application.Commands
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommandHandler<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
