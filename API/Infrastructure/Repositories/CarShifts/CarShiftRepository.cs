using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.CarShifts;
using Domain.ValueObjects.Parks;
using Domain.ValueObjects.Shifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.CarShift;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.CarShifts
{
    public class CarShiftRepository(IDbContext db) : ICarShiftRepository
    {
        public async Task<ShiftId> AddAsync(CarShift carShift)
        {
            var query = SqlQuery.Insert(CarShiftSchema.TableName, CarShiftSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                CarShiftSchema.ShiftId.ToMysqlParameter(carShift.ShiftId.Value),
                CarShiftSchema.ParkId.ToMysqlParameter(carShift.ParkId.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return ShiftId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            var query = SqlQuery.Delete(CarShiftSchema.TableName, [CarShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                CarShiftSchema.ShiftId.ToMysqlParameter(id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<CarShift?> GetAsync(ShiftId id)
        {
            var query = SqlQuery.Select(CarShiftSchema.TableName, CarShiftSelects.Full, [CarShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [CarShiftSchema.ShiftId.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return CarShift.Restore(
                    id,
                    ParkId.FromValue(reader.GetInt32(CarShiftSchema.ParkId))
                );

            return null;
        }

        public async Task UpdateAsync(CarShift carShift)
        {
            var query = SqlQuery.Update(CarShiftSchema.TableName, CarShiftSelects.Full, [CarShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                CarShiftSchema.ParkId.ToMysqlParameter(carShift.ParkId.Value),
                CarShiftSchema.ShiftId.ToMysqlParameter(carShift.ShiftId.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
