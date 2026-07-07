using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Right;
using Contracts.Objects.Dtos.Roles;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.Right;
using Infrastructure.Schema.Role;
using Infrastructure.Schema.RoleRight;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using System.Diagnostics;

namespace Infrastructure.Queries.Roles
{
    public class RoleQuery(IDbContext db) : IRoleQuery
    {
        public async Task<IReadOnlyList<RoleSimpleDto>> GetAllAsync()
        {
            var query = $"""
                SELECT {string.Join(", ", RoleSelects.Full)}
                FROM {RoleSchema.TableName}
                """;

            Activity.Current?.SetSqlTag(Shared.Types.Enums.DbOperation.INSERT, 0);

            List<RoleSimpleDto> result = [];

            await using var command = await db.CreateCommandAsync(query);
            await using var reader = await command.ExecuteReaderAsync();

            var idOrdinal = reader.GetOrdinal(RoleSchema.Id);
            var nameOrdinal = reader.GetOrdinal(RoleSchema.Name);

            while (await reader.ReadAsync())
            {
                result.Add(
                    new(
                        reader.GetInt32(idOrdinal),
                        reader.GetString(nameOrdinal)
                    )
                );
            }

            return result;
        }

        public async Task<RoleInfoDto?> GetByIdAsync(int id)
        {
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

            int roleId = default;
            string roleName = "";

            List<RightDto> rights = [];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            var rightIdOrdinal = reader.GetOrdinal("right_id");
            var rightCodeOrdinal = reader.GetOrdinal("right_code");

            while (await reader.ReadAsync())
            {
                if (roleId == default)
                {
                    roleId = reader.GetInt32("role_id");
                    roleName = reader.GetString("role_name");
                }

                if (reader.GetNullableInt32(rightIdOrdinal) is var rightId && rightId.HasValue)
                {
                    rights.Add(
                        new(
                            rightId.Value,
                            reader.GetString(rightCodeOrdinal)
                        )
                    );
                }
            }

            return roleId == default ? null : new(roleId, roleName, rights);
        }
    }
}
