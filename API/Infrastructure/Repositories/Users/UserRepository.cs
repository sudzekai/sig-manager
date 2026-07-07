using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Users;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
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
            var query = SqlQuery.Insert(UserSchema.TableName, UserSelects.Insertation) + "\nSELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = [
                UserSchema.Username.ToMysqlParameter(user.Username),
                UserSchema.FullName.ToMysqlParameter(user.FullName),
                UserSchema.PhoneNumber.ToMysqlParameter(user.PhoneNumber),
                UserSchema.PhoneNumberLastFour.ToMysqlParameter(user.PhoneNumberLastFour),
                UserSchema.Email.ToMysqlParameter(user.Email),
                UserSchema.PasswordHash.ToMysqlParameter(user.PasswordHash),
                UserSchema.CreatedAt.ToMysqlParameter(user.CreatedAt),
                UserSchema.UpdatedAt.ToMysqlParameter(user.UpdatedAt),
                UserSchema.RoleId.ToMysqlParameter(user.RoleId),
            ];

            Activity.Current?.SetSqlTag(DbOperation.INSERT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return Convert.ToInt32(idObj);
        }

        public async Task DeleteAsync(int id)
        {
            var query = SqlQuery.Delete(UserSchema.TableName, [UserSchema.Id]);

            MySqlParameter[] parameters = [UserSchema.Id.ToMysqlParameter(id)];

            Activity.Current?.SetSqlTag(DbOperation.DELETE, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<User?> GetAsync(int id)
        {
            var query = SqlQuery.Select(UserSchema.TableName, UserSelects.Full, [UserSchema.Id]);

            MySqlParameter[] parameters = [UserSchema.Id.ToMysqlParameter(id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return User.Restore(
                    id,
                    reader.GetString(UserSchema.Username),
                    reader.GetString(UserSchema.FullName),
                    reader.GetString(UserSchema.Email),
                    reader.GetString(UserSchema.PhoneNumber),
                    reader.GetString(UserSchema.PhoneNumberLastFour),
                    reader.GetString(UserSchema.PasswordHash),
                    reader.GetString(UserSchema.VerificationCode),
                    reader.GetDateTime(UserSchema.CreatedAt),
                    reader.GetDateTime(UserSchema.UpdatedAt),
                    reader.GetInt32(UserSchema.RoleId)
                );

            return null;
        }

        public async Task<int?> GetIdByEmailAsync(string email)
        {
            var query = SqlQuery.Select(UserSchema.TableName, [UserSchema.Id], [UserSchema.Email]);

            MySqlParameter[] parameters = [UserSchema.Email.ToMysqlParameter(email)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            if (idObj is null)
                return null;

            return Convert.ToInt32(idObj);
        }

        public async Task<int?> GetIdByPhoneNumberAsync(string phoneNumber)
        {
            var query = SqlQuery.Select(UserSchema.TableName, [UserSchema.Id], [UserSchema.PhoneNumber]);

            MySqlParameter[] parameters = [UserSchema.PhoneNumber.ToMysqlParameter(phoneNumber)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            if (idObj is null)
                return null;

            return Convert.ToInt32(idObj);
        }

        public async Task<int?> GetIdByUsernameAsync(string username)
        {
            var query = SqlQuery.Select(UserSchema.TableName, [UserSchema.Id], [UserSchema.Username]);

            MySqlParameter[] parameters = [UserSchema.Username.ToMysqlParameter(username)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            if (idObj is null)
                return null;

            return Convert.ToInt32(idObj);
        }

        public async Task UpdateAsync(User user)
        {
            var query = SqlQuery.Update(UserSchema.TableName, UserSelects.Full, [UserSchema.Id]);

            MySqlParameter[] parameters =
            [
                UserSchema.Id.ToMysqlParameter(user.Id),
                UserSchema.Username.ToMysqlParameter(user.Username),
                UserSchema.FullName.ToMysqlParameter(user.FullName),
                UserSchema.PhoneNumber.ToMysqlParameter(user.PhoneNumber),
                UserSchema.PhoneNumberLastFour.ToMysqlParameter(user.PhoneNumberLastFour),
                UserSchema.Email.ToMysqlParameter(user.Email),
                UserSchema.PasswordHash.ToMysqlParameter(user.PasswordHash),
                UserSchema.VerificationCode.ToMysqlParameter(user.VerificationCode),
                UserSchema.CreatedAt.ToMysqlParameter(user.CreatedAt),
                UserSchema.UpdatedAt.ToMysqlParameter(user.UpdatedAt),
                UserSchema.RoleId.ToMysqlParameter(user.RoleId),
            ];

            Activity.Current?.SetSqlTag(DbOperation.UPDATE, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
