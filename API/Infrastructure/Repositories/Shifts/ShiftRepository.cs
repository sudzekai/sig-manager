using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.CarShifts;
using Domain.Models.Shifts;
using Domain.ValueObjects.Parks;
using Domain.ValueObjects.Shifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Shift;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.Shifts
{
    public class ShiftRepository(IDbContext db) : IShiftRepository
    {
        public async Task<ShiftId> AddAsync(Shift shift)
        {
            var query = SqlQuery.Insert(ShiftSchema.TableName, ShiftSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                ShiftSchema.Type.ToMysqlParameter(shift.Type.Value),
                ShiftSchema.CreatedAt.ToMysqlParameter(shift.CreatedAt),
                ShiftSchema.UpdatedAt.ToMysqlParameter(shift.UpdatedAt)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return ShiftId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            var query = SqlQuery.Delete(ShiftSchema.TableName, [ShiftSchema.Id]);

            MySqlParameter[] parameters = [
                ShiftSchema.Id.ToMysqlParameter(id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<Shift?> GetAsync(ShiftId id)
        {
            var query = SqlQuery.Select(ShiftSchema.TableName, ShiftSelects.Full, [ShiftSchema.Id]);

            MySqlParameter[] parameters = [ShiftSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Shift.Restore(
                    id,
                    ShiftType.FromValue(reader.GetString(ShiftSchema.Type)),
                    ShiftStatus.FromValue(reader.GetString(ShiftSchema.Status)),
                    reader.GetDateTime(ShiftSchema.CreatedAt),
                    reader.GetDateTime(ShiftSchema.UpdatedAt),
                    reader.GetNullableDateTime(ShiftSchema.ClosedAt)
                );

            return null;
        }

        public async Task UpdateAsync(Shift shift)
        {
            var query = SqlQuery.Update(ShiftSchema.TableName, ShiftSelects.Full, [ShiftSchema.Id]);

            MySqlParameter[] parameters = [
                ShiftSchema.Type.ToMysqlParameter(shift.Type.Value),
                ShiftSchema.Status.ToMysqlParameter(shift.Status.Value),
                ShiftSchema.UpdatedAt.ToMysqlParameter(shift.UpdatedAt),
                ShiftSchema.ClosedAt.ToMysqlParameter(shift.ClosedAt),
                ShiftSchema.CreatedAt.ToMysqlParameter(shift.CreatedAt),
                ShiftSchema.Id.ToMysqlParameter(shift.Id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
