using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetCarsListRequest : GetListRequest
    {
        [OneOf("Status,CreateDate,UpdateDate,Id,Number,Name",
            ErrorMessage = "OrderBy должен быть одним из: Status, CreateDate, UpdateDate, Id, Number, Name")]
        public override string OrderBy { get; set; } = string.Empty;

        [OneOf("Working,Broken",
            ErrorMessage = "Status должен быть одним из: Working, Broken")]
        public string Status { get; set; } = string.Empty;
    }
}
