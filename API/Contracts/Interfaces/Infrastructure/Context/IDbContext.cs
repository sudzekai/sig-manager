using System.Data.Common;

namespace Contracts.Interfaces.Infrastructure.Context
{
    public interface IDbContext
    {
        Task TestConnectionAsync();
        Task<DbCommand> CreateCommandAsync(string query, DbParameter[]? parameters = null);
        Task BeginTransactionAsync();
        Task RollBackAsync();
        Task CommitAsync();
    }
}