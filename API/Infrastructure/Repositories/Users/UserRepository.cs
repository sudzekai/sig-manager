using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Users;
using Domain.ValueObjects.Roles;
using Domain.ValueObjects.Users;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.User;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository(IDbContext db) : IUserRepository
    {
        public async Task<UserId> AddAsync(User user)
        {
            var query = SqlQuery.Insert(UserSchema.TableName, UserSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                UserSchema.Username.ToMysqlParameter(user.Username.Value),
                UserSchema.FullName.ToMysqlParameter(user.FullName.Value),
                UserSchema.PhoneNumber.ToMysqlParameter(user.PhoneNumber.Value),
                UserSchema.PhoneNumberLastFour.ToMysqlParameter(user.PhoneNumber.LastFour),
                UserSchema.Email.ToMysqlParameter(user.Email.Value),
                UserSchema.PasswordHash.ToMysqlParameter(user.PasswordHash.Value),
                UserSchema.CreatedAt.ToMysqlParameter(user.CreatedAt),
                UserSchema.UpdatedAt.ToMysqlParameter(user.UpdatedAt),
                UserSchema.RoleId.ToMysqlParameter(user.RoleId.Value),
                UserSchema.VerificationCode.ToMysqlParameter(UserVerificationCode.Empty.Value)
                ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return UserId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(UserId id)
        {
            var query = SqlQuery.Delete(UserSchema.TableName, [UserSchema.Id]);

            MySqlParameter[] parameters = [
                UserSchema.Id.ToMysqlParameter(id.Value)
                ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<User?> GetAsync(UserId id)
        {
            var query = SqlQuery.Select(UserSchema.TableName, UserSelects.Full, [UserSchema.Id]);

            MySqlParameter[] parameters = [
                UserSchema.Id.ToMysqlParameter(id.Value)
                ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return User.Restore(
                    id,
                    Username.FromValue(reader.GetString(UserSchema.Username)),
                    UserFullName.FromValue(reader.GetString(UserSchema.FullName)),
                    UserEmail.FromValue(reader.GetString(UserSchema.Email)),
                    UserPhoneNumber.FromValue(reader.GetString(UserSchema.PhoneNumber)),
                    UserPasswordHash.FromValue(reader.GetString(UserSchema.PasswordHash)),
                    UserVerificationCode.FromValue(reader.GetString(UserSchema.VerificationCode)),
                    reader.GetDateTime(UserSchema.CreatedAt),
                    reader.GetDateTime(UserSchema.UpdatedAt),
                    RoleId.FromValue(reader.GetInt32(UserSchema.RoleId))
                );

            return null;
        }

        public async Task<UserId?> GetIdByEmailAsync(UserEmail email)
        {
            var query = SqlQuery.Select(UserSchema.TableName, [UserSchema.Id], [UserSchema.Email]);

            MySqlParameter[] parameters = [UserSchema.Email.ToMysqlParameter(email.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : UserId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<UserId?> GetIdByPhoneNumberAsync(UserPhoneNumber phoneNumber)
        {
            var query = SqlQuery.Select(UserSchema.TableName, [UserSchema.Id], [UserSchema.PhoneNumber]);

            MySqlParameter[] parameters = [UserSchema.PhoneNumber.ToMysqlParameter(phoneNumber.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : UserId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<UserId?> GetIdByUsernameAsync(Username username)
        {
            var query = SqlQuery.Select(UserSchema.TableName, [UserSchema.Id], [UserSchema.Username]);

            MySqlParameter[] parameters = [UserSchema.Username.ToMysqlParameter(username.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : UserId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task UpdateAsync(User user)
        {
            var query = SqlQuery.Update(UserSchema.TableName, UserSelects.Full, [UserSchema.Id]);

            MySqlParameter[] parameters = [
                UserSchema.Username.ToMysqlParameter(user.Username.Value),
                UserSchema.FullName.ToMysqlParameter(user.FullName.Value),
                UserSchema.PhoneNumber.ToMysqlParameter(user.PhoneNumber.Value),
                UserSchema.PhoneNumberLastFour.ToMysqlParameter(user.PhoneNumber.LastFour),
                UserSchema.Email.ToMysqlParameter(user.Email.Value),
                UserSchema.PasswordHash.ToMysqlParameter(user.PasswordHash.Value),
                UserSchema.CreatedAt.ToMysqlParameter(user.CreatedAt),
                UserSchema.UpdatedAt.ToMysqlParameter(user.UpdatedAt),
                UserSchema.RoleId.ToMysqlParameter(user.RoleId.Value),
                UserSchema.VerificationCode.ToMysqlParameter(user.VerificationCode.Value),
                UserSchema.Id.ToMysqlParameter(user.Id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
