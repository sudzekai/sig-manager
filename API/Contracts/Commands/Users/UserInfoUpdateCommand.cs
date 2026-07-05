using Contracts.Interfaces.Application.Commands;
using Contracts.Objects;
using Shared.Objects.Dtos.User;

namespace Contracts.Commands.Users
{
    public sealed record UserInfoUpdateCommand(int Id, UserInfoUpdateDto Dto) : ICommandHandler<Unit>;
}
