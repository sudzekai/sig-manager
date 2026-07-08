using Domain.Models.CarShiftCars;

namespace Contracts.Interfaces.Infrastructure.Repositories
{
    public interface ICarShiftCarRepository
    {
        Task<bool> AddAsync(CarShiftCar carShiftCar);
        Task<bool> DeleteAsync(CarShiftCar carShiftCar);
    }
}
