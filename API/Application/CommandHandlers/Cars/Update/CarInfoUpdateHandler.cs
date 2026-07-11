using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Cars.Update;
using Domain.ValueObjects.Cars;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Cars.Update
{
    public class CarInfoUpdateHandler(ICarRepository repository) : ICommandHandler<CarInfoUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(CarInfoUpdateCommand command)
        {
            var existing = await repository.GetAsync(CarId.FromValue(command.Id))
                ?? throw NotFoundException.CarWithId(command.Id);

            if (!string.IsNullOrWhiteSpace(command.Dto.Name))
            {
                var carName = CarName.FromValue(command.Dto.Name);

                if (await repository.GetIdByNameAsync(carName) is not null)
                    throw ConflictException.CarName();

                existing.Name = carName;
            }

            if (command.Dto.Id != default)
            {
                var carId = CarId.FromValue(command.Dto.Id);

                if (await repository.GetAsync(carId) is not null)
                    throw ConflictException.CarId();

                existing.Id = carId;
            }

            if (!string.IsNullOrWhiteSpace(command.Dto.Plate))
                existing.Plate = CarPlate.FromValue(command.Dto.Plate);

            await repository.UpdateAsync(existing);

            return Unit.Value;
        }
    }
}
