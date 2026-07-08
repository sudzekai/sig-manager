namespace Contracts.Interfaces.Infrastructure
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
    }
}