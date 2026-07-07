using Domain.Models.TicketShifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ITicketShiftRepository
    {
        Task<int> AddAsync(TicketShift ticketShift);
        Task DeleteAsync(int id);
        Task UpdateAsync(TicketShift ticketShift);
        Task<TicketShift?> GetAsync(int id);
    }
}
