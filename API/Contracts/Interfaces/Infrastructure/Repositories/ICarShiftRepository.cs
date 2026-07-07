using Domain.Models.CarShifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarShiftRepository
    {
        Task<int> AddAsync(CarShift carShift);
        Task DeleteAsync(int id);
        Task UpdateAsync(CarShift carShift);
        Task<CarShift?> GetAsync(int id);
    }
}
