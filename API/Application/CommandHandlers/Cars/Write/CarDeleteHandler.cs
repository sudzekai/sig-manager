using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects;
using Contracts.Objects.Commands.Cars.Write;
using Shared.Types.Exceptions;

namespace Application.CommandHandlers.Cars.Write
{
    public class CarDeleteHandler(ICarRepository repository) : ICommandHandler<CarDeleteCommand, Unit>
    {
        public async Task<Unit> HandleAsync(CarDeleteCommand command)
        {
            _ = await repository.GetAsync(command.Id) 
                ?? throw NotFoundException.CarWithId(command.Id);

            await repository.DeleteAsync(command.Id);

            return Unit.Value;
        }
    }
}
