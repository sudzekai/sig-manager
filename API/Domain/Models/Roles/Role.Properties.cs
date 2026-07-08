using Domain.ValueObjects.Roles;

namespace Domain.Models.Roles
{
    public partial class Role
    {
        public RoleId? Id
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

        public RoleName Name
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
