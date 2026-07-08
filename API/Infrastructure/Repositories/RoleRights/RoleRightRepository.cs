using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.RoleRight;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.RoleRight;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.RoleRights
{
    public class RoleRightRepository(IDbContext db) : IRoleRightRepository
    {
        public async Task<bool> AddAsync(RoleRight roleRight)
        {
            var query = SqlQuery.Insert(RoleRightSchema.TableName, RoleRightSelects.Insertation);

            MySqlParameter[] parameters = [
                RoleRightSchema.RoleId.ToMysqlParameter(roleRight.RoleId.Value),
                RoleRightSchema.RightId.ToMysqlParameter(roleRight.RightId.Value),
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);

            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<bool> DeleteAsync(RoleRight roleRight)
        {
            var query = SqlQuery.Delete(RoleRightSchema.TableName, RoleRightSelects.Full);

            MySqlParameter[] parameters = [
                RoleRightSchema.RoleId.ToMysqlParameter(roleRight.RoleId.Value),
                RoleRightSchema.RightId.ToMysqlParameter(roleRight.RightId.Value),
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);

            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }
    }
}
