using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Infrastructure.Internal.Conveters;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.User;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Text;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _db;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IDbContext db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<int> CreateAsync(User user)
        {
            var query = @$"
                INSERT INTO {UserSchema.TableName} ({string.Join(", ", UserSelects.Insertation)}) 
                VALUES (@username, @fullName, @phoneNumber, @email, @passwordHash, @createdAt, @updatedAt, @roleId);
                SELECT LAST_INSERT_ID();
            ";

            MySqlParameter[] parameters = [
                new("username", user.Username),
                new("fullName", user.FullName),
                new("phoneNumber", user.PhoneNumber),
                new("email", user.Email),
                new("passwordHash", user.PasswordHash),
                new("createdAt", user.CreatedAt),
                new("updatedAt", user.UpdatedAt),
                new("roleId", user.RoleId)
            ];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            var idObj = await _db.ExecuteScalarAsync(query, parameters);

            var id = Convert.ToInt32(idObj);

            return id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = $@"
                DELETE FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            await _db.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(GetUsersListRequest request)
        {
            StringBuilder query = new($@"
                SELECT {string.Join(", ", UserSelects.Simple)} 
                FROM {UserSchema.TableName}
                WHERE 1=1
            ");

            List<MySqlParameter> parameters = [];

            if (request.RoleId.HasValue)
            {
                query.Append($"\nAND {UserSchema.RoleId} = @roleId");
                parameters.Add(new("roleId", request.RoleId.Value));
            }

            if (request.CreatedAtStart != default)
            {
                query.Append($"\nAND {UserSchema.CreatedAt} > @createdAtStart");
                parameters.Add(new("createdAtStart", request.CreatedAtStart));
            }

            if (request.CreatedAtEnd != default)
            {
                query.Append($"\nAND {UserSchema.CreatedAt} < @createdAtEnd");
                parameters.Add(new("createdAtEnd", request.CreatedAtEnd));
            }

            if (request.UpdatedAtStart != default)
            {
                query.Append($"\nAND {UserSchema.UpdatedAt} > @updatedAtStart");
                parameters.Add(new("updatedAtStart", request.UpdatedAtStart));
            }

            if (request.UpdatedAtEnd != default)
            {
                query.Append($"\nAND {UserSchema.UpdatedAt} < @updatedAtEnd");
                parameters.Add(new("updatedAtEnd", request.UpdatedAtEnd));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query.Append(@$"
                    AND ({UserSchema.Username} LIKE @searchFull 
                    OR {UserSchema.FullName} LIKE @searchFull 
                    OR {UserSchema.Email} LIKE @searchStart 
                    OR {UserSchema.PhoneNumber} LIKE @searchEnd)
                ");

                parameters.Add(new("searchFull", $"%{request.SearchTerm}%"));
                parameters.Add(new("searchStart", $"{request.SearchTerm}%"));
                parameters.Add(new("searchEnd", $"%{request.SearchTerm}"));
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var orderBy = request.OrderBy.ToLower() switch
                {
                    "username" => UserSchema.Username,
                    "fullname" => UserSchema.FullName,
                    "role" => UserSchema.RoleId,
                    "createdate" => UserSchema.CreatedAt,
                    "updatedate" => UserSchema.UpdatedAt,
                    _ => UserSchema.Id
                };

                var orderDirection = request.OrderDirection.Equals("asc", StringComparison.OrdinalIgnoreCase)
                    ? "ASC"
                    : "DESC";

                query.Append($"\nORDER BY {orderBy} {orderDirection}");
            }

            query.Append($"\nLIMIT @limit OFFSET @offset;");

            parameters.Add(new("limit", request.Limit));
            parameters.Add(new("offset", request.Offset));

            _logger.CustomLogDebugSqlExecution(query.ToString(), [.. parameters]);

            using var reader = await _db.ExecuteReaderAsync(query.ToString(), [.. parameters]);

            var result = UserConverter.ListFromReader(reader);

            return result;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Email} = @email;
            ";

            MySqlParameter[] parameters = [new("email", email)];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            using var reader = await _db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            using var reader = await _db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.PhoneNumber} = @phoneNumber;
            ";

            MySqlParameter[] parameters = [new("phoneNumber", phoneNumber)];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            using var reader = await _db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Username} = @username;
            ";

            MySqlParameter[] parameters = [new("username", username)];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            using var reader = await _db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetFullByIdAsync(int id)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Full)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            using var reader = await _db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task UpdateAsync(User user)
        {
            var query = $@"
                UPDATE {UserSchema.TableName}
                SET
                    {UserSchema.Username} = @username,
                    {UserSchema.FullName} = @fullName,
                    {UserSchema.PhoneNumber} = @phoneNumber,
                    {UserSchema.Email} = @email,
                    {UserSchema.PasswordHash} = @passwordHash,
                    {UserSchema.VerificationCode} = @verificationCode,
                    {UserSchema.UpdatedAt} = @updatedAt,
                    {UserSchema.RoleId} = @roleId
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters =
            [
                new("id", user.Id),
                new("username", user.Username),
                new("fullName", user.FullName),
                new("phoneNumber", user.PhoneNumber),
                new("email", user.Email),
                new("passwordHash", user.PasswordHash),
                new("verificationCode", user.VerificationCode),
                new("updatedAt", user.UpdatedAt),
                new("roleId", user.RoleId)
            ];

            _logger.CustomLogDebugSqlExecution(query, [.. parameters]);

            await _db.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
