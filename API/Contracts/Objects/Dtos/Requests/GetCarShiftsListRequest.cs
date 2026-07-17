using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetCarShiftsListRequest : GetListRequest
    {
        [OneOf("Status,Id,CreatedAt,ClosedAt", Required = false,
            ErrorMessage = "OrderBy должен быть одним из: Status, Id, CreatedAt, ClosedAt")]
        public override string OrderBy { get; set; } = string.Empty;

        [OneOf("Opened,Closed", Required = false,
            ErrorMessage = "Status должен быть одним из: Opened, Closed")]
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAtStart { get; set; } = default;
        public DateTime CreatedAtEnd { get; set; } = default;
        public DateTime ClosedAtStart { get; set; } = default;
        public DateTime ClosedAtEnd { get; set; } = default;
    }
}
