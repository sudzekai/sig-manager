using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;

namespace Contracts.Interfaces.Infrastructure.Queries
{
    public interface IUserQuery
    {
        Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request);
        Task<UserInfoDto?> GetByIdAsync(int id);
    }
}