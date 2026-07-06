using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.User;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using Shared.Types.Enums;
using System.Diagnostics;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository(IDbContext db) : IUserRepository
    {
        public async Task<int> AddAsync(User user)
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

        public async Task DeleteAsync(int id)
        {
            var query = $@"
                DELETE FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.DELETE, parameters.Length);

            await db.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<User?> GetAsync(int id)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Full)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            User? result = null;

            await using (var reader = (MySqlDataReader)await db.ExecuteReaderAsync(query, parameters))
            {
                while (await reader.ReadAsync())
                {
                    result = User.Restore(
                        reader.GetInt32(UserSchema.Id),
                        reader.GetString(UserSchema.Username),
                        reader.GetString(UserSchema.FullName),
                        reader.GetString(UserSchema.Email),
                        reader.GetString(UserSchema.PhoneNumber),
                        reader.GetString(UserSchema.PhoneNumberLastFour),
                        reader.GetString(UserSchema.PasswordHash),
                        reader.GetNullableString(UserSchema.VerificationCode),
                        reader.GetDateTime(UserSchema.CreatedAt),
                        reader.GetDateTime(UserSchema.UpdatedAt),
                        reader.GetInt32(UserSchema.RoleId)
                    );
                }
            }

            return result;
        }

        public async Task<int?> GetIdByEmailAsync(string email)
        {
            var query = $@"
                SELECT {UserSchema.Id}
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Email} = @email;
            ";

            MySqlParameter[] parameters = [new("email", email)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            var idObj = await db.ExecuteScalarAsync(query, parameters);

            if (idObj is null)
                return null;

            return Convert.ToInt32(idObj);
        }

        public async Task<int?> GetIdByPhoneNumberAsync(string phoneNumber)
        {
            var query = $@"
                SELECT {UserSchema.Id}
                FROM {UserSchema.TableName}
                WHERE {UserSchema.PhoneNumber} = @phoneNumber;
            ";

            MySqlParameter[] parameters = [new("phoneNumber", phoneNumber)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            var idObj = await db.ExecuteScalarAsync(query, parameters);

            if (idObj is null)
                return null;

            return Convert.ToInt32(idObj);
        }

        public async Task<int?> GetIdByUsernameAsync(string username)
        {
            var query = $@"
                SELECT {UserSchema.Id}
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Username} = @username;
            ";

            MySqlParameter[] parameters = [new("username", username)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            var idObj = await db.ExecuteScalarAsync(query, parameters);

            if (idObj is null)
                return null;

            return Convert.ToInt32(idObj);
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
