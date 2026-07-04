using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Shared.Types.Exceptions;

namespace Application.Services.Cars
{
    public class CarsService(ICarRepository repository, ICarQuery query) : ICarsService
    {
        public async Task<CarInfoDto> CreateAsync(CarCreateDto createDto)
        {
            if (await IsNameExistsAsync(createDto.Name))
                throw ConflictException.CarName;

            if (await IsNumberExistsAsync(createDto.Number))
                throw ConflictException.CarNumber;

            Car car = Car.Create(createDto.Name, createDto.Number, createDto.Plate);

            var id = await repository.AddAsync(car);

            return await query.GetByIdAsync(id) ?? throw NotFoundException.CarWithId(id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            _ = await query.GetByIdAsync(id) ?? throw NotFoundException.CarWithId(id);

            await repository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request) => await query.GetAllAsync(request);

        public async Task<CarInfoDto> GetByIdAsync(int id) => await query.GetByIdAsync(id) ?? throw NotFoundException.CarWithId(id);

        public async Task UpdateInfoByIdAsync(int id, CarInfoUpdateDto updateDto)
        {
            var existing = await repository.GetAsync(id)
                ?? throw NotFoundException.CarWithId(id);

            if (updateDto.Name != null)
            {
                if (await IsNameExistsAsync(updateDto.Name, id))
                    throw ConflictException.CarName;

                existing.ChangeName(updateDto.Name);
            }

            if (updateDto.Number > 0)
            {
                if (await IsNumberExistsAsync(updateDto.Number, id))
                    throw ConflictException.CarNumber;

                existing.ChangeNumber(updateDto.Number);
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Plate))
                existing.ChangePlate(updateDto.Plate);

            await repository.UpdateAsync(existing);
        }

        public async Task UpdateStatusByIdAsync(int id, CarStatusUpdateDto updateDto)
        {
            var existing = await repository.GetAsync(id)
                ?? throw NotFoundException.CarWithId(id);

            existing.ChangeStatus(updateDto.Status);

            await repository.UpdateAsync(existing);
        }

        private async Task<bool> IsNameExistsAsync(string name, int? excludedId = null)
        {
            var id = await repository.GetIdByNameAsync(name);

            return id != null && (!excludedId.HasValue || id != excludedId.Value);

        }

        private async Task<bool> IsNumberExistsAsync(int number, int? excludedId = null)
        {
            var id = await repository.GetIdByNumberAsync(number);

            return id != null && (!excludedId.HasValue || id != excludedId.Value);
        }
    }
}
