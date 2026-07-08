using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.TicketShifts;
using Domain.ValueObjects.Shifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.TicketShifts
{
    public class TicketShiftRepositoryDecorator(ITicketShiftRepository inner, ILogger<ITicketShiftRepository> logger) : ITicketShiftRepository
    {
        public async Task<ShiftId> AddAsync(TicketShift ticketShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("ticketShift", "create");

            logger.LogInformation("Создание записи о билетной смене");

            return await inner.AddAsync(ticketShift);
        }

        public async Task<bool> DeleteAsync(ShiftId shiftId)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("ticketShift", "delete");

            logger.LogInformation("Удаление записи о билетной смене с shift_id {shift_id}", shiftId.Value);

            return await inner.DeleteAsync(shiftId);
        }

        public async Task<TicketShift?> GetAsync(ShiftId shiftId)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("ticketShift", "get");

            logger.LogInformation("Восстановление записи о билетной смене с shift_id {shift_id}", shiftId.Value);

            return await inner.GetAsync(shiftId);
        }

        public async Task UpdateAsync(TicketShift ticketShift)
        {
            using var activity = Telemetry.Repository.StartRepositoryActivity("ticketShift", "update");

            logger.LogInformation("Обновление записи о билетной смене с shift_id {shift_id}", ticketShift.ShiftId.Value);

            await inner.UpdateAsync(ticketShift);
        }
    }
}
