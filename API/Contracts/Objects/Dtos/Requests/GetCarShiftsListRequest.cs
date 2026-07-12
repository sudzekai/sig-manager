using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetCarShiftsListRequest : GetListRequest
    {
        [OneOf("Status,Id,Name,CreatedAt,ClosedAt", Required = false,
            ErrorMessage = "OrderBy должен быть одним из: Status, Id, Name, CreatedAt, ClosedAt")]
        public override string OrderBy { get; set; } = string.Empty;
    }
}
