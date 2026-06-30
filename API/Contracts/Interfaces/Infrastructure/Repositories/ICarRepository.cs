using Contracts.Objects.Dtos.Requests;
using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync(GetCarsListRequest request);

        Task<User?> GetFullById(int id);

        Task<User?> GetInfoById(int id);
        Task<User?> GetInfoByNumberAsync(int number);
        Task<User?> GetInfoByNameAsync(string name);

        Task<int> CreateAsync(Car car);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Car car);
    }
}