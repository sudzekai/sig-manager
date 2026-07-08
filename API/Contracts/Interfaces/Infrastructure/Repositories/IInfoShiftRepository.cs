using Domain.Models.InfoShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IInfoShiftRepository
    {
        Task<ShiftId> AddAsync(InfoShift infoShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<InfoShift?> GetAsync(ShiftId id);
        Task UpdateAsync(InfoShift infoShift);
    }
}
