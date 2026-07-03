using Contracts.Objects.Dtos.Requests;
using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<IReadOnlyList<Car>> GetAllAsync(GetCarsListRequest request);

        Task<Car?> GetFullByIdAsync(int id);

        Task<Car?> GetInfoByIdAsync(int id);
        Task<Car?> GetInfoByNumberAsync(int number);
        Task<Car?> GetInfoByNameAsync(string name);

        Task<int> CreateAsync(Car car);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(Car car);
    }
}