using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Roles;
using Domain.ValueObjects.Roles;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Role;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.Roles
{
    public class RoleRepository(IDbContext db) : IRoleRepository
    {
        public async Task<RoleId> AddAsync(Role role)
        {
            var query = SqlQuery.Insert(RoleSchema.TableName, RoleSelects.Insertation);

            MySqlParameter[] parameters = [
                RoleSchema.Name.ToMysqlParameter(role.Name.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return RoleId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(RoleId id)
        {
            var query = SqlQuery.Delete(RoleSchema.TableName, [RoleSchema.Id]);

            MySqlParameter[] parameters = [
                RoleSchema.Id.ToMysqlParameter(id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<Role?> GetAsync(RoleId id)
        {
            var query = SqlQuery.Select(RoleSchema.TableName, RoleSelects.Full, [RoleSchema.Id]);

            MySqlParameter[] parameters = [
                RoleSchema.Id.ToMysqlParameter(id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Role.Restore(
                    id,
                    RoleName.FromValue(reader.GetString(RoleSchema.Name))
                );

            return null;
        }

        public async Task<RoleId?> GetIdByName(RoleName name)
        {
            var query = SqlQuery.Select(RoleSchema.TableName, [RoleSchema.Id], [RoleSchema.Name]);

            MySqlParameter[] parameters = [
                RoleSchema.Name.ToMysqlParameter(name.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);

            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : RoleId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task UpdateAsync(Role role)
        {
            var query = SqlQuery.Update(RoleSchema.TableName, RoleSelects.Full, [RoleSchema.Id]);

            MySqlParameter[] parameters = [
                RoleSchema.Name.ToMysqlParameter(role.Name.Value),
                RoleSchema.Id.ToMysqlParameter(role.Id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
        }
    }
}
