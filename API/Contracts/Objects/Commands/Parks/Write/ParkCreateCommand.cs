using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Parks;

namespace Contracts.Objects.Commands.Parks.Write
{
    public record ParkCreateCommand(ParkWriteDto Dto) : ICommand<ParkDto>;
}
