using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Shared.Extensions;
using Shared.OpenTelemetry;
using System.Diagnostics;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepositoryDecorator(ICarRepository inner) : ICarRepository
    {
        private readonly ICarRepository _inner = inner;

        public async Task<int> CreateAsync(Car car)
        {
            using var activity = Telemetry.Repository.StartRichActivity();

            return await _inner.CreateAsync(car);
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartRichActivity();

            await _inner.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(GetCarsListRequest request)
        {
            using var activity = Telemetry.Repository.StartRichActivity();

            return await _inner.GetAllAsync(request);
        }

        public async Task<User?> GetFullById(int id)
        {
            using var activity = Telemetry.Repository.StartRichActivity();  

            return await _inner.GetFullById(id);
        }

        public async Task<User?> GetInfoById(int id)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            return await _inner.GetInfoById(id);
        }

        public async Task<User?> GetInfoByNameAsync(string name)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            return await _inner.GetInfoByNameAsync(name);
        }

        public async Task<User?> GetInfoByNumberAsync(int number)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            return await _inner.GetInfoByNumberAsync(number);
        }

        public async Task UpdateAsync(Car car)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            await _inner.UpdateAsync(car);
        }
    }
}
