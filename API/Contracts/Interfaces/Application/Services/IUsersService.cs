using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;

namespace Contracts.Interfaces.Application.Services
{
    public interface IUsersService
    {
        Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request);
        Task<UserInfoDto> GetById(int id);
        Task<UserInfoDto> CreateAsync(UserCreateDto createDto);
        Task UpdateInfoByIdAsync(int id, UserInfoUpdateDto updateDto);
        Task UpdatePasswordByIdAsync(int id, UserPasswordUpdateDto updateDto);
        Task UpdateRoleByIdAsync(int id, UserRoleUpdateDto updateDto);
        Task DeleteByIdAsync(int id);
    }
}
