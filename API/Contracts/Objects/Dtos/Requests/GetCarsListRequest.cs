using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetCarsListRequest : GetListRequest
    {
        [OneOf("Status,CreateDate,UpdateDate,Id,Number,Name", Required = false,
            ErrorMessage = "OrderBy должен быть одним из: Status, CreateDate, UpdateDate, Id, Number, Name")]
        public override string OrderBy { get; set; } = string.Empty;

        [OneOf("Working,Broken", Required = false,
            ErrorMessage = "Status должен быть одним из: Working, Broken")]
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAtStart { get; set; } = default;
        public DateTime CreatedAtEnd { get; set; } = default;
        public DateTime UpdatedAtStart { get; set; } = default;
        public DateTime UpdatedAtEnd { get; set; } = default;
    }
}
