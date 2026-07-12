using Domain.Models.UserShifts;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IUserShiftRepository
    {
        public Task AddAsync(UserShift userShift);
        public Task DeleteAsync(UserShift userShift);
    }
}
