using Domain.Models.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IShiftRepository
    {
        Task<int> AddAsync(Shift shift);
        Task DeleteAsync(int id);
        Task UpdateAsync(Shift shift);
        Task<Shift?> GetAsync(int id);
    }
}
