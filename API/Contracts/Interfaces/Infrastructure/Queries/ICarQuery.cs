using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Interfaces.Infrastructure.Queries
{
    public interface ICarQuery
    {
        Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request);
        Task<CarInfoDto?> GetByIdAsync(int id);
    }
}