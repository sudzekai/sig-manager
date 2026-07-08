using Domain.Models.TicketShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ITicketShiftRepository
    {
        Task<ShiftId> AddAsync(TicketShift ticketShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<TicketShift?> GetAsync(ShiftId id);
        Task UpdateAsync(TicketShift ticketShift);
    }
}
