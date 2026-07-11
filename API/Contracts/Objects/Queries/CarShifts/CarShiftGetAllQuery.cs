using Contracts.Interfaces.Application.Queries;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;

namespace Contracts.Objects.Queries.CarShifts
{
    public record CarShiftGetAllQuery(GetCarShiftsListRequest Request) : IQuery<IReadOnlyList<CarShiftSimpleDto>>;
}
