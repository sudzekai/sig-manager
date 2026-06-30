using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
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

        public async Task<IReadOnlyList<User>> GetAllAsync(GetUsersListRequest request)
        {
            using var activity = _activitySource.StartActivity(nameof(GetAllAsync));
            return await _inner.GetAllAsync(request);
        }

        public async Task<User?> GetInfoByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoByIdAsync));
            return await _inner.GetInfoByIdAsync(id);
        }

        public async Task<User?> GetInfoByUsernameAsync(string username)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoByUsernameAsync));
            return await _inner.GetInfoByUsernameAsync(username);
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

        public async Task<User?> GetInfoByEmailAsync(string email)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoByEmailAsync));
            return await _inner.GetInfoByEmailAsync(email);
        }

        public async Task<User?> GetInfoByPhoneNumberAsync(string phoneNumber)
        {
            using var activity = _activitySource.StartActivity(nameof(GetInfoByEmailAsync));
            return await _inner.GetInfoByEmailAsync(phoneNumber);
        }

        public async Task<User?> GetFullByIdAsync(int id)
        {
            using var activity = _activitySource.StartActivity(nameof(GetFullByIdAsync));
            return await _inner.GetFullByIdAsync(id);
        }
    }
}
