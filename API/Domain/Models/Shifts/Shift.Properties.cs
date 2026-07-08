using Domain.ValueObjects.Shifts;

namespace Domain.Models.Shifts
{
    public partial class Shift
    {
        public ShiftId? Id
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

        public ShiftType Type
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

        public ShiftStatus Status
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

        public DateTime CreatedAt
        {
            get;
            private set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public DateTime UpdatedAt { get; private set; }

        public DateTime? ClosedAt
        {
            get;
            private set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }
    }
}
