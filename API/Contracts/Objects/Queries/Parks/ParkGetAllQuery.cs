using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.Parks;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Objects.Queries.Parks
{
    public record ParkGetAllQuery(GetListRequest Request) : IQuery<IReadOnlyList<ParkDto>>;
}
