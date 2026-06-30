using Contracts.Interfaces.Infrastructure.Context;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Infrastructure.Databse.Context
{
    public partial class SiGManagerDB : IDbContext
    {
        private readonly string _connectionString;
        private readonly Regex _connectionStringPattern = ConnectionStringPattern();

        public SiGManagerDB(string connectionString)
        {
            if (!_connectionStringPattern.IsMatch(connectionString))
                throw new ArgumentException("Invalid connection string format.");

            _connectionString = connectionString;
        }

        public async Task TestConnectionAsync()
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
        }

        public async Task<IDbConnection> GetConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<int> ExecuteNonQueryAsync(string query, DbParameter[]? parameters = null)
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(query, connection);

            if (parameters is not null)
                command.Parameters.AddRange(parameters);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> ExecuteReaderAsync(string sql, DbParameter[]? parameters = null)
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new MySqlCommand(sql, connection);

            if (parameters is not null)
                command.Parameters.AddRange(parameters);

            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
        
        public async Task<object?> ExecuteScalarAsync(string sql, DbParameter[]? parameters = null)
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);

            if (parameters is not null)
                command.Parameters.AddRange(parameters);

            return await command.ExecuteScalarAsync();
        }

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.BeginTransactionAsync();
        }

        [GeneratedRegex(@"^Server=[^;]+;Port=[^;]+;Database=[^;]+;Uid=[^;]+;Pwd=[^;]+;$")]
        private static partial Regex ConnectionStringPattern();
    }
}