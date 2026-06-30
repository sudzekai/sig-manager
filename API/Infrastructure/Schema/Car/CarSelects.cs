using Infrastructure.Schema.User;

namespace Infrastructure.Schema.Car
{
    internal class CarSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            CarSchema.Name,
            CarSchema.Number,
            CarSchema.Plate,
            CarSchema.Status,
            CarSchema.CreatedAt,
            CarSchema.UpdatedAt
        ];

        public static readonly IReadOnlyList<string> Full = [
            CarSchema.Id,
            CarSchema.Name,
            CarSchema.Number,
            CarSchema.Plate,
            CarSchema.Status,
            CarSchema.CreatedAt,
            CarSchema.UpdatedAt
        ];

        public static readonly IReadOnlyList<string> Info = [
            CarSchema.Id,
            CarSchema.Name,
            CarSchema.Number,
            CarSchema.Plate,
            CarSchema.Status
        ];

        public static readonly IReadOnlyList<string> Simple = [
            CarSchema.Id,
            CarSchema.Name,
            CarSchema.Number
        ];
    }
}
