using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Internal.Conveters;
using Infrastructure.Schema.User;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

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

            await _db.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Simple)} 
                FROM {UserSchema.TableName};
            ";

            using var reader = await _db.ExecuteReaderAsync(query);

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

            await _db.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
