using Domain.Models.Rights;
using Domain.ValueObjects.Rights;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IRightRepository
    {
        Task<RightId> AddAsync(Right right);
        Task<bool> DeleteAsync(RightId id);
        Task<Right?> GetAsync(RightId id);
        Task<RightId?> GetIdByCode(RightCode code);
        Task UpdateAsync(Right right);
    }
}
