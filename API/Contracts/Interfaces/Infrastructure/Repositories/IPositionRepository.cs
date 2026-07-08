using Domain.Models.Positions;
using Domain.ValueObjects.Positions;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IPositionRepository
    {
        Task<PositionId> AddAsync(Position position);
        Task<bool> DeleteAsync(PositionId id);
        Task<Position?> GetAsync(PositionId id);
        Task<PositionId?> GetIdByName(PositionName name);
        Task UpdateAsync(Position position);
    }
}
