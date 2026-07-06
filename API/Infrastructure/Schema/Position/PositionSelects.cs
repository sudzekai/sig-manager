namespace Infrastructure.Schema.Position
{
    internal static class PositionSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            PositionSchema.Name,
            PositionSchema.PricePerHour
        ];

        public static readonly IReadOnlyList<string> Full = [
            PositionSchema.Id,
            PositionSchema.Name,
            PositionSchema.PricePerHour
        ];
    }
}
