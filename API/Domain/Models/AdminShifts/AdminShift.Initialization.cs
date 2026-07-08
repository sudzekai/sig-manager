using Domain.Models.Base;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.AdminShifts
{
    public partial class AdminShift : DomainModelBase
    {
        private AdminShift(ShiftId shiftId)
        {
            ShiftId = shiftId;

            _initialized = true;
        }

        public static AdminShift Restore(ShiftId shiftId) 
            => new(shiftId);

        public static AdminShift Create(ShiftId shiftId) 
            => new(shiftId);
    }
}
