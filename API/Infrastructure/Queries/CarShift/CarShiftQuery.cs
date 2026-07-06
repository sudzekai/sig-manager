using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Infrastructure.Schema.User;
using MySql.Data.MySqlClient;
using Shared.Types.Enums;
using System.Diagnostics;
using System.Text;
using Shared.Extensions;
using Contracts.Interfaces.Infrastructure.Context;


namespace Infrastructure.Queries.CarShift
{
    public class CarShiftQuery(IDbContext db)
    {
        public async Task<IReadOnlyList<CarShiftSimpleDto>> GetAllAsync(GetUsersListRequest request)
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

            await using (var reader = (MySqlDataReader)await db.ExecuteReaderAsync(query.ToString(), [.. parameters]))
            {
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
            }

            throw new NotImplementedException();
        }
    }
}
