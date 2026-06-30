using Contracts.Objects.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetUsersListRequest : GetListRequest
    {
        [OneOf("Id,Username,FullName,CreateDate,UpdateDate", Required = false, 
            ErrorMessage = "OrderBy должен быть одним из: Id, Username, FullName. CreateDate, UpdateDate")]
        public override string OrderBy { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "RoleId должен быть больше 0")]
        public int? RoleId { get; set; }

        public DateTime CreatedAtStart { get; set; } = default;
        public DateTime CreatedAtEnd { get; set; } = default;
        public DateTime UpdatedAtStart { get; set; } = default;
        public DateTime UpdatedAtEnd { get; set; } = default;
    }
}
