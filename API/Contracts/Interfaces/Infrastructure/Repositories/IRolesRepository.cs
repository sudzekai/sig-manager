using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetAsync(int id);
        Task<int> AddAsync(Role role);
        Task DeleteAsync(int id);
        Task UpdateAsync(Role role);
    }
}
