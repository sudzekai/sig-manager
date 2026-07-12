using Infrastructure.Schema.User;

namespace Infrastructure.Schema.UserShift
{
    public class UserShiftSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            UserShiftSchema.UserId,
            UserShiftSchema.ShiftId,
            UserShiftSchema.PositionId
        ];

        public static readonly IReadOnlyList<string> Full = [
            UserShiftSchema.UserId,
            UserShiftSchema.ShiftId,
            UserShiftSchema.PositionId
        ];
    }
}
