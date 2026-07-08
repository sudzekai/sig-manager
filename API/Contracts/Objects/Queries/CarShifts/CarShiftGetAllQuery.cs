using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Objects.Queries.CarShifts
{
    public record CarShiftGetAllQuery(GetShiftRequest Request) : IQuery<List<CarShiftSimpleDto>>;
}
