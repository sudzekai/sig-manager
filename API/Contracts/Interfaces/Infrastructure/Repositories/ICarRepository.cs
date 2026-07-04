using Domain.Models;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<Car?> GetAsync(int id);
        Task<int> AddAsync(Car car);
        Task DeleteAsync(int id);
        Task UpdateAsync(Car car);

        Task<int?> GetIdByNameAsync(string name);
        Task<int?> GetIdByNumberAsync(int number);
    }
}