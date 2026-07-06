using Contracts.Interfaces.Application.Commands;

namespace Contracts.Objects.Commands.Cars.Write
{
    public record CarDeleteCommand(int Id) : ICommand<Unit>;
}
