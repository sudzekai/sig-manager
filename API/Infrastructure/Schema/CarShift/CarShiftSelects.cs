using Infrastructure.Schema.InfoShift;

namespace Infrastructure.Schema.CarShift
{
    internal class CarShiftSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            CarShiftSchema.ShiftId,
            CarShiftSchema.ParkId
        ];

        public static readonly IReadOnlyList<string> Full = [
            CarShiftSchema.ParkId
        ];
    }
}
