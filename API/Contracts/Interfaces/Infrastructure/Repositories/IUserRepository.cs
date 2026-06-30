using Contracts.Objects.Dtos.Requests;
using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync(GetUsersListRequest request);

        Task<User?> GetFullByIdAsync(int id);

        Task<User?> GetInfoByIdAsync(int id);
        Task<User?> GetInfoByUsernameAsync(string username);
        Task<User?> GetInfoByEmailAsync(string email);
        Task<User?> GetInfoByPhoneNumberAsync(string phoneNumber);

        Task<int> CreateAsync(User user);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(User user);
    }
}
