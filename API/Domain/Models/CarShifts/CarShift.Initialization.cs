using Domain.Models.Base;
using Domain.ValueObjects.Parks;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.CarShifts
{
    public partial class CarShift : DomainModelBase
    {
        private CarShift(ShiftId shiftId, ParkId parkId)
        {
            ShiftId = shiftId;
            ParkId = parkId;

            _initialized = true;
        }

        public static CarShift Restore(ShiftId shiftId, ParkId parkId)
            => new(shiftId, parkId);

        public static CarShift Create(ShiftId shiftId, ParkId parkId)
            => new(shiftId, parkId);
    }
}
