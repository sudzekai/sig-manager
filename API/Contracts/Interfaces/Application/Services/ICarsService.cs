using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Interfaces.Application.Services
{
    public interface ICarsService
    {
        Task<CarInfoDto> CreateAsync(CarCreateDto createDto);
        Task DeleteByIdAsync(int id);
        Task<CarInfoDto> GetInfoByIdAsync(int id);
        Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request);
        Task UpdateInfoByIdAsync(int id, CarInfoUpdateDto updateDto);
        Task UpdateStatusByIdAsync(int id, CarStatusUpdateDto updateDto);
    }
}
