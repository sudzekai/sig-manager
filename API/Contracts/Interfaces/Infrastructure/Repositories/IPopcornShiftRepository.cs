using Domain.Models.PopcornShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IPopcornShiftRepository
    {
        Task<ShiftId> AddAsync(PopcornShift popcornShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<PopcornShift?> GetAsync(ShiftId id);
        Task UpdateAsync(PopcornShift popcornShift);
    }
}
