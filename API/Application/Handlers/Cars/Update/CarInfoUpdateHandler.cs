using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Cars.Update;
using Contracts.Objects.Dtos.Car;
using Shared.Types.Exceptions;

namespace Application.Handlers.Cars.Update
{
    public class CarInfoUpdateHandler(ICarRepository repository) : ICommandHandler<CarInfoUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(CarInfoUpdateCommand command)
        {
            var existing = await repository.GetAsync(command.Id)
                ?? throw NotFoundException.CarWithId(command.Id);

            if (!string.IsNullOrWhiteSpace(command.Dto.Name))
            {
                if (await IsNameExistsAsync(command.Dto.Name, command.Id))
                    throw ConflictException.CarName;

                existing.ChangeName(command.Dto.Name);
            }

            if (command.Dto.Id != default)
            {
                if (await IsIdExistsAsync(command.Dto.Id, command.Id))
                    throw ConflictException.CarId;

                existing.ChangeId(command.Dto.Id);
            }

            if (!string.IsNullOrWhiteSpace(command.Dto.Plate))
                existing.ChangePlate(command.Dto.Plate);

            await repository.UpdateAsync(existing);

            return Unit.Value;
        }

        private async Task<bool> IsNameExistsAsync(string name, int excludedId)
            => await repository.GetIdByNameAsync(name) is var id && id is not null && id != excludedId;

        private async Task<bool> IsIdExistsAsync(int id, int excludedId)
            => await repository.GetAsync(id) is var entity && entity is not null && entity.Id != excludedId;
    }
}
