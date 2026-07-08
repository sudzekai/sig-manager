using Domain.Models.Shifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IShiftRepository
    {
        Task<ShiftId> AddAsync(Shift shift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<Shift?> GetAsync(ShiftId id);
        Task UpdateAsync(Shift shift);
    }
}
