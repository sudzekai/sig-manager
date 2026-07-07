using Contracts.Interfaces.Infrastructure.Context;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Infrastructure.Databse.Context
{
    public sealed partial class SiGManagerDB : IDbContext, IAsyncDisposable
    {
        private readonly string _connectionString;
        private readonly Regex _connectionStringPattern = ConnectionStringPattern();

        private MySqlConnection _connection;
        private MySqlTransaction? _transaction;

        public SiGManagerDB(string connectionString)
        {
            if (!_connectionStringPattern.IsMatch(connectionString))
                throw new ArgumentException("Invalid connection string format.");

            _connectionString = connectionString;

            _connection = new MySqlConnection(connectionString);
        }

        public async Task TestConnectionAsync()
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
        }

        public async Task<DbCommand> CreateCommandAsync(string query, DbParameter[]? parameters = null)
        {
            await EnsureConnectedAsync();
            
            MySqlCommand command = _transaction is null
                        ? new(query, _connection)
                        : new(query, _connection, _transaction);

            if (parameters is not null)
                command.Parameters.AddRange(parameters);

            return command;
        }

        public async Task BeginTransactionAsync()
        {
            await EnsureConnectedAsync();

            if (_transaction is not null)
                throw new InvalidOperationException("Transaction already started.");

            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction is null)
                return;

            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();

            _transaction = null;
        }

        public async Task RollBackAsync()
        {
            if (_transaction is null)
                return;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();

            _transaction = null;
        }

        private async Task EnsureConnectedAsync()
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();
        }

        [GeneratedRegex(@"^Server=[^;]+;Port=[^;]+;Database=[^;]+;Uid=[^;]+;Pwd=[^;]+;$")]
        private static partial Regex ConnectionStringPattern();

        public async ValueTask DisposeAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }

            await _connection.DisposeAsync();
        }
    }
}