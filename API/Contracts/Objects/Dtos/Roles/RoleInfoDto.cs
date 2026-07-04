using Contracts.Objects.Dtos.Right;

namespace Contracts.Objects.Dtos.Roles
{
    public record RoleInfoDto(int Id, string Name, IReadOnlyList<RightDto> Rights);
}
