using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.User;

namespace Contracts.Objects.Commands.Users.Update
{
    public sealed record UserInfoUpdateCommand(int Id, UserInfoUpdateDto Dto) : ICommand<Unit>;
}
