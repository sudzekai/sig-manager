using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Commands.Users.Write
{
    public sealed record UserCreateCommand(UserCreateDto Dto) : ICommand<UserInfoDto>;
}
