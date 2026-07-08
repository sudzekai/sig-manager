using Domain.Models.CarShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarShiftRepository
    {
        Task<ShiftId> AddAsync(CarShift carShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<CarShift?> GetAsync(ShiftId id);
        Task UpdateAsync(CarShift carShift);
    }
}
