using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetCarShiftsListRequest : GetListRequest
    {
        [OneOf("Status,Id,Name", Required = false,
            ErrorMessage = "OrderBy должен быть одним из: Status, Id, Name")]
        public override string OrderBy { get; set; }
    }
}
