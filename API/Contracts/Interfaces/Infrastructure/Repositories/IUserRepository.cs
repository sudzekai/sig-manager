using Domain.Models.Users;
using Domain.ValueObjects.Users;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<UserId> AddAsync(User user);
        Task<bool> DeleteAsync(UserId id);
        Task<User?> GetAsync(UserId id);
        Task<UserId?> GetIdByUsernameAsync(Username username);
        Task<UserId?> GetIdByEmailAsync(UserEmail email);
        Task<UserId?> GetIdByPhoneNumberAsync(UserPhoneNumber phoneNumber);
        Task UpdateAsync(User user);
    }
}
