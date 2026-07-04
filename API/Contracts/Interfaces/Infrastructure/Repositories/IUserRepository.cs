using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(int id);
        Task<int> AddAsync(User user);
        Task DeleteAsync(int id);
        Task UpdateAsync(User user);

        Task<int?> GetIdByUsernameAsync(string username);
        Task<int?> GetIdByEmailAsync(string email);
        Task<int?> GetIdByPhoneNumberAsync(string phoneNumber);
    }
}
