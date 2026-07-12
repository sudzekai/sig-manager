using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.UserShifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.UserShift;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.UserShifts
{
    public class UserShiftRepository(IDbContext db) : IUserShiftRepository
    {
        public async Task AddAsync(UserShift userShift)
        {
            var query = SqlQuery.Insert(UserShiftSchema.TableName, UserShiftSelects.Insertation);

            MySqlParameter[] parameters = [
                UserShiftSchema.UserId.ToMysqlParameter(userShift.UserId.Value),
                UserShiftSchema.ShiftId.ToMysqlParameter(userShift.ShiftId.Value),
                UserShiftSchema.PositionId.ToMysqlParameter(userShift.PositionId.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(UserShift userShift)
        {
            var query = SqlQuery.Delete(UserShiftSchema.TableName, UserShiftSelects.Insertation);

            MySqlParameter[] parameters = [
                UserShiftSchema.UserId.ToMysqlParameter(userShift.UserId.Value),
                UserShiftSchema.ShiftId.ToMysqlParameter(userShift.ShiftId.Value),
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
