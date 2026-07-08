using Domain.ValueObjects.Positions;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Users;

namespace Domain.Models.UserShifts
{
    public partial class UserShift
    {
        public UserId UserId
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ShiftId ShiftId
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public PositionId PositionId
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }
    }
}
