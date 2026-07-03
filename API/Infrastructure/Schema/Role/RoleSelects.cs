namespace Infrastructure.Schema.Role
{
    internal static class RoleSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            RoleSchema.Name
        ];

        public static readonly IReadOnlyList<string> Full = [
            RoleSchema.Id,
            RoleSchema.Name
        ];
    }
}
