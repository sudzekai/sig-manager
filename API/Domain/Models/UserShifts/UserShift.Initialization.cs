using Domain.Models.Base;
using Domain.ValueObjects.Positions;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Users;

namespace Domain.Models.UserShifts
{
    public partial class UserShift : DomainModelBase
    {
        private UserShift(UserId userId, ShiftId shiftId, PositionId positionId)
        {
            UserId = userId;
            ShiftId = shiftId;
            PositionId = positionId;
        }

        public static UserShift Create(UserId userId, ShiftId shiftId, PositionId positionId)
            => new(userId, shiftId, positionId);
    }
}
