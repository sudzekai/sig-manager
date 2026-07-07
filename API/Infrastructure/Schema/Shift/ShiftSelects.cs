namespace Infrastructure.Schema.Shift
{
    internal class ShiftSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            ShiftSchema.Type,
            ShiftSchema.CreatedAt,
            ShiftSchema.UpdatedAt
        ];

        public static readonly IReadOnlyList<string> Full = [
            ShiftSchema.Type,
            ShiftSchema.Status,
            ShiftSchema.CreatedAt,
            ShiftSchema.UpdatedAt,
            ShiftSchema.ClosedAt
        ];
    }
}
