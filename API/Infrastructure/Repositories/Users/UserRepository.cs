using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Infrastructure.Internal.Conveters;
using Infrastructure.Schema.User;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using System.Diagnostics;
using System.Text;
using Shared.Types.Enums;
using Shared.App;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository(IDbContext db) : IUserRepository
    {
        public async Task<int> CreateAsync(User user)
        {
            var query = @$"
                INSERT INTO {UserSchema.TableName} ({string.Join(", ", UserSelects.Insertation)}) 
                VALUES (@username, @fullName, @phoneNumber, @phoneNumberLastFour, @email, @passwordHash, @createdAt, @updatedAt, @roleId);
                SELECT LAST_INSERT_ID();
            ";

            MySqlParameter[] parameters = [
                new("username", user.Username),
                new("fullName", user.FullName),
                new("phoneNumber", user.PhoneNumber),
                new("phoneNumberLastFour", user.PhoneNumberLastFour),
                new("email", user.Email),
                new("passwordHash", user.PasswordHash),
                new("createdAt", user.CreatedAt),
                new("updatedAt", user.UpdatedAt),
                new("roleId", user.RoleId)
            ];

            Activity.Current?.SetSqlTag(DbOperation.INSERT, parameters.Length);

            var idObj = await db.ExecuteScalarAsync(query, parameters);

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

            Activity.Current?.SetSqlTag(DbOperation.DELETE, parameters.Length);

            await db.ExecuteNonQueryAsync(query, parameters);
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
                    AND ({UserSchema.Username} LIKE @searchTerm 
                    OR {UserSchema.FullName} LIKE @searchTerm 
                    OR {UserSchema.Email} LIKE @searchTerm 
                    OR {UserSchema.PhoneNumberLastFour} LIKE @searchTerm)
                ");

                parameters.Add(new("searchTerm", $"{request.SearchTerm}%"));
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

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Count);

            using var reader = await db.ExecuteReaderAsync(query.ToString(), [.. parameters]);

            var result = UserConverter.ListFromReader(reader);

            return result;
        }

        public async Task<User?> GetInfoByEmailAsync(string email)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Email} = @email;
            ";

            MySqlParameter[] parameters = [new("email", email)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetInfoByIdAsync(int id)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetInfoByPhoneNumberAsync(string phoneNumber)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.PhoneNumber} = @phoneNumber;
            ";

            MySqlParameter[] parameters = [new("phoneNumber", phoneNumber)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = UserConverter.FromReader(reader);

            return result;
        }

        public async Task<User?> GetInfoByUsernameAsync(string username)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Username} = @username;
            ";

            MySqlParameter[] parameters = [new("username", username)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);
            
            using var reader = await db.ExecuteReaderAsync(query, parameters);

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

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);
            
            using var reader = await db.ExecuteReaderAsync(query, parameters);

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
                    {UserSchema.PhoneNumberLastFour} = @phoneNumberLastFour,
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
                new("phoneNumberLastFour", user.PhoneNumber[^4..]),
                new("email", user.Email),
                new("passwordHash", user.PasswordHash),
                new("verificationCode", user.VerificationCode),
                new("updatedAt", user.UpdatedAt),
                new("roleId", user.RoleId)
            ];

            Activity.Current?.SetSqlTag(DbOperation.UPDATE, parameters.Length);
            
            await db.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
