using Domain.Models.BouncerShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IBouncerShiftRepository
    {
        Task<ShiftId> AddAsync(BouncerShift bouncerShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<BouncerShift?> GetAsync(ShiftId id);
        Task UpdateAsync(BouncerShift bouncerShift);
    }
}
