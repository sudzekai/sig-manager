using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetCarsListRequest : GetListRequest
    {
        [OneOf("Status,Id,Name", Required = false,
            ErrorMessage = "OrderBy должен быть одним из: Status, Id, Name")]
        public override string OrderBy { get; set; } = string.Empty;

        [OneOf("Working,Broken", Required = false,
            ErrorMessage = "Status должен быть одним из: Working, Broken")]
        public string Status { get; set; } = string.Empty;
    }
}
