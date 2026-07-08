using Domain.Models.Base;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.BouncerShifts
{
    public partial class BouncerShift : DomainModelBase
    {
        private BouncerShift(ShiftId shiftId)
        {
            ShiftId = shiftId;

            _initialized = true;
        }

        public static BouncerShift Restore(ShiftId shiftId) 
            => new(shiftId);

        public static BouncerShift Create(ShiftId shiftId) 
            => new(shiftId);
    }
}
