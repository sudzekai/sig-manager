namespace Contracts.Objects.Dtos.Roles
{
    public class RoleRightsUpdateDto
    {
        public string Name { get; set; } = string.Empty;

        public int[] RightIds { get; set; } = [];
    }
}
