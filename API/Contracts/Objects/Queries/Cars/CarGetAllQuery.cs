using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Objects.Queries.Cars
{
    public record CarGetAllQuery(GetCarsListRequest Request) : IQuery<IReadOnlyList<CarSimpleDto>>;
}
