namespace Infrastructure.Schema.Car
{
    internal class CarSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            CarSchema.Id,
            CarSchema.Name,
            CarSchema.Plate,
            CarSchema.Status
        ];

        public static readonly IReadOnlyList<string> Full = [
            CarSchema.Id,
            CarSchema.Name,
            CarSchema.Plate,
            CarSchema.Status
        ];

        public static readonly IReadOnlyList<string> Simple = [
            CarSchema.Id,
            CarSchema.Name
        ];
    }
}
