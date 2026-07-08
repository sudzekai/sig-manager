using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetRolesListRequest : GetListRequest
    {
        [OneOf("Id,Name", Required = false,
                    ErrorMessage = "OrderBy должен быть одним из: Id, Name")]
        public override string OrderBy { get; set; } = string.Empty;
    }
}
