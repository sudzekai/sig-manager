using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Application.Services.Users
{
    public class UsersServiceDecorator(IUsersService inner, ILogger<UsersService> logger) : IUsersService
    {
        private readonly IUsersService _inner = inner;
        private readonly ILogger<UsersService> _logger = logger;

        public async Task<UserInfoDto> CreateAsync(UserCreateDto createDto)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var result = await _inner.CreateAsync(createDto);

            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            await _inner.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var result = await _inner.GetAllAsync(request);

            return result;
        }

        public async Task<UserInfoDto> GetById(int id)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var result = await _inner.GetById(id);

            return result;
        }

        public async Task<UserInfoDto> GetByUsernameAsync(string username)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var result = await _inner.GetByUsernameAsync(username);

            return result;
        }

        public async Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            await _inner.UpdateInfoByIdAsync(id, updateDto);
        }

        public async Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            await _inner.UpdatePasswordByIdAsync(id, updateDto);
        }

        public async Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            await _inner.UpdateRoleByIdAsync(id, updateDto);
        }
    }
}
