using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Shifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Shift;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.Shifts
{
    internal class ShiftRepository(IDbContext db) : IShiftRepository
    {
        public async Task<int> AddAsync(Shift shift)
        {
            var query = SqlQuery.Insert(ShiftSchema.TableName, ShiftSelects.Insertation)
                + "SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = [
                ShiftSchema.Type.ToMysqlParameter(shift.Type),
                ShiftSchema.CreatedAt.ToMysqlParameter(shift.CreatedAt)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return Convert.ToInt32(idObj);
        }

        public async Task DeleteAsync(int id)
        {
            var query = SqlQuery.Delete(ShiftSchema.TableName, [ShiftSchema.Id]);
            MySqlParameter[] parameters = [ShiftSchema.Id.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<Shift?> GetAsync(int id)
        {
            var query = SqlQuery.Select(ShiftSchema.TableName, ShiftSelects.Full, [ShiftSchema.Id]);

            MySqlParameter[] parameters = [ShiftSchema.Id.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Shift.Restore(
                    id,
                    reader.GetString(ShiftSchema.Type),
                    reader.GetString(ShiftSchema.Status),
                    reader.GetDateTime(ShiftSchema.CreatedAt),
                    reader.GetDateTime(ShiftSchema.UpdatedAt),
                    reader.GetNullableDateTime(ShiftSchema.ClosedAt)
                );
            }
         
            return null;
        }

        public async Task UpdateAsync(Shift shift)
        {
            var query = SqlQuery.Update(ShiftSchema.TableName, ShiftSelects.Full, [ShiftSchema.Id]);

            MySqlParameter[] parameters = [
                ShiftSchema.Type.ToMysqlParameter(shift.Type),
                ShiftSchema.Status.ToMysqlParameter(shift.Status),
                ShiftSchema.CreatedAt.ToMysqlParameter(shift.CreatedAt),
                ShiftSchema.UpdatedAt.ToMysqlParameter(shift.UpdatedAt),
                ShiftSchema.ClosedAt.ToMysqlParameter(shift.ClosedAt)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
