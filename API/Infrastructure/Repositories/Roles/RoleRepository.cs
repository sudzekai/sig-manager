using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Right;
using Infrastructure.Schema.Role;
using Infrastructure.Schema.RoleRight;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using System.Diagnostics;

namespace Infrastructure.Repositories.Roles
{
    internal class RoleRepository(IDbContext db) : IRoleRepository
    {
        public async Task<int> AddAsync(Role role)
        {
            var query = SqlQuery.Insert(RoleSchema.TableName, RoleSelects.Insertation);

            MySqlParameter[] parameters = [RoleSchema.Name.ToMysqlParameter(role.Name)];

            Activity.Current?.SetSqlTag(Shared.Types.Enums.DbOperation.INSERT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, [.. parameters]);
            var idObj = await command.ExecuteScalarAsync();

            return Convert.ToInt32(idObj);
        }

        public async Task DeleteAsync(int id)
        {
            var query = SqlQuery.Delete(RoleSchema.Name, [RoleSchema.Id]);

            MySqlParameter[] parameters = [RoleSchema.Id.ToMysqlParameter(id)];

            Activity.Current?.SetSqlTag(Shared.Types.Enums.DbOperation.DELETE, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, [.. parameters]);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<Role?> GetAsync(int id)
        {
            throw new NotImplementedException();

            var query = $"""
                SELECT
                    {RoleSchema.TableName}.{RoleSchema.Id} AS role_id,
                    {RoleSchema.TableName}.{RoleSchema.Name} AS role_name,
                    {RightSchema.TableName}.{RightSchema.Id} AS right_id,
                    {RightSchema.TableName}.{RightSchema.Code} AS right_code
                FROM
                    {RoleSchema.TableName}
                    LEFT JOIN {RoleRightSchema.TableName}
                        ON {RoleRightSchema.TableName}.{RoleRightSchema.RoleId} = {RoleSchema.TableName}.{RoleSchema.Id}
                    LEFT JOIN {RightSchema.TableName}
                        ON {RightSchema.TableName}.{RightSchema.Id} = {RoleRightSchema.TableName}.{RoleRightSchema.RightId}
                WHERE
                    {RoleSchema.TableName}.{RoleSchema.Id} = @roleId;
                """;

            MySqlParameter[] parameters = [new("roleId", id)];

            Role? role = null;

            Activity.Current?.SetSqlTag(Shared.Types.Enums.DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query, [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            var roleIdOrdinal = reader.GetOrdinal("role_id");
            var roleNameOrdinal = reader.GetOrdinal("role_name");
            var rightIdOrdinal = reader.GetOrdinal("right_id");
            var rightCodeOrdinal = reader.GetOrdinal("right_code");

            if (await reader.ReadAsync())
            {
                role ??= Role.Restore(
                    reader.GetInt32(roleIdOrdinal),
                    reader.GetString(roleNameOrdinal)
                );

                if (reader.GetNullableInt32(rightIdOrdinal) is var rightId && rightId.HasValue)
                {
                    role.AddRight(
                        Right.Restore(
                            rightId.Value,
                            reader.GetString(rightCodeOrdinal)
                        )
                    );
                }
            }

            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            throw new NotImplementedException();

            var query = $"""
                SELECT {string.Join(", ", RightSelects.Full)}
                FROM {RightSchema.TableName}
                INNER JOIN
                    {RoleRightSchema.TableName}
                    ON
                        {RoleRightSchema.RightId} = {RightSchema.TableName}.{RightSchema.Id}
                WHERE
                    {RoleRightSchema.RoleId} = @roleId
                """;


            await using var command = await db.CreateCommandAsync(query);
            await command.ExecuteReaderAsync();
        }
    }
}
