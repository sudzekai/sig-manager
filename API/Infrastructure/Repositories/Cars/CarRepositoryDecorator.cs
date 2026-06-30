using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Infrastructure.Schema.Car;
using Infrastructure.Schema.User;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared.OpenTelemetry.Tracing.Sources;
using System.Diagnostics;
using System.Xml.Linq;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepositoryDecorator : ICarRepository
    {
        private readonly ICarRepository _inner;
        private readonly ActivitySource _activitySource = ActivitySourceDictionary.Repositories.Cars;

        public CarRepositoryDecorator(ICarRepository inner)
        {
            _inner = inner;
        }

        public async Task<int> CreateAsync(Car car)
        {
            using var activity = _activitySource.StartActivity(nameof(CreateAsync));
            return await _inner.CreateAsync(car);
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(DeleteByIdAsync));
            await _inner.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync(GetCarsListRequest request)
        {
            using var activity = _activitySource.StartActivity(nameof(GetAllAsync));
            return await _inner.GetAllAsync(request);
        }

        public async Task<User?> GetFullById(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetFullById));
            return await _inner.GetFullById(id);
        }

        public async Task<User?> GetInfoById(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoById));
            return await _inner.GetInfoById(id);
        }

        public async Task<User?> GetInfoByNameAsync(string name)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoByNameAsync));
            return await _inner.GetInfoByNameAsync(name);
        }

        public async Task<User?> GetInfoByNumberAsync(int number)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoByNumberAsync));
            return await _inner.GetInfoByNumberAsync(number);
        }

        public async Task UpdateAsync(Car car)
        {
            using var activity = _activitySource.StartActivity(nameof(UpdateAsync));
            await _inner.UpdateAsync(car);
        }
    }
}
