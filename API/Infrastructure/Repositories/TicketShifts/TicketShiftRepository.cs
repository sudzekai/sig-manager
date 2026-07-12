using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.TicketShifts;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Ticket;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.TicketShift;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.TicketShifts
{
    public class TicketShiftsRepository(IDbContext db) : ITicketShiftRepository
    {
        public async Task<ShiftId> AddAsync(TicketShift ticketShift)
        {
            var query = SqlQuery.Insert(TicketShiftSchema.TableName, TicketShiftSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                TicketShiftSchema.ShiftId.ToMysqlParameter(ticketShift.ShiftId.Value),
                TicketShiftSchema.FirstTicket.ToMysqlParameter(ticketShift.FirstTicket.Value),
                TicketShiftSchema.TicketPrice.ToMysqlParameter(ticketShift.TicketPrice.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return ShiftId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            var query = SqlQuery.Delete(TicketShiftSchema.TableName, [TicketShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                TicketShiftSchema.ShiftId.ToMysqlParameter(id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<TicketShift?> GetAsync(ShiftId id)
        {
            var query = SqlQuery.Select(TicketShiftSchema.TableName, TicketShiftSelects.Full, [TicketShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [TicketShiftSchema.ShiftId.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var lastTicket = reader.GetNullableInt32(TicketShiftSchema.LastTicket);
                return TicketShift.Restore(
                    id,
                    ShiftFirstTicket.FromValue(reader.GetInt32(TicketShiftSchema.FirstTicket)),
                    lastTicket is null ? null : ShiftLastTicket.FromValue(lastTicket.Value),
                    ShiftTicketPrice.FromValue(reader.GetDecimal(TicketShiftSchema.TicketPrice))
                );
            }

            return null;
        }

        public async Task UpdateAsync(TicketShift ticketShift)
        {
            var query = SqlQuery.Update(TicketShiftSchema.TableName, TicketShiftSelects.Full, [TicketShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                TicketShiftSchema.FirstTicket.ToMysqlParameter(ticketShift.FirstTicket.Value),
                TicketShiftSchema.TicketPrice.ToMysqlParameter(ticketShift.TicketPrice.Value),
                TicketShiftSchema.LastTicket.ToMysqlParameter(ticketShift.LastTicket?.Value),
                TicketShiftSchema.ShiftId.ToMysqlParameter(ticketShift.ShiftId.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
