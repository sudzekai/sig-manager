using Contracts.Interfaces.Application.Services;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Shared.Types.Exceptions;

namespace Application.Services.Cars
{
    public class CarsService(ICarRepository repository) : ICarsService
    {
        public async Task<CarInfoDto> CreateAsync(CarCreateDto createDto)
        {
            if (await IsNameExistsAsync(createDto.Name))
                throw new ConflictException("Машина с таким названием уже существует", $"name: {createDto.Name} exists");
            if (await IsNumberExistsAsync(createDto.Number))
                throw new ConflictException("Машина с таким номером уже существует", $"number: {createDto.Number} exists");

            Car car = new(createDto.Name, createDto.Number, createDto.Plate);

            var id = await repository.CreateAsync(car);
            var created = await repository.GetInfoByIdAsync(id) ?? throw new InternalServerException("Внутренняя ошибка сервера", "internal error: created car was null");

            return new(created.Id, created.Name, created.Number, created.Plate, created.Status);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!(await IsCarExistsAsync(id)))
                throw new NotFoundException("Машина с таким идентификатором не найдена", $"id: {id} doesn't exist");

            await repository.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request)
        {
            var result = await repository.GetAllAsync(request);

            return [.. result.Select(x => new CarSimpleDto(x.Id, x.Name, x.Number))];
        }

        public async Task<CarInfoDto> GetInfoByIdAsync(int id)
        {
            var existing = await repository.GetInfoByIdAsync(id) ?? throw new NotFoundException("Машина с таким идентификатором не найдена", $"id: {id} doesn't exist");

            return new(existing.Id, existing.Name, existing.Number, existing.Plate, existing.Status);
        }

        public async Task UpdateInfoByIdAsync(int id, CarInfoUpdateDto updateDto)
        {
            var existing = await repository.GetFullByIdAsync(id) ?? throw new NotFoundException("Машина с таким идентификатором не найдена", $"id: {id} doesn't exist");

            if (updateDto.Name != null)
            {
                if (await IsNameExistsAsync(updateDto.Name, id))
                    throw new ConflictException("Машина с таким названием уже существует", $"name: {updateDto.Name} exists");

                existing.ChangeName(updateDto.Name);
            }
            if (updateDto.Number > 0)
            {
                if (await IsNumberExistsAsync(updateDto.Number, id))
                    throw new ConflictException("Машина с таким номером уже существует", $"number: {updateDto.Number} exists");

                existing.ChangeNumber(updateDto.Number);
            }
            if (!string.IsNullOrWhiteSpace(updateDto.Plate))
                existing.ChangePlate(updateDto.Plate);

            await repository.UpdateAsync(existing);
        }

        public async Task UpdateStatusByIdAsync(int id, CarStatusUpdateDto updateDto)
        {
            var existing = await repository.GetFullByIdAsync(id) ?? throw new NotFoundException("Машина с таким идентификатором не найдена", $"id: {id} doesn't exist");

            existing.ChangeStatus(updateDto.Status);

            await repository.UpdateAsync(existing);
        }

        private async Task<bool> IsCarExistsAsync(int id)
        {
            var existing = await repository.GetInfoByIdAsync(id);

            if (existing == null) return false;

            return true;
        }

        private async Task<bool> IsNameExistsAsync(string name, int? excludedId = null)
        {
            var existing = await repository.GetInfoByNameAsync(name);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }

        private async Task<bool> IsNumberExistsAsync(int number, int? excludedId = null)
        {
            var existing = await repository.GetInfoByNumberAsync(number);

            if (existing == null) return false;

            if (excludedId.HasValue && existing.Id == excludedId.Value) return false;

            return true;
        }
    }
}
