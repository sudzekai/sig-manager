using Domain.Models.CarouselShifts;
using Domain.ValueObjects.Shifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarouselShiftRepository
    {
        Task<ShiftId> AddAsync(CarouselShift carouselShift);
        Task<bool> DeleteAsync(ShiftId id);
        Task<CarouselShift?> GetAsync(ShiftId id);
        Task UpdateAsync(CarouselShift carouselShift);
    }
}
