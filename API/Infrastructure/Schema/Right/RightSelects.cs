namespace Infrastructure.Schema.Right
{
    internal static class RightSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            RightSchema.Code
        ];

        public static readonly IReadOnlyList<string> Full = [
            RightSchema.Id,
            RightSchema.Code
        ];
    }
}
