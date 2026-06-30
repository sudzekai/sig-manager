using Contracts.Objects.Dtos.Requests;
using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync(GetUsersListRequest request);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetFullByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<int> CreateAsync(User user);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(User user);
    }
}
