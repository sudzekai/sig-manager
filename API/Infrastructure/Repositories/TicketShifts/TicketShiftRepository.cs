using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.TicketShifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.TicketShift;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.TicketShifts
{
    internal class TicketShiftsRepository(IDbContext db) : ITicketShiftRepository
    {
        public async Task<int> AddAsync(TicketShift ticketshift)
        {
            var query = SqlQuery.Insert(TicketShiftSchema.TableName, TicketShiftSelects.Insertation)
                + "SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = [
                TicketShiftSchema.ShiftId.ToMysqlParameter(ticketshift.ShiftId),
                TicketShiftSchema.FirstTicket.ToMysqlParameter(ticketshift.FirstTicket),
                TicketShiftSchema.TicketPrice.ToMysqlParameter(ticketshift.TicketPrice),
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return Convert.ToInt32(idObj);
        }

        public async Task DeleteAsync(int id)
        {
            var query = SqlQuery.Delete(TicketShiftSchema.TableName, [TicketShiftSchema.ShiftId]);
            MySqlParameter[] parameters = [TicketShiftSchema.ShiftId.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<TicketShift?> GetAsync(int id)
        {
            var query = SqlQuery.Select(TicketShiftSchema.TableName, TicketShiftSelects.Full, [TicketShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [TicketShiftSchema.ShiftId.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return TicketShift.Restore(
                    id,
                    reader.GetInt32(TicketShiftSchema.FirstTicket),
                    reader.GetNullableInt32(TicketShiftSchema.LastTicket),
                    reader.GetDecimal(TicketShiftSchema.TicketPrice)
                );

            return null;
        }

        public async Task UpdateAsync(TicketShift ticketshift)
        {
            var query = SqlQuery.Update(TicketShiftSchema.TableName, TicketShiftSelects.Full, [TicketShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                TicketShiftSchema.FirstTicket.ToMysqlParameter(ticketshift.FirstTicket),
                TicketShiftSchema.LastTicket.ToMysqlParameter(ticketshift.LastTicket),
                TicketShiftSchema.TicketPrice.ToMysqlParameter(ticketshift.TicketPrice)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
