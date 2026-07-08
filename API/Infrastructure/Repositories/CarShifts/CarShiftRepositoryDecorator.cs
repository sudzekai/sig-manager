using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.CarShifts;
using Domain.ValueObjects.Shifts;

namespace Infrastructure.Repositories.CarShifts
{
    public class CarShiftRepositoryDecorator : ICarShiftRepository
    {
        public Task<ShiftId> AddAsync(CarShift carShift)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task<CarShift?> GetAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CarShift carShift)
        {
            throw new NotImplementedException();
        }
    }
}
