using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Parks;

namespace Contracts.Objects.Commands.Parks.Update
{
    public record ParkUpdateCommand(int Id, ParkWriteDto Dto) : ICommand<Unit>;
}
