using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Commands.Users.Get
{
    public sealed record UserGetCommand(int Id) : ICommand<UserInfoDto>;
}
