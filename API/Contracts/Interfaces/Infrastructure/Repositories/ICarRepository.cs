using Domain.Models.Cars;
using Domain.ValueObjects.Cars;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarRepository
    {
        Task<CarId> AddAsync(Car car);
        Task<bool> DeleteAsync(CarId id);
        Task<Car?> GetAsync(CarId id);
        Task<CarId?> GetIdByNameAsync(CarName name);
        Task UpdateAsync(Car car);
    }
}