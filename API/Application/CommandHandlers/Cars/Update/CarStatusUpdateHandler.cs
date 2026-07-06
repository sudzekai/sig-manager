using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Cars.Update;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Cars.Update
{
    public class CarStatusUpdateHandler(ICarRepository repository) : ICommandHandler<CarStatusUpdateCommand, Unit>
    {
        public async Task<Unit> HandleAsync(CarStatusUpdateCommand command)
        {
            var existing = await repository.GetAsync(command.Id)
                            ?? throw NotFoundException.CarWithId(command.Id);

            existing.Status  = command.Dto.Status;

            await repository.UpdateAsync(existing);

            return Unit.Value;
        }
    }
}
