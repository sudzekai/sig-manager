using Contracts.Interfaces.Application.Queries;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Queries.CarShifts;
using Shared.Types.Exceptions;

namespace Application.QueryHandlers.CarShifts
{
    public class CarShiftGetQueryHandler(ICarShiftQuery shifts) : IQueryHandler<CarShiftGetQuery, CarShiftInfoDto>
    {
        public async Task<CarShiftInfoDto> HandleAsync(CarShiftGetQuery query)
        {
            return await shifts.GetByIdAsync(query.Id)
                ?? throw NotFoundException.ShiftWithId(query.Id);
        }
    }
}
