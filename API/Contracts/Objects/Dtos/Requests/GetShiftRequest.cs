using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Requests
{
    public class GetShiftRequest : GetListRequest
    {
        [OneOf("Id,CreateDate,CloseDate,Tickets,Total")]
        public override string OrderBy { get; set; }
    }
}
