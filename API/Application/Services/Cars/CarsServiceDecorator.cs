using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Application.Services.Cars
{
    public class CarsServiceDecorator(ICarsService inner) : ICarsService
    {
        public async Task<CarInfoDto> CreateAsync(CarCreateDto createDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("car", "create");

            var result = await inner.CreateAsync(createDto);

            activity?.SetTag("id", result.Id);

            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Service.StartServiceActivity("car", "delete");

            activity?.SetTag("id", id);

            await inner.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request)
        {
            using var activity = Telemetry.Service.StartServiceActivity("car", "get_all");

            var result = await inner.GetAllAsync(request);

            activity?.SetTag("count", result.Count);

            return result;
        }

        public async Task<CarInfoDto> GetInfoByIdAsync(int id)
        {
            using var activity = Telemetry.Service.StartServiceActivity("car", "get_by_id");

            activity?.SetTag("id", id);

            return await inner.GetInfoByIdAsync(id);
        }

        public async Task UpdateInfoByIdAsync(int id, CarInfoUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("car", "update_info_by_id");

            activity?.SetTag("id", id);

            await inner.UpdateInfoByIdAsync(id, updateDto);
        }

        public async Task UpdateStatusByIdAsync(int id, CarStatusUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("car", "update_status_by_id");

            activity?.SetTag("id", id);

            await inner.UpdateStatusByIdAsync(id, updateDto);
        }
    }
}