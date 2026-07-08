using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Domain.Models.InfoShifts;
using Domain.ValueObjects.Shifts;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Repositories.InfoShifts
{
    public class InfoShiftRepositoryDecorator(IInfoShiftRepository inner, ILogger<IInfoShiftRepository> logger) : IInfoShiftRepository
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
