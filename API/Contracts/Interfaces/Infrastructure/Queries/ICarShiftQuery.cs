using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Interfaces.Infrastructure.Queries
{
    public interface ICarShiftQuery
    {
        Task<IReadOnlyList<CarShiftSimpleDto>> GetAllAsync(GetCarShiftsListRequest request);
        Task<CarShiftInfoDto?> GetByIdAsync(int id);
    }
}