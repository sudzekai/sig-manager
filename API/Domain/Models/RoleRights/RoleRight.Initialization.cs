using Domain.Models.Base;
using Domain.ValueObjects.Rights;
using Domain.ValueObjects.Roles;

namespace Domain.Models.RoleRight
{
    public partial class RoleRight : DomainModelBase
    {
        private RoleRight(RoleId roleId, RightId rightId)
        {
            RoleId = roleId;
            RightId = rightId;

            _initialized = true;
        }

        public static RoleRight Create(RoleId roleId, RightId rightId)
            => new(roleId, rightId);
    }
}
