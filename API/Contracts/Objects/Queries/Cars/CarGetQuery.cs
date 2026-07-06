using Contracts.Interfaces.Application.Commands;
using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.Car;

namespace Contracts.Objects.Queries.Cars
{
    public record CarGetQuery(int Id) : IQuery<CarInfoDto>;
}
