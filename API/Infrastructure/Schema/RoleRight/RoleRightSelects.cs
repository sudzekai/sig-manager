namespace Infrastructure.Schema.RoleRight
{
    internal static class RoleRightSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            RoleRightSchema.RoleId,
            RoleRightSchema.RightId
        ];

        public static readonly IReadOnlyList<string> Full = [
            RoleRightSchema.RoleId,
            RoleRightSchema.RightId
        ];
    }
}
