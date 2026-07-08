using Domain.Models.Base;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.TrainShifts
{
    public partial class TrainShift : DomainModelBase
    {
        private TrainShift(ShiftId shiftId)
        {
            ShiftId = shiftId;

            _initialized = true;
        }

        public static TrainShift Restore(ShiftId shiftId) 
            => new(shiftId);

        public static TrainShift Create(ShiftId shiftId) 
            => new(shiftId);
    }
}
