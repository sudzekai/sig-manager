using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.CarShifts;
using Domain.Models.InfoShifts;
using Domain.Models.Shifts;
using Domain.Models.TicketShifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.CarShift;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.CarShifts
{
    public class CarShiftRepository(
        IDbContext db,
        IShiftRepository shifts,
        ITicketShiftRepository tickets,
        IInfoShiftRepository infos
        ) : ICarShiftRepository
    {
        public async Task<int> AddAsync(CarShift carshift)
        {
            await db.BeginTransactionAsync();

            var shiftId = await shifts.AddAsync(carshift.Shift);

            var ticketShift = carshift.TicketShift;
            ticketShift.ShiftId = shiftId;

            await tickets.AddAsync(ticketShift);

            var query = SqlQuery.Insert(CarShiftSchema.TableName, CarShiftSelects.Insertation)
                + "SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = [
                CarShiftSchema.ShiftId.ToMysqlParameter(shiftId)
            ];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            var idObj = await command.ExecuteScalarAsync();

            await db.CommitAsync();
            
            return Convert.ToInt32(idObj);

        }

        public async Task DeleteAsync(int id)
        {
            await db.BeginTransactionAsync();

            var query = SqlQuery.Delete(CarShiftSchema.TableName, [CarShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [CarShiftSchema.ShiftId.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await command.ExecuteNonQueryAsync();

            await tickets.DeleteAsync(id);
            await infos.DeleteAsync(id);
            await shifts.DeleteAsync(id);

            await db.CommitAsync();
        }

        public async Task<CarShift?> GetAsync(int id)
        {
            Shift? shift = await shifts.GetAsync(id);

            if (shift is null)
                return null;

            TicketShift? ticketShift = await tickets.GetAsync(id);

            if (ticketShift is null)
                return null;

            InfoShift? infoShift = await infos.GetAsync(id);

            var query = SqlQuery.Select(CarShiftSchema.TableName, CarShiftSelects.Full, [CarShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [CarShiftSchema.ShiftId.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return CarShift.Restore(id, shift, infoShift, ticketShift);
        }

        public async Task UpdateAsync(CarShift carshift)
        {
            await db.BeginTransactionAsync();

            await shifts.UpdateAsync(carshift.Shift);

            if (carshift.InfoShift is not null)
            {
                if (await infos.GetAsync(carshift.ShiftId) is not null)
                    await infos.UpdateAsync(carshift.InfoShift);
                else
                    await infos.AddAsync(carshift.InfoShift);
            }

            await tickets.UpdateAsync(carshift.TicketShift);

            await db.CommitAsync();
        }
    }
}
