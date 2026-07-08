using Domain.Models.Base;
using Domain.ValueObjects.Roles;

namespace Domain.Models.Roles
{
    public partial class Role : DomainModelBase
    {
        private Role(RoleId id, RoleName name)
        {
            Id = id;
            Name = name;

            _initialized = true;
        }

        private Role(RoleName name)
        {
            Name = name;

            _initialized = true;
        }

        public static Role Restore(RoleId id, RoleName name)
            => new(id, name);

        public static Role Create(RoleName name)
            => new(name);
    }
}
