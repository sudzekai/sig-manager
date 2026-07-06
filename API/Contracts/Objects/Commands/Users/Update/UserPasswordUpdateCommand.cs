using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Commands.Users.Update
{
    public sealed record UserPasswordUpdateCommand(int Id, UserPasswordUpdateDto Dto) : ICommand<Unit>;
}
