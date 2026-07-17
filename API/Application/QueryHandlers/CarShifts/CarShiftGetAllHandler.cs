using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Queries.CarShifts;

namespace Application.QueryHandlers.CarShifts
{
    public class CarShiftGetAllHandler(ICarShiftQuery carShifts) : IQueryHandler<CarShiftGetAllQuery, IReadOnlyList<CarShiftSimpleDto>>
    {
        public async Task<IReadOnlyList<CarShiftSimpleDto>> HandleAsync(CarShiftGetAllQuery query)
        {
            return await carShifts.GetAllAsync(query.Request);
        }
    }
}
