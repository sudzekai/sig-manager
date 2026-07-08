using Domain.ValueObjects.Parks;

namespace Domain.Models.Parks
{
    public partial class Park
    {
        public ParkId? Id
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

        public ParkName Name
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
