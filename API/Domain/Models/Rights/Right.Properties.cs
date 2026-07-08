using Domain.ValueObjects.Rights;

namespace Domain.Models.Rights
{
    public partial class Right
    {
        public RightId? Id
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

        public RightCode Code
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
