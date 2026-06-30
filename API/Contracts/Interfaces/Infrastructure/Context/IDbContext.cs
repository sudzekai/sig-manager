using System.Data;
using System.Data.Common;

namespace Contracts.Interfaces.Infrastructure.Context
{
    public interface IDbContext
    {
        Task<IDbTransaction> BeginTransactionAsync();
        Task<int> ExecuteNonQueryAsync(string query, DbParameter[]? parameters = null);
        Task<IDataReader> ExecuteReaderAsync(string sql, DbParameter[]? parameters = null);
        Task<object?> ExecuteScalarAsync(string sql, DbParameter[]? parameters = null);
        Task<IDbConnection> GetConnectionAsync();
        Task TestConnectionAsync();
    }
}