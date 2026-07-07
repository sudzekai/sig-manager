using Domain.Models.InfoShifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IInfoShiftRepository
    {
        Task<int> AddAsync(InfoShift infoShift);
        Task DeleteAsync(int id);
        Task UpdateAsync(InfoShift infoShift);
        Task<InfoShift?> GetAsync(int id);
    }
}
