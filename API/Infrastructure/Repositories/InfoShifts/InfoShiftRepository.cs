using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.InfoShifts;
using Domain.ValueObjects.Shifts;

namespace Infrastructure.Repositories.InfoShifts
{
    public class InfoShiftRepository(IDbContext db) : IInfoShiftRepository
    {
        public Task<ShiftId> AddAsync(InfoShift infoShift)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task<InfoShift?> GetAsync(ShiftId id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(InfoShift infoShift)
        {
            throw new NotImplementedException();
        }
    }
}
