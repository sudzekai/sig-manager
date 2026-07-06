using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Commands.Cars.Write;
using Contracts.Objects.Dtos.Car;
using Domain.Models;
using Shared.Types.Exceptions;

namespace Application.Handlers.Cars.Write
{
    public class CarCreateHandler(ICarRepository repository, ICarQuery query) : ICommandHandler<CarCreateCommand, CarInfoDto>
    {
        public async Task<CarInfoDto> HandleAsync(CarCreateCommand command)
        {
            if (await IsNameExistsAsync(command.Dto.Name))
                throw ConflictException.CarName;

            if (await IsIdExistsAsync(command.Dto.Id))
                throw ConflictException.CarId;

            Car car = Car.Create(command.Dto.Id, command.Dto.Name, command.Dto.Plate);

            var id = await repository.AddAsync(car);

            return await query.GetByIdAsync(id) 
                ?? throw NotFoundException.CarWithId(id);
        }

        private async Task<bool> IsNameExistsAsync(string name)
            => await repository.GetIdByNameAsync(name) is not null;

        private async Task<bool> IsIdExistsAsync(int id)
            => await repository.GetAsync(id) is not null;
    }
}
