using Domain.Models.Parks;
using Domain.ValueObjects.Parks;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IParkRepository
    {
        Task<ParkId> AddAsync(Park park);
        Task<bool> DeleteAsync(ParkId id);
        Task<Park?> GetAsync(ParkId id);
        Task<ParkId?> GetIdByNameAsync(ParkName name);
        Task UpdateAsync(Park park);
    }
}