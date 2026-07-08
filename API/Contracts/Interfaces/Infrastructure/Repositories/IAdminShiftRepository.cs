using Domain.Models.AdminShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IAdminShiftRepository
    {
        Task<ShiftId> AddAsync(AdminShift adminShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<AdminShift?> GetAsync(ShiftId id);
        Task UpdateAsync(AdminShift adminShift);
    }
}
