using Domain.Models.Base;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.PopcornShifts
{
    public partial class PopcornShift : DomainModelBase
    {
        private PopcornShift(ShiftId shiftId)
        {
            ShiftId = shiftId;

            _initialized = true;
        }

        public static PopcornShift Restore(ShiftId shiftId) 
            => new(shiftId);

        public static PopcornShift Create(ShiftId shiftId) 
            => new(shiftId);
    }
}
