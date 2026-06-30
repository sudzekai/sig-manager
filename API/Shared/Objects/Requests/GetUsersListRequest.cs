using System.ComponentModel.DataAnnotations;

namespace Shared.Objects.Requests
{
    public class GetUsersListRequest : GetListRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Идентификатор роли не может быть отрицательным")]
        public int RoleId { get; set; }

        public DateTime CreatedAtStart { get; set; }
        public DateTime CreatedAtEnd { get; set; }
        public DateTime UpdatedAtStart { get; set; }
        public DateTime UpdatedAtEnd { get; set; }
    }
}
