using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
using Shared.OpenTelemetry.Tracing.Sources;
using System.Diagnostics;

namespace Infrastructure.Repositories.Users
{
    public class UserRepositoryDecorator : IUserRepository
    {
        private readonly IUserRepository _inner;
        private readonly ActivitySource _activitySource = ActivitySourceDictionary.Repositories.Users;

        public UserRepositoryDecorator(IUserRepository inner)
        {
            _inner = inner;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            using var activity = _activitySource.StartActivity(nameof(GetAllAsync));
            return await _inner.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetByIdAsync));
            return await _inner.GetByIdAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var activity = _activitySource.StartActivity(nameof(GetByUsernameAsync));
            return await _inner.GetByUsernameAsync(username);
        }

        public async Task<int> CreateAsync(User user)
        {
            using var activity = _activitySource.StartActivity(nameof(CreateAsync));
            return await _inner.CreateAsync(user);
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(DeleteByIdAsync));
            await _inner.DeleteByIdAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            using var activity = _activitySource.StartActivity(nameof(UpdateAsync));
            await _inner.UpdateAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var activity = _activitySource.StartActivity(nameof(GetByEmailAsync));
            return await _inner.GetByEmailAsync(email);
        }

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            using var activity = _activitySource.StartActivity(nameof(GetByEmailAsync));
            return await _inner.GetByEmailAsync(phoneNumber);
        }

        public async Task<User?> GetFullByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetFullByIdAsync));
            return await _inner.GetFullByIdAsync(id);
        }
    }
}
