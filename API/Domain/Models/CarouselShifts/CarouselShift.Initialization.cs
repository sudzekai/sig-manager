using Domain.Models.Base;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.CarouselShifts
{
    public partial class CarouselShift : DomainModelBase
    {
        private CarouselShift(ShiftId shiftId)
        {
            ShiftId = shiftId;

            _initialized = true;
        }

        public static CarouselShift Restore(ShiftId shiftId) 
            => new(shiftId);

        public static CarouselShift Create(ShiftId shiftId) 
            => new(shiftId);
    }
}
