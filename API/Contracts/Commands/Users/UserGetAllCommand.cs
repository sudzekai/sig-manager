using Contracts.Interfaces.Application.Commands;
using Shared.Objects.Dtos.Requests;
using Shared.Objects.Dtos.User;

namespace Contracts.Commands.Users
{
    public sealed record UserGetAllCommand(GetUsersListRequest Request) : ICommandHandler<IReadOnlyList<UserSimpleDto>>;
}
