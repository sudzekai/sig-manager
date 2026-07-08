using Domain.ValueObjects.Rights;
using Domain.ValueObjects.Roles;

namespace Domain.Models.RoleRight
{
    public partial class RoleRight
    {
        public RoleId RoleId
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

        public RightId RightId
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
