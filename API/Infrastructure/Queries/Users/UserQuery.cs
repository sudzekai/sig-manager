using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.User;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using Shared.Types.Enums;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Queries.Users
{
    public class UserQuery(IDbContext db) : IUserQuery
    {
        public async Task<IReadOnlyList<UserSimpleDto>> GetAllAsync(GetUsersListRequest request)
        {
            StringBuilder query = new($@"
                SELECT {string.Join(", ", UserSelects.Simple)} 
                FROM {UserSchema.TableName}
                WHERE 1=1
            ");

            List<MySqlParameter> parameters = [];

            if (request.RoleId.HasValue)
            {
                query.Append($"\nAND {UserSchema.RoleId} = @roleId");
                parameters.Add(new("roleId", request.RoleId.Value));
            }

            if (request.CreatedAtStart != default)
            {
                query.Append($"\nAND {UserSchema.CreatedAt} > @createdAtStart");
                parameters.Add(new("createdAtStart", request.CreatedAtStart));
            }

            if (request.CreatedAtEnd != default)
            {
                query.Append($"\nAND {UserSchema.CreatedAt} < @createdAtEnd");
                parameters.Add(new("createdAtEnd", request.CreatedAtEnd));
            }

            if (request.UpdatedAtStart != default)
            {
                query.Append($"\nAND {UserSchema.UpdatedAt} > @updatedAtStart");
                parameters.Add(new("updatedAtStart", request.UpdatedAtStart));
            }

            if (request.UpdatedAtEnd != default)
            {
                query.Append($"\nAND {UserSchema.UpdatedAt} < @updatedAtEnd");
                parameters.Add(new("updatedAtEnd", request.UpdatedAtEnd));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query.Append(@$"
                    AND ({UserSchema.Username} LIKE @searchTerm 
                    OR {UserSchema.FullName} LIKE @searchTerm 
                    OR {UserSchema.Email} LIKE @searchTerm 
                    OR {UserSchema.PhoneNumberLastFour} LIKE @searchTerm)
                ");

                parameters.Add(new("searchTerm", $"{request.SearchTerm}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var orderBy = request.OrderBy.ToLower() switch
                {
                    "username" => UserSchema.Username,
                    "fullname" => UserSchema.FullName,
                    "role" => UserSchema.RoleId,
                    "createdate" => UserSchema.CreatedAt,
                    "updatedate" => UserSchema.UpdatedAt,
                    _ => UserSchema.Id
                };

                var orderDirection = request.OrderDirection.Equals("asc", StringComparison.OrdinalIgnoreCase)
                    ? "ASC"
                    : "DESC";

                query.Append($"\nORDER BY {orderBy} {orderDirection}");
            }

            query.Append($"\nLIMIT @limit OFFSET @offset;");

            parameters.Add(new("limit", request.Limit));
            parameters.Add(new("offset", request.Offset));

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Count);

            List<UserSimpleDto> result = [];

            await using var command = await db.CreateCommandAsync(query.ToString(), [..parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            var idOrdinal = reader.GetOrdinal(UserSchema.Id);
            var usernameOrdinal = reader.GetOrdinal(UserSchema.Username);
            var fullNameOrdinal = reader.GetOrdinal(UserSchema.FullName);

            while (await reader.ReadAsync())
            {
                result.Add(
                    new(
                        reader.GetInt32(idOrdinal),
                        reader.GetString(usernameOrdinal),
                        reader.GetString(fullNameOrdinal)
                    )
                );
            }

            return result;
        }

        public async Task<UserInfoDto?> GetByIdAsync(int id)
        {
            var query = $@"
                SELECT {string.Join(", ", UserSelects.Info)} 
                FROM {UserSchema.TableName}
                WHERE {UserSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            UserInfoDto? result = null;

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                result = new(
                    reader.GetInt32(UserSchema.Id),
                    reader.GetString(UserSchema.Username),
                    reader.GetString(UserSchema.FullName),
                    reader.GetString(UserSchema.Email),
                    reader.GetString(UserSchema.PhoneNumber)
                );
            }

            return result;
        }
    }
}
