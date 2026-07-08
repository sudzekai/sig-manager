using Domain.Models.RoleRight;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IRoleRightRepository
    {
        Task<bool> AddAsync(RoleRight roleRight);
        Task<bool> DeleteAsync(RoleRight roleRight);
    }
}
