using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Commands.Users.Get
{
    public sealed record UserGetAllCommand(GetUsersListRequest Request) : ICommand<IReadOnlyList<UserSimpleDto>>;
}
