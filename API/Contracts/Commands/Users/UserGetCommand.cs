using Contracts.Interfaces.Application.Commands;
using Shared.Objects.Dtos.User;

namespace Contracts.Commands.Users
{
    public sealed record UserGetCommand(int Id) : ICommandHandler<UserInfoDto>;
}
