using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Shared.Extensions;
using Shared.OpenTelemetry;
using System.Diagnostics;

namespace Application.Services.Users
{
    public class UsersServiceDecorator(IUsersService inner) : IUsersService
    {
        public async Task<UserInfoDto> CreateAsync(UserCreateDto createDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "create");

            var result = await inner.CreateAsync(createDto);

            activity?.SetTag("id", result.Id);

            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "delete");
            activity?.SetTag("id", id);
            
            await inner.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "get_all");

            var result = await inner.GetAllAsync(request);

            activity?.SetTag("count", result.Count);
            
            return result;
        }

        public async Task<UserInfoDto> GetById(int id)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "get_by_id");
            activity?.SetTag("id", id);

            var result = await inner.GetById(id);

            return result;
        }

        public async Task<UserInfoDto> GetByUsernameAsync(string username)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "get_by_username");
            activity?.SetTag("username", username);

            var result = await inner.GetByUsernameAsync(username);

            return result;
        }

        public async Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "update_info_by_id");
            activity?.SetTag("id", id);

            await inner.UpdateInfoByIdAsync(id, updateDto);
        }

        public async Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "update_password_by_id");
            activity?.SetTag("id", id);
            
            await inner.UpdatePasswordByIdAsync(id, updateDto);
        }

        public async Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto)
        {
            using var activity = Telemetry.Service.StartServiceActivity("user", "update_role_by_id");
            activity?.SetTag("id", id);

            await inner.UpdateRoleByIdAsync(id, updateDto);
        }
    }
}
