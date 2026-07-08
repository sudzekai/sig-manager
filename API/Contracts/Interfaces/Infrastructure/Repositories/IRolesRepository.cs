using Domain.Models.Roles;
using Domain.ValueObjects.Roles;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleId> AddAsync(Role role);
        Task<bool> DeleteAsync(RoleId id);
        Task<Role?> GetAsync(RoleId id);
        Task<RoleId?> GetIdByName(RoleName name);
        Task UpdateAsync(Role role);
    }
}
