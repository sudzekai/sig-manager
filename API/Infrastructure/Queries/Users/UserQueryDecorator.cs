using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Infrastructure.Queries.Users
{
    internal class UserQueryDecorator(IUserQuery inner, ILogger<IUserQuery> logger) : IUserQuery
    {
        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("user", "get_all");

            logger.LogInformation("Получение списка записей о пользователях");

            return await inner.GetAllAsync(request);
        }

        public async Task<UserInfoDto?> GetByIdAsync(int id)
        {
            using var activity = Telemetry.Repository.StartQueryActivity("user", "get_by_id");

            logger.LogInformation("Получение информации о пользователе с id {id}", id);

            return await inner.GetByIdAsync(id);
        }
    }
}
