using Contracts.Objects.Dtos.Roles;

namespace Contracts.Interfaces.Infrastructure.Queries
{
    public interface IRoleQuery
    {
        Task<IReadOnlyList<RoleSimpleDto>> GetAllAsync();
        Task<RoleInfoDto?> GetByIdAsync(int id);
    }
}