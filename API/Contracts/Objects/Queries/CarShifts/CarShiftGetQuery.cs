using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.CarShift;

namespace Contracts.Objects.Queries.CarShifts
{
    public record CarShiftGetQuery(int Id) : IQuery<CarShiftInfoDto>;
}
