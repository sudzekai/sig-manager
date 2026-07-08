using Domain.Models.TrainShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ITrainShiftRepository
    {
        Task<ShiftId> AddAsync(TrainShift trainShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<TrainShift?> GetAsync(ShiftId id);
        Task UpdateAsync(TrainShift trainShift);
    }
}
