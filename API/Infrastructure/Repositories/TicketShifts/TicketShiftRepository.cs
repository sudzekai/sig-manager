using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.TicketShifts;
using Domain.ValueObjects.Shifts;

namespace Infrastructure.Repositories.TicketShifts
{
    public class TicketShiftsRepository(IDbContext db) : ITicketShiftRepository
    {
        public Task<ShiftId> AddAsync(TicketShift ticketShift)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task<TicketShift?> GetAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TicketShift ticketShift)
        {
            throw new NotImplementedException();
        }
    }
}
