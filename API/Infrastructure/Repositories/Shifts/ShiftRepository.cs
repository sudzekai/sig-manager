using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Shifts;
using Domain.ValueObjects.Shifts;

namespace Infrastructure.Repositories.Shifts
{
    public class ShiftRepository(IDbContext db) : IShiftRepository
    {
        public Task<ShiftId> AddAsync(Shift shift)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task<Shift?> GetAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Shift shift)
        {
            throw new NotImplementedException();
        }
    }
}
